
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
            int unknownCount = 0;
            int FieldNotRecorded = 0;
            int FieldCount = 0;
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
                //string name = "";
                //if (User.Identity.Name.Length < 3)
                //    name = "Not logged in";
                //else
                //    name = User.Identity.Name;
                //string msg = string.Format("Starting Nutrients Udate process at {0}",DateTime.Now);
                //LogError LE = new LogError();
                //LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath,"" , name, "Profiling");

                // moved from here to upload lachat XXXX
// XXXX   
                UpdateNutrients.Update(User.Identity.Name); // static class.. process any new lachat input before we get going.. 

                // msg = string.Format("Starting Nutrients Ending process at {0}", DateTime.Now);
                //LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, "", name, "Profiling");
                // just added & c.Validated == false to query below
                RiverWatchEntities RWE = new RiverWatchEntities();

                //msg = string.Format("Starting Validation nutrient counts process at {0}", DateTime.Now);
                //LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, "", name, "Profiling");

                // added sample number to query 
                var C = from c in RWE.NutrientDatas
                        where c.Valid == true & c.TypeCode.Contains("05") & c.Validated == false & c.SampleNumber != null
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
                //string name = "";
                //if (User.Identity.Name.Length < 3)
                //    name = "Not logged in";
                //else
                //    name = User.Identity.Name;
                //string msg = string.Format("Starting Validation sql validation counts process at {0}", DateTime.Now);
                //LogError LE = new LogError();
                //LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, "", name, "Profiling");
                // removed [Riverwatch].[dbo].
                using (SqlDataSource S = new SqlDataSource()) 
                {
                    DataSourceSelectArguments args = new DataSourceSelectArguments();
                    S.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  //GlobalSite.RiverWatchDev;
                    S.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '1' and valid = 1 and saved = 0";
                    System.Data.DataView result = (DataView)S.Select(args);
                    blankCount = result.Table.Rows.Count;

                    S.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '2' and valid = 1 and saved = 0";
                    result = (DataView)S.Select(args);
                    dupCount = result.Table.Rows.Count;

                    S.SelectCommand = "SELECT * FROM [InboundICPFinal] where left( DUPLICATE, 1) = '0' and valid = 1 and saved = 0";
                    result = (DataView)S.Select(args);
                    normalCount = result.Table.Rows.Count;

                    // SELECT * FROM [LachatBCnotEntered]

                    S.SelectCommand = "SELECT * FROM [LachatBCnotEntered]";
                    result = (DataView)S.Select(args);
                    lachatNotRecorded = result.Table.Rows.Count;


                    // run view to see if there are any barcodes in lachat that are not in nutrientbarcode table, ie they require attention. 
                     
                    if (blankCount == 0)
                    {
                        lblICPBlanks.Text = "There are no blank metals to validate";
                        btnICPBlanks.Enabled = false;
                        btnICPBlanks.BackColor = System.Drawing.Color.Maroon;
                    }
                    else
                    {
                        lblICPBlanks.Text = string.Format("There are {0} blank metal samples to validate", blankCount);
                        btnICPBlanks.Enabled = true;
                        btnICPBlanks.BackColor = System.Drawing.Color.LightCyan;
                    }

                    if (dupCount == 0)
                    {
                        lblICPDups.Text = "There are no metal blanks to validate";
                        btnICPDups.Enabled = false;
                        btnICPDups.BackColor = System.Drawing.Color.Maroon;
                    }
                    else
                    {
                        lblICPDups.Text = string.Format("There are {0} metal dup samples", dupCount);
                        btnICPDups.Enabled = true;
                        btnICPDups.BackColor = System.Drawing.Color.LightCyan;
                    }

                    if (normalCount == 0)
                    {
                        lblICPSamples.Text = "There are no metal normals to validate";
                        btnICPSamples.Enabled = false;
                        btnICPSamples.BackColor = System.Drawing.Color.Maroon;
                    }
                    else
                    {
                        lblICPSamples.Text = string.Format("There are {0} normal samples to validate", normalCount);
                        btnICPSamples.Enabled = true;
                        btnICPSamples.BackColor = System.Drawing.Color.LightCyan;
                    }

                    // now list Lachat samples 
                    if (nutrientCount > 0)
                    {
                        lblLachet.Text = string.Format("There are {0} Nutrient samples to validate", nutrientCount);
                        
                    }
                    else
                    {
                        lblLachet.Text = "There are no Nutrient Samples to Validate";
                    }

                     // now list Lachat samples 
                    if (nutrientDupCount > 0)
                    {

                        lblLachatDups.Text = string.Format("There are {0} nutrient dup samples to validate", nutrientDupCount);
                    }
                    else
                    {
                        lblLachatDups.Text = "There are no nutrient dup samples to Validate";
                    }

                    if(lachatNotRecorded > 0)
                    {
                        lblLachatMessage.Text = string.Format("NOTE: There are {0} unrecorded Lachat barcodes - view under Reports", lachatNotRecorded);
                        lblLachatMessage.ForeColor = System.Drawing.Color.Red;
                        lblLachatMessage.Visible = true;
                    }
                    else
                    {
                        lblLachatMessage.Text = "";
                        lblLachatMessage.Visible = false;
                    }

                    RiverWatchEntities RWE = new RiverWatchEntities();
                    var U = (from u in RWE.UnknownSample
                             where u.Valid == true & u.Validated == false
                             select u);

                    unknownCount = U.Count();
                    if(unknownCount > 0)
                        lblUnknowns.Text = string.Format("There are {0} Unknown samples to validate", unknownCount);
                    else
                        lblUnknowns.Text = string.Format("There are no Unknown samples to validate");

                    // now do field data 

                    string cmdCount = "select count(*) from [FieldNOTInSamples]";
                    string totalField = "select count(*) from [FieldINSamples]";
                    using (SqlConnection conn = new SqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; // RWE.Database.Connection.ConnectionString;
                            cmd.Connection = conn;
                            conn.Open();
                            cmd.CommandText = cmdCount;
                            FieldNotRecorded = (int)cmd.ExecuteScalar();

                            cmd.CommandText = totalField;
                            FieldCount = (int)cmd.ExecuteScalar();
                        }
                    }

                    //msg = string.Format("Ending Validation sql validation counts process at {0}", DateTime.Now);
                    //LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, "", name, "Profiling");

                    if(FieldNotRecorded > 0)
                    {
                        lblFieldNotRecorded.Text = string.Format("There are {0} unrecorded Field Data Records - view under Reports", FieldNotRecorded);
                        lblFieldNotRecorded.ForeColor = System.Drawing.Color.Red; 
                    }
                    else
                    {
                        lblFieldNotRecorded.Text = string.Format("There are NO incoming Field Data records not yet entered");
                        lblFieldNotRecorded.ForeColor = System.Drawing.Color.Black; 
                    }

                   if(FieldCount > 0)
                   {
                       lblFieldSamples.Text = string.Format("There are {0} Field Data records to validate", FieldCount);
                   }
                   else
                   {
                       lblFieldSamples.Text = string.Format("There are NO Field Data records to validate");
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

        protected void btnUnknown_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/ValidateUnknowns.aspx");
        }

        protected void btnField_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/ValidateField.aspx");
        }
    }
}