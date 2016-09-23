using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditEquipItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
            ErrorLabel.Text = "";
            SuccessLabel.Text = "";
        }

        public IQueryable<tlkEquipItem> GetEquipItems([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkEquipItems.Where(c => c.Description.Equals(descriptionSearchTerm))
                                                 .OrderBy(c => c.Code);
                }
                IQueryable<tlkEquipItem> equipItems = _db.tlkEquipItems
                                                     .OrderBy(c => c.Code);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return equipItems;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetEquipItems", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditEquipItems.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditEquipItems.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForequipItemsDescription(string prefixText, int count)
        {
            List<string> equipItemsDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    equipItemsDescriptions = _db.tlkEquipItems
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return equipItemsDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditEquipItems editEquipItems = new EditEquipItems();
                editEquipItems.HandleErrors(ex, ex.Message, "SearchForEquipItemsDescription", "", "");
                return equipItemsDescriptions;
            }
        }

        public void UpdateEquipItem(tlkEquipItem model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var equipItemToUpdate = _db.tlkEquipItems.Find(model.ID);

                    equipItemToUpdate.Code = model.Code;
                    equipItemToUpdate.Description = model.Description;
                    equipItemToUpdate.HasSerial = model.HasSerial;
                    equipItemToUpdate.HasRejuv = model.HasRejuv;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        equipItemToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        equipItemToUpdate.UserLastModified = "Unknown";
                    }

                    equipItemToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Equipment Item Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateEquipItem", "", "");
            }
        }

        public void DeleteEquipItem(tlkEquipItem model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var equipItemToDelete = _db.tlkEquipItems.Find(model.ID);
                    _db.tlkEquipItems.Remove(equipItemToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Equipment Item Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteEquipItem", "", "");
                }
            }
        }

        public void AddNewEquipItem(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)EquipItemsGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)EquipItemsGridView.FooterRow.FindControl("NewDescription")).Text;
                bool hasSerial = ((CheckBox)EquipItemsGridView.FooterRow.FindControl("NewHasSerial")).Checked;
                bool hasRejuv = ((CheckBox)EquipItemsGridView.FooterRow.FindControl("NewHasRejuv")).Checked;
                string category = ((TextBox)EquipItemsGridView.FooterRow.FindControl("NewCategory")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newEquipItem = new tlkEquipItem()
                    {
                        Code = code,
                        Description = description,
                        HasSerial = hasSerial,
                        HasRejuv = hasRejuv,
                        Category = category,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newEquipItem.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newEquipItem.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkEquipItems.Add(newEquipItem);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Equipment Item Added: " + newEquipItem.Description;
                        string redirect = "EditEquipItems.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewEquipItem", "", "");
            }
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SuccessLabel.Text = "";

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
                ErrorLabel.Text = sb.ToString();
            }
            else
            {
                SuccessLabel.Text = "";
                ErrorLabel.Text = ex.Message;
            }
        }
    }
}