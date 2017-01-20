using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Reports
{
    public partial class MailingLabels : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ReportViewer1.Visible = false;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string selection = "";
            string cmdString = "";

            selection = RadioButtonList1.SelectedValue;

            if(selection.ToUpper().StartsWith("ALL"))
            {
                cmdString = "SELECT * FROM [ParticipantswithOrgName] order by OrganizationName, Lastname, Firstname"; 
            }

            if (selection.ToUpper().StartsWith("PARTICIPANTS"))
            {
                cmdString = "SELECT * FROM [ParticipantswithOrgName] where  ParticpantActive = 1 order by OrganizationName, Lastname, Firstname";
            }
            // SELECT * FROM [ParticipantswithOrgName] WHERE Active=1 AND PrimaryContact=1 "
            if (selection.ToUpper().StartsWith("ACTIVE"))
            {
                cmdString = "SELECT * FROM [ParticipantswithOrgName] where [OrgActive] = 1 and ParticpantActive = 1 AND PrimaryContact=1 order by OrganizationName, Lastname, Firstname";
            }

            SqlDataSource1.SelectCommand = cmdString;

            ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            ReportViewer1.LocalReport.Refresh();

            ReportViewer1.DataBind();
            ReportViewer1.Visible = true;
        }
    }
}