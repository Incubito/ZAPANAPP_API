using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class SimpleMobileConnection
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string UUID { get; set; }
        public string AppCode { get; set; }
        public string CompanyCode { get; set; }
    }
}