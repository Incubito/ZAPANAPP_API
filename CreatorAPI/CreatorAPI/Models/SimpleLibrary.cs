using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleLibrary
    {
        public string Description { get; set; }

        public string Type { get; set; }

        public string Keywords { get; set; }

        public string Filename { get; set; }
    }
}