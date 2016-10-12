using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;

namespace RWInbound2.Admin
{
    public partial class EditInboundFieldData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    string cmd = SqlDataSource1.SelectCommand;
            //    if (Session["SELECTCOMMAND"] != null)
            //    {
            //        string selectcommand = (string)Session["SELECTCOMMAND"];
            //        SqlDataSource1.SelectCommand = selectcommand;
            //        GridView1.DataBind();
            //    }           
            //}
            if( Session["KITNUMBER"] != null)
            {
                int LocaLkitNumber = (int)Session["KITNUMBER"];
                int sampsToValidate = 0; 
                try
                {

                    string cmdCount = string.Format("SELECT count(InboundSamples.KitNum) FROM InboundSamples  " +
                         "  where InboundSamples.KitNum = {0}", LocaLkitNumber);

                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // RWE.Database.Connection.ConnectionString;
                            conn.Open();
                            cmd1.Connection = conn;
                            cmd1.CommandText = cmdCount;
                            sampsToValidate = (int)cmd1.ExecuteScalar();
                        }
                    }

                    if (sampsToValidate == 0)
                    {

                        lblNumberLeft.Text = "There are NO records to View";
                        return;
                    }
                    else
                        lblNumberLeft.Text = string.Format("There are {0} samples to View", sampsToValidate);
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
        }


        protected void btnSelectOrg_Click(object sender, EventArgs e)
        {
            int orgID = 0;
            int sampsToValidate = 0;
            string orgName = "";
            string sCommand = "";
            int LocaLkitNumber = -1;
            bool success;

            RiverWatchEntities RWE = new RiverWatchEntities();

            LocaLkitNumber = -1; // no real kit number yet
            if (tbKitNumber.Text.Length > 0)
            {
                success = int.TryParse(tbKitNumber.Text, out LocaLkitNumber);

                if (success) // we have a valid kit number, so process it
                {
                    var C = (from c in RWE.organizations
                             where c.KitNumber == LocaLkitNumber
                             select c).FirstOrDefault();
                    if (C == null)
                    {
                        lblMsg.Text = "Please choose a valid Kit Number";
                        return;
                    }
                    orgName = C.OrganizationName;
                    Session["KITNUMBER"] = LocaLkitNumber;
                }
            }
            else    // no valid kit number, so try for an org name
            {
                orgName = tbOrgName.Text;   // this may be empty if kit number is entered
                if (orgName.Length < 3)
                {
                    lblMsg.Text = "Please choose an organization or kit number";
                    return;
                }

                // get the org id and then set up a count of unknowns to work with
                var C = (from c in RWE.organizations
                         where c.OrganizationName.ToUpper() == orgName
                         select c).FirstOrDefault();
                if (C == null)
                {
                    lblMsg.Text = "Please choose a valid organization";
                    return;
                }

                orgID = C.ID;
                LocaLkitNumber = C.KitNumber.Value;
                Session["ORGID"] = orgID; // save for later
                Session["KITNUMBER"] = LocaLkitNumber;
            }


            try
            {

                string cmdCount = string.Format("SELECT count(InboundSamples.KitNum) FROM InboundSamples  " +
                     "  where InboundSamples.KitNum = {0}", LocaLkitNumber);

                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // RWE.Database.Connection.ConnectionString;
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = cmdCount;
                        sampsToValidate = (int)cmd.ExecuteScalar();
                    }
                }

                if (sampsToValidate == 0)
                {

                    lblNumberLeft.Text = "There are NO records to View";
                    return;
                }
                else
                    lblNumberLeft.Text = string.Format("There are {0} samples to View", sampsToValidate);
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

            // we have some samples to edit, so set up the query and bind to formview
            try
            {
               
                sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[InboundSamples] " +
                    " where KitNum = {0} and valid = 1 order by date desc ", LocaLkitNumber);
                Session["SELECTCOMMAND"] = sCommand;
                SqlDataSource1.SelectCommand = sCommand;
                Session["COMMAND"] = sCommand;
                tbKitNumber.Text = LocaLkitNumber.ToString();
                tbOrgName.Text = orgName; // fill in for user
                GridView1.Visible = true;
                GridView1.DataBind();

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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count)
        {
            List<string> orgs = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  //GlobalSite.RiverWatchDev;
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
                                orgs.Add(sdr["OrganizationName"].ToString());
                            }
                        }
                        conn.Close();
                        return orgs;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Web Method Validate Unknowns";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "validateField.aspx", ex.StackTrace.ToString(), nam, "Web Method, no reference to page");
                return orgs;
            }
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //if(Session["SELECTCOMMAND"] != null)
            //{
            //    string selectcommand = (string)Session["SELECTCOMMAND"];
            //    SqlDataSource1.SelectCommand = selectcommand;
            //    GridView1.DataBind(); 
            //}
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["SELECTCOMMAND"] != null)
            {
                string selectcommand = (string)Session["SELECTCOMMAND"];
             //   SqlDataSource1.SelectCommand = selectcommand;
              //  GridView1.DataBind();
            }
        } 

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (Session["SELECTCOMMAND"] != null)
            {
                //string selectcommand = (string)Session["SELECTCOMMAND"];
                //SqlDataSource1.SelectCommand = selectcommand;
              //  GridView1.DataBind();
            }
        }

        protected void SqlDataSource1_Deleting(object sender, SqlDataSourceCommandEventArgs e)
        {
            string delCommand = SqlDataSource1.DeleteCommand; 

           
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string updateCmd = SqlDataSource1.UpdateCommand;
            string selCommand = SqlDataSource1.SelectCommand; 

        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string updateCmd = SqlDataSource1.UpdateCommand;
            string selCommand = SqlDataSource1.SelectCommand; 
        }

        protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            string selCommand = SqlDataSource1.SelectCommand; 
            if (Session["SELECTCOMMAND"] != null)
            {
                string selectcommand = (string)Session["SELECTCOMMAND"];
           //     SqlDataSource1.SelectCommand = selectcommand;
             //   GridView1.DataBind();
            }
        }

        // this seems to work OK
        protected void SqlDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            string selCommand = SqlDataSource1.SelectCommand; 
            if (Session["SELECTCOMMAND"] != null)
            {
                string selectcommand = (string)Session["SELECTCOMMAND"];
          //      SqlDataSource1.SelectCommand = selectcommand;
              //  GridView1.DataBind();
            }
        }
    }
}