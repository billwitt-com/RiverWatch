﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnErrorLogReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ViewErrorLog.aspx");
        }
    }
}