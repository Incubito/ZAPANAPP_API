using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleMenu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Red { get; set; }
        public string Green { get; set; }
        public string Blue { get; set; }
        public string FontFamily { get; set; }
        public string Icon { get; set; }
        public string Header { get; set; }
        public string Updated { get; set; }
    }
}