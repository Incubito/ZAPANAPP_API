using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Library")]
    public class LibraryController : ApiController
    {
        [Authorize]
        [Route("List")]
        public IEnumerable<SimpleLibrary> PostList([FromHeader]string DeviceSyncDate)
        {
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleLibrary> RequestedLibraryItems = new List<SimpleLibrary>();

            CreatorEntities db = new CreatorEntities();

            RequestedLibraryItems = db.Library.Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                              .Select(itm => new SimpleLibrary
                                              {
                                                Description = itm.Description,
                                                Type = itm.Type,
                                                Keywords = itm.Keywords,
                                                Filename = itm.Filename,
                                                Updated = itm.ChangeDate.ToString()
                                              })
                                             .ToList();

            return RequestedLibraryItems;
        }

        [Authorize]
        [Route("Cleanup")]
        public IEnumerable<ActiveList> PostCleanUp()
        {
            string CurrentList = "'";

            CreatorEntities db = new CreatorEntities();
            List<ActiveList> CleanupList = new List<ActiveList>();
            List<Library> library = db.Library.ToList();

            foreach(Library libraryitem in library)
            {
                CurrentList = CurrentList + libraryitem.Filename + "','";
            }

            if (library.Count > 0)
            {
                CurrentList = "(" + CurrentList.Substring(0, CurrentList.Length - 2) + ")";
            }

            ActiveList lib = new ActiveList();
            lib.List = CurrentList;

            CleanupList.Add(lib);

            return CleanupList;
        }
    }
}
