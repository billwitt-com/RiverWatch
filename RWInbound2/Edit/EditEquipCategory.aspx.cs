using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditEquipCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }

        public IQueryable<tlkEquipCategory> GetEquipCategories([QueryString]string descriptionSearchTerm = "",
                                                              [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = successLabelMessage;
                }

                if (!string.IsNullOrEmpty(descriptionSearchTerm))
                {
                    return _db.tlkEquipCategories.Where(c => c.Description.Equals(descriptionSearchTerm))
                                                 .OrderBy(c => c.Code);
                }
                IQueryable<tlkEquipCategory> equipCategories = _db.tlkEquipCategories
                                                     .OrderBy(c => c.Code);
                return equipCategories;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetEquipCategories", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditEquipCategory.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditEquipCategory.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForEquipCategoriesDescription(string prefixText, int count)
        {
            List<string> equipCategoriesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    equipCategoriesDescriptions = _db.tlkEquipCategories
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return equipCategoriesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditEquipCategory editEquipCategory = new EditEquipCategory();
                editEquipCategory.HandleErrors(ex, ex.Message, "SearchForEquipCategoriesDescription", "", "");
                return equipCategoriesDescriptions;
            }
        }

        public void UpdateEquipCategory(tlkEquipCategory model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var equipCategoryToUpdate = _db.tlkEquipCategories.Find(model.ID);

                    equipCategoryToUpdate.Code = model.Code;
                    equipCategoryToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        equipCategoryToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        equipCategoryToUpdate.UserLastModified = "Unknown";
                    }

                    equipCategoryToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Equipment Category Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateEquipCategory", "", "");
            }
        }

        public void DeleteEquipCategory(tlkEquipCategory model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var equipCategoryToDelete = _db.tlkEquipCategories.Find(model.ID);
                    _db.tlkEquipCategories.Remove(equipCategoryToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Equipment Category Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteEquipCategory", "", "");
                }
            }
        }

        public void AddNewEquipCategory(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)EquipCategoriesGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)EquipCategoriesGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newEquipCategory = new tlkEquipCategory()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newEquipCategory.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newEquipCategory.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkEquipCategories.Add(newEquipCategory);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Equipment Category Added: " + newEquipCategory.Description;
                        string redirect = "EditEquipCategory.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewEquipCategory", "", "");
            }
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SuccessLabel.Text = "";
            ErrorLabel.Text = ex.Message;
        }
    }
}