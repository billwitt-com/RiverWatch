using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditActivityCategory : System.Web.UI.Page
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
        public IQueryable<tlkActivityCategory> GetActivityCategories()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tlkActivityCategory> activityCategories = _db.tlkActivityCategories;

                return activityCategories;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetActivityCategories", "", "");
                return null;
            }            
        }

        public void UpdateActivityCategory(tlkActivityCategory model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var activityCategoryToUpdate = _db.tlkActivityCategories.Find(model.ID);

                    activityCategoryToUpdate.Code = model.Code;
                    activityCategoryToUpdate.Description = model.Description;
                    //activityCategoryToUpdate.Valid = 
                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        activityCategoryToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        activityCategoryToUpdate.UserLastModified = "Unknown";
                    }

                    activityCategoryToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Activity Category Updated";
                }                        
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateActivityCategory", "", "");               
            }                   
        }

        public void DeleteActivityCategory(tlkActivityCategory model)
        {  
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var activityCategoryToDelete = _db.tlkActivityCategories.Find(model.ID);
                    _db.tlkActivityCategories.Remove(activityCategoryToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Activity Category Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteActivityCategory", "", "");                   
                }
            }
        }

        public void AddNewActivityCategory(object sender, EventArgs e)
        {   
            try
            {
                string strCode = ((TextBox)ActivityCategoryGridView.FooterRow.FindControl("NewCode")).Text;                
                string description = ((TextBox)ActivityCategoryGridView.FooterRow.FindControl("NewDescription")).Text;

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
                        var newActivityCategory = new tlkActivityCategory()
                        {
                            Code = code,
                            Description = description,
                            Valid = true,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newActivityCategory.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newActivityCategory.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkActivityCategories.Add(newActivityCategory);
                            _db.SaveChanges();                            
                            ErrorLabel.Text = "";
                            SuccessLabel.Text = "New Activity Category Added";
                            Response.Redirect("EditActivityCategory.aspx", false);
                        }
                    }                   
                }                    
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewActivityCategory", "", "");                
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