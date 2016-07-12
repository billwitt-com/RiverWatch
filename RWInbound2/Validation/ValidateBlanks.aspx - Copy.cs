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
    public partial class ValidateBlanks : System.Web.UI.Page
    {
        Dictionary<string, decimal> D2TLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> MeasurementLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
       

        protected void Page_Load(object sender, EventArgs e)
        {
            int x = 0;
            int sampID = 0;
            string sampleType = ""; // this is Duplicate in data base, for now
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            string name = "";
            decimal D2Tvalue = 0;
            decimal MeasurementValue = 0;
            string searchDup = "";
            bool controlsSet; 
            dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2();
            
            if (!IsPostBack)
            {
                Session["CONTROLSSET"] = false; 
                // fill in limits values
                pnlHelp.Visible = false; // make sure user does not see this unless requested

                try
                {
                    // changed this to use tlkLimits as they seem to correspond to Barb's note. 
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchWaterDEV"].ConnectionString;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = string.Format("select distinct Element, DvsTDifference, MDL from  [dbRiverwatchWaterData].[dbo].[tlkLimits]");
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
                    string msg = ex.Message;    // XXXX need to build an error log file and logging code               
                }

                Session["D2TLimits"] = D2TLimits;   // SAVE
                Session["MEASUREMENTLIMITS"] = MeasurementLimits;
              
            }

           

            // fetch all blanks and put them in formview
            SqlDataSourceBlanks.SelectCommand = "SELECT* FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] where left( DUPLICATE, 1) = '1'";
         //   SqlDataSourceBlanks.Select(args);
      
            // build data table with blanks so we can work with them
            System.Data.DataView result = (DataView)SqlDataSourceBlanks.Select(args);
            DataTable DT = result.ToTable();    // build data table of results 
            int rowCount = (int)DT.Rows.Count; // get number of rows in result   
            lblCount.Text = string.Format("There are {0} ICP blank records to validate", rowCount);

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
                searchDup = sampleType.Substring(1, 1); // get right most char
                searchDup = "0" + searchDup; // build string for related normal sample

                // now query db for this sample to see if there is a barcode

                string Q = (from q in RWDE.tblInboundICPs
                            where q.tblSampleID == sampID & q.DUPLICATE == searchDup
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
            // not sure we need to save... 
            Session["OURTABLE"] = DT;   //save current copy for later

            // now, get page we are on and get associated normal, if it exists

            int idx = FormViewBlank.PageIndex;
            int id = (int)DT.Rows[idx]["tblSampleID"];
            string barcode = (string)DT.Rows[idx]["NormalBarCode"];

            if (barcode.Length > 4)
            {
                // build query to get associated normal
                string cmmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] where Code = '{0}'" , barcode);
                SqlDataSourceSamples.SelectCommand = cmmd; 
                FormViewSample.DataBind();
                FormViewSample.Visible = true; 
            }
            else
            {
                FormViewSample.Visible = false; 
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
       
        // this is swap button XXXX
        protected void Button1_Click(object sender, CommandEventArgs ea)  
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

            bid = btnID.Substring(3, sl-3); // parse string to get metals type prefix

            uniqueID = FormViewBlank.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page

            string tbDName = uniqueID + "$" + bid + "_DTextBox";
            string tbTName = uniqueID + "$" + bid + "_TTextBox";

            tbD = this.FindControl(tbDName) as TextBox;
            tbT = this.FindControl(tbTName) as TextBox;
            tmpStr = tbD.Text; // save for swap
            tbD.Text = tbT.Text;
            tbT.Text = tmpStr;           
            
            setControls(); // update color schemes
            
        }

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
         //   string uniqueID = FormViewBlank.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page
            lblNote.Text = "";
            lblNote.ForeColor = Color.White;
            int pageWeAreOn = 0;

            // get the dictionaries of limits from session state - these do not change during session
            Dictionary<string, decimal> D2TLimits = (Dictionary<string, decimal>) Session["D2TLimits"];  //  Session["D2TLimits"] = D2TLimits;  
            Dictionary<string, decimal> MeasurementLimits = (Dictionary<string, decimal>)Session["MEASUREMENTLIMITS"];

            // get table we built earlier where we found no normal condition
            DataTable DT = (DataTable)Session["OURTABLE"];

            string uniqueID = FormViewBlank.Controls[0].UniqueID;
            // get barcode for this page as it is unique
            codeTextBoxName = uniqueID + "$" + "tbCode"; // get the text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;
            barCode = CTB.Text;

            // get dup - Type - two digit number for this sample
            dupTextBoxName = uniqueID + "$" + "TextBox1"; // will have to change this name on aspx page XXXX
            TextBox DTB = this.FindControl(dupTextBoxName) as TextBox;
            dupCode = DTB.Text;

            // search datatable for this barcode and the isNormalHere 
            int count = DT.Rows.Count;

            pageWeAreOn  = FormViewBlank.PageIndex;
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

                    if (Total > (MeasureLimit))
                    {
                        tbT.ForeColor = Color.Red;
                        tbT.ToolTip = string.Format("Total is greater than limit of {0}", MeasureLimit); 
                    }
                    else
                    {
                        tbT.ForeColor = Color.Black;
                        tbT.ToolTip = string.Format("Total is under limit of {0}", MeasureLimit); 
                    }
                    if (Disolved > (MeasureLimit))
                    {
                        tbD.ForeColor = Color.Red;
                        tbD.ToolTip = string.Format("Dissolved is greater than limit of {0}", MeasureLimit); 
                    }
                    else
                    {
                        tbD.ForeColor = Color.Black;
                        tbD.ToolTip = string.Format("Dissolved is under limit of {0}", MeasureLimit); 
                    }
                }
            }
        }

       
         // this does not see useful here, as page data has not yet changed so data is stale
        protected void FormViewBlank_PageIndexChanged(object sender, EventArgs e)
        {
                
        }

       

        // user updated a value in a text box, so re-calc the values
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            setControls(); // update color schemes
        }

        // user has progressed to another page without updating 
        // We must get the associated bar code and display it in right hand table
        // ez
        protected void FormViewBlank_DataBound(object sender, EventArgs e)
        {
            DataTable DT = (DataTable)Session["OURTABLE"];

            int idx = FormViewBlank.PageIndex;
            int id = (int)DT.Rows[idx]["tblSampleID"];
            string barcode = (string)DT.Rows[idx]["NormalBarCode"];

            if (barcode.Length > 4)
            {
                // build query to get associated normal
                string cmmd = string.Format("SELECT * FROM [dbRiverwatchWaterData].[dbo].[tblInboundICP] where Code = '{0}'", barcode);
                SqlDataSourceSamples.SelectCommand = cmmd;
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

        protected void FormViewBlank_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        // user has edited (or just accepts) the blank and now we will save it to table
        // must update page as there will one fewer blanks to process
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            updateBlank("UPDATE");

        }

        // user has chosen to mark as bad blank and now we will save it to XXXX table
        // must update page as there will one fewer blanks to process
        protected void btnBadBlank_Click(object sender, EventArgs e)
        {
            updateBlank("BAD");
        }


        public void updateBlank(string type)
        {
            NewRiverwatchEntities NewRWE = new NewRiverwatchEntities(); // new database RiverWatch 
            NEWexpWater NEW;
            dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2(); // get access to old db for details, this is temp. XXXX
            bool existingRecord = false;

            string uniqueID = FormViewBlank.Controls[0].UniqueID;

            // scrape text box strings, which will never be null, but can be zero length
            string codeTextBoxName = uniqueID + "$" + "tbCode"; // get the text box off the page
            TextBox CTB = this.FindControl(codeTextBoxName) as TextBox;
            string barCode = CTB.Text;

            string sampleType = uniqueID + "$" + "TextBox1";
            TextBox ST = this.FindControl(sampleType) as TextBox;
            string typeCode = ST.Text.Trim();

            string co = uniqueID + "$" + "CommentsTextBox";
            TextBox Com = this.FindControl(co) as TextBox;
            string comment = Com.Text.Trim();

            // tblSampleIDTextBox
            string sampID = uniqueID + "$" + "tblSampleIDTextBox";
            TextBox SID = this.FindControl(sampID) as TextBox;
            int sID = int.Parse(SID.Text.Trim());

            // XXXX check to see if a record already exists, it may if field data was entered first.... 
            // will create a method for this, I think so we can reuse

            NEWexpWater TEST = (from t in NewRWE.NEWexpWaters
                                where t.tblSampleID == sID & t.Valid == true
                                select t).FirstOrDefault();

            if (TEST != null)
            {
                // skip these as they are not our business to insert into a row that already exists
                // items like kit number, etc. will be here already as a result of inserting field or nutrient data
                NEW = TEST; // keep the name common to this method
                existingRecord = true; // flag for later
            }
            else
            {
                NEW = new NEWexpWater(); // create new entity as there is not one yet
            }

            // no existing record, so we are first
            if (!existingRecord)
            {
                NEW.BadBlank = false;   // XXXX we can deal with these in the near future
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

                tblSample ts = (from t in RWDE.tblSamples
                                where t.SampleID == sID & t.Valid == true
                                select t).FirstOrDefault(); // should be only one copy



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
                NEW.Rep = null;
                
                NEW.SampleDate = ts.DateCollected; // this is date part only, no time and may be junk XXXX
                if (ts.TimeCollected.Value.Year > 1970)  // likely a real value - otherwise, leave blank
                {
                    NEW.SampleDate.AddHours(ts.TimeCollected.Value.Hour); // add in pieces
                    NEW.SampleDate.AddMinutes(ts.TimeCollected.Value.Minute);
                }

                NEW.SampleNumber = ts.SampleNumber; // this is the big string of station id + date time - build at sample entry
                // tblSample has station id 
                var STN = (from s in RWDE.tblStations
                                 where s.StationID == ts.StationID
                                 select s).FirstOrDefault(); 

             //   ts.StationID
                NEW.StationName = STN.StationName;
                NEW.River_CD = null;
                NEW.RiverName = STN.River; 
                NEW.StationNumber = (short) ts.StationID;   // XXXX hope this gets working soon and we can get rid of shorts
                NEW.Sulfate = null;              
             
                NEW.tblSampleID = ts.SampleID;
                NEW.TempC = null;
                NEW.TKN = null;
                NEW.TOTAL_ALK = null;
                NEW.TOTAL_HARD = null;
            }

            // now add in the chemistry from ICP

            string workString = "";
            string tbDName;
            string tbTName;
            TextBox tbD;
            TextBox tbT;
            decimal Total;
            decimal Disolved;

            workString = "AL";
            tbDName = uniqueID + "$" + workString + "_DTextBox";  // use the key value to build the name of the text box to be processed
            tbTName = uniqueID + "$" + workString + "_TTextBox";

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

            NEW.AL_D = Disolved;
            //NEW.AL_T = Total; 




            NewRWE.NEWexpWaters.Add(NEW);
        }

      
     
    }
}