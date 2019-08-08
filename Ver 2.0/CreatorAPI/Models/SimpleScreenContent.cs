using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleScreenContent
    {
        public int ID { get; set; }
        public int ScreenID { get; set; }
        public string Name { get; set; }
        public string SCType { get; set; }
        public string Contents { get; set; }
        public string Updated { get; set; }

    }
}