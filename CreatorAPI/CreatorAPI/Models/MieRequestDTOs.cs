using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    [XmlRoot(ElementName = "Item")]
    public class MieItem
    {
        [XmlElement(ElementName = "RemoteItemKey")]
        public string RemoteItemKey { get; set; }
        [XmlElement(ElementName = "ItemTypeCode")]
        public string ItemTypeCode { get; set; }
        [XmlElement(ElementName = "Indemnity")]
        public string Indemnity { get; set; }
        [XmlElement(ElementName = "ItemInputGroupList")]
        public ItemInputGroupList ItemInputGroupList { get; set; }
    }

    [XmlRoot(ElementName = "ItemAttribute")]
    public class ItemAttribute
    {
        [XmlElement(ElementName = "SystemCode")]
        public string SystemCode { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "ItemAttributeList")]
    public class ItemAttributeList
    {
        [XmlElement(ElementName = "ItemAttribute")]
        public List<ItemAttribute> ItemAttribute { get; set; }
    }

    [XmlRoot(ElementName = "ItemInputLine")]
    public class ItemInputLine
    {
        [XmlElement(ElementName = "ItemAttributeList")]
        public ItemAttributeList ItemAttributeList { get; set; }
    }

    [XmlRoot(ElementName = "ItemInputLineList")]
    public class ItemInputLineList
    {
        [XmlElement(ElementName = "ItemInputLine")]
        public ItemInputLine ItemInputLine { get; set; }
    }

    [XmlRoot(ElementName = "ItemInputGroup")]
    public class ItemInputGroup
    {
        [XmlElement(ElementName = "ItemInputLineList")]
        public ItemInputLineList ItemInputLineList { get; set; }
    }

    [XmlRoot(ElementName = "ItemInputGroupList")]
    public class ItemInputGroupList
    {
        [XmlElement(ElementName = "ItemInputGroup")]
        public ItemInputGroup ItemInputGroup { get; set; }
    }

    [XmlRoot(ElementName = "ItemList")]
    public class ItemList
    {
        [XmlElement(ElementName = "Item")]
        public List<MieItem> Item { get; set; }
    }

    [XmlRoot(ElementName = "Prerequisite")]
    public class Prerequisite
    {
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "PrerequisiteGroupKey")]
        public string PrerequisiteGroupKey { get; set; }
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "Completed")]
        public string Completed { get; set; }
    }

    [XmlRoot(ElementName = "PrerequisiteList")]
    public class PrerequisiteList
    {
        [XmlElement(ElementName = "Prerequisite")]
        public List<Prerequisite> Prerequisite { get; set; }
    }

    [XmlRoot(ElementName = "PrerequisiteGroupEx")]
    public class PrerequisiteGroupEx
    {
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "Key")]
        public string Key { get; set; }
        [XmlElement(ElementName = "PrerequisiteList")]
        public PrerequisiteList PrerequisiteList { get; set; }
    }

    [XmlRoot(ElementName = "PrerequisiteGroupList")]
    public class PrerequisiteGroupList
    {
        [XmlElement(ElementName = "PrerequisiteGroupEx")]
        public PrerequisiteGroupEx PrerequisiteGroupEx { get; set; }
    }

    [XmlRoot(ElementName = "Request")]
    public class Request
    {
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "AgentClient")]
        public string AgentClient { get; set; }
        [XmlElement(ElementName = "AgentKey")]
        public string AgentKey { get; set; }
        [XmlElement(ElementName = "RemoteRequest")]
        public string RemoteRequest { get; set; }
        [XmlElement(ElementName = "OrderNumber")]
        public string OrderNumber { get; set; }
        [XmlElement(ElementName = "Note")]
        public string Note { get; set; }
        [XmlElement(ElementName = "FirstNames")]
        public string FirstNames { get; set; }
        [XmlElement(ElementName = "Surname")]
        public string Surname { get; set; }
        [XmlElement(ElementName = "MaidenName")]
        public string MaidenName { get; set; }
        [XmlElement(ElementName = "KnownAs")]
        public string KnownAs { get; set; }
        [XmlElement(ElementName = "IdNumber")]
        public string IdNumber { get; set; }
        [XmlElement(ElementName = "Passport")]
        public string Passport { get; set; }
        [XmlElement(ElementName = "DateOfBirth")]
        public string DateOfBirth { get; set; }
        [XmlElement(ElementName = "AlternateEmail")]
        public string AlternateEmail { get; set; }
        [XmlElement(ElementName = "Source")]
        public string Source { get; set; }
        [XmlElement(ElementName = "RemoteCaptureDate")]
        public string RemoteCaptureDate { get; set; }
        [XmlElement(ElementName = "RemoteSendDate")]
        public string RemoteSendDate { get; set; }
        [XmlElement(ElementName = "RemoteGroup")]
        public string RemoteGroup { get; set; }
        [XmlElement(ElementName = "FingerPrintHostClientKey")]
        public string FingerPrintHostClientKey { get; set; }
        [XmlElement(ElementName = "FingerPrintHostKey")]
        public string FingerPrintHostKey { get; set; }
        [XmlElement(ElementName = "GroupKey")]
        public string GroupKey { get; set; }
        [XmlElement(ElementName = "ClientGroupKey")]
        public string ClientGroupKey { get; set; }
        [XmlElement(ElementName = "ContactNumber")]
        public string ContactNumber { get; set; }
        [XmlElement(ElementName = "ItemList")]
        public ItemList ItemList { get; set; }
        [XmlElement(ElementName = "RegistrationNumber")]
        public string RegistrationNumber { get; set; }
        [XmlElement(ElementName = "CompanyName")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "HasDocuments")]
        public bool HasDocuments { get; set; }
        [XmlElement(ElementName = "RequireOrderNumber")]
        public bool RequireOrderNumber { get; set; }
        [XmlElement(ElementName = "EntityKind")]
        public string EntityKind { get; set; }
        [XmlElement(ElementName = "PrerequisiteGroupList")]
        public PrerequisiteGroupList PrerequisiteGroupList { get; set; }
    }

    [Serializable, XmlRoot(ElementName = "xml")]
    public class MieRequestModel
    {
        [XmlElement(ElementName = "Request")]
        public Request Request { get; set; }
    }


    [Serializable, XmlRoot(ElementName = "xml")]
    public class MieRequestResponseModel
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "Request")]
        public MieResponseRequest Request { get; set; }
    }

    [XmlRoot(ElementName = "Item")]
    public class MieResponseItem
    {
        [XmlElement(ElementName = "ItemKey")]
        public string ItemKey { get; set; }
        [XmlElement(ElementName = "RemoteKey")]
        public string RemoteKey { get; set; }
    }

    [XmlRoot(ElementName = "ItemList")]
    public class ResponseItemList
    {
        [XmlElement(ElementName = "Item")]
        public MieResponseItem Item { get; set; }
    }

    [XmlRoot(ElementName = "Request")]
    public class MieResponseRequest
    {
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "RequestKey")]
        public string RequestKey { get; set; }
        [XmlElement(ElementName = "RemoteKey")]
        public string RemoteKey { get; set; }
        [XmlElement(ElementName = "Identifier")]
        public string Identifier { get; set; }
        [XmlElement(ElementName = "ItemList")]
        public ResponseItemList ItemList { get; set; }
    }
}