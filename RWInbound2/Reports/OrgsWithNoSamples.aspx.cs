using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Reports
{
    public partial class OrgsWithNoSamples : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int rows = e.AffectedRows;

            if (rows == 0)
            {
                lblNoResults.Text = "No results for this report";
                ReportViewer1.Visible = false;
            }
            else
            {
                lblNoResults.Text = "";
                ReportViewer1.Visible = true; 
            }

        }
    }
}