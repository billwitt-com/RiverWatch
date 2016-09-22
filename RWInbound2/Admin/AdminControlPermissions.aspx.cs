using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class AdminControlPermissions : System.Web.UI.Page
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

        public IQueryable<ControlPermission> GetControlPermissions([QueryString]string controlPermissionsPageNameSearchTerm = "",
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

                if (!string.IsNullOrEmpty(controlPermissionsPageNameSearchTerm))
                {
                    return _db.ControlPermissions.Where(c => c.PageName.Equals(controlPermissionsPageNameSearchTerm))
                                       .OrderBy(c => c.PageName);
                }
                IQueryable<ControlPermission> controlPermissions = _db.ControlPermissions
                                               .OrderBy(c => c.PageName);
                return controlPermissions;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetControlPermissions", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string controlPermissionsPageNameSearchTerm = controlPermissionsPageNameSearch.Text;
                string redirect = "AdminControlPermissions.aspx?controlPermissionsPageNameSearchTerm=" + controlPermissionsPageNameSearchTerm;

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
                Response.Redirect("AdminControlPermissions.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForControlPermissionsPageName(string prefixText, int count)
        {
            List<string> pageNames = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    pageNames = _db.ControlPermissions
                                             .Where(c => c.PageName.Contains(prefixText))                                             
                                             .Select(c => c.PageName)
                                             .Distinct()
                                             .ToList();
                    return pageNames;
                }
            }
            catch (Exception ex)
            {
                AdminControlPermissions adminControlPermissions = new AdminControlPermissions();
                adminControlPermissions.HandleErrors(ex, ex.Message, "SearchForControlPermissionsPageName", "", "");
                return pageNames;
            }
        }

        public void UpdateControlPermission(ControlPermission model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var controlPermissionToUpdate = _db.ControlPermissions.Find(model.ID);

                    controlPermissionToUpdate.Description = model.Description;
                    controlPermissionToUpdate.PageName = model.PageName;
                    controlPermissionToUpdate.ControlID = model.ControlID;
                    controlPermissionToUpdate.RoleValue = model.RoleValue;
                    controlPermissionToUpdate.Comments = model.Comments;

                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Control Permission Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateControlPermission", "", "");
            }
        }

        public void DeleteControlPermission(ControlPermission model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var controlPermissionToDelete = _db.ControlPermissions.Find(model.ID);
                    _db.ControlPermissions.Remove(controlPermissionToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Control Permission Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteControlPermission", "", "");
                }
            }
        }

        public void AddNewControlPermission(object sender, EventArgs e)
        {
            try
            {
                string description = ((TextBox)ControlPermissionsGridView.FooterRow.FindControl("NewDescription")).Text;
                string pageName = ((TextBox)ControlPermissionsGridView.FooterRow.FindControl("NewPageName")).Text;
                string controlID = ((TextBox)ControlPermissionsGridView.FooterRow.FindControl("NewControlID")).Text;
                int roleValue = Convert.ToInt32(((TextBox)ControlPermissionsGridView.FooterRow.FindControl("NewRoleValue")).Text);
                string comments = ((TextBox)ControlPermissionsGridView.FooterRow.FindControl("NewComments")).Text;

                if (string.IsNullOrEmpty(pageName))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Page Name field is required.";
                }
                else
                {
                    var newControlPermission = new ControlPermission()
                    {
                        Description = description,
                        PageName = pageName,
                        ControlID = controlID,
                        RoleValue = roleValue,
                        Comments = comments,
                        Valid = true,
                        DateCreated = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newControlPermission.CreatedBy
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newControlPermission.CreatedBy = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.ControlPermissions.Add(newControlPermission);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Control Permission Added: " + newControlPermission.Description;
                        string redirect = "AdminControlPermissions.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddControlPermission", "", "");
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