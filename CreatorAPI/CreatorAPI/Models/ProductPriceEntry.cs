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
    
    public partial class ProductPriceEntry
    {
        public long PriceEntryID { get; set; }
        public int ClientID { get; set; }
        public string ProductID { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual VerificationProduct VerificationProduct { get; set; }
    }
}