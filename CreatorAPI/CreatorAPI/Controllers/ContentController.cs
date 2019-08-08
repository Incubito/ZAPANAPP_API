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
        public IEnumerable<SimpleContent> PostContentList([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            List<SimpleContent> ListOfContent = new List<SimpleContent>();

            CreatorEntities db = new CreatorEntities();
            ListOfContent = db.ClientContent.Where(c => c.ClientSubMenus.ClientMenus.ClientApps.Clients.Code == CompanyCode)
                                             .Where(ca => ca.ClientSubMenus.ClientMenus.ClientApps.Apps.AppCode == AppCode)
                                             .Select(itm => new SimpleContent
                                             {
                                                ID = itm.ID,
                                                SubMenuID = itm.ClientSubMenus.ID,
                                                CategoryName = itm.Categories.Name,
                                                Description = itm.Library.Description,
                                                FileType = itm.Library.Type,
                                                Filename = itm.Library.Filename,
                                                Preview = itm.Library.Preview
                                             })
                                             .ToList();
            return ListOfContent;
        }

        [Authorize]
        [Route("Active")]
        public IEnumerable<String> PostActive([FromHeader]string CompanyCode)
        {
            List<string> ActiveList = new List<string>();

            return ActiveList;
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
    }
}