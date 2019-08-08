using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;

namespace CreatorAPI.Controllers
{
    [RoutePrefix ("api/ZenDesk")]
    public class ZenDeskController : ApiController
    {
        [Authorize]
        [Route("Subscriptions")]
        public IEnumerable<SimpleZenDeskInfo> PostSubscriptions([FromHeader]string CompanyCode)
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            List<SimpleZenDeskInfo> ListOfZendesks = new List<SimpleZenDeskInfo>();            

            CreatorEntities db = new CreatorEntities();
            ListOfZendesks = db.ClientZendesk.Where(zd => zd.Clients.Code == UpperCaseCC).
                                                        Select(itm => new SimpleZenDeskInfo
                                                        {
                                                            Name = itm.Zendesk.Name,
                                                            AccountKey = itm.Zendesk.AccountKey
                                                        }).ToList();

            return ListOfZendesks;
        }

        //[Authorize]
        //[Route("Active")]
        //public IEnumerable<String> PostActive([FromHeader]string CompanyCode)
        //{
        //    string UpperCaseCC = CompanyCode.Trim().ToUpper();
        //    List<string> ActiveList = new List<string>();

        //    CreatorEntities db = new CreatorEntities();
        //    ActiveList = db.ClientZendesk.Where(zd => zd.Clients.Code == UpperCaseCC).
        //                                                Select(zd => new string { zd.Zendesk.Name }).ToString();

        //    return ActiveList;
        //}
    }
}
