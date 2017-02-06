using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity.Validation;
using System.Reflection;

namespace RWInbound2.Edit
{
    public partial class EditActivityCategory : System.Web.UI.Page
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

        public IQueryable<tlkActivityCategory> GetActivityCategories([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkActivityCategories.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkActivityCategory> activityCategories = _db.tlkActivityCategories
                                                     .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return activityCategories;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetActivityCategories", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditActivityCategory.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditActivityCategory.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForActivityCategoriesDescription(string prefixText, int count)
        {
            List<string> activityCategoriesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    activityCategoriesDescriptions = _db.tlkActivityCategories
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return activityCategoriesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditActivityCategory editActivityCategory = new EditActivityCategory();
                editActivityCategory.HandleErrors(ex, ex.Message, "SearchForActivityCategoriesDescription", "", "");
                return activityCategoriesDescriptions;
            }
        }

        public void UpdateActivityCategory(tlkActivityCategory model)
        {
            try
            {
                SetMessages();
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var activityCategoryToUpdate = _db.tlkActivityCategories.Find(model.ID);

                    activityCategoryToUpdate.Code = model.Code;
                    activityCategoryToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        activityCategoryToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        activityCategoryToUpdate.UserLastModified = "Unknown";
                    }

                    activityCategoryToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    string successMsg = string.Format("Activity Category Updated: {0}", model.Description);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateActivityCategory", "", "");
            }
        }

        public void DeleteActivityCategory(tlkActivityCategory model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    SetMessages();

                    var tblBenSampSampleNumber = (from bs in _db.tblBenSamps
                                                  join s in _db.Samples on bs.SampleID equals s.ID
                                                  where bs.ActivityID == model.ID
                                                  select s.SampleNumber).FirstOrDefault();

                    if (!string.IsNullOrEmpty(tblBenSampSampleNumber))
                    {
                        string errorMsg
                            = string.Format("Activity Category {0} can not be deleted because it is assigned to one or more Benthic Samples. One Sample Number is: {1}", model.Description, tblBenSampSampleNumber);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var activityCategoryToDelete = _db.tlkActivityCategories.Find(model.ID);
                        _db.tlkActivityCategories.Remove(activityCategoryToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Activity Category Deleted: {0}", model.Description);
                        SetMessages("Success", successMsg);
                    }                   
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteActivityCategory", "", "");
                }
            }
        }

        public void AddNewActivityCategory(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string strCode = ((TextBox)ActivityCategoriesGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)ActivityCategoriesGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(strCode))
                {
                    SetMessages("Error", "Code field is required.");
                }
                else
                {
                    int code;
                    bool convertToInt = int.TryParse(strCode, out code);
                    if (!convertToInt)
                    {
                        SetMessages("Error", "Code field must be an integer number.");
                    }
                    else
                    {
                        var newActivityCategory = new tlkActivityCategory()
                        {
                            Code = code,
                            Description = description,
                            Valid = true,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newActivityCategory.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newActivityCategory.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkActivityCategories.Add(newActivityCategory);
                            _db.SaveChanges();

                            string successLabelText = "New Activity Category Added: " + newActivityCategory.Description;
                            string redirect = "EditActivityCategory.aspx?successLabelMessage=" + successLabelText;

                            Response.Redirect(redirect, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewActivityCategory", "", "");
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

        protected void ActivityCategoriesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
    }
}