using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditActivityType : System.Web.UI.Page
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

        public IQueryable<tlkActivityType> GetActivityTypes()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tlkActivityType> activityTypes = _db.tlkActivityTypes;

                return activityTypes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetActivityTypes", "", "");
                return null;
            }
        }

        public void UpdateActivityType(tlkActivityType model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var activityTypeToUpdate = _db.tlkActivityTypes.Find(model.ID);

                    if (string.IsNullOrEmpty(model.Code))
                    {
                        SuccessLabel.Text = "";
                        ErrorLabel.Text = "Code field is required.";
                    }
                    else
                    {
                        activityTypeToUpdate.Code = model.Code;
                        activityTypeToUpdate.Description = model.Description;
                        //activityCategoryToUpdate.Valid = 
                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            activityTypeToUpdate.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            activityTypeToUpdate.UserLastModified = "Unknown";
                        }

                        activityTypeToUpdate.DateLastModified = DateTime.Now;
                        _db.SaveChanges();
                    }

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Activity Type Updated";
                }                        
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateActivityType", "", "");
            }
        }

        public void DeleteActivityType(tlkActivityType model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var activityTypeToDelete = _db.tlkActivityTypes.Find(model.ID);
                    _db.tlkActivityTypes.Remove(activityTypeToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Activity Type Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteActivityType", "", "");
                }
            }
        }

        public void AddNewActivityType(object sender, EventArgs e)
        {
            try
            {
                string code = ((TextBox)ActivityTypeGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)ActivityTypeGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(code)) {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }               
                else
                {
                    var newActivityType = new tlkActivityType()
                    {
                        Code = code,
                        Description = description,
                        Valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newActivityType.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newActivityType.UserLastModified = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.tlkActivityTypes.Add(newActivityType);
                        _db.SaveChanges();
                        Response.Redirect("EditActivityType.aspx", false);
                        ErrorLabel.Text = "";
                        SuccessLabel.Text = "New Activity Type Added";
                    }
                }  
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewDeleteActivityType", "", "");                
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