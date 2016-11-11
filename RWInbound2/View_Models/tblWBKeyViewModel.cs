using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWInbound2.View_Models
{
    public class tblWBKeyViewModel
    {
        public int ID { get; set; }
        public string WBID { get; set; }

        public string WATERSHED { get; set; }        

        public List<tlkRiverWatchWaterShed> RiverWatchWaterSheds { get; set; }

        public string BASIN { get; set; }

        public List<tlkWQCCWaterShed> BASINWQCCWaterSheds { get; set; }

        public string SUBBASIN { get; set; }
        
        public List<tlkWQCCWaterShed> SUBBASINWQCCWaterSheds { get; set; }

        public string REGION { get; set; }

        public string SEGMENT { get; set; }

        public string DESIG { get; set; }

        public string SegDesc { get; set; }

        public Nullable<System.DateTime> VerifyDate { get; set; }

        public string Comment { get; set; }
    }
}