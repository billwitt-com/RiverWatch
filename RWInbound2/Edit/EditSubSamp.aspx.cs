using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditSubSamp : System.Web.UI.Page
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

        public IQueryable<tblSubSamp> GetSubSamps([QueryString]string sampleIDSearchSearchTerm = "",
                                                  [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (!string.IsNullOrEmpty(sampleIDSearchSearchTerm))
                {
                    int sampleID = Convert.ToInt32(sampleIDSearchSearchTerm);
                    return _db.tblSubSamp
                              .Where(s => s.SampleID == sampleID)
                               .OrderBy(s => s.SampleID);
                }

                IQueryable<tblSubSamp> subSamps = _db.tblSubSamp
                                                     .OrderBy(s => s.SampleID);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return subSamps;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetSubSamps", "", "");
                return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForSampleID(string prefixText, int count)
        {
            List<string> sampleIDs = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    sampleIDs = _db.tblSubSamp
                                        .Where(s => s.SampleID.ToString().StartsWith(prefixText))                                        
                                        .OrderBy(s => s.SampleID.ToString())
                                        .Select(s => s.SampleID.ToString()).Distinct().ToList();

                    sampleIDs.Sort();

                    return sampleIDs;
                }
            }
            catch (Exception ex)
            {
                EditSubSamp editSubSamp = new EditSubSamp();
                editSubSamp.HandleErrors(ex, ex.Message, "SearchForSampleID", "", "");
                return sampleIDs;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sampleIDSearchSearchTerm = sampleIDSearch.Text;
                string redirect = "EditSubSamp.aspx?sampleIDSearchSearchTerm=" + sampleIDSearchSearchTerm;

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
                Response.Redirect("EditSubSamp.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdateSubSamp(tblSubSamp model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var subSampToUpdate = _db.tblSubSamp.Find(model.SubstrateID);
                    TryUpdateModel(subSampToUpdate);                    

                    _db.SaveChanges();
                    string successMsg = string.Format("Sub Samp Updated: {0}", subSampToUpdate.SampleID);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateSubSamp", "", "");
            }
        }

        public void DeleteSubSamp(tblSubSamp model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var subSampToDelete = _db.tblSubSamp.Find(model.SubstrateID);
                    _db.tblSubSamp.Remove(subSampToDelete);
                    _db.SaveChanges();

                    SetMessages("Success", "Sub Samp Deleted for SampleID: " + subSampToDelete.SampleID);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteSubSamp", "", "");
                }
            }
        }

        public void AddNewSubSamp(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                bool errors = false;

                if (string.IsNullOrEmpty(((TextBox)SubSampGridView.FooterRow.FindControl("NewRepNum")).Text))
                {
                    ((Label)SubSampGridView.FooterRow.FindControl("lblNewRepNumRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)SubSampGridView.FooterRow.FindControl("NewParaID")).Text))
                {
                    ((Label)SubSampGridView.FooterRow.FindControl("lblNewParaIDRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)SubSampGridView.FooterRow.FindControl("NewSampleID")).Text))
                {
                    ((Label)SubSampGridView.FooterRow.FindControl("lblNewSampleIDRequired")).Visible = true;
                    errors = true;
                }               
                if (string.IsNullOrEmpty(((TextBox)SubSampGridView.FooterRow.FindControl("NewStoretUploaded")).Text))
                {
                    ((Label)SubSampGridView.FooterRow.FindControl("lblNewStoretUploadedRequired")).Visible = true;
                    errors = true;
                }
                if (errors)
                {
                    SetMessages("Error", "Can't create new Sub Samp because Required fields are blank.");
                    ((Button)SubSampGridView.FooterRow.FindControl("btnAdd")).Focus();
                }
                else
                {
                    decimal? value = 0;
                    if (!string.IsNullOrEmpty(((TextBox)SubSampGridView.FooterRow.FindControl("NewValue")).Text))
                    {
                        value = Convert.ToDecimal(((TextBox)SubSampGridView.FooterRow.FindControl("NewValue")).Text);
                    }
                    int repNum = Convert.ToInt32(((TextBox)SubSampGridView.FooterRow.FindControl("NewRepNum")).Text);
                    int paraID = Convert.ToInt32(((TextBox)SubSampGridView.FooterRow.FindControl("NewParaID")).Text);
                    int sampleID = Convert.ToInt32(((TextBox)SubSampGridView.FooterRow.FindControl("NewSampleID")).Text);
                    string storetUploaded = ((TextBox)SubSampGridView.FooterRow.FindControl("NewStoretUploaded")).Text;

                    DateTime? enterDate = null;
                    if (!string.IsNullOrEmpty(((TextBox)SubSampGridView.FooterRow.FindControl("NewEnterDate")).Text))
                    {
                        enterDate = Convert.ToDateTime(((TextBox)SubSampGridView.FooterRow.FindControl("NewEnterDate")).Text);
                    }

                    var newSubSamp = new tblSubSamp()
                    {
                        RepNum = repNum,
                        ParaID = paraID,
                        Value = value,
                        SampleID = sampleID,
                        StoretUploaded = storetUploaded,
                        EnterDate = enterDate,
                        Valid = true
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblSubSamp.Add(newSubSamp);
                        _db.SaveChanges();

                        string successLabelText = "New Sub Samp Added: " + newSubSamp.SampleID;
                        string redirect = "EditSubSamp.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }                
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewSubSamp", "", "");
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
    }
}