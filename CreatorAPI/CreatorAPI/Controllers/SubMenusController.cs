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
        public IEnumerable<SimpleSubMenu> PostSubMenuList([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleSubMenu> ListOfSubMenus = new List<SimpleSubMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientSubMenus> Listcsm = db.ClientSubMenus.Where(c => c.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                           .Where(ca => ca.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                           .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                           .ToList();

            foreach(ClientSubMenus csmitem in Listcsm)
            {
                SimpleSubMenu csm = new SimpleSubMenu();
                csm.ID = csmitem.ID;
                csm.MenuID = csmitem.ClientMenus.ID;
                csm.Name = csmitem.Name;
                csm.PopupMessage = csmitem.PopupMessage;

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
                csm.Updated = ((DateTime)csmitem.ChangeDate).ToString("yyyy-MM-dd HH:mm:ss");

                ListOfSubMenus.Add(csm);
            }

            return ListOfSubMenus;
        }

        [Authorize]
        [Route("SubMenuMenuList")]
        public IEnumerable<SimpleSubMenu> PostSubMenuMenuList([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleSubMenu> ListOfSubMenus = new List<SimpleSubMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientSubMenuMenus> Listcsm = db.ClientSubMenuMenus.Where(c => c.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                                    .Where(ca => ca.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                                    .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                                    .ToList();

            foreach (ClientSubMenuMenus csmitem in Listcsm)
            {
                SimpleSubMenu csm = new SimpleSubMenu();
                csm.ID = csmitem.ID;
                csm.MenuID = csmitem.ClientMenus.ID;
                csm.Name = csmitem.Name;
                csm.PopupMessage = csmitem.PopupMessage;

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
                csm.Updated = ((DateTime)csmitem.ChangeDate).ToString("yyyy-MM-dd HH:mm:ss");

                ListOfSubMenus.Add(csm);
            }

            return ListOfSubMenus;
        }

        [Authorize]
        [Route("SubSubMenuList")]
        public IEnumerable<SimpleSubMenu> PostSubSubMenuList([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleSubMenu> ListOfSubMenus = new List<SimpleSubMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientSubSubMenus> Listcsm = db.ClientSubSubMenus.Where(c => c.ClientSubMenuMenus.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                                  .Where(ca => ca.ClientSubMenuMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                                  .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                                  .ToList();

            foreach (ClientSubSubMenus csmitem in Listcsm)
            {
                SimpleSubMenu csm = new SimpleSubMenu();
                csm.ID = csmitem.ID;
                csm.MenuID = csmitem.ClientSubMenuMenus.ID;
                csm.Name = csmitem.Name;
                csm.PopupMessage = csmitem.PopupMessage;

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
                csm.Updated = ((DateTime)csmitem.ChangeDate).ToString("yyyy-MM-dd HH:mm:ss");

                ListOfSubMenus.Add(csm);
            }

            return ListOfSubMenus;
        }

        [Authorize]
        [Route("Cleanup")]
        public IEnumerable<ActiveSubList> PostCleanup([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            //Legacy 
            string CurrentSubMenuList = "";
            string CurrentSubMenuMenuList = "";
            string CurrentSubSubMenuList = "";
            string UpperCaseCC = CompanyCode.Trim().ToUpper();

            CreatorEntities db = new CreatorEntities();
            List<ActiveSubList> CleanupList = new List<ActiveSubList>();

            //SubMenus
            List<ClientSubMenus> Listcsm = db.ClientSubMenus.Where(c => c.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                            .Where(ca => ca.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                            .ToList();
            foreach (ClientSubMenus csm in Listcsm)
            {
                CurrentSubMenuList = CurrentSubMenuList + csm.ID + ",";
            }

            if (Listcsm.Count > 0)
            {
                CurrentSubMenuList = "(" + CurrentSubMenuList.Substring(0, CurrentSubMenuList.Length - 1) + ")";
            }

            //SubMenuMenus
            List<ClientSubMenuMenus> Listcsmm = db.ClientSubMenuMenus.Where(c => c.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                                     .Where(ca => ca.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                                     .ToList();
            foreach (ClientSubMenuMenus csmm in Listcsmm)
            {
                CurrentSubMenuMenuList = CurrentSubMenuMenuList + csmm.ID + ",";
            }

            if (Listcsmm.Count > 0)
            {
                CurrentSubMenuMenuList = "(" + CurrentSubMenuMenuList.Substring(0, CurrentSubMenuMenuList.Length - 1) + ")";
            }

            //SubSubMenus
            List<ClientSubSubMenus> Listcssm = db.ClientSubSubMenus.Where(c => c.ClientSubMenuMenus.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                                   .Where(ca => ca.ClientSubMenuMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                                   .ToList();
            foreach (ClientSubSubMenus cssm in Listcssm)
            {
                CurrentSubSubMenuList = CurrentSubSubMenuList + cssm.ID + ",";
            }

            if (Listcssm.Count > 0)
            {
                CurrentSubSubMenuList = "(" + CurrentSubSubMenuList.Substring(0, CurrentSubSubMenuList.Length - 1) + ")";
            }

            ActiveSubList list = new ActiveSubList();
            list.SubMenus = CurrentSubMenuList;
            list.SubMenuMenus = CurrentSubMenuMenuList;
            list.SubSubMenus = CurrentSubSubMenuList;

            CleanupList.Add(list);

            return CleanupList;
        }
    }
}