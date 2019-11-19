using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        [Authorize]
        [Route("HelplineNr")]
        public IEnumerable<SimpleHelplineNr> PostGetHelplineNr([FromHeader] string ClientCode)
        {
            List<SimpleHelplineNr> ClientHelpLineList = new List<SimpleHelplineNr>();
            string UpperCaseCC = ClientCode.Trim().ToUpper();

            CreatorEntities creatordb = new CreatorEntities();
            Clients client = creatordb.Clients.Single(c => c.Code == UpperCaseCC);

            SimpleHelplineNr ClientHelpLine = new SimpleHelplineNr();
            ClientHelpLine.Helpline = client.Helpline;

            ClientHelpLineList.Add(ClientHelpLine);

            return ClientHelpLineList;
        }

        [Authorize]
        [Route("Configuration")]
        public List<SimpleSettings> PostConfiguration([FromHeader] string ClientCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            string UpperCaseCC = ClientCode.Trim().ToUpper();

            List<SimpleSettings> ListOfSettings = new List<SimpleSettings>();
            List<ActiveList> CleanupList = new List<ActiveList>();
            CreatorEntities creatordb = new CreatorEntities();
            ListOfSettings = creatordb.ClientSettings.Where(s => s.Clients.Code == UpperCaseCC)
                                              .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                              .Select(itm => new SimpleSettings
                                               {
                                                   SettingKey = itm.SettingKey,
                                                   SettingValue = itm.SettingValue,
                                                   Updated = itm.ChangeDate.ToString()
                                              })
                                              .ToList();

            return ListOfSettings;
        }

        [Authorize]
        [Route("Messages")]
        public List<SimplePN> PostMessages([FromHeader] string ClientCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            string UpperCaseCC = ClientCode.Trim().ToUpper();

            List<SimplePN> ListOfPNs = new List<SimplePN>();
            CreatorEntities creatordb = new CreatorEntities();
            ListOfPNs = creatordb.BulkPushNotifications.Where(s => s.Clients.Code == UpperCaseCC)
                                                .Where(s => s.Status == "Sent")
                                                .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.DateSent, LastSyncDate) < 0)
                                                .Select(itm => new SimplePN
                                                {
                                                    Title = itm.Title,
                                                    Message = itm.Message,
                                                    DateSent = (DateTime)itm.DateSent
                                                })
                                                .ToList();

            return ListOfPNs;
        }

        [Route("AddCandidate")]
        public string PostAddCandidate([FromHeader]string Firstname, [FromHeader]string Surname, [FromHeader] string Idnumber, [FromHeader] string CompanyName, [FromHeader] string IDVerification, [FromHeader] string CrimCheck)
        {

            string result = "";
            try
            {
                CreatorEntities creatordb = new CreatorEntities();

                VALINFOEntities valinfodb = new VALINFOEntities();
                string username = User.Identity.GetUserName().ToString();
                Client client = valinfodb.Clients.Single(x => x.CompanyName == CompanyName);
                Candidate candidate = valinfodb.Candidates.Where(x => x.IDNumber == Idnumber && x.ClientID == client.ClientID && x.AppUserID == username).SingleOrDefault();
                if (candidate == null)
                {
                    candidate.Firstname = Firstname;
                    candidate.Surname = Surname;
                    candidate.IDNumber = Idnumber;
                    candidate.Cellphone = "0760619183";
                    candidate.ClientID = client.ClientID;
                    candidate.AppUserID = User.Identity.GetUserName();
                    valinfodb.Candidates.Add(candidate);
                    valinfodb.SaveChanges();
                }

                var localRequest = new MieRequest()
                {
                    CandidateID = candidate.CandidateID,
                    ClientID = candidate.ClientID,
                    Date = DateTime.Now,
                    RequestType = 0,
                    UserID = "433a858b-3b33-4c65-92df-b8eea190ecf9"
                };
                valinfodb.MieRequests.Add(localRequest);
                valinfodb.SaveChanges();
                if (candidate != null && Convert.ToBoolean(IDVerification))
                {

                    ItemTypeViewModel itemTypeViewModel = new ItemTypeViewModel();
                    itemTypeViewModel.Code = "IDALG";
                    itemTypeViewModel.Name = "MIE ID Validation";
                    itemTypeViewModel.IsActive = true;

                    List<ItemTypeViewModel> itemTypeViewModelList = new List<ItemTypeViewModel>();
                    itemTypeViewModelList.Add(itemTypeViewModel);
                    var items = new List<ItemType>();
                    items = Services.ItemTypes.Join(itemTypeViewModelList, x => x.Code, i => i.Code, (x, i) => x).Distinct().ToList();
                    var requestModel = new MieRequestModel();
                    requestModel.Request = new Models.Request()
                    {
                        ClientKey = Services.Client.Key,
                        AgentKey = Services.Agent.Key,
                        AgentClient = Services.Agent.ClientKey,
                        FirstNames = candidate.Firstname,
                        Surname = candidate.Surname,
                        KnownAs = candidate.Firstname,
                        IdNumber = !string.IsNullOrWhiteSpace(candidate.IDNumber) ? candidate.IDNumber : "",
                        Passport = !string.IsNullOrWhiteSpace(candidate.Passport) ? candidate.Passport : "",
                        DateOfBirth = candidate.BirthDate.HasValue ? candidate.BirthDate.Value.ToString("s") : IdNumberToDateOfBirth(candidate.IDNumber),
                        Source = Constants.MieRequestSource,
                        RemoteCaptureDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK"),
                        RemoteSendDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK"),
                        ContactNumber = "",
                        RemoteRequest = localRequest.RequestID.ToString(),
                        PrerequisiteGroupList = new PrerequisiteGroupList(),
                        ItemList = new ItemList() { Item = new List<MieItem>() }
                    };
                    foreach (var item in items.Select((value, i) => new { i, value }))
                    {

                        var requestItem = new MieRequestItem
                        {
                            IsPackageItem = false,
                            Amount = GetProductAmount(item.value.Code, client.Category, client.ClientID, null),
                            ClientID = candidate.ClientID,
                            ItemCode = item.value.Code,
                            RequestID = localRequest.RequestID
                        };
                        valinfodb.MieRequestItems.Add(requestItem);
                        valinfodb.SaveChanges();

                        requestModel.Request.ItemList.Item.Add(new MieItem
                        {
                            RemoteItemKey = requestItem.RequestItemID.ToString(),
                            ItemTypeCode = item.value.Code,
                            Indemnity = (!string.IsNullOrEmpty(item.value.IndemnityType) && !item.value.IndemnityType.Equals("none", StringComparison.InvariantCultureIgnoreCase)) ? "true" : "false",

                            // Handle Input groups and attributes
                            ItemInputGroupList = GetInputGroupList(item.value, itemTypeViewModelList)
                        });
                    }
                    try
                    {
                        // Post request
                        var data = Services.PutRequest(requestModel);

                        // Create user action
                        if (data != null && data.Status != null && data.Status.Code == "0")
                        {
                            if (data.Request != null)
                            {
                                localRequest.Status = (int)MieRequestStatus.NoResult; // No results returned yet.
                                localRequest.MieRequestID = Convert.ToInt64(data.Request.RequestKey);
                                valinfodb.SaveChanges();
                            }


                            candidate.VettingStatus = 1;

                            candidate.DateModified = DateTime.UtcNow.AddHours(2);

                            valinfodb.SaveChanges();


                        }
                        else
                        {

                            var itemsToDelete = valinfodb.MieRequestItems.Where(x => x.RequestID == localRequest.RequestID);
                            valinfodb.MieRequestItems.RemoveRange(itemsToDelete);
                            valinfodb.MieRequests.Remove(localRequest);
                            valinfodb.SaveChanges();
                            return data.Status.Description;
                        }
                    }
                    catch (Exception ex)
                    {

                        var itemsToDelete = valinfodb.MieRequestItems.Where(x => x.RequestID == localRequest.RequestID);
                        valinfodb.MieRequestItems.RemoveRange(itemsToDelete);
                        valinfodb.MieRequests.Remove(localRequest);
                        valinfodb.SaveChanges();
                        return "Request failed, please try again.";
                    }
                }
                if (candidate != null && Convert.ToBoolean(CrimCheck))
                {


                }
                result = "success";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;

            }
            return result;
        }



        private decimal GetProductAmount(string code, int category, int clientID, int? packageID = null)
        {
            VALINFOEntities valinfodb = new VALINFOEntities();
            var product = valinfodb.VerificationProducts.Where(x => x.ItemCode == code); if (product != null)
            {

                var clientPricing = valinfodb.ProductPriceEntries.Where(x => (x.VerificationProduct.ItemCode == code) && (x.ClientID == clientID));
                return clientPricing != null ? clientPricing.Select(x => x.Price).Single() : product.Select(x => x.SellingPrice.Value).Single();
            }
            else
                return 0;

        }

        private ItemInputGroupList GetInputGroupList(ItemType itemType, IEnumerable<ItemTypeViewModel> products)
        {
            var inputGroupList = new ItemInputGroupList();

            if (itemType.InputGroupList.InputGroup == null || itemType.InputGroupList.InputGroup.InputAttributeList == null)
                return inputGroupList;
            else
            {
                // Get attributes
                var inputAttributes = products.Where(x => x.Code == itemType.Code && x.InputAttributes != null).SelectMany(x => x.InputAttributes).ToList();

                if (inputAttributes.Count > 0)
                {
                    inputGroupList.ItemInputGroup = new ItemInputGroup();
                    inputGroupList.ItemInputGroup.ItemInputLineList = new ItemInputLineList();
                    inputGroupList.ItemInputGroup.ItemInputLineList.ItemInputLine = new ItemInputLine();
                    inputGroupList.ItemInputGroup.ItemInputLineList.ItemInputLine.ItemAttributeList = new ItemAttributeList();
                    var attributeList = inputGroupList.ItemInputGroup.ItemInputLineList.ItemInputLine.ItemAttributeList.ItemAttribute = new List<ItemAttribute>();

                    foreach (var inputLine in inputAttributes)
                    {
                        attributeList.Add(new ItemAttribute()
                        {
                            SystemCode = inputLine.SystemCode,
                            Value = inputLine.Value
                        });
                    }
                }

                return inputGroupList;
            }
        }
        public static string IdNumberToDateOfBirth(string idNumber, string format = null)
        {
            if (string.IsNullOrWhiteSpace(idNumber))
                return string.Empty;
            else
            {
                try
                {
                    int number;
                    var dateString = idNumber.Substring(0, 6);

                    if (dateString.Length == 6 && int.TryParse(dateString, out number))
                    {
                        return DateTime.ParseExact(dateString, "yyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString((!string.IsNullOrEmpty(format) ? format : "s"));
                    }
                    else
                    {
                        throw new ArgumentException("Invalid ID number.");
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }
    }
}
