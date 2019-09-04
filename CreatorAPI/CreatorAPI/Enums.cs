using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorApi
{
    public enum DocumentType
    {
        Qualification,
        Document,
        Report
    }

    public enum NotificationType
    {
        CandidateMessage,
        CandidateComment,
        ClientMessage,
        AddInfoRequest,
        BgScreeningRequest
    }

    public enum ProductType
    {
        CandidateVetting,
        Qualifications,
        BackgroundScreening,
        CompanyVetting,
        CandidateCompanyVetting
    }

    public enum MieRequestStatus
    {
        NoResult,
        PartialResult,
        Complete,
        AwaitingAdditionalInfo,
        Negative
    }

    public enum ClientCategory
    {
        Silver,
        Gold,
        Platinum
    }

    public enum ManualRequestStatus
    {
        NoRequest,
        NoAction,
        Assigned,
        Complete
    }
}