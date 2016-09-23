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
    public partial class EditRange : System.Web.UI.Page
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

        public IQueryable<tlkRange> GetRanges([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkRanges.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkRange> ranges = _db.tlkRanges
                                               .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return ranges;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetRanges", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditRange.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditRange.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForRangesDescription(string prefixText, int count)
        {
            List<string> rangesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    rangesDescriptions = _db.tlkRanges
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return rangesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditRange editRange = new EditRange();
                editRange.HandleErrors(ex, ex.Message, "SearchForRangesDescription", "", "");
                return rangesDescriptions;
            }
        }

        public void UpdateRange(tlkRange model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var rangeToUpdate = _db.tlkRanges.Find(model.ID);

                    rangeToUpdate.Code = model.Code;
                    rangeToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        rangeToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        rangeToUpdate.UserLastModified = "Unknown";
                    }

                    rangeToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Range Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateRange", "", "");
            }
        }

        public void DeleteRange(tlkRange model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var rangeToDelete = _db.tlkRanges.Find(model.ID);
                    _db.tlkRanges.Remove(rangeToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Range Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteRange", "", "");
                }
            }
        }

        public void AddNewRange(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)RangesGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)RangesGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newRange = new tlkRange()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newRange.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newRange.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkRanges.Add(newRange);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Range Added: " + newRange.Description;
                        string redirect = "EditRange.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewRange", "", "");
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