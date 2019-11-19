using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleCalendarLibrary
    {
        public int ID { get; set; }
        public int CalendarDetailsID { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public string Filename { get; set; }
        public string Preview { get; set; }
        public string Updated { get; set; }
    }
}