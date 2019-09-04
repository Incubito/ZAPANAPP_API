//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreatorAPI.ExternalModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Candidate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidate()
        {
            this.MieRequests = new HashSet<MieRequest>();
        }
    
        public long CandidateID { get; set; }
        public int ClientID { get; set; }
        public Nullable<int> ContractorID { get; set; }
        public Nullable<int> OperationID { get; set; }
        public Nullable<int> LevelID { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string MaidenName { get; set; }
        public string IDNumber { get; set; }
        public string Passport { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public Nullable<bool> HasPrints { get; set; }
        public Nullable<int> CrimCheckStatus { get; set; }
        public Nullable<int> VettingStatus { get; set; }
        public Nullable<int> QualificationStatus { get; set; }
        public Nullable<int> BackgroundScreeningStatus { get; set; }
        public Nullable<int> OverallStatus { get; set; }
        public string Cellphone { get; set; }
        public string AddrHome1 { get; set; }
        public string AddrHome2 { get; set; }
        public string AddrHomeCode { get; set; }
        public string ID_Type { get; set; }
        public string Equ_Text { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> IsLegacy { get; set; }
        public string LegacyRef { get; set; }
        public string StatusNotes { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string MaritalStatus { get; set; }
        public string Dependants { get; set; }
        public string LanguagesSpoken { get; set; }
        public string PlaceOfBirth { get; set; }
        public string ContactNr { get; set; }
        public string EmailAdress { get; set; }
        public string IdentityConfirmation { get; set; }
        public string AppUserID { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MieRequest> MieRequests { get; set; }
    }
}
