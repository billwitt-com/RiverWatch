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


namespace RWInbound2.Applications
{
    public partial class AWQMSChems : System.Web.UI.Page
    {
        DateTime StartDate;
        DateTime EndDate;
        string Wbid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string herewego = "Here we go again"; 
            if(!IsPostBack)
            {
                tbStartDate.Text = DateTime.Now.ToShortDateString();    // preload to some value... 
                tbEndDate.Text = DateTime.Now.ToShortDateString();
            }

        }

        // create report

        protected bool createReport(DateTime startDate, DateTime endDate, string WBID)
        {
            DataTable DT = new DataTable();
            // first, read translation table into data table to avoid a db query for each line.
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
                        // SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                DT.Load(sdr);
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

            // now create data table for output data, make them all strings since we will do no changes, just write to CSV file. 

            DataTable DTout = new DataTable(); 
            DTout.Columns.Add(new DataColumn("Monitoring Location ID", typeof(string)));    // station number
            DTout.Columns.Add(new DataColumn("Project ID", typeof(string),"1"));
            DTout.Columns.Add(new DataColumn("Activity ID", typeof(string)));   // our event code
            DTout.Columns.Add(new DataColumn("Activity Type", typeof(string), "Sample-Routine"));
            DTout.Columns.Add(new DataColumn("Sample Collection Method ID", typeof(string), "SM 1060B"));
            DTout.Columns.Add(new DataColumn("Equipment ID", typeof(string), "Water Bottle"));
            DTout.Columns.Add(new DataColumn("Activity Start Date", typeof(string)));   // MM/DD/YYYY
            DTout.Columns.Add(new DataColumn("Activity Start Time", typeof(string)));   //HHMM
            DTout.Columns.Add(new DataColumn("Activity Start Time Zone", typeof(string), "MST"));
            DTout.Columns.Add(new DataColumn("Activity Media Name", typeof(string), "Water"));
            DTout.Columns.Add(new DataColumn("Characteristic Name", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Sample Fraction", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Value", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Unit", typeof(string)));
            DTout.Columns.Add(new DataColumn("Result Value Type", typeof(string), "Actual"));
            DTout.Columns.Add(new DataColumn("Result Status ID", typeof(string), "Validated"));
            DTout.Columns.Add(new DataColumn("Result Analytical Method ID", typeof(string)));
            DTout.Columns.Add(new DataColumn("AResult Analytical Method Context", typeof(string)));
            DTout.Columns.Add(new DataColumn("Method Speciation", typeof(string)));
            DTout.Columns.Add(new DataColumn("Activity Media Subdivision Name", typeof(string), "Surface Water"));
            DTout.Columns.Add(new DataColumn("Analysis Start Date", typeof(string)));   // MM/DD/YYYY
            DTout.Columns.Add(new DataColumn("Analysis Start Time", typeof(string)));   // HHMM
            DTout.Columns.Add(new DataColumn("Analysis Start Time Zone", typeof(string), "MST"));








            return true; 
        }


        protected void btnSelect4_Click(object sender, EventArgs e)
        {
            string msg = ""; 
            StartDate = DateTime.Parse(tbStartDate.Text);
            EndDate = DateTime.Parse(tbEndDate.Text); 
            Wbid = tbWBID4.Text.Trim(); 
            if(Wbid.Length < 4)
            {
                tbResults.Text = "Please choose a valid WBID";
                tbWBID4.Focus();
                return;
            }
            msg = string.Format("You have chosen the AWQMS Chemical report starting at {0} through {1} for WBID(4) {2}", StartDate.ToShortDateString(), EndDate.ToShortDateString(), Wbid );
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
    }
    
}