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
    public partial class EditMetalBarCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int role = 1;

            if (Session["Role"] != null)
            {
                role = (int)Session["Role"];    // get users role
            }

            RiverWatchEntities RWE = new RiverWatchEntities();

            var R = from r in RWE.ControlPermissions
                    where r.PageName.ToUpper() == "EditMetalBarCode"
                    select r;

            int? Q = (from r in R
                      where r.ControlID.ToUpper() == "Page"
                      select r.RoleValue).FirstOrDefault();

            if (Q != null)
            {
                if (role < Q.Value)
                    Response.Redirect("~/index.aspx");
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }

            // check to see if there is a barcode in the request

            if(Request.QueryString.Count > 0)
            {
                string bc = Request.QueryString["BARCODE"]; 
                string selStr = string.Format("Select * from [metalbarcode] where [labid] like '{0}'", bc);
                SqlDataSource1.SelectCommand = selStr;
                FormView1.DataBind(); 
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
                        cmd.CommandText = "select LabID from MetalBarCode where LabID like @SearchText + '%'";
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

        protected void btnSelectBarCode_Click(object sender, EventArgs e)
        {
            string barCode = tbSelectBarcode.Text.Trim();
            string selStr = string.Format("Select * from [metalbarcode] where [labid] like '{0}'", barCode);
            SqlDataSource1.SelectCommand = selStr; 
        }

        protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            string user = User.Identity.Name;
            e.Command.Parameters["@UserCreated"].Value = user;
            e.Command.Parameters["@DateCreated"].Value = DateTime.Now;
         //   e.Command.Parameters["@Valid"].Value = 1;
        }
    }
}