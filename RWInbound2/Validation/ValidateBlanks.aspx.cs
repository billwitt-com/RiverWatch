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

namespace RWInbound2.Validation
{
    public partial class ValidateBlanks : System.Web.UI.Page
    {
        Dictionary<string, decimal> D2TLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> MeasurementLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        RiverWatchEntities NRWDE = new RiverWatchEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            // no longer used, but being careful here... 
            //int x = 0;
            //int sampID = 0;
            //string sampleType = ""; // this is Value2 in data base, for now
            //string searchDup = "";
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            string name = "";
            decimal D2Tvalue = 0;
            decimal MeasurementValue = 0;
            DataTable DT = null; 

            bool controlsSet;
            bool allowed = false;

            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx"); 

            // count rows of not saved, valid Blanks first
            // removed [Riverwatch].[dbo].
            SqlDataSourceBlanks.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '1' and valid = 1 and saved = 0";    // this is the working table

            System.Data.DataView result = (DataView)SqlDataSourceBlanks.Select(args);
           // int rowCount = result.Table.Rows.Count;

            int rowCount = result.Table.Rows.Count; 
            if(rowCount == 0)
            {
                lblCount.Text = "There are NO ICP blank records to validate";
                pnlHelp.Visible = false; // make sure user does not see this unless requested
                return; 
            }
            else
                lblCount.Text = string.Format("There are {0} ICP blank records to validate", rowCount);
            
           // Session["OURTABLE"] = DT; // save for later use        

            // do this one time
            if (!IsPostBack)
            {
                // XXXX no longer needed until production
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
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
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

                // now, get page we are on and get associated normal and dup if they exist
                DT = buildDatatable(); // make a fresh copy

                int idx = FormViewBlank.PageIndex;
                int id = (int)DT.Rows[idx]["tblSampleID"];
                string barcode = (string)DT.Rows[idx]["NormalBarCode"];

                // removed [Riverwatch].[dbo].
                if (barcode.Length > 2)
                {
                    // build query to get associated normal
                    string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}' ", barcode);
                    SqlDataSourceNormals.SelectCommand = cmmd;
                    FormViewNormals.DataBind();
                    FormViewNormals.Visible = true;
                }
                else
                {
                    FormViewNormals.Visible = false;
                }

                // do the same for duplicate

                barcode = (string)DT.Rows[idx]["DuplicateBarCode"];

                // removed [Riverwatch].[dbo].
                if (barcode.Length > 2)
                {
                    // build query to get associated normal
                    string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}'", barcode);
                    SqlDataSourceDups.SelectCommand = cmmd;
                    FormViewDuplicate.DataBind();
                    FormViewDuplicate.Visible = true;
                }
                else
                {
                    FormViewDuplicate.Visible = false;  // hide since empty
                }

                // do one time from postback

                if (Session["CONTROLSSET"] != null)
                {
                    controlsSet = (bool)Session["CONTROLSSET"];
                    if (!controlsSet)
                    {
                        setControls();      // update color schemes
                        Session["CONTROLSSET"] = true;
                    }
                }
                else
                {
                    string whoknew = "";
                }
            }

       //     setControls(); // update color schemes
        }

        public DataTable buildDatatable()
        {
            int x = 0;
            int sampID = 0;
            string sampleType = ""; // this is Value2 in data base, for now
            string searchDup = "";
            int rowCount = 0; 
            DataSourceSelectArguments args = new DataSourceSelectArguments();

            // removed [Riverwatch].[dbo].
            SqlDataSourceBlanks.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '1' and valid = 1 and saved = 0";    // this is the working table
            System.Data.DataView r = (DataView)SqlDataSourceBlanks.Select(args);
            DataTable DT = r.Table;

            DataColumn DC = new DataColumn();
            DC.AllowDBNull = true;
            DC.ColumnName = "isNormalHere";
            DC.DataType = typeof(bool);
            DT.Columns.Add(DC);

            DataColumn DC12 = new DataColumn();
            DC12.AllowDBNull = true;
            DC12.ColumnName = "isDuplicateHere";
            DC12.DataType = typeof(bool);
            DT.Columns.Add(DC12);

            DataColumn DC1 = new DataColumn();
            DC1.AllowDBNull = true;
            DC1.ColumnName = "NormalBarCode";
            DC1.DataType = typeof(string);
            DT.Columns.Add(DC1);

            DataColumn DC2 = new DataColumn();
            DC2.AllowDBNull = true;
            DC2.ColumnName = "DuplicateBarCode";
            DC2.DataType = typeof(string);
            DT.Columns.Add(DC2);

            rowCount = DT.Rows.Count; 

            // now, loop through each row and see if there is a 'normal' sample associated with this blank
            // for now, use csampID but this may change

            for (x = 0; x < rowCount; x++)  // one pass for each sample in icp inbound
            {
                DT.Rows[x]["isNormalHere"] = false; // set false as default 
                DT.Rows[x]["isDuplicateHere"] = false;
                DT.Rows[x]["NormalBarCode"] = ""; // make sure something is here...
                DT.Rows[x]["DuplicateBarCode"] = "";

                sampID = (int)DT.Rows[x]["tblSampleID"];    // get the sample id which is link to all barcodes from this sample set
                sampleType = (string)DT.Rows[x]["DUPLICATE"];
                searchDup = sampleType.Substring(1, 1); // get right most char
                searchDup = "0" + searchDup; // build string for related normal sample

                // now query db for this sample to see if there is a barcode

                string Q = (from q in NRWDE.InboundICPFinals
                            where q.tblSampleID == sampID & q.DUPLICATE == searchDup & q.Saved == false & q.Valid == true
                            select q.CODE).FirstOrDefault();

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

                // query for related duplicate sample so we can display it, or not... 

                searchDup = sampleType.Substring(1, 1); // get right most char
                searchDup = "2" + searchDup; // build string for related DUPLICATE sample

                // now query db for this sample to see if there is a barcode

                string QQ = (from q in NRWDE.InboundICPFinals
                             where q.tblSampleID == sampID & q.DUPLICATE == searchDup & q.Saved == false & q.Valid == true
                             select q.CODE).FirstOrDefault();

                if (QQ != null)  //  associated 'duplicate' sample
                {
                    DT.Rows[x]["DuplicateBarCode"] = QQ;    // add duplicate sample barcode
                    DT.Rows[x]["isDuplicateHere"] = true;  // mark as existing
                }
                else
                {
                    DT.Rows[x]["DuplicateBarCode"] = "";    // NO duplicate barcode
                    DT.Rows[x]["isDuplicateHere"] = false;
                }
            }

            // not sure we need to save... 
            Session["OURTABLE"] = DT;   //save current copy for later
            return DT; 
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
            string dupTextBoxName;
            decimal Total;
            decimal Disolved;
            lblNote.Text = "";
            lblNote.ForeColor = Color.White;
            int pageWeAreOn = 0;

            // **** moved from dups databind as this code was not called:
            //if (Session["OURTABLE"] == null)
            //{
            //    return;
            //}
            DataTable DT = buildDatatable();  //(DataTable)Session["OURTABLE"];

            //// FormView locFV = sender as FormView;
            //int idx = FormViewBlank.PageIndex;
            //int id = (int)DT.Rows[idx]["tblSampleID"];
            //string barcode = (string)DT.Rows[idx]["NormalBarCode"];

            //if (barcode.Length > 4)
            //{
            //    // build query to get associated normal
            //    string cmmd = string.Format("SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where Code = '{0}'", barcode);
            //    SqlDataSourceNormals.SelectCommand = cmmd;
            //    FormViewNormals.DataBind();
            //    FormViewNormals.Visible = true;
            //}
            //else
            //{
            //    FormViewNormals.Visible = false; // nothing to show as no normal sample
            //}

            // now do for duplicate bar code
            //barcode = (string)DT.Rows[idx]["DuplicateBarCode"];
            //if (barcode.Length > 4)
            //{
            //    // build query to get associated normal
            //    string cmmd = string.Format("SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where Code = '{0}'", barcode);
            //    SqlDataSourceDups.SelectCommand = cmmd;

            //    FormViewDuplicate.DataBind();
            //    FormViewDuplicate.Visible = true;
            //}
            //else
            //{
            //    FormViewDuplicate.Visible = false; // nothing to show as no Dup sample
            //}

            // **** end of transplanted code


            // get the dictionaries of limits from session state - these do not change during session
            if (Session["HighLimit"] == null)
                Response.Redirect("timedout.axpx"); 

            Dictionary<string, decimal> D2TLimits = (Dictionary<string, decimal>) Session["HighLimit"];  //  Session["HighLimit"] = HighLimit;  
            Dictionary<string, decimal> MeasurementLimits = (Dictionary<string, decimal>)Session["MEASUREMENTLIMITS"];

            // get table we built earlier where we found no normal condition
          //  DataTable DT = (DataTable)Session["OURTABLE"];

            string uniqueID = FormViewBlank.Controls[0].UniqueID;
 //           FormViewBlank.DataBind(); // try this, to see if it helps. 
            
            // get barcode for this page as it is unique
            codeTextBoxName = uniqueID + "$" + "tbBlankCode"; // get the text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;
            if(CTB == null)
            {
                // change DupsUniqueID
                uniqueID = uniqueID.Replace("$ctl00", "");
                codeTextBoxName = uniqueID + "$" + "tbBlankCode"; // get the text box off the page
                CTB = this.FindControl(codeTextBoxName) as TextBox;
            }

            if (CTB == null)
            {
                // wtf
                string wtf = uniqueID;
                Response.Redirect("timedout.axpx"); 
            }

            barCode = CTB.Text;

            // get dup - Type - two digit number for this sample
            dupTextBoxName = uniqueID + "$" + "tbBlankType"; // will have to change this name on aspx page XXXX
            TextBox DTB = this.FindControl(dupTextBoxName) as TextBox;
            dupCode = DTB.Text;
            if(DTB == null)
            {
                return;
            }
            if(DTB.Text.Length < 1)
            {
                return; 
            }


            // search datatable for this barcode and the isNormalHere 
            int count = DT.Rows.Count;

            pageWeAreOn  = FormViewBlank.PageIndex;

            bool isThereDup = (bool)DT.Rows[pageWeAreOn]["isDuplicateHere"];
            if (isThereDup)
            {
                lblNote.Text = "";
                lblNote.Visible = false;
            }
            else
            {
                lblNote.Text = "No Dup Sample";
                lblNote.Visible = true;
                lblNote.ForeColor = Color.Red;
            }
            bool isThereANormal = (bool)DT.Rows[pageWeAreOn]["isNormalHere"];
            if (isThereANormal)
            {
                lblNote.Text = "";
                lblNote.Visible = false;                  
            }
            else
            {
                lblNote.Text = "No Normal Sample";
                lblNote.Visible = true;
                lblNote.ForeColor = Color.Red; 
            }            

            // process each metals data row to see if it falls out of 'specs' 
            foreach (string item in D2TLimits.Keys)
            {
                tbDName = uniqueID + "$" + item + "_DTextBox";  // use the key value to build the name of the text box to be processed
                tbTName = uniqueID + "$" + item + "_TTextBox";
                D2Tlimit =  D2TLimits[item];

                tbD = this.FindControl(tbDName) as TextBox;
                tbT = this.FindControl(tbTName) as TextBox;

                if (!decimal.TryParse(tbT.Text, out Total))
                {
                    Total = 0;
                }

                if (!decimal.TryParse(tbD.Text, out Disolved))
                {
                    Disolved = 0;
                }

                // see if the difference between the two is greater than 2 times the D2Tlimit
                if ((Disolved - Total) >= D2Tlimit)
                {
                    tbD.BackColor = Color.PowderBlue;
                    tbD.ToolTip = string.Format("Disolved - Total is greater than limit of {0}", D2Tlimit); 
                    tbT.BackColor = Color.PowderBlue;
                    tbT.ToolTip = string.Format("Disolved - Total is greater than limit of {0}", D2Tlimit); 
                }
                else
                {
                    tbD.BackColor = Color.White;
                    tbT.BackColor = Color.White;
                    tbD.ToolTip = string.Format("Disolved - Total is less than limit of {0}", D2Tlimit); 
                    tbT.ToolTip = string.Format("Disolved - Total is less than limit of {0}", D2Tlimit); 
                }

                // now see if this is a blank, then test for out of range

                if (dupCode.Substring(0, 1) == "1")   // we have a blank
                {
                    // use item from this loop to find limit for this comparison
                    MeasureLimit = MeasurementLimits[item];
                    tbT.ToolTip = "";
                    tbD.ToolTip = "";

                    if (Total > (MeasureLimit))
                    {
                        tbT.ForeColor = Color.Black;
                        tbT.ToolTip += string.Format(" Total is greater than MDL of {0}", MeasureLimit); // this is good
                    }
                    else
                    {
                        tbT.ForeColor = Color.Red;
                        tbT.ToolTip += string.Format(" Total is under MDL of {0}", MeasureLimit); 
                    }
                    if (Disolved > (MeasureLimit))
                    {
                        tbD.ForeColor = Color.Black;
                        tbD.ToolTip += string.Format(" Dissolved is greater than MDL of {0}", MeasureLimit); 
                    }
                    else
                    {
                        tbD.ForeColor = Color.Red; 
                        tbD.ToolTip += string.Format(" Dissolved is under MDL of {0}", MeasureLimit); 
                    }
                }
            }
        }

        // user updated a value in a text box, so re-calc the values
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            // now, we must record these changes in the data base to persist them... 
            // or in the data table
            TextBox TB = sender as TextBox; 
            int idx = FormViewBlank.Row.ItemIndex;
            int pageWeAreOn = FormViewBlank.PageIndex;  // this is index into datatable
            string tbID = TB.ID;
            int sl = tbID.IndexOf("_"); 

            string bid = tbID.Substring(0, sl + 2 ); // parse string to get metals type prefix
     
            setControls(); // update color schemes
        }

        // user has progressed to another page without updating 
        // We must get the associated bar code and display it in right hand tables
        // ez DT.Rows[x]["DuplicateBarCode"] = ""; 
        
        protected void FormViewBlank_DataBound(object sender, EventArgs e) 
        {
            setControls();            
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
            CommitBlank("UPDATE");
        }

        // user has chosen to mark as bad blank and now we will save it to XXXX table
        // must update page as there will one fewer blanks to process
        protected void btnBadBlank_Click(object sender, EventArgs e)
        {
            CommitBlank("BAD");
        }

        // type:
        // EDIT user changed one value in text box but we will not update record until button is pressed, then scrape screen
        // table inboundICPFinal - which will hold all edits until the record is 'saved' to the new watersample table
        // this code is NOT correct in that it does not update record but sends it to new water table
        // must update the final table also. 

        public void CommitBlank(string type)
        {
            RiverWatchEntities NewRWE = new RiverWatchEntities(); // new database RiverWatch 
            NEWexpWater NEW = null;
            //dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2(); // get access to old db for details, this is temp. XXXX
            bool existingRecord = false;
            decimal Total;
            decimal Disolved;
            bool isbad = false;
            int OrgID = 0;
            int KitNumber = 0;


            string uniqueID = FormViewBlank.Controls[0].UniqueID;

            // XXXX another WTF moment, why is the uniqueid different here, just because we pressed a button
            // note that the data is correct whey we have the correct string
            uniqueID = uniqueID.Replace("$ctl00", "");
            // scrape text box strings, which will never be null, but can be zero length
            string codeTextBoxName = uniqueID + "$" + "tbBlankCode"; // get the text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;

            string barCode = CTB.Text;

            string sampleType = uniqueID + "$" + "tbBlankType";
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

                // changed query to use barcode 
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
            }

            // now add in the chemistry from ICP
            // XXXX is this easier and or more accurate than using Datatable ?
            
            // get existing record from data base and update chems and such
            // there must be a result, as this is where we got the data from in the first place... 

            try
            {
                InboundICPFinal F = (from f in NewRWE.InboundICPFinals
                                     where f.CODE == barCode    // changed to use local bar code
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


                F.Comments = comment; 
                F.CreatedBy = User.Identity.Name;
                F.CreatedDate = DateTime.Now;
                F.Saved = true;
                F.Edited = true;
                F.Valid = true;

                NewRWE.NEWexpWaters.Add(NEW); // add or update record - we will overwrite the old record since this is not an EDIT but an update

                int cnt =  NewRWE.SaveChanges(); // update final table as it is 'attached' we don't need to refer to it
               
                // count rows again
                // removed [Riverwatch].[dbo].
                DataSourceSelectArguments args = new DataSourceSelectArguments();
                SqlDataSourceBlanks.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '1' and saved = 0";    // this is the working table

                // recount the number of blanks, since we just processed one of them
                System.Data.DataView result = (DataView)SqlDataSourceBlanks.Select(args);
                int rows = result.Table.Rows.Count; 
                if(rows < 1)    // no more records
                {
                    Response.Redirect("~/Validation/Validation.aspx"); // send user to menu page
                }
 
                lblCount.Text = string.Format("There are {0} ICP blank records to validate", rows);
                FormViewBlank.DataBind(); // update the records so we see a new one                
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

        public void getText2Values(string workStr, string UID,  out decimal Disolved, out decimal Total)
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
            
            if(tbD == null)
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
        protected void SqlDataSourceDups_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            int thispage = FormViewBlank.PageIndex;
            Session["THISPAGE"] = thispage; 
            string uzr = User.Identity.Name;
            if ((uzr == null) | (uzr.Length < 3))
                uzr = "Dev User"; 
            e.Command.Parameters["@CreatedBy"].Value = uzr; 
            e.Command.Parameters["@CreatedDate"].Value = DateTime.Now;
            e.Command.Parameters["@Valid"].Value = true;
            e.Command.Parameters["@Edited"].Value = true;
            e.Command.Parameters["@Saved"].Value = false; 
          //  [CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [Valid] = @Valid, [Edited] = @Edited, [Saved] = @Saved
            
        }
      
        protected void SqlDataSourceNormals_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            int thispage = FormViewBlank.PageIndex;
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

        protected void SqlDataSourceNormals_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
          //  updateForms(); // XXXX I don't think we want to update here, since they have not changed... 
            setControls(); 

            //int thispage = (int)Session["THISPAGE"];
            //FormViewBlank.PageIndex = thispage;
            //if (Session["OURTABLE"] == null)
            //{
            //    return;
            //}
            //DataTable DT = (DataTable)Session["OURTABLE"];

            //// FormView locFV = sender as FormView;
            //int idx = FormViewBlank.PageIndex;
            //int id = (int)DT.Rows[idx]["tblSampleID"];
            //string barcode = (string)DT.Rows[idx]["NormalBarCode"];

            //barcode = (string)DT.Rows[idx]["DuplicateBarCode"];
            //if (barcode.Length > 4)
            //{
            //    // build query to get associated normal
            //    string cmmd = string.Format("SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where Code = '{0}'", barcode);
            //    SqlDataSourceDups.SelectCommand = cmmd;

            //    FormViewDuplicate.DataBind();
            //    FormViewDuplicate.Visible = true;
            //}
            //else
            //{
            //    FormViewDuplicate.Visible = false; // nothing to show as no Dup sample
            //}
            // try this:


           // FormViewBlank.DataBind();
            //FormViewDuplicate.DataBind();
            //FormViewNormals.DataBind();
        }
        // XXXX moved update back here
        protected void SqlDataSourceDups_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
        //    updateForms(); 
           // int thispage = (int)Session["THISPAGE"];
            setControls();             
        }

        // added this to update both forms on page change or updating individual forms...
        // XXXX
        public void updateForms()
        {
            //if (Session["THISPAGE"] != null)
            //{
            //    int thispage = (int)Session["THISPAGE"];
            //    FormViewBlank.PageIndex = thispage;
            //}


            try
            {
                DataTable DT = buildDatatable();  // (DataTable)Session["OURTABLE"];

                int idx = FormViewBlank.PageIndex;
                int id = (int)DT.Rows[idx]["tblSampleID"];
                string barcode = (string)DT.Rows[idx]["NormalBarCode"];

                // removed [Riverwatch].[dbo].
                barcode = (string)DT.Rows[idx]["DuplicateBarCode"];
                if (barcode.Length > 2)
                {
                    // build query to get associated normal
                    string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}'", barcode);
                    SqlDataSourceDups.SelectCommand = cmmd;

                    FormViewDuplicate.DataBind();
                    FormViewDuplicate.Visible = true;
                }
                else
                {
                    FormViewDuplicate.Visible = false; // nothing to show as no Dup sample
                }

                barcode = (string)DT.Rows[idx]["NormalBarCode"];    // get barcode for normal sample

                if (barcode.Length > 2)
                {
                    // build query to get associated normal
                    // removed [Riverwatch].[dbo].
                    string cmmd = string.Format("SELECT * FROM [InboundICPFinal] where Code = '{0}'", barcode);
                    SqlDataSourceNormals.SelectCommand = cmmd;
                    FormViewNormals.DataBind();
                    FormViewNormals.Visible = true;
                }
                else
                {
                    FormViewNormals.Visible = false; // nothing to show as no normal sample
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; // GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from tblOrganization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        List<string> customers = new List<string>();

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
               
                string nam = "WebMethod";               
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "WebMethod searchOrgs Failed", ex.StackTrace.ToString(), nam, "");
                return null; 
            }     
        }

        protected void FormViewBlank_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            updateForms();
            setControls(); 
        }

        protected void FormViewBlank_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {
            string pagechanged = "pc";
            //updateForms();
            //setControls();

        }

        protected void FormViewBlank_PageIndexChanged(object sender, EventArgs e)
        {
            updateForms();
            setControls();
        }

        protected void FormViewDuplicate_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            updateForms();
            setControls();
        }

        protected void FormViewNormals_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            updateForms();
            setControls();
        }


    }
}
