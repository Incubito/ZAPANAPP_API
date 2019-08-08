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
    [RoutePrefix("api/SubMenus")]
    public class SubMenusController : ApiController
    {
        [Authorize]
        [Route("SubMenuList")]
        public IEnumerable<SimpleSubMenu> PostSubMenuList([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            List<SimpleSubMenu> ListOfSubMenus = new List<SimpleSubMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientSubMenus> Listcsm = db.ClientSubMenus.Where(c => c.ClientMenus.ClientApps.Clients.Code == CompanyCode)
                                                           .Where(ca => ca.ClientMenus.ClientApps.Apps.AppCode == AppCode).ToList();

            foreach(ClientSubMenus csmitem in Listcsm)
            {
                SimpleSubMenu csm = new SimpleSubMenu();
                csm.ID = csmitem.ID;
                csm.MenuID = csmitem.ClientMenus.ID;
                csm.Name = csmitem.Name;

                if ((csmitem.ImageReference != "") && (File.Exists(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + csmitem.ImageReference) == false))
                {
                    int StartIndex = csmitem.ImageReference.IndexOf("_") + 1;
                    int CopyLenght = csmitem.ImageReference.Length - (StartIndex);
                    string OriginalFilename = csmitem.ImageReference.Substring(StartIndex, CopyLenght);
                    File.Copy(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + OriginalFilename, CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + csmitem.ImageReference);
                }

                csm.Image = Convert.ToBase64String(File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + csmitem.ImageReference));

                if ((csmitem.HeadingReference != "") && (File.Exists(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + csmitem.HeadingReference) == false))
                {
                    int StartIndex = csmitem.HeadingReference.IndexOf("_") + 1;
                    int CopyLenght = csmitem.HeadingReference.Length - (StartIndex);
                    string OriginalFilename = csmitem.HeadingReference.Substring(StartIndex, CopyLenght);
                    File.Copy(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + OriginalFilename, CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + csmitem.HeadingReference);
                }

                csm.Header = Convert.ToBase64String(File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ServerPath + "\\" + csmitem.HeadingReference));

                ListOfSubMenus.Add(csm);
            }

            return ListOfSubMenus;
        }
    }
}