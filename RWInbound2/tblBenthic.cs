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
    
    public partial class tblBenthic
    {
        public int BenTaxaID { get; set; }
        public string Stage { get; set; }
        public int BenSampID { get; set; }
        public int RepNum { get; set; }
        public Nullable<int> Individuals { get; set; }
        public Nullable<bool> ExcludedTaxa { get; set; }
        public string HabitatType { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public string StoretUploaded { get; set; }
        public int ID { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
        public Nullable<int> NumInHundredPct { get; set; }
    
        public virtual tblBenRep tblBenRep { get; set; }
        public virtual tblBenTaxa tblBenTaxa { get; set; }
    }
}
