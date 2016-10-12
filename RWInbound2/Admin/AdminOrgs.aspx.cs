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

namespace RWInbound2.Admin
{
    public partial class AdminOrgs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                btnAddNew.Visible = false; 
               // FormView1.DataBind();
              //  FormView1.Visible = true;
            }
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; // GlobalSite.RiverWatchDev;

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from Organization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                customers.Add(sdr["OrganizationName"].ToString());
                            }
                        }
                        conn.Close();
                        return customers;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return customers;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string orgName = "";
            string sqlCmd = ""; 
            orgName = tbOrgName.Text.Trim();
            FormView1.ChangeMode(FormViewMode.Edit); 
            if (orgName.Length > 2)
            {
                sqlCmd = string.Format("SELECT * FROM [organization] where OrganizationName like '{0}'", orgName);
                SqlDataSource1.SelectCommand = sqlCmd;
                FormView1.DataBind();
                FormView1.Visible = true;
            }
            btnAddNew.Visible = true; 
        }
      
        private string selectedValue;
        protected void PreventErrorsOn_DataBinding(object sender, EventArgs e)
        { 
            DropDownList theDropDownList = (DropDownList)sender;
            theDropDownList.DataBinding -= new EventHandler(PreventErrorsOn_DataBinding);
            theDropDownList.AppendDataBoundItems = true;

            selectedValue = "";
            try
            {
                theDropDownList.DataBind();
            }
            catch (ArgumentOutOfRangeException)
            {
                theDropDownList.Items.Clear();
                theDropDownList.Items.Insert(0, new ListItem("Please select", ""));
                theDropDownList.SelectedValue = "";
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
         //   FormView1.DefaultMode = FormViewMode.Insert;
            FormView1.ChangeMode(FormViewMode.Insert); 

            //DropDownList DDL1;
            //DropDownList DDL2; 
            //tbOrgName.Text = "";
            //FormView FV = FormView1; 
            //if (FV != null)
            //{
            //    DDL1 = FV.Controls[0].FindControl("ddlWaterShed") as DropDownList;
            //    DDL2 = FV.Controls[0].FindControl("ddlWaterShed") as DropDownList;
            //    if (DDL1 != null)
            //    {
            //        DDL1.DataSourceID = "SqlDataSourceWaterShed"; 
            //        DDL1.DataBind();
            //    }
            //    if (DDL2 != null)
            //    {
            //        DDL2.DataSourceID = "SqlDataSourceWSGathering";
            //        DDL2.DataBind();
            //    }
            //}
        }

        protected void FormView1_DataBinding(object sender, EventArgs e)
        {
            //DropDownList DDL1;
            //DropDownList DDL2;
            //tbOrgName.Text = "";
            //FormView FV = sender as FormView;
            //if (FV != null)
            //{
            //    DDL1 = FV.Controls[0].FindControl("ddlWaterShedUpdate") as DropDownList;
            //    DDL2 = FV.Controls[0].FindControl("ddlWaterShedEdit") as DropDownList;
            //    if (DDL1 != null)
            //        DDL1.DataBind();
            //    if (DDL2 != null)
            //        DDL2.DataBind();
            //}
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
           // DropDownList DDL1;
           // DropDownList DDL2;
           //// tbOrgName.Text = "";
           // FormView FV = sender as FormView;
           // if (FV != null)
           // {
           //     DDL1 = FV.Controls[0].FindControl("ddlWaterShed") as DropDownList;
           //     DDL2 = FV.Controls[0].FindControl("ddlWaterShed") as DropDownList;
           //     if (DDL1 != null)
           //         DDL1.DataBind();
           //     if (DDL2 != null)
           //         DDL2.DataBind();
           // }
        }         
    }
}