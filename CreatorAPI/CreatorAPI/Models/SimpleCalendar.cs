using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleCalendar
    {
        public int ID { get; set; }
        public long Day { get; set; }
        public string Updated { get; set; }
    }
}