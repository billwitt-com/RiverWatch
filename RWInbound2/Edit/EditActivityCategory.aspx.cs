using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RWInbound2
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
            RiverWatchEntities _db = new RiverWatchEntities();

            IQueryable<tlkActivityCategory> activityCategories = _db.tlkActivityCategories;           

            return activityCategories;
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
                }

                ErrorLabel.Text = "";
                SuccessLabel.Text = "Activity Category Updated";
            }
            catch (Exception exp)
            {
                SuccessLabel.Text = "";
                ErrorLabel.Text = exp.Message;
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
                catch (Exception exp)
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = exp.Message;
                }
            }
        }

        public void AddNewActivityCategory(object sender, EventArgs e)
        {   
            try
            {
                string strCode = ((TextBox)ActivityCategoryGridView.FooterRow.FindControl("NewCode")).Text;
                int code = Convert.ToInt32(strCode);
                string description = ((TextBox)ActivityCategoryGridView.FooterRow.FindControl("NewDescription")).Text;

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
                }
                Response.Redirect("EditActivityCategory.aspx");
            }
            catch (Exception exp)
            {
                SuccessLabel.Text = "";
                ErrorLabel.Text = exp.Message;
            }
        }
    }
}