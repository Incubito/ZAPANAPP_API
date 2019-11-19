using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class DailyEvents
    {
        public int DailyEventID { get; set; }
        public int CalendarID { get; set; }
        public string EventName { get; set; }
        public string EventDetails { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Updated { get; set; }
    }
}