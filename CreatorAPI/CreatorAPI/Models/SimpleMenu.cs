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
        public bool Side { get; set; }
        public bool Bottom { get; set; }
        public bool Slide { get; set; }

        public string Action { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public string Header { get; set; }
        public string Updated { get; set; }
    }
}