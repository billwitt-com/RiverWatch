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
    public partial class EditRiverWatchWaterShed : System.Web.UI.Page
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

        public IQueryable<tlkRiverWatchWaterShed> GetRiverWatchWaterSheds([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkRiverWatchWaterSheds.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkRiverWatchWaterShed> riverWatchWaterSheds = _db.tlkRiverWatchWaterSheds
                                               .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return riverWatchWaterSheds;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetRiverWatchWaterSheds", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditRiverWatchWaterShed.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditRiverWatchWaterShed.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForRiverWatchWaterShedsDescription(string prefixText, int count)
        {
            List<string> riverWatchWaterShedsDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    riverWatchWaterShedsDescriptions = _db.tlkRiverWatchWaterSheds
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return riverWatchWaterShedsDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditRiverWatchWaterShed editRiverWatchWaterShed = new EditRiverWatchWaterShed();
                editRiverWatchWaterShed.HandleErrors(ex, ex.Message, "SearchForRiverWatchWaterShedsDescription", "", "");
                return riverWatchWaterShedsDescriptions;
            }
        }

        public void UpdateRiverWatchWaterShed(tlkRiverWatchWaterShed model)
        {
            try
            {  
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var riverWatchWaterShedToUpdate = _db.tlkRiverWatchWaterSheds.Find(model.ID);

                    riverWatchWaterShedToUpdate.Code = model.Code;
                    riverWatchWaterShedToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        riverWatchWaterShedToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        riverWatchWaterShedToUpdate.UserLastModified = "Unknown";
                    }

                    riverWatchWaterShedToUpdate.DateLastModified = DateTime.Now;                    
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "River Watch Water Shed Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateRiverWatchWaterShed", "", "");
            }
        }

        public void DeleteRiverWatchWaterShed(tlkRiverWatchWaterShed model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var riverWatchWaterShedToDelete = _db.tlkRiverWatchWaterSheds.Find(model.ID);
                    _db.tlkRiverWatchWaterSheds.Remove(riverWatchWaterShedToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "River Watch Water Shed Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteRiverWatchWaterShed", "", "");
                }
            }
        }

        public void AddNewRiverWatchWaterShed(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)RiverWatchWaterShedsGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)RiverWatchWaterShedsGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newRiverWatchWaterShed = new tlkRiverWatchWaterShed()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newRiverWatchWaterShed.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newRiverWatchWaterShed.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkRiverWatchWaterSheds.Add(newRiverWatchWaterShed);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New River Watch Water Shed Added: " + newRiverWatchWaterShed.Description;
                        string redirect = "EditRiverWatchWaterShed.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewRiverWatchWaterShed", "", "");
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