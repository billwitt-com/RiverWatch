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
    
    public partial class StationImageType
    {
        public StationImageType()
        {
            this.StationImages = new HashSet<StationImage>();
        }
    
        public int ID { get; set; }
        public string Type { get; set; }
        public bool EnforcePrimary { get; set; }
    
        public virtual ICollection<StationImage> StationImages { get; set; }
    }
}