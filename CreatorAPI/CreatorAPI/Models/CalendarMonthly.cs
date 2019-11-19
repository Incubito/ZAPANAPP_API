using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class CalendarMonthly
    {
        public string TickDate { get; set; }

        public CalendarDaily Contents { get; set; }
    }
}