using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;

namespace RWInbound2.Edit
{
    public partial class EditEcoRegion : System.Web.UI.Page
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

        public IQueryable<tlkEcoRegion> GetEcoRegions([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkEcoRegions.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkEcoRegion> ecoRegions = _db.tlkEcoRegions
                                                     .OrderBy(c => c.Code);
                return ecoRegions;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetEcoRegions", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditEcoRegion.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditEcoRegion.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForEcoRegionsDescription(string prefixText, int count)
        {
            List<string> ecoRegionsDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    ecoRegionsDescriptions = _db.tlkEcoRegions
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return ecoRegionsDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditEcoRegion editEcoRegion = new EditEcoRegion();
                editEcoRegion.HandleErrors(ex, ex.Message, "SearchForEcoRegionsDescription", "", "");
                return ecoRegionsDescriptions;
            }
        }

        public void UpdateEcoRegion(tlkEcoRegion model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var ecoRegionToUpdate = _db.tlkEcoRegions.Find(model.ID);

                    ecoRegionToUpdate.Code = model.Code;
                    ecoRegionToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        ecoRegionToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        ecoRegionToUpdate.UserLastModified = "Unknown";
                    }

                    ecoRegionToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Eco Region Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateEcoRegion", "", "");
            }
        }

        public void DeleteEcoRegion(tlkEcoRegion model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var ecoRegionToDelete = _db.tlkEcoRegions.Find(model.ID);
                    _db.tlkEcoRegions.Remove(ecoRegionToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Eco Region Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteEcoRegion", "", "");
                }
            }
        }

        public void AddNewEcoRegion(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)EcoRegionGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)EcoRegionGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newEcoRegion = new tlkEcoRegion()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newEcoRegion.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newEcoRegion.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkEcoRegions.Add(newEcoRegion);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Eco Region Added: " + newEcoRegion.Description;
                        string redirect = "EditEcoRegion.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewEcoRegion", "", "");
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