using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditCounty : System.Web.UI.Page
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

        public IQueryable<tlkCounty> GetCounties()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tlkCounty> activityCategories = _db.tlkCounties.OrderBy(c => c.Code);

                return activityCategories;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetCounties", "", "");
                return null;
            }
        }

        public void UpdateCounty(tlkCounty model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var countyToUpdate = _db.tlkCounties.Find(model.ID);

                    countyToUpdate.Code = model.Code;
                    countyToUpdate.Description = model.Description;
                    //activityCategoryToUpdate.Valid = 
                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        countyToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        countyToUpdate.UserLastModified = "Unknown";
                    }

                    countyToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "County Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateCounty", "", "");
            }
        }

        public void DeleteCounty(tlkCounty model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var countyToDelete = _db.tlkCounties.Find(model.ID);
                    _db.tlkCounties.Remove(countyToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "County Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteCounty", "", "");
                }
            }
        }

        public void AddNewCounty(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)CountyGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)CountyGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    var newCounty = new tlkCounty()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newCounty.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newCounty.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkCounties.Add(newCounty);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";
                        SuccessLabel.Text = "New County Added";
                        Response.Redirect("EditCounty.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewCounty", "", "");
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