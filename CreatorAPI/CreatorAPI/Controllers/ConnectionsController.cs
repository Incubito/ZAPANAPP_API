using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using ICASCryptoGraphy;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Connections")]
    public class ConnectionsController : ApiController
    {
        //[Authorize]
        [Route("Registration")]
        public ProcessingResult PostRegistration([FromHeader] string ConnectionDetails)
        {
            JObject JSON = JObject.Parse(ConnectionDetails);

            string ConnectionIdentifier = "";
            string Firstname = JSON["Firstname"].ToString().Trim();
            string Surname = JSON["Surname"].ToString().Trim();
            string Cellphone = JSON["Cellphone"].ToString().Trim();
            string Email = JSON["Email"].ToString().Trim();
            string UUID = JSON["UUID"].ToString().Trim().ToUpper();
            string AppCode = JSON["AppCode"].ToString().Trim();
            string CompanyCode = JSON["CompanyCode"].ToString().Trim().ToUpper();

            ProcessingResult result = new ProcessingResult();
            CreatorEntities db = new CreatorEntities();
            LogManager ConnLog = new LogManager();

            try
            {
                if (Email != "")
                {
                    ConnectionIdentifier = Email;
                }
                else
                if (Cellphone != "")
                {
                    ConnectionIdentifier = Cellphone;
                }

                result.Status = "Company Code";

                ConnLog.Add(UUID, ConnectionIdentifier, "Validating Company Code");

                Clients client = db.Clients.Single(c => c.Code == CompanyCode);
                ClientApps clientapp = client.ClientApps.Single(ca => ca.Clients.Code == CompanyCode);

                ConnLog.Add(UUID, ConnectionIdentifier, "Company Code Validated");

                if (Email != "")
                {
                    result.Status = "Email Adress";

                    ConnLog.Add(UUID, ConnectionIdentifier, "Validating Email Adress");

                    int StartIndex = Email.IndexOf("@") + 1;
                    string EmailDomain = Email.Substring(StartIndex, Email.Length - StartIndex);

                    TrustedEmailDomains emaildomain = client.TrustedEmailDomains.Single(ted => ted.DomainName.Trim().ToUpper() == EmailDomain.Trim().ToUpper());

                    ConnLog.Add(UUID, ConnectionIdentifier, "Email Adress found in list of Trusted Email Domains");
                }

                ConnLog.Add(UUID, ConnectionIdentifier, "Registering User Details");

                result.Status = "Mobile Connection";

                try
                {
                    MobileConnections checkconnection = db.MobileConnections.Single(mc => mc.UUID == UUID);
                    checkconnection.Firstname = Firstname;
                    checkconnection.Surname = Surname;
                    checkconnection.Cellphone = Cellphone;
                    checkconnection.EmailAdress = Email;
                }
                catch(Exception ex)
                {
                    MobileConnections newconnection = new MobileConnections();
                    newconnection.ClientApps = clientapp;
                    newconnection.Firstname = Firstname;
                    newconnection.Surname = Surname;
                    newconnection.Cellphone = Cellphone;
                    newconnection.EmailAdress = Email;
                    newconnection.UUID = UUID;

                    db.MobileConnections.Add(newconnection);
                }

                db.SaveChanges();

                ConnLog.Add(UUID, ConnectionIdentifier, "User Details Registration Sucessfull");
                ConnLog.SavetoSQL();

                result.Status = "Registration";
                result.Message = "Sucessfull";
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Sequence contains no") >= 0)
                {
                    result.Message = "Not Found";
                }
                else
                {
                    result.Message = "Unable to Complete Registration";
                }

                ConnLog.Add(UUID, ConnectionIdentifier, ex.Message);
                ConnLog.SavetoSQL();
            }

            return result;
        }

        [Authorize]
        [Route("UpdatePassword")]
        public ProcessingResult PostUpdatePassword ([FromHeader] string UUID, [FromHeader] string Password)
        {
            ProcessingResult result = new ProcessingResult();

            try
            {
                ICASCrypto EncryptedPassword = new ICASCrypto(Password.Trim(), true);

                CreatorEntities db = new CreatorEntities();
                MobileConnections mobileconnection = db.MobileConnections.Single(mc => mc.UUID == UUID.Trim().ToUpper());
                mobileconnection.Password = EncryptedPassword.StringToStringEncryption();
                db.SaveChanges();

                result.Status = "Mobile Users";
                result.Message = "Sucessfully Updated Password";
            }
            catch(Exception ex)
            {
                result.Status = "Mobile Users";
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
