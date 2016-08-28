using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditCommunities : System.Web.UI.Page
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

        public IQueryable<tlkCommunity> GetCommunities()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tlkCommunity> community = _db.tlkCommunities;

                return community;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetCommunities", "", "");
                return null;
            }
        }

        public void UpdateCommunity(tlkCommunity model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var communityToUpdate = _db.tlkCommunities.Find(model.ID);

                    communityToUpdate.Code = model.Code;
                    communityToUpdate.Description = model.Description;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        communityToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        communityToUpdate.UserLastModified = "Unknown";
                    }

                    communityToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Community Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateCommunity", "", "");
            }
        }

        public void DeleteCommunity(tlkCommunity model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var communityToDelete = _db.tlkCommunities.Find(model.ID);
                    _db.tlkCommunities.Remove(communityToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Community Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteCommunity", "", "");
                }
            }
        }

        public void AddNewCommunity(object sender, EventArgs e)
        {
            try
            {
                string strCode = ((TextBox)CommunitiesGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)CommunitiesGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(strCode))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    int code;
                    bool convertToInt = int.TryParse(strCode, out code);
                    if (!convertToInt)
                    {
                        SuccessLabel.Text = "";
                        ErrorLabel.Text = "Code field must be an integer number.";
                    }
                    else
                    {
                        var newCommunity = new tlkCommunity()
                        {
                            Code = code,
                            Description = description,
                            Valid = true,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newCommunity.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newCommunity.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkCommunities.Add(newCommunity);
                            _db.SaveChanges();
                            ErrorLabel.Text = "";
                            SuccessLabel.Text = "New Community Type Added";
                            Response.Redirect("EditCommunities.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewCommunity", "", "");
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