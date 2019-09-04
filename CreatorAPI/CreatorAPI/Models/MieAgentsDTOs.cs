using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    [XmlRoot(ElementName = "Position")]
    public class MiePosition
    {
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "AgentKey")]
        public string AgentKey { get; set; }
        [XmlElement(ElementName = "BranchKey")]
        public string BranchKey { get; set; }
        [XmlElement(ElementName = "Rule")]
        public string Rule { get; set; }
    }

    [XmlRoot(ElementName = "Positions")]
    public class Positions
    {
        [XmlElement(ElementName = "Position")]
        public MiePosition Position { get; set; }
    }

    [XmlRoot(ElementName = "Agent")]
    public class MieAgent
    {
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "CanCapture")]
        public string CanCapture { get; set; }
        [XmlElement(ElementName = "BlockOfflineLogin")]
        public string BlockOfflineLogin { get; set; }
        [XmlElement(ElementName = "IsActive")]
        public string IsActive { get; set; }
        [XmlElement(ElementName = "IsAdministrator")]
        public string IsAdministrator { get; set; }
        [XmlElement(ElementName = "CanSMS")]
        public string CanSMS { get; set; }
        [XmlElement(ElementName = "Positions")]
        public Positions Positions { get; set; }
    }

    [XmlRoot(ElementName = "AgentList")]
    public class AgentList
    {
        [XmlElement(ElementName = "Agent")]
        public MieAgent Agent { get; set; }
    }

    [XmlRoot(ElementName = "xml")]
    public class MieAgentsModel
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "AgentList")]
        public AgentList AgentList { get; set; }
    }
}