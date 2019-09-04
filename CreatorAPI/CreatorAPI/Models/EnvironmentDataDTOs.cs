using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{

    [Serializable()]
    [XmlRoot("Status")]
    public class Status
    {
        [XmlElement("code")]
        public string Code { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
    }

    [Serializable()]
    [XmlRoot("SmartValue")]
    public class SmartValue
    {
        [XmlElement("SmvName")]
        public string SmvName { get; set; }
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Code")]
        public string Code { get; set; }
        [XmlElement("Value")]
        public string Value { get; set; }
    }

    [Serializable()]
    [XmlRoot("SmartValueList")]
    public class SmartValueList
    {
        [XmlElement("SmartValue")]
        public List<SmartValue> SmartValue { get; set; }
    }

    [Serializable()]
    [XmlRoot("SmartEnvironment")]
    public class SmartEnvironment
    {
        [XmlElement("SmeKey")]
        public string SmeKey { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Default")]
        public string Default { get; set; }
        [XmlElement("SmartValueList")]
        public SmartValueList SmartValueList { get; set; }
    }

    [Serializable()]
    [XmlRoot("xml")]
    public class EnvironmentData
    {
        [XmlElement("status")]
        public Status Status { get; set; }
        [XmlElement("SmartEnvironment")]
        public SmartEnvironment SmartEnvironment { get; set; }
    }
}