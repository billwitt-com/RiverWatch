//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RWInbound2
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblStatusStation
    {
        public int StatusStationID { get; set; }
        public int StatusID { get; set; }
        public int StationID { get; set; }
        public string StationStatus { get; set; }
        public Nullable<int> Frequency { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
    
        public virtual tblStatu tblStatu { get; set; }
        public virtual tblStation tblStation { get; set; }
    }
}
