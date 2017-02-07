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
    public partial class EditFieldGear : System.Web.UI.Page
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

        public IQueryable<tlkFieldGear> GetFieldGear([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkFieldGears.Where(c => c.Description.Equals(descriptionSearchTerm))
                                                 .OrderBy(c => c.Code);
                }
                IQueryable<tlkFieldGear> fieldGear = _db.tlkFieldGears
                                                     .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                  = typeof(System.Collections.Specialized.NameValueCollection)
                          .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return fieldGear;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetFieldGear", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditFieldGear.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditFieldGear.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForFieldGearDescription(string prefixText, int count)
        {
            List<string> fieldGearDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    fieldGearDescriptions = _db.tlkFieldGears
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return fieldGearDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditFieldGear editFieldGear = new EditFieldGear();
                editFieldGear.HandleErrors(ex, ex.Message, "SearchForFieldGearDescription", "", "");
                return fieldGearDescriptions;
            }
        }

        public void UpdateFieldGear(tlkFieldGear model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var fieldGearToUpdate = _db.tlkFieldGears.Find(model.ID);

                    fieldGearToUpdate.Code = model.Code;
                    fieldGearToUpdate.Description = model.Description;
                    fieldGearToUpdate.F5 = model.F5;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        fieldGearToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        fieldGearToUpdate.UserLastModified = "Unknown";
                    }

                    fieldGearToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    string successMsg = string.Format("Field Gear Updated: {0}", model.Description);
                    SetMessages("Success", successMsg);                  
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateFieldGear", "", "");
            }
        }

        public void DeleteFieldGear(tlkFieldGear model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var tblBenSampSampleNumber = (from bs in _db.tblBenSamps
                                                  join s in _db.Samples on bs.SampleID equals s.ID
                                                  where bs.FieldGearID == model.ID
                                                  select s.SampleNumber).FirstOrDefault();

                    if (!string.IsNullOrEmpty(tblBenSampSampleNumber))
                    {
                        string errorMsg
                            = string.Format("Field Gear {0} can not be deleted because it is assigned to one or more Benthic Samples. One Sample Number is: {1}", model.Code, tblBenSampSampleNumber);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var fieldGearToDelete = _db.tlkFieldGears.Find(model.ID);
                        _db.tlkFieldGears.Remove(fieldGearToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Field Gear Deleted: {0}", model.Description);
                        SetMessages("Success", successMsg);
                    }                    
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteFieldGear", "", "");
                }
            }
        }

        public void AddNewFieldGear(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string code = ((TextBox)FieldGearGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)FieldGearGridView.FooterRow.FindControl("NewDescription")).Text;
                string f5 = ((TextBox)FieldGearGridView.FooterRow.FindControl("NewF5")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SetMessages("Error", "Code field is required.");
                }
                else
                {
                    var newFieldGear = new tlkFieldGear()
                    {
                        Code = code,
                        Description = description,
                        F5 = f5,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newFieldGear.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newFieldGear.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkFieldGears.Add(newFieldGear);
                        _db.SaveChanges();

                        string successLabelText = "New Field Gear Added: " + newFieldGear.Description;
                        string redirect = "EditFieldGear.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewFieldGear", "", "");
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

        protected void FieldGearGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
    }
}