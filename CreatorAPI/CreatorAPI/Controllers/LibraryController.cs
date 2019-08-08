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
        public IEnumerable<SimpleLibrary> PostList()
        {
            List<SimpleLibrary> RequestedLibraryItems = new List<SimpleLibrary>();

            CreatorEntities db = new CreatorEntities();

            RequestedLibraryItems = db.Library.Select(itm => new SimpleLibrary
                                              {
                                                Description = itm.Description,
                                                Type = itm.Type,
                                                Keywords = itm.Keywords,
                                                Filename = itm.Filename
                                              }).ToList();

            return RequestedLibraryItems;
        }
    }
}
