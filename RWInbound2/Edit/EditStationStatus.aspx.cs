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
    public partial class EditStationStatus : System.Web.UI.Page
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

        public IQueryable<tlkStationStatu> GetStationStatus([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkStationStatus.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkStationStatu> stationStatus = _db.tlkStationStatus
                                               .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return stationStatus;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetStationStatus", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditStationStatus.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditStationStatus.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForStationStatusDescription(string prefixText, int count)
        {
            List<string> stationStatusDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    stationStatusDescriptions = _db.tlkStationStatus
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return stationStatusDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditStationStatus editStationStatus = new EditStationStatus();
                editStationStatus.HandleErrors(ex, ex.Message, "SearchForStationStatusDescription", "", "");
                return stationStatusDescriptions;
            }
        }

        public void UpdateStationStatus(tlkStationStatu model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var stationStatusToUpdate = _db.tlkStationStatus.Find(model.ID);

                    stationStatusToUpdate.Code = model.Code;
                    stationStatusToUpdate.Description = model.Description;
                    stationStatusToUpdate.ActiveFlag = model.ActiveFlag;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        stationStatusToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        stationStatusToUpdate.UserLastModified = "Unknown";
                    }

                    stationStatusToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Station Status Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateStationStatus", "", "");
            }
        }

        public void DeleteStationStatus(tlkStationStatu model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var stationStatusToDelete = _db.tlkStationStatus.Find(model.ID);
                    _db.tlkStationStatus.Remove(stationStatusToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Station Status Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteStationStatus", "", "");
                }
            }
        }

        public void AddNewStationStatus(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)StationStatusGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)StationStatusGridView.FooterRow.FindControl("NewDescription")).Text;
                bool activeFlag = ((CheckBox)StationStatusGridView.FooterRow.FindControl("ActiveFlag")).Checked;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newStationStatus = new tlkStationStatu()
                    {
                        Code = code,
                        Description = description,
                        ActiveFlag = activeFlag,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newStationStatus.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newStationStatus.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkStationStatus.Add(newStationStatus);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Station Status Added: " + newStationStatus.Description;
                        string redirect = "EditStationStatus.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewStationStatus", "", "");
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