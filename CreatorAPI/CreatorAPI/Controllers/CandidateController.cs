using CreatorApi;
using CreatorAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Net.Http;



namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Candidate")]
    public class CandidateController : ApiController
    {
        // GET: Candidate
        
        [Route("AddCandidate")]
        public string PostAddCandidate([FromHeader]string firstname, [FromHeader]string surname, [FromHeader] string Idnumber, [FromHeader] string CompanyName, [FromHeader] string IDVerification, [FromHeader] string CrimCheck, [FromHeader] string Username=null)
        {

            string result = "";
            try
            {
                CreatorEntities db = new CreatorEntities();

                CreatorAPI.ExternalModel.VALINFOEntities vALINFOEntities = new ExternalModel.VALINFOEntities();
                CreatorAPI.ExternalModel.Client client = vALINFOEntities.Clients.Single(x => x.CompanyName == CompanyName);
                CreatorAPI.ExternalModel.Candidate candidate = new ExternalModel.Candidate();
                candidate.Firstname = firstname;
                candidate.Surname = surname;
                candidate.IDNumber = Idnumber;
                candidate.ClientID = client.ClientID;
                candidate.AppUserID = User.Identity.GetUserName();
                vALINFOEntities.Candidates.Add(candidate);
                vALINFOEntities.SaveChanges();


               var localRequest = new ExternalModel.MieRequest()
                {
                    CandidateID = candidate.CandidateID,
                    ClientID = candidate.ClientID,
                    Date = DateTime.Now,
                    RequestType = 0,
                    UserID = User.Identity.GetUserId()
                };
                vALINFOEntities.MieRequests.Add(localRequest);
                vALINFOEntities.SaveChanges();
                if (candidate != null && Convert.ToBoolean(IDVerification))
                {

                    ItemTypeViewModel itemTypeViewModel = new ItemTypeViewModel();
                    itemTypeViewModel.Code = "TUSAID";
                    itemTypeViewModel.Name = "ID Verification";

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
                        DateOfBirth = candidate.BirthDate.HasValue ? candidate.BirthDate.Value.ToString("s") :IdNumberToDateOfBirth(candidate.IDNumber),
                        Source = Constants.MieRequestSource,
                        RemoteCaptureDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK"),
                        RemoteSendDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK"),
                        ContactNumber = candidate.Cellphone,
                        RemoteRequest = localRequest.RequestID.ToString(),
                        PrerequisiteGroupList = new PrerequisiteGroupList(),
                        ItemList = new ItemList() { Item = new List<MieItem>() }
                    };
                    foreach (var item in items.Select((value, i) => new { i, value }))
                    {

                        var requestItem = new ExternalModel.MieRequestItem
                        {
                            IsPackageItem = false,
                            Amount = GetProductAmount(item.value.Code, client.Category, client.ClientID, null),
                            ClientID = candidate.ClientID,
                            ItemCode = item.value.Code,
                            RequestID = localRequest.RequestID
                        };
                        vALINFOEntities.MieRequestItems.Add(requestItem);
                        vALINFOEntities.SaveChanges();

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
                                vALINFOEntities.SaveChanges();
                            }

                            var item = vALINFOEntities.VerificationProducts.Where(x => x.ItemCode == itemTypeViewModelList.ToList().First().Code);
                            var type = item != null ? item.Select(x => x.Type) : null;
                            var typename = type != null ? ((ProductType)Convert.ToInt32(type)).ToString() : "";

                            

                         
                            candidate.VettingStatus = 1;
                            
                            candidate.DateModified = DateTime.UtcNow.AddHours(2);



                            return string.Empty;
                        }
                        else
                        {
                           
                            var itemsToDelete = vALINFOEntities.MieRequestItems.Where(x=>x.RequestID== localRequest.RequestID);
                            vALINFOEntities.MieRequestItems.RemoveRange(itemsToDelete);
                            vALINFOEntities.MieRequests.Remove(localRequest);
                            vALINFOEntities.SaveChanges();
                            return data.Status.Description;
                        }
                    }
                    catch (Exception ex)
                    {

                        var itemsToDelete = vALINFOEntities.MieRequestItems.Where(x => x.RequestID == localRequest.RequestID);
                        vALINFOEntities.MieRequestItems.RemoveRange(itemsToDelete);
                        vALINFOEntities.MieRequests.Remove(localRequest);
                        vALINFOEntities.SaveChanges();
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
            CreatorAPI.ExternalModel.VALINFOEntities vALINFOEntities = new ExternalModel.VALINFOEntities();
            var product = vALINFOEntities.VerificationProducts.Where(x => x.ItemCode == code);
            if (product != null)
            {
                var clientPricing = vALINFOEntities.ProductPriceEntries.Where(x => (x.VerificationProduct.ItemCode == code) && (x.ClientID == clientID));
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
                        return DateTime.ParseExact(dateString, "yyMMdd", CultureInfo.CurrentCulture).ToString((!string.IsNullOrEmpty(format) ? format : "s"));
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