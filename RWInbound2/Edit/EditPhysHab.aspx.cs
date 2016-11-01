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
    public partial class EditPhysHab : System.Web.UI.Page
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

        public IQueryable<tblPhysHab> GetPhysHabs([QueryString]string sampleIDSearchSearchTerm = "",
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
                    return _db.tblPhysHab
                              .Where(p => p.SampleID == sampleID)
                               .OrderBy(p => p.SampleID);
                }

                IQueryable<tblPhysHab> physHabs = _db.tblPhysHab
                                                     .OrderBy(p => p.SampleID);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return physHabs;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetPhysHabs", "", "");
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
                    sampleIDs = _db.tblPhysHab
                                    .Where(p => p.SampleID.ToString().StartsWith(prefixText))
                                    .OrderBy(p => p.SampleID.ToString())
                                    .Select(p => p.SampleID.ToString()).Distinct().ToList();

                    sampleIDs.Sort();

                    return sampleIDs;
                }
            }
            catch (Exception ex)
            {
                EditPhysHab editPhysHab = new EditPhysHab();
                editPhysHab.HandleErrors(ex, ex.Message, "SearchForSampleID", "", "");
                return sampleIDs;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sampleIDSearchSearchTerm = sampleIDSearch.Text;
                string redirect = "EditPhysHab.aspx?sampleIDSearchSearchTerm=" + sampleIDSearchSearchTerm;

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
                Response.Redirect("EditPhysHab.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdatePhysHab(tblPhysHab model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var physHabToUpdate = _db.tblPhysHab.Find(model.ID);
                    TryUpdateModel(physHabToUpdate);

                    _db.SaveChanges();
                    string successMsg = string.Format("Phys Hab Updated: {0}", physHabToUpdate.SampleID);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdatePhysHab", "", "");
            }
        }

        public void DeletePhysHab(tblPhysHab model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var physHabToDelete = _db.tblPhysHab.Find(model.ID);
                    _db.tblPhysHab.Remove(physHabToDelete);
                    _db.SaveChanges();

                    SetMessages("Success", "Phys Hab Deleted for SampleID: " + physHabToDelete.SampleID);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeletePhysHab", "", "");
                }
            }
        }

        public void AddNewPhysHab(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                bool errors = false;

                if (string.IsNullOrEmpty(((TextBox)PhysHabGridView.FooterRow.FindControl("NewRepNum")).Text))
                {
                    ((Label)PhysHabGridView.FooterRow.FindControl("lblNewRepNumRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)PhysHabGridView.FooterRow.FindControl("NewParaID")).Text))
                {
                    ((Label)PhysHabGridView.FooterRow.FindControl("lblNewParaIDRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)PhysHabGridView.FooterRow.FindControl("NewSampleID")).Text))
                {
                    ((Label)PhysHabGridView.FooterRow.FindControl("lblNewSampleIDRequired")).Visible = true;
                    errors = true;
                }                
                if (string.IsNullOrEmpty(((TextBox)PhysHabGridView.FooterRow.FindControl("NewStoretUploaded")).Text))
                {
                    ((Label)PhysHabGridView.FooterRow.FindControl("lblNewStoretUploadedRequired")).Visible = true;
                    errors = true;
                }
                if (errors)
                {
                    SetMessages("Error", "Can't create new Phys Hab because Required fields are blank.");
                    ((Button)PhysHabGridView.FooterRow.FindControl("btnAdd")).Focus();
                }
                else
                {
                    decimal? value = 0;
                    if (!string.IsNullOrEmpty(((TextBox)PhysHabGridView.FooterRow.FindControl("NewValue")).Text))
                    {
                        value = Convert.ToDecimal(((TextBox)PhysHabGridView.FooterRow.FindControl("NewValue")).Text);
                    }
                    int repNum = Convert.ToInt32(((TextBox)PhysHabGridView.FooterRow.FindControl("NewRepNum")).Text);
                    int paraID = Convert.ToInt32(((TextBox)PhysHabGridView.FooterRow.FindControl("NewParaID")).Text);
                    int sampleID = Convert.ToInt32(((TextBox)PhysHabGridView.FooterRow.FindControl("NewSampleID")).Text);
                    string storetUploaded = ((TextBox)PhysHabGridView.FooterRow.FindControl("NewStoretUploaded")).Text;
                    string comment = ((TextBox)PhysHabGridView.FooterRow.FindControl("NewComment")).Text;

                    DateTime? enterDate = null;
                    if (!string.IsNullOrEmpty(((TextBox)PhysHabGridView.FooterRow.FindControl("NewEnterDate")).Text))
                    {
                        enterDate = Convert.ToDateTime(((TextBox)PhysHabGridView.FooterRow.FindControl("NewEnterDate")).Text);
                    }

                    var newPhysHab = new tblPhysHab()
                    {
                        RepNum = repNum,
                        ParaID = paraID,
                        Value = value,
                        EnterDate = enterDate,
                        Comment = comment,
                        SampleID = sampleID,
                        StoretUploaded = storetUploaded,
                        Valid = true
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblPhysHab.Add(newPhysHab);
                        _db.SaveChanges();

                        string successLabelText = "New Phys Hab Added: " + newPhysHab.SampleID;
                        string redirect = "EditPhysHab.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewPhysHab", "", "");
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