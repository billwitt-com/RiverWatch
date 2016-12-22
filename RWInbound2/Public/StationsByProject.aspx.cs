using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Providers.Entities;
using System.Drawing;
using Microsoft.Reporting.WebForms; 

namespace RWInbound2.Reports
{
    public partial class StationsByProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

            if(ddlProjects.SelectedIndex != 0)
            {
                selectedValue = ddlProjects.SelectedValue;

                if(selectedValue.Length > 3)
                {
                    selcmd = string.Format("SELECT * FROM [ViewStationsByProject] where [projectname] like '{0}' order by [stationnumber] desc", selectedValue);
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