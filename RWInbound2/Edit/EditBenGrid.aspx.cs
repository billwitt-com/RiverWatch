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
    public partial class EditBenGrid : System.Web.UI.Page
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

        public IQueryable<tblBenGrid> GetBenGrids([QueryString]string benSampleIDSearchSearchTerm = "",
                                                  [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }                

                if (!string.IsNullOrEmpty(benSampleIDSearchSearchTerm))
                {
                    int benSampleID = Convert.ToInt32(benSampleIDSearchSearchTerm);
                    return _db.tblBenGrid
                              .Where(b => b.BenSampID == benSampleID)
                              .OrderBy(b => b.BenSampID);
                }

                IQueryable<tblBenGrid> benGrids = _db.tblBenGrid
                                                     .OrderBy(b => b.BenSampID);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return benGrids;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBenGrids", "", "");
                return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForBenSampleID(string prefixText, int count)
        {
            List<string> benSampleIDs = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    benSampleIDs = _db.tblBenGrid
                                    .Where(b => b.BenSampID.ToString().StartsWith(prefixText))
                                    .OrderBy(b => b.BenSampID.ToString())
                                    .Select(b => b.BenSampID.ToString()).Distinct().ToList();

                    benSampleIDs.Sort();

                    return benSampleIDs;
                }
            }
            catch (Exception ex)
            {
                EditBenGrid editBenGrid = new EditBenGrid();
                editBenGrid.HandleErrors(ex, ex.Message, "SearchForBenSampleID", "", "");
                return benSampleIDs;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string benSampleIDSearchSearchTerm = benSampleIDSearch.Text;
                string redirect = "EditBenGrid.aspx?benSampleIDSearchSearchTerm=" + benSampleIDSearchSearchTerm;

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
                Response.Redirect("EditBenGrid.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdateBenGrid(tblBenGrid model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var benGridToUpdate = _db.tblBenGrid.Find(model.ID);
                    TryUpdateModel(benGridToUpdate);

                    _db.SaveChanges();
                    string successMsg = string.Format("Ben Grid Updated for Ben SampleID: {0}", benGridToUpdate.BenSampID);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBenGrid", "", "");
            }
        }

        public void DeleteBenGrid(tblBenGrid model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var benGridToDelete = _db.tblBenGrid.Find(model.ID);
                    _db.tblBenGrid.Remove(benGridToDelete);
                    _db.SaveChanges();

                    SetMessages("Success", "Ben Grid Deleted for Ben SampleID: {0}" + benGridToDelete.BenSampID);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteBenGrid", "", "");
                }
            }
        }

        public void AddNewBenGrid(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                bool errors = false;

                if (string.IsNullOrEmpty(((TextBox)BenGridGridView.FooterRow.FindControl("NewBenSampID")).Text))
                {
                    ((Label)BenGridGridView.FooterRow.FindControl("lblNewBenSampIDRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)BenGridGridView.FooterRow.FindControl("NewRepNum")).Text))
                {
                    ((Label)BenGridGridView.FooterRow.FindControl("lblNewRepNumRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)BenGridGridView.FooterRow.FindControl("NewGridNum")).Text))
                {
                    ((Label)BenGridGridView.FooterRow.FindControl("lblNewGridNumRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)BenGridGridView.FooterRow.FindControl("NewBenCount")).Text))
                {
                    ((Label)BenGridGridView.FooterRow.FindControl("lblNewBenCountRequired")).Visible = true;
                    errors = true;
                }
                if (string.IsNullOrEmpty(((TextBox)BenGridGridView.FooterRow.FindControl("NewStoretUploaded")).Text))
                {
                    ((Label)BenGridGridView.FooterRow.FindControl("lblNewStoretUploadedRequired")).Visible = true;
                    errors = true;
                }
                if (errors)
                {
                    SetMessages("Error", "Can't create new Ben Grid because Required fields are blank.");
                    ((Button)BenGridGridView.FooterRow.FindControl("btnAdd")).Focus();
                }
                else
                {                    
                    int benSampID = Convert.ToInt32(((TextBox)BenGridGridView.FooterRow.FindControl("NewBenSampID")).Text);
                    int repNum = Convert.ToInt32(((TextBox)BenGridGridView.FooterRow.FindControl("NewRepNum")).Text);
                    int gridNum = Convert.ToInt32(((TextBox)BenGridGridView.FooterRow.FindControl("NewGridNum")).Text);
                    int benCount = Convert.ToInt32(((TextBox)BenGridGridView.FooterRow.FindControl("NewBenCount")).Text);
                    string storetUploaded = ((TextBox)BenGridGridView.FooterRow.FindControl("NewStoretUploaded")).Text;

                    var newBenGrid = new tblBenGrid()
                    {
                        BenSampID = benSampID,
                        RepNum = repNum,
                        GridNum = gridNum,
                        BenCount = benCount,
                        StoretUploaded = storetUploaded,
                        Valid = true
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblBenGrid.Add(newBenGrid);
                        _db.SaveChanges();

                        string successLabelText = "New Ben Grid Added for Ben SampleID: " + newBenGrid.BenSampID;
                        string redirect = "EditBenGrid.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenGrid", "", "");
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