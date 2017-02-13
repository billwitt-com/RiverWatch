using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Web.UI;
using System.Web.UI.WebControls;
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
          //  decimal? result;
            decimal? result; 
            string symbol = "";

         //   bool updateRowResult = false;

            // first, read translation table into in-memory data table to avoid a second db query for each line.
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; //GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format(" select * from [dbo].[tlkAQWMStranslation] where valid = 1");
                        cmd.Connection = conn;
                        conn.Open();
 
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                DTlookup.Load(sdr);
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
                return false; 
            }

            

            if(DTlookup.Rows.Count < 20)
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
            DTout.Columns.Add(new DataColumn("Result Detection Limit Value", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Detection Limit Unit", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Detection Limit Type", typeof(string)));
            DTout.Columns.Add(new DataColumn("Method Speciation", typeof(string)));
            DTout.Columns.Add(new DataColumn("Activity Media Subdivision Name", typeof(string)));   // , "Surface Water"
            DTout.Columns.Add(new DataColumn("Analysis Start Date", typeof(string)));   // MM/DD/YYYY
            DTout.Columns.Add(new DataColumn("Analysis Start Time", typeof(string)));   // HHMM
            DTout.Columns.Add(new DataColumn("Analysis Start Time Zone", typeof(string)));      // , "MST"


            var R = from r in RWE.viewAWQMSDatas
                    where r.Valid == true & r.WaterBodyID.StartsWith(WBID) & r.SampleDate >= startDate & r.SampleDate <= endDate & !r.TypeCode.StartsWith("1") & !r.TypeCode.StartsWith("2")
                    orderby r.SampleDate descending
                    select r ;

            foreach(viewAWQMSData r in R)
            {
                    symbol = "AL_D";
                    result = r.AL_D;                  
                    calculateRow( r, result, symbol );
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "AL_T";
                    result = r.AL_T;
                    calculateRow(r, result, symbol);

                    symbol = "AS_D";
                    result = r.AS_D; 
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "AS_T";
                    result = r.AS_T;
                    calculateRow(r, result, symbol);

                    symbol = "CA_D";
                    result = r.CA_D; 
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "CA_T";
                    result = r.CA_T;
                    calculateRow(r, result, symbol);

                    symbol = "CD_D";
                    result = r.CD_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "CD_T";
                    result = r.CD_T;
                    calculateRow(r, result, symbol);

                    symbol = "CU_D";
                    result = r.CU_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "CU_T";
                    result = r.CU_T;
                    calculateRow(r, result, symbol);

                    symbol = "FE_D";
                    result = r.FE_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "FE_T";
                    result = r.FE_T;
                    calculateRow(r, result, symbol);

                    symbol = "K_D";
                    result = r.K_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "K_T";
                    result = r.K_T;
                    calculateRow(r, result, symbol);

                    symbol = "MG_D";
                    result = r.MG_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "MG_T";
                    result = r.MG_T;
                    calculateRow(r, result, symbol);

                    symbol = "MN_D";
                    result = r.MN_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "MN_T";
                    result = r.MN_T;
                    calculateRow(r, result, symbol);

                    symbol = "NA_D";
                    result = r.NA_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "NA_T";
                    result = r.NA_T;
                    calculateRow(r, result, symbol);

                    symbol = "PB_D";
                    result = r.PB_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "PB_T";
                    result = r.PB_T;
                    calculateRow(r, result, symbol);

                    symbol = "SE_D";
                    result = r.SE_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "SE_T";
                    result = r.SE_T;
                    calculateRow(r, result, symbol);

                    symbol = "ZN_D";
                    result = r.ZN_D;
                    calculateRow(r, result, symbol);
                    // could write row to file now, to take advantage of file buffering.                

                    symbol = "ZN_T";
                    result = r.ZN_T;
                    calculateRow(r, result, symbol);

                    // could write row to file now, to take advantage of file buffering.                

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

                // FIELD DATA - MAY NEED TO MOVE IT TO SEPARATE REPORT

                    //symbol = "PHEN_ALK";
                    //result = (decimal)r.PHEN_ALK.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "TempC";
                    //result = (decimal)r.TempC.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "TOTAL_ALK";
                    //result = (decimal)r.TOTAL_ALK.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "TOTAL_HARD";
                    //result = (decimal)r.TOTAL_HARD.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "DO_MGL";
                    //result = (decimal)r.DO_MGL.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "DO_MGL";
                    //result = (decimal)r.DO_MGL.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "USGS_Flow";
                    //result = (decimal)r.USGS_Flow.Value;
                    //calculateRow(r, result, symbol);

                    //symbol = "DOSAT";
                    //result = (decimal)r.DOSAT.Value;
                    //calculateRow(r, result, symbol);




                // and do the rest... 
            }


            // when we get here, we have completed data table, ready to write out to file... 
            // may consider writing each new row... May be slower but... 

            string app_DataDirectory = HttpContext.Current.Server.MapPath("~/App_Data");
            if (!Directory.Exists(app_DataDirectory))
            {
                Directory.CreateDirectory(app_DataDirectory);
            }

            string outFile = "~/App_Data/AWQMSMetalsandNutrient." + DateTime.Now.ToString("MM.dd.yyyy") + ".csv";
            outFile = HttpContext.Current.Server.MapPath(outFile);

            FileInfo FI = new FileInfo(outFile);    // delete if exists
            if (FI.Exists)
                FI.Delete();
            Session["OUTFILE"] = outFile; 
            
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                Rfc4180Writer.WriteDataTable(DTout, writer, true);
                //using (StreamWriter writer = new StreamWriter("C:\\Temp\\dump.csv")) {
                //    Rfc4180Writer.WriteDataTable(sourceTable, writer, true);
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
                    DR["Analysis Start Time"] = r.SampleDate.Value.Hour.ToString() + r.SampleDate.Value.Minute.ToString();
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
                //  [LocalName] ,[CName] ,[ResultUnit] ,[ResultFraction] ,[AnaMethodID] ,[AnaMethodContext] ,[DetectionLevel] 
                // ,[LowerReportingLimit] ,[DetectionUnit]  ,[MethodSpec]  ,[StartDate] ,[EndDate]

                    DataRow[] RR = null ; 
                try
                {
                  //  selStr = string.Format("StartDate >= #{0}#", anaDate);       // and EndDate < #{0}# and LocalName = '{1}'", anaDate, "AL_D" );
                    selStr = string.Format("LocalName = '{0}'", symbol); 
                    RR = DTlookup.Select(selStr); 
                }
                catch(Exception ex)
                {
                    string whattodo = "I don't know"; 
                }

                if(RR != null)
                {
                    DR["Characteristic Name"] = RR[0]["CName"];
                    DR["Result Sample Fraction"] = RR[0]["ResultFraction"] ?? "";
                    DR["Result Unit"] = RR[0]["ResultUnit"] ?? "";

                    DR["Result Analytical Method ID"] = RR[0]["AnaMethodID"] ?? "";
                    DR["Result Analytical Method Context"] = RR[0]["AnaMethodContext"] ?? "";
                    DR["Result Detection Limit Value"] = RR[0]["DetectionLevel"] ?? ""; 
                    DR["Result Detection Limit Unit"] = RR[0]["DetectionUnit"] ?? "";
                    DR["Result Detection Limit Type"] = RR[0]["LowerReportingLimit"] ?? "";

                    DR["Method Speciation"] = RR[0]["MethodSpec"] ?? "";

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

                    else if (!DBNull.Value.Equals(RR[0]["DetectionLevel"]))
                    {
                        DL = (decimal)RR[0]["DetectionLevel"];
                        if (!DBNull.Value.Equals(RR[0]["LowerReportingLimit"]))
                        {
                            RL = (decimal)RR[0]["LowerReportingLimit"];
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
            

                DTout.Rows.Add(DR); // add to the table
            
        }
        protected void btnSelect4_Click(object sender, EventArgs e)
        {
            string msg = "";
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
         //   msg = string.Format("You have chosen the AWQMS Chemical report starting at {0} through {1} for WBID(4) {2}", StartDate.ToShortDateString(), EndDate.ToShortDateString(), Wbid);
            msg = string.Format ("Reports returned {0}", success);
            tbResults.Text = msg; 
        }

        protected void btnSelect6_Click(object sender, EventArgs e)
        {
            StartDate = DateTime.Parse(tbStartDate.Text);
            EndDate = DateTime.Parse(tbEndDate.Text);
            Wbid = tbWBID6.Text.Trim();
            if (Wbid.Length < 6)
            {
                tbResults.Text = "Please choose a valid WBID";
                tbWBID4.Focus();
                return;
            }
            string msg = string.Format("You have chosen the AWQMS Chemical report starting at {0} through {1} for WBID(6) {2}", StartDate.ToShortDateString(), EndDate.ToShortDateString(), Wbid);
            tbResults.Text = msg; 
        }

        protected void btnSelect8_Click(object sender, EventArgs e)
        {
            StartDate = DateTime.Parse(tbStartDate.Text);
            EndDate = DateTime.Parse(tbEndDate.Text);
            Wbid = tbWBID8.Text.Trim();
            if (Wbid.Length < 8)
            {
                tbResults.Text = "Please choose a valid WBID";
                tbWBID4.Focus();
                return;
            }
            string msg = string.Format("You have chosen the AWQMS Chemical report starting at {0} through {1} for WBID(8) {2}", StartDate.ToShortDateString(), EndDate.ToShortDateString(), Wbid);
            tbResults.Text = msg; 
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
             string mappedPath = ""; 
             if(Session["OUTFILE"] != null)
             {
                 outFile = (string)Session["OUTFILE"];

                 try
                 {
                     // set the http content type 
                     Response.ContentType = "application/x-csv"; 

                     // initialize the http content-disposition header to
                     // indicate a file attachment with the default filename
                     // "myFile.txt"
                     System.IO.FileInfo fileToDownload = new System.IO.FileInfo(outFile);
                     mappedPath = fileToDownload.Name;
                     System.String disHeader = "Attachment; Filename=\"" + mappedPath + "\"";
                     Response.AppendHeader("Content-Disposition", disHeader);

                     // transfer the file byte-by-byte to the response object
                                  //"C:\\downloadJSP\\DownloadConv\\myFile.txt");
                     Response.Flush();
                     Response.WriteFile(fileToDownload.FullName);
                 }
                 catch (System.Exception exx)
                 // file IO errors
                 {
                     string msg = exx.Message; 
                 }



                 // FileUpload1.PostedFile.ContentType == "application/x-csv"
                
             }
         }
    }
    
}


                        // result = 0.00 - qualifier = U and detection condition ="Not Detected"
                        // blank or null results, qualifier =A and detection condition -"Not Reported"
                       // between DL and RL and result is a number vs 0, qualifier-= J and detection condition = >DL and <RL

// list of elements in lookup table 
//  [LocalName] ,[CName] ,[ResultUnit] ,[ResultFraction] ,[AnaMethodID] ,[AnaMethodContext] ,[DetectionLevel] 
// ,[LowerReportingLimit] ,[DetectionUnit]  ,[MethodSpec]  ,[StartDate] ,[EndDate]
// now, read in a line of data at a time, and if value not null, put it in a new row, and then to the DTout table 

public static class Rfc4180Writer
{
    public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders) 
    {
        if (includeHeaders) {
            IEnumerable<String> headerValues = sourceTable.Columns
                .OfType<DataColumn>()
                .Select(column => QuoteValue(column.ColumnName));
                
            writer.WriteLine(String.Join(",", headerValues));
        }

        IEnumerable<String> items = null;

        foreach (DataRow row in sourceTable.Rows) {
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