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
    
    public partial class tblBenRep
    {
        public int BenSampID { get; set; }
        public int RepNum { get; set; }
        public Nullable<int> ActivityCategory { get; set; }
        public Nullable<int> Grids { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public string StoretUploaded { get; set; }
        public int ID { get; set; }
    
        public virtual tlkActivityCategory tlkActivityCategory { get; set; }
        public virtual tblBenSamp tblBenSamp { get; set; }
    }
}
