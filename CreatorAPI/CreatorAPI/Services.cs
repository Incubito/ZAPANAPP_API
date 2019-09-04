using CreatorAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace CreatorAPI
{
    public class Services
    {
        public static List<ItemType> ItemTypes = CacheItemTypes();
        public static MieAgent Agent = GetAgent();
        public static MieClient Client = GetClient();
        public static List<MieTable> Tables = CacheTables();

        public static string GetGreeting()
        {
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var greeting = mieClient.GetGreeting();
            return greeting;
        }

       

        public static EnvironmentData Authenticate()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoAuthenticate(GetLogin());

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(EnvironmentData));
            using (var reader = new StringReader(data))
            {
                return (EnvironmentData)serializer.Deserialize(reader);
            }
        }

        public static MieAgentsModel GetAgents()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetAgents(GetLogin());

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(MieAgentsModel));
            using (var reader = new StringReader(data))
            {
                return (MieAgentsModel)serializer.Deserialize(reader);
            }
        }

        public static MieClients GetClients()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetClients(GetLogin());

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(MieClients));
            using (var reader = new StringReader(data))
            {
                return (MieClients)serializer.Deserialize(reader);
            }
        }

        public static MieClients GetClientTree()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetClientTree(GetLogin());

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(MieClients));
            using (var reader = new StringReader(data))
            {
                return (MieClients)serializer.Deserialize(reader);
            }
        }

        public static MieTables GetTables()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetTables(GetLogin());

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(MieTables));
            using (var reader = new StringReader(data))
            {
                return (MieTables)serializer.Deserialize(reader);
            }
        }

        public static string GetRequestRedirect()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var argumentModel = string.Format("<?xml version=\"1.0\" encoding=\"utf - 8\"?>" +
                "<xml>" +
                  "<ArgumentList>" +
                  "<Argument>" +
                  "<Name>ClientKey</Name>" +
                  "<Value>{0}</Value>" +
                  "</Argument>" +
                  "<Argument>" +
                  "<Name>RequestKey</Name>" +
                  "<Value>{1}</Value>" +
                  "</Argument>" +
                  "</ArgumentList>" +
                  "</xml>", Client.Key, "9066702");

            var data = mieClient.ksoGetRequest(GetLogin(), argumentModel);

            return data;

            //// Serialise result
            //XmlSerializer serializer = new XmlSerializer(typeof(MieTables));
            //using (var reader = new StringReader(data))
            //{
            //    return (MieTables)serializer.Deserialize(reader);
            //}
        }

        public static string GetTable(string key)
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetTable(GetLogin(), key);
            return data;
            // Serialise result
            //XmlSerializer serializer = new XmlSerializer(typeof(MieTables));
            //using (var reader = new StringReader(data))
            //{
            //    return (MieTables)serializer.Deserialize(reader);
            //}
        }

        public static AddInfoReportWrapper GetAddInfo()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetAddInfo(GetLogin());

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(AddInfoReportWrapper));
            using (var reader = new StringReader(data))
            {
                return (AddInfoReportWrapper)serializer.Deserialize(reader);
            }
        }

        public static MieItemTypes GetItemTypes()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetItemTypes(GetLogin("SMARTWEB"));

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(MieItemTypes));
            using (var reader = new StringReader(data))
            {
                return (MieItemTypes)serializer.Deserialize(reader);
            }
        }

        public static string GetItemTypeGroups()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetItemTypeGroups(GetLogin("SMARTWEB"));
            return data;

            //// Serialise result
            //XmlSerializer serializer = new XmlSerializer(typeof(MieTables));
            //using (var reader = new StringReader(data))
            //{
            //    return (MieTables)serializer.Deserialize(reader);
            //}
        }

        public static string GetPrerequisites()
        {
            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var data = mieClient.ksoGetPrerequisites(GetLogin());
            return data;

            //// Serialise result
            //XmlSerializer serializer = new XmlSerializer(typeof(MieTables));
            //using (var reader = new StringReader(data))
            //{
            //    return (MieTables)serializer.Deserialize(reader);
            //}
        }

        public static MieRequestResponseModel PutRequest(MieRequestModel request)
        {
            // serialise model to xml
            var requestXml = ModelToString(ref request);
            requestXml = requestXml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            requestXml = requestXml.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");

            // Request authentication
            var mieClient = new MieProducer.epcvServiceSoapClient();
            var alogon = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><xml><Token><UserName>{0}</UserName><Password>{1}</Password><Source>{2}</Source></Token></xml>",
                Constants.MieUsername, Constants.MiePassword, Constants.MieSource);
            var data = mieClient.ksoPutRequest(alogon, requestXml);

            // Serialise result
            XmlSerializer serializer = new XmlSerializer(typeof(MieRequestResponseModel));
            using (var reader = new StringReader(data))
            {
                var response = (MieRequestResponseModel)serializer.Deserialize(reader);

                // log error
                //Methods.WriteToLog(string.Format("Request {0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), requestXml));
                //Methods.WriteToLog(string.Format("Response: {0} {1}", response.Status.Code, response.Status.Description));

                return response;
            }
        }

        public void PutRequestDocument(int requestKey, string filename, byte[] data)
        {
            try
            {
                var mieClient = new MieRequestWCF.IepcvRequestWCFClient();
                var token = new MieRequestWCF.Token
                {
                    Password = Constants.MiePassword,
                    Source = Constants.MieSource,
                    UserName = Constants.MieUsername
                };

                mieClient.PutRequestDocument(token, requestKey, filename, data);
            }
            catch (Exception ex)
            {
                
                throw new ArgumentException("Failed to upload document.");
            }
        }

    

        private static string ModelToString<T>(ref T model)
        {
            XmlSerializer serializer = new XmlSerializer(model.GetType());

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, model, null);
                    return sww.ToString(); // Your XML
                }
            }
        }

        private static MieAgent GetAgent()
        {
            var agents = GetAgents();

            if (agents == null || agents.Status == null || agents.Status.Code != "0" || agents.AgentList == null || agents.AgentList.Agent == null)
                return null;
            else
                return agents.AgentList.Agent;
        }

        private static MieClient GetClient()
        {
            var clients = GetClientTree();

            if (clients == null || clients.Status == null || clients.Status.Code != "0" || clients.ClientList == null || clients.ClientList.Client == null)
                return null;
            else
                return clients.ClientList.Client;
        }

        private static List<ItemType> CacheItemTypes()
        {
            try
            {
                CreatorAPI.ExternalModel.VALINFOEntities vALINFOEntities = new ExternalModel.VALINFOEntities();
                ExternalModel.VerificationProduct productManager = new ExternalModel.VerificationProduct();
                var data = GetItemTypes();

                if (data == null || data.Status == null || data.Status.Code != "0" || data.ItemTypeList == null || data.ItemTypeList.ItemType == null)
                    return new List<ItemType>();
                else
                {
                    // Get South African item types
                    var itemTypes = data.ItemTypeList.ItemType.Where(x => x.CountryName.Equals("South Africa", StringComparison.InvariantCultureIgnoreCase)).ToList();

                    foreach (var i in itemTypes)
                    {
                        // Get existing product
                        var existingProduct = vALINFOEntities.VerificationProducts.Single(x => x.ItemCode == i.Code);

                        if (existingProduct == null)
                        {
                            // Create new item type
                            productManager = (new ExternalModel.VerificationProduct
                            {
                                IndemnityPrompt = i.IndemnityPrompt,
                                IndemnityText = i.IndemnityText,
                                IndemnityType = i.IndemnityType,
                                ItemCode = i.Code,
                                ItemPrice = i.ItemPrice,
                                Name = i.Name,
                                IsActive = false,
                                IsArchived = false,
                                HasAttributes = i.InputGroupList.InputGroup != null,
                                SellingPrice = 0
                            });
                            vALINFOEntities.VerificationProducts.Add(productManager);
                        }
                        else
                        {
                            // Update item type
                            existingProduct.IndemnityPrompt = i.IndemnityPrompt;
                            existingProduct.IndemnityText = i.IndemnityText;
                            existingProduct.IndemnityType = i.IndemnityType;
                            existingProduct.Name = i.Name;
                            existingProduct.HasAttributes = i.InputGroupList.InputGroup != null;
                            existingProduct.IsArchived = false;

                        }
                    }

                    // Delete removed item types
                    //var itemsToDelete = vALINFOEntities.VerificationProducts.Where(x => !itemTypes.Select(i => i.Code).Contains(x.ItemCode));

                    //foreach (var itd in itemsToDelete)
                    //{
                    //    itd.IsArchived = true;
                    //    itd.IsActive = false;
                    //}

                    // Save changes
                    vALINFOEntities.SaveChanges();

                    return itemTypes;
                }
            }
            catch (Exception ex)
            {
                //Methods.WriteToLog(string.Format("{0} Mie Get Item Types Error: {1}", DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss tt"), ex.Message));
                return null;
            }
        }

        private static List<MieTable> CacheTables()
        {
            var tables = GetTables();

            if (tables.Status != null && Convert.ToInt32(tables.Status.Code) == 0 && tables.TableList != null && tables.TableList.Table != null)
            {
                return tables.TableList.Table;
            }
            else
                return null;
        }

        private static string GetLogin(string source = null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LoginXml));
            var login = new LoginXml
            {
                Token = new Token
                {
                    Password = Constants.MiePassword,
                    Source = !string.IsNullOrEmpty(source) ? source : Constants.MieSource,
                    UserName = Constants.MieUsername
                }
            };

            var token = ModelToString(ref login);
            return token;
        }
    }
}