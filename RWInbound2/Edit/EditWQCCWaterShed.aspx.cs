using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditWQCCWaterShed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }

        public IQueryable<tlkWQCCWaterShed> GetWQCCWaterSheds([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkWQCCWaterSheds.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkWQCCWaterShed> wQCCWaterSheds = _db.tlkWQCCWaterSheds
                                               .OrderBy(c => c.Code);
                return wQCCWaterSheds;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetWQCCWaterSheds", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditWQCCWaterShed.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditWQCCWaterShed.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForWQCCWaterShedsDescription(string prefixText, int count)
        {
            List<string> wQCCWaterShedsDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    wQCCWaterShedsDescriptions = _db.tlkWQCCWaterSheds
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return wQCCWaterShedsDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditWQCCWaterShed editWQCCWaterShed = new EditWQCCWaterShed();
                editWQCCWaterShed.HandleErrors(ex, ex.Message, "SearchForWQCCWaterShedsDescription", "", "");
                return wQCCWaterShedsDescriptions;
            }
        }

        public void UpdateWQCCWaterShed(tlkWQCCWaterShed model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var wQCCWaterShedToUpdate = _db.tlkWQCCWaterSheds.Find(model.ID);

                    wQCCWaterShedToUpdate.Code = model.Code;
                    wQCCWaterShedToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        wQCCWaterShedToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        wQCCWaterShedToUpdate.UserLastModified = "Unknown";
                    }

                    wQCCWaterShedToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "WQCC Water Shed Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateWQCCWaterShed", "", "");
            }
        }

        public void DeleteWQCCWaterShed(tlkWQCCWaterShed model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var wQCCWaterShedToDelete = _db.tlkWQCCWaterSheds.Find(model.ID);
                    _db.tlkWQCCWaterSheds.Remove(wQCCWaterShedToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "WQCC Water Shed Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteWQCCWaterShed", "", "");
                }
            }
        }

        public void AddNewWQCCWaterShed(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)WQCCWaterShedsGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)WQCCWaterShedsGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newWQCCWaterShed = new tlkWQCCWaterShed()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newWQCCWaterShed.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newWQCCWaterShed.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkWQCCWaterSheds.Add(newWQCCWaterShed);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New WQCC Water Shed Added: " + newWQCCWaterShed.Description;
                        string redirect = "EditWQCCWaterShed.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewWQCCWaterShed", "", "");
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