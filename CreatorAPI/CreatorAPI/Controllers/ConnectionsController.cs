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
        [Authorize]
        [Route("UpdatePassword")]
        public ProcessingResult PostUpdatePassword([FromHeader] string Request)
        {
            ProcessingResult result = new ProcessingResult();

            IncubitoCryptoGraphy.IncubitoCrypto EncryptedRequest = new IncubitoCryptoGraphy.IncubitoCrypto(Request, true);
            string DecryptedRequest = EncryptedRequest.StringToStringDecryption();

            JObject JSON = JObject.Parse(DecryptedRequest);

            string UUID = JSON["UUID"].ToString().Trim();
            string Password = JSON["Password"].ToString().Trim();

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
