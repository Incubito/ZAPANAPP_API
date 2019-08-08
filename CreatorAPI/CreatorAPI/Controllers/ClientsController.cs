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
        [Route("Helpline")]
        public string PostGetHelpline([FromHeader] string ClientCode)
        {
            string ClientHelpLine = "";
            string UpperCaseCC = ClientCode.Trim().ToUpper();

            CreatorEntities db = new CreatorEntities();
            Clients client = db.Clients.Single(c => c.Code == UpperCaseCC);

            ClientHelpLine = client.Helpline;

            return ClientHelpLine;
        }

        [Authorize]
        [Route("Configuration")]
        public List<SimpleSettings> PostConfiguration([FromHeader] string ClientCode)
        {
            string UpperCaseCC = ClientCode.Trim().ToUpper();
            List<SimpleSettings> ListOfSettings = new List<SimpleSettings>();

            CreatorEntities db = new CreatorEntities();
            ListOfSettings = db.ClientSettings.Where(s => s.Clients.Code == UpperCaseCC).
                                                        Select(itm => new SimpleSettings
                                                        {
                                                            SettingKey = itm.SettingKey,
                                                            SettingValue = itm.SettingValue
                                                        }).ToList();

            return ListOfSettings;
        }
    }
}
