using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
            ErrorLabel.Text = "";
            SuccessLabel.Text = "";
        }

        public IQueryable<Project> GetProjects([QueryString]string projectNameSearchTerm = "",
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

                if (!string.IsNullOrEmpty(projectNameSearchTerm))
                {
                    return _db.Projects.Where(c => c.ProjectName.Equals(projectNameSearchTerm))
                                       .OrderBy(c => c.ProjectName);
                }
                IQueryable<Project> projects = _db.Projects
                                               .OrderBy(c => c.ProjectName);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return projects;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetProjects", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string projectNameSearchTerm = projectNameSearch.Text;
                string redirect = "EditProject.aspx?projectNameSearchterm=" + projectNameSearchTerm;

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
                Response.Redirect("EditProject.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForProjectsName(string prefixText, int count)
        {
            List<string> projectsNames = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    projectsNames = _db.Projects
                                             .Where(c => c.ProjectName.Contains(prefixText))
                                             .Select(c => c.ProjectName).ToList();

                    return projectsNames;
                }
            }
            catch (Exception ex)
            {
                EditProject editProject = new EditProject();
                editProject.HandleErrors(ex, ex.Message, "SearchForProjectsName", "", "");
                return projectsNames;
            }
        }

        public void UpdateProject(Project model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var projectToUpdate = _db.Projects.Find(model.ProjectID);

                    projectToUpdate.ProjectName = model.ProjectName;
                    projectToUpdate.ProjectDescription = model.ProjectDescription;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        projectToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        projectToUpdate.UserLastModified = "Unknown";
                    }

                    projectToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Project Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateProject", "", "");
            }
        }

        public void DeleteProject(Project model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var projectToDelete = _db.Projects.Find(model.ProjectID);
                    _db.Projects.Remove(projectToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Project Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteProject", "", "");
                }
            }
        }

        public void AddNewProject(object sender, EventArgs e)
        {
            try
            {
                string name = ((TextBox)ProjectsGridView.FooterRow.FindControl("NewProjectName")).Text;
                string projectDescription = ((TextBox)ProjectsGridView.FooterRow.FindControl("NewProjectDescription")).Text;

                if (string.IsNullOrEmpty(name))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Project Name field is required.";
                }
                else
                {
                    var newProject = new Project()
                    {
                        ProjectName = name,
                        ProjectDescription = projectDescription,
                        valid = true,
                        DateLastModified = DateTime.Now
                    };

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newProject.UserCreated
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newProject.UserCreated = "Unknown";
                    }

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        _db.Projects.Add(newProject);
                        _db.SaveChanges();
                        ErrorLabel.Text = "";

                        string successLabelText = "New Project Added: " + newProject.ProjectName;
                        string redirect = "EditProject.aspx?successLabelMessage=" + successLabelText;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewProject", "", "");
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