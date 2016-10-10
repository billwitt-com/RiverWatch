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
            bool allowed = false;

            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx"); 

            if (Session["COMMAND"] != null) // reset the command each time
            {
                string sCommand = (string)Session["COMMAND"];
                SqlDataSource1.SelectCommand = sCommand;
                FormView1.Visible = true; 
            }
            else
            {
                SqlDataSource1.SelectCommand = "";
                FormView1.Visible = false;
            }
            if (Session["KITNUMBER"] != null)
            {
                int kitNumber = (int)Session["KITNUMBER"];

                if (!countSamples(kitNumber))
                    return;   
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
            string uniqueID = FormView1.Controls[0].UniqueID;
            compareTextBoxes(uniqueID);
        }

        public void compareTextBoxes(string UID)
        {
            TextBox tb1;
            decimal pH = 0;
            bool ispH = false;
            decimal PhenolAlk = 0;
            bool isPhenolAlk = false;
            decimal TotalAlk = 0;
            bool isTotalAlk = false;
            decimal DO = 0;
            bool isDO = false;
            decimal DOSat = 0;
            bool isDOSat = false;
            decimal Hardness = 0;
            bool isHardness = false;
            decimal TempC = 0;
            bool isTempC = false; 
            Label lblpH;
            Label lblPhenol;
            Label lblAlkTotal;
            Label lblHardness;
            Label lblDO;
            Label lblDoSat;
            Label lblTempC;
            string Pmsg1 = "";
            string Pmsg2 = ""; 

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

            // scrape page data 

            UID += "$"; // because I cut and pasted the code below
            tb1 = this.FindControl(UID + "TempCTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out TempC))
                    isTempC = true;
            }

            tb1 = this.FindControl(UID + "PHTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out pH))
                    ispH = true;
            }

            tb1 = this.FindControl(UID + "PhenAlkTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out PhenolAlk))
                    isPhenolAlk = true;
            }

            tb1 = this.FindControl(UID + "AlkTotalTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out TotalAlk))
                    isTotalAlk = true;
            }

            tb1 = this.FindControl(UID + "TotalHardTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out Hardness))
                    isHardness = true;
            }

            tb1 = this.FindControl(UID + "DOSatTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out DOSat))
                    isDOSat = true;
            }

            tb1 = this.FindControl(UID + "DOTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out DO))
                    isDO = true;
            }

            // rule: if pH < 8.3 AND pHAlk > 0 == warning
            if (isPhenolAlk & ispH)
            {
                if ((PhenolAlk > 0) & (pH < 8.3M))
                {
                    Pmsg1 = "PhenAlk > 0 while pH < 8.3";
                    lblPhenol.Text = Pmsg1; 
                    lblPhenol.ForeColor = System.Drawing.Color.Red;
                }
                else  // we are removing msg1 but not msg2, if it exists
                {
                    if (Pmsg2.Length > 5)
                    {
                        lblPhenol.Text = Pmsg2;                         
                    }
                    else
                    {
                        Pmsg1 = ""; 
                        lblPhenol.Text = "";
                        lblPhenol.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }

            // rule: TotalAlk > Hardness + 10
            // rule: Hardness - TotalAlk > 10
            if (isTotalAlk & isHardness)
            {
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
            }

            // rule PhenAlk > 60
            if (isPhenolAlk)
            {
                if (PhenolAlk > 60)
                {
                    Pmsg2 = "PhenolAlk > 60";
                    if (Pmsg1.Length > 2)
                        lblPhenol.Text = Pmsg1 + Pmsg2;
                    else
                        lblPhenol.Text = Pmsg2; 
                    lblPhenol.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (Pmsg1.Length > 2)
                        lblPhenol.Text = Pmsg1; 
                    else
                    {
                        Pmsg2 = "";
                        lblPhenol.Text = "";
                        lblPhenol.ForeColor = System.Drawing.Color.Black;
                    }
                }                
            }

            // pH > 9.1
            if (ispH)
            {
                if (pH > 9.1m)
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

                if ((pH > 4.5m) & (pH < 9.1m))
                {
                    lblpH.Text = "";
                    lblpH.ForeColor = System.Drawing.Color.Black;
                }
            }

            // rule: DO > 14 = warning
            // rule: DO < 4.5
            if (isDO)
            {
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
            }

            // rule 50 < DOSat > 110
            if (isDOSat)
            {
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
            }

            // rule If TempC > 25
            if (isTempC)
            {
                if (TempC > 25.0m)
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
        }
      
        protected void btnSelectOrg_Click(object sender, EventArgs e)
        {
            int orgID = 0;

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
            // valid kit number here... 

            if(!countSamples(LocaLkitNumber))
                return;           

            // we have some samples to validate, so set up the query and bind to formview
            try
            {
                sCommand = string.Format(" select *  FROM [RiverWatch].[dbo].[InboundSamples] JOIN Samples on InboundSamples.SampleID = " +
                       " Samples.SampleNumber " +
                    " where KitNum = {0} and [RiverWatch].[dbo].[InboundSamples].[valid] = 1 and Samples.Valid = 1 and passValStep = -1 order by date desc ", LocaLkitNumber);

                //string cmdCount = string.Format("SELECT  count(InboundSamples.KitNum) FROM InboundSamples JOIN Samples on InboundSamples.SampleID = " +
                //   " Samples.SampleNumber where InboundSamples.Valid = 1 and PassValStep = -1 and InboundSamples.KitNum  {0}", kitNumber);

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

        // need to set pasval = 1 so it is recorded as validated [PassValStep]
        // then update NEWexpwater with this data 

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@PassValStep"].Value = 1; // mark record as validated
            e.Command.Parameters["@Valid"].Value = true; 

            RiverWatchEntities RWE = new RiverWatchEntities();
            NEWexpWater NEW = new NEWexpWater();

            bool existingRecord = false; 
            TextBox tb1;
            DateTime sampleDate = DateTime.Now;
            bool isSampleDate = false;
            decimal pH = 0;
            bool ispH = false;
            decimal PhenolAlk = 0;
            bool isPhenolAlk = false;
            decimal TotalAlk = 0;
            bool isTotalAlk = false;
            decimal DO = 0;
            bool isDO = false; 
            decimal DOSat = 0;
            bool isDOSat = false;
            decimal Hardness = 0;
            bool isHardness = false;
            decimal TempC = 0;
            bool isTempC = false; 
            string FieldComments = "";
            string SampleNumber = ""; // sampleid
            decimal Flow = 0;
            bool isFlow = false;
            string eventID = "";
            int tblSampleID = 0;
            string UID = FormView1.Controls[0].UniqueID + "$";

            // scrape page data 
            // DateTextBox

            tb1 = this.FindControl(UID + "DateTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (DateTime.TryParse(tb1.Text, out sampleDate))
                    isSampleDate = true;                
            }

            tb1 = this.FindControl(UID + "USGSFlowTextBox") as TextBox;
            if (tb1 == null)
                return;
            if(tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out Flow)) // pH      
                    isFlow = true; 
            }

            tb1 = this.FindControl(UID +"TempCTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out TempC))
                    isTempC = true; 
            }

            tb1 = this.FindControl(UID + "PHTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out pH))
                    ispH = true;
            }

            tb1 = this.FindControl(UID + "PhenAlkTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out PhenolAlk))
                    isPhenolAlk = true;
            }

            tb1 = this.FindControl(UID + "AlkTotalTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out TotalAlk))
                    isTotalAlk = true;
            }

            tb1 = this.FindControl(UID + "TotalHardTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out Hardness))
                    isHardness = true; 
            }

            tb1 = this.FindControl(UID + "DOSatTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out DOSat))
                    isDOSat = true; 
            }

            tb1 = this.FindControl(UID + "DOTextBox") as TextBox;
            if (tb1 == null)
                return;
            if (tb1.Text.Length > 0)
            {
                if (decimal.TryParse(tb1.Text, out DO))
                    isDO = true;
            }

            tb1 = this.FindControl(UID + "CommentsTextBox") as TextBox;
            if (tb1 == null)
                return;
            FieldComments = tb1.Text.Trim();

            //sampleid

            tb1 = this.FindControl(UID + "SampleIDTextBox") as TextBox;    // this is populated by query and binding
            if (tb1 == null)
                return;
            SampleNumber = tb1.Text.Trim();

              try
            {
                NEWexpWater TEST = (from t in RWE.NEWexpWaters
                                    where t.Valid == true & t.SampleNumber == SampleNumber
                                    select t).FirstOrDefault();
                if (TEST != null)
                {
                    // skip these as they are not our business to insert into a row that already exists
                    // items like kit number, etc. will be here already as a result of inserting field or nutrient data earlier
                    NEW = TEST; // keep the name common to this method
                    existingRecord = true; // flag for later
                }
                else
                {
                    NEW = new NEWexpWater(); // create new entity as there is not one yet
                    // now we must get eventID or numberSample from Samples table

                    var Q = (from q in RWE.Samples
                             where q.SampleNumber == SampleNumber & q.Valid == true
                             select q).FirstOrDefault();
                    eventID = (string)Q.NumberSample;
                    tblSampleID = Q.ID; 
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

              try
              {

                  if (!existingRecord) // Should not happen - no existing record, so we are first
                  {
                      NEW.SampleNumber = SampleNumber;
                      NEW.Event = eventID;
                      NEW.tblSampleID = tblSampleID; 
                  }

                  if(ispH)
                    NEW.PH = (double)pH;
                  if(isPhenolAlk)
                    NEW.PHEN_ALK = (double)PhenolAlk;
                  if(isFlow)
                    NEW.USGS_Flow = (double)Flow;
                  if(isTempC)
                    NEW.TempC = (double)TempC;
                  if(isTotalAlk)
                    NEW.TOTAL_ALK = (double)TotalAlk;
                  if(isHardness)
                    NEW.TOTAL_HARD = (double)Hardness;
                  if(isDO)
                    NEW.DO_MGL = (double)DO;
                  if(isDOSat)
                    NEW.DOSAT = (short)DOSat;
                  if (isSampleDate)
                      NEW.SampleDate = sampleDate; 
                  NEW.FieldComment = FieldComments;
                  if (!existingRecord)
                  {
                      RWE.NEWexpWaters.Add(NEW);
                  }
                  RWE.SaveChanges();

            // now update inbound to mark valided
                  
                      var IBS = from i in RWE.InboundSamples
                                where i.SampleID == SampleNumber & i.Valid == true
                                select i;
                      if (IBS.Count() > 0) // will always happen... :)
                      {
                          foreach (var z in IBS)
                          {
                              z.PassValStep = 1;
                          }
                          RWE.SaveChanges();
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

              if (Session["KITNUMBER"] != null)
              {
                  int kitNumber = (int)Session["KITNUMBER"];

                  if (!countSamples(kitNumber))
                      return;
              }
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            // use this event to populate time string, if it is not already there 
            // will be nice for validation.
            FormView FV = sender as FormView;
            string tmString = ""; 
            TextBox T = FV.Controls[0].FindControl("TimeTextBox") as TextBox;
            if (T != null)
            {
                tmString = T.Text;

                if (tmString.Length < 4)
                {
                    TextBox TB = FV.Controls[0].FindControl("SampleIDTextBox") as TextBox;

                    //string sampHours = TB.Text.Substring(TB.Text.Length - 4, 2); // get first 2 chars of last 4 chars
                    //string sampMins = TB.Text.Substring(TB.Text.Length - 2);
                    //T.Text = string.Format("{0:D4}:{1:D2}", sampHours, sampMins);
                    string sampHours = TB.Text.Substring(TB.Text.Length - 4, 2); // get first 2 chars of last 4 chars
                    string tm = TB.Text.Substring(TB.Text.Length - 4);
                    T.Text = string.Format("{0:D4}", tm);
                }
            }
        }
        public bool countSamples(int kitNumber)
        {
            int sampsToValidate = 0; 

            string cmdCount = string.Format("SELECT count(InboundSamples.KitNum) FROM InboundSamples JOIN Samples on InboundSamples.SampleID = " +
            " Samples.SampleNumber where InboundSamples.Valid = 1 and Samples.Valid = 1 and PassValStep = -1 and InboundSamples.KitNum = {0}", kitNumber);

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

            if (sampsToValidate < 0)
            {

                lblNumberLeft.Text = "There are NO records to validate";
                return false; 
            }
            else
                lblNumberLeft.Text = string.Format("There are {0} samples to validate", sampsToValidate);
            return true; 
        }
    }
}