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
    
    public partial class Colours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Colours()
        {
            this.ClientScreens = new HashSet<ClientScreens>();
        }
    
        public int ID { get; set; }
        public Nullable<int> FontRed { get; set; }
        public Nullable<int> FontGreen { get; set; }
        public Nullable<int> FontBlue { get; set; }
        public Nullable<int> LineRed { get; set; }
        public Nullable<int> LineGreen { get; set; }
        public Nullable<int> LineBlue { get; set; }
        public Nullable<int> BackgroundRed { get; set; }
        public Nullable<int> BackgroundGreen { get; set; }
        public Nullable<int> BackgroundBlue { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientScreens> ClientScreens { get; set; }
    }
}
