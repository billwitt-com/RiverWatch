using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Public
{
    public partial class OrgsByProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RiverWatchEntities NRWE = new RiverWatchEntities();
                var P = (from p in NRWE.Project

                         select new
                         {
                             p.ProjectName,
                             p.ProjectDescription
                         }).AsEnumerable();
                foreach (var x in P)
                {
                    ListItem LI = new ListItem(x.ProjectName, x.ProjectName);
                    ddlProjects.Items.Add(LI);
                }

                ListItem L = new ListItem("ALL");
                ddlProjects.Items.Insert(0, L);

                ReportViewer1.Visible = false;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string selectedValue = "";
            string selcmd = "SELECT * FROM [ViewStationsByProject] order by [projectname], [stationnumber] desc";

            if (ddlProjects.SelectedIndex != 0)
            {
                selectedValue = ddlProjects.SelectedValue;

                if (selectedValue.Length > 3)
                {
                    selcmd = string.Format("SELECT * FROM [ViewOrgsByProject] where [projectname] like '{0}' order by [OrganizationName]", selectedValue);
                }
            }

            SqlDataSource1.SelectCommand = selcmd;

            ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            ReportViewer1.LocalReport.Refresh();

            ReportViewer1.DataBind();
            ReportViewer1.Visible = true;
        }
    }
}