using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditWSG : System.Web.UI.Page
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

        public IQueryable<tlkWSG> GetWSGs([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkWSGs.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkWSG> wSGs = _db.tlkWSGs
                                               .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                  = typeof(System.Collections.Specialized.NameValueCollection)
                          .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return wSGs;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetWSGs", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditWSG.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditWSG.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForWSGsDescription(string prefixText, int count)
        {
            List<string> wSGsDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    wSGsDescriptions = _db.tlkWSGs
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return wSGsDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditWSG editWSG = new EditWSG();
                editWSG.HandleErrors(ex, ex.Message, "SearchForWSGsDescription", "", "");
                return wSGsDescriptions;
            }
        }

        public void UpdateWSG(tlkWSG model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var wSGToUpdate = _db.tlkWSGs.Find(model.ID);

                    wSGToUpdate.Code = model.Code;
                    wSGToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        wSGToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        wSGToUpdate.UserLastModified = "Unknown";
                    }

                    wSGToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "WSG Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateWSG", "", "");
            }
        }

        public void DeleteWSG(tlkWSG model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var wSGToDelete = _db.tlkWSGs.Find(model.ID);
                    _db.tlkWSGs.Remove(wSGToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "WSG Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteWSG", "", "");
                }
            }
        }

        public void AddNewWSG(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)WSGsGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)WSGsGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newWSG = new tlkWSG()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newWSG.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newWSG.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkWSGs.Add(newWSG);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New WSG Added: " + newWSG.Description;
                        string redirect = "EditWSG.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewWSG", "", "");
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