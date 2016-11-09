using BotDetect.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Public
{
    public partial class PublicReports : System.Web.UI.Page
    {      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    SetMessages();
                    // Validate initially to force asterisks
                    // to appear before the first roundtrip.
                    Validate();

                    DropDownList states
                        = ((DropDownList)PublicUserFormView.FindControl("newStateDropDown"));
                    states.Items.Clear();
                    states.DataSource = GetAllStates();
                    states.DataBind();
                }

                // check users permissons 
                bool allowed = false;

                allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
                if (!allowed)
                {
                    pnlLogin.Visible = true;
                    pnlRequest.Visible = false;
                    pnlReports.Visible = false;
                }
                else
                {
                    pnlLogin.Visible = false;
                    pnlRequest.Visible = false;
                    pnlReports.Visible = true;
                }

                var clientID = ((TextBox)PublicUserFormView.FindControl("CaptchaCode")).ClientID;
                ((WebFormsCaptcha)PublicUserFormView.FindControl("RegisterCaptcha")).UserInputID = clientID;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Page_Load", "", "");
            }
        }       

        private void SetMessages(string type = "", string message = "")
        {
            switch (type)
            {
                case "Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    break;
                case "Error":
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    break;
                case "CaptchaError":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    ((Label)PublicUserFormView.FindControl("verifyCaptchaErrorLabel")).Text = message;
                    break;               
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    ((Label)PublicUserFormView.FindControl("verifyCaptchaErrorLabel")).Text = "";
                    break;
            }
        }   

        public void InboundICPFormView_InsertItem()
        {
            try
            {                
                string verified = ((HiddenField)PublicUserFormView.FindControl("verified")).Value;
                var model = new PublicUsers();
                TryUpdateModel(model);
                                
                if (!string.IsNullOrEmpty(verified) && verified.Equals("verified"))
                {
                    if (ModelState.IsValid)
                    {
                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            var currentUser = _db.PublicUsers
                                                 .Where(u => u.Email.Equals(model.Email))
                                                 .FirstOrDefault();

                            if (currentUser == null)
                            {
                                string selectedState
                                    = ((DropDownList)PublicUserFormView.FindControl("newStateDropDown")).SelectedItem.Value;

                                model.State = selectedState;
                                model.DateCreated = DateTime.Now;
                                model.DateLastActivity = DateTime.Now;
                                model.UseCount = 1;
                                model.Valid = true;
                                string test = model.Email;
                                int length = test.Length;
                                _db.PublicUsers.Add(model);
                                _db.SaveChanges();

                                pnlLogin.Visible = false;

                                SetMessages("Success", "Thank you. Your request has been submitted and you will receive an email shortly.");
                            }
                            else
                            {
                                SetMessages("Error", "Email already exists.");                             
                            }
                        }
                    }
                }
                else
                {
                    pnlRequest.Visible = true;
                    SetMessages("CaptchaError", "Incorrect code, please try again.");
                }
                
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "InboundICPFormView_InsertItem", "", "");
            }
        }

        protected void btnVerifyCaptcha_Click(object sender, EventArgs e)
        {
            // setup client-side input processing  
            var userInput = ((TextBox)PublicUserFormView.FindControl("CaptchaCode")).Text;
            var isHuman = ((WebFormsCaptcha)PublicUserFormView.FindControl("RegisterCaptcha")).Validate(userInput);

            if (isHuman)
            {
                pnlRequest.Visible = true;
                ((Panel)PublicUserFormView.FindControl("pnlVerifyCaptcha")).Visible = false;
                ((Panel)PublicUserFormView.FindControl("pnlSubmit")).Visible = true;
                ((HiddenField)PublicUserFormView.FindControl("verified")).Value = "verified";
            }
            else
            {
                pnlRequest.Visible = true;
                string errorMsg = "Incorrect code, please try again.";
                SetMessages("CaptchaError", errorMsg);
            }
        }

        protected void btnErrorLogReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ViewErrorLog.aspx");
        }

        protected void btnStationsWithGauges_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ViewStationsWithGauges.aspx");
        }

        protected void btnLachatNoBC_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ReportLachatNOTINNutrientBarcode.aspx");
        }

        protected void btnOrgStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/OrgStatus.aspx");
        }

        protected void btnOrgStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/OrgStations.aspx");
        }

        protected void btnOrganizations_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Organizations.aspx");
        }

        protected void btnParticipants_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Participants.aspx");
        }

        protected void btnStations_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Stations.aspx");
        }

        protected void btnSamples_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Samples.aspx");
        }

        protected void btnMetalBarCodes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/MetalBarCodes.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                if (!string.IsNullOrEmpty(email))
                {
                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        var currentUser = _db.PublicUsers
                                             .Where(u => u.Email.Equals(email))
                                             .FirstOrDefault();
                        if (currentUser != null)
                        {
                            if (!currentUser.Approved)
                            {
                                SetMessages("Error", "Account pending approval.");
                            }
                            else
                            {
                                Session["ROLE"] = 3;
                                currentUser.UseCount += 1;
                                currentUser.DateLastActivity = DateTime.Now;
                                //SetMessages("Success", "Welcome " + email);

                                pnlLogin.Visible = false;
                                pnlRequest.Visible = false;
                                pnlReports.Visible = true;

                                _db.SaveChanges();

                                Response.Redirect("PublicReports.aspx");
                            }
                        }
                        else
                        {
                            SetMessages("Error", "Account not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnLogin_Click", "", "");
            }
        }

        protected void btnRequestAccess_Click(object sender, EventArgs e)
        {
            SetMessages();
            pnlRequest.Visible = true;
            ((Panel)PublicUserFormView.FindControl("pnlVerifyCaptcha")).Visible = true;
            ((TextBox)PublicUserFormView.FindControl("CaptchaCode")).Text = null; // clear previous user input
            ((Panel)PublicUserFormView.FindControl("pnlSubmit")).Visible = false;
        }

        public ArrayList GetAllStates()
        {
            ArrayList stateList = new ArrayList();

            stateList.Add(new ListItem("", "--Select State--"));
            stateList.Add(new ListItem("AL", "Alabama"));
            stateList.Add(new ListItem("AK", "Alaska"));
            stateList.Add(new ListItem("AZ", "Arizona"));
            stateList.Add(new ListItem("AR", "Arkansas"));
            stateList.Add(new ListItem("CA", "California"));
            stateList.Add(new ListItem("CO", "Colorado"));
            stateList.Add(new ListItem("CT", "Connecticut"));
            stateList.Add(new ListItem("DE", "Delaware"));
            stateList.Add(new ListItem("FL", "Florida"));
            stateList.Add(new ListItem("GA", "Georgia"));
            stateList.Add(new ListItem("HI", "Hawaii"));
            stateList.Add(new ListItem("ID", "Idaho"));
            stateList.Add(new ListItem("IL", "Illinois"));
            stateList.Add(new ListItem("IN", "Indiana"));
            stateList.Add(new ListItem("IA", "Iowa"));
            stateList.Add(new ListItem("KS", "Kansas"));
            stateList.Add(new ListItem("KY", "Kentucky"));
            stateList.Add(new ListItem("LA", "Louisiana"));
            stateList.Add(new ListItem("ME", "Maine"));
            stateList.Add(new ListItem("MD", "Maryland"));
            stateList.Add(new ListItem("MA", "Massachusetts"));
            stateList.Add(new ListItem("MI", "Michigan"));
            stateList.Add(new ListItem("MN", "Minnesota"));
            stateList.Add(new ListItem("MS", "Mississippi"));
            stateList.Add(new ListItem("MO", "Missouri"));
            stateList.Add(new ListItem("MT", "Montana"));
            stateList.Add(new ListItem("NE", "Nebraska"));
            stateList.Add(new ListItem("NV", "Nevada"));
            stateList.Add(new ListItem("NH", "New Hampshire"));
            stateList.Add(new ListItem("NJ", "New Jersey"));
            stateList.Add(new ListItem("NM", "New Mexico"));
            stateList.Add(new ListItem("NY", "New York"));
            stateList.Add(new ListItem("NC", "North Carolina"));
            stateList.Add(new ListItem("ND", "North Dakota"));
            stateList.Add(new ListItem("OH", "Ohio"));
            stateList.Add(new ListItem("OK", "Oklahoma"));
            stateList.Add(new ListItem("OR", "Oregon"));
            stateList.Add(new ListItem("PA", "Pennsylvania"));
            stateList.Add(new ListItem("RI", "Rhode Island"));
            stateList.Add(new ListItem("SC", "South Carolina"));
            stateList.Add(new ListItem("SD", "South Dakota"));
            stateList.Add(new ListItem("TN", "Tennessee"));
            stateList.Add(new ListItem("TX", "Texas"));
            stateList.Add(new ListItem("UT", "Utah"));
            stateList.Add(new ListItem("VT", "Vermont"));
            stateList.Add(new ListItem("VA", "Virginia"));
            stateList.Add(new ListItem("WA", "Washington"));
            stateList.Add(new ListItem("WV", "West Virginia"));
            stateList.Add(new ListItem("WI", "Wisconsin"));
            stateList.Add(new ListItem("WY", "Wyoming"));

            return stateList;
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SetMessages();

            if (ex.GetType().IsAssignableFrom(typeof(DbEntityValidationException)))
            {
                DbEntityValidationException efException = ex as DbEntityValidationException;
                StringBuilder sb = new StringBuilder();

                foreach (var eve in efException.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendFormat("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage + Environment.NewLine);
                    }
                }
                SetMessages("Error", sb.ToString());
            }
            else
            {
                SetMessages("Error", ex.Message);
            }
        }
        
    }
}