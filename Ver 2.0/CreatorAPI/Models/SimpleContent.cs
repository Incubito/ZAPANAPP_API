using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleContent
    {
        public int ID { get; set; }
        public int SubMenuID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public string Filename { get; set; }
        public string Preview { get; set; }
        public string Updated { get; set; }
    }
}