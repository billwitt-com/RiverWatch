using RWInbound2.View_Models;
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
    public partial class EditWaterBodyID : System.Web.UI.Page
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

        public IQueryable<tblWBKeyViewModel> GettblWBKeys([QueryString]string waterBodyIDSearchSearchTerm = "",
                                                          [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (!string.IsNullOrEmpty(waterBodyIDSearchSearchTerm))
                {    
                    return (from w in _db.tblWBKeys
                            orderby w.WBID
                            select new tblWBKeyViewModel
                            {
                                ID = w.ID,
                                WBID = w.WBID,
                                WATERSHED = w.WATERSHED,
                                BASIN = w.BASIN,
                                SUBBASIN = w.SUBBASIN,
                                REGION = w.REGION,
                                SEGMENT = w.SEGMENT,
                                DESIG = w.DESIG,
                                SegDesc = w.SegDesc,
                                VerifyDate = w.VerifyDate,
                                Comment = w.Comment
                            })
                            .Where(w => w.WBID.ToLower().Equals(waterBodyIDSearchSearchTerm.ToLower()))
                            .OrderBy(wb => wb.WBID);
                }

                IQueryable<tblWBKeyViewModel> tblWBKeys = (from w in _db.tblWBKeys
                                                           orderby w.WBID
                                                           select new tblWBKeyViewModel
                                                           {
                                                               ID = w.ID,
                                                               WBID = w.WBID,
                                                               WATERSHED = w.WATERSHED,
                                                               BASIN = w.BASIN,
                                                               SUBBASIN = w.SUBBASIN,
                                                               REGION = w.REGION,
                                                               SEGMENT = w.SEGMENT,
                                                               DESIG = w.DESIG,
                                                               SegDesc = w.SegDesc,
                                                               VerifyDate = w.VerifyDate,
                                                               Comment = w.Comment
                                                           }).OrderBy(wb => wb.WBID);

                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return tblWBKeys;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GettblWBKeys", "", "");
                return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForWaterBodyID(string prefixText, int count)
        {
            List<string> waterBodyIDs = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    waterBodyIDs = _db.tblWBKeys
                                    .Where(wb => wb.WBID.StartsWith(prefixText))
                                    .OrderBy(wb => wb.WBID)
                                    .Select(wb => wb.WBID).ToList();

                    return waterBodyIDs;
                }
            }
            catch (Exception ex)
            {
                EditWaterBodyID editWaterBodyID = new EditWaterBodyID();
                editWaterBodyID.HandleErrors(ex, ex.Message, "SearchForParameterName", "", "");
                return waterBodyIDs;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string waterBodyIDSearchSearchTerm = waterBodyIDSearch.Text;
                string redirect = "EditWaterBodyID.aspx?waterBodyIDSearchSearchTerm=" + waterBodyIDSearchSearchTerm;

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
                Response.Redirect("EditWaterBodyID.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdatetblWBKey(tblWBKeyViewModel model, [Control("dropDownWATERSHED")] string watershed,
                                   [Control("dropdownBASIN")] string basin, [Control("dropdownSUBBASIN")] string subbasin)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var tblWBKeyToUpdate = _db.tblWBKeys.Find(model.ID);
                    
                    if(tblWBKeyToUpdate != null)
                    {
                        TryUpdateModel(tblWBKeyToUpdate);

                        tblWBKeyToUpdate.WATERSHED = watershed;
                        tblWBKeyToUpdate.BASIN = basin;
                        tblWBKeyToUpdate.SUBBASIN = subbasin;

                        if (ModelState.IsValid)
                        {
                            _db.SaveChanges();

                            string successMsg = string.Format("Water Body ID Updated: {0}", tblWBKeyToUpdate.WBID);
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
                HandleErrors(ex, ex.Message, "UpdatetblWBKey", "", "");
            }
        }

        public void DeletetblWBKey(tblWBKeyViewModel model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var tblWBKeyToDelete = _db.tblWBKeys.Find(model.ID);
                    _db.tblWBKeys.Remove(tblWBKeyToDelete);
                    _db.SaveChanges();

                    string successLabelText = string.Format("Water Body ID Deleted: {0} ", tblWBKeyToDelete.WBID);
                    SetMessages("Success", successLabelText);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeletetblWBKey", "", "");
                }
            }
        }
        public void InserttblWBKey(tblWBKeyViewModel model, [Control("newDropDownWATERSHED")] string watershed,
                                   [Control("newDropdownBASIN")] string basin, [Control("newDropdownSUBBASIN")] string subbasin)
        {
            try
            {
                SetMessages();
               
                tblWBKey newtblWBKey = new tblWBKey()
                {
                    WBID = model.WBID,
                    WATERSHED = watershed,
                    BASIN = basin,
                    SUBBASIN = subbasin,
                    REGION = model.REGION,
                    SEGMENT = model.SEGMENT,
                    DESIG = model.DESIG,
                    SegDesc = model.SegDesc,
                    VerifyDate = model.VerifyDate,
                    Comment = model.Comment,
                    Valid = true
                };
                
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    _db.tblWBKeys.Add(newtblWBKey);
                    _db.SaveChanges();

                    string successLabelText = string.Format("New Water Body ID Added: {0}", newtblWBKey.WBID);
                    string redirect = "EditWaterBodyID.aspx?successLabelMessage=" + successLabelText;

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "InserttblWBKey", "", "");
            }
        }

       public IQueryable<tlkRiverWatchWaterShed> BindWATERSHED()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var riverWatchWaterSheds = _db.tlkRiverWatchWaterSheds
                                              .OrderBy(ws => ws.Description);
                return riverWatchWaterSheds;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindWATERSHED", "", "");
                return null;
            }
        }

        public IQueryable<tlkWQCCWaterShed> BindBASIN()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                var tlkWQCCWaterSheds = _db.tlkWQCCWaterSheds
                                            .OrderBy(ws => ws.Description);
                return tlkWQCCWaterSheds;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "BindBASIN", "", "");
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

        protected void tblWBKeyViewModelFormView_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            if(e.Cancel || e.CancelingEdit || e.NewMode == FormViewMode.Edit || e.NewMode == FormViewMode.Insert)
            {
                SetMessages();
            }
        }
    }
}