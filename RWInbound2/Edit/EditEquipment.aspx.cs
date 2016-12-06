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
                SetMessages("OrgName", "", false, "");
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }

        private void SetMessages(string type = "", string message = "", bool showOrgName = false, string orgName = "")
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
                case "OrgName":
                    OrganizationNamePanel.Visible = showOrgName;
                    lblOrganizationName.Text = orgName;
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    OrganizationNamePanel.Visible = showOrgName;
                    lblOrganizationName.Text = orgName;
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
                    SetMessages("OrgName", "", false, "");
                    return null;
                }

                int orgID = 0;
                bool orgIDIsInt = Int32.TryParse(orgSelected, out orgID);

                if (!orgIDIsInt)
                {
                    SetMessages("OrgName", "", false, "");
                    return null;
                }

                IQueryable<EquipmentViewModel> equipment = (from e in _db.tblEquipments
                                                            join c in _db.tlkEquipCategories on e.CategoryID equals c.ID
                                                            orderby e.ItemName
                                                           select new EquipmentViewModel
                                                           {
                                                               ID = e.ID,
                                                               OrganizationID = e.OrganizationID,
                                                               ItemName = e.ItemName,
                                                               ItemDescription = e.ItemDescription,
                                                               CategoryID = e.CategoryID,
                                                               CategoryCode = c.Code,
                                                               Quantity = e.Quantity,
                                                               SerialNumber = e.SerialNumber,
                                                               DateReceived = e.DateReceived,
                                                               DateReJuv1 = e.DateReJuv1,
                                                               DateReJuv2 = e.DateReJuv2,
                                                               AutoReplaceDt = e.AutoReplaceDt,
                                                               Comment = e.Comment
                                                           }).OrderBy(e => e.ItemName);

                //var equipment = _db.tblEquipments
                //                    .Where(e => e.OrganizationID == orgID)
                //                    .OrderBy(e => e.ItemName);

                string orgName = _db.organizations
                                    .Where(o => o.ID == orgID)
                                    .Select(o => o.OrganizationName)
                                    .FirstOrDefault();

                SetMessages("OrgName", "", true, orgName);

                // remove
                this.Request.QueryString.Remove("successLabelMessage");
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
                //string kitNumber = string.Empty;

                orgNameSearch.Text = "";
                //kitNumber = kitNumberSearch.Text.Trim();

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
                SetMessages();
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
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewEquipment", "", "");
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