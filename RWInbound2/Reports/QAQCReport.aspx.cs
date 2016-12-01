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
using Microsoft.Reporting.WebForms;

namespace RWInbound2.Reports
{
    public partial class QAQCReport : System.Web.UI.Page
    {
        public DataSet DS = new DataSet("QAQCDataSet");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                ReportViewer1.Visible = false;
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]

        public static List<string> SearchOrgs(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; // GlobalSite.RiverWatchDev;

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from Organization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

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
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return customers;
            }
        }

        protected void btnOrgName_Click(object sender, EventArgs e)
        {
            string orgName = tbOrgName.Text.Trim();
            lblOrgNameMsg.Visible = false; // set it so it never shows.. unless
            lblKitNumMsg.Visible = false;
            lblStnNumMsg.Visible = false;

            if(orgName.Length < 2)
            {
                lblOrgNameMsg.Text = "Please choose a valid Org Name!";
                lblOrgNameMsg.Visible = true;
                lblOrgNameMsg.BackColor = System.Drawing.Color.Red;
            }

            string cmdStr = string.Format("Select [TypeCode] ,[SampleNumber], [AL_D] ,[AL_T] ,[AS_D] ,[AS_T] ,[CA_D] ,[CA_T] ,[CD_D] ,[CD_T] ,[CU_D] " +
            " ,[CU_T] ,[FE_D] ,[FE_T] ,[MG_D] ,[MG_T] ,[MN_D] ,[MN_T] ,[PB_D] ,[PB_T] ,[SE_D] ,[SE_T] ,[ZN_D] ,[ZN_T] ,[NA_D] ,[NA_T] ,[K_D],[K_T] " +
            " from [NEWexpWater] where valid = 1 " +
            " and [OrganizationName] like '{0}' and typecode in ('20', '00', '10') order by [SampleNumber], [TypeCode] desc ", orgName);

            buildReport(cmdStr); 
        }

        // fill data table and do math...
        protected void buildReport(string cmdString)
        {
            DataTable DT = new DataTable();
            //double N = 0;
            //double D = 0;
            //double R = 0;

            decimal N = 0;
            decimal D = 0;
            decimal R = 0;

            using(SqlCommand cmd = new SqlCommand())
            {
                using(SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;
                    cmd.Connection = conn;
                    conn.Open(); 
                    cmd.CommandText = cmdString ;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    DT.Load(rdr);
                }
            }

            if(DT.Rows.Count > 0)
            {
                DataTable RES = DT.Clone(); // make an empty copy

                // walk the origional table looking for at [TypeCode] column
                int c = DT.Rows.Count;


                for (int x = 0; x < c - 1; x++ )
                {
                    var X = DT.Rows[x]["TypeCode"]; 
                    if(X != null)
                    {
                        // **************
                        if((string)X == "20")    // this is a dup - could be a '10' after this, or not
                        {
                            var Y = DT.Rows[x]["SampleNumber"]; 
                            if((string)Y != null)
                            {
                                var Z = DT.Rows[x+1]["SampleNumber"]; // look at next row to see if it is same sample

                                if(Z != null)
                                {
                                    if((string)Y == (string)Z)  // same sample numbers
                                    {
                                        var Q = DT.Rows[x + 1]["TypeCode"]; 
                                        if(Q != null)                                        
                                        {          
                                            if ((string)X == "10")   // check for 'blank' first, as '10' follows '20' 
                                                // if this is a blank, put in Results table and inc x to see if next is normal
                                            {
                                                DataRow D1 = DT.Rows[x];
                                                             

                                                // round values so they present better

                                                for (int y = 2; y < 28; y++)
                                                {
                                                    try
                                                    {
                                                        if (D1[y] != null)
                                                        {
                                                            D1[y] = Math.Round((decimal)D1[y], 2);
                                                        }
           
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string msg = ex.Message;
                                                    }
                                                }

                                                // put them in the results table and inc the counter
                                                RES.ImportRow(D1);
                                                x++;
                                            }                                                                
                                        }
                                    }
                                }
                            }
                        }





                                            // **************
                                            if((string)Q == "00")   // we have a normal for the next row
                                            {

                                                DataRow D1 = DT.Rows[x];
                                                DataRow D2 = DT.Rows[x + 1]; 

                                                // round values so they present better

                                                for (int y = 2; y < 28; y++)
                                                {
                                                    try
                                                    {
                                                        if (D1[y] != null)
                                                        {
                                                            D1[y] = Math.Round((decimal)D1[y], 2);
                                                        }
                                                        if (D2[y] != null)
                                                        {
                                                            D2[y] = Math.Round((decimal)D2[y], 2);
                                                        }
                                                    }
                                                    catch(Exception ex)
                                                    {
                                                        string msg = ex.Message; 
                                                    }
                                                
                                                }

                                                RES.ImportRow(D1);
                                                RES.ImportRow(D2); 

                                                DataRow DR = DT.Rows[x];
                                                DR["TypeCode"] = "%R";

                                                for (int y = 2; y < 28; y++)
                                                {
                                                    if (DT.Rows[x][y] != null)
                                                    {
                                                        D = (decimal)DT.Rows[x][y];                                                             
                                                        if (DT.Rows[x + 1][y] != null)
                                                        {
                                                            N = (decimal)DT.Rows[x + 1][y];         // normal row, one after dup
                                                            R = (N - D) ;
                                                            if (D > .0001m)
                                                            {
                                                                R = R / D;              // R / N;
                                                                R = R * 100;
                                                                DR[y] = Math.Round(100 - R, 2);
                                                            }
                                                        }
                                                    }
                                                }
                                                RES.ImportRow(DR);
                                                x++; 
                                            }
                                        }
                                    }
                                }
                            }
                        }  
                    }                   
                

                if (RES.Rows.Count > 0)
                {
                    ReportViewer1.Visible = true;
                    RES.TableName = "QAQCReport";
                    ReportDataSource rds = new ReportDataSource("DataSet1", RES);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    ReportViewer1.Visible = false;
                }


                //GridView1.DataSource = RES;
                //GridView1.DataBind(); 
                //int count = DT.Rows.Count;

              
                //DS.Tables.Add(DT);
            }
        }

        protected void btnKitNumber_Click(object sender, EventArgs e)
        {
            int kitNumber = 0;
            bool isValidKitNumber = false;
            string kn = tbKitNumber.Text.Trim();
            lblOrgNameMsg.Visible = false; // set it so it never shows.. unless
            lblKitNumMsg.Visible = false;
            lblStnNumMsg.Visible = false;

            if(kn.Length < 1)
            {
                lblKitNumMsg.Text = "Please choose a valid kit number";
                lblKitNumMsg.Visible = true;
                lblKitNumMsg.BackColor = System.Drawing.Color.Red;
                return; 
            }
            bool success = int.TryParse(kn, out kitNumber);
            if(!success)
            {
                lblKitNumMsg.Text = "Please choose a valid kit number";
                lblKitNumMsg.Visible = true;
                lblKitNumMsg.BackColor = System.Drawing.Color.Red;
                return; 
            }


            string cmdStr = string.Format("Select [TypeCode] ,[SampleNumber], [AL_D] ,[AL_T] ,[AS_D] ,[AS_T] ,[CA_D] ,[CA_T] ,[CD_D] ,[CD_T] ,[CU_D] " +
           " ,[CU_T] ,[FE_D] ,[FE_T] ,[MG_D] ,[MG_T] ,[MN_D] ,[MN_T] ,[PB_D] ,[PB_T] ,[SE_D] ,[SE_T] ,[ZN_D] ,[ZN_T] ,[NA_D] ,[NA_T] ,[K_D],[K_T] " +
           " from [NEWexpWater] where valid = 1 " +
           " and [KitNumber] = {0} and typecode in ('20', '00', '10') order by [SampleNumber], [TypeCode] desc ", kitNumber);

            // check to see if there is as kit number like this

            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = cmdStr;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    isValidKitNumber = rdr.HasRows; 
                }
            }
            if (isValidKitNumber)
            {
                buildReport(cmdStr);
            }
            else
            {
                lblKitNumMsg.Text = string.Format("There are no results for {0}", kitNumber);
                lblKitNumMsg.Visible = true;
                lblKitNumMsg.BackColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void btnStnNumber_Click(object sender, EventArgs e)
        {
            int stnNumber = 0;
            bool isValidStnNumber = false;
            string sn = tbStnNumber.Text.Trim();
            lblOrgNameMsg.Visible = false; // set it so it never shows.. unless
            lblKitNumMsg.Visible = false;
            lblStnNumMsg.Visible = false;

            if (sn.Length < 1)
            {
                lblStnNumMsg.Text = "Please choose a valid station number";
                lblStnNumMsg.Visible = true;
                lblStnNumMsg.BackColor = System.Drawing.Color.Red;
                return;
            }
            bool success = int.TryParse(sn, out stnNumber);
            if (!success)
            {
                lblStnNumMsg.Text = "Please choose a valid station number";
                lblStnNumMsg.Visible = true;
                lblStnNumMsg.BackColor = System.Drawing.Color.Red;
                return;
            }


            string cmdStr = string.Format("Select [TypeCode] ,[SampleNumber], [AL_D] ,[AL_T] ,[AS_D] ,[AS_T] ,[CA_D] ,[CA_T] ,[CD_D] ,[CD_T] ,[CU_D] " +
           " ,[CU_T] ,[FE_D] ,[FE_T] ,[MG_D] ,[MG_T] ,[MN_D] ,[MN_T] ,[PB_D] ,[PB_T] ,[SE_D] ,[SE_T] ,[ZN_D] ,[ZN_T] ,[NA_D] ,[NA_T] ,[K_D],[K_T] " +
           " from [NEWexpWater] where ([TypeCode] = '20' or [TypeCode] = '00') and valid = 1 " +
           " and [StationNumber] = {0} and typecode in ('20', '00', '10') order by [SampleNumber], [TypeCode] desc ", stnNumber);

            // check to see if there is as kit number like this

            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = cmdStr;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    isValidStnNumber = rdr.HasRows;
                }
            }
            if (isValidStnNumber)
            {
                buildReport(cmdStr);
            }
            else
            {
                lblStnNumMsg.Text = string.Format("There are no results for Station Number {0}", stnNumber);
                lblStnNumMsg.Visible = true;
                lblStnNumMsg.BackColor = System.Drawing.Color.Red;
                return;
            }
        }

    }
}