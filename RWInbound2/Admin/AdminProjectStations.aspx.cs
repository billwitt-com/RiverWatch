using AjaxControlToolkit;
using RWInbound2.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.ModelBinding;
using System.Collections;

namespace RWInbound2.Admin
{
    public partial class AdminProjectStations : System.Web.UI.Page
    {
        private ArrayList assignedList = new ArrayList();
       
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                SetMessages();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }       

        private void SetMessages(string type="", string message="")
        {
            var errorLabel = ((Label)UpdateProjectStationsForm.FindControl("ErrorLabel"));
            var successLabel = ((Label)UpdateProjectStationsForm.FindControl("SuccessLabel"));
            var stationNumberAssignedLabel = ((Label)UpdateProjectStationsForm.FindControl("StationNumberAssignedLabel"));
            
            switch (type)
            {
                case "Success":
                    errorLabel.Text = "";
                    successLabel.Text = message;
                    stationNumberAssignedLabel.Text = "";
                    break;
                case "Error":
                    errorLabel.Text = message;
                    successLabel.Text = "";
                    stationNumberAssignedLabel.Text = "";
                    break;
                case "StationNumber":
                    errorLabel.Text = "";
                    successLabel.Text = "";
                    stationNumberAssignedLabel.Text = message;
                    break;
                default:
                    errorLabel.Text = "";
                    successLabel.Text = "";
                    stationNumberAssignedLabel.Text = "";
                    break;
            }
        }

        public ProjectStationViewModel GetProjectStations([QueryString]string project = "",
                                                          [QueryString]string river = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();
                
                var projects = _db.Project.OrderBy(p => p.ProjectName).ToList<Project>();
                var rivers = (from r in _db.Stations
                              orderby r.River
                              select new River { Value = r.River, Text = r.River }).Distinct().OrderBy(r => r.Text).ToList<River>();               

                ProjectStationViewModel vm = new ProjectStationViewModel()
                {
                    Projects = projects,
                    Rivers = rivers
                };

                return vm;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetProjects", "", "");
                return null;
            }            
        }        
        protected void ProjectsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                SetMessages();

                int selectedProject 
                    = Convert.ToInt32(((DropDownList)UpdateProjectStationsForm.FindControl("ProjectsDropDown")).SelectedItem.Value);
                ListBox avaliableStationsListBox 
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox"));
                ListBox assignedStationsListBox 
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox"));

                ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).ClearSelection();
                avaliableStationsListBox.Items.Clear();
                avaliableStationsListBox.DataBind();

                ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).Enabled = true;
                ((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Enabled = true;
                ((Button)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBoxButton")).Enabled = true;

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var assignedStations = (from s in _db.Stations
                                            join ps in _db.ProjectStations on s.StationNumber equals ps.StationNumber
                                            where ps.ProjectID == selectedProject
                                            orderby s.StationName
                                            select s).ToList<Station>();

                    assignedStationsListBox.Items.Clear();
                    assignedStationsListBox.DataSource = assignedStations;
                    assignedStationsListBox.DataBind();                   
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "ProjectsDropDown_SelectedIndexChanged", "", "");
            }
        }

        protected void RiversDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string selectedRiver 
                    = ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).SelectedItem.Text;
                int selectedProject 
                    = Convert.ToInt32(((DropDownList)UpdateProjectStationsForm.FindControl("ProjectsDropDown")).SelectedItem.Value);
                ListBox avaliableStationsListBox 
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox"));

                ((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Text = "";

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var stationsForSpecifiedRiver = _db.Stations
                                                       .Where(s => s.River.Equals(selectedRiver))
                                                       .OrderBy(s => s.River).ToList<Station>();

                    var assignedStations = (from s in _db.Stations
                                            join ps in _db.ProjectStations on s.StationNumber equals ps.StationNumber
                                            where ps.ProjectID == selectedProject
                                            orderby s.StationName
                                            select s).ToList<Station>();

                    var avaliableStations 
                        = stationsForSpecifiedRiver.Except(assignedStations)
                                                    .Select(s => s)
                                                    .OrderBy(s => s.StationName)
                                                    .ToList<Station>();

                    avaliableStationsListBox.Items.Clear();
                    avaliableStationsListBox.DataSource = avaliableStations;
                    avaliableStationsListBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "RiversDropDown_SelectedIndexChanged", "", "");
            }
        }

        protected void StationNumberSearchTextBoxButton_Click(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                int selectedProject
                    = Convert.ToInt32(((DropDownList)UpdateProjectStationsForm.FindControl("ProjectsDropDown")).SelectedItem.Value);
                int stationNumberSearchNumber = Convert.ToInt32(((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Text);
                ListBox avaliableStationsListBox
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox"));

                ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).ClearSelection();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var stationsForStationNumber = _db.Stations
                                                       .Where(s => s.StationNumber == stationNumberSearchNumber)
                                                       .OrderBy(s => s.StationName).ToList<Station>();

                    var assignedStations = (from s in _db.Stations
                                            join ps in _db.ProjectStations on s.StationNumber equals ps.StationNumber
                                            where ps.ProjectID == selectedProject
                                            orderby s.StationName
                                            select s).ToList<Station>();

                    var avaliableStations
                        = stationsForStationNumber.Except(assignedStations)
                                                    .Select(s => s)
                                                    .OrderBy(s => s.StationName)
                                                    .ToList<Station>();

                    if(avaliableStations.Count > 0)
                    {
                        avaliableStationsListBox.Items.Clear();
                        avaliableStationsListBox.DataSource = avaliableStations;
                        avaliableStationsListBox.DataBind();
                    }
                    else
                    {
                        SetMessages("StationNumber", "Station Number is already assigned.");
                    }                    
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "StationNumberSearchTextBoxButton_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForStationNumbers(string prefixText, int count)
        {
            AdminProjectStations adminProjectStations = new AdminProjectStations();
            List<string> stationNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    stationNumbers = _db.Stations
                                  .Where(s => s.StationNumber.ToString().Contains(prefixText))
                                  .Select(s => s.StationNumber.ToString()).ToList();

                    return stationNumbers;
                }
            }
            catch (Exception ex)
            {                
                adminProjectStations.HandleErrors(ex, ex.Message, " SearchForStationNumbers", "", "");
                return stationNumbers;
            }
        }

        protected void AddSingleToAssigned_Click(object sender, EventArgs e)
        {
            try
            {
                ListBox avaliableStationsListBox 
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox"));
                ListBox assignedStationsListBox 
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox"));

                if (avaliableStationsListBox.SelectedIndex >= 0)
                {
                    for (int i = 0; i < avaliableStationsListBox.Items.Count; i++)
                    {
                        if (avaliableStationsListBox.Items[i].Selected)
                        {
                            if (!assignedList.Contains(avaliableStationsListBox.Items[i]))
                            {
                                assignedList.Add(avaliableStationsListBox.Items[i]);
                            }
                        }
                    }
                    for (int i = 0; i < assignedList.Count; i++)
                    {
                        if (!assignedStationsListBox.Items.Contains(((ListItem)assignedList[i])))
                        {
                            ListItem newItem = ((ListItem)assignedList[i]);
                            assignedStationsListBox.Items.Add(newItem);
                        }
                        avaliableStationsListBox.Items.Remove(((ListItem)assignedList[i]));
                    }
                    assignedStationsListBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddSingleToAssigned_Click", "", "");
            }            
        }

        protected void AddAllToAssigned_Click(object sender, EventArgs e)
        {
            try
            {
                ListBox avaliableStationsListBox = ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox"));
                ListBox assignedStationsListBox = ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox"));

                while (avaliableStationsListBox.Items.Count != 0)
                {
                    for (int i = 0; i < avaliableStationsListBox.Items.Count; i++)
                    {
                        assignedStationsListBox.Items.Add(avaliableStationsListBox.Items[i]);
                        avaliableStationsListBox.Items.Remove(avaliableStationsListBox.Items[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddAllToAssigned_Click", "", "");
            }            
        }

        protected void RemoveAssigned_Click(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                ListBox assignedStationsListBox = ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox"));
                List<ListItem> listItemsToRemove = new List<ListItem>();

                if (assignedStationsListBox.SelectedIndex >= 0)
                {
                    for (int i = 0; i < assignedStationsListBox.Items.Count; i++)
                    {
                        if (assignedStationsListBox.Items[i].Selected)
                        {
                            listItemsToRemove.Add(assignedStationsListBox.Items[i]);
                            //assignedStationsListBox.Items.Remove(assignedStationsListBox.Items[i]);
                        }
                    }  
                     
                    for(int j = 0; j < listItemsToRemove.Count; j++)
                    {
                        assignedStationsListBox.Items.Remove(listItemsToRemove[j]);
                    }                 
                    assignedStationsListBox.SelectedIndex = -1;
                    //SetMessages("Success", "Station(s)  Removed!");
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "RemoveAssigned_Click", "", "");
            }
        }        

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                bool updated = false;
                int selectedProjectID 
                    = Convert.ToInt32(((DropDownList)UpdateProjectStationsForm.FindControl("ProjectsDropDown")).SelectedItem.Value);
                var assignedStationsListBox 
                    = ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox"));
                var newAssignedStationNumber = new List<int?>();
                for (int i = 0; i < assignedStationsListBox.Items.Count; i++)
                {
                    int stationNumber = Convert.ToInt32(assignedStationsListBox.Items[i].Value);
                    newAssignedStationNumber.Add(stationNumber);
                }

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {   
                    var currentAssignedStations = (from s in _db.Stations
                                                    join ps in _db.ProjectStations on s.StationNumber equals ps.StationNumber
                                                    where ps.ProjectID == selectedProjectID
                                                    orderby s.StationName
                                                    select s).ToList<Station>();

                    var currentStationIds = currentAssignedStations.Select(s => s.StationNumber).ToList<int?>();
                    var newStationNumbers = newAssignedStationNumber.Except(currentStationIds).ToList<int?>();
                    var removedStationNumbers = currentStationIds.Except(newAssignedStationNumber).ToList<int?>();

                    string userCreated = string.Empty;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        userCreated
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        userCreated = "Unknown";
                    }

                    var projectStations = _db.ProjectStations
                                             .Where(ps => ps.ProjectID == selectedProjectID)
                                             .ToList();
                    for (int i = 0; i < newStationNumbers.Count; i++)
                    {
                        int? stationNumber = newStationNumbers[i];
                        if (stationNumber != null)
                        {
                            ProjectStation newProjectStation = new ProjectStation()
                            {
                                ProjectID = selectedProjectID,
                                StationNumber = stationNumber,
                                DateCreated = DateTime.Now,
                                UserCreated = userCreated,
                                Valid = true
                            };

                            _db.ProjectStations.Add(newProjectStation);
                            updated = true;
                        }
                    }
                    for (int j = 0; j < removedStationNumbers.Count; j++)
                    {
                        int? stationNumber = removedStationNumbers[j];
                        if (stationNumber != null)
                        {
                            var projectStationToRemove = projectStations.Where(ps => ps.StationNumber == stationNumber)
                                                                        .FirstOrDefault();
                            if (projectStationToRemove != null)
                            {
                                _db.ProjectStations.Remove(projectStationToRemove);
                                updated = true;
                            }
                        }
                    }
                    
                    _db.SaveChanges();

                    ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).ClearSelection();
                    ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).Enabled = true;
                    ((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Text = "";
                    ((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Enabled = true;
                    ((Button)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBoxButton")).Enabled = true;

                    if (updated) SetMessages("Success", "Project Stations Updated!");
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "Update_Click", "", "");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            SetMessages();

            ((DropDownList)UpdateProjectStationsForm.FindControl("ProjectsDropDown")).ClearSelection();
            ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).ClearSelection();
            ((DropDownList)UpdateProjectStationsForm.FindControl("RiversDropDown")).Enabled = false;
            ((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Text = "";
            ((TextBox)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBox")).Enabled = false;
            ((Button)UpdateProjectStationsForm.FindControl("StationNumberSearchTextBoxButton")).Enabled = false;

            ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox")).Items.Clear();
            ((ListBox)UpdateProjectStationsForm.FindControl("AvaliableStationsListBox")).DataBind();

            ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox")).Items.Clear();
            ((ListBox)UpdateProjectStationsForm.FindControl("AssignedStationsListBox")).DataBind();
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SetMessages();

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
                SetMessages("Error", sb.ToString());               
            }
            else
            {
               SetMessages("Error", ex.Message);
            }
        }
        
    }
}