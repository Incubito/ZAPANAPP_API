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
    
    public partial class ClientScreenContent
    {
        public int ID { get; set; }
        public Nullable<int> ClientScreenID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ImageReference { get; set; }
        public string LinkReference { get; set; }
    
        public virtual ClientScreens ClientScreens { get; set; }
    }
}
