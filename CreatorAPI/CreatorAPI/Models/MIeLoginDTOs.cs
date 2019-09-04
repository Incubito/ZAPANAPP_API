using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    [XmlRoot(ElementName = "Token")]
    public class Token
    {
        [XmlElement(ElementName = "UserName")]
        public string UserName { get; set; }
        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }
        [XmlElement(ElementName = "Source")]
        public string Source { get; set; }
    }

    [XmlRoot(ElementName = "xml")]
    public class LoginXml
    {
        [XmlElement(ElementName = "Token")]
        public Token Token { get; set; }
    }
}