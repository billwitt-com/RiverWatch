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
    public partial class EditActivityType : System.Web.UI.Page
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

        public IQueryable<tlkActivityType> GetActivityTypes([QueryString]string descriptionSearchTerm = "",
                                                       [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (!string.IsNullOrEmpty(descriptionSearchTerm))
                {
                    return _db.tlkActivityTypes.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkActivityType> activityTypes = _db.tlkActivityTypes
                                                     .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return activityTypes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetActivityTypes", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditActivityType.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditActivityType.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForActivityTypesDescription(string prefixText, int count)
        {
            List<string> activityTypesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    activityTypesDescriptions = _db.tlkActivityTypes
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return activityTypesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditActivityType editActivityType = new EditActivityType();
                editActivityType.HandleErrors(ex, ex.Message, "SearchForActivityTypesDescription", "", "");
                return activityTypesDescriptions;
            }
        }

        public void UpdateActivityType(tlkActivityType model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var activityTypeToUpdate = _db.tlkActivityTypes.Find(model.Code);

                    activityTypeToUpdate.Code = model.Code;
                    activityTypeToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        activityTypeToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        activityTypeToUpdate.UserLastModified = "Unknown";
                    }

                    activityTypeToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    string successMsg = string.Format("Activity Type Updated: {0}", model.Code);                        
                    SetMessages("Success", successMsg);                 
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateActivityType", "", "");
            }
        }

        public void DeleteActivityType(tlkActivityType model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var tblBenSampSampleNumber = (from bs in _db.tblBenSamps
                                                  join s in _db.Samples on bs.SampleID equals s.ID
                                                  where bs.ActivityTypeID == model.ID
                                                  select s.SampleNumber).FirstOrDefault();

                    if (!string.IsNullOrEmpty(tblBenSampSampleNumber))
                    {
                        string errorMsg
                            = string.Format("Activity Type {0} can not be deleted because it is assigned to one or more Benthic Samples. One Sample Number is: {1}", model.Code, tblBenSampSampleNumber);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var activityTypeToDelete = _db.tlkActivityTypes.Find(model.ID);
                        _db.tlkActivityTypes.Remove(activityTypeToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Activity Type Deleted: {0}", model.Code);
                        SetMessages("Success", successMsg);
                    }                  
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteActivityType", "", "");
                }
            }
        }

        public void AddNewActivityType(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string code = ((TextBox)ActivityTypeGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)ActivityTypeGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SetMessages("Error", "Code field is required.");
                }
                else
                {
                    var newActivityType = new tlkActivityType()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newActivityType.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newActivityType.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkActivityTypes.Add(newActivityType);
                        _db.SaveChanges();

                        string successLabelText = "New Activity Type Added: " + newActivityType.Description;
                        string redirect = "EditActivityType.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewActivityType", "", "");
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

        protected void ActivityTypeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
    }
}