using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditOrganizationType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
            ErrorLabel.Text = "";
            SuccessLabel.Text = "";
        }

        public IQueryable<tlkOrganizationType> GetOrganizationTypes([QueryString]string descriptionSearchTerm = "",
                                                      [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = successLabelMessage;
                }

                if (!string.IsNullOrEmpty(descriptionSearchTerm))
                {
                    return _db.tlkOrganizationTypes.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkOrganizationType> organizationTypes = _db.tlkOrganizationTypes
                                               .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return organizationTypes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetOrganizationTypes", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditOrganizationType.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditOrganizationType.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForOrganizationTypesDescription(string prefixText, int count)
        {
            List<string> organizationTypesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    organizationTypesDescriptions = _db.tlkOrganizationTypes
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return organizationTypesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditOrganizationType editOrganizationType = new EditOrganizationType();
                editOrganizationType.HandleErrors(ex, ex.Message, "SearchForOrganizationTypesDescription", "", "");
                return organizationTypesDescriptions;
            }
        }

        public void UpdateOrganizationType(tlkOrganizationType model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var organizationTypeToUpdate = _db.tlkOrganizationTypes.Find(model.ID);

                    organizationTypeToUpdate.Code = model.Code;
                    organizationTypeToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        organizationTypeToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        organizationTypeToUpdate.UserLastModified = "Unknown";
                    }

                    organizationTypeToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Organization Type Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateOrganizationType", "", "");
            }
        }

        public void DeleteOrganizationType(tlkOrganizationType model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var organizationTypeToDelete = _db.tlkOrganizationTypes.Find(model.ID);
                    _db.tlkOrganizationTypes.Remove(organizationTypeToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = " Organization Type Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteOrganizationType", "", "");
                }
            }
        }

        public void AddNewOrganizationType(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)OrganizationTypesGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)OrganizationTypesGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newOrganizationType = new tlkOrganizationType()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newOrganizationType.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newOrganizationType.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkOrganizationTypes.Add(newOrganizationType);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Organization Type Added: " + newOrganizationType.Description;
                        string redirect = "EditOrganizationType.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewOrganizationType", "", "");
            }
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SuccessLabel.Text = "";

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
                ErrorLabel.Text = sb.ToString();
            }
            else
            {
                SuccessLabel.Text = "";
                ErrorLabel.Text = ex.Message;
            }
        }
    }
}