﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// modified to only have one pH value bw 2016/10/20
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

        //    lblErrorMsg.Visible = false;

            if(!IsPostBack)
            {
                Session["TRIES"] = 0;
                if (User.Identity.IsAuthenticated)
                {
                    lblPassword.Visible = false;
                    tbOrgPwd.Visible = false;
                    lblWelcome.Text = string.Format("Welcome {0}", User.Identity.Name);
                }
                else
                {
                    lblWelcome.Visible = false;
                    lblPassword.Visible = true;
                    tbOrgPwd.Visible = true;
                }
                tbTestDate.Text = DateTime.Now.ToShortDateString(); 
            }
        }

        // a lot of extra stuff here because this can be public facing.. 

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int kitNumber = 0;
            string kn = tbKitNumber.Text.Trim();
            string passWord = "";

            lblErrorMsg.Visible = false; 
            if(int.TryParse(kn, out kitNumber))
            {
                passWord = tbOrgPwd.Text.Trim();
                if((passWord.Length > 1) | (User.Identity.IsAuthenticated))
                {
                    // query to see if this kit and pwd exist... 

                    RiverWatchEntities RWE = new RiverWatchEntities();
                    organization Q;
                    if (User.Identity.IsAuthenticated)
                    {
                         Q = (from q in RWE.organizations
                                 where q.KitNumber == kitNumber 
                                 select q).FirstOrDefault();
                    }
                    else
                    {
                         Q = (from q in RWE.organizations
                                 where q.KitNumber == kitNumber & q.Password.ToUpper() == passWord.ToUpper()
                                 select q).FirstOrDefault();
                    }

                    if(Q != null)
                    {
                        pnlData.Visible = true;
                        lblKitNumber.Text = kitNumber.ToString();
                        lblOrgName.Text = Q.OrganizationName; 
                        tbAlk1.Focus();
                        return;
                    }
                    else
                    {
                        if (Session["TRIES"] != null)
                        {
                            int tries = (int)Session["TRIES"];
                            tries++;
                            Session["TRIES"] = tries;
                            if (tries > 3)
                            {
                                // for now, take user to timedout page, not sure what else to do... 
                                Response.Redirect("~/timedout.aspx");
                            }
                            else
                            {
                                lblErrorMsg.Visible = true;
                                lblErrorMsg.Text = "Your loging information is incorrect, please try again";
                                tbOrgPwd.Text = "";
                                tbKitNumber.Text = "";                               
                                return;
                            }
                        }
                    }
                }
                tbKitNumber.Text = "";
                tbOrgPwd.Text = "";
                return; 
            }
            if (Session["TRIES"] != null)
            {
                int tries = (int)Session["TRIES"];
                tries++;
                Session["TRIES"] = tries;
                if (tries > 3)
                {
                    // for now, take user to timedout page, not sure what else to do... 
                    Response.Redirect("timedout.aspx");
                }
                else
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = "Your loging information is incorrect, please try again";
                    tbOrgPwd.Text = "";
                    tbKitNumber.Text = "";
                    return;
                }
            }

        }

        // we may need to make up to three entries into table, one for each sample type pH, alk and hardness, plus sample type
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RiverWatchEntities RWE = new RiverWatchEntities();
            int kitNumber = 0;
            string kn = tbKitNumber.Text.Trim();
            string sampleNumber = ""; 
            string nam = "";
            DateTime dateStarted;
            decimal val1 = 0;
            decimal val2 = 0;
            int recordsSaved = 0;
            int numSamples = 0;
            int sampNum = 0;

            if (int.TryParse(kn, out kitNumber))
            {
                try
                {
                    var Q = (from q in RWE.organizations
                             where q.KitNumber == kitNumber // & q.Password.ToUpper() == passWord.ToUpper()
                             select q).FirstOrDefault();

                    if (Q != null)
                    {
                      //  sampleNumber = tbSampleNumber.Text.Trim();    we took this off the page

                        if (tbAlkBatchNumber.Text.Length > 1)    // we have alk info
                        {
                            UnknownSample US = new UnknownSample();
                            US.OrganizationID = Q.ID;
                            US.Comment = tbComments.Text.Trim();
                            US.BatchSampleNumber = tbAlkBatchNumber.Text;
                            US.SampleNumber = sampleNumber;

                            if (DateTime.TryParse(tbTestDate.Text.Trim(), out dateStarted))
                            {
                                US.DateSent = dateStarted;
                            }

                            US.DateCreated = DateTime.Now;
                            if (User.Identity.Name.Length < 3)
                                nam = Q.OrganizationName;
                            else
                                nam = User.Identity.Name;
                            US.UserCreated = nam;
                            US.Valid = true;
                            US.Validated = false;

                            if (decimal.TryParse(tbAlk1.Text, out val1))
                            {
                                US.Value1 = val1;
                                numSamples++;
                            }
                            if (decimal.TryParse(tbAlk2.Text, out val2))
                            {
                                US.Value2 = val2;
                                numSamples++;
                            }

                            US.SampleType = "A";
                            US.Path = "M"; 

                            // save this
                            RWE.UnknownSample.Add(US);
                            RWE.SaveChanges();
                            recordsSaved++;
                        }

                        if (tbHardnessBatchNumber.Text.Length > 1)
                        {
                            UnknownSample US = new UnknownSample();
                            US.OrganizationID = Q.ID;
                            US.Comment = tbComments.Text.Trim();
                            US.BatchSampleNumber = tbHardnessBatchNumber.Text;
                            US.SampleNumber = sampleNumber;
                            if (DateTime.TryParse(tbTestDate.Text.Trim(), out dateStarted))
                            {
                                US.DateSent = dateStarted;
                            }

                            US.DateCreated = DateTime.Now;
                            if (User.Identity.Name.Length < 3)
                                nam = Q.OrganizationName;
                            else
                                nam = User.Identity.Name;
                            US.UserCreated = nam;
                            US.Valid = true;
                            US.Validated = false;

                            if (decimal.TryParse(tbHard1.Text, out val1))
                                US.Value1 = val1;
                            if (decimal.TryParse(tbHard2.Text, out val2))
                                US.Value2 = val2;
                            
                            US.SampleType = "H";
                            US.Path = "M"; 

                            RWE.UnknownSample.Add(US);
                            RWE.SaveChanges();
                            recordsSaved++;
                        }
                        if (tbpHBatchNumber.Text.Length > 1)
                        {
                            UnknownSample US = new UnknownSample();
                            US.OrganizationID = Q.ID;
                            US.Comment = tbComments.Text.Trim();
                            US.BatchSampleNumber = tbpHBatchNumber.Text;
                            US.SampleNumber = sampleNumber;
                            if (DateTime.TryParse(tbTestDate.Text.Trim(), out dateStarted))
                            {
                                US.DateSent = dateStarted;
                            }

                            US.DateCreated = DateTime.Now;
                            if (User.Identity.Name.Length < 3)
                                nam = Q.OrganizationName;
                            else
                                nam = User.Identity.Name;
                            US.UserCreated = nam;
                            US.Valid = true;
                            US.Validated = false;

                            if (decimal.TryParse(tbpH1.Text, out val1))
                                US.Value1 = val1;
                            //if (decimal.TryParse(tbpH2.Text, out val2))
                            //    US.Value2 = val2;
                                
                            US.SampleType = "P";
                            US.Path = "M"; 

                            // save this
                            RWE.UnknownSample.Add(US);
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
            lblMessage0.Text = "No Records Saved, please check your entries"; 
        }  
    }
}