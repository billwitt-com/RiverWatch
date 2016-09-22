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
            RiverWatchEntities RWE = new RiverWatchEntities();
            int DUPnutrientCount = 0;
            bool allowed = false;

            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx");
            lblError.Visible = false; 
            //  where c.Valid == true & c.TypeCode.Contains("05") & c.Validated == false & c.SampleNumber != null

            var C = from c in RWE.NutrientDatas
                    where c.Valid == true & (c.TypeCode.Contains("25") | c.TypeCode.Contains("35")) & c.Validated == false
                    select c;
            if (C.Count() > 0)
            {
                DUPnutrientCount = C.Count();
            }

            if (DUPnutrientCount > 0)
            {
                lblNumberLeft.Text = string.Format("There are {0} 'NormalDUP' samples left to validate", DUPnutrientCount);
            }
            else
            {
                lblNumberLeft.Text = "There are NO samples left to validate";
            }
            updateControls(); 
        }

        protected void btnSelectBatch_Click(object sender, EventArgs e)
        {
            string batchNumber = "";
            string cmdStr = "";
            batchNumber = tbBatchNumber.Text.Trim();
            // SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and SampleNumber is not null and typecode LIKE '05'
            cmdStr = string.Format("SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and typecode LIKE '25' and Batch like '{0}'", batchNumber);

            SqlDataSource1.SelectCommand = cmdStr;
            FormView1.DataBind(); 
        }
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
             updateControls();
        }
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

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; //GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("select distinct Element, HighLimit, Reporting from  [Riverwatch].[dbo].[NutrientLimits]");
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
            //  public void compareTextBoxes(string tbName, string UID1, string UID2, out decimal Normal, out decimal Duplicate)
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
        /// <param name="Normal">Value from left, form1, text box</param>
        /// <param name="Duplicate">Value from right form</param>
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
        // this will need to change to accomodate two forms
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // TotalPhosTextBox
            // OrthoPhosTextBox            
            // TotalNitroTextBox
            // NitrateNitriteTextBox
            // AmmoniaTextBox
            // DOCTextBox
            // ChlorideTextBox
            // SulfateTextBox
            // TSSTextBox
            // ChlorATextBox

            RiverWatchEntities RWE = new RiverWatchEntities();

            TextBox TB;
            double dVal = 0;
            string barcode = "";
            string sampleNumber = "";
            bool existingRecord = false;

            NEWexpWater NW = null;

            string uniqueID = FormView1.Controls[0].UniqueID;
            string tbName = uniqueID + "$" + "BARCODETextBox";  // use the key value to build the name of the text box to be processed   

            TB = FormView1.Controls[0].FindControl("BARCODETextBox") as TextBox;
            if (TB != null)
            {
                barcode = TB.Text.Trim().ToUpper();
            }

            tbName = uniqueID + "$" + "SampleNumberTextBox";
            TB = FormView1.Controls[0].FindControl("SampleNumberTextBox") as TextBox;
            //   TB = FormView1.Controls[0].FindControl(tbName) as TextBox;
            if (TB != null)
            {
                sampleNumber = TB.Text.Trim().ToUpper();
            }
            try
            {
                NEWexpWater TEST = (from t in RWE.NEWexpWaters
                                    where t.SampleNumber == sampleNumber & t.Valid == true // & t.NutrientBarCode == barcode
                                    select t).FirstOrDefault();
                if (TEST != null)
                {
                    NW = TEST; // keep the name common to this method
                    existingRecord = true; // flag for later
                }
                else
                {
                    NW = new NEWexpWater(); // create new entity as there is not one yet
                    existingRecord = false;
                }

                TB = FormView1.Controls[0].FindControl("TotalPhosTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.totP = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("OrthoPhosTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.OP = dVal;
                    }
                }

                TB = FormView1.Controls[0].FindControl("TotalNitroTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.totN = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("NitrateNitriteTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.NN = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("AmmoniaTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.Ammonia = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("DOCTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.DOC = dVal;
                    }
                }

                // ChlorideTextBox
                // SulfateTextBox
                // TSSTextBox
                // ChlorATextBox

                TB = FormView1.Controls[0].FindControl("ChlorideTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.Chloride = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("SulfateTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.Sulfate = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("TSSTextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.TSS = dVal;
                    }
                }
                TB = FormView1.Controls[0].FindControl("ChlorATextBox") as TextBox;
                if (TB.Text.Length > 0)
                {
                    if (double.TryParse(TB.Text, out dVal))
                    {
                        NW.ChlorophyllA = dVal;
                    }
                }
                NW.CreateDate = DateTime.Now;
                string namm = "";
                if (User.Identity.Name.Length < 3)
                    namm = "Not logged in";
                else
                    namm = User.Identity.Name;
                NW.CreatedBy = namm;
                NW.NutrientBarCode = barcode; // we will overwrite if there is already a bar code. Should be the same if there is one. 
                NW.SampleNumber = sampleNumber; // if existing record, this will be the same... 
                if (!existingRecord) // no existing record, so we are first
                {
                    RWE.NEWexpWaters.Add(NW);
                }
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


            SqlDataSource1.Update(); // force update 
            SqlDataSource2.Update(); 

            // update all rows that have this barcode in lachet table
            try
            {
                var L = from l in RWE.Lachats
                        where l.SampleType.ToUpper() == barcode.ToUpper()
                        select l;
                if (L != null)   // should never happen
                {
                    foreach (Lachat l in L)
                    {
                        l.Validated = true;
                        l.Valid = true;
                    }
                    RWE.SaveChanges();
                }

                var ND = (from nd in RWE.NutrientDatas
                          where nd.BARCODE.ToUpper() == barcode.ToUpper()
                          select nd).FirstOrDefault();
                if (ND != null)
                {
                    ND.Validated = true;
                    RWE.SaveChanges();
                }

                // now, update for dup if it exists using form 2
                if (FormView2.Visible == true)
                {
                    // get this barcode
                    TB = FormView2.Controls[0].FindControl("BARCODETextBox") as TextBox;    // note use of form 2
                    if (TB != null)
                    {
                        barcode = TB.Text.Trim().ToUpper();
                    }

                    var LL = from l in RWE.Lachats
                             where l.SampleType.ToUpper() == barcode.ToUpper()
                             select l;
                    if (LL != null)   // should never happen
                    {
                        foreach (Lachat l in L)
                        {
                            l.Validated = true;
                            l.Valid = true;
                        }
                        RWE.SaveChanges();
                    }

                    var ND1 = (from nd in RWE.NutrientDatas
                               where nd.BARCODE.ToUpper() == barcode.ToUpper()
                               select nd).FirstOrDefault();
                    if (ND1 != null)
                    {
                        ND1.Validated = true;                        
                        RWE.SaveChanges();
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
            if (TB == null)
            {
                lblError.Text = "There is no valid sample number for this barcode";
                lblError.Visible = true;
                return;
                // do something here, not sure what yet
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

            query = string.Format("SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and barcode = '{0}'", BC);
            SqlDataSource2.SelectCommand = query;
            FormView2.DataBind(); // force update from sql data source
        }

        protected void SqlDataSource2_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {

        }

        protected void FormView2_DataBound(object sender, EventArgs e)
        {
            updateControls(); 
        }
    }
}