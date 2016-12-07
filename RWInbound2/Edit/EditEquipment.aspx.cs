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
    public partial class EditEquipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SetMessages("OrgName", "", false, "");
                SetMessages();
                OrganizationNamePanel.Visible = false;
                lblOrganizationName.Text = "";
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }

        //private void SetMessages(string type = "", string message = "", bool showOrgName = false, string orgName = "")
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

        public IQueryable BindEquipItems()
        {
            RiverWatchEntities _db = new RiverWatchEntities();
            var equipItems = (from e in _db.tlkEquipItems
                             orderby e.Description
                             select e);

            return equipItems;
        }

        public IQueryable BindCategories()
        {
            RiverWatchEntities _db = new RiverWatchEntities();
            var equipCategories = (from e in _db.tlkEquipCategories
                              orderby e.Code
                              select e);

            return equipCategories;
        }

        public IQueryable BindOrgnizations()
        {
            RiverWatchEntities _db = new RiverWatchEntities();
            var orgnizations = (from o in _db.organizations
                                   orderby o.OrganizationName
                                   select o);

            return orgnizations;
        }

        public IQueryable<EquipmentViewModel> GetEquipment([QueryString]string orgSelected = "",
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
               
                if (string.IsNullOrEmpty(orgSelected))
                {
                    //SetMessages("OrgName", "", false, "");
                    OrganizationNamePanel.Visible = false;
                    lblOrganizationName.Text = "";
                    return null;
                }

                int orgID = 0;
                bool orgIDIsInt = Int32.TryParse(orgSelected, out orgID);

                if (!orgIDIsInt)
                {
                    //SetMessages("OrgName", "", false, "");
                    OrganizationNamePanel.Visible = false;
                    lblOrganizationName.Text = "";
                    return null;
                }

                string orgName = _db.organizations
                                    .Where(o => o.ID == orgID)
                                    .Select(o => o.OrganizationName)
                                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(orgName))
                {                    
                    //SetMessages("OrgName", "", true, orgName);
                    OrganizationNamePanel.Visible = true;
                    lblOrganizationName.Text = orgName;
                }

                IQueryable<EquipmentViewModel> equipment = (from e in _db.tblEquipments
                                                            join c in _db.tlkEquipCategories on e.CategoryID equals c.ID into results
                                                            from subequip in results.DefaultIfEmpty()
                                                            where e.OrganizationID == orgID
                                                            orderby e.ItemName
                                                           select new EquipmentViewModel
                                                           {
                                                               ID = e.ID,
                                                               OrganizationID = e.OrganizationID,
                                                               OrganizationName = orgName,
                                                               ItemName = e.ItemName,
                                                               ItemDescription = e.ItemDescription,
                                                               CategoryID = (e.CategoryID == null ? 1 : e.CategoryID),
                                                               CategoryCode = (subequip == null ? String.Empty : subequip.Code),
                                                               Quantity = e.Quantity,
                                                               SerialNumber = e.SerialNumber,
                                                               DateReceived = e.DateReceived,
                                                               DateReJuv1 = e.DateReJuv1,
                                                               DateReJuv2 = e.DateReJuv2,
                                                               AutoReplaceDt = e.AutoReplaceDt,
                                                               Comment = e.Comment
                                                           }).OrderBy(e => e.ItemName);

                // remove
                this.Request.QueryString.Remove("successLabelMessage");
                if (string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", "");
                }                    
                //this.Request.QueryString.Clear();

                return equipment;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetEquipment", "", "");
                return null;
            }
        }

        protected void btnSearchOrgName_Click(object sender, EventArgs e)
        {
            try
            {
                string orgName = string.Empty;

                kitNumberSearch.Text = "";
                orgName = orgNameSearch.Text.Trim();

                int orgSelected = 0;

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    orgSelected = _db.organizations
                                    .Where(o => o.OrganizationName.Equals(orgName))
                                    .Select(o => o.ID)
                                    .FirstOrDefault();
                }

                if(orgSelected > 0)
                {
                    string redirect = "EditEquipment.aspx?orgSelected=" + orgSelected;

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchOrgName_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForOrgName(string prefixText, int count)
        {
            List<string> orgNames = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    orgNames = _db.organizations
                                    .Where(o => o.OrganizationName.StartsWith(prefixText))
                                    .OrderBy(o => o.OrganizationName)
                                    .Select(o => o.OrganizationName)
                                    .Distinct()
                                    .ToList();
                    orgNames.Sort();
                    return orgNames;
                }
            }
            catch (Exception ex)
            {
                EditEquipment editEquipment = new EditEquipment();
                editEquipment.HandleErrors(ex, ex.Message, "SearchForOrgName", "", "");
                return orgNames;
            }
        }

        protected void btnSearchKitNumber_Click(object sender, EventArgs e)
        {
            try
            {
                orgNameSearch.Text = "";

                int kitNumber = 0;
                bool kitNumberIsInt
                        = Int32.TryParse(kitNumberSearch.Text.Trim(), out kitNumber);
                if (kitNumberIsInt)
                {
                    int orgSelected = 0;

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        orgSelected = _db.organizations
                                        .Where(o => o.KitNumber == kitNumber)
                                        .Select(o => o.ID)
                                        .FirstOrDefault();
                    }

                    if (orgSelected > 0)
                    {
                        string redirect = "EditEquipment.aspx?orgSelected=" + orgSelected;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchKitNumber_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForkitNumber(string prefixText, int count)
        {
            List<string> kitNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    kitNumbers = _db.organizations
                                    .Where(o => o.KitNumber.ToString().StartsWith(prefixText))
                                    .OrderBy(o => o.OrganizationName)
                                    .Select(o => o.KitNumber.ToString())
                                    .Distinct()
                                    .ToList();
                    return kitNumbers;
                }
            }
            catch (Exception ex)
            {
                EditEquipment editEquipment = new EditEquipment();
                editEquipment.HandleErrors(ex, ex.Message, "SearchForkitNumber", "", "");
                return kitNumbers;
            }
        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                orgNameSearch.Text = "";
                kitNumberSearch.Text = "";

                string redirect = "EditEquipment.aspx";

                Response.Redirect(redirect, false);

            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdateEquipment(EquipmentViewModel model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var equipmentToUpdate = _db.tblEquipments.Find(model.ID);

                    if (equipmentToUpdate != null)
                    {
                        TryUpdateModel(equipmentToUpdate);  

                        if (ModelState.IsValid)
                        {
                            _db.SaveChanges();
                            
                            string successLabelMessage
                                    = string.Format("Equipment Updated: {0}", model.ItemName);

                            string redirect = string.Format("EditEquipment.aspx?orgSelected={0}&successLabelMessage={1}",
                                                             model.OrganizationID, successLabelMessage);

                            Response.Redirect(redirect, false);
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
                HandleErrors(ex, ex.Message, "UpdateEquipment", "", "");
            }
        }

        public void DeleteEquipment(EquipmentViewModel model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var equipmentToDelete = _db.tblEquipments.Find(model.ID);
                    _db.tblEquipments.Remove(equipmentToDelete);
                    _db.SaveChanges();

                    string orgName = lblOrganizationName.Text;
                    string successLabelMessage
                            = string.Format("Equipment item deleted: {0} Org: {1}", model.ItemName, orgName);

                    string redirect = string.Format("EditEquipment.aspx?orgSelected={0}&successLabelMessage={1}", 
                                                     model.OrganizationID, successLabelMessage);

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteEquipment", "", "");
            }
        }

        public void AddNewEquipment(object sender, EventArgs e)
        {
            try
            {
                SetMessages();

                var gridView = EquipmentGridView.Controls[0].Controls[0].FindControl("dropDownNewOrganizationIDs") == null ?
                                    EquipmentGridView.FooterRow : EquipmentGridView.Controls[0].Controls[0];

                var organizationID = ((DropDownList)gridView.FindControl("dropDownNewOrganizationIDs")).SelectedValue;
                int orgID = 0;
                bool orgIDIsInt = Int32.TryParse(organizationID, out orgID);

                var itemName = ((DropDownList)gridView.FindControl("dropDownNewItemNames")).SelectedItem.Text;

                string itemDescription = ((TextBox)gridView.FindControl("NewItemDescription")).Text;

                var equipCategory = ((DropDownList)gridView.FindControl("dropDownNewEquipCategories")).SelectedValue;
                int categoryID = 0;
                bool categoryIDIsInt = Int32.TryParse(equipCategory, out categoryID);


                string newQuantity = ((TextBox)gridView.FindControl("NewQuantity")).Text;
                int quantity = 0;
                bool quantityIsInt = Int32.TryParse(newQuantity, out quantity);

                string serialNumber = ((TextBox)gridView.FindControl("NewSerialNumber")).Text;
                DateTime? dateReceived = null;
                if (!string.IsNullOrEmpty(((TextBox)gridView.FindControl("NewDateReceived")).Text))
                {
                    dateReceived = Convert.ToDateTime(((TextBox)gridView.FindControl("NewDateReceived")).Text);
                }

                DateTime? dateReJuv1 = null;
                if (!string.IsNullOrEmpty(((TextBox)gridView.FindControl("NewDateReJuv1")).Text))
                {
                    dateReJuv1 = Convert.ToDateTime(((TextBox)gridView.FindControl("NewDateReJuv1")).Text);
                }

                DateTime? dateReJuv2 = null;
                if (!string.IsNullOrEmpty(((TextBox)gridView.FindControl("NewDateReJuv2")).Text))
                {
                    dateReJuv2 = Convert.ToDateTime(((TextBox)gridView.FindControl("NewDateReJuv2")).Text);
                }

                DateTime? autoReplaceDt = null;
                if (!string.IsNullOrEmpty(((TextBox)gridView.FindControl("NewAutoReplaceDt")).Text))
                {
                    autoReplaceDt = Convert.ToDateTime(((TextBox)gridView.FindControl("NewAutoReplaceDt")).Text);
                }

                string comment = ((TextBox)gridView.FindControl("NewComment")).Text;

                var newEquipment = new tblEquipment()
                {
                    OrganizationID = orgID,
                    ItemName = itemName,
                    ItemDescription = itemDescription,
                    CategoryID = categoryID,
                    Quantity = quantity,
                    SerialNumber = serialNumber,
                    DateReceived = dateReceived,
                    DateReJuv1 = dateReJuv1,
                    DateReJuv2 = dateReJuv2,
                    AutoReplaceDt = autoReplaceDt,
                    Comment = comment
                };

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    _db.tblEquipments.Add(newEquipment);
                    _db.SaveChanges();

                    string orgName = ((DropDownList)gridView.FindControl("dropDownNewOrganizationIDs")).SelectedItem.Text;
                    string successLabelMessage
                                = string.Format("New Equipment item added: {0} Org: {1}", itemName, orgName);

                    string redirect = string.Format("EditEquipment.aspx?orgSelected={0}&successLabelMessage={1}", orgID, successLabelMessage);

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewEquipment", "", "");
            }
        }

        protected void EquipmentGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }

        protected void EquipmentGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetMessages();
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