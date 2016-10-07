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

namespace RWInbound2.Validation
{
    public partial class ValidateUnknowns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sCommand = "";
            int sampsToValidate = 0;
            int orgID = 0;
            if(!IsPostBack)
            {
                FormView1.Visible = false;
            }
            if (Session["COMMAND"] != null) // reset the command each time
            {
                sCommand = (string) Session["COMMAND"]; 
                SqlDataSource1.SelectCommand = sCommand;
            }
            string uniqueID = FormView1.Controls[0].UniqueID;
            compareTextBoxes("Value1TextBox", "Value2TextBox", uniqueID);

            if (Session["ORGID"] != null)
            {
                orgID = (int)Session["ORGID"];
                if (orgID != 0)
                {
                    countSamples(orgID);                     
                }
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
                LE.logError(msg, "validateUnknowns.aspx", ex.StackTrace.ToString(), nam, "Web Method, no reference to page");
                return orgs; 
            }
        }

        protected void btnSelectOrg_Click(object sender, EventArgs e)
        {
            int orgID = 0;
            int sampsToValidate = 0;
            string orgName = tbOrgName.Text.Trim().ToUpper();
            string sCommand = ""; 
            if(orgName.Length < 3)
            {
                lblMsg.Text = "Please choose an organization"; 
                return; 
            }
            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();
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
                Session["ORGID"] = orgID; // save for later
                // count the number of samples that are to be validated

                countSamples(orgID);
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

            // we have some samples to validate, so set up the query and bind to formview
            try
            {
                sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[UnknownSample] " +
                    " where validated = 0 and OrganizationID = {0} and valid = 1 order by datesent desc ", orgID);
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

        public void countSamples(int orgID)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();
            int sampsToValidate = 0; 
            var U = (from u in RWE.UnknownSample
                     where u.OrganizationID == orgID & u.Valid == true & u.Validated == false
                     select u);

            sampsToValidate = U.Count();

            if (sampsToValidate == 0)
            {

                lblNumberLeft.Text = "There are NO records to validate";
                return;
            }
            else
                lblNumberLeft.Text = string.Format("There are {0} samples to validate", sampsToValidate);
        }

        protected void btnBAD_Click(object sender, EventArgs e)
        {
            int unknownID = 0; 
            string unID = "";
            string uCommand = "";
            Label TB = FormView1.Controls[0].FindControl("UnknownSampleIDLabel1") as Label;
            if (TB != null)
            {
                unID = TB.Text.Trim();
                if (int.TryParse(unID, out unknownID))
                {
                    uCommand = string.Format("update [RiverWatch].[dbo].[UnknownSample]	set validated = 1, valid = 0 where [UnknownSampleID] = {0} ", unknownID);
                    SqlDataSource1.UpdateCommand = uCommand;
                    SqlDataSource1.Update();
                }
            }
            if (Session["ORGID"] != null)
            {
                int orgID = (int)Session["ORGID"];
                if (orgID != 0)
                {
                    countSamples(orgID);
                }
            }            
        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = User.Identity.Name;
            e.Command.Parameters["@UserCreated"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
          //  e.Command.Parameters["@Valid"].Value = 1;
        }

        public void compareTextBoxes(string tbName1, string tbName2, string UID)
        {
            string tbNName;
            string tbDName;
            TextBox tb1;
            TextBox tb2;
            decimal Value1 = 0;
            decimal Value2 = 0;

            tbNName = UID + "$" + tbName1; // +"_DTextBox";  // use the key value to build the name of the text box to be processed
            tbDName = UID + "$" + tbName2; // +"_TTextBox";

            tb1 = this.FindControl(tbNName) as TextBox;
            tb2 = this.FindControl(tbDName) as TextBox;

            if (tb1 == null)
            {
                string s = tbNName; // for debug, not used
                return;
            }
            if (tb2 == null)
            {
                string s = tbDName;
                return;
            }

            if (!decimal.TryParse(tb1.Text, out Value1))
            {
                return; // do nothing
            }

            if (!decimal.TryParse(tb2.Text, out Value2))
            {
                return; // do nothing
            }

            if (Value2 >= .0001m)    // make sure this is not too close to 0 as it may be 0 and we don't want to divide by 0
            {
                if (((Value1 / Value2) > 1.2m) | ((Value1 / Value2) < 0.80m))
                {
                    tb1.BorderColor = Color.Red;
                    tb1.BorderWidth = Unit.Pixel(2);
                    tb1.BorderStyle = BorderStyle.Solid;
                    tb1.ToolTip += string.Format(" - Values differ by more than 120% or less than 80%");
                }
                else
                {
                    tb1.BorderColor = Color.Black;
                    tb1.BorderWidth = Unit.Pixel(0);
                    tb1.BorderStyle = BorderStyle.None;
                    tb1.ToolTip += string.Format(" - Values are within range of 80% - 120%");
                }
            }
        }

        // only called when one of the two text boxes for values change
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            string uniqueID = FormView1.Controls[0].UniqueID;
            compareTextBoxes("Value1TextBox", "Value2TextBox", uniqueID);   
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int unknownID = 0;
            string unID = "";
            string uCommand = "";
            Label TB = FormView1.Controls[0].FindControl("UnknownSampleIDLabel1") as Label;
            if (TB != null)
            {
                unID = TB.Text.Trim();
                if (int.TryParse(unID, out unknownID))
                {
                    uCommand = string.Format("update [RiverWatch].[dbo].[UnknownSample]	set validated = 1, valid = 1 where [UnknownSampleID] = {0} ", unknownID);
                    SqlDataSource1.UpdateCommand = uCommand;
                    SqlDataSource1.Update();
                }
            }
            if (Session["ORGID"] != null)
            {
                int orgID = (int)Session["ORGID"];
                if (orgID != 0)
                {
                    countSamples(orgID);
                }
            }            

        }
    }
}