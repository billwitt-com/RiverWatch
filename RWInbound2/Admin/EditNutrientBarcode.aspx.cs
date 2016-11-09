using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Providers.Entities;

namespace RWInbound2.Admin
{
    public partial class EditNutrientBarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FormView1.Visible = false; 
            }
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static List<string> SearchBarcodes(string prefixText, int count)
        {
            List<string> barcodes = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select LabID from [NutrientBarCode] where LabID like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                barcodes.Add(sdr["LabID"].ToString());
                            }
                        }
                        conn.Close();
                        return barcodes;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return barcodes;
            }
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static List<string> SearchSampleNumber(string prefixText, int count)
        {
            List<string> barcodes = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select [SampleNumber] from [NutrientBarCode] where [SampleNumber] like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                barcodes.Add(sdr["LabID"].ToString());
                            }
                        }
                        conn.Close();
                        return barcodes;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return barcodes;
            }
        }

        protected void btnSelectBarcode_Click(object sender, EventArgs e)
        {
            string barCode = tbSelectBarcode.Text.Trim();
            string selStr = string.Format("Select * from [NutrientBarCode] where [labid] like '{0}'", barCode);
            SqlDataSource1.SelectCommand = selStr;
            tbSelectSampleNumber.Text = "";
            tbSelectBarcode.Text = "";
            FormView1.Visible = true;
        }

        protected void btnSelectSampleNumber_Click(object sender, EventArgs e)
        {
            string sampleNumber = tbSelectSampleNumber.Text.Trim();
            string selStr = string.Format("Select * from [NutrientBarCode] where [SampleNumber] like '{0}'", sampleNumber);
            SqlDataSource1.SelectCommand = selStr;
            tbSelectBarcode.Text = "";
            tbSelectSampleNumber.Text = ""; 
            FormView1.Visible = true;
        }
    }
}