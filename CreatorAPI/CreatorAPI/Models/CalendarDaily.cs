using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class CalendarDaily
    {
        public int CalendarID { get; set; }
        public string PartOfMonth { get; set; }
        public string PartOfMonthDetails { get; set; }
        public string PartOfWeek { get; set; }
        public string PartOfWeekDetails { get; set; }
        public List<DailyEvents> Events { get; set; }
        public string Updated { get; set; }
    }
}