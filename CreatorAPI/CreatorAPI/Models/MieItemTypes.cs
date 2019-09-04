using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    [XmlRoot(ElementName = "ItemType")]
    public class ItemType
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "CategoryKey")]
        public string CategoryKey { get; set; }
        [XmlElement(ElementName = "TypeKey")]
        public string TypeKey { get; set; }
        [XmlElement(ElementName = "CopyAskType")]
        public string CopyAskType { get; set; }
        [XmlElement(ElementName = "AllowMultiple")]
        public string AllowMultiple { get; set; }
        [XmlElement(ElementName = "IndemnityType")]
        public string IndemnityType { get; set; }
        [XmlElement(ElementName = "ItemPrice")]
        public string ItemPrice { get; set; }
        [XmlElement(ElementName = "AutoProcType")]
        public string AutoProcType { get; set; }
        [XmlElement(ElementName = "IsBiometric")]
        public string IsBiometric { get; set; }
        [XmlElement(ElementName = "CountryKey")]
        public string CountryKey { get; set; }
        [XmlElement(ElementName = "CountryName")]
        public string CountryName { get; set; }
        [XmlElement(ElementName = "ZoneKey")]
        public string ZoneKey { get; set; }
        [XmlElement(ElementName = "ZoneName")]
        public string ZoneName { get; set; }
        [XmlElement(ElementName = "CategoryName")]
        public string CategoryName { get; set; }
        [XmlElement(ElementName = "PrefixCategory")]
        public string PrefixCategory { get; set; }
        [XmlElement(ElementName = "GroupMnemonics")]
        public string GroupMnemonics { get; set; }
        [XmlElement(ElementName = "CountryDefault")]
        public string CountryDefault { get; set; }
        [XmlElement(ElementName = "IndemnityPrompt")]
        public string IndemnityPrompt { get; set; }
        [XmlElement(ElementName = "IndemnityText")]
        public string IndemnityText { get; set; }
        [XmlElement(ElementName = "MINKey")]
        public string MINKey { get; set; }
        [XmlElement(ElementName = "InputGroupList")]
        public InputGroupList InputGroupList { get; set; }
    }

    [XmlRoot(ElementName = "InputAttribute")]
    public class InputAttribute
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "NameShort")]
        public string NameShort { get; set; }
        [XmlElement(ElementName = "NameLong")]
        public string NameLong { get; set; }
        [XmlElement(ElementName = "DataType")]
        public string DataType { get; set; }
        [XmlElement(ElementName = "Required")]
        public string Required { get; set; }
        [XmlElement(ElementName = "MinLength")]
        public string MinLength { get; set; }
        [XmlElement(ElementName = "DisplaySequence")]
        public string DisplaySequence { get; set; }
        [XmlElement(ElementName = "DependencyAction")]
        public string DependencyAction { get; set; }
        [XmlElement(ElementName = "TableKey")]
        public string TableKey { get; set; }
        [XmlElement(ElementName = "DependencyCode")]
        public string DependencyCode { get; set; }
        [XmlElement(ElementName = "DependencyValue")]
        public string DependencyValue { get; set; }
    }

    [XmlRoot(ElementName = "InputAttributeList")]
    public class InputAttributeList
    {
        [XmlElement(ElementName = "InputAttribute")]
        public List<InputAttribute> InputAttribute { get; set; }
    }

    [XmlRoot(ElementName = "InputGroup")]
    public class InputGroup
    {
        [XmlElement(ElementName = "MetaInputKey")]
        public string MetaInputKey { get; set; }
        [XmlElement(ElementName = "MetaInputGroupKey")]
        public string MetaInputGroupKey { get; set; }
        [XmlElement(ElementName = "GroupName")]
        public string GroupName { get; set; }
        [XmlElement(ElementName = "MultiLine")]
        public string MultiLine { get; set; }
        [XmlElement(ElementName = "EmptySuffix")]
        public string EmptySuffix { get; set; }
        [XmlElement(ElementName = "Default")]
        public string Default { get; set; }
        [XmlElement(ElementName = "InputAttributeList")]
        public InputAttributeList InputAttributeList { get; set; }
    }

    [XmlRoot(ElementName = "InputGroupList")]
    public class InputGroupList
    {
        [XmlElement(ElementName = "InputGroup")]
        public InputGroup InputGroup { get; set; }
    }

    [XmlRoot(ElementName = "ItemTypeList")]
    public class ItemTypeList
    {
        [XmlElement(ElementName = "ItemType")]
        public List<ItemType> ItemType { get; set; }
    }

    [XmlRoot(ElementName = "xml")]
    public class MieItemTypes
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "ItemTypeList")]
        public ItemTypeList ItemTypeList { get; set; }
    }
}