using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class EditWaterCodes1 : System.Web.UI.Page
    {
        private string selectedDrainage = "";
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

        public IQueryable BindDrainages()
        {
            RiverWatchEntities _db = new RiverWatchEntities();
            var drainages = (from d in _db.WaterCodeDrainages 
                             orderby d.Description
                             select d);
                            
            return drainages;
        }

        public List<WaterCodeDrainage> BindDrainagesToList()
        {
            RiverWatchEntities _db = new RiverWatchEntities();
            var drainages = (from d in _db.WaterCodeDrainages
                             orderby d.Description
                             select d).ToList<WaterCodeDrainage>();

            return drainages;
        }

        public IQueryable<tblWatercode> GetWaterCodes([QueryString]string drainageSelected = "",
                                                      [QueryString]string waterCodeSelected = "",
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

                if(!string.IsNullOrEmpty(waterCodeSelected) && string.IsNullOrEmpty(drainageSelected))
                {
                    var waterCodesFilteredByWaterCode = _db.tblWatercodes
                                                           .Where(wc => wc.WATERCODE.Equals(waterCodeSelected))
                                                           .OrderBy(wc => wc.WATERNAME);
                    DrainageDropDown.Items.FindByValue("0").Selected = true;
         
                    return waterCodesFilteredByWaterCode;
                }

                if (string.IsNullOrEmpty(drainageSelected))
                {
                    return null;
                }

                var waterCodes = _db.tblWatercodes
                                    .Where(wc => wc.DRAINAGE.Equals(drainageSelected))
                                    .OrderBy(wc => wc.WATERNAME);

                DrainageDropDown.Items.FindByValue(drainageSelected).Selected = true;
                
                // remove
                this.Request.QueryString.Remove("successLabelMessage");
                //this.Request.QueryString.Clear();

                return waterCodes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetWaterCodes", "", "");
                return null;
            }
        }

        protected void DrainageDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string drainageSelected = string.Empty;
            string selectedValue = DrainageDropDown.SelectedItem.Value;
            if (!selectedValue.Equals("0"))
            {
                drainageSelected
                    = DrainageDropDown.SelectedItem.Value;
            }
            
            string redirect = "EditWaterCodes.aspx?drainageSelected=" + drainageSelected;

            Response.Redirect(redirect, false);
        }

        public void UpdateWaterCode(tblWatercode model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var waterCodeToUpdate = _db.tblWatercodes.Find(model.ID);

                    TryUpdateModel(waterCodeToUpdate);

                    waterCodeToUpdate.DRAINAGE = selectedDrainage;

                    _db.SaveChanges();

                    string successMsg = string.Format("Water Code Updated: {0} ", waterCodeToUpdate.WATERCODE);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateWaterCode", "", "");
            }
        }

        public void DeleteWaterCode(tblWatercode model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var waterCodeToDelete = _db.tblWatercodes.Find(model.ID);
                    _db.tblWatercodes.Remove(waterCodeToDelete);
                    _db.SaveChanges();

                    string successMsg = string.Format("Water Code Deleted: {0} ", model.WATERCODE);
                    SetMessages("Success", successMsg);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteWaterCode", "", "");
                }
            }
        }

        public void AddNewWaterCode(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                string wATERCODE = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewWATERCODE")).Text;
                string dRAINAGE
                            = ((DropDownList)WaterCodeGridView.FooterRow.FindControl("NewDrainageDropDown")).SelectedItem.Value;

                if (string.IsNullOrEmpty(wATERCODE))
                {
                    ((Label)WaterCodeGridView.FooterRow.FindControl("lblWATERCODERequired")).Visible = true;
                }
                else if (string.IsNullOrEmpty(dRAINAGE))
                {
                    ((Label)WaterCodeGridView.FooterRow.FindControl("lblDRAINAGERequired")).Visible = true;
                }
                else
                {
                    string wATERNAME = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewWATERNAME")).Text;
                    bool sTREAM = ((CheckBox)WaterCodeGridView.FooterRow.FindControl("NewSTREAM")).Checked;

                    decimal? aCRESMILES = 0;
                    if (!string.IsNullOrEmpty(((TextBox)WaterCodeGridView.FooterRow.FindControl("NewACRESMILES")).Text))
                    {
                        aCRESMILES = Convert.ToDecimal(((TextBox)WaterCodeGridView.FooterRow.FindControl("NewACRESMILES")).Text);
                    }
                    string lOCATION = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewLOCATION")).Text;
                    string dRAINNAME = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewDRAINNAME")).Text;
                    string hYDROCODE = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewHYDROCODE")).Text;
                    string cOUNTY = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewCOUNTY")).Text;
                    string rEGION = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewREGION")).Text;
                    string aWMAREA = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewAWMAREA")).Text;
                    string sTKCODE = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewSTKCODE")).Text;
                    string bIOLOGIST = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewBIOLOGIST")).Text;
                    string aREABIO = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewAREABIO")).Text;
                    string aDDLDATA = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewADDLDATA")).Text;
                    string mAPCOORDIN = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewMAPCOORDIN")).Text;
                    string aTLASPAGE = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewATLASPAGE")).Text;
                    string cATEGORY = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewCATEGORY")).Text;

                    decimal? eLEVATION = 0;
                    if (!string.IsNullOrEmpty(((TextBox)WaterCodeGridView.FooterRow.FindControl("NewELEVATION")).Text))
                    {
                        eLEVATION = Convert.ToDecimal(((TextBox)WaterCodeGridView.FooterRow.FindControl("NewELEVATION")).Text);
                    }

                    decimal? uT_ELEVATI = 0;
                    if (!string.IsNullOrEmpty(((TextBox)WaterCodeGridView.FooterRow.FindControl("NewUT_ELEVATI")).Text))
                    {
                        uT_ELEVATI = Convert.ToDecimal(((TextBox)WaterCodeGridView.FooterRow.FindControl("NewUT_ELEVATI")).Text);
                    }
                    string oWNERSHIP = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewOWNERSHIP")).Text;
                    bool wILDERNESS = ((CheckBox)WaterCodeGridView.FooterRow.FindControl("NewWILDERNESS")).Checked;
                    string bLMMAP = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewBLMMAP")).Text;
                    string tOPOMAP = ((TextBox)WaterCodeGridView.FooterRow.FindControl("NewTOPOMAP")).Text;
                    bool oBSOLETE = ((CheckBox)WaterCodeGridView.FooterRow.FindControl("NewOBSOLETE")).Checked;

                    var newWaterCode = new tblWatercode()
                    {
                        WATERCODE = wATERCODE,
                        WATERNAME = wATERNAME,
                        STREAM = sTREAM,
                        ACRESMILES = aCRESMILES,
                        LOCATION = lOCATION,
                        DRAINAGE = dRAINAGE,
                        DRAINNAME = dRAINNAME,
                        HYDROCODE = hYDROCODE,
                        COUNTY = cOUNTY,
                        REGION = rEGION,
                        AWMAREA = aWMAREA,
                        STKCODE = sTKCODE,
                        BIOLOGIST = bIOLOGIST,
                        AREABIO = aREABIO,
                        ADDLDATA = aDDLDATA,
                        MAPCOORDIN = mAPCOORDIN,
                        ATLASPAGE = aTLASPAGE,
                        CATEGORY = cATEGORY,
                        ELEVATION = eLEVATION,
                        UT_ELEVATI = uT_ELEVATI,
                        OWNERSHIP = oWNERSHIP,
                        WILDERNESS = wILDERNESS,
                        BLMMAP = bLMMAP,
                        TOPOMAP = tOPOMAP,
                        OBSOLETE = oBSOLETE,
                        Valid = true
                    };

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tblWatercodes.Add(newWaterCode);
                        _db.SaveChanges();

                        string successLabelText = "New Water Code Added: " + newWaterCode.WATERCODE;
                        
                        string drainageSelected = string.Empty;
                        string selectedValue = DrainageDropDown.SelectedItem.Value;
                        if (!selectedValue.Equals("0"))
                        {
                            drainageSelected
                                = DrainageDropDown.SelectedItem.Text;
                        }
                        string redirect = string.Empty;
                        if (string.IsNullOrEmpty(drainageSelected))
                        {
                            redirect = string.Format("EditWaterCodes.aspx?successLabelMessage={0}", successLabelText);
                        }
                        else
                        {
                            redirect = string.Format("EditWaterCodes.aspx?drainageSelected={0}&successLabelMessage={1}", drainageSelected, successLabelText);
                        }

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewWaterCode", "", "");
            }
        }


        protected void WaterCodeGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string test = string.Empty;
        }

        protected void WaterCodeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();            
        }

        protected void WaterCodeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetMessages();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForWaterCode(string prefixText, int count)
        {
            List<string> waterCodes = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    waterCodes = _db.tblWatercodes
                                    .Where(wc => wc.WATERCODE.StartsWith(prefixText))
                                    .OrderBy(wc => wc.WATERCODE)
                                    .Select(wc => wc.WATERCODE)
                                    .Distinct()
                                    .ToList();
                    waterCodes.Sort();
                    return waterCodes;
                }
            }
            catch (Exception ex)
            {
                EditWaterCodes1 editWaterCodes1 = new EditWaterCodes1();
                editWaterCodes1.HandleErrors(ex, ex.Message, "SearchForWaterCode", "", "");
                return waterCodes;
            }
        }

        protected void btnSearchWaterCode_Click(object sender, EventArgs e)
        {
            try
            {
                string waterCodeSelected = string.Empty;

                waterCodeSelected = WaterCodeSearchTextBox.Text;

                string redirect = string.Empty;
                if (string.IsNullOrEmpty(waterCodeSelected))
                {
                    redirect = "EditWaterCodes.aspx";
                }
                else
                {
                    redirect = "EditWaterCodes.aspx?waterCodeSelected=" + waterCodeSelected;
                }

                Response.Redirect(redirect, false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchWaterCode_Click", "", "");
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

        protected void WaterCodeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList updateDrainageDropDown
                        = (DropDownList)e.Row.FindControl("updateDrainageDropDown");
                    updateDrainageDropDown.Items.Clear();
                    updateDrainageDropDown.DataSource = BindDrainagesToList();
                    updateDrainageDropDown.DataBind();
                    var item = e.Row.DataItem as tblWatercode;
                    updateDrainageDropDown.Items.FindByValue(item.DRAINAGE).Selected = true;
                }
            }
        }

        protected void WaterCodeGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            selectedDrainage = 
                ((DropDownList)WaterCodeGridView.Rows[e.RowIndex]
                                                      .FindControl("updateDrainageDropDown")).SelectedItem.Value;
        }
    }
}