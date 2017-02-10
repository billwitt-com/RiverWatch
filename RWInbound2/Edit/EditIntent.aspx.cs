using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditIntent : System.Web.UI.Page
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

        public IQueryable<tlkIntent> GetIntents([QueryString]string descriptionSearchTerm = "",
                                                        [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }
               
                IQueryable<tlkIntent> intents = _db.tlkIntents
                                                   .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                  = typeof(System.Collections.Specialized.NameValueCollection)
                          .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return intents;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetIntents", "", "");
                return null;
            }
        }

        public void UpdateIntent(tlkIntent model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var intentToUpdate = _db.tlkIntents.Find(model.ID);

                    intentToUpdate.Code = model.Code;
                    intentToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        intentToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        intentToUpdate.UserLastModified = "Unknown";
                    }

                    intentToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    string successMsg = string.Format("Intent Updated: {0}", model.Description);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateIntent", "", "");
            }
        }

        public void DeleteIntent(tlkIntent model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    SetMessages();

                    var tblBenSampSampleNumbers = (from bs in _db.tblBenSamps
                                                   join s in _db.Samples on bs.SampleID equals s.ID
                                                   where bs.Intent == model.ID
                                                   orderby s.SampleNumber
                                                   select s.SampleNumber).ToList();

                    if (tblBenSampSampleNumbers.Count > 0)
                    {
                        string errorMsg
                            = string.Format("Intent {1} can not be deleted because it is assigned to one or more Benthic Samples. {0}",
                                            "\r\n Click the Download Assigned Samples button to get a list of the samples.", model.Code);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var intentToDelete = _db.tlkIntents.Find(model.ID);
                        _db.tlkIntents.Remove(intentToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Intent Deleted: {0}", model.Description);
                        SetMessages("Success", successMsg);
                    }
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteIntent", "", "");
                }
            }
        }

        public void AddNewIntent(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string strCode = ((TextBox)IntentGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)IntentGridView.FooterRow.FindControl("NewDescription")).Text;

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
                        var newIntent = new tlkIntent()
                        {
                            Code = code,
                            Description = description,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newIntent.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newIntent.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkIntents.Add(newIntent);
                            _db.SaveChanges();

                            string successLabelText = "New Intent Added: " + newIntent.Description;
                            string redirect = "EditIntent.aspx?successLabelMessage=" + successLabelText;

                            Response.Redirect(redirect, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewIntent", "", "");
            }
        }

        protected void IntentGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GetAssignedSamples")
                {
                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        if (e.CommandName == "GetAssignedSamples")
                        {
                            int iD = Convert.ToInt32(e.CommandArgument);

                            // Retrieve the row that contains the button 
                            // from the Rows collection.
                            //GridViewRow row = FieldProceduresGridView.Rows[iD];

                            var tblBenSampSampleNumbers = (from bs in _db.tblBenSamps
                                                           join s in _db.Samples on bs.SampleID equals s.ID
                                                           where bs.Intent == iD
                                                           orderby s.SampleNumber
                                                           select s.SampleNumber).Distinct().ToList();

                            if (tblBenSampSampleNumbers.Count > 0)
                            {
                                //return csv to show Assigned Sample numbers
                                Response.ClearContent();
                                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Assigned_SampleNumbers.csv"));
                                Response.ContentType = "application/text";
                                string csvContents = GetSamplesCSVData(tblBenSampSampleNumbers);
                                Response.Write(csvContents);
                                Response.End();
                            }
                            else
                            {
                                SetMessages("Error", "There are 0 Benthic Samples assigned to this item.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "IntentGridView_RowCommand", "", "");
            }
        }

        private string GetSamplesCSVData(List<string> listOfSamples)
        {
            StringBuilder strbldr = new StringBuilder();
            foreach (var sample in listOfSamples)
            {
                strbldr.AppendLine(sample);
            }
            return strbldr.ToString();
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

        protected void IntentGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
    }
}