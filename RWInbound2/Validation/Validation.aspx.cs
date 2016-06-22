using System;
using System.Collections.Generic;
using System.Configuration;
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
            int sampleCount = 0;
            btnICPDups.Enabled = false; 

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchWaterDEV"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("select count (*) from [tblInboundICP] where left( DUPLICATE, 1) = '1'"); 
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr.HasRows)
                                {
                                    blankCount = (int)sdr[0];                                    
                                }
                            }
                        }
                         
                        cmd.CommandText = string.Format("select count (*) from [tblInboundICP] where left( DUPLICATE, 1) = '2'");     

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr.HasRows)
                                {
                                    dupCount = (int)sdr[0];                                    
                                }
                            }
                        }

                        cmd.CommandText = string.Format("select count (*) from [tblInboundICP] where left( DUPLICATE, 1) = '0'");
                      
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr.HasRows)
                                {
                                   sampleCount = (int)sdr[0];
                                }
                            }
                        }

                        conn.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                string msg = ex.Message;    // XXXX need to build an error log file and logging code               
            }

            lblICPBlanks.Text = string.Format("There are {0} Blank samples", blankCount);
            lblICPSamples.Text = string.Format("There are {0} Actual samples", sampleCount);
            lblICPDups.Text = string.Format("There are {0} Duplicate samples", dupCount);
        }

        protected void btnICPBlanks_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Validation/ValidateBlanks.aspx"); 
        }

        protected void btnICP_Click(object sender, EventArgs e)
        {

        }

        protected void btnICPDups_Click(object sender, EventArgs e)
        {

        }

        protected void btnICPSamples_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Validation/Default.aspx");
        }
    }
}