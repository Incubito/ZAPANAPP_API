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
    [RoutePrefix("api/Menus")]
    public class MenusController : ApiController
    {
        [Authorize]
        [Route("MenusList")]
        public IEnumerable<SimpleMenu> PostMenuList([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            List<SimpleMenu> ListOfMenus = new List<SimpleMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientMenus> Listcm = db.ClientMenus.Where(c => c.ClientApps.Clients.Code == CompanyCode)
                                                    .Where(ca => ca.ClientApps.Apps.AppCode == AppCode).ToList();

            foreach(ClientMenus cmitem in Listcm )
            {
                SimpleMenu cm = new SimpleMenu();
                cm.ID = cmitem.ID;
                cm.Name = cmitem.Name;

                if ((cmitem.ImageReference != "") && (File.Exists(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cmitem.ImageReference) == false))
                {
                    int StartIndex = cmitem.ImageReference.IndexOf("_") + 1;
                    int CopyLenght = cmitem.ImageReference.Length - (StartIndex);
                    string OriginalFilename = cmitem.ImageReference.Substring(StartIndex, CopyLenght);
                    File.Copy(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + OriginalFilename, CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cmitem.ImageReference);
                }

                cm.Icon = Convert.ToBase64String(File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cmitem.ImageReference));

                if ((cmitem.HeadingReference != "") && (File.Exists(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cmitem.HeadingReference) == false))
                {
                    int StartIndex = cmitem.HeadingReference.IndexOf("_") + 1;
                    int CopyLenght = cmitem.HeadingReference.Length - (StartIndex);
                    string OriginalFilename = cmitem.HeadingReference.Substring(StartIndex, CopyLenght);
                    File.Copy(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + OriginalFilename, CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cmitem.HeadingReference);
                }

                cm.Header = Convert.ToBase64String(File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + cmitem.HeadingReference));

                ListOfMenus.Add(cm);
            }

            return ListOfMenus;
        }

        [Authorize]
        [Route("Active")]
        public IEnumerable<String> PostActive([FromHeader]string CompanyCode)
        {
            List<string> ActiveList = new List<string>();

            return ActiveList;
        }
    }
}