using System;
using System.Linq;
using System.Web;

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
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                var activityCategoryToUpdate = _db.tlkActivityCategories.Find(model.ID);

                activityCategoryToUpdate.Code = model.Code;
                activityCategoryToUpdate.Description = model.Description;
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
        }

        public void AddNewActivityCategory(tlkActivityCategory model)
        {
            var code = model.Code;
            int i = 0;
        }
    }
}