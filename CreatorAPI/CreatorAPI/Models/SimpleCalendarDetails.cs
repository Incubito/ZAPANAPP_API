using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleCalendarDetails
    {
        public int ID { get; set; }
        public int ClientCalendarID { get; set; }
        public string EventName { get; set; }
        public string EventDetails { get; set; }
        public long StartTime { get; set; }
        public long Endtime { get; set; }
        public string DetailsType { get; set; }
        public string Updated { get; set; }
    }
}