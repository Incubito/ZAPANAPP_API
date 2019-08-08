using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using System.Data.Entity;
using System.IO;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/ScreenContent")]
    public class ScreenContentController : ApiController
    {
        [Authorize]
        [Route("ScreenContentList")]
        public IEnumerable<SimpleScreenContent> PostScreenContentList([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            List<SimpleScreenContent> ListOfScreenContent = new List<SimpleScreenContent>();

            CreatorEntities db = new CreatorEntities();
            List<ClientScreenContent> Listcsc = db.ClientScreenContent.Where(c => c.ClientScreens.ClientApps.Clients.Code == CompanyCode)
                                                                       .Where(ca => ca.ClientScreens.ClientApps.Apps.AppCode == AppCode).ToList();

            foreach (ClientScreenContent cscitem in Listcsc)
            {
                SimpleScreenContent ss = new SimpleScreenContent();
                ss.ID = cscitem.ID;
                ss.ScreenID = cscitem.ClientScreens.ID;
                ss.Name = cscitem.Name;
                ss.SCType = cscitem.Type;

                if ((cscitem.ImageReference != "") && (File.Exists(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cscitem.ImageReference) == false))
                {
                    int StartIndex = cscitem.ImageReference.IndexOf("_") + 1;
                    int CopyLenght = cscitem.ImageReference.Length - (StartIndex);
                    string OriginalFilename = cscitem.ImageReference.Substring(StartIndex, CopyLenght);
                    File.Copy(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + OriginalFilename, CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cscitem.ImageReference);
                }

                ss.Contents = Convert.ToBase64String(File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cscitem.ImageReference));

                ListOfScreenContent.Add(ss);
            }

            return ListOfScreenContent;
        }
    }
}