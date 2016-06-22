//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblSample
    {
        public int SampleID { get; set; }
        public int StationID { get; set; }
        public Nullable<int> OrganizationID { get; set; }
        public string SampleNumber { get; set; }
        public string NumberSample { get; set; }
        public System.DateTime DateCollected { get; set; }
        public Nullable<System.DateTime> TimeCollected { get; set; }
        public Nullable<System.DateTime> DateReceived { get; set; }
        public bool DataSheetIncluded { get; set; }
        public Nullable<System.DateTime> MissingDataSheetReqDate { get; set; }
        public bool ChainOfCustody { get; set; }
        public bool MissingDataSheetReceived { get; set; }
        public bool NoMetals { get; set; }
        public bool PhysicalHabitat { get; set; }
        public bool Bug { get; set; }
        public bool NoNutrient { get; set; }
        public bool TotalSuspendedSolids { get; set; }
        public bool NitratePhosphorus { get; set; }
        public bool DuplicatedTSS { get; set; }
        public bool DuplicatedNP { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
        public Nullable<bool> ChlorideSulfate { get; set; }
        public Nullable<bool> BlankMetals { get; set; }
        public Nullable<bool> DuplicatedMetals { get; set; }
        public Nullable<bool> BugsQA { get; set; }
        public Nullable<bool> DuplicatedCS { get; set; }
        public Nullable<bool> Valid { get; set; }
    }
}
