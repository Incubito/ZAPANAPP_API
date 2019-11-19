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
        public IEnumerable<SimpleMenu> PostMenuList([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate)
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleMenu> ListOfMenus = new List<SimpleMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientMenus> Listcm = db.ClientMenus.Where(c => c.ClientApps.Clients.Code == UpperCaseCC)
                                                    .Where(ca => ca.ClientApps.Apps.AppCode == AppCode)
                                                    .Where(ca => ca.Name == "MY BENEFITS")
                                                    .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                    .ToList();

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
                cm.Updated = ((DateTime)cmitem.ChangeDate).ToString("yyyy-MM-dd HH:mm:ss");

                ListOfMenus.Add(cm);
            }

            return ListOfMenus;
        }

        [Authorize]
        [Route("MenusListV2")]
        public IEnumerable<SimpleMenu> PostMenuListV2([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleMenu> ListOfMenus = new List<SimpleMenu>();

            CreatorEntities db = new CreatorEntities();
            List<ClientMenus> Listcm = db.ClientMenus.Where(c => c.ClientApps.Clients.Code == UpperCaseCC)
                                                    .Where(ca => ca.ClientApps.Apps.AppCode == AppCode)
                                                    .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                    .ToList();

            foreach (ClientMenus cmitem in Listcm)
            {
                SimpleMenu cm = new SimpleMenu();
                cm.ID = cmitem.ID;
                cm.Name = cmitem.Name;
                cm.Side = (bool)cmitem.SideMenu;
                cm.Bottom = (bool)cmitem.BottomMenu;
                cm.Slide = (bool)cmitem.ScrollPage;
                cm.Action = cmitem.MenuAction;
                cm.Order = (int)cmitem.MenuOrder;

                if ((cmitem.ImageReference != null) && (cmitem.HeadingReference != null))
                {
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
                }
                else
                {
                    cm.Icon = "";
                    cm.Header = "";
                }

                cm.Updated = ((DateTime)cmitem.ChangeDate).ToString("yyyy-MM-dd HH:mm:ss");

                ListOfMenus.Add(cm);
            }

            return ListOfMenus;
        }

        [Authorize]
        [Route("Cleanup")]
        public IEnumerable<ActiveList> PostCleanUp([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            string CurrentList = "";
            string UpperCaseCC = CompanyCode.Trim().ToUpper();

            CreatorEntities db = new CreatorEntities();
            List<ActiveList> CleanupList = new List<ActiveList>();
            List<ClientMenus> Listcm = db.ClientMenus.Where(c => c.ClientApps.Clients.Code == UpperCaseCC)
                                                     .Where(ca => ca.ClientApps.Apps.AppCode == AppCode)
                                                     .ToList();

            foreach (ClientMenus cm in Listcm)
            {
                CurrentList = CurrentList + cm.ID + ",";
            }

            if (Listcm.Count > 0)
            {
                CurrentList = "(" + CurrentList.Substring(0, CurrentList.Length - 1) + ")";
            }

            ActiveList lib = new ActiveList();
            lib.List = CurrentList;

            CleanupList.Add(lib);

            return CleanupList;
        }
    }
}