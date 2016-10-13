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
    public partial class EditUnknowns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sCommand = "";
            if (!IsPostBack)
            {
                FormView1.Visible = false;
            }
            if (Session["COMMAND"] != null) // reset the command each time
            {
                sCommand = (string)Session["COMMAND"];
                SqlDataSource1.SelectCommand = sCommand;
            }                  
            if(!IsPostBack)
            {
                btnNew.Visible = false;
            }

            if(Session["ORGID"] != null)
            {
                int orgID = (int) Session["ORGID"];
                countSamples(orgID); 
            }
        }

        // & u.Valid == true & u.Validated == false
        public int countSamples(int orgID)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();
            int sampsToValidate = 0;
            var U = (from u in RWE.UnknownSample
                     where u.OrganizationID == orgID 
                     select u);

            sampsToValidate = U.Count();

            if (sampsToValidate == 0)
            {

                lblNumberLeft.Text = "There are NO records to Edit";
                return 0;
            }    
                
            lblNumberLeft.Text = string.Format("There are {0} samples to Edit", sampsToValidate);
            return sampsToValidate; 
        }

        protected void btnSelectOrg_Click(object sender, EventArgs e)
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
                         where c.OrganizationName.ToUpper() == orgName
                         select c).FirstOrDefault();
                if (C == null)
                {
                    lblMsg.Text = "Please choose a valid organization";
                    return;
                }
                lblMsg.Text = "";

                orgID = C.ID;
                Session["ORGID"] = orgID; // save for later
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
            
            try
            {
                // first, check to see if there are any 
                //sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[UnknownSample] " +
                //    " where validated = 0 and OrganizationID = {0} and valid = 1 order by datesent desc ", orgID);
                sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[UnknownSample] " +
                    " where OrganizationID = {0} order by datesent desc ", orgID);
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
            btnNew.Visible = true;
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

        // OrganizationIDTextBox
        // need to populate the orgid text box with current org id from selection, if it existes
        /// <summary>
        ///  this comes from form button embedded in form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// don't think we need this
      

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            updateNumbers(); 
        }


        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            if (Session["ORGID"] != null)
            {
                int orgID = (int)Session["ORGID"];
                if (Session["NEW"] != null)
                {
                    if ((bool)Session["NEW"] )
                    {
                        FormView1.ChangeMode(FormViewMode.Insert);  // no samples so put in insert mode

                        TextBox TB = FormView1.FindControl("OrganizationIDTextBox") as TextBox;
                        if (TB != null)
                        {
                            TB.Text = orgID.ToString();
                        }
                        return;
                    }
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["NEW"] = true;
            FormView1.ChangeMode(FormViewMode.Insert);  // force into insert mode
            FormView1.Visible = true;
        }

        // used by ddl failures
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

        // user has entered a true value, so do math
        protected void TrueValueTextBox_TextChanged(object sender, EventArgs e)
        {
            updateNumbers();           
        }

        public void updateNumbers()
        {
            string tbValue1Name;
            string tbValue2Name;
            TextBox tb1;
            TextBox tb2;
            TextBox TBTrueValue;
            TextBox TBMean;
            TextBox TBPctRecovery;
            decimal Value1 = 0;
            decimal Value2 = 0;
            decimal mean = 0;
            decimal pctRecovery = 0;
            decimal trueValue = 0;
            string UID = FormView1.UniqueID;

            tbValue1Name = UID + "$" + "Value1TextBox";
            tbValue2Name = UID + "$" + "Value2TextBox";

            // TrueValueTextBox
            // MeanValueTextBox
            // PctRecoveryTextBox

            tb1 = this.FindControl(tbValue1Name) as TextBox;
            tb2 = this.FindControl(tbValue2Name) as TextBox;
            TBMean = this.FindControl(UID + "$" + "MeanValueTextBox") as TextBox;
            TBPctRecovery = this.FindControl(UID + "$" + "PctRecoveryTextBox") as TextBox;
            TBTrueValue = this.FindControl(UID + "$" + "TrueValueTextBox") as TextBox;

            if (tb1 == null)
            {
                return;
            }
            if (tb2 == null)
            {
                return;
            }
            if (TBMean == null)
                return;
            if (TBPctRecovery == null)
                return;
            if (TBTrueValue == null)
                return;

            // note: there must be two values.
            if (!decimal.TryParse(tb1.Text, out Value1))
            {
                return; // do nothing
            }

            if (!decimal.TryParse(tb2.Text, out Value2))
            {
                return; // do nothing
            }

            if (!decimal.TryParse(TBTrueValue.Text, out trueValue))
            {
                return; // do nothing
            }
            // here so we must have the 'right stuff' 

            mean = (Value1 + Value2) / 2;
            TBMean.Text = mean.ToString();
            pctRecovery = mean / trueValue * 100;
            TBPctRecovery.Text = pctRecovery.ToString(); 
        }

        // this is hooked to updated and inserted so both will reset 
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
    }
}