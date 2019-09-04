using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    [XmlRoot(ElementName = "Client")]
    public class MieClient
    {
        [XmlElement(ElementName = "ParentKey")]
        public string ParentKey { get; set; }
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "CanCapture")]
        public string CanCapture { get; set; }
        [XmlElement(ElementName = "RequireOrderNumber")]
        public string RequireOrderNumber { get; set; }
        [XmlElement(ElementName = "AgentParentKey")]
        public string AgentParentKey { get; set; }
        [XmlElement(ElementName = "RequireReason")]
        public string RequireReason { get; set; }
    }

    [XmlRoot(ElementName = "ClientList")]
    public class ClientList
    {
        [XmlElement(ElementName = "Client")]
        public MieClient Client { get; set; }
    }

    [XmlRoot(ElementName = "xml")]
    public class MieClients
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "ClientList")]
        public ClientList ClientList { get; set; }
    }
}