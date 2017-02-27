using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class EditBenthicSamples : System.Web.UI.Page
    {
        private int benSampIDSelected = 0;

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
                if (BenthicSamplesGridView.Rows != null)
                {
                    if(BenthicSamplesGridView.Rows.Count > 0 && BenthicSamplesGridView.SelectedIndex >= 0)
                    {
                        benSampIDSelected 
                            = Convert.ToInt32(((HiddenField)BenthicSamplesGridView.Rows[BenthicSamplesGridView.SelectedIndex]
                                                                                   .FindControl("HiddenField_SelectedGridRowBenSampID"))
                                                                                   .Value);                        
                    }  
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Page_Load_Get_benSampIDSelected", "", "");              
            }          
        }
        protected void btnLinkToManageBugsPhysHab_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/EnterBenthicsPhysHab.aspx");
        }

        #region Messages
        private void SetMessages(string type = "", string message = "")
        {
            switch (type)
            {
                case "Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "Error":
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "BenthicsData_Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = message;
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "BenthicsData_Error":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = message;
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "BenthicsRep_Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = message;
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "BenthicsRep_Error":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = message;
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "BenthicsGrid_Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = message;
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "BenthicsGrid_Error":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = message;
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                case "Benthics_Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = message;
                    UpdatePanels();
                    break;
                case "Benthics_Error":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = message;
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    BenthicsDataErrorLabel.Text = "";
                    BenthicsDataSuccessLabel.Text = "";
                    BenthicsRepErrorLabel.Text = "";
                    BenthicsRepSuccessLabel.Text = "";
                    BenthicsGridErrorLabel.Text = "";
                    BenthicsGridSuccessLabel.Text = "";
                    BenthicsErrorLabel.Text = "";
                    BenthicsSuccessLabel.Text = "";
                    UpdatePanels();
                    break;
            }
        }

        private void UpdatePanels()
        {
            BenthicSamplesGridView_UpdatePanel.Update();
            BenthicDataFormView_UpdatePanel.Update();
            BenthicsReps_UpdatePanel.Update();
            BenthicsGrids_UpdatePanel.Update();
            Benthics_UpdatePanel.Update();
        }
        #endregion
       
        #region Bind Dropdown lists
        public IQueryable<tlkActivityCategory> BindActivityCategories()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var activityCategories = _db.tlkActivityCategories
                                              .OrderBy(ws => ws.Description);
                return activityCategories;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindActivityCategories", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkFieldProcedure> BindFieldProcedures()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkFieldProcedures = _db.tlkFieldProcedures
                                              .OrderBy(ws => ws.Description);
                return tlkFieldProcedures;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindFieldProcedures", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkFieldGear> BindFieldGears()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkFieldGearsList = _db.tlkFieldGears
                                            .OrderBy(ws => ws.Description);
                return tlkFieldGearsList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindFieldGear", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkGearConfig> BindGearConfigs()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkGearConfigList = _db.tlkGearConfigs
                                            .OrderBy(ws => ws.Description);
                return tlkGearConfigList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindGearConfig", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkActivityType> BindActivityTypes()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkActivityTypeList = _db.tlkActivityTypes
                                            .OrderBy(ws => ws.Description);
                return tlkActivityTypeList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindActivityType", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkMedium> BindMediums()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkMediumList = _db.tlkMediums
                                            .OrderBy(ws => ws.Description);
                return tlkMediumList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindMedium", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkIntent> BindIntents()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkIntentList = _db.tlkIntents
                                       .OrderBy(ws => ws.Description);
                return tlkIntentList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindIntent", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkCommunity> BindCommunities()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkCommunityList = _db.tlkCommunities
                                       .OrderBy(ws => ws.Description);
                return tlkCommunityList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindCommunities", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<tlkBioResultsType> BindBioResultsTypes()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkBioResultsTypeList = _db.tlkBioResultsTypes
                                       .OrderBy(ws => ws.Description);
                return tlkBioResultsTypeList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindBioResultsTypes", "", "", "BenthicsData_Error");
                return null;
            }
        }

        public IQueryable<GridReps> BindGridReps()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var gridReps = (from br in _db.tblBenReps
                                where br.BenSampID == benSampIDSelected
                                orderby br.RepNum
                                select new GridReps { RepNum = br.RepNum });
              
                return gridReps;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindGridReps", "", "", "BenthicsGrid_Error");
                return null;
            }
        }

        public IQueryable<tblBenTaxa> BindBenTaxa()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tblBentaxaList = _db.tblBenTaxas
                                       .OrderBy(bt => bt.FinalID);
                return tblBentaxaList;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindBenTaxa", "", "", "Benthics_Error");
                return null;
            }
        }
        #endregion
        
        #region Main Sample Panel
        public IQueryable<tblBenSamp> GetBenthicSamples([QueryString]string sampleIDSelected = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();                   

                if (string.IsNullOrEmpty(sampleIDSelected))
                {
                    SetSampleData();
                    return null;
                }

                int sampleID = 0;

                bool stationIDIsInt = Int32.TryParse(sampleIDSelected, out sampleID);

                if (sampleID == 0)
                {
                    SetSampleData();
                    return null;
                }               

                Sample sampleData = (from s in _db.Samples
                                     where s.ID == sampleID
                                     select s).FirstOrDefault();

                SetSampleData(true, sampleData);                

                IQueryable<tblBenSamp> benthicSamples = _db.tblBenSamps
                                            .Where(bs => bs.SampleID == sampleID)
                                            .OrderBy(bs => bs.CollDate);                

                return benthicSamples;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBenthicSamples", "", "");
                return null;
            }
        }

        public void UpdateBenthicSample(tblBenSamp model)
        {
            try
            {
                SetMessages();                
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBenthicSample", "", "");
            }
        }

        public void DeleteBenthicSample(tblBenSamp model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var tblBenRepsList = _db.tblBenReps
                                             .Where(br => br.BenSampID == model.ID).ToList();
                    if(tblBenRepsList.Count > 0)
                    {
                        HideAllSubPanels();

                        string errorMsg 
                            = string.Format("Benthics Sample {0} can not be deleted! <br> You <b>MUST DELETE</b> all related Reps, Grids and Taxa first!!!",
                                                        model.tlkActivityCategory.Description);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        var benthicSampleToDelete = _db.tblBenSamps.Find(model.ID);
                        _db.tblBenSamps.Remove(benthicSampleToDelete);
                        _db.SaveChanges();

                        BenthicSamplesGridView.DataBind();
                        BenthicSamplesGridView_UpdatePanel.Update();
                        HideAllSubPanels();

                        string successMsg = string.Format("Benthics Sample Deleted For Activity: {0}", model.tlkActivityCategory.Description);
                        SetMessages("Success", successMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenthicSample", "", "");
            }
        }

        public void AddNewBenthicSample(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                if(benSampIDSelected > 0)
                {
                    benSampIDSelected = 0;
                }

                BenthicDataFormView_Panel.Visible = true;
                BenthicDataFormView.DataBind();
                
                BenthicDataFormView_UpdatePanel.Update();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicSample", "", "");
            }
        }

        private void HideAllSubPanels()
        {
            BenthicDataFormView_Panel.Visible = false;
            BenthicsRepsGridView_Panel.Visible = false;
            BenthicsGridsGridView_Panel.Visible = false;
            BenthicsGridView_Panel.Visible = false;
        }

        private void SetSampleData(bool showPanel = false, Sample sampleData = null)
        {
            try
            {
                if (sampleData != null)
                {
                    SampleData_Panel.Visible = showPanel;
                    lblSampleNumber.Text = sampleData.SampleNumber;
                    lblSampleEvent.Text = sampleData.NumberSample;
                    HiddenField_SampleID.Value = sampleData.ID.ToString();
                    HiddenField_DateCollected.Value = sampleData.DateCollected.ToString();
                    HiddenField_TimeCollected.Value = sampleData.TimeCollected.ToString();
                }
                else
                {
                    SampleData_Panel.Visible = showPanel;
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "SetSampleData", "", "");
            }
        }

        protected void Show(object sender, EventArgs e)
        {
            try
            {
                if (lblSampleNumber != null && !string.IsNullOrEmpty(lblSampleNumber.Text) &&
                    !BenthicSamplesGridView.Controls[0].Controls[0].FindControl("NoResultsPanel").Visible)
                {
                    BenthicSamplesGridView.Controls[0].Controls[0].FindControl("NoResultsPanel").Visible = true;
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Show", "", "");
            }
        }

        protected void BenthicSamplesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }       

        protected void BenthicSamplesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "GetAllBenthicsData")
                {
                    SetMessages();

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        int selectedGridRowIndex = Convert.ToInt32(commandArgs[0]);
                        benSampIDSelected = Convert.ToInt32(commandArgs[1]);

                        if (benSampIDSelected > 0)
                        {
                           foreach(GridViewRow row in BenthicSamplesGridView.Rows)
                           {
                                if (row.RowIndex == selectedGridRowIndex)
                                {
                                    BenthicSamplesGridView.SelectedIndex = row.RowIndex;
                                    row.BackColor = ColorTranslator.FromHtml("#FFFF00");
                                    row.ToolTip = "This Sample's Benthics data is being displayed below.";
                                }
                                else
                                {
                                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                                    row.ToolTip = "Click on the View button to Edit this Sample's Benthics data.";
                                }
                            }                             
                                                       
                            BenthicDataFormView_Panel.Visible = true;
                            BenthicDataFormView.DataBind();
                            BenthicDataFormView_UpdatePanel.Update();

                            BenthicsRepsGridView_Panel.Visible = true;
                            BenthicsRepsGridView_Panel.DataBind();
                            BenthicsReps_UpdatePanel.Update();

                            BenthicsGridsGridView_Panel.Visible = true;
                            BenthicsGridsGridView_Panel.DataBind();
                            BenthicsGrids_UpdatePanel.Update();

                            BenthicsGridView_Panel.Visible = true;
                            BenthicsGridView_Panel.DataBind();
                            lblBenthicsSampleEvent.Text = lblSampleEvent == null ? string.Empty : lblSampleEvent.Text;
                            Benthics_UpdatePanel.Update();
                       
                        }
                        else
                        {
                            SetMessages("Error", "The ID for Benthic Sample Selected could not be found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenthicSamplesGridView_RowCommand", "", "");
            }
        }
        #endregion
        
        #region Benthics Data Panel
        public IQueryable<tblBenSamp> GetSelectedBenthicData()
        {
            try
            {
                if (benSampIDSelected == 0 && SampleData_Panel.Visible)
                {
                    DateTime collDate = new DateTime();
                    bool convertCollDate = DateTime.TryParse(HiddenField_DateCollected.Value, out collDate);

                    string collectedTime = string.IsNullOrEmpty(HiddenField_TimeCollected.Value.ToString()) ? "1900-01-01 00:00:00.000" :
                                                                HiddenField_TimeCollected.Value.ToString();
                    DateTime collTime = new DateTime();
                    bool convertCollTime = DateTime.TryParse(collectedTime, out collTime);

                    if(!convertCollDate || !convertCollTime)
                    {
                        string errorMsg
                            = string.Format("There was a problem with the formatting of the Collected Date: {0] and/or Time: {1} ",
                                            collDate.ToString(), collTime.ToString());
                        SetMessages("Error", errorMsg);
                        return null;
                    }
                    else
                    {
                        SetMessages();

                        var newBenthicSample = new tblBenSamp()
                        {
                            SampleID = Convert.ToInt32(HiddenField_SampleID.Value),
                            ActivityID = 1,
                            CollDate = collDate,
                            CollTime = collTime,
                            CollMeth = 1,
                            GearConfigID = 1,
                            Medium = 1,
                            Intent = 1, 
                            Community = 1,
                            BioResultGroupID = 1,
                            NumKicksSamples = 0,
                            ActivityTypeID = 1,
                            FieldGearID = 1,
                            LabCount = 300
                        };

                        foreach (GridViewRow row in BenthicSamplesGridView.Rows)
                        {
                            row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                            row.ToolTip = "Click on the View button to Edit this Sample's Benthics data.";
                        }

                        BenthicsRepsGridView_Panel.Visible = false;
                        BenthicsGridsGridView_Panel.Visible = false;
                        BenthicsGridView_Panel.Visible = false;

                        IQueryable<tblBenSamp> newBenthicSamples = new List<tblBenSamp>() { newBenthicSample }.AsQueryable<tblBenSamp>();

                        return newBenthicSamples;
                    }
                }

                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tblBenSamp> benthicSamples = _db.tblBenSamps
                                            .Where(bs => bs.ID == benSampIDSelected);
                return benthicSamples;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetSelectedBenthicData", "", "");
                return null;
            }
        }

        protected void BenthicDataFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Cancel"))
                {
                    SetMessages();

                    var datakeyBenSampID = (int)BenthicDataFormView.DataKey.Value;

                    //New set the ben samp id to 0
                    if(datakeyBenSampID == 0)
                    {
                        benSampIDSelected = 0;
                    }
                    BenthicDataFormView.DataBind();
                    BenthicDataFormView_UpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenthicDataFormView_ItemCommand", "", "");
            }            
        }

        /// <summary>
        /// This Method combines both Update and Add to reduce the amount of
        /// code in code behind and on aspx page.
        /// </summary>
        /// <param id="model"></param>
        public void UpdateOrAddBenthicData(tblBenSamp model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    tblBenSamp selectedBenthicsDataToUpdateOrAdd = new tblBenSamp();
                    bool newSample = model.ID == 0;

                    if (!newSample)
                    {
                        selectedBenthicsDataToUpdateOrAdd = _db.tblBenSamps.Find(model.ID);
                    }
                    
                    if (selectedBenthicsDataToUpdateOrAdd != null)
                    {                        
                        TryUpdateModel(selectedBenthicsDataToUpdateOrAdd);

                        if (ModelState.IsValid)
                        {                            
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                selectedBenthicsDataToUpdateOrAdd.UserLastModified
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                selectedBenthicsDataToUpdateOrAdd.UserLastModified = "Unknown";
                            }

                            selectedBenthicsDataToUpdateOrAdd.DateLastModified = DateTime.Now;

                            if (newSample)
                            {
                                selectedBenthicsDataToUpdateOrAdd.EnterDate = DateTime.Now;
                                _db.tblBenSamps.Add(selectedBenthicsDataToUpdateOrAdd);
                                _db.SaveChanges();

                                BenthicDataFormView_Panel.Visible = false;

                                BenthicSamplesGridView.DataBind();
                                SampleData_Panel.DataBind();

                                string addSuccessMsg = string.Format("New Benthics Sample Added.");
                                SetMessages("Success", addSuccessMsg);
                            }
                            else
                            {
                                _db.SaveChanges();                             

                                BenthicSamplesGridView.DataBind();
                                SampleData_Panel.DataBind();
                                BenthicSamplesGridView_UpdatePanel.Update();

                                foreach (GridViewRow row in BenthicSamplesGridView.Rows)
                                {
                                    var selectedGridRowBenSampID = Convert.ToInt32(((HiddenField)row.FindControl("HiddenField_SelectedGridRowBenSampID")).Value);
                                    if (selectedGridRowBenSampID == benSampIDSelected)
                                    {
                                        row.BackColor = ColorTranslator.FromHtml("#FFFF00");
                                        row.ToolTip = "This Sample's Benthics data is being displayed below.";
                                    }
                                    else
                                    {
                                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                                        row.ToolTip = "Click on the View button to Edit this Sample's Benthics data.";
                                    }
                                }

                                string successMsg = string.Format("Benthics Data Updated: {0}", selectedBenthicsDataToUpdateOrAdd.tlkActivityCategory.Description);
                                SetMessages("BenthicsData_Success", successMsg);
                            }
                        }
                        else
                        {
                            SetMessages("BenthicsData_Error", "Correct all input errors");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateSelectedBenthicData", "", "");
            }
        }
        #endregion
       
        #region Benthics Reps Panel 
        public IQueryable<tblBenRep> GetBenthicsReps()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
               
                IQueryable<tblBenRep> benthicReps = _db.tblBenReps
                                            .Where(br => br.BenSampID == benSampIDSelected);
                return benthicReps;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBenthicsReps", "", "");
                return null;
            }
        }

        public void UpdateBenthicRep(tblBenRep model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var selectedBenthicsRepToUpdate = _db.tblBenReps.Find(benSampIDSelected, model.RepNum);

                    if (selectedBenthicsRepToUpdate != null)
                    {
                        TryUpdateModel(selectedBenthicsRepToUpdate);

                        if (ModelState.IsValid)
                        {                            
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                selectedBenthicsRepToUpdate.UserLastModified
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                selectedBenthicsRepToUpdate.UserLastModified = "Unknown";
                            }

                            selectedBenthicsRepToUpdate.DateLastModified = DateTime.Now;
                            _db.SaveChanges();

                            string successMsg 
                                = string.Format("Benthics Rep Updated for Rep Num {0}, Activity {1}, Type {2} ", 
                                                    selectedBenthicsRepToUpdate.RepNum,
                                                    selectedBenthicsRepToUpdate.tlkActivityCategory.Description,
                                                    selectedBenthicsRepToUpdate.Type);

                            SetMessages("BenthicsRep_Success", successMsg);
                            BenthicsGridsGridView_Panel.DataBind();
                            BenthicsGrids_UpdatePanel.Update();

                            BenthicsGridView_Panel.DataBind();
                            Benthics_UpdatePanel.Update();
                        }
                        else
                        {
                            SetMessages("BenthicsRep_Error", "Correct all input errors");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBenthicRep", "", "", "BenthicsRep_Error");
            }
        }

        public void DeleteBenthicRep(tblBenRep model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var tblGrids = (from g in _db.tblBenGrids
                                    where g.BenSampID == benSampIDSelected && g.RepNum == model.RepNum
                                    select g).ToList();
                    if (tblGrids.Count > 0)
                    {
                        string errorMsg
                            = string.Format("Benthics Rep {0} can not be deleted because it is assigned to one or more Grids or Ben Taxa below.", model.RepNum);
                        SetMessages("BenthicsRep_Error", errorMsg);
                    }
                    else
                    {
                        var benthicRepToDelete = _db.tblBenReps.Find(benSampIDSelected, model.RepNum);
                        _db.tblBenReps.Remove(benthicRepToDelete);
                        _db.SaveChanges();

                        string successMsg = string.Format("Benthics Rep Deleted: {0}", model.RepNum);
                        SetMessages("BenthicsRep_Success", successMsg);

                        BenthicsRepsGridView_Panel.DataBind();
                        BenthicsReps_UpdatePanel.Update();

                        BenthicsGridsGridView_Panel.DataBind();
                        BenthicsGrids_UpdatePanel.Update();

                        BenthicsGridView_Panel.DataBind();
                        Benthics_UpdatePanel.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenthicRep", "", "", "BenthicsRep_Error");
            }
        }

        public void AddNewBenthicRep(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                var gridView = BenthicsRepsGridView.Controls[0].Controls[0].FindControl("btnAdd") == null ?
                                    BenthicsRepsGridView.FooterRow : BenthicsRepsGridView.Controls[0].Controls[0];

                string repNumText = ((TextBox)gridView.FindControl("txtNewRepNum")).Text;

                if (string.IsNullOrEmpty(repNumText))
                {
                    ((Label)gridView.FindControl("lblNewRepNumRequired")).Visible = true; 
                }
                else
                {     
                    int repNum = Convert.ToInt32(repNumText);
                    int activityCategory =
                            Convert.ToInt32(((DropDownList)gridView.FindControl("dropDownNewBenthicsRepActivity")).SelectedValue);

                    string gridsText = ((TextBox)gridView.FindControl("txtNewGrids")).Text;
                    int grids = string.IsNullOrEmpty(gridsText) ? 0 : Convert.ToInt32(gridsText);

                    string type = ((TextBox)gridView.FindControl("txtNewType")).Text;
                    string comments = ((TextBox)gridView.FindControl("txtNewComments")).Text;
                    string userLastModified = "Unknown";

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        userLastModified = HttpContext.Current.User.Identity.Name;
                    }

                    var newBenRep = new tblBenRep()
                    {
                        BenSampID = benSampIDSelected,
                        RepNum = repNum,
                        ActivityCategory = activityCategory,
                        Grids = grids,
                        Type = type,
                        Comments = comments,
                        EnterDate = DateTime.Now,
                        UserLastModified = userLastModified,
                        DateLastModified = DateTime.Now
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblBenReps.Add(newBenRep);
                        _db.SaveChanges();

                        string successMsg
                                = string.Format("Benthics Rep Added for Rep Num {0}", repNum);

                        SetMessages("BenthicsRep_Success", successMsg);

                        BenthicsRepsGridView_Panel.DataBind();
                        BenthicsReps_UpdatePanel.Update();

                        BenthicsGridsGridView_Panel.DataBind();
                        BenthicsGrids_UpdatePanel.Update();

                        BenthicsGridView_Panel.DataBind();
                        Benthics_UpdatePanel.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicRep", "", "", "BenthicsRep_Error");
            }
        }

        protected void BenthicsRepsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SetMessages();
                BenthicsRepsGridView.EditIndex = e.NewEditIndex;
                BenthicsRepsGridView_Panel.DataBind();               
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenthicsRepsGridView_RowEditing", "", "", "BenthicsRep_Error");
            }
        }
        #endregion
        
        #region Benthics Grids Panel  
        public class GridReps
        {
            public int RepNum { get; set; }
        }

        public IQueryable<tblBenGrid> GetBenthicsGrids()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tblBenGrid> benthicGrids = _db.tblBenGrids
                                            .Where(bg => bg.BenSampID == benSampIDSelected)
                                            .OrderBy(bg => bg.RepNum);
                return benthicGrids;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBenthicsGrids", "", "");
                return null;
            }
        }

        public void UpdateBenthicGrid(tblBenGrid model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var selectedBenthicsGridToUpdate = _db.tblBenGrids.Find(model.ID);

                    if (selectedBenthicsGridToUpdate != null)
                    {
                        TryUpdateModel(selectedBenthicsGridToUpdate);

                        if (ModelState.IsValid)
                        {
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                selectedBenthicsGridToUpdate.UserLastModified
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                selectedBenthicsGridToUpdate.UserLastModified = "Unknown";
                            }

                            selectedBenthicsGridToUpdate.DateLastModified = DateTime.Now;
                            _db.SaveChanges();

                            string successMsg
                                = string.Format("Benthics Grid Updated for Rep Num {0}, GridNum {1} ",
                                                    selectedBenthicsGridToUpdate.RepNum,
                                                    selectedBenthicsGridToUpdate.GridNum);

                            SetMessages("BenthicsGrid_Success", successMsg);
                        }
                        else
                        {
                            SetMessages("BenthicsGrid_Error", "Correct all input errors");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBenthicGrid", "", "", "BenthicsGrid_Error");
            }
        }

        public void DeleteBenthicGrid(tblBenGrid model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var benthicGridToDelete = _db.tblBenGrids.Find(model.ID);
                    _db.tblBenGrids.Remove(benthicGridToDelete);
                    _db.SaveChanges();

                    string successMsg = string.Format("Benthics Grid Deleted: Rep Num {0}, GridNum {1}", model.RepNum, model.GridNum);
                    SetMessages("BenthicsGrid_Success", successMsg);

                    BenthicsGridsGridView_Panel.DataBind();
                    BenthicsGrids_UpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenthicGrid", "", "", "BenthicsGrid_Error");
            }
        }

        public void AddNewBenthicGrid(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                var gridView = BenthicsGridsGridView.Controls[0].Controls[0].FindControl("btnAdd") == null ?
                               BenthicsGridsGridView.FooterRow : BenthicsGridsGridView.Controls[0].Controls[0];
                int repNum = 0;
                bool repNumExists = int.TryParse(((DropDownList)gridView.FindControl("dropDownNewBenthicsGridReps")).SelectedValue,
                                                    out repNum);
                string gridNumText = ((TextBox)gridView.FindControl("txtNewGridNum")).Text;

                if (!repNumExists)
                {
                    string errorMsg
                           = string.Format("A Benthic Rep must be created before you can perform this operation.");
                    SetMessages("BenthicsGrid_Error", errorMsg);
                }                
                else if (string.IsNullOrEmpty(gridNumText))
                {
                    ((Label)gridView.FindControl("lblNewGridNumRequired")).Visible = true;
                }               
                else
                {
                    int gridNum = Convert.ToInt32(gridNumText);                   

                    string benCountText = ((TextBox)gridView.FindControl("txtNewBenCount")).Text;
                    int benCount = string.IsNullOrEmpty(benCountText) ? 0 : Convert.ToInt32(benCountText);
                   
                    string userLastModified = "Unknown";

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        userLastModified = HttpContext.Current.User.Identity.Name;
                    }

                    var newBenGrid = new tblBenGrid()
                    {
                        BenSampID = benSampIDSelected,
                        RepNum = repNum,
                        GridNum = gridNum,
                        BenCount = benCount,
                        UserLastModified = userLastModified,
                        DateLastModified = DateTime.Now
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblBenGrids.Add(newBenGrid);
                        _db.SaveChanges();

                        string successMsg
                                = string.Format("Benthics Grid Added for Rep {0} Grid Num {1}", repNum, gridNum);
                        SetMessages("BenthicsGrid_Success", successMsg);

                        BenthicsGridsGridView_Panel.DataBind();
                        BenthicsGrids_UpdatePanel.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicGrid", "", "", "BenthicsGrid_Error");
            }
        }

        protected void BenthicsGridsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SetMessages();
                BenthicsGridsGridView.EditIndex = e.NewEditIndex;
                BenthicsGridsGridView_Panel.DataBind();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenthicsGridsGridView_RowEditing", "", "");
            }
        }
        #endregion

        #region Ben Taxa (Benthics) Panel  
        public IQueryable<tblBenthic> GetBenthics(string sortByExpression, [Control("dropDownBenthicsSelectedRepNum")] int? repNum)
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if(repNum == null)
                {
                    repNum = 1;
                }

                if(!string.IsNullOrEmpty(sortByExpression))
                {
                    switch (sortByExpression)
                    {
                        case "SortByBenTaxaFinalID DESC":
                            return _db.tblBenthics
                                      .Where(b => b.BenSampID == benSampIDSelected && b.RepNum == repNum)
                                      .OrderByDescending(b => b.tblBenTaxa.FinalID);
                        case "EnterDate DESC":
                            return _db.tblBenthics
                                      .Where(b => b.BenSampID == benSampIDSelected && b.RepNum == repNum)
                                      .OrderByDescending(b => b.EnterDate);
                        case "EnterDate":
                            return _db.tblBenthics
                                      .Where(b => b.BenSampID == benSampIDSelected && b.RepNum == repNum)
                                      .OrderBy(b => b.EnterDate);
                        default:
                            return _db.tblBenthics
                                      .Where(b => b.BenSampID == benSampIDSelected && b.RepNum == repNum)
                                      .OrderBy(b => b.tblBenTaxa.FinalID);
                    }                            
                }

                IQueryable<tblBenthic> benthics = _db.tblBenthics
                                                     .Where(b => b.BenSampID == benSampIDSelected && b.RepNum == repNum)
                                                     .OrderBy(b => b.tblBenTaxa.FinalID);

                return benthics;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBenthics", "", "");
                return null;
            }
        }

        public void UpdateBenthic(tblBenthic model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var selectedBenthicsToUpdate = _db.tblBenthics.Find(model.BenTaxaID, benSampIDSelected, model.RepNum);

                    if (selectedBenthicsToUpdate != null)
                    {
                        TryUpdateModel(selectedBenthicsToUpdate);

                        if (ModelState.IsValid)
                        {
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                selectedBenthicsToUpdate.UserLastModified
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                selectedBenthicsToUpdate.UserLastModified = "Unknown";
                            }

                            selectedBenthicsToUpdate.DateLastModified = DateTime.Now;
                            _db.SaveChanges();

                            string successMsg
                                = string.Format("Ben Taxa Updated for Taxa {0} ",
                                                    selectedBenthicsToUpdate.tblBenTaxa.FinalID);

                            SetMessages("Benthics_Success", successMsg);
                        }
                        else
                        {
                            SetMessages("Benthics_Error", "Correct all input errors");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBenthic", "", "", "Benthics_Error");
            }
        }

        public void DeleteBenthic(tblBenthic model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var benthicToDelete = _db.tblBenthics.Find(model.BenTaxaID, benSampIDSelected, model.RepNum);
                    _db.tblBenthics.Remove(benthicToDelete);
                    _db.SaveChanges();

                    string successMsg
                            = string.Format("Ben Taxa Deleted {0} for Rep Num {1}", model.tblBenTaxa.FinalID, model.RepNum);
                    SetMessages("Benthics_Success", successMsg);                   
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenthic", "", "", "Benthics_Error");
            }
        }

        public void AddNewBenthic(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                var gridView = BenthicsGridView.Controls[0].Controls[0].FindControl("btnAdd") == null ?
                               BenthicsGridView.FooterRow : BenthicsGridView.Controls[0].Controls[0];

                int repNum = 0;
                bool repNumExists = int.TryParse(dropDownBenthicsSelectedRepNum.SelectedValue, out repNum);

                if (!repNumExists)
                {
                    string errorMsg
                           = string.Format("A Benthic Rep must be created before you can perform this operation.");
                    SetMessages("Benthics_Error", errorMsg);
                }
                else
                {
                    string taxaFinalID = ((DropDownList)gridView.FindControl("dropDownNewBenthicsTaxa")).SelectedItem.Text;

                    int benTaxaID =
                            Convert.ToInt32(((DropDownList)gridView.FindControl("dropDownNewBenthicsTaxa")).SelectedValue);                
                    int individuals =
                            Convert.ToInt32(string.IsNullOrEmpty(((TextBox)gridView.FindControl("txtNewIndividuals")).Text) ? "0" :
                                            ((TextBox)gridView.FindControl("txtNewIndividuals")).Text);
                    int numInHundredPct =
                        Convert.ToInt32(string.IsNullOrEmpty(((TextBox)gridView.FindControl("txtNewNumInHundredPct")).Text) ? "0" :
                                        ((TextBox)gridView.FindControl("txtNewNumInHundredPct")).Text);
                
                    string comments = ((TextBox)gridView.FindControl("txtNewComments")).Text;
                
                    string userLastModified = "Unknown";

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        userLastModified = HttpContext.Current.User.Identity.Name;
                    }

                    var newBenthic = new tblBenthic()
                    {
                        BenSampID = benSampIDSelected,
                        BenTaxaID = benTaxaID,
                        RepNum = repNum,
                        Individuals = individuals,
                        NumInHundredPct = numInHundredPct,
                        Comments = comments,
                        EnterDate = DateTime.Now,
                        Stage = "X",
                        UserLastModified = userLastModified,
                        DateLastModified = DateTime.Now
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblBenthics.Add(newBenthic);
                        _db.SaveChanges();

                        string successMsg
                                = string.Format("Ben Taxa Added {0} for Rep Num {1}", taxaFinalID, repNum);

                        SetMessages("Benthics_Success", successMsg);
                    
                        BenthicsGridView.DataBind();
                        Benthics_UpdatePanel.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicRep", "", "", "Benthics_Error");
            }
        }

        protected void BenthicsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            //    {
            //        dropDownBenthicsSelectedRepNum.Items.Clear();
            //        dropDownBenthicsSelectedRepNum.DataBind();
            //        var item = e.Row.DataItem as tblBenthic;
            //        dropDownBenthicsSelectedRepNum.Items.FindByText(item.RepNum.ToString()).Selected = true;
            //    }
            //}
        }

        protected void ShowAllBenTaxaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (BenthicsGridView != null)
                {
                    bool showAll = BenthicsGridView.AllowPaging;
                    if (showAll)
                    {
                        BenthicsGridView.AllowPaging = false;
                        ShowAllBenTaxaButton.Text = "Turn Paging Back On";
                    }
                    else
                    {
                        BenthicsGridView.AllowPaging = true;
                        ShowAllBenTaxaButton.Text = "Show All For Rep";
                    }
                    BenthicsGridView.DataBind();
                    Benthics_UpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "ShowAllBenTaxaButton_Click", "", "", "Benthics_Error");
            }
        }

        protected void BenthicsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //SetMessages();
                //BenthicsGridView.EditIndex = e.NewEditIndex;
                //BenthicsGridView_Panel.DataBind();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenthicsGridView_RowEditing", "", "", "Benthics_Error");
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

                if(validationResultList != null)
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