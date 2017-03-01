using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditBenTaxa : System.Web.UI.Page
    {
        private int taxaIDSelected = 0;
        private bool update = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMessages();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }

            try
            {
                if (BenTaxaGridView.Rows != null)
                {
                    if (BenTaxaGridView.Rows.Count > 0 && BenTaxaGridView.SelectedIndex >= 0)
                    {
                        taxaIDSelected
                            = Convert.ToInt32(((HiddenField)BenTaxaGridView.Rows[BenTaxaGridView.SelectedIndex]
                                                                                   .FindControl("HiddenField_SelectedGridRowTaxaID"))
                                                                                   .Value);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Page_Load_Get_benSampIDSelected", "", "");
            }
        }

        #region Messages
        private void SetMessages(string type = "", string message = "")
        {
            switch (type)
            {
                case "Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    UpdatePanels();
                    break;
                case "Error":
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "TaxaData_Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenTaxaDataErrorLabel.Text = "";
                    BenTaxaDataSuccessLabel.Text = message;                    
                    UpdatePanels();
                    break;
                case "TaxaData_Error":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenTaxaDataErrorLabel.Text = message;
                    BenTaxaDataSuccessLabel.Text = "";                   
                    UpdatePanels();
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    UpdatePanels();
                    break;
            }
        }

        private void UpdatePanels()
        {
            BenTaxaGridView_UpdatePanel.Update();
            BenTaxaDataFormView_UpdatePanel.Update();
        }
        #endregion

        #region Search
        protected void btnSearchTaxa_Click(object sender, EventArgs e)
        {
            try
            {  
                string selectedTaxa = taxaSearch.Text.Trim();

                if (!string.IsNullOrEmpty(selectedTaxa))
                {
                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        int taxaIDFound = _db.tblBenTaxas
                                                .Where(bt => bt.FinalID.Equals(selectedTaxa))
                                                .Select(bt => bt.ID)
                                                .SingleOrDefault();
                        if (taxaIDFound > 0)
                        {
                            taxaIDSelected = taxaIDFound;

                            BenTaxaGridView.DataBind();
                            BenTaxaGridView_UpdatePanel.Update();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchTaxa_Click", "", "");
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchForTaxa(string prefixText, int count)
        {
            List<string> taxa = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    taxa = _db.tblBenTaxas
                              .Where(bt => bt.FinalID.StartsWith(prefixText))
                              .OrderBy(bt => bt.FinalID)
                              .Select(bt => bt.FinalID)                             
                              .ToList();
                    return taxa;
                }
            }
            catch (Exception ex)
            {
                EditBenTaxa editBenTaxa = new EditBenTaxa();
                editBenTaxa.HandleErrors(ex, ex.Message, "SearchForTaxa", "", "");
                return taxa;
            }
        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                taxaIDSelected = 0;

                BenTaxaGridView.DataBind();
                BenTaxaGridView_UpdatePanel.Update();

                BenTaxaDataFormView_Panel.Visible = false;
                BenTaxaDataFormView_UpdatePanel.Update();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }
        #endregion

        #region Ben Taxa Main GridView
        //public IQueryable<tblBenSamp> GetBenTaxas([QueryString]string sampleIDSelected = "")
        public IQueryable<tblBenTaxa> GetBenTaxas()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                //if (string.IsNullOrEmpty(sampleIDSelected))
                //{
                //    SetSampleData();
                //    return null;
                //}

                //int sampleID = 0;

                //bool stationIDIsInt = Int32.TryParse(sampleIDSelected, out sampleID);

                //if (sampleID == 0)
                //{
                //    SetSampleData();
                //    return null;
                //}

                //Sample sampleData = (from s in _db.Samples
                //                     where s.ID == sampleID
                //                     select s).FirstOrDefault();

                //SetSampleData(true, sampleData);

                if(taxaIDSelected > 0 && !update)
                {
                    return _db.tblBenTaxas
                              .Where(bt => bt.ID == taxaIDSelected)
                              .OrderBy(bt => bt.FinalID);

                }

                IQueryable<tblBenTaxa> bentaxas = _db.tblBenTaxas                                          
                                            .OrderBy(bt => bt.FinalID);

                return bentaxas;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBenTaxas", "", "");
                return null;
            }
        }

        public void UpdateOrAddBenTaxaData(tblBenTaxa model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    tblBenTaxa selectedBenTaxaDataToUpdateOrAdd = new tblBenTaxa();
                    bool newBenTaxa = model.ID == 0;

                    if (!newBenTaxa)
                    {
                        selectedBenTaxaDataToUpdateOrAdd = _db.tblBenTaxas.Find(model.ID);
                    }

                    if (selectedBenTaxaDataToUpdateOrAdd != null)
                    {
                        TryUpdateModel(selectedBenTaxaDataToUpdateOrAdd);

                        if (ModelState.IsValid)
                        {
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                selectedBenTaxaDataToUpdateOrAdd.UserLastModified
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                selectedBenTaxaDataToUpdateOrAdd.UserLastModified = "Unknown";
                            }

                            selectedBenTaxaDataToUpdateOrAdd.DateLastModified = DateTime.Now;

                            if (newBenTaxa)
                            {
                                selectedBenTaxaDataToUpdateOrAdd.EnterDate = DateTime.Now;
                                _db.tblBenTaxas.Add(selectedBenTaxaDataToUpdateOrAdd);
                                _db.SaveChanges();

                                taxaIDSelected = model.ID;
                                BenTaxaDataFormView_Panel.Visible = false;

                                BenTaxaGridView.DataBind();
                                BenTaxaGridView_UpdatePanel.Update();

                                string addSuccessMsg = string.Format("New Ben Taxa Added {0}", model.FinalID);
                                SetMessages("Success", addSuccessMsg);
                            }
                            else
                            {
                                _db.SaveChanges();

                                BenTaxaGridView.DataBind();
                                BenTaxaGridView_UpdatePanel.Update();                                

                                foreach (GridViewRow row in BenTaxaGridView.Rows)
                                {
                                    var selectedGridRowBenTaxaID = Convert.ToInt32(((HiddenField)row.FindControl("HiddenField_SelectedGridRowTaxaID")).Value);
                                    if (selectedGridRowBenTaxaID == taxaIDSelected)
                                    {
                                        update = true;
                                        //BenTaxaGridView.SelectedIndex = row.RowIndex;
                                        row.BackColor = ColorTranslator.FromHtml("#FFFF00");
                                        row.ToolTip = "This Taxa's data is being displayed below.";
                                    }
                                    else
                                    {
                                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                                        row.ToolTip = "Click on the View button to Edit this Taxa's data.";
                                    }
                                }

                                string successMsg = string.Format("Taxa Data Updated: {0}", selectedBenTaxaDataToUpdateOrAdd.FinalID);
                                SetMessages("TaxaData_Success", successMsg);
                            }
                        }
                        else
                        {
                            SetMessages("TaxaData_Error", "Correct all input errors");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBenTaxa", "", "", "TaxaData_Error");
            }
        }

        public void DeleteBenTaxa(tblBenTaxa model)
        {
            try
            {
                SetMessages();

                //using (RiverWatchEntities _db = new RiverWatchEntities())
                //{
                //    var tblBenRepsList = _db.tblBenReps
                //                             .Where(br => br.BenSampID == model.ID).ToList();
                //    if (tblBenRepsList.Count > 0)
                //    {
                //        HideAllSubPanels();

                //        string errorMsg
                //            = string.Format("Benthics Sample {0} can not be deleted! <br> You <b>MUST DELETE</b> all related Reps, Grids and Taxa first!!!",
                //                                        model.tlkActivityCategory.Description);
                //        SetMessages("Error", errorMsg);
                //    }
                //    else
                //    {
                //        var benthicSampleToDelete = _db.tblBenSamps.Find(model.ID);
                //        _db.tblBenSamps.Remove(benthicSampleToDelete);
                //        _db.SaveChanges();

                //        BenthicSamplesGridView.DataBind();
                //        BenthicSamplesGridView_UpdatePanel.Update();
                //        HideAllSubPanels();

                //        string successMsg = string.Format("Benthics Sample Deleted For Activity: {0}", model.tlkActivityCategory.Description);
                //        SetMessages("Success", successMsg);
                //    }
                //}
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenTaxa", "", "");
            }
        }

        public void AddNewBenTaxa(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                //if (benSampIDSelected > 0)
                //{
                //    benSampIDSelected = 0;
                //}

                //if (BenthicSamplesGridView.Controls[0].Controls[0].FindControl("btnAddNewSample") != null)
                //{
                //    ((Button)BenthicSamplesGridView.Controls[0].Controls[0].FindControl("btnAddNewSample")).Visible = false;
                //}

                //BenthicDataFormView_Panel.Visible = true;
                //BenthicDataFormView.DataBind();

                //BenthicDataFormView_UpdatePanel.Update();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenTaxa", "", "");
            }
        }

        protected void BenTaxaGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GetTaxaData")
                {
                    SetMessages();

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        int selectedGridRowIndex = Convert.ToInt32(commandArgs[0]);
                        taxaIDSelected = Convert.ToInt32(commandArgs[1]);

                        if (taxaIDSelected > 0)
                        {
                            foreach (GridViewRow row in BenTaxaGridView.Rows)
                            {
                                if (row.RowIndex == selectedGridRowIndex)
                                {
                                    BenTaxaGridView.SelectedIndex = row.RowIndex;
                                    row.BackColor = ColorTranslator.FromHtml("#FFFF00");
                                    row.ToolTip = "This Taxa's data is being displayed below.";
                                }
                                else
                                {
                                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                                    row.ToolTip = "Click on the View button to Edit this Taxa's data.";
                                }
                            }

                            BenTaxaDataFormView_Panel.Visible = true;
                            BenTaxaDataFormView_Panel.DataBind();
                            BenTaxaDataFormView_UpdatePanel.Update();                            
                        }
                        else
                        {
                            SetMessages("Error", "The ID for the Taxa Selected could not be found.");
                        }
                    }
                }

                if (e.CommandName == "GetAssignedBenthicSamples")
                {
                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        int id = Convert.ToInt32(e.CommandArgument);

                        var tblBenSampSampleNumbers = (from s in _db.Samples
                                                       join bs in _db.tblBenSamps on s.SampleID equals bs.ID
                                                       join tb in _db.tblBenthics on bs.ID equals tb.BenSampID
                                                       where tb.BenTaxaID == id
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
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "MediumGridView_RowCommand", "", "");
            }
        }
        protected void BenTaxaGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }
        #endregion

        #region Helpers 
        private void HideAllSubPanels()
        {
            //BenthicDataFormView_Panel.Visible = false;
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
        #endregion

        #region Ben Taxa Data Panel
        public IQueryable<tblBenTaxa> GetSelectedBenTaxaData()
        {
            try
            {
                if (taxaIDSelected == 0 && BenTaxaDataFormView_Panel.Visible)
                {
                    SetMessages();

                    //var newBenthicSample = new tblBenSamp()
                    //{
                    //    SampleID = Convert.ToInt32(HiddenField_SampleID.Value),
                    //    ActivityID = 1,
                    //    CollDate = collDate,
                    //    CollTime = collTime,
                    //    CollMeth = 1,
                    //    GearConfigID = 1,
                    //    Medium = 1,
                    //    Intent = 1,
                    //    Community = 1,
                    //    BioResultGroupID = 1,
                    //    NumKicksSamples = 0,
                    //    ActivityTypeID = 1,
                    //    FieldGearID = 1,
                    //    LabCount = 300
                    //};

                    //foreach (GridViewRow row in BenthicSamplesGridView.Rows)
                    //{
                    //    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    //    row.ToolTip = "Click on the View button to Edit this Sample's Benthics data.";
                    //}

                    //BenthicsRepsGridView_Panel.Visible = false;
                    //BenthicsGridsGridView_Panel.Visible = false;
                    //BenthicsGridView_Panel.Visible = false;

                    //IQueryable<tblBenSamp> newBenthicSamples = new List<tblBenSamp>() { newBenthicSample }.AsQueryable<tblBenSamp>();

                    //return newBenthicSamples;
                    return null;
                }

                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tblBenTaxa> benTaxa = _db.tblBenTaxas
                                                    .Where(bt => bt.ID == taxaIDSelected);
                return benTaxa;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetSelectedBenTaxaData", "", "");
                return null;
            }
        }

        protected void BenTaxaDataFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Cancel"))
                {
                    SetMessages();

                    var datakeyTaxaID = (int)BenTaxaDataFormView.DataKey.Value;

                    //New set the ben samp id to 0
                    if (datakeyTaxaID == 0)
                    {
                        taxaIDSelected = 0;
                    }
                    BenTaxaDataFormView.DataBind();
                    BenTaxaDataFormView_UpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenTaxaDataFormView_ItemCommand", "", "");
            }
        }
       
        #endregion

        #region Error Handling        
        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment, string type = "Error")
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
                SetMessages(type, sb.ToString());
            }
            else if (ex.GetType().IsAssignableFrom(typeof(DbUpdateException)))
            {
                DbUpdateException efUpdateException = ex as DbUpdateException;
                var validationResultList = TryDecodeDbUpdateException(efUpdateException);

                if (validationResultList != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("ERROR! ");
                    foreach (var vr in validationResultList)
                    {
                        sb.AppendFormat("{0} ", vr.ErrorMessage);
                    }

                    SetMessages(type, sb.ToString());
                }
            }
            else
            {
                SetMessages(type, ex.Message);
            }
        }

        /***************************************************************************************
         * New Error Handling - Can move to seperate class
        ****************************************************************************************/
        private static readonly Dictionary<int, string> _sqlErrorTextDict =
            new Dictionary<int, string>
        {
            {547,
             "This operation failed because another data entry uses this entry."},
            {2601,
             "One of the properties is marked as Unique index and there is already an entry with that value."},
            {2627,
             "This operation failed because there is already an entry with that value ."}
        };

        /// <summary>
        /// This decodes the DbUpdateException. If there are any errors it can
        /// handle then it returns a list of errors. Otherwise it returns null
        /// which means rethrow the error as it has not been handled
        /// </summary>
        /// <param id="ex""></param>
        /// <returns>null if cannot handle errors, otherwise a list of errors</returns>
        IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException ex)
        {
            if (!(ex.InnerException is System.Data.Entity.Core.UpdateException) ||
                !(ex.InnerException.InnerException is System.Data.SqlClient.SqlException))
                return null;
            var sqlException =
                (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;
            var result = new List<ValidationResult>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {
                var errorNum = sqlException.Errors[i].Number;
                string errorText;
                if (!_sqlErrorTextDict.TryGetValue(errorNum, out errorText))
                    errorText = sqlException.Errors[i].Message;
                result.Add(new ValidationResult(errorText));
            }
            return result.Any() ? result : null;
        }
        #endregion
    }
}