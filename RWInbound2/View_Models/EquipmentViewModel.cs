using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWInbound2.View_Models
{
    public class EquipmentViewModel
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<System.DateTime> DateReceived { get; set; }
        public Nullable<System.DateTime> DateReJuv1 { get; set; }
        public Nullable<System.DateTime> DateReJuv2 { get; set; }
        public Nullable<System.DateTime> AutoReplaceDt { get; set; }
        public string Comment { get; set; }
    }
}