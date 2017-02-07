using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class GearConfig : System.Web.UI.Page
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

        public IQueryable<tlkFieldGear> BindFieldGears()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkFieldGearList = _db.tlkFieldGears
                                           .OrderBy(ws => ws.Description);
                return tlkFieldGearList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindFieldGears", "", "");
                return null;
            }
        }

        public IQueryable<tlkGearConfig> GetGearConfigs([QueryString]string descriptionSearchTerm = "",
                                                            [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }
                
                IQueryable<tlkGearConfig> gearConfigs = _db.tlkGearConfigs
                                                           .OrderBy(gc => gc.Description);

                PropertyInfo isreadonly
                  = typeof(System.Collections.Specialized.NameValueCollection)
                          .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return gearConfigs;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetFieldProcedures", "", "");
                return null;
            }
        }

        public void UpdateGearConfig(tlkGearConfig model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var gearConfigToUpdate = _db.tlkGearConfigs.Find(model.ID);

                    gearConfigToUpdate.Code = model.Code;
                    gearConfigToUpdate.Description = model.Description;
                    gearConfigToUpdate.FieldGearID = model.FieldGearID;//Convert.ToInt32(model.FieldGearID);

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        gearConfigToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        gearConfigToUpdate.UserLastModified = "Unknown";
                    }

                    gearConfigToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    string successMsg = string.Format("Field Gear Config Updated: {0}", model.Description);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateGearConfig", "", "");
            }
        }

        public void DeleteGearConfig(tlkGearConfig model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    SetMessages();

                    var tblBenSampSampleNumber = (from bs in _db.tblBenSamps
                                                  join s in _db.Samples on bs.SampleID equals s.ID
                                                  where bs.GearConfigID == model.ID
                                                  select s.SampleNumber).FirstOrDefault();

                    if (!string.IsNullOrEmpty(tblBenSampSampleNumber))
                    {
                        string errorMsg
                            = string.Format("Field Gear Configuration {0} can not be deleted because it is assigned to one or more Benthic Samples. One Sample Number is: {1}", model.Code, tblBenSampSampleNumber);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var gearConfigToDelete = _db.tlkGearConfigs.Find(model.ID);
                        _db.tlkGearConfigs.Remove(gearConfigToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Field Gear Configuration Deleted: {0}", model.Description);
                        SetMessages("Success", successMsg);
                    }
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteGearConfig", "", "");
                }
            }
        }

        public void AddNewGearConfig(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string strCode = ((TextBox)GearConfigGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)GearConfigGridView.FooterRow.FindControl("NewDescription")).Text;
                int fieldGearID = Convert.ToInt32(((DropDownList)GearConfigGridView.FooterRow.FindControl("NewFieldGearID")).SelectedValue);

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
                        var newGearConfig = new tlkGearConfig()
                        {
                            Code = code,
                            Description = description,
                            FieldGearID = fieldGearID,
                            Valid = true,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newGearConfig.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newGearConfig.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkGearConfigs.Add(newGearConfig);
                            _db.SaveChanges();

                            string successLabelText = "New Field Gear Configuration Added: " + newGearConfig.Description;
                            string redirect = "GearConfig.aspx?successLabelMessage=" + successLabelText;

                            Response.Redirect(redirect, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewGearConfig", "", "");
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

        protected void GearConfigGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
    }
}