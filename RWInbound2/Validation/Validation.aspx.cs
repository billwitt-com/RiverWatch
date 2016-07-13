using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Validation
{
    public partial class Validation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int dupCount = 0;
            int blankCount = 0;
            int normalCount = 0;
            btnICPDups.Enabled = false;           

            // FIRST, move any samples from incomingICP to new tables using sproc 
            // XXXX will not need this when we change fort collins
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection())
            //    {
            //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
            //        using (SqlCommand cmd = new SqlCommand())
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            SqlParameter userName = cmd.Parameters.Add("@User", SqlDbType.NVarChar, 90);
            //            userName.Direction = ParameterDirection.Input;
            //            userName.Value = User.Identity.Name;            //"Bill for Now";
            //            cmd.CommandText = "[UpdateLocalTablesFromIncomingICP]"; // name of the sproc 
            //            cmd.Connection = conn;
            //            conn.Open();
            //            int rowsAffected = cmd.ExecuteNonQuery(); // not accurate as there are two updates so only result of second will show up here... 
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string nam = "";
            //    if (User.Identity.Name.Length < 3)
            //        nam = "Not logged in";
            //    else
            //        nam = User.Identity.Name;
            //    string msg = ex.Message;
            //    LogError LE = new LogError();
            //    LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            //}     

            try
            {
                using (SqlDataSource S = new SqlDataSource())
                {
                    DataSourceSelectArguments args = new DataSourceSelectArguments();
                    S.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
                    S.SelectCommand = "SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where left( DUPLICATE, 1) = '1' and valid = 1 and saved = 0";
                    System.Data.DataView result = (DataView)S.Select(args);
                    blankCount = result.Table.Rows.Count;

                    S.SelectCommand = "SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where left( DUPLICATE, 1) = '2' and valid = 1 and saved = 0";
                    result = (DataView)S.Select(args);
                    dupCount = result.Table.Rows.Count;

                    S.SelectCommand = "SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where left( DUPLICATE, 1) = '0' and valid = 1 and saved = 0";
                    result = (DataView)S.Select(args);
                    normalCount = result.Table.Rows.Count;

                    if (blankCount == 0)
                    {
                        lblICPBlanks.Text = "There are no Blanks to validate";
                        btnICPBlanks.Enabled = false;
                        btnICPBlanks.BackColor = System.Drawing.Color.Maroon;
                    }
                    else
                    {
                        lblICPBlanks.Text = string.Format("There are {0} Blank samples", blankCount);
                        btnICPBlanks.Enabled = true;
                        btnICPBlanks.BackColor = System.Drawing.Color.LightCyan;
                    }

                    if (dupCount == 0)
                    {
                        lblICPDups.Text = "There are no Duplicates to validate";

                        btnICPDups.Enabled = false;
                        btnICPDups.BackColor = System.Drawing.Color.Maroon;
                    }
                    else
                    {
                        lblICPDups.Text = string.Format("There are {0} Duplicate samples", dupCount);
                        btnICPDups.Enabled = true;
                        btnICPDups.BackColor = System.Drawing.Color.LightCyan;
                    }

                    if (normalCount == 0)
                    {
                        lblICPSamples.Text = "There are no Normals to validate";
                        btnICPSamples.Enabled = false;
                        btnICPSamples.BackColor = System.Drawing.Color.Maroon;
                    }
                    else
                    {
                        lblICPSamples.Text = string.Format("There are {0} Normal samples", normalCount);
                        btnICPSamples.Enabled = true;
                        btnICPSamples.BackColor = System.Drawing.Color.LightCyan;
                    }
                }
            }

            catch (Exception ex)
            {       
                string name = "";
                if (User.Identity.Name.Length < 3)
                    name = "Not logged in";
                else
                    name = User.Identity.Name;
                string msg = ex.Message; 
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), name, "");
            }     
        }

        protected void btnICPBlanks_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Validation/ValidateBlanks.aspx"); 
        }

        protected void btnICPDups_Click(object sender, EventArgs e)
        {
             Response.Redirect("~/Validation/ValidateDups.aspx"); 
        }
        
        protected void btnICPSamples_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/ValidateNormals.aspx");
        }
    }
}