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
    
    public partial class RiverWatchEntities : DbContext
    {
        public RiverWatchEntities()
            : base("name=RiverWatchEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<GearConfig> GearConfigs { get; set; }
        public virtual DbSet<InboundICPFinal> InboundICPFinals { get; set; }
        public virtual DbSet<InboundICPOrigional> InboundICPOrigionals { get; set; }
        public virtual DbSet<MetalBarCode> MetalBarCodes { get; set; }
        public virtual DbSet<NutrientBarCode> NutrientBarCodes { get; set; }
        public virtual DbSet<ProjectStation> ProjectStations { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<tblParticipant> tblParticipants { get; set; }
        public virtual DbSet<tblPartInfo> tblPartInfoes { get; set; }
        public virtual DbSet<tblPhysHabPara> tblPhysHabParas { get; set; }
        public virtual DbSet<tblSampleXXXX> tblSampleXXXXes { get; set; }
        public virtual DbSet<tblWatercode> tblWatercodes { get; set; }
        public virtual DbSet<tblWatershedGrp> tblWatershedGrps { get; set; }
        public virtual DbSet<tblWBKey> tblWBKeys { get; set; }
        public virtual DbSet<tblXSRep> tblXSReps { get; set; }
        public virtual DbSet<tlkActivityCategory> tlkActivityCategories { get; set; }
        public virtual DbSet<tlkActivityType> tlkActivityTypes { get; set; }
        public virtual DbSet<tlkBioResultsType> tlkBioResultsTypes { get; set; }
        public virtual DbSet<tlkCommunity> tlkCommunities { get; set; }
        public virtual DbSet<tlkCounty> tlkCounties { get; set; }
        public virtual DbSet<tlkEquipItem> tlkEquipItems { get; set; }
        public virtual DbSet<tlkFieldGear> tlkFieldGears { get; set; }
        public virtual DbSet<tlkFieldProcedure> tlkFieldProcedures { get; set; }
        public virtual DbSet<tlkGearConfig> tlkGearConfigs { get; set; }
        public virtual DbSet<tlkGrid> tlkGrids { get; set; }
        public virtual DbSet<tlkHydroUnit> tlkHydroUnits { get; set; }
        public virtual DbSet<tlkLimit> tlkLimits { get; set; }
        public virtual DbSet<tlkMetalBarCodeType> tlkMetalBarCodeTypes { get; set; }
        public virtual DbSet<tlkNutrientBarCodeType> tlkNutrientBarCodeTypes { get; set; }
        public virtual DbSet<tlkOrganizationType> tlkOrganizationTypes { get; set; }
        public virtual DbSet<tlkQUADI> tlkQUADIs { get; set; }
        public virtual DbSet<tlkQuarterSection> tlkQuarterSections { get; set; }
        public virtual DbSet<tlkRange> tlkRanges { get; set; }
        public virtual DbSet<tlkregion> tlkregions { get; set; }
        public virtual DbSet<tlkRiverWatchWaterShed> tlkRiverWatchWaterSheds { get; set; }
        public virtual DbSet<tlkSampleType> tlkSampleTypes { get; set; }
        public virtual DbSet<tlkState> tlkStates { get; set; }
        public virtual DbSet<tlkStationQUAD> tlkStationQUADs { get; set; }
        public virtual DbSet<tlkStationStatu> tlkStationStatus { get; set; }
        public virtual DbSet<tlkStationType> tlkStationTypes { get; set; }
        public virtual DbSet<tlkTownship> tlkTownships { get; set; }
        public virtual DbSet<tlkWQCCWaterShed> tlkWQCCWaterSheds { get; set; }
        public virtual DbSet<tlkWSG> tlkWSGs { get; set; }
        public virtual DbSet<tlkWSR> tlkWSRs { get; set; }
        public virtual DbSet<trsChemParaMapColumn> trsChemParaMapColumns { get; set; }
        public virtual DbSet<tblX> tblXS { get; set; }
        public virtual DbSet<FileStorage> FileStorages { get; set; }
        public virtual DbSet<Lachat> Lachats { get; set; }
        public virtual DbSet<ControlPermission> ControlPermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<tlkEcoRegion> tlkEcoRegions { get; set; }
        public virtual DbSet<tlkEquipCategory> tlkEquipCategories { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblRegistration> tblRegistrations { get; set; }
        public virtual DbSet<tblTrainee> tblTrainees { get; set; }
        public virtual DbSet<NutrientData> NutrientDatas { get; set; }
        public virtual DbSet<NutrientLimit> NutrientLimits { get; set; }
        public virtual DbSet<tlkNutrientLimit> tlkNutrientLimits { get; set; }
        public virtual DbSet<ParticipantswithOrgName> ParticipantswithOrgNames { get; set; }
        public virtual DbSet<ViewSoloNutrientDup> ViewSoloNutrientDups { get; set; }
        public virtual DbSet<UnknownSample> UnknownSample { get; set; }
        public virtual DbSet<tblProject> tblProject { get; set; }
        public virtual DbSet<tblBenGrid> tblBenGrid { get; set; }
        public virtual DbSet<tblPhysHab> tblPhysHab { get; set; }
        public virtual DbSet<tlkPhysHabPara> tlkPhysHabPara { get; set; }
        public virtual DbSet<tlkSubPara> tlkSubPara { get; set; }
        public virtual DbSet<tblSubSamp> tblSubSamp { get; set; }
        public virtual DbSet<tlkSection> tlkSection { get; set; }
        public virtual DbSet<InboundSamples> InboundSamples { get; set; }
        public virtual DbSet<tblBenSamp> tblBenSamps { get; set; }
        public virtual DbSet<organization> organizations { get; set; }
        public virtual DbSet<NEWexpWater> NEWexpWaters { get; set; }
        public virtual DbSet<OrgStatu> OrgStatus { get; set; }
        public virtual DbSet<tblBenthic> tblBenthics { get; set; }
        public virtual DbSet<PublicUsers> PublicUsers { get; set; }
        public virtual DbSet<Project> Project { get; set; }
    }
}
