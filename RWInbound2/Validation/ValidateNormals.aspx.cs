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
using System.Web.Providers.Entities;
using System.Runtime.Serialization;
using System.IO;

// reviewed this and I believe all is good. It adds all station, org and sample detail to new record. bwitt 01/04/'17

namespace RWInbound2.Validation
{
    public partial class ValidateNormals : System.Web.UI.Page
    {
        Dictionary<string, decimal> D2TLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> MeasurementLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        RiverWatchEntities NRWDE = new RiverWatchEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            string name = "";
            decimal D2Tvalue = 0;
            decimal MeasurementValue = 0;
            bool allowed = false; 

            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx");

            // count rows of not saved, valid Blanks first
            // removed  [Riverwatch].[dbo].
            SqlDataSourceNormals.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '0' and valid = 1 and saved = 0";    // this is the working table

            System.Data.DataView result = (DataView)SqlDataSourceNormals.Select(args);
            // int rowCount = result.Table.Rows.Count;

            int rowCount = result.Table.Rows.Count;
            if (rowCount == 0)
            {
                lblCount.Text = "There are NO ICP Normal records to validate";
                pnlHelp.Visible = false; // make sure user does not see this unless requested
                return;
            }
            else
                lblCount.Text = string.Format("There are {0} ICP Normal records to validate", rowCount);

            // Session["OURTABLE"] = DT; // save for later use        

            if (!IsPostBack)
            {
                // first thing, make copies of incomingICP record to preserve history and have a final copy of the edited data
                // this means we will now be working, not from the origonial inbound table but the table that is a copy.
                // we will also make an archive copy so we can do stats and such.
                // do this every pass since the data could be changing, ie new records incoming
                //try
                //{
                //    using (SqlConnection conn = new SqlConnection())
                //    {
                //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
                //        using (SqlCommand cmd = new SqlCommand())
                //        {
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            SqlParameter userName = cmd.Parameters.Add("@User", SqlDbType.NVarChar, 90);
                //            userName.Direction = ParameterDirection.Input;
                //            userName.Value = User.Identity.Name;            //"Bill for Now";
                //            cmd.CommandText = "[UpdateLocalTablesFromIncomingICP]"; // name of the sproc 
                //            cmd.Connection = conn;
                //            conn.Open();
                //            rowsAffected = cmd.ExecuteNonQuery(); // not accurate as there are two updates so only result of second will show up here... 
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    string nam = "";
                //    if (User.Identity.Name.Length < 3)
                //        nam = "Not logged in";
                //    else
                //        nam = User.Identity.Name;
                //    string msg = ex.Message;
                //    LogError LE = new LogError();
                //    LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                //}

                Session["CONTROLSSET"] = false;
                // fill in limits values
                pnlHelp.Visible = false; // make sure user does not see this unless requested

                try
                {
                    // changed this to use tlkLimits as they seem to correspond to Barb's note. 
                    // removed [Riverwatch].[dbo].
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; //GlobalSite.RiverWatchDev;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = string.Format("select distinct Element, DvsTDifference, MDL from  [tlkLimits]");
                            cmd.Connection = conn;
                            conn.Open();

                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                if (sdr.HasRows)
                                {
                                    while (sdr.Read())
                                    {
                                        if (sdr["Element"].GetType() != typeof(System.DBNull))      // is this crap or what???
                                        {
                                            name = ((string)sdr["Element"]).ToUpper();  // make upper case to be sure
                                            D2Tvalue = (decimal)sdr["DvsTDifference"];
                                            MeasurementValue = (decimal)sdr["MDL"];
                                            D2TLimits.Add(name, D2Tvalue);
                                            MeasurementLimits.Add(name, MeasurementValue);
                                        }
                                    }
                                }
                            }

                            conn.Close();
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

                Session["HighLimit"] = D2TLimits;   // SAVE
                Session["MEASUREMENTLIMITS"] = MeasurementLimits;

             
            }
        }

        /// <summary>
        /// Reads each text box and does validation steps 
        /// </summary>
        public void setControls()
        {
            TextBox tbD;
            TextBox tbT;
            string barCode = "";
            string dupCode = "";
            decimal D2Tlimit = 0;
            decimal MeasureLimit = 0;
            string tbDName;
            string tbTName;
            string codeTextBoxName;

            decimal Total;
            decimal Disolved;
            lblNote.Text = "";
            lblNote.ForeColor = Color.White;
            int pageWeAreOn = 0;
            string uniqueID = ""; 


            // get the dictionaries of limits from session state - these do not change during session
            Dictionary<string, decimal> D2TLimits = (Dictionary<string, decimal>)Session["HighLimit"];  //  Session["HighLimit"] = HighLimit;  
            Dictionary<string, decimal> MeasurementLimits = (Dictionary<string, decimal>)Session["MEASUREMENTLIMITS"];

            uniqueID = FormViewNormals.Controls[0].UniqueID;
            // process each metals data row to see if it falls out of 'specs' 
            // using data from limits table, which makes this easy to name the text boxes and scrape them
            foreach (string item in D2TLimits.Keys)
            {
                tbDName = uniqueID + "$" + item + "_DTextBox";  // use the key value to build the name of the text box to be processed
                tbTName = uniqueID + "$" + item + "_TTextBox";
                D2Tlimit = D2TLimits[item];

                tbD = this.FindControl(tbDName) as TextBox;
                tbT = this.FindControl(tbTName) as TextBox;


                if (tbD == null)    // skip any junk that does not result in text box
                    continue;
                if (tbT == null)
                    continue; 

                if (!decimal.TryParse(tbT.Text, out Total))
                {
                    Total = 0;
                }

                if (!decimal.TryParse(tbD.Text, out Disolved))
                {
                    Disolved = 0;
                }

                // see if the difference between the two is greater than the D2Tlimit
                if ((Disolved - Total) >= D2Tlimit)
                {
                    tbD.BackColor = Color.PowderBlue;
                    tbD.ToolTip = string.Format("Dissolved - Total is greater than limit of {0}", D2Tlimit);
                    tbT.BackColor = Color.PowderBlue;
                    tbT.ToolTip = string.Format("Dissolved - Total is greater than limit of {0}", D2Tlimit);
                }
                else
                {
                    tbD.BackColor = Color.White;
                    tbT.BackColor = Color.White;
                    tbD.ToolTip = string.Format("Dissolved - Total is less than limit of {0}", D2Tlimit);
                    tbT.ToolTip = string.Format("Dissolved - Total is less than limit of {0}", D2Tlimit);
                }                
            }
        }

        // user updated a value in a text box, so re-calc the values
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            setControls(); // update color schemes
        }

        protected void FormViewNormals_DataBound(object sender, EventArgs e)
        {
            setControls(); // update color schemes
        }

        /// <summary>
        ///  help buttons, hide or not help text in panel - Div
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDone_Click(object sender, EventArgs e)
        {
            pnlHelp.Visible = false;
            btnHelp.Visible = true;
        }

        protected void btnHelp_Click(object sender, EventArgs e)
        {
            pnlHelp.Visible = true;
            btnHelp.Visible = false;
        }
     
        // user has edited (or just accepts) the blank and now we will save it to table
        // must update page as there will one fewer blanks to process
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            CommitNormal("UPDATE");
        }

        // user has chosen to mark as bad blank and now we will save it to XXXX table
        // must update page as there will one fewer blanks to process
        protected void btnBadNormal_Click(object sender, EventArgs e)
        {
            CommitNormal("BAD");
        }

        // type:
        // EDIT user changed one value in text box but we will not update record until button is pressed, then scrape screen
        // table inboundICPFinal - which will hold all edits until the record is 'saved' to the new expNewWater table
        // this code is NOT correct in that it does not update record but sends it to new water table
        // must update the final table also. 

        public void CommitNormal(string type)
        {
            RiverWatchEntities NewRWE = new RiverWatchEntities(); // new database RiverWatch 
            NEWexpWater NEW = null;
            //dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2(); // get access to old db for details, this is temp. XXXX
            bool existingRecord = false;
            decimal Total;
            decimal Disolved;
            bool isbad = false;
            string barCode = "";
            string typeCode = "";
            int OrgID = 0;
            int KitNumber = 0;
            string comment = "";
            int sID = 0;
            int orgID = 0;
            int stnID = 0;
            string uniqueID = "";
            bool haveCurrentBarcode = false;
            bool haveOtherBarcode = false;
            List<string> EsistingBarCodes = new List<string>(); 

            int NewExpWaterRows = 0;

            try
            {
                uniqueID = FormViewNormals.Controls[0].UniqueID;

                // XXXX another WTF moment, why is the uniqueid different here, just because we pressed a button
                // note that the data is correct when we have the correct string
                uniqueID = uniqueID.Replace("$ctl00", "");
                // scrape text box strings, which will never be null, but can be zero length
                string codeTextBoxName = uniqueID + "$" + "tbCode"; // get the text box off the page
                TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;

                barCode = CTB.Text;

                string sampleType = uniqueID + "$" + "tbType";
                TextBox ST = this.FindControl(sampleType) as TextBox;
                typeCode = ST.Text.Trim();

                string co = uniqueID + "$" + "CommentsTextBox";
                TextBox Com = this.FindControl(co) as TextBox;
                comment = Com.Text.Trim();

                // tblSampleIDTextBox
                string sampID = uniqueID + "$" + "tblSampleIDTextBox";
                TextBox SID = this.FindControl(sampID) as TextBox;
                sID = int.Parse(SID.Text.Trim());

                if (type.ToUpper().Equals("BAD"))
                    isbad = true;
                else
                    isbad = false;
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

            // check to see if a record already exists, it may if field data was entered first.... 
            // XXXX 12/29 should never update this from valdation.. 
            // XXXX create new row for each barcode and add needed detail

            try
            {
                //NEWexpWater TEST = (from t in NewRWE.NEWexpWaters
                //                    where t.tblSampleID == sID & t.Valid == true & t.MetalsBarCode == barCode
                //                    select t).FirstOrDefault();

                // see if we have any entries in the table with this sample id 
                var TEST = from t in NewRWE.NEWexpWaters
                           where t.tblSampleID == sID & t.Valid == true
                           select t;

                NewExpWaterRows = TEST.Count();
                if (NewExpWaterRows > 0)     // we have at least one matching record 
                {
                    foreach (NEWexpWater NW in TEST)
                    {
                        if (NW.MetalsBarCode == barCode)
                        {
                            haveCurrentBarcode = true;
                            break;
                        }
                        if (NW.MetalsBarCode != null)
                        {
                            haveOtherBarcode = true;
                            EsistingBarCodes.Add(NW.MetalsBarCode); // make a list of barcodes that may exist
                        }
                    }
                }
                else   // no results from query, no existing record 
                {
                    NEW = new NEWexpWater(); // create new entity as there is not one yet
                    existingRecord = false;
                }

                // here we have one or more existing records 
                if (haveCurrentBarcode)
                {
                    // not sure what to do... Update this entry or move on as this may be an error

                    NEWexpWater T = (from t in NewRWE.NEWexpWaters
                                     where t.tblSampleID == sID & t.Valid == true & t.MetalsBarCode == barCode
                                     select t).FirstOrDefault();
                    if (T != null)
                    {
                        // skip these as they are not our business to insert into a row that already exists
                        // items like kit number, etc. will be here already as a result of inserting field or nutrient data earlier
                        NEW = T; // keep the name common to this method
                        existingRecord = true; // flag for later
                    }
                    haveOtherBarcode = false; // don't process another bar code if we have 'ours' 
                }

                else if (haveOtherBarcode)   // we have at least one row with another bar code, we can copy and reuse
                {
                    string bc = EsistingBarCodes.FirstOrDefault();
                    // create a detached row that we can update and put back in as new
                    NEWexpWater N = (from t in NewRWE.NEWexpWaters
                                     where t.tblSampleID == sID & t.Valid == true & t.MetalsBarCode == bc
                                     select t).FirstOrDefault();
                    NEW = CloneNewExpWater(N);
                    existingRecord = true;  // we just want to update this record for our current bc and mark bad

                }
                else
                {
                    NEW = new NEWexpWater(); // create new entity as there is not one yet
                    existingRecord = false;
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

            // no existing record, so we are first
            if (!existingRecord)
            {
                NEW.BadDuplicate = false;
                NEW.BadSample = false;
                NEW.BenthicsComments = null;
                NEW.BugsBarCode = null;
                NEW.BugsComments = null;
                NEW.Chloride = null;
                NEW.ChlorophyllA = null;
                NEW.CreateDate = DateTime.Now;
                NEW.CreatedBy = User.Identity.Name;
                NEW.DO_MGL = null;
                NEW.DOC = null;
                NEW.DOSAT = null;
                NEW.FieldBarCode = null;
                NEW.FieldComment = null;
            }

            NEW.TypeCode = typeCode;
            NEW.MetalsComment = comment;
            NEW.MetalsBarCode = barCode;
            NEW.tblSampleID = sID; // FK to tblSample

            // get some detail from the sample table, which already exists (mostly...)
            // note: use new table, not origional
            try
            {
                //   where t.SampleID == sID & t.Valid == true
                var ts = (from t in NewRWE.Samples
                          where t.ID == sID & t.Valid == true
                          select t).FirstOrDefault(); // should be only one copy
                if (ts != null)
                {
                    // make STATION number 
                    string numS = ts.NumberSample; // looks weird and is, this is the string like 44.096 and kit # is on the left of decimal place
                    int idx = numS.IndexOf(".");
                    string numS1 = numS.Substring(0, idx);  // get chars to left of decimal point
                    if (ts.OrganizationID != null)
                    {
                        OrgID = ts.OrganizationID.Value;

                        organization O = (from o in NewRWE.organizations
                                          where o.ID == OrgID & o.Valid == true
                                          select o).FirstOrDefault();
                        NEW.OrganizationName = O.OrganizationName;
                        if (O.KitNumber != null)
                        {
                            KitNumber = O.KitNumber.Value;
                            NEW.KitNumber = KitNumber;
                        }
                    }

                    NEW.Event = numS; // string like above, 10.095

                    NEW.NutrientBarCode = null;
                    NEW.NutrientComment = null;
                    NEW.OP = null;

                    NEW.orgN = null;
                    NEW.PH = null;
                    NEW.PHEN_ALK = null;

                    NEW.SampleDate = ts.DateCollected; // this is date part only, no time and may be junk XXXX
                    if (ts.TimeCollected.Value.Year > 1970)  // likely a real value - otherwise, leave blank
                    {
                        NEW.SampleDate.Value.AddHours(ts.TimeCollected.Value.Hour); // add in pieces
                        NEW.SampleDate.Value.AddMinutes(ts.TimeCollected.Value.Minute);
                    }

                    NEW.SampleNumber = ts.SampleNumber; // this is the big string of station id + date time - build at sample entry
                    // tblSample has station id 
                    var STN = (from s in NRWDE.Stations
                               where s.ID == ts.StationID
                               select s).FirstOrDefault();

                    //   ts.StationID
                    NEW.StationName = STN.StationName;
                    NEW.RiverName = STN.River;
                    NEW.StationNumber = (short)ts.StationID;   // XXXX hope this gets working soon and we can get rid of shorts
                    NEW.Sulfate = null;

                    NEW.tblSampleID = ts.SampleID;
                    NEW.TempC = null;
                    NEW.TKN = null;
                    NEW.TOTAL_ALK = null;
                    NEW.TOTAL_HARD = null;
                    NEW.BadBlank = isbad; // record value from type passed in by caller
                    NEW.Valid = true;
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
            

            // now add in the chemistry from ICP
            // get existing record from data base and update chems and such
            // there must be a result, as this is where we got the data from in the first place... 

            try
            {
                // update final table here, as the results may have changed due to editing in validation process
                // also add metals to newExpWater table and mark as validated 
                InboundICPFinal F = (from f in NewRWE.InboundICPFinals
                                     where f.CODE == NEW.MetalsBarCode
                                     select f).FirstOrDefault();

                string workString = "";

                workString = "AL";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.AL_D = Disolved;
                NEW.AL_T = Total;
                F.AL_D = Disolved;
                F.AL_T = Total; ;

                workString = "AS";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.AS_D = Disolved;
                NEW.AS_T = Total;
                F.AS_D = Disolved;
                F.AS_T = Total;

                workString = "CA";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.CA_D = Disolved;
                NEW.CA_T = Total;
                F.CA_D = Disolved;
                F.CA_T = Total;

                workString = "CD";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.CD_D = Disolved;
                NEW.CD_T = Total;
                F.CD_D = Disolved;
                F.CD_T = Total;

                workString = "CU";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.CU_D = Disolved;
                NEW.CU_T = Total;
                F.CU_D = Disolved;
                F.CU_T = Total;

                workString = "FE";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.FE_D = Disolved;
                NEW.FE_T = Total;
                F.FE_D = Disolved;
                F.FE_T = Total;

                workString = "PB";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.PB_D = Disolved;
                NEW.PB_T = Total;
                F.PB_D = Disolved;
                F.PB_T = Total;

                workString = "MG";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.MG_D = Disolved;
                NEW.MG_T = Total;
                F.MG_D = Disolved;
                F.MG_T = Total;

                workString = "MN";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.MN_D = Disolved;
                NEW.MN_T = Total;
                F.MN_D = Disolved;
                F.MN_T = Total;

                workString = "NA";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.NA_D = Disolved;
                NEW.NA_T = Total;
                F.NA_D = Disolved;
                F.NA_T = Total;

                workString = "SE";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.SE_D = Disolved;
                NEW.SE_T = Total;
                F.SE_D = Disolved;
                F.SE_T = Disolved;

                workString = "ZN";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.ZN_D = Disolved;
                NEW.ZN_T = Total;
                F.ZN_D = Disolved;
                F.ZN_T = Total;

                workString = "K";
                getText2Values(workString, uniqueID, out  Disolved, out  Total);
                NEW.K_D = Disolved;
                NEW.K_T = Total;
                F.K_D = Disolved;
                F.K_T = Total;

                F.Comments = NEW.MetalsComment;
                F.CreatedBy = User.Identity.Name; 
                F.CreatedDate = DateTime.Now;
                F.Saved = true;
                F.Edited = true;
                F.Valid = true;


                if (haveOtherBarcode)    // we are inserting a clone
                {
                    NewRWE.NEWexpWaters.Add(NEW); // add or update record - we will overwrite the old record since this is not an EDIT but an update
                }
                if (!existingRecord)
                {
                    NewRWE.NEWexpWaters.Add(NEW); // add or update record - we will overwrite the old record since this is not an EDIT but an update
                }

                int cnt = NewRWE.SaveChanges(); // update final table as it is 'attached' we don't need to refer to it

                // count rows again
                // removed [Riverwatch].[dbo].
                DataSourceSelectArguments args = new DataSourceSelectArguments();
                SqlDataSourceNormals.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '0' and saved = 0";    // this is the working table

                // recount the number of blanks, since we just processed one of them
                System.Data.DataView result = (DataView)SqlDataSourceNormals.Select(args);
                int rows = result.Table.Rows.Count;

                if (rows < 1)    // no more records
                {
                    Response.Redirect("~/Validation/Validation.aspx"); // send user to menu page
                }
 
                lblCount.Text = string.Format("There are {0} ICP Normal records to validate", rows);
                SqlDataSourceNormals.DataBind(); // update the records so we see a new one                
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

        public void getText2Values(string workStr, string UID, out decimal Disolved, out decimal Total)
        {
            string workString = ""; 
            string uniqueID;
            string tbDName;
            string tbTName;
            TextBox tbD;
            TextBox tbT;

            workString = workStr;
            uniqueID = UID; // FormViewBlank.Controls[0].UniqueID;  // I think this is valid here it is not

            tbDName = uniqueID + "$" + workString + "_DTextBox";  // use the key value to build the name of the text box to be processed
            tbTName = uniqueID + "$" + workString + "_TTextBox";

            tbD = this.FindControl(tbDName) as TextBox;
            tbT = this.FindControl(tbTName) as TextBox;

            if (tbD == null)
            {
                string s = tbDName;
            }
            if (tbT == null)
            {
                string s = tbTName;
            }

            if (!decimal.TryParse(tbT.Text, out Total))
            {
                Total = 0;
            }

            if (!decimal.TryParse(tbD.Text, out Disolved))
            {
                Disolved = 0;
            }
        }

        // these events allow us to insert our own data before commiting to storage
        // NOTE on this page, we do not commit the data in its final form, just mark it as edited, not saved
        protected void SqlDataSourceNormals_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            int thispage = FormViewNormals.PageIndex;
            Session["THISPAGE"] = thispage;
            string uzr = "Dev User"; 
            if(User.Identity.Name.Length > 3)
            {
                uzr = User.Identity.Name;  
            }          

            e.Command.Parameters["@CreatedBy"].Value = uzr;
            e.Command.Parameters["@CreatedDate"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = true;
            e.Command.Parameters["@Edited"].Value = true;
            e.Command.Parameters["@Saved"].Value = false;
            //  [CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [Valid] = @Valid, [Edited] = @Edited, [Saved] = @Saved

        }

        protected void btnSwap_Click(object sender, CommandEventArgs ea)
        {
            string tmpStr = "";
            TextBox tbD = null;
            TextBox tbT = null;
            string btnID = "";
            string bid = "";
            string uniqueID = "";
            string CA = ea.CommandArgument.ToString();
            Button btn = sender as Button;
            btnID = btn.ID;
            int sl = btnID.Length;

            bid = btnID.Substring(3, sl - 3); // parse string to get metals type prefix

            uniqueID = FormViewNormals.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page

            string tbDName = uniqueID + "$" + bid + "_DTextBox";
            string tbTName = uniqueID + "$" + bid + "_TTextBox";

            tbD = this.FindControl(tbDName) as TextBox;
            tbT = this.FindControl(tbTName) as TextBox;
            tmpStr = tbD.Text; // save for swap
            tbD.Text = tbT.Text;
            tbT.Text = tmpStr;

            setControls(); // update color schemes
        }

        // update the inbound icp final record as edited but not saved
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
          //  SqlDataSourceNormals.Update(); // force an update
        }

        // restore page if just update
        protected void SqlDataSourceNormals_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            int thispage = (int)Session["THISPAGE"];
            FormViewNormals.PageIndex = thispage;
            FormViewNormals.DataBind();
        }

        protected void SqlDataSourceDups_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            int thispage = (int)Session["THISPAGE"];
            FormViewNormals.PageIndex = thispage;
            FormViewNormals.DataBind();
        }

        private static NEWexpWater CloneNewExpWater(NEWexpWater obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();
            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;
            NEWexpWater newObject = (NEWexpWater)dcSer.ReadObject(memoryStream);
            return newObject;
        }
    }
}
