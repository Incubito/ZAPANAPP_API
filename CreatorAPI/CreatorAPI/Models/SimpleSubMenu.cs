using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleSubMenu
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public string Name { get; set; }
        public string PopupMessage { get; set; }
        public string Image { get; set; }
        public string Header { get; set; }
        public string Updated { get; set; }
    }
}