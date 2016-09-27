
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
            int nutrientCount = 0;
            int nutrientDupCount = 0;
            int lachatNotRecorded = 0;
            btnICPDups.Enabled = false;
            bool allowed = false;

            // public static bool Test(string pageName, string controlName)
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if(!allowed)
                Response.Redirect("~/index.aspx"); 

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

            // update nutrientData table so we can count and also so we can use it later

            try
            {
                UpdateNutrients.Update(User.Identity.Name); // static class.. process any new lachat input before we get going.. 
                // just added & c.Validated == false to query below
                RiverWatchEntities RWE = new RiverWatchEntities();
                var C = from c in RWE.NutrientDatas
                        where c.Valid == true & c.TypeCode.Contains("05") & c.Validated == false
                        select c;
                if(C.Count() > 0)
                {
                    nutrientCount = C.Count(); 
                }

                var C1 = from c1 in RWE.NutrientDatas
                         where c1.Valid == true & c1.TypeCode.Contains("25") & c1.Validated == false
                        select c1;
                if (C1.Count() > 0)
                {
                    nutrientDupCount = C1.Count();
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

            try
            {
                using (SqlDataSource S = new SqlDataSource()) 
                {
                    DataSourceSelectArguments args = new DataSourceSelectArguments();
                    S.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  //GlobalSite.RiverWatchDev;
                    S.SelectCommand = "SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where left( DUPLICATE, 1) = '1' and valid = 1 and saved = 0";
                    System.Data.DataView result = (DataView)S.Select(args);
                    blankCount = result.Table.Rows.Count;

                    S.SelectCommand = "SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where left( DUPLICATE, 1) = '2' and valid = 1 and saved = 0";
                    result = (DataView)S.Select(args);
                    dupCount = result.Table.Rows.Count;

                    S.SelectCommand = "SELECT * FROM [Riverwatch].[dbo].[InboundICPFinal] where left( DUPLICATE, 1) = '0' and valid = 1 and saved = 0";
                    result = (DataView)S.Select(args);
                    normalCount = result.Table.Rows.Count;

                    // SELECT * FROM [LachatBCnotEntered]

                    S.SelectCommand = "SELECT * FROM [LachatBCnotEntered]";
                    result = (DataView)S.Select(args);
                    lachatNotRecorded = result.Table.Rows.Count;

                    // run view to see if there are any barcodes in lachat that are not in nutrientbarcode table, ie they require attention. 
                     
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

                    // now list Lachat samples 
                    if (nutrientCount > 0)
                    {
                        lblLachet.Text = string.Format("There are {0} Nutrient samples", nutrientCount);
                        
                    }
                    else
                    {
                        lblLachet.Text = "There are no Nutrient Samples to Validate";
                    }

                     // now list Lachat samples 
                    if (nutrientDupCount > 0)
                    {

                        lblLachatDups.Text = string.Format("There are {0} Duplicate Nutrient samples", nutrientDupCount);
                    }
                    else
                    {
                        lblLachatDups.Text = "There are no Duplicate Nutrient Samples to Validate";
                    }

                    if(lachatNotRecorded > 0)
                    {
                        lblLachatMessage.Text = string.Format("NOTE: There are {0} unrecorded Lachat barcodes", lachatNotRecorded);
                        lblLachatMessage.ForeColor = System.Drawing.Color.Red;
                        lblLachatMessage.Visible = true;
                    }
                    else
                    {
                        lblLachatMessage.Text = "";
                        lblLachatMessage.Visible = false;
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

        protected void btnLachet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/ValidateNutrients.aspx");
        }

        protected void btnLachatDups_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/ValidateDUPNutrients.aspx");
        }
    }
}