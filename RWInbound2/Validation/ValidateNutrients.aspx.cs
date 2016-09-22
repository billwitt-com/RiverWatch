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
    public partial class ValidateNutrients : System.Web.UI.Page
    {
        Dictionary<string, decimal> NLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        Dictionary<string, decimal> MeasurementLimits = new Dictionary<string, decimal>();   // holds symbol and D2Tlimit values
        RiverWatchEntities NRWDE = new RiverWatchEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            // first, find all inbound lachat data that is not validated. 
            // mark all rows that have CODEs not in 05,25,35 as blkdup and validated
            // then get all barcodes that have sample types (barcodes) that start with 'RW' and not validated and valid 
            RiverWatchEntities RWE = new RiverWatchEntities();
            int nutrientCount = 0;
            bool allowed = false;

            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx");

            //  where c.Valid == true & c.TypeCode.Contains("05") & c.Validated == false & c.SampleNumber != null

            var C = from c in RWE.NutrientDatas
                    where c.Valid == true & c.TypeCode.Contains("05") & c.Validated == false 
                    select c;
            if (C.Count() > 0)
            {
                nutrientCount = C.Count();
            }

            if (nutrientCount > 0)
            {
                lblNumberLeft.Text = string.Format("There are {0} 'Normal' samples left to validate", nutrientCount);
            }
            else
            {
                lblNumberLeft.Text = "There are NO samples left to validate";
            }

            if (!IsPostBack)
            {
                DataSourceSelectArguments args = new DataSourceSelectArguments();
                string name = "";
                decimal D2Tvalue = 0;
                decimal MeasurementValue = 0;               
                try
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; //GlobalSite.RiverWatchDev;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = string.Format("select distinct Element, DvsTDifference, MDL from  [Riverwatch].[dbo].[tlkNutrientLimits]");
                            cmd.Connection = conn;
                            conn.Open();

                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                if (sdr.HasRows)
                                {
                                    while (sdr.Read())
                                    {
                                        if (sdr["RowID"].GetType() != typeof(System.DBNull))      // is this crap or what???
                                        {
                                            name = ((string)sdr["[RowID]"]).ToUpper();  // make upper case to be sure
                                            D2Tvalue = (decimal)sdr["DvsTDifference"];
                                            MeasurementValue = (decimal)sdr["MDL"];
                                            NLimits.Add(name, D2Tvalue);
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
            }
               
        }
        // this is the place to do metrics and change colors, etc
        // let's use green for below limits and pink for above
        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            updateControls(); // run validation on these values 
        }

        // can get text box here, so get all values 
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

            NEWexpWater NW = null ;

            string uniqueID = FormView1.Controls[0].UniqueID;
            string tbName = uniqueID + "$" + "BARCODETextBox";  // use the key value to build the name of the text box to be processed   

            TB = FormView1.Controls[0].FindControl("BARCODETextBox") as TextBox;
            if(TB != null)
            {
                barcode = TB.Text.Trim().ToUpper(); 
            }

            tbName = uniqueID + "$" + "SampleNumberTextBox";
            TB = FormView1.Controls[0].FindControl("SampleNumberTextBox") as TextBox;
         //   TB = FormView1.Controls[0].FindControl(tbName) as TextBox;
            if(TB != null)
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
            
            TB = FormView1.Controls[0].FindControl("TotalPhosTextBox") as TextBox;
            if(TB.Text.Length > 0)
            {
                if (double.TryParse(TB.Text, out dVal ))
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

            // update all rows that have this barcode in lachet table
            var L = from l in RWE.Lachats
                    where l.SampleType.ToUpper() == barcode.ToUpper()
                    select l;
            if(L != null)   // should never happen
            {
                foreach(Lachat l in L)
                {
                    l.Validated = true;
                    l.Valid = true; 
                }
                RWE.SaveChanges(); 
            }

            // I think this must happen before the sqldatasource update
            var ND = (from nd in RWE.NutrientDatas
                     where nd.BARCODE.ToUpper() == barcode.ToUpper()
                     select nd).FirstOrDefault();  
            if(ND != null)
            {       
                ND.Validated = true;
                RWE.SaveChanges();
            }
        }

        // TextBox_TextChanged called by all text box changes
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            updateControls();             
        }
        public void updateControls()
        {
            string uniqueID = FormView1.Controls[0].UniqueID;           
            decimal Limit = 0;
            bool rezult = false;
            string name = "";
            decimal D2Tvalue = 0;
            decimal MeasurementValue = 0;
            string tbName = "";
            string parmName = "";

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; //GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("select distinct Element, DvsTDifference, MDL from  [Riverwatch].[dbo].[tlkNutrientLimits]");
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
                                        NLimits.Add(name, D2Tvalue);    // not using this now, but... 
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
            // public bool processTB(string tbName, string UID, out decimal Value)
            // get value and compare with limits, changing background color if needed 

            tbName = "TotalPhosTextBox";
            parmName = "TotalPhos";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "OrthoPhosTextBox";
            parmName = "OrthoPhos";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "TotalNitroTextBox";
            parmName = "TotalNitro";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "NitrateNitriteTextBox";
            parmName = "NitrateNitrite";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "AmmoniaTextBox";
            parmName = "Ammonia";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "DOCTextBox";
            parmName = "DOC";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "ChlorideTextBox";
            parmName = "Chloride";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "SulfateTextBox";
            parmName = "Sulfate";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "TSSTextBox";
            parmName = "TSS";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName);

            tbName = "ChlorATextBox";
            parmName = "ChlorA";
            Limit = MeasurementLimits[parmName.ToUpper()];
            rezult = processTB(tbName, uniqueID, Limit, parmName); 

            // compare with min value from list
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
        }

        // don't think this is being used as we set up these parms in our code
        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            // <asp:Parameter Name="Valid" Type="Boolean" />
            //<asp:Parameter Name="Validated" Type="Boolean" />
            //<asp:Parameter Name="DateCreated" Type="DateTime" />
            //<asp:Parameter Name="CreatedBy" Type="String" />

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
        /// <summary>
        ///  pass in text box name and this will return parsed decimal value if return is true
        ///  returns false if no value was found in the text
        /// </summary>
        /// <param name="tbName">name of text box</param>
        /// <param name="UID">the unique id of the control set from callers viewpoint</param>
        /// <param name="Value">decimal value of text box, if any</param>
        ///    /// <param name="TB">TextBox</param>
        /// <returns>true if value could be parsed, false for empty string or unknown textbox</returns>
        public bool processTB(string workStr, string UID, decimal Limit, string parmName)
        {
            string workString = "";
            string uniqueID;
            string tbName;
            TextBox TB;
            decimal Value = 0; 

            workString = workStr;
            uniqueID = UID; // FormViewBlank.Controls[0].UniqueID;  
            tbName = uniqueID + "$" + workString ;  // use the key value to build the name of the text box to be processed   
            Value = 0; 
            TB = this.FindControl(tbName) as TextBox;
            if(TB == null)
            {
                return false; 
            }

            if (TB.Text.Length < 1)
            {
                return false;
            }

            if (!decimal.TryParse(TB.Text, out Value))  // failed to get proper decimal value 
            {
                return false; 
            }
            
            if (Value < Limit)
            {
                TB.BackColor = System.Drawing.Color.Pink;
                TB.ToolTip = string.Format("{0} is under the limit of {1}", parmName, Limit);
            }
            else
            {
                TB.BackColor = System.Drawing.Color.White;
                TB.ToolTip = string.Format("{0} is above the limit of {1}", parmName, Limit);
            }

            return true;         
        }

        // we do not add this sample to nutrientbarcode, nor newExpWater
        // but do mark it as invalid and failed
        protected void btnBad_Click(object sender, EventArgs e)
        {
            // update all rows that have this barcode in lachet table
            RiverWatchEntities RWE = new RiverWatchEntities();
            string barcode = "";
            TextBox TB = null;

            // get barcode so we can select
            string uniqueID = FormView1.Controls[0].UniqueID;
            string tbName = uniqueID + "$" + "BARCODETextBox";  // use the key value to build the name of the text box to be processed   

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
        }

        // a batch number was input, so we need to filter by batch number

        protected void btnSelectBatch_Click(object sender, EventArgs e)
        {
            string batchNumber = "";
            string cmdStr = "";
            batchNumber = tbBatchNumber.Text.Trim();
            // SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and SampleNumber is not null and typecode LIKE '05'
            cmdStr = string.Format("SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and typecode LIKE '05' and Batch like '{0}'", batchNumber);
            
            SqlDataSource1.SelectCommand = cmdStr;
            FormView1.DataBind(); 
        }
    }
}