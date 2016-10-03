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
    public partial class ValidateField : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sampsToValidate = 0;
            if (Session["COMMAND"] != null) // reset the command each time
            {
                string sCommand = (string)Session["COMMAND"];
                SqlDataSource1.SelectCommand = sCommand;
            }
            else
            {
                SqlDataSource1.SelectCommand = "";
                FormView1.Visible = false;
            }
            if (Session["KITNUMBER"] != null)
            {
                int kitNumber = (int)Session["KITNUMBER"];
                try
                {
                    RiverWatchEntities RWE = new RiverWatchEntities();
                    // count the number of samples that are to be validated

                    var U = (from u in RWE.InboundSamples
                             where u.KitNum == kitNumber & u.Valid == true & u.PassValStep != -1
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

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }

        // mark record invalid and move on
        protected void btnBAD_Click(object sender, EventArgs e)
        {
            int unknownID = 0;
            string unID = "";
            string uCommand = "";
            try
            {
                TextBox TB = FormView1.Controls[0].FindControl("txtSampleIDTextBox") as TextBox;
                if (TB != null)
                {
                    unID = TB.Text.Trim();
                    if (int.TryParse(unID, out unknownID))
                    {
                        uCommand = string.Format("update [RiverWatch].[dbo].[InboundSamples] set valid = 0 where [ID] = {0} ", unknownID);
                        SqlDataSource1.UpdateCommand = uCommand;
                        SqlDataSource1.Update();
                    }
                }
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

        // here for any value change where we need to do any checking
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            //          TextBox_TextChanged
            string uniqueID = FormView1.Controls[0].UniqueID;
            compareTextBoxes(uniqueID);
        }

        public void compareTextBoxes(string UID)
        {
            string tb1Name;
            string tb2Name;
            TextBox tb1;
            TextBox tb2;
            decimal Value1 = 0;
            decimal Value2 = 0;
            decimal pH = 0;
            decimal PhenolAlk = 0;
            decimal TotalAlk = 0;
            decimal DO = 0;
            decimal DOSat = 0;
            decimal Hardness = 0;
            decimal TempC = 0; 
            Label lblpH;
            Label lblPhenol;
            Label lblAlkTotal;
            Label lblHardness;
            Label lblDO;
            Label lblDoSat;
            Label lblTempC; 

            // hook up controls from formview1 what a PITA

            lblTempC = this.FindControl(UID + "$" + "lblTempC") as Label;
            if (lblTempC == null)
                return;

            lblpH = this.FindControl(UID + "$" + "lblpH") as Label;
            if (lblpH == null)
                return;

            lblPhenol = this.FindControl(UID + "$" + "lblPhenol") as Label;
            if (lblPhenol == null)
                return;

            lblAlkTotal = this.FindControl(UID + "$" + "lblAlkTotal") as Label;
            if (lblAlkTotal == null)
                return;

            lblHardness = this.FindControl(UID + "$" + "lblHardness") as Label;
            if (lblHardness == null)
                return;

            lblDO = this.FindControl(UID + "$" + "lblDO") as Label;
            if (lblDO == null)
                return;

            lblDoSat = this.FindControl(UID + "$" + "lblDoSat") as Label;
            if (lblDoSat == null)
                return;

            tb1 = this.FindControl(UID + "$" + "TempCTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (!decimal.TryParse(tb1.Text, out TempC))  // pH         
                return; // do nothing
    

            tb1Name = UID + "$" + "PHTextBox";  // Value1 PH
            tb2Name = UID + "$" + "PhenAlkTextBox";

            tb1 = this.FindControl(tb1Name) as TextBox;
            tb2 = this.FindControl(tb2Name) as TextBox;

            if (tb1 == null)
                return;
            if (tb2 == null)
                return;
            if (!decimal.TryParse(tb1.Text, out Value1))  // pH         
                return; // do nothing
            if (!decimal.TryParse(tb2.Text, out Value2))
                return;

            pH = Value1;
            PhenolAlk = Value2;

            tb1Name = UID + "$" + "AlkTotalTextBox";  // Value1 PH
            tb2Name = UID + "$" + "TotalHardTextBox";

            tb1 = this.FindControl(tb1Name) as TextBox;
            tb2 = this.FindControl(tb2Name) as TextBox;

            if (tb1 == null)
                return;
            if (tb2 == null)
                return;
            if (!decimal.TryParse(tb1.Text, out Value1))  // pH         
                return; // do nothing
            if (!decimal.TryParse(tb2.Text, out Value2))
                return;
            TotalAlk = Value1;
            Hardness = Value2;

            tb1Name = UID + "$" + "DOTextBox";  // Value1 PH
            tb2Name = UID + "$" + "DOSatTextBox";

            tb1 = this.FindControl(tb1Name) as TextBox;
            tb2 = this.FindControl(tb2Name) as TextBox;

            if (tb1 == null)
                return;
            if (tb2 == null)
                return;
            if (!decimal.TryParse(tb1.Text, out Value1))  // pH         
                return; // do nothing
            if (!decimal.TryParse(tb2.Text, out Value2))
                return;

            DO = Value1;
            DOSat = Value2;


            // rule: if pH < 8.3 AND pHAlk > 0 == warning
            if ((PhenolAlk > 0) & (pH < 8.3M))
            {
                lblPhenol.Text = "PhenAlk > 0 while pH < 8.3";
                lblPhenol.ForeColor = System.Drawing.Color.Red; 
            }
            else
            {
                lblPhenol.Text = "";
                lblPhenol.ForeColor = System.Drawing.Color.Black;  
            }

            // rule: TotalAlk > Hardness + 10
            // rule: Hardness - TotalAlk > 10

            if (Hardness - TotalAlk > 10)
            {
                lblHardness.Text = "Hardness - TotalAlk > 10";
                lblHardness.ForeColor = System.Drawing.Color.Red; 
            }
            else
            {
                lblHardness.Text = "";
                lblHardness.ForeColor = System.Drawing.Color.Black; 
            }

            // rule PhenAlk > 60

            if(PhenolAlk > 60)
            {
                lblPhenol.Text = "PhenolAlk > 60";
                lblPhenol.ForeColor = System.Drawing.Color.Red; 
            }
            else
            {
                lblPhenol.Text = "";
                lblPhenol.ForeColor = System.Drawing.Color.Black; 
            }

            // pH > 9.1
            if(pH > 9.1m)
            {
                lblpH.Text = "pH > 9.1";
                lblpH.ForeColor = System.Drawing.Color.Red; 
            }

            // rule pH < 4.5
            if (pH < 4.5m)
            {
                lblpH.Text = "pH < 4.5";
                lblpH.ForeColor = System.Drawing.Color.Red;
            }

            if((pH > 4.5m) & (pH < 9.1m))
            {
                lblpH.Text = "";
                lblpH.ForeColor = System.Drawing.Color.Black;
            }

            // rule: DO > 14 = warning
            // rule: DO < 4.5

            if (DO > 14m)
            {
                lblDO.Text = "DO > 14";
                lblDO.ForeColor = System.Drawing.Color.Red;
            }

            if (DO < 4.5m)
            {
                lblDO.Text = "DO < 4.5";
                lblDO.ForeColor = System.Drawing.Color.Red;
            }

            if ((DO > 4.5m) & (DO < 14.0m))
            {
                lblDO.Text = "";
                lblDO.ForeColor = System.Drawing.Color.Black;
            }

            // rule 50 < DOSat > 110
            if (DOSat > 110)
            {

                lblDoSat.Text = "DOSat > 110";
                lblDoSat.ForeColor = System.Drawing.Color.Red;
            }

            if (DOSat < 50.0m)
            {
                lblDoSat.Text = "DOSat < 50";
                lblDoSat.ForeColor = System.Drawing.Color.Red;
            }

            if ((DOSat > 50) & (DOSat < 110.0m))
            {
                lblDoSat.Text = "";
                lblDoSat.ForeColor = System.Drawing.Color.Black;
            }

            // rule If TempC > 25
            if(TempC > 25.0m)
            {
                lblTempC.Text = "Temp C > 25";
                lblTempC.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblTempC.Text = "";
                lblTempC.ForeColor = System.Drawing.Color.Black;
            }
        }
      
        protected void btnSelectOrg_Click(object sender, EventArgs e)
        {
            int orgID = 0;
            int kitNumber = 0;
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
                kitNumber = C.KitNumber.Value;
                Session["ORGID"] = orgID; // save for later
            }

            // ++++++++++++++++++++++++
            try
            {

                // count the number of samples that are to be validated

                var U = (from u in RWE.InboundSamples
                         where u.KitNum == LocaLkitNumber & u.Valid == true & u.PassValStep == -1
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
                sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[InboundSamples] " +
                    " where KitNum = {0} and valid = 1 and passValStep = -1 order by date desc ", LocaLkitNumber);
                SqlDataSource1.SelectCommand = sCommand;
                Session["COMMAND"] = sCommand;
                tbKitNumber.Text = LocaLkitNumber.ToString();
                tbOrgName.Text = orgName; // fill in for user
                FormView1.Visible = true;
                FormView1.DataBind();
                string uniqueID = FormView1.Controls[0].UniqueID;
                compareTextBoxes(uniqueID);
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

        // need to set pasval = 1 so it is recorded as validated
        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {

        }
    }
}