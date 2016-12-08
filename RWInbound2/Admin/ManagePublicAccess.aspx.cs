using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Text;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;

namespace RWInbound2.Admin
{
    public partial class ManagePublicAccess : System.Web.UI.Page
    {
        private bool invalid = false;
        private string selectedState = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMessages();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
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
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    break;
            }
        }

        public IQueryable<PublicUsers> GetPublicUsers([QueryString]int? seeOnlyApprovedUsers = 0,
                                                              [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (seeOnlyApprovedUsers == 0)
                {
                    return _db.PublicUsers
                              .Where(p => p.Approved == false)
                              .OrderBy(c => c.Email);
                }

                IQueryable<PublicUsers> publicUsers = _db.PublicUsers
                                                     .OrderBy(c => c.Email);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return publicUsers;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetPublicUsers", "", "");
                return null;
            }
        }

        public void UpdatePublicUser(PublicUsers model, [Control("updateStateDropDown")] string text)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var publicUserToUpdate = _db.PublicUsers.Find(model.ID);
                    bool approved = publicUserToUpdate.Approved;
                    TryUpdateModel(publicUserToUpdate);

                    bool isValidEmail = IsValidEmail(publicUserToUpdate.Email);

                    if (!isValidEmail)
                    {
                        string errorMsg = string.Format("Incorrect Email format: {0}", publicUserToUpdate.Email);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        if (publicUserToUpdate.Approved && !approved)
                        {
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                publicUserToUpdate.ApprovedBy
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                publicUserToUpdate.ApprovedBy = "Unknown";
                            }
                            publicUserToUpdate.ApprovedDate = DateTime.Now;
                        }
                       
                        publicUserToUpdate.State = selectedState;

                        _db.SaveChanges();
                        string successMsg = string.Format("Public User Updated: {0}", publicUserToUpdate.Email);
                        SetMessages("Success", successMsg);
                    }                    
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdatePublicUser", "", "");
            }
        }

        public void DeletePublicUser(PublicUsers model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var publicUserToDelete = _db.PublicUsers.Find(model.ID);
                    _db.PublicUsers.Remove(publicUserToDelete);
                    _db.SaveChanges();

                    SetMessages("Success", "Public User Deleted");
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeletePublicUser", "", "");
                }
            }
        }

        public void AddNewPublicUser(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                string email = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewEmail")).Text;

                if (string.IsNullOrEmpty(email))
                {
                    ((Label)PublicUsersGridView.FooterRow.FindControl("lblEmailRequired")).Visible = true;
                }
                else
                {
                    string firstName = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewFirstName")).Text;
                    string lastName = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewLastName")).Text;
                    string address1 = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewAddress1")).Text;
                    string address2 = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewAddress2")).Text;
                    string city = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewCity")).Text;
                    string selectedState
                            = ((DropDownList)PublicUsersGridView.FooterRow.FindControl("NewStateDropDown")).SelectedItem.Value;
                    string zip = ((TextBox)PublicUsersGridView.FooterRow.FindControl("NewZip")).Text;
                    bool approved = ((CheckBox)PublicUsersGridView.FooterRow.FindControl("NewApproved")).Checked;

                    string approvedBy = string.Empty;
                    DateTime? approvedDate = null;
                    if (approved)
                    {
                        approvedBy = "Unknown";
                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            approvedBy
                                = HttpContext.Current.User.Identity.Name;
                        }
                        approvedDate = DateTime.Now;                    
                    }

                    bool receiveEmailUpdates = ((CheckBox)PublicUsersGridView.FooterRow.FindControl("NewReceiveEmailUpdates")).Checked;

                    var newPublicUser = new PublicUsers()
                    {
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        Address1 = address1,
                        Address2 = address2,
                        City = city,
                        State = selectedState,
                        Zip = zip,
                        Approved = approved,
                        ApprovedBy = approvedBy,
                        ApprovedDate = approvedDate,
                        DateCreated = DateTime.Now,
                        DateLastActivity = null,
                        UseCount = 0,
                        ReceiveEmailUpdates = receiveEmailUpdates,
                        Valid = true
                    };                   

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.PublicUsers.Add(newPublicUser);
                        _db.SaveChanges();                        

                        string successLabelText = "New Public User Added: " + newPublicUser.Email;
                        string redirect = "ManagePublicAccess.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewPublicUser", "", "");
            }
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

        protected void btnUnapprovedUsers_Click(object sender, EventArgs e)
        {            
            string redirect = "ManagePublicAccess.aspx?seeOnlyApprovedUsers=" + 0;

            Response.Redirect(redirect, false);
        }

        protected void btnSeeAllUsers_Click(object sender, EventArgs e)
        {
            string redirect = "ManagePublicAccess.aspx";

            Response.Redirect(redirect, false);
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

        private bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                //string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-SampleNumber2]{2,4}$";
                //string input = @"handro@comcast.net";
                //string inputs = @strIn;

                //string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-SampleNumber2]{2,4}$";
                //var valid = Regex.IsMatch(inputs, pattern);
                //return valid;
                bool valid = Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                return valid;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        protected void PublicUsersGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //PublicUsersGridView.EditIndex = e.NewEditIndex;
            //gridviewBind();// your gridview binding function
        }

        protected void PublicUsersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList updateStateDropDown
                        = (DropDownList)e.Row.FindControl("updateStateDropDown");
                    updateStateDropDown.Items.Clear();
                    updateStateDropDown.DataSource = GetAllStates();
                    updateStateDropDown.DataBind();
                    var item = e.Row.DataItem as PublicUsers;
                    updateStateDropDown.Items.FindByValue(item.State).Selected = true;
                }
            }                
        }

        public IQueryable BindStates()
        {
            var states = GetAllStates().ToArray().AsQueryable();
            return states;
        }

        protected void PublicUsersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            selectedState = 
                ((DropDownList)PublicUsersGridView.Rows[e.RowIndex]
                                                  .FindControl("updateStateDropDown")).SelectedItem.Value;
        }
    }
}