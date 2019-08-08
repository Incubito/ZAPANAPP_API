using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using System.Data.Entity;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/XML")]
    public class XMLController : ApiController
    {
        [Authorize]
        [Route("CallBackRequest")]
        public ProcessingResult PostCallBackRequest([FromHeader] string CompanyCode, [FromHeader] string RequestContents)
        {
            ProcessingResult result;

            try
            {
                CreatorEntities db = new CreatorEntities();
                Clients client = db.Clients.Single(c => c.Code == CompanyCode);

                byte[] CallBackData = Convert.FromBase64String(RequestContents);
                string CallBackDecoded = System.Text.Encoding.UTF8.GetString(CallBackData);

                CallBackDecoded = CallBackDecoded.Replace(CompanyCode, client.Name);

                CallBackData = System.Text.Encoding.UTF8.GetBytes(CallBackDecoded);
                string CallBackEncoded = System.Text.Encoding.UTF8.GetString(CallBackData);

                ClientRequests newrequest = new ClientRequests();
                newrequest.Clients = client;
                newrequest.Type = "CallBack";
                newrequest.Contents = CallBackEncoded;
                newrequest.Status = "Submit";
                client.ClientRequests.Add(newrequest);

                db.SaveChanges();

                result = new ProcessingResult();
                result.Status = "Sucessfully submitted";
                result.Message = "";
            }
            catch(Exception ex)
            {
                result = new ProcessingResult();
                result.Status = "Error Occured";
                result.Message = ex.Message;
            }

            return result;
        }

        [Authorize]
        [Route("FDARequest")]
        public ProcessingResult PostFDARequest([FromHeader] string CompanyCode, [FromHeader] string RequestContents)
        {
            ProcessingResult result;

            try
            {
                CreatorEntities db = new CreatorEntities();
                Clients client = db.Clients.Single(c => c.Code == CompanyCode);

                byte[] FDAData = Convert.FromBase64String(RequestContents);
                string FDADecoded = System.Text.Encoding.UTF8.GetString(FDAData);

                FDADecoded = FDADecoded.Replace(CompanyCode, client.Name);

                FDAData = System.Text.Encoding.UTF8.GetBytes(FDADecoded);
                string FDAEncoded = System.Text.Encoding.UTF8.GetString(FDAData);

                ClientRequests newrequest = new ClientRequests();
                newrequest.Clients = client;
                newrequest.Type = "FDA";
                newrequest.Contents = FDAEncoded;
                newrequest.Status = "Submit";
                client.ClientRequests.Add(newrequest);

                db.SaveChanges();

                result = new ProcessingResult();
                result.Status = "Sucessfully submitted";
                result.Message = "";
            }
            catch (Exception ex)
            {
                result = new ProcessingResult();
                result.Status = "Error Occured";
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
