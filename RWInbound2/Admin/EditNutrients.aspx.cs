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
using System.Data;
using RWInbound2.Validation;

namespace RWInbound2.Admin
{
    public partial class EditNutrients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["CMD"] != null)
            {
                SqlDataSource1.SelectCommand = (string)Session["CMD"]; 
            }
        }

        protected void btnBarcodeSelected_Click(object sender, EventArgs e)
        {
            string barCode = tbSelectBarcode.Text.Trim();
            Session["BARCODE"] = barCode; 
            string selStr = string.Format("Select * from [Lachat] where [SampleType] like'{0}' ", barCode);
            SqlDataSource1.SelectCommand = selStr;
            Session["CMD"] = selStr; 
            //tbSelectBarcode.Text = "";
            FormView1.Visible = true;
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
                        cmd.CommandText = "select distinct [SampleType] from [Lachat] where [SampleType] like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                barcodes.Add(sdr["SampleType"].ToString());
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

        // here we must update [NutrientData] to reflect the changes
        // call method to update all related data, since we have not validated this value yet
        protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            try
            {
                string userName = "Edit Nutrients";
                if (User.Identity.Name != null)
                    userName = User.Identity.Name;
                UpdateNutrients.Update(userName);   // this should manage updating a row in the nutrientdata table
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

        protected void FormView1_DataBound(object sender, EventArgs e)
        {            
            Button BTN;

            DataRowView row = FormView1.DataItem as DataRowView;
            if (row != null)
            {
                bool? validated = row["Validated"] as bool?;
                if (validated != null)
                {
                    if (validated.Value)
                    {
                        btnDeleteAll.Enabled = false;
                        FormView1.DefaultMode = FormViewMode.ReadOnly;
                        BTN = FormView1.Controls[0].FindControl("EditButton") as Button;
                        if (BTN != null)
                        {
                            BTN.Enabled = false;
                            lblMsg.Visible = true;
                            lblMsg.Text = "This value has been validated and can not be changed!";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        btnDeleteAll.Enabled = true; 
                        FormView1.DefaultMode = FormViewMode.Edit;
                        BTN = FormView1.Controls[0].FindControl("EditButton") as Button;
                        if (BTN != null)
                        {
                            BTN.Enabled = true;
                            lblMsg.Visible = false;
                        }
                    }
                }
            }
        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {
            string msg = "Msg"; 
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string barcode = ""; 
            if(Session["BARCODE"] != null)
            {
                try
                {
                    barcode = (string)Session["BARCODE"];
                    using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = string.Format("UPDATE [Lachat] SET [Valid] = 0 where [SampleType] = {0} and [Validated] = 0", barcode);
                            cmd.Connection = conn;
                            conn.Open();
                            cmd.ExecuteNonQuery();

                            // now handle NutrientData
                            cmd.CommandText = string.Format("UPDATE [NutrientData] SET [Valid] = 0 where [BARCODE] = {0} and [Validated] = 0", barcode);
                            cmd.ExecuteNonQuery();
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

        // single row deleted - marked valid = 0
        // must call update nutrients to update that table, perhaps setting a value to null or empty 
        protected void SqlDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            try
            {
                string userName = "Edit Nutrients";
                if (User.Identity.Name != null)
                    userName = User.Identity.Name;
                UpdateNutrients.Update(userName);   // this should manage updating a row in the nutrientdata table
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
}