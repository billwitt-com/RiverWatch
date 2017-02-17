using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class BatchDeleteNutrients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblMsg.Visible = false;
                btnDelete.Visible = false;
                pnlConfirm.Visible = false;
                pnlAreYouSure.Visible = false;
                pnlResults.Visible = false;
            }

        }



        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static List<string> SearchBatchNumbers(string prefixText, int count)
        {
            List<string> batches = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select distinct [Batch] from [Lachat] where [Batch] is not null and [Batch] like @SearchText + '%' order by  Batch ";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                batches.Add(sdr["Batch"].ToString());
                            }
                        }
                        conn.Close();
                        return batches;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return batches;
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string batch = "";
            int rawCount = 0;
            int validatedCount = 0;
            int NotValidatedCount = 0;

            batch = tbSelectBatch.Text;
            if(batch.Length < 1)
            {
                lblMsg.Text = "Please choose a valid Batch";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true; 
            }
            else
            {
                lblMsg.Visible = false;
            }

            pnlResults.Visible = true;
            RiverWatchEntities RWE = new RiverWatchEntities();

            // all entries, validated and not
            var T = from t in RWE.Lachats
                    where t.Batch == batch & t.Valid == true
                    select t;
            
            if (T != null)
            {
                rawCount = T.Count();
                lblTotalResults.Text = string.Format("There are {0} total entries for Batch {1}", rawCount, batch);                
            }
            else
            {
                lblResults.Text = string.Format("There are no Valid entries in Batch {0}", batch);
            }

            // NOT validated
            var R = from r in RWE.Lachats
                    where r.Validated == false & r.Batch == batch & r.Valid == true
                    select r;

            // already validated
            var V = from v in RWE.Lachats
                    where v.Validated == true & v.Batch == batch & v.Valid == true
                    select v;

            if(V != null)
            {
                validatedCount = V.Count(); 
            }

            if(R != null)
            {
                // pnlResults
                NotValidatedCount = R.Count();
                lblResults.Text = string.Format("There are {0} entries that have NOT been validated in Batch {1}", NotValidatedCount, batch);
                
                if (validatedCount > 0)
                {
                    lblCanBeDeleted.Text = string.Format("Would you like to delete the {0} unvalidated entries?", NotValidatedCount);
                    btnDelete.Visible = true;
                    btnDelete.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblResults.Text = string.Format("There are no entries in Batch {0} that can be deleted, they have been validated", batch);
                    btnDelete.Visible = false; 
                }                
            }
            else
            {
                lblResults.Text = string.Format("There are no Valid entries for Batch {0}", batch);
                btnDelete.Visible = false; 
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            pnlAreYouSure.Visible = true;

        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();
            int counter = 0;
            string batch = tbSelectBatch.Text;
            var T = from t in RWE.Lachats
                    where t.Batch == batch & t.Valid == true & t.Validated == false 
                    select t;

            if(T != null)
            {
                foreach(var z in T)
                {
                    z.Valid = false;
                    counter++;
                }
                RWE.SaveChanges(); 
            }

            pnlConfirm.Visible = true;
            lblConfirm.Visible = true;
            lblConfirm.Text = string.Format("{0} entries have been deleted - Click OK to continue", counter); // confirm button

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            btnDelete.Visible = false;
            pnlConfirm.Visible = false;
            pnlAreYouSure.Visible = false;
            pnlResults.Visible = false;
            tbSelectBatch.Text = "";
            return; 
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            btnDelete.Visible = false;
            pnlConfirm.Visible = false;
            pnlAreYouSure.Visible = false;
            pnlResults.Visible = false;
            tbSelectBatch.Text = "";
            return; 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            btnDelete.Visible = false;
            pnlConfirm.Visible = false;
            pnlAreYouSure.Visible = false;
            pnlResults.Visible = false;
            tbSelectBatch.Text = "";
            return; 

        }
    }
}