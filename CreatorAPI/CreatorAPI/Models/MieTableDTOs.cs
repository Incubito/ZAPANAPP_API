using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    [XmlRoot(ElementName = "TableValue")]
    public class TableValue
    {
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "Default")]
        public string Default { get; set; }
    }

    [XmlRoot(ElementName = "TableValueList")]
    public class TableValueList
    {
        [XmlElement(ElementName = "TableValue")]
        public List<TableValue> TableValue { get; set; }
    }

    [XmlRoot(ElementName = "Table")]
    public class MieTable
    {
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Forced")]
        public string Forced { get; set; }
        [XmlElement(ElementName = "TableValueList")]
        public TableValueList TableValueList { get; set; }
    }

    [XmlRoot(ElementName = "TableList")]
    public class TableList
    {
        [XmlElement(ElementName = "Table")]
        public List<MieTable> Table { get; set; }
    }

    [XmlRoot(ElementName = "xml")]
    public class MieTables
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "TableList")]
        public TableList TableList { get; set; }
    }
}