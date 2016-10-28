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
            //bool allowed = false;
            //allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            //if (!allowed)
            //    Response.Redirect("~/index.aspx");

            string sCommand = "";
            if (!IsPostBack)
            {
                FormView1.Visible = false;
                lblKitNumber.Visible = false;
                lblLastUsedText.Visible = false;
                chbStatusAdd.Visible = false;
            }
            if (Session["COMMAND"] != null) // reset the command each time
            {
                sCommand = (string)Session["COMMAND"];
                SqlDataSource1.SelectCommand = sCommand;
             //   FormView1.DataBind(); 
            }
            if(Session["NEW"] != null)
            {
                bool isnew = (bool) Session["NEW"];
                if (isnew)
                    chbStatusAdd.Visible = true;
                else
                    chbStatusAdd.Visible = false; 
            }
            else
                chbStatusAdd.Visible = false;
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
            int orgID = 0;

            string orgName = tbOrgName.Text.Trim().ToUpper();
            string sCommand = "";
            if (orgName.Length < 3)
            {
                lblMsg.Text = "Please choose an organization";
                return;
            }
            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();
                // get the org id and then set up a count of unknowns to work with
                var C = (from c in RWE.organizations
                         where c.OrganizationName.ToUpper() == orgName.ToUpper()
                         select c).FirstOrDefault();
                if (C == null)
                {
                    lblMsg.Text = "Please choose a valid organization";
                    return;
                }
                lblMsg.Text = "";
                orgID = C.ID;

                // first, check to see if there are any 
                //sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[UnknownSample] " +
                //    " where validated = 0 and OrganizationID = {0} and valid = 1 order by datesent desc ", orgID);
                // removed [RiverWatch].[dbo]. from select
                sCommand = string.Format(" select *  FROM [Organization] " +
                    " where OrganizationName = '{0}' ", orgName);
                SqlDataSource1.SelectCommand = sCommand;
                Session["COMMAND"] = sCommand;
                FormView1.Visible = true;
                FormView1.DataBind();
            }

            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
            btnAddNew.Visible = false; // turn off is org chosen
            lblKitNumber.Visible = false; 
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

        // this is our button
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Session["NEW"] = true;
            FormView1.ChangeMode(FormViewMode.Insert);  // force into insert mode
            FormView1.Visible = true;
            btnSelect.Visible = false;  // hide so we don't have any errors
            tbOrgName.Visible = false;
            btnAddNew.Visible = false; 

            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();

                int LKN = (int)(from c in RWE.organizations
                                select c.KitNumber).Max();

                lblKitNumber.Text = LKN.ToString();
                
                lblKitNumber.Visible = true;
                lblLastUsedText.Visible = true;
            }
            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Session["NEW"] = false;
            FormView1.ChangeMode(FormViewMode.ReadOnly);  // force into insert mode
            FormView1.DataBind();
        }

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Session["NEW"] = false;
            FormView1.ChangeMode(FormViewMode.ReadOnly);  // force into insert mode
            FormView1.DataBind();
        }

        protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            lblLastUsedText.Visible = false;
            lblKitNumber.Visible = false;
            lblMsg.Visible = false; 

            if (chbStatusAdd.Visible == true)
            {
                bool addStatus = chbStatusAdd.Checked;
                if (addStatus)
                {
                    if (Session["ORGID"] != null)
                    {
                        int orgID = (int)Session["ORGID"];
                        RiverWatchEntities RWE = new RiverWatchEntities();
                        OrgStatu OS = new OrgStatu();
                        OS.OrganizationID = orgID;
                        OS.DateCreated = DateTime.Now;
                        OS.UserCreated = User.Identity.Name;
                        RWE.OrgStatus.Add(OS);
                        RWE.SaveChanges();
                        lblMsg.Text = "New Org Status created";
                        chbStatusAdd.Checked = false; // just in case
                    }
                   
                }
            }
            // new record inserted, turn off all messages, etc

            chbStatusAdd.Visible = false;
            lblMsg.Visible = false;
            lblLastUsedText.Visible = false;
            lblKitNumber.Visible = false;
            btnSelect.Visible = true;
            tbOrgName.Visible = true;
            btnAddNew.Visible = true;

        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            chbStatusAdd.Visible = false;
            lblMsg.Visible = false;
            lblLastUsedText.Visible = false;
            lblKitNumber.Visible = false;
            btnSelect.Visible = true;
            tbOrgName.Visible = true;
            btnAddNew.Visible = true; 
        }

        protected void UpdateCancelButton_Click(object sender, EventArgs e)
        {
            chbStatusAdd.Visible = false;
            lblMsg.Visible = false;
            lblLastUsedText.Visible = false;
            lblKitNumber.Visible = false;
            btnSelect.Visible = true;
            tbOrgName.Visible = true;
            btnAddNew.Visible = true; 
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            chbStatusAdd.Visible = false;
            lblMsg.Visible = false;
            lblLastUsedText.Visible = false;
            lblKitNumber.Visible = false;
            btnSelect.Visible = true;
            tbOrgName.Visible = true;
            btnAddNew.Visible = true; 
        }   
    }
}