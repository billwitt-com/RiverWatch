using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Public
{
    public partial class UnknownData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool allowed = false;
            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx"); 
            pnlData.Visible = false; // hide until login
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int kitNumber = 0;
            string kn = tbKitNumber.Text.Trim();
            string passWord = "";


            if(int.TryParse(kn, out kitNumber))
            {
                passWord = tbOrgPwd.Text.Trim();
                if(passWord.Length > 4)
                {
                    // query to see if this kit and pwd exist... 

                    RiverWatchEntities RWE = new RiverWatchEntities();
                    var Q = (from q in RWE.organizations
                            where q.KitNumber == kitNumber & q.Password.ToUpper() == passWord.ToUpper()
                            select q).FirstOrDefault(); 

                    if(Q != null)
                    {
                        pnlData.Visible = true;
                        lblKitNumber.Text = kitNumber.ToString();
                        lblOrgName.Text = Q.OrganizationName; 
                        tbAlk1.Focus();
                        return;
                    }
                }
            }
            tbKitNumber.Text = "";
            tbOrgPwd.Text = "";
            return; 
        }

        // we may need to make up to three entries into table, one for each sample type pH, alk and hardness, plus sample type
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();

            int kitNumber = 0;
            string kn = tbKitNumber.Text.Trim();
            string passWord = "";
            string nam = "";
            DateTime dateStarted;
            decimal val1 = 0;
            decimal val2 = 0;
            int recordsSaved = 0;
            if (int.TryParse(kn, out kitNumber))
            {
                passWord = tbOrgPwd.Text.Trim();
                if (passWord.Length > 4)
                {
                    // query to see if this kit and pwd exist... second time as something may have changed 
                    try
                    {
                        var Q = (from q in RWE.organizations
                                 where q.KitNumber == kitNumber & q.Password.ToUpper() == passWord.ToUpper()
                                 select q).FirstOrDefault();

                        if (Q != null)
                        {
                            if(tbAlkBatchNumber.Text.Length > 4)    // we have alk info
                            {
                                UnknownSample US = new UnknownSample();
                                US.OrganizationID = Q.ID;
                                US.Comment = tbComments.Text.Trim();
                                US.BatchSampleNumber = tbAlkBatchNumber.Text; 
                                if(DateTime.TryParse(tbTestDate.Text.Trim(), out dateStarted))
                                {
                                   US.DateSent = dateStarted;
                                }

                                US.DateCreated = DateTime.Now; 
                                if (User.Identity.Name.Length < 3)
                                    nam = Q.OrganizationName; 
                                else
                                    nam = User.Identity.Name;
                                US.UserCreated = nam;   
                                US.Valid = "N";

                                if(decimal.TryParse(tbAlk1.Text, out val1))
                                    US.Value1 = val1; 
                                 if(decimal.TryParse(tbAlk2.Text, out val2))
                                    US.Value2 = val2; 
                              
                                // save this
                                RWE.UnknownSamples.Add(US);
                                RWE.SaveChanges();
                                recordsSaved++;
                            }

                            if(tbHardnessBatchNumber.Text.Length > 4)
                            {
                                UnknownSample US = new UnknownSample();
                                US.OrganizationID = Q.ID;
                                US.Comment = tbComments.Text.Trim();
                                US.BatchSampleNumber = tbHardnessBatchNumber.Text; 
                                if(DateTime.TryParse(tbTestDate.Text.Trim(), out dateStarted))
                                {
                                   US.DateSent = dateStarted;
                                }

                                US.DateCreated = DateTime.Now; 
                                if (User.Identity.Name.Length < 3)
                                    nam = Q.OrganizationName; 
                                else
                                    nam = User.Identity.Name;
                                US.UserCreated = nam;   
                                US.Valid = "N";

                                if (decimal.TryParse(tbHard1.Text, out val1))
                                    US.Value1 = val1;
                                if (decimal.TryParse(tbHard2.Text, out val2))
                                    US.Value2 = val2; 
                              
                                // save this
                                RWE.UnknownSamples.Add(US);
                                RWE.SaveChanges();
                                recordsSaved++;
                            }
                            if (tbpHBatchNumber.Text.Length > 4)
                            {
                                UnknownSample US = new UnknownSample();
                                US.OrganizationID = Q.ID;
                                US.Comment = tbComments.Text.Trim();
                                US.BatchSampleNumber = tbpHBatchNumber.Text; 
                                if(DateTime.TryParse(tbTestDate.Text.Trim(), out dateStarted))
                                {
                                   US.DateSent = dateStarted;
                                }

                                US.DateCreated = DateTime.Now; 
                                if (User.Identity.Name.Length < 3)
                                    nam = Q.OrganizationName; 
                                else
                                    nam = User.Identity.Name;
                                US.UserCreated = nam;   
                                US.Valid = "N";

                                if (decimal.TryParse(tbpH1.Text, out val1))
                                    US.Value1 = val1;
                                if (decimal.TryParse(tbpH2.Text, out val2))
                                    US.Value2 = val2; 
                              
                                // save this
                                RWE.UnknownSamples.Add(US);
                                RWE.SaveChanges();
                                recordsSaved++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        if (User.Identity.Name.Length < 3)
                            nam = "Not logged in";
                        else
                            nam = User.Identity.Name;
                        string msg = ex.Message;
                        LogError LE = new LogError();
                        LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                    }
                    lblMessage0.Text = string.Format("{0} Records Saved", recordsSaved);
                    return;
                }
            }
            lblMessage0.Text = "No Records Saved, please check your entries"; 
        }  
    }
}