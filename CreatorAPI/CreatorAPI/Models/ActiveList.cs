using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class ActiveList
    {
        public string List { get; set; }
    }

    public class ActiveCalList
    {
        public string CalList { get; set; }
        public string CalDetailsList { get; set; }
        public string CalLibraryList { get; set; }
    }

    public class ActiveContentList
    {
        public string Content { get; set; }
        public string SubContent { get; set; }
    }

    public class ActiveSubList
    {
        public string SubMenus { get; set; }
        public string SubMenuMenus { get; set; }
        public string SubSubMenus { get; set; }
    }
}