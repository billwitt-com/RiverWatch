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
    public partial class ValidateDupNutrients : System.Web.UI.Page
    {
        Dictionary<string, decimal> HighLimit = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> LowLimit = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        RiverWatchEntities NRWDE = new RiverWatchEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            // first, find all inbound lachat data that is not validated. 
            // mark all rows that have CODEs not in 05,25,35 as blkdup and validated
            // then get all barcodes that have sample types (barcodes) that start with 'RW' and not validated and valid 


            bool allowed = false;
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx");
            lblError.Visible = false;
            if (!IsPostBack)    // load panel if there is data 
            {
                string cmdStr = string.Format("SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and typecode LIKE '25' ");
                SqlDataSource1.SelectCommand = cmdStr;
                Session["CMDSTR"] = cmdStr;
                FormView1.DataBind();
            }  
 
            //  where c.Valid == true & c.TypeCode.Contains("05") & c.Validated == false & c.SampleNumber != null

            updateCounts();
            updateControls(); 
        }

        // called when something is saved or deleted to update the label message
        public void updateCounts()
        {
            int DUPnutrientCount = 0;
            RiverWatchEntities RWE = new RiverWatchEntities();
            var C = from c in RWE.NutrientDatas
                    where c.Valid == true & c.TypeCode.Contains("25") & c.Validated == false
                    select c;
            if (C.Count() > 0)
            {
                DUPnutrientCount = C.Count();
            }

            if (DUPnutrientCount > 0)
            {
                lblNumberLeft.Text = string.Format("There are {0} 'Nutrient DUP' samples left to validate", DUPnutrientCount);
            }
            else
            {
                lblNumberLeft.Text = "There are NO Nutrient DUP samples left to validate";
            }
        }

        // user selected batch number on main page, so see if it is valid and if so, load it up to datasource 1
        protected void btnSelectBatch_Click(object sender, EventArgs e)
        {
            string batchNumber = "";
            string cmdStr = "";
            batchNumber = tbBatchNumber.Text.Trim();
            // SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and SampleNumber is not null and typecode LIKE '05'
            cmdStr = string.Format("SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and typecode LIKE '25' and Batch like '{0}'", batchNumber);
            Session["CMDSTR"] = cmdStr; 
            SqlDataSource1.SelectCommand = cmdStr;
            FormView1.DataBind(); 
        }

        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
             updateControls();
        }

        // called any time text is changed - will update the rules and such 
        public void updateControls()
        {
            string uniqueID = FormView1.Controls[0].UniqueID;
            string uniqueID_D = FormView2.Controls[0].UniqueID; // id for dup form

            decimal LoLimit = 0;
            decimal HiLimit = 0;
            bool rezult = false;
            string name = "";
            decimal HighValue = 0;
            decimal LowValue = 0;
            string tbName = "";
            string parmName = "";

            // removed [Riverwatch].[dbo].
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; //GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("select distinct Element, HighLimit, Reporting from  [NutrientLimits]");
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader()) 
                        {
                            if (sdr.HasRows)
                            {
                                HighLimit.Clear();  // we are going to refill these, so empty first
                                LowLimit.Clear(); 
                                while (sdr.Read())
                                {
                                    if (sdr["Element"].GetType() != typeof(System.DBNull))      // is this crap or what???
                                    {
                                        name = ((string)sdr["Element"]).ToUpper();  // make upper case to be sure
                                        HighValue = (decimal)sdr["HighLimit"];
                                    //    LowValue = (decimal)sdr["MDL"];
                                        LowValue = (decimal)sdr["Reporting"];
                                        HighLimit.Add(name, HighValue);    // not using this now, but... 
                                        LowLimit.Add(name, LowValue);
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
            // public bool processTB(string tbName, string UID, out decimal Value)
            // get value and compare with limits, changing background color if needed 

            tbName = "TotalPhosTextBox";
            parmName = "TotalPhos";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit,parmName);

            tbName = "OrthoPhosTextBox";
            parmName = "OrthoPhos";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "TotalNitroTextBox";
            parmName = "TotalNitro";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "NitrateNitriteTextBox";
            parmName = "NitrateNitrite";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "AmmoniaTextBox";
            parmName = "Ammonia";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "DOCTextBox";
            parmName = "DOC";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "ChlorideTextBox";
            parmName = "Chloride";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "SulfateTextBox";
            parmName = "Sulfate";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "TSSTextBox";
            parmName = "TSS";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            tbName = "ChlorATextBox";
            parmName = "ChlorA";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);

            // now do form2, duplicates
            // add in percentage difference to just dups 
            //  public void compareTextBoxes(string tbName, string UID1, string UID2, out decimal Value1, out decimal Value2)
            tbName = "TotalPhosTextBox";
            parmName = "TotalPhos";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            // now compare to each other and warn if difference is too great
            compareTextBoxes(tbName, uniqueID, uniqueID_D);           

            tbName = "OrthoPhosTextBox";
            parmName = "OrthoPhos";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "TotalNitroTextBox";
            parmName = "TotalNitro";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "NitrateNitriteTextBox";
            parmName = "NitrateNitrite";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "AmmoniaTextBox";
            parmName = "Ammonia";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "DOCTextBox";
            parmName = "DOC";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "ChlorideTextBox";
            parmName = "Chloride";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "SulfateTextBox";
            parmName = "Sulfate";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "TSSTextBox";
            parmName = "TSS";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    

            tbName = "ChlorATextBox";
            parmName = "ChlorA";
            LoLimit = LowLimit[parmName.ToUpper()];
            HiLimit = HighLimit[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, LoLimit, HiLimit, parmName);
            compareTextBoxes(tbName, uniqueID, uniqueID_D);    
        }

        /// <summary>
        ///  pass in text box name and this will return parsed decimal value if return is true
        ///  returns false if no value was found in the text
        /// </summary>
        /// <param name="tbName">name of text box</param>
        /// <param name="UID">the unique id of the control set from callers viewpoint</param>
        /// <param name="Value">decimal value of text box, if any</param>
        ///    /// <param name="TB">TextBox</param>
        /// <returns>true if value could be parsed, false for empty string or unknown textbox</returns>
        /// changed to add high limit 
        public bool processTB(string tbName, string UID, decimal LowLimit, decimal HiLimit, string parmName)
        {
            string uniqueID;
            string tbnam;
            TextBox TB;
            decimal Value = 0;
  
            uniqueID = UID; // FormViewBlank.Controls[0].UniqueID;  
            tbnam = uniqueID + "$" + tbName;  // use the key value to build the name of the text box to be processed   
            Value = 0;
            TB = this.FindControl(tbnam) as TextBox;
            if (TB == null)
            {
                return false;
            }

            if (TB.Text.Length < 1)
            {
                TB.BackColor = System.Drawing.Color.White; // nothing there, so color white and return
                return false;
            }

            if (!decimal.TryParse(TB.Text, out Value))  // failed to get proper decimal value 
            {
                TB.Text = ""; // bad decimal value, so remove... 
                TB.BackColor = System.Drawing.Color.White; // nothing there, so color white and return
                return false;
            }

            if (Value < LowLimit)
            {
                TB.BackColor = System.Drawing.Color.LightBlue;
                TB.ToolTip = string.Format("{0} is under the limit of {1}", parmName, LowLimit);
            }
            else
            {
                TB.BackColor = System.Drawing.Color.White;
                TB.ToolTip = string.Format("{0} is above the limit of {1}", parmName, LowLimit);
            }
            if(Value > HiLimit)
            {
                TB.ForeColor = System.Drawing.Color.Red; 
                TB.ToolTip = string.Format("{0} is above the limit of {1}", parmName, HiLimit);
            }
            else
            {
                TB.ForeColor = System.Drawing.Color.Black;
                TB.ToolTip = string.Format("{0} is below the limit of {1}", parmName, HiLimit);
            }
            return true;
        }

        // from validate dups, changed to return two values from different forms... 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbName">name of text box to scrape values</param>
        /// <param name="UID1">unique id of form1</param>
        /// <param name="UID2">inique id of form2</param>
        /// <param name="Value1">Value from left, form1, text box</param>
        /// <param name="Value2">Value from right form</param>
        /// applies the rule that dup must be within +- 20 % or there is a flag set for user om tooltip 
        public void compareTextBoxes(string tbName, string UID1, string UID2)
        {
            string tbNName;
            string tbDName;
            TextBox tbN;
            TextBox tbD;
            decimal Normal = 0;
            decimal Duplicate = 0;

            tbNName = UID1 + "$" + tbName; // +"_DTextBox";  // use the key value to build the name of the text box to be processed
            tbDName = UID2 + "$" + tbName; // +"_TTextBox";

            tbN = this.FindControl(tbNName) as TextBox;
            tbD = this.FindControl(tbDName) as TextBox;

            if (tbN == null)
            {
                string s = tbNName; // for debug, not used
                return;
            }
            if (tbD == null)
            {
                string s = tbDName;
                return; 
            }

            if (!decimal.TryParse(tbN.Text, out Normal))
            {
                return; // do nothing
            }

            if (!decimal.TryParse(tbD.Text, out Duplicate))
            {
                return; // do nothing
            }

            if (Duplicate >= .0001m)    // make sure this is not too close to 0 as it may be 0 and we don't want to divide by 0
            {
                if (((Normal / Duplicate) > 1.2m) | ((Normal / Duplicate) < 0.80m))
                {

                    tbD.BorderColor = Color.Red;
                    tbD.BorderWidth = Unit.Pixel(2);
                    tbD.BorderStyle = BorderStyle.Solid;
                    tbD.ToolTip += string.Format(" - Values differ by more than 120% or less than 80%");
                }
                else
                {
                    tbD.BorderColor = Color.Black;
                    tbD.BorderWidth = Unit.Pixel(0);
                    tbD.BorderStyle = BorderStyle.None; 
                    tbD.ToolTip += string.Format(" - Values are within range of 80% - 120%");
                }
            }
        }

           // can get text box here, so get all values 
        // this is the validate button !!
        // we should have valid data in text boxes... 
        // also, may be a number of rows in newExpWater table to update with this data, or none yet - this could be first or one of many
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            TextBox TB;
            double dVal = 0; 
            string barcode = "";
            string sampleNumber = "";
            bool existingRecord = false;

            double? totP = null;
            double? OP = null;
            double? totN = null;
            double? NN = null;
            double? Ammonia = null;
            double? DOC = null;
            double? Chloride = null;
            double? Sulfate = null;
            double? TSS = null;
            double? ChlorA = null;
            NEWexpWater NW = null ;
            RiverWatchEntities RWE = new RiverWatchEntities(); 

            string uniqueID = FormView1.Controls[0].UniqueID;
            string tbName = uniqueID + "$" + "BARCODETextBox";  // use the key value to build the name of the text box to be processed   

            TB = FormView1.Controls[0].FindControl("BARCODETextBox") as TextBox;
            if(TB != null)
            {
                barcode = TB.Text.Trim().ToUpper(); 
            }

            tbName = uniqueID + "$" + "SampleNumberTextBox";
            TB = FormView1.Controls[0].FindControl("SampleNumberTextBox") as TextBox;
            if(TB != null)
            {
                sampleNumber = TB.Text.Trim().ToUpper(); 
            }
            try
            {
                // get all rows 
                var T = (from t in RWE.NEWexpWaters
                         where t.SampleNumber == sampleNumber & t.Valid == true // & t.NutrientBarCode == barcode
                         select t); 

              //  if (T == null)  // no current record, so we are first and must add all detail
                   
                if (T.Count() == 0) // updated 1/27 bwitt
                {
                    NW = new NEWexpWater(); // create new entity as there is not one yet
                    existingRecord = false;
                }
                else
                {
                    existingRecord = true; 
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
            
            // scrape values off of page for updating 
            TB = FormView1.Controls[0].FindControl("TotalPhosTextBox") as TextBox;
            if(TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal ))
                {
                    totP = dVal; 
                }
            }
            TB = FormView1.Controls[0].FindControl("OrthoPhosTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    OP = dVal;
                }
            }

            TB = FormView1.Controls[0].FindControl("TotalNitroTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    totN = dVal;
                }
            }
            TB = FormView1.Controls[0].FindControl("NitrateNitriteTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    NN = dVal;
                }
            }
            TB = FormView1.Controls[0].FindControl("AmmoniaTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    Ammonia = dVal;
                }
            }
            TB = FormView1.Controls[0].FindControl("DOCTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    DOC = dVal;
                }
            }

            TB = FormView1.Controls[0].FindControl("ChlorideTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    Chloride = dVal;
                }
            }
            TB = FormView1.Controls[0].FindControl("SulfateTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    Sulfate = dVal;
                }
            }
            TB = FormView1.Controls[0].FindControl("TSSTextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    TSS = dVal;
                }
            }
            TB = FormView1.Controls[0].FindControl("ChlorATextBox") as TextBox;
            if (TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal))
                {
                    ChlorA = dVal;
                }
            }

            if (existingRecord == false)
            {    
                try
                {
                    NW.CreateDate = DateTime.Now;
                    string namm = "";
                    if (User.Identity.Name.Length < 3)
                        namm = "Not logged in";
                    else
                        namm = User.Identity.Name;

                    NW.CreatedBy = namm;  
                    NW.NutrientBarCode = barcode; // we will overwrite if there is already a bar code. Should be the same if there is one. 
                    NW.SampleNumber = sampleNumber; // if existing record, this will be the same... 
                    
                    // fill in analytical data:

                    NW.totP = totP;
                    NW.OP = OP;
                    NW.totN = totN;
                    NW.NN = NN;
                    NW.Ammonia = Ammonia;
                    NW.DOC = DOC;
                    NW.Chloride = Chloride;
                    NW.Sulfate = Sulfate ;
                    NW.TSS = TSS;
                    NW.ChlorophyllA = ChlorA;
                    NW.NutrientBarCode = barcode;
                    // must get event number from samples table
                    //var NS = (from ns in RWE.Samples
                    //          where ns.SampleNumber == sampleNumber
                    //          select new
                    //          {
                    //              ns.NumberSample,
                    //              ns.DateCollected
                    //          }).FirstOrDefault();

                    // fill in sample details
                    var NS = (from ns in RWE.Samples
                              where ns.SampleNumber == sampleNumber
                              select ns
                              ).FirstOrDefault();
                    if (NS.NumberSample.Length > 3)
                        NW.Event = NS.NumberSample; // fill in as this is the first
                    else
                        NW.Event = "";
                    if (NS.DateCollected.Year > 1900)
                        NW.SampleDate = NS.DateCollected;
                    else
                        NW.SampleDate = DateTime.Now; 
                    if(NS.StationID != null)
                        NW.StationID = NS.StationID;
                    if (NS.OrganizationID != null)
                        NW.OrganizationID = NS.OrganizationID;
                    if (NS.Comment.Length > 0)
                        NW.SampleComments = NS.Comment;

                    // fill in org details

                    var ORG = (from org in RWE.organizations
                               where org.ID == NW.OrganizationID.Value & NW.Valid == true
                               select org
                               ).FirstOrDefault(); 
                               
                    if(ORG != null)
                    {
                        NW.OrganizationName = ORG.OrganizationName;
                        NW.KitNumber = ORG.KitNumber;
                    }  
                     
                    // now add station detail

                    var STN = (from stn in RWE.Stations
                               where stn.ID == NW.StationID.Value
                               select stn).FirstOrDefault(); 
                    if(STN != null)
                    {
                        NW.StationName = STN.StationName;
                        NW.StationID = STN.ID; 
                        NW.StationNumber = STN.StationNumber;
                        NW.WaterShed = STN.RWWaterShed;   
                    }
                    RWE.NEWexpWaters.Add(NW);
                    RWE.SaveChanges();
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
            else  // there is one or more existing rows, so update nutrient data in them
            {
                try
                {
                    var T = (from t in RWE.NEWexpWaters
                    where t.SampleNumber == sampleNumber & t.Valid == true // & t.NutrientBarCode == barcode
                    select t); 
                
                    foreach(NEWexpWater n in T) // may be one or more but we just update our data, the rest must be there
                    {
                        n.totP = totP;
                        n.OP = OP;
                        n.totN = totN;
                        n.NN = NN;
                        n.Ammonia = Ammonia;
                        n.DOC = DOC;
                        n.Chloride = Chloride;
                        n.Sulfate = Sulfate;
                        n.TSS = TSS;
                        n.ChlorophyllA = ChlorA;
                        n.NutrientBarCode = barcode; 
                    }
                    RWE.SaveChanges(); // update all records
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
            try
            {
                // update all rows that have this barcode in lachet table
                var L = from l in RWE.Lachats
                        where l.SampleType.ToUpper() == barcode.ToUpper()
                        select l;
                if (L != null)   
                {
                    foreach (Lachat l in L)
                    {
                        l.Validated = true;
                        l.Valid = true;
                        l.PassValStep = 2.0m; // just in case
                    }
                    RWE.SaveChanges();
                }

                // I think this must happen before the sqldatasource update
                // JUST COMMENTED OUT THE EXISTING UPDATE COMMAND FROM THIS BUTTON
                var ND = (from nd in RWE.NutrientDatas
                          where nd.BARCODE.ToUpper() == barcode.ToUpper()
                          select nd).FirstOrDefault();
                if (ND != null)
                {
                    ND.Validated = true; 
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
            updateCounts();

            if(Session["CMDSTR"] != null)
            {
                string cmdStr = (string)Session["CMDSTR"];
                SqlDataSource1.SelectCommand = cmdStr;  // keep it current
            }
            SqlDataSource1.Update(); // refresh the data as we just edited it...     

            SqlDataSource2.Update(); // the only differnce with validatenutrients .... 
        }

        // I think we need to make both samples bad in this case, or change the type of the sister dup to 05 
        // we are not changing data here, just marking lachat and nutrientData entries as invalid, not validated and failed as true
        protected void btnBad_Click(object sender, EventArgs e)
        {
            // update all rows that have this barcode in lachet table
            RiverWatchEntities RWE = new RiverWatchEntities();
            string barcode = "";
            TextBox TB = null;

            // get barcode so we can select      

            TB = FormView1.Controls[0].FindControl("BARCODETextBox") as TextBox;
            if (TB != null)
            {
                barcode = TB.Text.Trim().ToUpper();
            }

            try
            {
                var L = from l in RWE.Lachats
                        where l.SampleType.ToUpper() == barcode.ToUpper()
                        select l;
                if (L != null)
                {
                    foreach (Lachat l in L)
                    {
                        l.Validated = false;
                        l.Valid = false;
                        l.Failed = true;
                    }
                    RWE.SaveChanges();
                }

                // I think this must happen before the sqldatasource update
                var ND = (from nd in RWE.NutrientDatas
                          where nd.BARCODE.ToUpper() == barcode.ToUpper()
                          select nd).FirstOrDefault();
                if (ND != null)
                {
                    ND.Validated = false;   // mark as bad sample 
                    ND.Valid = false;
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

            // now process dup, if it exists... 
            if (FormView2.Visible == false)
                return;

            // process duplicate
            TB = FormView2.Controls[0].FindControl("BARCODETextBox") as TextBox;
            if (TB != null)
            {
                barcode = TB.Text.Trim().ToUpper();
            }

            try
            {

                var L = from l in RWE.Lachats
                        where l.SampleType.ToUpper() == barcode.ToUpper()
                        select l;
                if (L != null)
                {
                    foreach (Lachat l in L)
                    {
                        l.Validated = false;
                        l.Valid = false;
                        l.Failed = true;
                    }
                    RWE.SaveChanges();
                }

                // I think this must happen before the sqldatasource update
                var ND = (from nd in RWE.NutrientDatas
                          where nd.BARCODE.ToUpper() == barcode.ToUpper()
                          select nd).FirstOrDefault();
                if (ND != null)
                {
                    ND.Validated = false;   // mark as bad sample 
                    ND.Valid = false;
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
            updateCounts(); 
        }



        protected void FormView1_DataBinding(object sender, EventArgs e)
        {
          
        }
        // this should be where we update the data for formview2, the dup
        // get sample number so we can query for dup bar code
        // controls not yet known... 
        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();

            string sampleNumber = "";
            string query = "";
            TextBox TB = FormView1.Controls[0].FindControl("SampleNumberTextBox") as TextBox;
            lblError.Visible = false;
            
            if (TB == null) // this happens when page opens, so may be OK
            {
                //lblError.Text = "There is no valid sample number for this barcode";
                //lblError.Visible = true;
                return;
                // do something here, not sure what yet
            }
            if(TB.Text.Length < 1)
            {
                lblError.Text = "There is no valid sample number for this data";
                lblError.Visible = true;
                return;
            }

            sampleNumber = TB.Text.Trim();
           
            // check to see if there is an entry for this sample number, should be..

            string BC = (from s in RWE.NutrientDatas
                         where s.SampleNumber == sampleNumber & s.TypeCode == "35" & s.Valid == true & s.Validated == false
                         select s.BARCODE).FirstOrDefault();

            if (BC == null) // we have no dup, so notify user and return
            {
                lblError.Text = string.Format("There is no matching duplicate for this barcode");
                lblError.Visible = true;
                FormView2.Visible = false; 
                return;
            }
            FormView2.Visible = true; 
            // we have good barcode for dup, so set formview2 to this barcode. 

            //if (Session["CMDSTR"] != null)
            //{
            //    query = (string)Session["BATCH_CMDSTR"];
            //}
            //else
            //{
                query = string.Format("SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and barcode = '{0}'", BC);
          //  }
            SqlDataSource2.SelectCommand = query;
            FormView2.DataBind(); // force update from sql data source
        }


        protected void FormView2_DataBound(object sender, EventArgs e)
        {
            updateControls(); 
        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {

            string uzr = "Unknown";
            if (User.Identity.Name.Length > 3)
            {
                uzr = User.Identity.Name;
            }
            e.Command.Parameters["@CreatedBy"].Value = uzr;
            e.Command.Parameters["@Valid"].Value = true;
            e.Command.Parameters["@Validated"].Value = true;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now; 
        }

        protected void SqlDataSource2_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string uzr = "Unknown";
            if (User.Identity.Name.Length > 3)
            {
                uzr = User.Identity.Name;
            }
            e.Command.Parameters["@CreatedBy"].Value = uzr;
            e.Command.Parameters["@Valid"].Value = true;
            e.Command.Parameters["@Validated"].Value = true;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now; 
        }
    }
}