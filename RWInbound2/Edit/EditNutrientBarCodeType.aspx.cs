using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditNutrientBarCodeType : System.Web.UI.Page
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

        public IQueryable<tlkNutrientBarCodeType> GetNutrientBarCodeTypes([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkNutrientBarCodeTypes.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkNutrientBarCodeType> nutrientBarCodeTypes = _db.tlkNutrientBarCodeTypes
                                               .OrderBy(c => c.Code);
                return nutrientBarCodeTypes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetNutrientBarCodeTypes", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditNutrientBarCodeType.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditNutrientBarCodeType.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForNutrientBarCodeTypesDescription(string prefixText, int count)
        {
            List<string> nutrientBarCodeTypesDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    nutrientBarCodeTypesDescriptions = _db.tlkNutrientBarCodeTypes
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return nutrientBarCodeTypesDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditNutrientBarCodeType editNutrientBarCodeType = new EditNutrientBarCodeType();
                editNutrientBarCodeType.HandleErrors(ex, ex.Message, "SearchForNutrientBarCodeTypesDescription", "", "");
                return nutrientBarCodeTypesDescriptions;
            }
        }

        public void UpdateNutrientBarCodeType(tlkNutrientBarCodeType model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var nutrientBarCodeTypeToUpdate = _db.tlkNutrientBarCodeTypes.Find(model.ID);

                    nutrientBarCodeTypeToUpdate.Code = model.Code;
                    nutrientBarCodeTypeToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        nutrientBarCodeTypeToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        nutrientBarCodeTypeToUpdate.UserLastModified = "Unknown";
                    }

                    nutrientBarCodeTypeToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Nutrient Bar Code Type Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateNutrientBarCodeType", "", "");
            }
        }

        public void DeleteNutrientBarCodeType(tlkNutrientBarCodeType model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var nutrientBarCodeTypeToDelete = _db.tlkNutrientBarCodeTypes.Find(model.ID);
                    _db.tlkNutrientBarCodeTypes.Remove(nutrientBarCodeTypeToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Nutrient Bar Code Type Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteNutrientBarCodeType", "", "");
                }
            }
        }

        public void AddNewNutrientBarCodeType(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)NutrientBarCodeTypesGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)NutrientBarCodeTypesGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newNutrientBarCodeType = new tlkNutrientBarCodeType()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newNutrientBarCodeType.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newNutrientBarCodeType.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkNutrientBarCodeTypes.Add(newNutrientBarCodeType);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Nutrient Bar Code Type Added: " + newNutrientBarCodeType.Description;
                        string redirect = "EditNutrientBarCodeType.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewNutrientBarCodeType", "", "");
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