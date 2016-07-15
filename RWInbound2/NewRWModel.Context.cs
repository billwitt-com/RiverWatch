﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RWInbound2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NewRiverwatchEntities : DbContext
    {
        public NewRiverwatchEntities()
            : base("name=NewRiverwatchEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GearConfig> GearConfigs { get; set; }
        public virtual DbSet<InboundICPFinal> InboundICPFinals { get; set; }
        public virtual DbSet<InboundICPOrigional> InboundICPOrigionals { get; set; }
        public virtual DbSet<NEWexpWater> NEWexpWaters { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<MetalBarCode> MetalBarCodes { get; set; }
        public virtual DbSet<OrgStatu> OrgStatus { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<tblParticipant> tblParticipants { get; set; }
        public virtual DbSet<tblPartInfo> tblPartInfoes { get; set; }
        public virtual DbSet<tblPhysHabPara> tblPhysHabParas { get; set; }
        public virtual DbSet<tblProject> tblProjects { get; set; }
        public virtual DbSet<tblProjectStation> tblProjectStations { get; set; }
        public virtual DbSet<tblRegistration> tblRegistrations { get; set; }
        public virtual DbSet<tlkLimit> tlkLimits { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<NutrientBarCode> NutrientBarCodes { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<InboundSample> InboundSamples { get; set; }
    
        public virtual int UpdateLocalTablesFromIncomingICP(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateLocalTablesFromIncomingICP", userParameter);
        }
    }
}
