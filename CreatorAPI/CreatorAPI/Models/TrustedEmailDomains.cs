//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreatorAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrustedEmailDomains
    {
        public int ID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string DomainName { get; set; }
    
        public virtual Clients Clients { get; set; }
    }
}
