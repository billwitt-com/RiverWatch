using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWInbound2.View_Models
{
    public class ProjectStationViewModel
    {
        public List<Project> Projects { get; set; }
        public List<River> Rivers { get; set; }       
    }

    public class River
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}