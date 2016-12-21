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
 
// updated around line 562 to make sure a barcode that is not yet in new expwater will be created as new item and not overwrite another bar code
// thus we could have three bar codes in newEXPwater for a single sample event

namespace RWInbound2.Validation
{
    public partial class ValidateDups : System.Web.UI.Page
    {
        Dictionary<string, decimal> D2TLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> MeasurementLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> ReportingLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        RiverWatchEntities NRWDE = new RiverWatchEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            int x = 0;
            int sampID = 0;
            string sampleType = ""; // this is Value2 in data base, for now
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            string name = "";
            decimal D2Tvalue = 0;
            decimal MeasurementValue = 0;
            decimal ReportingValue = 0;
            string searchNormal = "";
            bool controlsSet;
            //dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2();
            //RiverWatchEntities NRWDE = new RiverWatchEntities();

            // count rows of not saved, valid Dups first
            // removed [Riverwatch].[dbo].
            SqlDataSourceDups.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '2' and valid = 1 and saved = 0";    // this is the working table

            System.Data.DataView result = (DataView)SqlDataSourceDups.Select(args);
            // int rowCount = result.Table.Rows.Count;

            int rowCount = result.Table.Rows.Count;
            if (rowCount == 0)
            {
                lblCount.Text = "There are NO ICP duplicate records to validate";
                pnlHelp.Visible = false; // make sure user does not see this unless requested
                return;
            }
            else
                lblCount.Text = string.Format("There are {0} ICP duplicate records to validate", rowCount);

            if (!IsPostBack)
            {

                //try
                //{
                //    using (SqlConnection conn = new SqlConnection())
                //    {
                //        conn.ng = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
                //        using (SqlCommand cmd = new SqlCommand())
                //        {
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            SqlParameter userName = cmd.Parameters.Add("@User", SqlDbType.NVarChar, 90);
                //            userName.Direction = ParameterDirection.Input;
                //            userName.Value = User.Identity.Name;            //"Bill for Now";
                //            cmd.CommandText = "[UpdateLocalTablesFromIncomingICP]"; // name of the sproc 
                //            cmd.Connection = conn;
                //            conn.Open();
                //            cmd.ExecuteNonQuery();
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

                pnlHelp.Visible = false; // make sure user does not see this unless requested
                Session["CONTROLSSET"] = false;
                pnlHelp.Visible = false; // make sure user does not see this unless requested

                // removed  [Riverwatch].[dbo].
                try
                {
                    // changed this to use tlkLimits as they seem to correspond to Barb's note. 
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = string.Format("select distinct Element, Reporting, DvsTDifference, MDL from [tlkLimits] where valid = 1");
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
                                            ReportingValue = (decimal)sdr["Reporting"];
                                            D2TLimits.Add(name, D2Tvalue);
                                            MeasurementLimits.Add(name, MeasurementValue);
                                            ReportingLimits.Add(name, ReportingValue);
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
                Session["REPORTINGLIMITS"] = ReportingLimits;

                // fetch all duplicates and put them in formview
                // removed [RiverWatch].[dbo].
                SqlDataSourceDups.SelectCommand = "SELECT* FROM [InboundICPFinal] where left( DUPLICATE, 1) = '2' and valid = 1 and saved = 0";
                //   SqlDataSourceDups.Select(args);

                // build data table with blanks so we can work with them
                result = (DataView)SqlDataSourceDups.Select(args);
                DataTable DT = result.ToTable();    // build data table of results 
                rowCount = (int)DT.Rows.Count; // get number of rows in result   
                lblCount.Text = string.Format("There are {0} ICP duplicate records to validate", rowCount);

                DataColumn DC = new DataColumn();
                DC.AllowDBNull = true;
                DC.ColumnName = "isNormalHere";
                DC.DataType = typeof(bool);
                DT.Columns.Add(DC);

                DataColumn DC1 = new DataColumn();
                DC1.AllowDBNull = true;
                DC1.ColumnName = "NormalBarCode";
                DC1.DataType = typeof(string);
                DT.Columns.Add(DC1);

                // now, loop through each row and see if there is a 'normal' sample associated with this blank
                // for now, use csampID but this may change

                for (x = 0; x < rowCount; x++)  // one pass for each sample in icp inbound
                {
                    DT.Rows[x]["isNormalHere"] = false; // set false as default
                    DT.Rows[x]["NormalBarCode"] = ""; // make sure something is here...
                    sampID = (int)DT.Rows[x]["tblSampleID"];    // get the sample id which is link to all barcodes from this sample set
                    sampleType = (string)DT.Rows[x]["DUPLICATE"];
                    searchNormal = sampleType.Substring(1, 1); // get right most char
                    searchNormal = "0" + searchNormal; // build string for related normal sample

                    // now query db for this sample to see if there is a barcode

                    string Q = (from q in NRWDE.InboundICPFinals
                                where q.tblSampleID == sampID & q.DUPLICATE == searchNormal & q.Saved == false & q.Valid == true
                                select q.CODE).FirstOrDefault();

                    //    if (Q.Count() > 0)  //  associated 'normal' sample
                    if (Q != null)  //  associated 'normal' sample
                    {
                        DT.Rows[x]["isNormalHere"] = true;  // mark as existing
                        DT.Rows[x]["NormalBarCode"] = Q;    // add normal barcode
                    }
                    else
                    {
                        DT.Rows[x]["isNormalHere"] = false;  // mark as NOT existing
                        DT.Rows[x]["NormalBarCode"] = "";    // NO normal barcode
                    }
                }

                Session["OURTABLE"] = DT;   //save current copy for later

                // now, get page we are on and get associated normal, if it exists

                int idx = FormViewDup.PageIndex;
                int id = (int)DT.Rows[idx]["tblSampleID"];
                string barcode = (string)DT.Rows[idx]["NormalBarCode"];

                // removed [Riverwatch].[dbo].
                if (barcode.Length > 4)
                {
                    // build query to get associated normal
                    string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}' and valid = 1 and saved = 0 ", barcode);
                    SqlDataSourceNormals.SelectCommand = cmmd;
                    FormViewSample.DataBind();
                    FormViewSample.Visible = true;
                }
                else
                {
                    FormViewSample.Visible = false;
                }

                // do one time from postback

                //if (Session["CONTROLSSET"] != null)
                //{
                //    controlsSet = (bool)Session["CONTROLSSET"];
                //    if (!controlsSet)
                //    {
                //        setControls();      // update color schemes
                //        Session["CONTROLSSET"] = true;
                //    }
                //}
                //else
                //{
                //    string whoknew = "";
                //}
            }
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

            uniqueID = FormViewDup.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page

            string tbDName = uniqueID + "$" + bid + "_DTextBox";
            string tbTName = uniqueID + "$" + bid + "_TTextBox";

            tbD = this.FindControl(tbDName) as TextBox;
            tbT = this.FindControl(tbTName) as TextBox;
            tmpStr = tbD.Text; // save for swap
            tbD.Text = tbT.Text;
            tbT.Text = tmpStr;

            setControls(); // update color schemes
        }

        // this is modified for Duplicates
        public void setControls()
        {
            TextBox tbDups_D;
            TextBox tbDups_T;
            TextBox tbNormals_D;
            TextBox tbNormals_T; 
            string barCode = "";
            string dupCode = "";
            decimal D2Tlimit = 0;
            //decimal MeasureLimit = 0;
            //decimal ReportLimit = 0; 
            string tbDups_DName;
            string tbDups_TName;
            string tbNormals_DName;
            string tbNormals_TName;
            string codeTextBoxName;
            string dupTextBoxName;
            decimal Total_Dups;
            decimal Dissolved_Dups = 0; ;
            decimal Total_Normals = 0;
            decimal Dissolved_Normals = 0; 
            //   string DupsUniqueID = FormViewBlank.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page
            lblNote.Text = "";
            lblNote.ForeColor = Color.White;
            int pageWeAreOn = 0;
            bool isThereANormal = false;
            string DupsUniqueID = "";
            string NormalsUniqueID = "";
            string Normalbarcode = "";

            if (Session["OURTABLE"] == null)
                return;

            // get the dictionaries of limits from session state - these do not change during session
            Dictionary<string, decimal> D2TLimits = (Dictionary<string, decimal>)Session["HighLimit"];  //  Session["HighLimit"] = HighLimit;  
            Dictionary<string, decimal> MeasurementLimits = (Dictionary<string, decimal>)Session["MEASUREMENTLIMITS"];
            Dictionary<string, decimal> ReportingLimits = (Dictionary<string, decimal>)Session["REPORTINGLIMITS"];

            // **** moved from formview data bind            
            DataTable DT = (DataTable)Session["OURTABLE"];

            pageWeAreOn = FormViewDup.PageIndex;
            isThereANormal = (bool)DT.Rows[pageWeAreOn]["isNormalHere"];

            //if (isThereANormal)
            //{
            //    lblNote.Text = "";
            //    lblNote.Visible = false;
            //    Normalbarcode = (string)DT.Rows[pageWeAreOn]["NormalBarCode"];
            //    NormalsUniqueID = FormViewSample.Controls[0].UniqueID; 
            //    // commented out 12/16
            //    if (Normalbarcode.Length > 4)
            //    {
            //        // build query to get associated normal
            //        //string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}'  and valid = 1 and saved = 0 ", Normalbarcode);
            //        ////           string cmmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] where Code = '{0}'", barcode);
            //        //SqlDataSourceNormals.SelectCommand = cmmd;
            //        //FormViewSample.DataBind();
            //        //FormViewSample.Visible = true;
            //    }
            //    else
            //    {
            //        lblNote.Text = "No Normal Sample";
            //        lblNote.Visible = true;
            //        lblNote.ForeColor = Color.Red;
            //        FormViewSample.Visible = false; // nothing to show as no normal sample
            //    }
            //}
            //else
            //{
            //    FormViewSample.Visible = false; // nothing to show as no normal sample
            //}

            if(!isThereANormal)
            {
                lblNote.Text = "No Matching Normal Sample";
                lblNote.Visible = true;
                lblNote.ForeColor = Color.Red;
                FormViewSample.Visible = false; // nothing to show as no normal sample
            }
            else
            {
                lblNote.Visible = false;
            }
            
            DupsUniqueID = FormViewDup.Controls[0].UniqueID;
            NormalsUniqueID = FormViewSample.Controls[0].UniqueID; 
            // get barcode for this page as it is unique
            codeTextBoxName = DupsUniqueID + "$" + "tbCode"; // get the BAR CODE text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;
            barCode = CTB.Text;

            // get dup - Type - two digit number for this sample
            dupTextBoxName = DupsUniqueID + "$" + "tbType"; // will have to change this name on aspx page XXXX
            TextBox DTB = this.FindControl(dupTextBoxName) as TextBox;
            dupCode = DTB.Text;

            // search datatable for this barcode and the isNormalHere 
            int count = DT.Rows.Count;

            // process each metals data row to see if it falls out of 'specs' 
            foreach (string item in D2TLimits.Keys)
            {
                tbDups_DName = DupsUniqueID + "$" + item + "_DTextBox";  // use the key value to build the name of the text box to be processed
                tbDups_TName = DupsUniqueID + "$" + item + "_TTextBox";

                if (isThereANormal)
                {
                    tbNormals_DName = NormalsUniqueID + "$" + item + "_DTextBox";  // use the key value to build the name of the text box to be processed
                    tbNormals_TName = NormalsUniqueID + "$" + item + "_TTextBox";
                    tbNormals_D = this.FindControl(tbNormals_DName) as TextBox;
                    tbNormals_T = this.FindControl(tbNormals_TName) as TextBox;
                    if (tbNormals_D == null)
                    {
                        break; // nothing to do here.. 
                    }

                    if (!decimal.TryParse(tbNormals_T.Text, out Total_Normals))
                    {
                        Total_Normals = 0;
                    }

                    if (!decimal.TryParse(tbNormals_D.Text, out Dissolved_Normals))
                    {
                        Dissolved_Normals = 0;
                    }
                }

                D2Tlimit = D2TLimits[item];

                tbDups_D = this.FindControl(tbDups_DName) as TextBox;
                tbDups_T = this.FindControl(tbDups_TName) as TextBox;

                if (!decimal.TryParse(tbDups_T.Text, out Total_Dups))
                {
                    Total_Dups = 0;
                }

                if (!decimal.TryParse(tbDups_D.Text, out Dissolved_Dups))
                {
                    Dissolved_Dups = 0;
                }

                // see if the difference between the two is greater than the D2Tlimit
                if ((Dissolved_Dups - Total_Dups) >= D2Tlimit)
                {
                    tbDups_D.BackColor = Color.PowderBlue;
                    tbDups_D.ToolTip = string.Format("Dissolved - Total is greater than limit of {0}", D2Tlimit);
                    tbDups_T.BackColor = Color.PowderBlue;
                    tbDups_T.ToolTip = string.Format("Dissolved - Total is greater than limit of {0}", D2Tlimit);
                }
                else
                {
                    tbDups_D.BackColor = Color.White;
                    tbDups_T.BackColor = Color.White;
                    tbDups_D.ToolTip = string.Format("Dissolved - Total is less than limit of {0}", D2Tlimit);
                    tbDups_T.ToolTip = string.Format("Dissolved - Total is less than limit of {0}", D2Tlimit);
                }

                // now check for samples to be within range
                if (isThereANormal)
                {
                    if (Total_Dups >= .0001m)    // make sure this is not too close to 0 as it may be 0
                    {
                        if (((Total_Normals / Total_Dups) > 1.2m) | ((Total_Normals / Total_Dups) < 0.80m))
                        {
                            tbDups_T.ForeColor = Color.Red;
                            tbDups_T.ToolTip = string.Format("Totals differ by more than 120% or less than 80%");
                        }
                        else
                        {
                            tbDups_T.ForeColor = Color.Black;
                            tbDups_T.ToolTip = string.Format("Totals are within range of 80% - 120%");
                        }
                    }

                    if (Dissolved_Dups >= .0001m)
                    {
                        if (((Dissolved_Normals / Dissolved_Dups) > 1.2m) | ((Dissolved_Normals / Dissolved_Dups) < 0.80m))
                        {

                            tbDups_D.ForeColor = Color.Red;
                            tbDups_D.ToolTip = string.Format("Dissolveds differ by more than 120% or less than 80%");
                        }
                        else
                        {
                            tbDups_D.ForeColor = Color.Black;
                            tbDups_D.ToolTip = string.Format("Dissolveds are within range of 80% - 120%");
                        }
                    }
                }
            }
        }

        // this is a typo that is easier to leave here - same as below handler
        protected void AL_DTextBox_TextChanged(object sender, EventArgs e)
        {
            setControls(); // update color schemes
        }
      
        //   user updated a value in a text box, so re-calc the values
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            setControls(); // update color schemes
        }

        // user has progressed to another page without updating 
        // We must get the associated bar code and display it in right hand table
        // ez
        protected void FormViewDup_DataBound(object sender, EventArgs e)
        {
            // XXXX undid this as we need to update only one time, when formview loads
            if (Session["OURTABLE"] == null)
                return;

            DataTable DT = (DataTable)Session["OURTABLE"];

            int idx = FormViewDup.PageIndex;
            //  int id = (int)DT.Rows[idx]["tblSampleID"];
            string barcode = (string)DT.Rows[idx]["NormalBarCode"];

            if (barcode.Length > 4)
            {
                // build query to get associated normal
                string cmmd = string.Format("SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where Code = '{0}'  and valid = 1 and saved = 0 ", barcode);
                //           string cmmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] where Code = '{0}'", barcode);
                SqlDataSourceNormals.SelectCommand = cmmd;
                FormViewSample.DataBind();
                FormViewSample.Visible = true;
            }
            else
            {
                FormViewSample.Visible = false; // nothing to show as no normal sample
            }
            setControls(); // update color schemes
        }

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

        protected void FormViewDup_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        // user has edited (or just accepts) the DUP and now we will save it to table
        // must update page as there will one fewer blanks to process
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            updateDup("UPDATE");

        }

        // user has chosen to mark as bad blank and now we will save it to XXXX table
        // must update page as there will one fewer blanks to process
        protected void btnBadDup_Click(object sender, EventArgs e)
        {
            updateDup("BAD");
        }

        public void updateDup(string type)
        {
            RiverWatchEntities NewRWE = new RiverWatchEntities(); // new database RiverWatch 
            NEWexpWater NEW = null;
            //dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2(); // get access to old db for details, this is temp. XXXX
            bool existingRecord = false;
            decimal Total;
            decimal Disolved;
            bool isbad = false;

            string uniqueID = FormViewDup.Controls[0].UniqueID;

            // XXXX another WTF moment, why is the uniqueid different here, just because we pressed a button
            // note that the data is correct when we have the correct string
            uniqueID = uniqueID.Replace("$ctl00", "");
            // scrape text box strings, which will never be null, but can be zero length
            string codeTextBoxName = uniqueID + "$" + "tbCode"; // get the text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;

            string barCode = CTB.Text;

            string sampleType = uniqueID + "$" + "tbType";  // bound to 'duplicate' 
            TextBox ST = this.FindControl(sampleType) as TextBox;
            string typeCode = ST.Text.Trim();

            string co = uniqueID + "$" + "CommentsTextBox";
            TextBox Com = this.FindControl(co) as TextBox;
            string comment = Com.Text.Trim();

            // tblSampleIDTextBox
            string sampID = uniqueID + "$" + "tblSampleIDTextBox";
            TextBox SID = this.FindControl(sampID) as TextBox;
            int sID = int.Parse(SID.Text.Trim());

            if (type.ToUpper().Equals("BAD"))
                isbad = true;
            else
                isbad = false;

            // XXXX check to see if a record already exists, it may if field data was entered first.... 
            // will create a method for this, I think, so we can reuse
            // note new master summary table

            try
            {
                //NEWexpWater TEST = (from t in NewRWE.NEWexpWaters
                //                    where t.tblSampleID == sID & t.Valid == true & t.MetalsBarCode == barCode
                //                    select t).FirstOrDefault();

                NEWexpWater TEST = (from t in NewRWE.NEWexpWaters
                                    where t.Valid == true & t.MetalsBarCode == barCode
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

                NEW.TypeCode = typeCode;
                NEW.MetalsComment = comment;
                NEW.MetalsBarCode = barCode;
                NEW.tblSampleID = sID; // FK to tblSample

                // get some detail from the sample table, which already exists (mostly...)
                // note: use new table, not origional
                // XXXX this may not work well as we have changed samples to use ID as identity and I don't think SampleID will change
                try
                {
                    Sample ts = (from t in NewRWE.Samples
                                    where t.SampleID == sID & t.Valid == true
                                    select t).FirstOrDefault(); // should be only one copy
                    if (ts != null)
                    {
                        // make kit number 
                        string numS = ts.NumberSample; // looks weird and is, this is the string like 44.096 and kit # is on the right of decimal place
                        int idx = numS.IndexOf(".");
                        string numS1 = numS.Substring(0, idx);  // get chars to right of decimal point

                        NEW.KitNumber = short.Parse(numS1);
                        NEW.Event = numS; // string like above, 10.095

                        NEW.NutrientBarCode = null;
                        NEW.NutrientComment = null;
                        NEW.OP = null;

                        NEW.OrganizationName = "";
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
            }

            // now add in the chemistry from ICP
            // XXXX is this easier and or more accurate than using Datatable ?

            // get existing record from data base and update chems and such
            // there must be a result, as this is where we got the data from in the first place... 

            try
            {
                InboundICPFinal F = (from f in NewRWE.InboundICPFinals
                                     where f.CODE == barCode    // changed 7/8/16
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

                NewRWE.NEWexpWaters.Add(NEW); // add or update record - we will overwrite the old record since this is not an EDIT but an update

                int cnt = NewRWE.SaveChanges(); // update final table as it is 'attached' we don't need to refer to it

                // count rows again
                // removed [Riverwatch].[dbo].
                DataSourceSelectArguments args = new DataSourceSelectArguments();
                SqlDataSourceDups.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '1' and saved = 0";    // this is the working table

                // recount the number of blanks, since we just processed one of them
                System.Data.DataView result = (DataView)SqlDataSourceDups.Select(args);
                int rows = result.Table.Rows.Count;
                if (rows < 1)    // no more records
                {
                    Response.Redirect("~/Validation/Validation.aspx"); // send user to menu page
                }

                lblCount.Text = string.Format("There are {0} ICP duplicate records to validate", rows);
                FormViewDup.DataBind(); // update the records so we see a new one                
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

        // here we change parameters to our own and save page number so we return to same page
        protected void SqlDataSourceNormals_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            int thispage = FormViewDup.PageIndex;
            Session["THISPAGE"] = thispage;
            string uzr = User.Identity.Name;
            if ((uzr == null) | (uzr.Length < 3))
                uzr = "Dev User";
            e.Command.Parameters["@CreatedBy"].Value = uzr;
            e.Command.Parameters["@CreatedDate"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = true;
            e.Command.Parameters["@Edited"].Value = true;
            e.Command.Parameters["@Saved"].Value = false;        
         }

        // return to working page
        protected void SqlDataSourceNormals_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            int thispage = (int)Session["THISPAGE"];
            FormViewDup.PageIndex = thispage;
           // FormViewDup.DataBind();
            // this is where the data should be updated
            if (Session["OURTABLE"] == null)
                return;

            int idx = FormViewDup.PageIndex;
            DataTable DT = (DataTable)Session["OURTABLE"];
            string barcode = (string)DT.Rows[idx]["NormalBarCode"];

            // removed [Riverwatch].[dbo].
            if (barcode.Length > 4)
            {
                // build query to get associated normal
                string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}'  and valid = 1 and saved = 0 ", barcode);
                //           string cmmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] where Code = '{0}'", barcode);
                SqlDataSourceNormals.SelectCommand = cmmd;
                FormViewSample.DataBind();
                FormViewSample.Visible = true;
            }
            else
            {
                FormViewSample.Visible = false; // nothing to show as no normal sample
            }
            setControls(); 
        }

        protected void btnBadNormal_Click(object sender, EventArgs e)
        {
            updateDup("BAD");
        }


    }
}