using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Calendar")]
    public class CalendarController : ApiController
    {
        [Authorize]
        [Route("DayList")]
        public IEnumerable<SimpleCalendar> PostDayList([FromHeader] string CompanyCode, [FromHeader] long Startdate, [FromHeader] long EndDate, [FromHeader]string DeviceSyncDate)
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleCalendar> ListOfDays = new List<SimpleCalendar>();

            CreatorEntities db = new CreatorEntities();
            ListOfDays = db.ClientCalendar.Where(cd => cd.Clients.Code == UpperCaseCC)
                                          .Where(ccl => ccl.Day >= Startdate)
                                          .Where(ccl => ccl.Day <= EndDate)
                                          .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                          .Select(itm => new SimpleCalendar
                                          {
                                              ID = itm.ID,
                                              Day = (long)itm.Day,
                                              Updated = itm.ChangeDate.ToString()
                                          })
                                          .ToList();

            return ListOfDays;
        }

        [Authorize]
        [Route("EventsList")]
        public IEnumerable<SimpleCalendarDetails> PostEventsList([FromHeader] string CompanyCode, [FromHeader] long Startdate, [FromHeader] long EndDate, [FromHeader]string DeviceSyncDate)
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleCalendarDetails> ListOfEvents = new List<SimpleCalendarDetails>();

            CreatorEntities db = new CreatorEntities();
            ListOfEvents = db.ClientCalendarDetails.Where(cd => cd.ClientCalendar.Clients.Code == UpperCaseCC)
                                                   .Where(ccl => ccl.ClientCalendar.Day >= Startdate)
                                                   .Where(ccl => ccl.ClientCalendar.Day <= EndDate)
                                                   .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                   .Select(itm => new SimpleCalendarDetails
                                                    {
                                                        ID = itm.ID,
                                                        ClientCalendarID = (int)itm.ClientCalendarID,
                                                        EventName = itm.EventName,
                                                        EventDetails = itm.Event,
                                                        StartTime = (long)itm.StartTime,
                                                        Endtime = (long)itm.EndTime,
                                                        DetailsType = itm.Type,
                                                        Updated = itm.ChangeDate.ToString()
                                                    })
                                                   .ToList();

            return ListOfEvents;
        }

        [Authorize]
        [Route("LibraryList")]
        public IEnumerable<SimpleCalendarLibrary> PostLibraryList([FromHeader] string CompanyCode, [FromHeader] long Startdate, [FromHeader] long EndDate, [FromHeader]string DeviceSyncDate)
        {
            string UpperCaseCC = CompanyCode.Trim().ToUpper();
            DateTime LastSyncDate = DateTime.Parse(DeviceSyncDate);
            List<SimpleCalendarLibrary> ListOfLibrary = new List<SimpleCalendarLibrary>();

            CreatorEntities db = new CreatorEntities();
            ListOfLibrary = db.ClientCalendarLibrary.Where(cd => cd.ClientCalendarDetails.ClientCalendar.Clients.Code == UpperCaseCC)
                                                    .Where(ccl => ccl.ClientCalendarDetails.ClientCalendar.Day >= Startdate)
                                                    .Where(ccl => ccl.ClientCalendarDetails.ClientCalendar.Day <= EndDate)
                                                    .Where(csc => System.Data.Entity.SqlServer.SqlFunctions.DateDiff("MINUTE", csc.ChangeDate, LastSyncDate) < 0)
                                                    .Select(itm => new SimpleCalendarLibrary
                                                    {
                                                        ID = itm.ID,
                                                        CalendarDetailsID = (int)itm.ClientCalendarDetailsID,
                                                        Description = itm.Library.Description,
                                                        FileType = itm.Library.Type,
                                                        Filename = itm.Library.Filename,
                                                        Preview = itm.Library.Preview,
                                                        Updated = itm.Library.ChangeDate.ToString()
                                                    })
                                                    .ToList();

            return ListOfLibrary;
        }

        [Authorize]
        [Route("Cleanup")]
        public IEnumerable<ActiveCalList> PostCleanUpV2([FromHeader]string CompanyCode)
        {
            string CurrentCalList = "";
            string CurrentCalDetailsList = "";
            string CurrentCalLibraryList = "";
            string UpperCaseCC = CompanyCode.Trim().ToUpper();

            List<ActiveCalList> CleanupList = new List<ActiveCalList>();
            CreatorEntities db = new CreatorEntities();

            List<ClientCalendarDetails> caldetailslist = db.ClientCalendarDetails.Where(ccl => ccl.ClientCalendar.Clients.Code == UpperCaseCC)
                                                                                 .ToList();

            foreach (ClientCalendarDetails caldetailsitem in caldetailslist)
            {
                CurrentCalDetailsList = CurrentCalDetailsList + caldetailsitem.ID + ",";
            }

            if (caldetailslist.Count > 0)
            {
                CurrentCalDetailsList = "(" + CurrentCalDetailsList.Substring(0, CurrentCalDetailsList.Length - 1) + ")";
            }

            List<ClientCalendarLibrary> callibrarylist = db.ClientCalendarLibrary.Where(ccl => ccl.ClientCalendarDetails.ClientCalendar.Clients.Code == UpperCaseCC)
                                                                                 .ToList();

            foreach (ClientCalendarLibrary callibraryitem in callibrarylist)
            {
                CurrentCalLibraryList = CurrentCalLibraryList + callibraryitem.ID + ",";
            }

            if (caldetailslist.Count > 0)
            {
                CurrentCalLibraryList = "(" + CurrentCalLibraryList.Substring(0, CurrentCalLibraryList.Length - 1) + ")";
            }

            ActiveCalList calactivelist = new ActiveCalList();
            calactivelist.CalList = CurrentCalList;
            calactivelist.CalDetailsList = CurrentCalDetailsList;
            calactivelist.CalLibraryList = CurrentCalLibraryList;

            CleanupList.Add(calactivelist);

            return CleanupList;
        }
    }
}
