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
    
    public partial class tlkAQWMStranslation
    {
        public int ID { get; set; }
        public string LocalName { get; set; }
        public string Characteristic_Name { get; set; }
        public string Result_Unit { get; set; }
        public string Result_Sample_Fraction { get; set; }
        public string Result_Analytical_Method_ID { get; set; }
        public string Result_Analytical_Method_Context { get; set; }
        public Nullable<decimal> Method_Detection_Level { get; set; }
        public Nullable<decimal> Lower_Reporting_Limit { get; set; }
        public string Result_Detection_Limit_Unit { get; set; }
        public string Method_Speciation { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserCreated { get; set; }
        public Nullable<bool> Valid { get; set; }
    }
}
