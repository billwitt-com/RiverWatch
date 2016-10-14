using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWInbound2.View_Models
{
    public class ParticipantsViewModel
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public Nullable<int> YearSignedOn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string HomeEmail { get; set; }
        public string MailPreference { get; set; }
        public bool Active { get; set; }
        public bool PrimaryContact { get; set; }
        public bool Training { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string UserCreated { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
        public Nullable<bool> Valid { get; set; }
        public int ID { get; set; }
    }

    public class OrganizationParticipant
    {
        public int ID { get; set; }
        public string OrganizationName { get; set; }
    }
}