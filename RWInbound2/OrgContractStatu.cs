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
    
    public partial class OrgContractStatu
    {
        public string OrganizationName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<bool> ContractSigned { get; set; }
        public Nullable<System.DateTime> ContractSignedDate { get; set; }
        public Nullable<bool> SiteVisited { get; set; }
        public bool PrimaryContact { get; set; }
    }
}