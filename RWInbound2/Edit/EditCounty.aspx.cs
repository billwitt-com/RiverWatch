using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Data.Entity.Validation;
using System.Text;
using System.Reflection;

namespace RWInbound2.Edit
{
    public partial class EditCounty : System.Web.UI.Page
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

        public IQueryable<tlkCounty> GetCounties([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkCounties.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkCounty> counties = _db.tlkCounties
                                                     .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return counties;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetCounties", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditCounty.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditCounty.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForCountiesDescription(string prefixText, int count)
        {
            List<string> countiesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    countiesDescriptions = _db.tlkCounties
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return countiesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditCounty editCounty = new EditCounty();
                editCounty.HandleErrors(ex, ex.Message, "SearchForCountiesDescription", "", "");
                return countiesDescriptions;
            }
        }

        public void UpdateCounty(tlkCounty model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var countyToUpdate = _db.tlkCounties.Find(model.ID);

                    countyToUpdate.Code = model.Code;
                    countyToUpdate.Description = model.Description;
                    //activityCategoryToUpdate.Valid = 
                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        countyToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        countyToUpdate.UserLastModified = "Unknown";
                    }

                    countyToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "County Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateCounty", "", "");
            }
        }

        public void DeleteCounty(tlkCounty model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var countyToDelete = _db.tlkCounties.Find(model.ID);
                    _db.tlkCounties.Remove(countyToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "County Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteCounty", "", "");
                }
            }
        }

        public void AddNewCounty(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)CountyGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)CountyGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newCounty = new tlkCounty()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newCounty.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newCounty.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkCounties.Add(newCounty);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New County Added: " + newCounty.Description;
                        string redirect = "EditCounty.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewCounty", "", "");
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