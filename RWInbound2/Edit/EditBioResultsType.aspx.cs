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
    public partial class EditBioResultsType : System.Web.UI.Page
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

        public IQueryable<tlkBioResultsType> GetBioResultsTypes([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkBioResultsTypes.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkBioResultsType> bioResultsTypes = _db.tlkBioResultsTypes
                                                     .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return bioResultsTypes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBioResultsTypes", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditBioResultsType.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditBioResultsType.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForBioResultsTypesDescription(string prefixText, int count)
        {
            List<string> bioResultsTypesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    bioResultsTypesDescriptions = _db.tlkBioResultsTypes
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return bioResultsTypesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditBioResultsType editBioResultsType = new EditBioResultsType();
                editBioResultsType.HandleErrors(ex, ex.Message, "SearchForBioResultsTypesDescription", "", "");
                return bioResultsTypesDescriptions;
            }
        }

        public void UpdateBioResultsType(tlkBioResultsType model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var bioResultsTypeToUpdate = _db.tlkBioResultsTypes.Find(model.ID);

                    bioResultsTypeToUpdate.Code = model.Code;
                    bioResultsTypeToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        bioResultsTypeToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        bioResultsTypeToUpdate.UserLastModified = "Unknown";
                    }

                    bioResultsTypeToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    string successMsg = string.Format("Eco Region Updated: {0}", model.Description);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBioResultsType", "", "");
            }
        }

        public void DeleteBioResultsType(tlkBioResultsType model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var tblBenSampSampleNumber = (from bs in _db.tblBenSamps
                                                  join s in _db.Samples on bs.SampleID equals s.ID
                                                  where bs.BioResultGroupID == model.ID
                                                  select s.SampleNumber).FirstOrDefault();

                    if (!string.IsNullOrEmpty(tblBenSampSampleNumber))
                    {
                        string errorMsg
                            = string.Format("Eco Region {0} can not be deleted because it is assigned to the a Benthic Sample for Sample Number: {1}", model.Code, tblBenSampSampleNumber);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var bioResultsTypeToDelete = _db.tlkBioResultsTypes.Find(model.ID);
                        _db.tlkBioResultsTypes.Remove(bioResultsTypeToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Eco Region Deleted: {0}", model.Code);
                        SetMessages("Success", successMsg);
                    }                    
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteBioResultsType", "", "");
                }
            }
        }

        public void AddNewBioResultsType(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string strCode = ((TextBox)BioResultsTypeGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)BioResultsTypeGridView.FooterRow.FindControl("NewDescription")).Text;

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
                        var newBioResultsType = new tlkBioResultsType()
                        {
                            Code = code,
                            Description = description,
                            Valid = true,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newBioResultsType.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newBioResultsType.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkBioResultsTypes.Add(newBioResultsType);
                            _db.SaveChanges();

                            string successLabelText = "New Bio Results Type Added: " + newBioResultsType.Description;
                            string redirect = "EditBioResultsType.aspx?successLabelMessage=" + successLabelText;

                            Response.Redirect(redirect, false);
                        }
                    }                       
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBioResultsType", "", "");
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

        protected void BioResultsTypeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
    }
}