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
    
    public partial class tblBenSamp
    {
        public int ID { get; set; }
        public Nullable<int> SampleID { get; set; }
        public Nullable<int> ActivityID { get; set; }
        public Nullable<System.DateTime> CollDate { get; set; }
        public Nullable<int> CollMeth { get; set; }
        public string FieldGearID { get; set; }
        public Nullable<int> GearConfigID { get; set; }
        public Nullable<System.DateTime> CollTime { get; set; }
        public Nullable<int> Medium { get; set; }
        public Nullable<int> Intent { get; set; }
        public Nullable<int> Community { get; set; }
        public Nullable<int> BioResultGroupID { get; set; }
        public Nullable<int> NumKicksSamples { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<bool> Valid { get; set; }
        public int ActivityTypeID { get; set; }
    
        public virtual tlkActivityType tlkActivityType { get; set; }
    }
}
