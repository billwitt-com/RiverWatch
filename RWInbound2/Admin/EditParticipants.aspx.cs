using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity.Validation;
using System.Reflection;
using RWInbound2.View_Models;

namespace RWInbound2.Admin
{
    public partial class EditParticipants : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMessages();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();                
            }
            try
            {
                //var editButton = ((Button)ExpWaterFormView.FindControl("EditButton"));
                var itemTemplatePanel = ((Panel)ExpWaterFormView.FindControl("ItemTemplatePanel"));

                int id = 0;
                if (ExpWaterFormView != null && ExpWaterFormView.DataKey.Value != null)
                {
                    int.TryParse(ExpWaterFormView.DataKey.Value.ToString(), out id);
                }

                if(itemTemplatePanel != null && id > 0)
                {
                    itemTemplatePanel.Visible = true;
                    OrganizationLabel.Visible = false;
                    OrganizationName.Visible = false;
                }
                else if (itemTemplatePanel != null)
                {
                    itemTemplatePanel.Visible = false;
                    OrganizationLabel.Visible = true;
                    OrganizationName.Visible = true;
                }                
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Page_Load", "", "");
            }            
        }

        private void SetMessages(string type = "", string message = "", string id = "")
        {
            switch (type)
            {
                case "Success":
                    OrganizationName.Text = "";
                    OrgID.Text = "";
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    break;
                case "Error":
                    OrganizationName.Text = "";
                    OrgID.Text = "";
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    break;
                case "OrganizationID":
                    OrgID.Text = id;
                    OrganizationName.Text = message;
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    break;
            }
        }

        public IQueryable<ParticipantsViewModel> GetParticipants([QueryString]string organizationSearchTerm = "",
                                                      [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                var newButton = ((Button)ExpWaterFormView.FindControl("NewButton"));

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);

                    PropertyInfo isreadonly
                    = typeof(System.Collections.Specialized.NameValueCollection)
                            .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    // make collection editable
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    // remove
                    //this.Request.QueryString.Clear();
                    this.Request.QueryString.Remove("successLabelMessage");
                }
                
                if (!string.IsNullOrEmpty(organizationSearchTerm))
                {
                    
                    var organization = (from o in _db.organizations
                                        where o.OrganizationName == organizationSearchTerm
                                        select new ParticipantsViewModel
                                        {
                                            ID = 0,
                                            OrganizationID = o.ID,                                             
                                            OrganizationName = o.OrganizationName
                                        });
              
                    int organizationId = 0;

                    foreach(var org in organization)
                    {
                        organizationId = org.OrganizationID;                        
                        break;
                    }

                    if (organizationId > 0)
                    {
                        SetMessages("OrganizationID", organizationSearchTerm, organizationId.ToString());

                        var participants = (from p in _db.tblParticipants
                                            where p.OrganizationID == organizationId
                                            orderby p.FirstName
                                            select new ParticipantsViewModel
                                            {
                                                OrganizationID = p.OrganizationID,
                                                OrganizationName = organizationSearchTerm,
                                                LastName = p.LastName,
                                                FirstName = p.FirstName,
                                                Title = p.Title,
                                                YearSignedOn = p.YearSignedOn,
                                                Phone = p.Phone,
                                                Email = p.Email,
                                                Address1 = p.Address1,
                                                Address2 = p.Address2,
                                                City = p.City,
                                                State = p.State,
                                                Zip = p.Zip,
                                                HomePhone = p.HomePhone,
                                                HomeEmail = p.HomeEmail,
                                                MailPreference = p.MailPreference,
                                                Active = p.Active,
                                                PrimaryContact = p.PrimaryContact,
                                                Training = p.Training,
                                                DateCreated = p.DateCreated,
                                                UserCreated = p.UserCreated,
                                                DateLastModified = p.DateLastModified,
                                                UserLastModified = p.UserLastModified,
                                                Valid = p.Valid,
                                                ID = p.ID
                                            }).OrderBy(p => p.FirstName);                        

                        if (participants.Count() > 0)
                        {
                            ExpWaterFormView.Visible = true;
                            return participants;
                        }
                        else
                        {
                            var itemTemplatePanel = ((Panel)ExpWaterFormView.FindControl("ItemTemplatePanel"));
                            if (itemTemplatePanel != null)
                            {
                                itemTemplatePanel.Visible = false;
                                OrganizationLabel.Visible = true;
                                OrganizationName.Visible = true;
                            }

                            return organization;
                        }
                    }   
                }                

                ExpWaterFormView.Visible = false;

                var allParticipants = (from p in _db.tblParticipants
                                       join o in _db.organizations on p.OrganizationID equals o.ID
                                        orderby p.FirstName
                                        select new ParticipantsViewModel
                                        {
                                            OrganizationID = p.OrganizationID,
                                            OrganizationName = o.OrganizationName,
                                            LastName = p.LastName,
                                            FirstName = p.FirstName,
                                            Title = p.Title,
                                            YearSignedOn = p.YearSignedOn,
                                            Phone = p.Phone,
                                            Email = p.Email,
                                            Address1 = p.Address1,
                                            Address2 = p.Address2,
                                            City = p.City,
                                            State = p.State,
                                            Zip = p.Zip,
                                            HomePhone = p.HomePhone,
                                            HomeEmail = p.HomeEmail,
                                            MailPreference = p.MailPreference,
                                            Active = p.Active,
                                            PrimaryContact = p.PrimaryContact,
                                            Training = p.Training,
                                            DateCreated = p.DateCreated,
                                            UserCreated = p.UserCreated,
                                            DateLastModified = p.DateLastModified,
                                            UserLastModified = p.UserLastModified,
                                            Valid = p.Valid,
                                            ID = p.ID
                                        }).OrderBy(p => p.FirstName);               

                return allParticipants;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetParticipants", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string organizationSearchTerm = organizationSearch.Text;
                string redirect = "EditParticipants.aspx?organizationSearchTerm=" + organizationSearchTerm;

                Response.Redirect(redirect, false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearch_Click", "", "");
            }
        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("EditParticipants.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForOrganization(string prefixText, int count)
        {
            List<string> organizations = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    organizations = _db.organizations
                                        .Where(o => o.OrganizationName.StartsWith(prefixText))
                                        .Select(c => c.OrganizationName).ToList();

                    return organizations;
                }
            }
            catch (Exception ex)
            {
                EditParticipants editParticipants = new EditParticipants();
                editParticipants.HandleErrors(ex, ex.Message, "SearchForOrganization", "", "");
                return organizations;
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

        public void UpdateParticipant(ParticipantsViewModel model)
        {
            try
            {
                SetMessages();
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var participantToUpdate = _db.tblParticipants.Find(model.ID);

                    participantToUpdate.FirstName = model.FirstName;
                    participantToUpdate.LastName = model.LastName;
                    participantToUpdate.Title = model.Title;
                    participantToUpdate.YearSignedOn = model.YearSignedOn;
                    participantToUpdate.Phone = model.Phone;
                    participantToUpdate.Email = model.Email;
                    participantToUpdate.Address1 = model.Address1;
                    participantToUpdate.Address2 = model.Address2;
                    participantToUpdate.State = model.State;
                    participantToUpdate.Zip = model.Zip;
                    participantToUpdate.HomePhone = model.HomePhone;
                    participantToUpdate.HomeEmail = model.HomeEmail;
                    participantToUpdate.MailPreference = model.MailPreference;
                    participantToUpdate.Active = model.Active;
                    participantToUpdate.PrimaryContact = model.PrimaryContact;
                    participantToUpdate.Training = model.Training;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        participantToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        participantToUpdate.UserLastModified = "Unknown";
                    }

                    participantToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    SetMessages("Success", "Participant Updated");
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateParticipant", "", "");
            }
        }

        public void DeleteParticipant(ParticipantsViewModel model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var participantToDelete = _db.tblParticipants.Find(model.ID);
                    _db.tblParticipants.Remove(participantToDelete);
                    _db.SaveChanges();

                    OrgID.Text = string.Empty;
                    OrganizationName.Text = string.Empty;

                    string successLabelText = string.Format("Participant Deleted: {0} {1} ", model.FirstName, model.LastName);
                    string redirect = string.Format("EditParticipants.aspx?organizationSearchTerm={0}&successLabelMessage={1}", model.OrganizationName, successLabelText);
                    //string redirect = string.Format("EditParticipants.aspx?organizationSearchTerm={0}", model.OrganizationName);

                    Response.Redirect(redirect, false);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteParticipant", "", "");
                }
            }
        }

        public void InsertParticipant(ParticipantsViewModel model)
        {
            try
            {
                var organizationID = Convert.ToInt32(OrgID.Text);
                string organizationName = OrganizationName.Text;

                string userCreated = "Unknown";
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    userCreated = HttpContext.Current.User.Identity.Name;
                }

                model.OrganizationID = organizationID;

                var newParticipant = new tblParticipant()
                {
                    OrganizationID = model.OrganizationID,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Title = model.Title,
                    YearSignedOn = model.YearSignedOn,
                    Phone = model.Phone,
                    Email = model.Email,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    HomePhone = model.HomePhone,
                    HomeEmail = model.HomeEmail,
                    MailPreference = model.MailPreference,
                    Active = model.Active,
                    PrimaryContact = model.PrimaryContact,
                    Training = model.Training,
                    DateCreated = DateTime.Now,
                    UserCreated = userCreated,
                    Valid = true
                };

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {                 
                    _db.tblParticipants.Add(newParticipant);
                    _db.SaveChanges();

                    //SetMessages("Success", "New Participant Added");

                    string successLabelText = string.Format("New Participant Added: {0} {1} ", model.FirstName, model.LastName);
                    string redirect = string.Format("EditParticipants.aspx?organizationSearchTerm={0}&successLabelMessage={1}", organizationName, successLabelText);
                    //string redirect = string.Format("EditParticipants.aspx?organizationSearchTerm={0}", organizationName);

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "InsertParticipant", "", "");
            }
        }

        protected void ExpWaterFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("New"))
            {
                OrganizationLabel.Visible = true;
                OrganizationName.Visible = true;
            }
            else if (e.CommandName.Equals("Cancel"))
            {               
                if (ExpWaterFormView != null && ExpWaterFormView.DataKey.Value == null)
                {
                    string organizationName = OrganizationName.Text;
                    string redirect = string.Format("EditParticipants.aspx?organizationSearchTerm={0}", organizationName);

                    Response.Redirect(redirect, false);
                }
            }
            else if (e.CommandName.Equals("Delete"))
            {
                if (ExpWaterFormView != null && ExpWaterFormView.DataKey.Value == null)
                {
                    string organizationName = OrganizationName.Text;
                    string redirect = string.Format("EditParticipants.aspx?organizationSearchTerm={0}", organizationName);

                    Response.Redirect(redirect, false);
                }
            }
            else
            {
                OrganizationLabel.Visible = false;
                OrganizationName.Visible = false;
            }
        }

        protected void ExpWaterFormView_ModeChanged(object sender, EventArgs e)
        {
            var test = e;
        }

        protected void ExpWaterFormView_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            var test = e;
        }
    }
}