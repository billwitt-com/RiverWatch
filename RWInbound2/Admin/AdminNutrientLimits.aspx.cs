using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class AdminNutrientLimits : System.Web.UI.Page
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

        public IQueryable<NutrientLimit> GetNutrientLimits([QueryString]string elementSearchTerm = "",
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

                if (!string.IsNullOrEmpty(elementSearchTerm))
                {
                    return _db.NutrientLimits.Where(c => c.Element.Equals(elementSearchTerm))
                                       .OrderBy(c => c.Element);
                }
                IQueryable<NutrientLimit> nutrientLimits = _db.NutrientLimits
                                            .OrderBy(c => c.Element);
                PropertyInfo isreadonly 
                    = typeof(System.Collections.Specialized.NameValueCollection)
                            .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();
                return nutrientLimits;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetNutrientLimits", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string elementSearchTerm = elementSearch.Text;
                string redirect = "AdminNutrientLimits.aspx?elementSearchTerm=" + elementSearchTerm;

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
                Response.Redirect("AdminNutrientLimits.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForElement(string prefixText, int count)
        {
            List<string> elements = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    elements = _db.NutrientLimits
                                    .Where(c => c.Element.Contains(prefixText))
                                    .Select(c => c.Element)
                                    .Distinct()
                                    .ToList();
                    return elements;
                }
            }
            catch (Exception ex)
            {
                AdminNutrientLimits adminNutrientLimits = new AdminNutrientLimits();
                adminNutrientLimits.HandleErrors(ex, ex.Message, "SSearchForElement", "", "");
                return elements;
            }
        }

        public void UpdateNutrientLimit(NutrientLimit model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var nutrientLimitToUpdate = _db.NutrientLimits.Find(model.ID);

                    nutrientLimitToUpdate.Element = model.Element;
                    nutrientLimitToUpdate.RowID = model.RowID;
                    nutrientLimitToUpdate.Reporting = model.Reporting;
                    nutrientLimitToUpdate.MDL = model.MDL;
                    nutrientLimitToUpdate.DvsTDifference = model.DvsTDifference;
                    nutrientLimitToUpdate.HighLimit = model.HighLimit;

                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Nutrient Limit Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateNutrientLimit", "", "");
            }
        }

        public void DeleteNutrientLimit(NutrientLimit model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var nutrientLimitToDelete = _db.NutrientLimits.Find(model.ID);
                    _db.NutrientLimits.Remove(nutrientLimitToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Nutrient Limit Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteNutrientLimit", "", "");
                }
            }
        }

        protected void AddNutrientLimit(object sender, EventArgs e)
        {
            try
            {
                string element = ((TextBox)NutrientLimitsGridView.FooterRow.FindControl("NewElement")).Text;
                string rowID = ((TextBox)NutrientLimitsGridView.FooterRow.FindControl("NewRowId")).Text;
                decimal? reporting = Convert.ToDecimal(((TextBox)NutrientLimitsGridView.FooterRow.FindControl("NewReporting")).Text);
                decimal? mDL = Convert.ToDecimal(((TextBox)NutrientLimitsGridView.FooterRow.FindControl("NewMDL")).Text);
                decimal? dvsTDifference = Convert.ToDecimal(((TextBox)NutrientLimitsGridView.FooterRow.FindControl("NewDvsTDifference")).Text);
                decimal? highLimit = Convert.ToDecimal(((TextBox)NutrientLimitsGridView.FooterRow.FindControl("NewHighLimit")).Text);

                var newNutrientLimit = new NutrientLimit()
                {
                    Element = element,
                    RowID = rowID,
                    Reporting = reporting,
                    MDL = mDL,
                    DvsTDifference = dvsTDifference,
                    HighLimit = highLimit,
                    Valid = true,
                    DateCreated = DateTime.Now
                };

                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    newNutrientLimit.UserCreated
                        = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    newNutrientLimit.UserCreated = "Unknown";
                }

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    _db.NutrientLimits.Add(newNutrientLimit);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";

                    string successLabelText = "New Nutrient Limit Added: " + newNutrientLimit.Element;
                    string redirect = "AdminNutrientLimits.aspx?successLabelMessage=" + successLabelText;

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNutrientLimit", "", "");
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