using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class ContentModels
    {
        public string ContentID { get; set; }
        public string ContentSubMenuID { get; set; }
        public string ContentDescription { get; set; }
        public string ContentType { get; set; }
        public string ContentCategory { get; set; }
        public string ContentName { get; set; }
        public string ContentUpdated { get; set; }
    }
}