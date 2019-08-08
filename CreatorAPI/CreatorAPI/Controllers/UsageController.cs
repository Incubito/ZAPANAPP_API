using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using Newtonsoft.Json.Linq;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Usage")]
    public class UsageController : ApiController
    {
        [Authorize]
        [Route("Rx")]
        public ProcessingResult PostRx([FromHeader] string UsageDetails)
        {
            ProcessingResult result = new ProcessingResult();

            try
            {
                JObject JSON = JObject.Parse(UsageDetails);
                List<JToken> JSONList = JSON.SelectToken("Usage").ToList();

                foreach (JToken Token in JSONList)
                {
                    string UUID = Token["UUID"].ToString();
                    string Timestamp = Token["Timestamp"].ToString();
                    string Description = Token["Description"].ToString();

                    CreatorEntities db = new CreatorEntities();
                    MobileConnections mobileconnection = db.MobileConnections.Single(mc => mc.UUID == UUID);

                    UsageTracker newusage = new UsageTracker();
                    newusage.MobileConnections = mobileconnection;
                    newusage.Timestamp = DateTime.Parse(Timestamp);
                    newusage.Description = Description;

                    db.UsageTracker.Add(newusage);
                    db.SaveChanges();
                }

                result.Message = "Sucessfully added";
                result.Status = "Usage Tracking";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = "Usage Tracking";
            }

            return result;
        }
    }
}
