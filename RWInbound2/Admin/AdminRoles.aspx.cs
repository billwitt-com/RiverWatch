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
    public partial class AdminRoles : System.Web.UI.Page
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

        public IQueryable<Role> GetRoles([QueryString]string roleNameSearchTerm = "",
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

                if (!string.IsNullOrEmpty(roleNameSearchTerm))
                {
                    return _db.Roles.Where(c => c.Name.Equals(roleNameSearchTerm))
                                       .OrderBy(c => c.Name);
                }
                IQueryable<Role> roles = _db.Roles
                                            .OrderBy(c => c.Name);
                return roles;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetRoles", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string roleNameSearchTerm = nameSearch.Text;
                string redirect = "AdminRoles.aspx?roleNameSearchTerm=" + roleNameSearchTerm;

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
                Response.Redirect("AdminRoles.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForRoleName(string prefixText, int count)
        {
            List<string> roleNames = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    roleNames = _db.Roles
                                    .Where(c => c.Name.Contains(prefixText))
                                    .Select(c => c.Name)
                                    .Distinct()
                                    .ToList();
                    return roleNames;
                }
            }
            catch (Exception ex)
            {
                AdminRoles adminRoles = new AdminRoles();
                adminRoles.HandleErrors(ex, ex.Message, "SearchForRoleName", "", "");
                return roleNames;
            }
        }

        public void UpdateRole(Role model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var roleToUpdate = _db.Roles.Find(model.ID);

                    roleToUpdate.Name = model.Name;
                    roleToUpdate.Level = model.Level;

                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Role Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateRole", "", "");
            }
        }

        public void DeleteRole(Role model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var roleToDelete = _db.Roles.Find(model.ID);
                    _db.Roles.Remove(roleToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Role Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteRole", "", "");
                }
            }
        }

        public void AddNewRole(object sender, EventArgs e)
        {
            try
            {
                string name = ((TextBox)RolesGridView.FooterRow.FindControl("NewName")).Text;
                int level = Convert.ToInt32(((TextBox)RolesGridView.FooterRow.FindControl("NewLevel")).Text);

                var newRole = new Role()
                {
                    Name = name,
                    Level = level,
                    Valid = true,
                    DateCreated = DateTime.Now
                };

                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    newRole.CreatedBy
                        = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    newRole.CreatedBy = "Unknown";
                }

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    _db.Roles.Add(newRole);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";

                    string successLabelText = "New Role Added: " + newRole.Name;
                    string redirect = "AdminRoles.aspx?successLabelMessage=" + successLabelText;

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddRole", "", "");
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