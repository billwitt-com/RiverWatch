using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;


namespace RWInbound2.Applications
{
    public partial class AWQMSChems : System.Web.UI.Page
    {
        DateTime StartDate;
        DateTime EndDate;
        string Wbid = "";
        DataTable DTlookup = new DataTable(); // make public so it can be shared without memory overhead in a method call
        DataTable DTout = new DataTable(); // output table
   //     static IQueryable<tlkAQWMStranslation> LKUP; 
        List<tlkAQWMStranslation> LKUP = new List<tlkAQWMStranslation>(); 

        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                tbStartDate.Text = DateTime.Now.AddYears(-2).ToShortDateString();    // preload to some value... 
                tbEndDate.Text = DateTime.Now.ToShortDateString();
                btnDownload.Visible = false;  
                lblDownload.Visible = false;
            }
        }

        // create report

        protected bool createReport(DateTime startDate, DateTime endDate, string WBID)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();

         //  IEnumerable<tlkAQWMStranslation> Lookup = null; 
          //  decimal? result;
            decimal? result; 
            string symbol = "";

         //   bool updateRowResult = false;

            // first, read translation table into in-memory data table to avoid a second db query for each line.
            try
            {
                LKUP.Clear(); 
               var ZZ = from Z in RWE.tlkAQWMStranslations
                         where Z.Valid == true
                         select Z;
               foreach (tlkAQWMStranslation s in ZZ)
                   LKUP.Add(s);                                
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
                return false; 
            }

            

           // if(DTlookup.Rows.Count < 20)
            if (LKUP.Count() < 20)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = "Data table in AWQMSChems did not fill, fatal error";
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath,   "", nam, "");
                return false; // not sure what to do here... 
            }

            // now create data table for output data, make them all strings since we will do no changes, just write to CSV file.  
            // is 27 columns currently

            DTout.Columns.Add(new DataColumn("Monitoring Location ID", typeof(string)));    // station number   XX
            DTout.Columns.Add(new DataColumn("Project ID", typeof(string)));    //,"1"                  XX
            DTout.Columns.Add(new DataColumn("Activity ID", typeof(string)));   // our event code
            DTout.Columns.Add(new DataColumn("Activity Type", typeof(string)));     // , "Sample-Routine"
            DTout.Columns.Add(new DataColumn("Sample Collection Method ID", typeof(string)));       //, "SM 1060B"
            DTout.Columns.Add(new DataColumn("Equipment ID", typeof(string)));      // , "Water Bottle"
            DTout.Columns.Add(new DataColumn("Activity Start Date", typeof(string)));   // MM/DD/YYYY
            DTout.Columns.Add(new DataColumn("Activity Start Time", typeof(string)));   //HHMM
            DTout.Columns.Add(new DataColumn("Activity Start Time Zone", typeof(string)));  // , "MST"
            DTout.Columns.Add(new DataColumn("Activity Media Name", typeof(string)));       // , "Water"
            DTout.Columns.Add(new DataColumn("Characteristic Name", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Sample Fraction", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Value", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Unit", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Value Type", typeof(string))); // , "Actual"
            DTout.Columns.Add(new DataColumn("Result Detection Condition", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Qualifier", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Status ID", typeof(string)));  // , "Validated"
            DTout.Columns.Add(new DataColumn("Result Analytical Method ID", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Analytical Method Context", typeof(string)));
            // above unchanged

            // three columns below have been renamed
            DTout.Columns.Add(new DataColumn("Method Detection Level", typeof(string)));
            DTout.Columns.Add(new DataColumn("Lower Reporting Limit", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Detection Limit Unit", typeof(string)));

            DTout.Columns.Add(new DataColumn("Method Speciation", typeof(string)));
            DTout.Columns.Add(new DataColumn("Activity Media Subdivision Name", typeof(string)));   // , "Surface Water"
            DTout.Columns.Add(new DataColumn("Analysis Start Date", typeof(string)));   // MM/DD/YYYY
            DTout.Columns.Add(new DataColumn("Analysis Start Time", typeof(string)));   // HHMM
            DTout.Columns.Add(new DataColumn("Analysis Start Time Zone", typeof(string)));      // , "MST"

            
            var R = from r in RWE.viewAWQMSDatas
                    where r.Valid == true & r.WaterBodyID.StartsWith(WBID) & r.SampleDate >= startDate & r.SampleDate <= endDate & !r.TypeCode.StartsWith("1") & !r.TypeCode.StartsWith("2")
                    orderby r.SampleDate.Value
                    select r;
       
            foreach(viewAWQMSData r in R)
            {
                    symbol = "AL_D";
                    result = r.AL_D;                  
                    calculateRow( r, result, symbol );                                

                    symbol = "AL_T";
                    result = r.AL_T;
                    calculateRow(r, result, symbol);

                    symbol = "AS_D";
                    result = r.AS_D; 
                    calculateRow(r, result, symbol);

                    symbol = "AS_T";
                    result = r.AS_T;
                    calculateRow(r, result, symbol);

                    symbol = "CA_D";
                    result = r.CA_D; 
                    calculateRow(r, result, symbol);               

                    symbol = "CA_T";
                    result = r.CA_T;
                    calculateRow(r, result, symbol);

                    symbol = "CD_D";
                    result = r.CD_D;
                    calculateRow(r, result, symbol);
                 

                    symbol = "CD_T";
                    result = r.CD_T;
                    calculateRow(r, result, symbol);

                    symbol = "CU_D";
                    result = r.CU_D;
                    calculateRow(r, result, symbol);
           

                    symbol = "CU_T";
                    result = r.CU_T;
                    calculateRow(r, result, symbol);

                    symbol = "FE_D";
                    result = r.FE_D;
                    calculateRow(r, result, symbol);
             

                    symbol = "FE_T";
                    result = r.FE_T;
                    calculateRow(r, result, symbol);

                    symbol = "K_D";
                    result = r.K_D;
                    calculateRow(r, result, symbol);

                    symbol = "K_T";
                    result = r.K_T;
                    calculateRow(r, result, symbol);

                    symbol = "MG_D";
                    result = r.MG_D;
                    calculateRow(r, result, symbol);

                    symbol = "MG_T";
                    result = r.MG_T;
                    calculateRow(r, result, symbol);

                    symbol = "MN_D";
                    result = r.MN_D;
                    calculateRow(r, result, symbol);

                    symbol = "MN_T";
                    result = r.MN_T;
                    calculateRow(r, result, symbol);

                    symbol = "NA_D";
                    result = r.NA_D;
                    calculateRow(r, result, symbol);              

                    symbol = "NA_T";
                    result = r.NA_T;
                    calculateRow(r, result, symbol);

                    symbol = "PB_D";
                    result = r.PB_D;
                    calculateRow(r, result, symbol);               

                    symbol = "PB_T";
                    result = r.PB_T;
                    calculateRow(r, result, symbol);

                    symbol = "SE_D";
                    result = r.SE_D;
                    calculateRow(r, result, symbol);              

                    symbol = "SE_T";
                    result = r.SE_T;
                    calculateRow(r, result, symbol);

                    symbol = "ZN_D";
                    result = r.ZN_D;
                    calculateRow(r, result, symbol);

                    symbol = "ZN_T";
                    result = r.ZN_T;
                    calculateRow(r, result, symbol);

                    symbol = "Ammonia";
                    result = (decimal?)r.Ammonia;
                    calculateRow(r, result, symbol);

                    symbol = "Chloride";
                    result = (decimal?)r.Chloride;
                    calculateRow(r, result, symbol);

                    symbol = "totP";
                    result = (decimal?)r.totP;
                    calculateRow(r, result, symbol);

                    symbol = "Sulfate";
                    result = (decimal?)r.Sulfate;
                    calculateRow(r, result, symbol);

                    symbol = "TotalNitro";
                    result = (decimal?)r.totN;
                    calculateRow(r, result, symbol);

                    symbol = "NitrateNitrite";
                    result = (decimal?)r.NN;
                    calculateRow(r, result, symbol);

                    symbol = "TSS";
                    result = (decimal?)r.TSS;
                    calculateRow(r, result, symbol);

                // FIELD DATA - 

                    symbol = "PH";
                    result = (decimal?)r.PH;
                    calculateRow(r, result, symbol);

                    symbol = "PHEN_ALK";
                    result = (decimal?)r.PHEN_ALK;
                    calculateRow(r, result, symbol);

                    symbol = "TempC";
                    result = (decimal?)r.TempC;
                    calculateRow(r, result, symbol);

                    symbol = "TOTAL_ALK";
                    result = (decimal?)r.TOTAL_ALK;
                    calculateRow(r, result, symbol);

                    symbol = "TOTAL_HARD";
                    result = (decimal?)r.TOTAL_HARD;
                    calculateRow(r, result, symbol);

                    symbol = "DO_MGL";
                    result = (decimal?)r.DO_MGL;
                    calculateRow(r, result, symbol);

                    symbol = "USGS_Flow";
                    result = (decimal?)r.USGS_Flow;
                    calculateRow(r, result, symbol);

                    symbol = "DOSAT";
                    result = (decimal?)r.DOSAT;
                    calculateRow(r, result, symbol);

            }


            // when we get here, we have completed data table, ready to write out to file... 
            // may consider writing each new row... May be slower but... 

            string app_DataDirectory = HttpContext.Current.Server.MapPath("~/App_Data");
            if (!Directory.Exists(app_DataDirectory))
            {
                Directory.CreateDirectory(app_DataDirectory);
            }

         //   string filesToDelete = "~/App_Data/AWQMSMetalsandNutrient*.csv";

            string[] txtList = Directory.GetFiles(app_DataDirectory, "AWQMSMetalsandNutrient*.csv");
            foreach (string f in txtList)
            {
                File.Delete(f);
            }

            string outFile = "~/App_Data/AWQMSMetalsandNutrient." + DateTime.Now.ToString("MM.dd.yyyy") + ".csv";
            outFile = HttpContext.Current.Server.MapPath(outFile);

            FileInfo FI = new FileInfo(outFile);    // delete if exists
            if (FI.Exists)
                FI.Delete();

            Session["OUTFILE"] = outFile; 
            
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                OurWriter.WriteDataTable(DTout, writer, true);
            }

            FileInfo FII = new FileInfo(outFile);
            string shortFileName = FII.Name; 
            btnDownload.Visible = true;
            btnDownload.Text = "DownLoad"; 
            lblDownload.Visible = true;
            lblDownload.Text = string.Format("Download {0}", shortFileName); 

            return true; 
        }


        protected void calculateRow(viewAWQMSData r, decimal? result, string symbol )
        {
           
            int stationNumber;
            DateTime anaDate;
            string selStr = "";
            decimal DL = 0.0m;
            decimal RL = 0.0m;
            bool isNullValue = false;


                System.Data.DataRow DR = DTout.NewRow();                // make a new row for the data table

            if(result == null)
            {
                DR["Result Value"] = ""; // nothing
                isNullValue = true;
            }
            else  if (result.Value <= .0001m) //  'zero'
                {
                    DR["Result Value"] = "0.0000"; // make it look like a real zero
                    isNullValue = false; 
                }
                else  // result is a positive number
                {
                    DR["Result Value"] = result.Value.ToString("F4"); // format for 4 digits to right of decimal point
                    isNullValue = false;
                }

                if (r.StationNumber != null)
                {
                    stationNumber = r.StationNumber.Value;
                    DR["Monitoring Location ID"] = stationNumber.ToString();                // ("{0:0.0000}"); 
                }

                if (r.SampleDate != null)
                {
                    DR["Activity Start Date"] = r.SampleDate.Value.Month.ToString() + "/" + r.SampleDate.Value.Day.ToString() + "/" + r.SampleDate.Value.Year.ToString();
                    DR["Activity Start Time"] = r.SampleDate.Value.Hour.ToString() + r.SampleDate.Value.Minute.ToString();
                    DR["Analysis Start Date"] = r.SampleDate.Value.Month.ToString() + "/" + r.SampleDate.Value.Day.ToString() + "/" + r.SampleDate.Value.Year.ToString();
                    DR["Analysis Start Time"] = r.SampleDate.Value.ToString("MMmm");
                        //r.SampleDate.Value.Hour.ToString() + r.SampleDate.Value.Minute.ToString();
                     
                    DR["Activity Start Time Zone"] = "MST";
                    DR["Analysis Start Time Zone"] = "MST";
                    anaDate = r.SampleDate.Value;
                }
                else
                {
                    anaDate = DateTime.Parse("1950-01-01");         // defalult if date unknown
                }

                // fill in the 'constant' values
                DR["Project ID"] = '1'; 
                DR["Activity ID"] = r.Event; 
                DR["Activity Type"] = "Sample-Routine";
                DR["Sample Collection Method ID"] = "SM 1060B";
                DR["Equipment ID"] = "Water Bottle"; 
                DR["Activity Media Name"] = "Water";
                DR["Result Value Type"] = "Actual";
                DR["Activity Media Subdivision Name"] = "Surface Water";
                DR["Result Status ID"] = "Validated" ;

                // now do the lookups from DT
      //,[LocalName]
      //,[Characteristic Name]
      //,[Result Unit]
      //,[Result Sample Fraction]
      //,[Result Analytical Method ID]
      //,[Result Analytical Method Context]
      //,[Method Detection Level]
      //,[Lower Reporting Limit]
      //,[Result Detection Limit Unit]
      //,[Method Speciation]
      //,[StartDate]
      //,[EndDate]
      //,[DateCreated]
      //,[UserCreated]
      //,[Valid]

           //     DataRow[] RR = null ;

                string stophere = "stop"; 
           tlkAQWMStranslation T = (from t in LKUP
                         where t.LocalName == symbol & t.StartDate <= anaDate & t.EndDate >= anaDate
                         select t).FirstOrDefault();                                    

               
                  //  selStr = string.Format("StartDate >= #{0}#", anaDate);       // and EndDate < #{0}# and LocalName = '{1}'", anaDate, "AL_D" );
                  //  selStr = string.Format("LocalName = '{0}'", symbol); 
                //    RR = DTlookup.Select(selStr); 


                 if(T != null)                           //  if(RR != null)
                 {
                     DR["Characteristic Name"] = (T.Characteristic_Name ?? "").TrimEnd('\r', '\n');                   //T.Characteristic_Name.TrimEnd('\r', '\n');
                     DR["Result Sample Fraction"] = (T.Result_Sample_Fraction ?? "").TrimEnd('\r', '\n');                            //RR[0]["Result Sample Fraction"] ?? "";
                     DR["Result Unit"] = (T.Result_Unit ?? "").TrimEnd('\r', '\n');                                                 //RR[0]["Result Unit"] ?? "";

                     DR["Result Analytical Method ID"] = (T.Result_Analytical_Method_ID ?? "").TrimEnd('\r', '\n');                  //RR[0]["Result Analytical Method ID"] ?? "";
                     DR["Result Analytical Method Context"] = (T.Result_Analytical_Method_Context ?? "").TrimEnd('\r', '\n');        //RR[0]["Result Analytical Method Context"] ?? "";

                    // replaced 
                    //DR["Result Detection Limit Value"] = RR[0]["DetectionLevel"] ?? ""; 
                    //DR["Result Detection Limit Unit"] = RR[0]["DetectionUnit"] ?? "";
                    //DR["Result Detection Limit Type"] = RR[0]["LowerReportingLimit"] ?? "";

                    // examples
                    //DTout.Columns.Add(new DataColumn("Method Detection Level", typeof(string)));
                    //DTout.Columns.Add(new DataColumn("Lower Reporting Limit", typeof(string)));
                    //DTout.Columns.Add(new DataColumn("Result Detection Limit Unit", typeof(string)));

                    DR["Method Detection Level"] = T.Method_Detection_Level;                 
                    DR["Lower Reporting Limit"] = T.Lower_Reporting_Limit; 
                    DR["Result Detection Limit Unit"] = (T.Result_Detection_Limit_Unit ?? "").TrimEnd('\r', '\n');                                 //RR[0]["Result Detection Limit Unit"] ?? "";

                    DR["Method Speciation"] = (T.Method_Speciation ?? "").TrimEnd('\r', '\n');                     //RR[0]["Method Speciation"] ?? "";

                    if (isNullValue) // nothing measured
                    {
                        DR["Result Detection Condition"] = "Not Reported";
                        DR["Result Qualifier"] = "A";
                    }

                    else if (result < .00001m) // this 'looks like' 0 in floating point math
                    {
                        DR["Result Detection Condition"] = "Not Detected";
                        DR["Result Qualifier"] = "U";                        
                    }

                    else if (T.Method_Detection_Level != null)                                 //RR[0]["Method Detection Level"]))
                    {
                        DL = (decimal)T.Method_Detection_Level;                             // RR[0]["Method Detection Level"];
                        if (T.Lower_Reporting_Limit != null)                                        //   RR[0]["Lower Reporting Limit"]))
                        {
                            RL = (decimal) T.Lower_Reporting_Limit;                                                 //RR[0]["Lower Reporting Limit"];
                        }
                        if ((RL > .00001m) & (DL > .00001m))
                        {
                            if ((RL > result) & (DL < result))
                            {
                                DR["Result Detection Condition"] = ">DL and <RL";
                                DR["Result Qualifier"] = "J";
                            }
                        }       
                    }
                }
                 else   // we did not find values in translation table
                 {
                     string nam = "";
                     if (User.Identity.Name.Length < 3)
                         nam = "Not logged in";
                     else
                         nam = User.Identity.Name;
                     string msg = string.Format("AWQMS translation table failed to find entries for {0}, date {1}", symbol, anaDate);
                     LogError LE = new LogError();
                     LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath,"", nam, "");   
                 }
            

                DTout.Rows.Add(DR); // add to the table
            
        }
        protected void btnSelect4_Click(object sender, EventArgs e)
        {
            bool success = false;
            StartDate = DateTime.Parse(tbStartDate.Text);
            EndDate = DateTime.Parse(tbEndDate.Text); 
            Wbid = tbWBID4.Text.Trim(); 
            if(Wbid.Length < 4)
            {
                tbResults.Text = "Please choose a valid WBID";
                tbWBID4.Focus();
                return;
            }
            success = createReport(StartDate, EndDate, Wbid);
        
        }

        protected void btnSelect6_Click(object sender, EventArgs e)
        {
            bool success = false;
            StartDate = DateTime.Parse(tbStartDate.Text);
            EndDate = DateTime.Parse(tbEndDate.Text);
            Wbid = tbWBID6.Text.Trim();
            if (Wbid.Length < 6)
            {
                tbResults.Text = "Please choose a valid WBID";
                tbWBID4.Focus();
                return;
            }
            success = createReport(StartDate, EndDate, Wbid);
        }

        protected void btnSelect8_Click(object sender, EventArgs e)
        {
            bool success = false;
            StartDate = DateTime.Parse(tbStartDate.Text);
            EndDate = DateTime.Parse(tbEndDate.Text);
            Wbid = tbWBID8.Text.Trim();
            if (Wbid.Length < 8)
            {
                tbResults.Text = "Please choose a valid WBID";
                tbWBID4.Focus();
                return;
            }
            success = createReport(StartDate, EndDate, Wbid);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchWBID4(string prefixText, int count)
        {
            List<string> WBIDs = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "SELECT distinct left([WBID],4) as [WBID] FROM [dbo].[tblWBKey]  where [WBID] like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                               // WBIDs.Add(sdr["OrganizationName"].ToString());
                                WBIDs.Add(sdr["WBID"].ToString());
                            }
                        }
                        conn.Close();
                        return WBIDs;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Web Method Validate Unknowns";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "validateField.aspx", ex.StackTrace.ToString(), nam, "Web Method, no reference to page");
                return WBIDs;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchWBID6(string prefixText, int count)
        {
            List<string> WBIDs = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "SELECT distinct left([WBID],6) as [WBID] FROM [dbo].[tblWBKey]  where [WBID] like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                // WBIDs.Add(sdr["OrganizationName"].ToString());
                                WBIDs.Add(sdr["WBID"].ToString());
                            }
                        }
                        conn.Close();
                        return WBIDs;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Web Method Validate Unknowns";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "validateField.aspx", ex.StackTrace.ToString(), nam, "Web Method, no reference to page");
                return WBIDs;
            }
        }

         [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchWBID8(string prefixText, int count)
        {
            List<string> WBIDs = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "SELECT distinct [WBID] as [WBID] FROM [dbo].[tblWBKey]  where [WBID] like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                // WBIDs.Add(sdr["OrganizationName"].ToString());
                                WBIDs.Add(sdr["WBID"].ToString());
                            }
                        }
                        conn.Close();
                        return WBIDs;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Web Method Validate Unknowns";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "validateField.aspx", ex.StackTrace.ToString(), nam, "Web Method, no reference to page");
                return WBIDs;
            }
        }

         protected void btnDownload_Click(object sender, System.EventArgs e)
         {
             string outFile = ""; 

             lblDownload.Text = "";
             if(Session["OUTFILE"] != null)
             {
                 outFile = (string)Session["OUTFILE"];

             //   outFile = @"\App_Data\AWQMSMetalsandNutrient.02.16.2017.csv"; 

                 try
                 {          
                FileStream LiveFileStream = new FileStream(outFile, FileMode.Open, FileAccess.Read);
                byte[] filebuffer = new byte[(int)LiveFileStream.Length+1];

                int result = LiveFileStream.Read(filebuffer, 0, (int)LiveFileStream.Length); 
                  //  BufferedStream   fileBuffer = new BufferedStream( LiveFileStream, (int)LiveFileStream.Length); 
                 //   LiveFileStream.Read(fileBuffer, 0, (int)LiveFileStream.Length); 
                    LiveFileStream.Close();
                    Response.Clear(); 

                    Response.Charset = "utf-8";
                  //  Response.ContentType = "text/plain";
                    Response.ContentType = "application/x-csv"; 
                    Response.AddHeader("Content-Length", filebuffer.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=\\App_Data\\AWQMSMetalsandNutrient.02.16.2017.csv");
                    Response.BinaryWrite(filebuffer);
                    Response.End();

                 }
                 catch (System.Exception exx)
                 // file IO errors
                 {
                     // we seem to be getting here via aborted thread. Seems OK as the response is closed in code
                     string msg = exx.Message;
                     lblDownload.Text = "";
                 }
             }
         }

         protected void btnSelectAll_Click(object sender, EventArgs e)
         {
             bool success = false;
             StartDate = DateTime.Parse(tbStartDate.Text);
             EndDate = DateTime.Parse(tbEndDate.Text);
             Wbid = "CO";   // this will get all the wbids which all start with CO - for Colorado ?
             success = createReport(StartDate, EndDate, Wbid);
         }    
    }    
}

public static class OurWriter
{
    public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders) 
    {
        if (includeHeaders)
        {
            IEnumerable<String> headerValues = sourceTable.Columns
                .OfType<DataColumn>().Select(column => QuoteValue(column.ColumnName));

            writer.WriteLine(String.Join(",", headerValues));
        }

        IEnumerable<String> items = null;

        foreach (DataRow row in sourceTable.Rows) 
        {
            items = row.ItemArray.Select(o => QuoteValue(o.ToString()));
            writer.WriteLine(String.Join(",", items));
        }

        writer.Flush();
    }

    private static string QuoteValue(string value)
    {
        return String.Concat("\"",
        value.Replace("\"", "\"\""), "\"");
    }
}

//string app_DataDirectory = HttpContext.Current.Server.MapPath("~/App_Data");
//            if (!Directory.Exists(app_DataDirectory))
//            {
//                Directory.CreateDirectory(app_DataDirectory);
//            }
            
//            string logFile = "~/App_Data/ErrorLog." + DateTime.Now.ToString("MM.dd.yyyy") + ".txt";
//            logFile = HttpContext.Current.Server.MapPath(logFile);

//        //using (StreamWriter writer = new StreamWriter("C:\\Temp\\dump.csv")) {
//        //    Rfc4180Writer.WriteDataTable(sourceTable, writer, true);


//    public static string ToCsv(this DataTable dataTable) {
//        StringBuilder sbData = new StringBuilder();

//        // Only return Null if there is no structure.
//        if (dataTable.Columns.Count == 0)
//            return null;

//        foreach (var col in dataTable.Columns) {
//            if (col == null)
//                sbData.Append(",");
//            else
//                sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
//        }

//        sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

//        foreach (DataRow dr in dataTable.Rows) {
//            foreach (var column in dr.ItemArray) {
//                if (column == null)
//                    sbData.Append(",");
//                else
//                    sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
//            }
//            sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);
//        }

//        return sbData.ToString();
//    }

//You call it as follows:

//var csvData = dataTableOject.ToCsv();