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
    
    public partial class ClientCalendar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientCalendar()
        {
            this.ClientCalendarDetails = new HashSet<ClientCalendarDetails>();
        }
    
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Nullable<long> Day { get; set; }
        public Nullable<System.DateTime> ChangeDate { get; set; }
    
        public virtual Clients Clients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientCalendarDetails> ClientCalendarDetails { get; set; }
    }
}