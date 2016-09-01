using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditQuarterSection : System.Web.UI.Page
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

        public IQueryable<tlkQuarterSection> GetQuarterSections([QueryString]string descriptionSearchTerm = "",
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
                    return _db.tlkQuarterSections.Where(c => c.Description.Equals(descriptionSearchTerm))
                                       .OrderBy(c => c.Code);
                }
                IQueryable<tlkQuarterSection> quarterSections = _db.tlkQuarterSections
                                               .OrderBy(c => c.Code);
                return quarterSections;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetQuarterSections", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string descriptionSearchTerm = descriptionSearch.Text;
                string redirect = "EditQuarterSection.aspx?descriptionSearchterm=" + descriptionSearchTerm;

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
                Response.Redirect("EditQuarterSection.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForQuarterSectionsDescription(string prefixText, int count)
        {
            List<string> quarterSectionsDescriptions = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    quarterSectionsDescriptions = _db.tlkQuarterSections
                                             .Where(c => c.Description.Contains(prefixText))
                                             .Select(c => c.Description).ToList();

                    return quarterSectionsDescriptions;
                }
            }
            catch (Exception ex)
            {
                EditQuarterSection editQuarterSection = new EditQuarterSection();
                editQuarterSection.HandleErrors(ex, ex.Message, "SearchForQuarterSectionsDescription", "", "");
                return quarterSectionsDescriptions;
            }
        }

        public void UpdateQuarterSection(tlkQuarterSection model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var quarterSectionToUpdate = _db.tlkQuarterSections.Find(model.ID);

                    quarterSectionToUpdate.Code = model.Code;
                    quarterSectionToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        quarterSectionToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        quarterSectionToUpdate.UserLastModified = "Unknown";
                    }

                    quarterSectionToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Quarter Section Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateQuarterSection", "", "");
            }
        }

        public void DeleteQuarterSection(tlkQuarterSection model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var quarterSectionToDelete = _db.tlkQuarterSections.Find(model.ID);
                    _db.tlkQuarterSections.Remove(quarterSectionToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Quarter Section Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteQuarterSection", "", "");
                }
            }
        }

        public void AddNewQuarterSection(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)QuarterSectionsGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)QuarterSectionsGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newQuarterSection = new tlkQuarterSection()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newQuarterSection.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newQuarterSection.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkQuarterSections.Add(newQuarterSection);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Quarter Section Added: " + newQuarterSection.Description;
                        string redirect = "EditQuarterSection.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewQuarterSection", "", "");
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