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
    public partial class EditWaterCodeDrainage : System.Web.UI.Page
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

        public IQueryable<WaterCodeDrainage> GetWaterCodeDrainages([QueryString]string sampleIDSearchSearchTerm = "",
                                                  [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                //if (!string.IsNullOrEmpty(sampleIDSearchSearchTerm))
                //{
                //    int sampleID = Convert.ToInt32(sampleIDSearchSearchTerm);
                //    return _db.tblSubSamp
                //              .Where(s => s.SampleID == sampleID)
                //               .OrderBy(s => s.SampleID);
                //}

                IQueryable<WaterCodeDrainage> waterCodeDrainages = _db.WaterCodeDrainages
                                                     .OrderBy(w => w.Description);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return waterCodeDrainages;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetWaterCodeDrainages", "", "");
                return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        //public static List<string> SearchForSampleID(string prefixText, int count)
        //{
        //    List<string> sampleIDs = new List<string>();

        //    try
        //    {
        //        using (RiverWatchEntities _db = new RiverWatchEntities())
        //        {
        //            sampleIDs = _db.tblSubSamp
        //                                .Where(s => s.SampleID.ToString().StartsWith(prefixText))
        //                                .OrderBy(s => s.SampleID.ToString())
        //                                .Select(s => s.SampleID.ToString()).Distinct().ToList();

        //            sampleIDs.Sort();

        //            return sampleIDs;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        EditSubSamp editSubSamp = new EditSubSamp();
        //        editSubSamp.HandleErrors(ex, ex.Message, "SearchForSampleID", "", "");
        //        return sampleIDs;
        //    }
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sampleIDSearchSearchTerm = sampleIDSearch.Text;
        //        string redirect = "EditSubSamp.aspx?sampleIDSearchSearchTerm=" + sampleIDSearchSearchTerm;

        //        Response.Redirect(redirect, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleErrors(ex, ex.Message, "btnSearch_Click", "", "");
        //    }
        //}

        //protected void btnSearchRefresh_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Response.Redirect("EditSubSamp.aspx", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
        //    }
        //}

        public void UpdateWaterCodeDrainage(WaterCodeDrainage model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var waterCodeDrainageToUpdate = _db.WaterCodeDrainages.Find(model.ID);
                    TryUpdateModel(waterCodeDrainageToUpdate);

                    _db.SaveChanges();
                    string successMsg = string.Format("Water Code Drainage Updated: {0}", waterCodeDrainageToUpdate.Description);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateWaterCodeDrainage", "", "");
            }
        }

        public void DeleteWaterCodeDrainage(WaterCodeDrainage model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var waterCodeDrainageToDelete = _db.WaterCodeDrainages.Find(model.ID);
                    _db.WaterCodeDrainages.Remove(waterCodeDrainageToDelete);
                    _db.SaveChanges();

                    SetMessages("Success", "Water Code Drainage Deleted: " + waterCodeDrainageToDelete.Description);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteWaterCodeDrainage", "", "");
                }
            }
        }

        public void AddNewWaterCodeDrainage(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                bool errors = false;

                if (string.IsNullOrEmpty(((TextBox)WaterCodeDrainageGridView.FooterRow.FindControl("NewCode")).Text))
                {
                    ((Label)WaterCodeDrainageGridView.FooterRow.FindControl("lblNewCodeRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)WaterCodeDrainageGridView.FooterRow.FindControl("NewDescription")).Text))
                {
                    ((Label)WaterCodeDrainageGridView.FooterRow.FindControl("lblNewDescriptionRequired")).Visible = true;
                    errors = true;
                }
                if (errors)
                {
                    SetMessages("Error", "Can't create new wWater Code Drainage because Required fields are blank.");
                    ((Button)WaterCodeDrainageGridView.FooterRow.FindControl("btnAdd")).Focus();
                }
                else
                {
                    string code = ((TextBox)WaterCodeDrainageGridView.FooterRow.FindControl("NewCode")).Text;
                    string description = ((TextBox)WaterCodeDrainageGridView.FooterRow.FindControl("NewDescription")).Text;
                    string comment = ((TextBox)WaterCodeDrainageGridView.FooterRow.FindControl("NewComment")).Text;

                    var newWaterCodeDrainage = new WaterCodeDrainage()
                    {
                        Code = code,
                        Description = description,
                        Comment = comment,
                        DateCreated = DateTime.Now,
                        Valid = true
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newWaterCodeDrainage.CreatedBy
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newWaterCodeDrainage.CreatedBy = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.WaterCodeDrainages.Add(newWaterCodeDrainage);
                        _db.SaveChanges();

                        string successLabelText = "New Water Code Drainage Added: " + newWaterCodeDrainage.Description;
                        string redirect = "EditWaterCodeDrainage.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewWaterCodeDrainage", "", "");
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

        protected void WaterCodeDrainageGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }

        protected void WaterCodeDrainageGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetMessages();
        }
    }
}