using System;
using System.Collections.Generic;
using System.Data;
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
                            = Convert.ToInt32(((HiddenField)BenthicSamplesGridView.Rows[BenthicSamplesGridView.SelectedIndex].FindControl("HiddenField_SelectedGridRowBenSampID")).Value);                        
                    }  
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Page_Load_Get_benSampIDSelected", "", "");              
            }          
        }

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
                    break;
            }
        }

        /***************************************************************************************
         * Bind Dropdown lists 
        ****************************************************************************************/

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
                HandleErrors(ex, ex.Message, "BindActivityCategories", "", "");
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
                HandleErrors(ex, ex.Message, "BindFieldProcedures", "", "");
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
                HandleErrors(ex, ex.Message, "BindFieldGear", "", "");
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
                HandleErrors(ex, ex.Message, "BindGearConfig", "", "");
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
                HandleErrors(ex, ex.Message, "BindActivityType", "", "");
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
                HandleErrors(ex, ex.Message, "BindMedium", "", "");
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
                HandleErrors(ex, ex.Message, "BindIntent", "", "");
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
                HandleErrors(ex, ex.Message, "BindCommunities", "", "");
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
                HandleErrors(ex, ex.Message, "BindBioResultsTypes", "", "");
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
                HandleErrors(ex, ex.Message, "BindGridReps", "", "");
                return null;
            }
        }

        /***************************************************************************************
         * Main Sample Grid
        ****************************************************************************************/
        public IQueryable<tblBenSamp> GetBenthicSamples([QueryString]string sampleIDSelected = "",
                                             [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                PropertyInfo isreadonly
                       = typeof(System.Collections.Specialized.NameValueCollection)
                               .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

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

                SampleData sampleData = (from s in _db.Samples
                                          where s.ID == sampleID
                                          select new SampleData
                                          {
                                              SampleNumber = s.SampleNumber.ToString(),
                                              SampleEvent = s.NumberSample.ToString()
                                           }).FirstOrDefault();

                SetSampleData(true, sampleData.SampleNumber, sampleData.SampleEvent);                

                IQueryable<tblBenSamp> benthicSamples = _db.tblBenSamps
                                            .Where(bs => bs.SampleID == sampleID && bs.Valid == true)
                                            .OrderBy(bs => bs.CollDate);

                // remove
                this.Request.QueryString.Remove("successLabelMessage");
                if (string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", "");
                }
                //this.Request.QueryString.Clear();

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
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicSample", "", "");
            }
        }

        private void SetSampleData(bool showPanel = false, string sampleNumber = "", string stationEvent = "")
        {
            SampleData_Panel.Visible = showPanel;
            lblSampleNumber.Text = sampleNumber;
            lblSampleEvent.Text = stationEvent;
        }

        protected void Show(object sender, EventArgs e)
        {
            if (lblSampleNumber != null && !string.IsNullOrEmpty(lblSampleNumber.Text) &&
                !BenthicSamplesGridView.Controls[0].Controls[0].FindControl("NoResultsPanel").Visible)
            {
                BenthicSamplesGridView.Controls[0].Controls[0].FindControl("NoResultsPanel").Visible = true;
            }
        }

        private class SampleData
        {
            public string SampleNumber { get; set; }
            public string SampleEvent { get; set; }
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

        /***************************************************************************************
         * Benthics Data Panel 
        ****************************************************************************************/
        public IQueryable<tblBenSamp> GetSelectedBenthicData()
        {
            try
            {
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

                    BenthicDataFormView_Panel.Visible = true;
                    BenthicDataFormView.DataBind();
                    BenthicDataFormView_UpdatePanel.Update();

                    //string forViewID = e.CommandArgument.ToString();
                    //bool convertIDToInt = int.TryParse(forViewID, out benSampIDSelected);
                    //if (!convertIDToInt)
                    //{
                    //    SetMessages("BenthicsData_Error", "The Benthics Sample ID provided is not a number. Please correct the error and try again.");
                    //}
                    //else
                    //{
                    //    BenthicDataFormView_Panel.Visible = true;
                    //    BenthicDataFormView.DataBind();
                    //    BenthicDataFormView_UpdatePanel.Update();                    
                    //}                  
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BenthicDataFormView_ItemCommand", "", "");
            }            
        }

        public void UpdateSelectedBenthicData(tblBenSamp model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var selectedBenthicsDataToUpdate = _db.tblBenSamps.Find(model.ID);

                    if (selectedBenthicsDataToUpdate != null)
                    {
                        TryUpdateModel(selectedBenthicsDataToUpdate);

                        if (ModelState.IsValid)
                        {                            
                            if (this.User != null && this.User.Identity.IsAuthenticated)
                            {
                                selectedBenthicsDataToUpdate.UserLastModified
                                    = HttpContext.Current.User.Identity.Name;
                            }
                            else
                            {
                                selectedBenthicsDataToUpdate.UserLastModified = "Unknown";
                            }

                            selectedBenthicsDataToUpdate.DateLastModified = DateTime.Now;
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
                                    row.ToolTip = "This Sample's Benthics data is being displayed below test.";
                                }
                                else
                                {
                                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                                    row.ToolTip = "Click on the View button to Edit this Sample's Benthics data test.";
                                }
                            }

                            string successMsg = string.Format("Benthics Data Updated: {0}", selectedBenthicsDataToUpdate.tlkActivityCategory.Description);
                            SetMessages("BenthicsData_Success", successMsg);
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

        /***************************************************************************************
         * Benthics Reps Panel 
        ****************************************************************************************/

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
                HandleErrors(ex, ex.Message, "UpdateBenthicRep", "", "");
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
                            = string.Format("Benthics Rep {0} can not be deleted because it is assigned to one or more Grids below.", model.RepNum);
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
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenthicRep", "", "");
            }
        }

        public void AddNewBenthicRep(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                string repNumText = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewRepNum")).Text;

                if (string.IsNullOrEmpty(repNumText))
                {
                    ((Label)BenthicsRepsGridView.FooterRow.FindControl("lblNewRepNumRequired")).Visible = true; 
                }
                else
                {     
                    int repNum = Convert.ToInt32(repNumText);
                    int activityCategory =
                            Convert.ToInt32(((DropDownList)BenthicsRepsGridView.FooterRow.FindControl("dropDownNewBenthicsRepActivity")).SelectedValue);

                    string gridsText = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewGrids")).Text;
                    int grids = string.IsNullOrEmpty(gridsText) ? 0 : Convert.ToInt32(gridsText);

                    string type = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewType")).Text;
                    string comments = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewComments")).Text;
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
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicRep", "", "");
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
                HandleErrors(ex, ex.Message, "BenthicsRepsGridView_RowEditing", "", "");
            }
        }

        /***************************************************************************************
         * Benthics Grids Panel 
        ****************************************************************************************/

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
                                            .Where(br => br.BenSampID == benSampIDSelected);
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
                HandleErrors(ex, ex.Message, "UpdateBenthicGrid", "", "");
            }
        }

        public void DeleteBenthicGrid(tblBenGrid model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var benthicGridToDelete = _db.tblBenReps.Find(model.ID);
                    _db.tblBenReps.Remove(benthicGridToDelete);
                    _db.SaveChanges();

                    string successMsg = string.Format("Benthics Grid Deleted: Rep Num {0}, GridNum {1}", model.RepNum, model.GridNum);
                    SetMessages("BenthicsGrid_Success", successMsg);

                    BenthicsGridsGridView_Panel.DataBind();
                    BenthicsGrids_UpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteBenthicGrid", "", "");
            }
        }

        public void AddNewBenthicGrid(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                //string repNumText = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewRepNum")).Text;

                //if (string.IsNullOrEmpty(repNumText))
                //{
                //    ((Label)BenthicsRepsGridView.FooterRow.FindControl("lblNewRepNumRequired")).Visible = true;
                //}
                //else
                //{
                //    int repNum = Convert.ToInt32(repNumText);
                //    int activityCategory =
                //            Convert.ToInt32(((DropDownList)BenthicsRepsGridView.FooterRow.FindControl("dropDownNewBenthicsRepActivity")).SelectedValue);

                //    string gridsText = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewGrids")).Text;
                //    int grids = string.IsNullOrEmpty(gridsText) ? 0 : Convert.ToInt32(gridsText);

                //    string type = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewType")).Text;
                //    string comments = ((TextBox)BenthicsRepsGridView.FooterRow.FindControl("txtNewComments")).Text;
                //    string userLastModified = "Unknown";

                //    if (this.User != null && this.User.Identity.IsAuthenticated)
                //    {
                //        userLastModified = HttpContext.Current.User.Identity.Name;
                //    }

                //    var newBenRep = new tblBenRep()
                //    {
                //        BenSampID = benSampIDSelected,
                //        RepNum = repNum,
                //        ActivityCategory = activityCategory,
                //        Grids = grids,
                //        Type = type,
                //        Comments = comments,
                //        EnterDate = DateTime.Now,
                //        UserLastModified = userLastModified,
                //        DateLastModified = DateTime.Now
                //    };

                //    using (RiverWatchEntities _db = new RiverWatchEntities())
                //    {
                //        _db.tblBenReps.Add(newBenRep);
                //        _db.SaveChanges();

                //        string successMsg
                //                = string.Format("Benthics Rep Added for Rep Num {0}", repNum);

                //        SetMessages("BenthicsRep_Success", successMsg);

                //        BenthicsRepsGridView_Panel.DataBind();
                //        BenthicsReps_UpdatePanel.Update();
                //    }
                //}
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBenthicGrid", "", "");
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