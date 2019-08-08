﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CreatorEntities : DbContext
    {
        public CreatorEntities()
            : base("name=CreatorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Apps> Apps { get; set; }
        public virtual DbSet<Assessments> Assessments { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ClientApps> ClientApps { get; set; }
        public virtual DbSet<ClientContent> ClientContent { get; set; }
        public virtual DbSet<ClientMenus> ClientMenus { get; set; }
        public virtual DbSet<ClientRequests> ClientRequests { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientScreenContent> ClientScreenContent { get; set; }
        public virtual DbSet<ClientScreens> ClientScreens { get; set; }
        public virtual DbSet<ClientSubMenus> ClientSubMenus { get; set; }
        public virtual DbSet<Colours> Colours { get; set; }
        public virtual DbSet<ConnectionsLog> ConnectionsLog { get; set; }
        public virtual DbSet<InstalledFonts> InstalledFonts { get; set; }
        public virtual DbSet<Library> Library { get; set; }
        public virtual DbSet<MobileConnections> MobileConnections { get; set; }
        public virtual DbSet<TemplateSelection> TemplateSelection { get; set; }
        public virtual DbSet<TrustedEmailDomains> TrustedEmailDomains { get; set; }
        public virtual DbSet<UsageTracker> UsageTracker { get; set; }
        public virtual DbSet<Zendesk> Zendesk { get; set; }
        public virtual DbSet<ClientZendesk> ClientZendesk { get; set; }
        public virtual DbSet<ClientSettings> ClientSettings { get; set; }
    }
}
