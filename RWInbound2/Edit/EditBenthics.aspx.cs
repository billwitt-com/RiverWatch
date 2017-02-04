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
    public partial class EditBenthics : System.Web.UI.Page
    {
        private string selectedBenSampID = "";

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

        public IQueryable<tblBenthic> GettblBenthics([QueryString]string benSampIDSearchSearchTerm = "",
                                                          [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (!string.IsNullOrEmpty(benSampIDSearchSearchTerm))
                {
                    int benSampID = Convert.ToInt32(benSampIDSearchSearchTerm);
                    return _db.tblBenthics
                              .Where(b => b.BenSampID == benSampID)
                              .OrderBy(b => b.BenSampID);                  
                }

                IQueryable<tblBenthic> tblBenthicsList = _db.tblBenthics
                                                            .OrderBy(b => b.BenSampID);

                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return tblBenthicsList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GettblBenthics", "", "");
                return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForBenSampID(string prefixText, int count)
        {
            List<string> benSampIDs = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    benSampIDs = _db.tblBenthics
                                    .Where(b => b.BenSampID.ToString().StartsWith(prefixText))
                                    .OrderBy(b => b.BenSampID)
                                    .Select(b => b.BenSampID.ToString()).Distinct().ToList();
                    
                    return benSampIDs;
                }
            }
            catch (Exception ex)
            {
                EditBenthics editBenthics = new EditBenthics();
                editBenthics.HandleErrors(ex, ex.Message, "SearchForBenSampID", "", "");
                return benSampIDs;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string benSampIDSearchSearchTerm = benSampIDSearch.Text;
                string redirect = "EditBenthics.aspx?benSampIDSearchSearchTerm=" + benSampIDSearchSearchTerm;

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
                Response.Redirect("EditBenthics.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdatetblBenthic(tblBenthic model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var tblBenthicsToUpdate = _db.tblBenthics.Find(model.ID);

                    if (tblBenthicsToUpdate != null)
                    {
                        TryUpdateModel(tblBenthicsToUpdate);

                        tblBenthicsToUpdate.BenSampID = Convert.ToInt32(selectedBenSampID);

                        if (ModelState.IsValid)
                        {
                            _db.SaveChanges();

                            string successMsg = string.Format("Benthic Sample ID Updated: {0}", tblBenthicsToUpdate.BenSampID);
                            SetMessages("Success", successMsg);
                        }
                        else
                        {
                            SetMessages("Error", "Complete all values");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdatetblBenthic", "", "");
            }
        }

        public void DeletetblBenthic(tblBenthic model)
        {
            SetMessages();

            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var tblBenthicsToDelete = _db.tblBenthics.Find(model.ID);
                    _db.tblBenthics.Remove(tblBenthicsToDelete);
                    _db.SaveChanges();

                    string successLabelText = string.Format("Benthic Sample ID Deleted: {0} ", tblBenthicsToDelete.BenSampID);
                    SetMessages("Success", successLabelText);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeletetblBenthic", "", "");
                }
            }
        }
        public void AddNewtblBenthic(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                string stage = ((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewStage")).Text;
                int repNum = string.IsNullOrEmpty(((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewRepNum")).Text) ? 0 :
                             Convert.ToInt32(((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewRepNum")).Text);

                if (string.IsNullOrEmpty(stage))
                {
                    ((Label)tblBenthicsGridView.FooterRow.FindControl("lblStageRequired")).Visible = true;
                }
                else if(repNum == 0)
                {
                    ((Label)tblBenthicsGridView.FooterRow.FindControl("lblRepNumRequired")).Visible = true;
                }
                else
                {
                    int benSampID = string.IsNullOrEmpty(((DropDownList)tblBenthicsGridView.FooterRow.FindControl("NewBenSampIDDropDown")).SelectedItem.Value) ? 0 :
                             Convert.ToInt32(((DropDownList)tblBenthicsGridView.FooterRow.FindControl("NewBenSampIDDropDown")).SelectedItem.Value);

                    int individuals = string.IsNullOrEmpty(((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewIndividuals")).Text) ? 0 :
                             Convert.ToInt32(((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewIndividuals")).Text);
                    string habitatType = ((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewHabitatType")).Text;
                    bool excludedTaxa = ((CheckBox)tblBenthicsGridView.FooterRow.FindControl("NewExcludedTaxa")).Checked;
                    string comments = ((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewComments")).Text;
                    DateTime enterDate = string.IsNullOrEmpty((((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewEnterDate")).Text)) ? DateTime.Now :
                                         Convert.ToDateTime(((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewEnterDate")).Text);
                    string storetUploaded = ((TextBox)tblBenthicsGridView.FooterRow.FindControl("NewStoretUploaded")).Text;

                    var newtblBenthics = new tblBenthic()
                    {
                        Stage = stage,
                        BenSampID = benSampID,
                        RepNum = repNum,
                        Individuals = individuals,
                        ExcludedTaxa = excludedTaxa,
                        HabitatType = habitatType,
                        Comments = comments,
                        EnterDate = enterDate,
                        StoretUploaded = storetUploaded,
                        Valid = true
                    };    

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblBenthics.Add(newtblBenthics);
                        _db.SaveChanges();                        

                        string successLabelText = "New Benthic Sample ID Added: " + newtblBenthics.BenSampID;
                        string redirect = "EditBenthics.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewtblBenthic", "", "");
            }
        }

        public IQueryable<tblBenSamp> BindBenSampIDs()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var benSampIDs = _db.tblBenSamps
                                    .OrderBy(b => b.ID);
                return benSampIDs;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindBenSampIDs", "", "");
                return null;
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

        protected void tblBenthicsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            selectedBenSampID = 
                ((DropDownList)tblBenthicsGridView.Rows[e.RowIndex]
                                                   .FindControl("updateBenSampIDDropDown")).SelectedItem.Value;
        }

        protected void tblBenthicsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList updateBenSampIDDropDown
                        = (DropDownList)e.Row.FindControl("updateBenSampIDDropDown");
                    updateBenSampIDDropDown.Items.Clear();
                    updateBenSampIDDropDown.DataSource = BindBenSampIDs().ToList<tblBenSamp>();
                    updateBenSampIDDropDown.DataBind();
                    var item = e.Row.DataItem as tblBenthic;
                    updateBenSampIDDropDown.Items.FindByValue(item.BenSampID.ToString()).Selected = true;
                }
            }
        }

        protected void tblBenthicsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel") || e.CommandName.Equals("Edit"))
            {
                SetMessages();
            }
        }
    }
}