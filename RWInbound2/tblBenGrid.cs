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
    
    public partial class tblBenGrid
    {
        public int ID { get; set; }
        public Nullable<int> BenSampID { get; set; }
        public int RepNum { get; set; }
        public int GridNum { get; set; }
        public Nullable<int> BenCount { get; set; }
        public string StoretUploaded { get; set; }
        public Nullable<bool> Valid { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
    
        public virtual tblBenSamp tblBenSamp { get; set; }
    }
}
