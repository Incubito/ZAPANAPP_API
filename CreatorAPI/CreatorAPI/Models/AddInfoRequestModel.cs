using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CreatorAPI.Models
{
    public class AddInfoRequestModel
    {
        public long? AddInfoEntryID { get; set; }
        public string CandidateName { get; set; }
        public string CandidateID { get; set; }
        public string RequestType { get; set; }
        public string Product { get; set; }
        public string AddInfoPrompt { get; set; }
        public string AddInfoKey { get; set; }
        public string AddInfoCost { get; set; }
        public long RequestItemID { get; set; }
        public string CapturedDate { get; set; }
        public string Request { get; set; }
        public string TypeKey { get; set; }
        public string CredentialKey { get; set; }
    }
    public class AddInfoRequestModelCompany
    {
        public long? AddInfoEntryID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyID { get; set; }
        public string RequestType { get; set; }
        public string Product { get; set; }
        public string AddInfoPrompt { get; set; }
        public string AddInfoKey { get; set; }
        public string AddInfoCost { get; set; }
        public long RequestItemID { get; set; }
        public string CapturedDate { get; set; }
        public string Request { get; set; }
        public string TypeKey { get; set; }
        public string CredentialKey { get; set; }
    }
    public class AddInfoDTO
    {
        public long AddInfoEntryID { get; set; }
        public string InfoText { get; set; }
        public string AddInfoKey { get; set; }
        public long RequestItemID { get; set; }
        public string TypeKey { get; set; }
        public string CredentialKey { get; set; }
        public string AddInfoCost { get; set; }
        public List<string> Attachments { get; set; }
    }

    [XmlRoot(ElementName = "AddInfoReport")]
    public class AddInfoReport
    {
        [XmlElement(ElementName = "ClientKey")]
        public string ClientKey { get; set; }
        [XmlElement(ElementName = "InquiryKey")]
        public string InquiryKey { get; set; }
        [XmlElement(ElementName = "CredentialKey")]
        public string CredentialKey { get; set; }
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "Surname")]
        public string Surname { get; set; }
        [XmlElement(ElementName = "IdNumber")]
        public string IdNumber { get; set; }
        [XmlElement(ElementName = "Note")]
        public string Note { get; set; }
        [XmlElement(ElementName = "DateCaptured")]
        public string DateCaptured { get; set; }
        [XmlElement(ElementName = "UserCapturedName")]
        public string UserCapturedName { get; set; }
        [XmlElement(ElementName = "UserCapturedEmail")]
        public string UserCapturedEmail { get; set; }
        [XmlElement(ElementName = "UserCapturedPhone")]
        public string UserCapturedPhone { get; set; }
        [XmlElement(ElementName = "AddInfoKey")]
        public string AddInfoKey { get; set; }
        [XmlElement(ElementName = "AddInfoTypeKey")]
        public string AddInfoTypeKey { get; set; }
        [XmlElement(ElementName = "HasDocuments")]
        public string HasDocuments { get; set; }
        [XmlElement(ElementName = "Feedback")]
        public string Feedback { get; set; }
        [XmlElement(ElementName = "IsRequest")]
        public string IsRequest { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Asks")]
        public string Asks { get; set; }
        [XmlElement(ElementName = "ElectronicStatus")]
        public string ElectronicStatus { get; set; }
        [XmlElement(ElementName = "AddInfoStatus")]
        public string AddInfoStatus { get; set; }
        [XmlElement(ElementName = "AC0_Value")]
        public string AC0_Value { get; set; }
        [XmlElement(ElementName = "Entity")]
        public string Entity { get; set; }
        [XmlElement(ElementName = "CompanyName")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "CompanyRegistration")]
        public string CompanyRegistration { get; set; }
        [XmlElement(ElementName = "Files")]
        public string Files { get; set; }
    }

    [XmlRoot(ElementName = "AddInfoReportList")]
    public class AddInfoReportList
    {
        [XmlElement(ElementName = "AddInfoReport")]
        public List<AddInfoReport> AddInfoReport { get; set; }
    }

    [XmlRoot(ElementName = "xml")]
    public class AddInfoReportWrapper
    {
        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "AddInfoReportList")]
        public AddInfoReportList AddInfoReportList { get; set; }
    }
}