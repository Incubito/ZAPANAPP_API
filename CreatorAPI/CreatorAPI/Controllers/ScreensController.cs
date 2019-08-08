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
    [RoutePrefix("api/Screens")]
    public class ScreensController : ApiController
    {
        [Authorize]
        [Route("ScreenList")]
        public IEnumerable<SimpleScreen> PostScreenList([FromHeader]string AppCode, [FromHeader]string CompanyCode)
        {
            List<SimpleScreen> ListOfScreens = new List<SimpleScreen>();

            CreatorEntities db = new CreatorEntities();
            Clients client = db.Clients.Single(c => c.Code == CompanyCode);
            ClientApps clientapp = client.ClientApps.Single(ca => ca.Apps.AppCode == AppCode);
            ListOfScreens = clientapp.ClientScreens.Where(app => app.ClientApps.Apps.AppCode == AppCode)
                                                          .Select(itm => new SimpleScreen
                                                          {
                                                              ID = itm.ID,
                                                              Design = itm.Design,
                                                              FontFamily = itm.InstalledFonts.Name,
                                                              FontRed = itm.Colours.FontRed.ToString(),
                                                              FontGreen = itm.Colours.FontGreen.ToString(),
                                                              FontBlue = itm.Colours.FontBlue.ToString(),
                                                              LineRed = itm.Colours.LineRed.ToString(),
                                                              LineGreen = itm.Colours.LineGreen.ToString(),
                                                              LineBlue = itm.Colours.LineBlue.ToString(),
                                                              BackgroundRed = itm.Colours.BackgroundRed.ToString(),
                                                              BackgroudGreen = itm.Colours.BackgroundGreen.ToString(),
                                                              BackgroundBlue = itm.Colours.BackgroundBlue.ToString(),
                                                              Description = itm.Description
                                                          })
                                                          .ToList();

            return ListOfScreens;
        }

    }
}