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
    public partial class AdminOrgStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sCommand = "";
            if (!IsPostBack)
            {
                FormView1.Visible = false;
                lblOrgName.Text = "";
                lblMsg.Text = "";
            }

            if (Session["COMMAND"] != null) // reset the command each time
            {
                sCommand = (string)Session["COMMAND"];
                SqlDataSource1.SelectCommand = sCommand;
                
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
                lblOrgName.Text = C.OrganizationName;

                Session["ORGID"] = orgID; 

                // first, check to see if there are any 
                //sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[UnknownSample] " +
                //    " where validated = 0 and OrganizationID = {0} and valid = 1 order by datesent desc ", orgID);
                // removed [RiverWatch].[dbo]. from select
                sCommand = string.Format("   select * from OrgStatus where OrganizationID = {0} order by ContractEndDate desc", orgID);
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
        }       
     
        protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            int orgID = 0;
            if (Session["ORGID"] != null)
            {
                orgID = (int)Session["ORGID"];
                string user = User.Identity.Name;
                e.Command.Parameters["@UserCreated"].Value = user;
                e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
                e.Command.Parameters["@OrganizationID"].Value = orgID;
            }
            else
            {
                lblMsg.Text = "Please select a valid Organization";
                return;
            }

        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = User.Identity.Name;
            e.Command.Parameters["@UserCreated"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
        }

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            int orgID = 0;
            string user = User.Identity.Name;
            if (Session["ORGID"] != null)
            {
                orgID = (int)Session["ORGID"];
                SqlDataSource1.InsertParameters["UserCreated"].DefaultValue = user;
                SqlDataSource1.InsertParameters["DateCreated"].DefaultValue = DateTime.Now.ToString(); 
                SqlDataSource1.InsertParameters[0].DefaultValue = orgID.ToString(); 
            }
            else
            {
                lblMsg.Text = "Please select a valid Organization";
                return;
            }
        }       
    }
}