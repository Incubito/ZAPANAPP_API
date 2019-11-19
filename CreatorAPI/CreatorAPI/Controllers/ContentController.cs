using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using System.Data.Entity;
using System.IO;
using System.Text;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Content")]
    public class ContentController : ApiController
    {
        [Authorize]
        [Route("ContentList")]
        public IEnumerable<SimpleContent> PostContentList([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleContent> ListOfContent = new List<SimpleContent>();

            CreatorEntities db = new CreatorEntities();
            ListOfContent = db.ClientContent.Where(c => c.ClientSubMenus.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                             .Where(ca => ca.ClientSubMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                             .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                             .Select(itm => new SimpleContent
                                             {
                                                ID = itm.ID,
                                                SubMenuID = itm.ClientSubMenus.ID,
                                                CategoryName = itm.Categories.Name,
                                                Description = itm.Library.Description,
                                                FileType = itm.Library.Type,
                                                Filename = itm.Library.Filename,
                                                Preview = itm.Library.Preview,
                                                Updated = itm.ChangeDate.ToString()
                                             })
                                             .ToList();
            return ListOfContent;
        }

        [Authorize]
        [Route("SubContentList")]
        public IEnumerable<SimpleContent> PostSubContentList([FromHeader]string AppCode, [FromHeader]string CompanyCode, [FromHeader]string DeviceSyncDate = "1900-01-01 00:00:00")
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleContent> ListOfContent = new List<SimpleContent>();            

            CreatorEntities db = new CreatorEntities();
            ListOfContent = db.ClientSubContent.Where(c => c.ClientSubSubMenus.ClientSubMenuMenus.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                            .Where(ca => ca.ClientSubSubMenus.ClientSubMenuMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                            .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                            .Select(itm => new SimpleContent
                                             {
                                                 ID = itm.ClientSubContentID,
                                                 SubMenuID = itm.ClientSubSubMenus.ID,
                                                 CategoryName = itm.Categories.Name,
                                                 Description = itm.Library.Description,
                                                 FileType = itm.Library.Type,
                                                 Filename = itm.Library.Filename,
                                                 Preview = itm.Library.Preview,
                                                 Updated = itm.ChangeDate.ToString()
                                             })
                                             .ToList();
            return ListOfContent;
        }

        [Authorize]
        [Route("DataPrivacy")]
        public byte[] PostDataPrivacy()
        {
            byte[] DPByte = File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ExternalDocs + "\\DataPrivacy.pdf");
            return DPByte;
        }

        [Authorize]
        [Route("LibraryItem")]
        public byte[] PostLibraryItem([FromHeader] String Filename)
        {
            byte[] LibraryByte = File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.Library + "\\" + Filename);
            return LibraryByte;
        }

        [Authorize]
        [Route("FAQs")]
        public byte[] PostFAQs()
        {
            byte[] DPByte = File.ReadAllBytes(CreatorAPI.Properties.Settings.Default.ExternalDocs + "\\FAQs.pdf");
            return DPByte;
        }

        [Authorize]
        [Route("Cleanup")]
        public IEnumerable<ActiveContentList> PostCleanup([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            //Containts Cleanup Instruction for bot ClientContent and ClientSubContent
            string CurrentContentList = "";
            string CurrentSubContentList = "";
            string UpperCaseCC = CompanyCode.Trim().ToUpper();

            CreatorEntities db = new CreatorEntities();
            List<ActiveContentList> CleanupList = new List<ActiveContentList>();

            //ClientContent
            List<ClientContent> Listc = db.ClientContent.Where(c => c.ClientSubMenus.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                        .Where(ca => ca.ClientSubMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                        .ToList();

            foreach (ClientContent c in Listc)
            {
                CurrentContentList = CurrentContentList + c.ID + ",";
            }

            if (Listc.Count > 0)
            {
                CurrentContentList = "(" + CurrentContentList.Substring(0, CurrentContentList.Length - 1) + ")";
            }

            //ClientSubContent
            List<ClientSubContent> Listsc = db.ClientSubContent.Where(c => c.ClientSubSubMenus.ClientSubMenuMenus.ClientMenus.ClientApps.Clients.Code == UpperCaseCC)
                                                               .Where(ca => ca.ClientSubSubMenus.ClientSubMenuMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                                               .ToList();

            foreach (ClientSubContent sc in Listsc)
            {
                CurrentSubContentList = CurrentSubContentList + sc.ClientSubContentID + ",";
            }

            if (Listsc.Count > 0)
            {
                CurrentSubContentList = "(" + CurrentSubContentList.Substring(0, CurrentSubContentList.Length - 1) + ")";
            }

            ActiveContentList list = new ActiveContentList();
            list.Content = CurrentContentList;
            list.SubContent = CurrentSubContentList;

            CleanupList.Add(list);

            return CleanupList;
        }
    }
}