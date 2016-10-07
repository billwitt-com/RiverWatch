using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity.Validation;
using System.Reflection;

namespace RWInbound2.Admin
{
    public partial class EditExpWater : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
                ExpWaterFormView.ChangeMode(FormViewMode.Edit);
            }
            ErrorLabel.Text = "";
            SuccessLabel.Text = "";
            ExpWaterFormView.ChangeMode(FormViewMode.Edit);
        }

        public NEWexpWater GetExpWater([QueryString]string sampleNumberSearchTerm = "",
                                       [QueryString]string successLabelMessage = "")
        {
            try
            {
                String s = Request.QueryString["sampleNumberSearchTerm"];
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = successLabelMessage;
                }
                string searchTerm = string.Empty;

                if (!string.IsNullOrEmpty(sampleNumberSearchTerm))
                {
                    searchTerm = sampleNumberSearchTerm;                    
                }

                var expWater = _db.NEWexpWaters
                                  .Where(e => (e.SampleNumber.Equals(searchTerm) && e.Valid == true))
                                  .FirstOrDefault();
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return expWater;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetExpWater", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sampleNumberSearchTerm = sampleNumberSearch.Text;
                string redirect = "EditExpWater.aspx?sampleNumberSearchTerm=" + sampleNumberSearchTerm;

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
                Response.Redirect("EditExpWater.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForExpWaterSampleNumber(string prefixText, int count)
        {
            List<string> sampleNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    sampleNumbers = _db.NEWexpWaters
                                        .Where(e => e.SampleNumber.Contains(prefixText) && e.Valid == true)
                                        .Select(c => c.SampleNumber).ToList();

                    return sampleNumbers;
                }
            }
            catch (Exception ex)
            {
                EditExpWater editExpWater = new EditExpWater();
                editExpWater.HandleErrors(ex, ex.Message, "SearchForExpWaterSampleNumber", "", "");
                return sampleNumbers;
            }
        }

        public void UpdateExpWater(NEWexpWater model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var oldExpWater = _db.NEWexpWaters.Find(model.ID);
                    oldExpWater.Valid = false;

                    var newExpWater = new NEWexpWater();
                    newExpWater = model;

                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        newExpWater.CreatedBy
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        newExpWater.CreatedBy = "Unknown";
                    }

                    newExpWater.CreateDate = DateTime.Now;
                    _db.NEWexpWaters.Add(newExpWater);
                    _db.SaveChanges();                    

                    string sampleNumberSearchTerm = oldExpWater.SampleNumber;
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "Exp Water " + sampleNumberSearchTerm + " Updated.";
                    string successLabelMessage = "Exp Water " + sampleNumberSearchTerm + " Updated.";
                    string redirect = "EditExpWater.aspx?sampleNumberSearchTerm=" + sampleNumberSearchTerm +
                                       "&successLabelMessage=" + successLabelMessage;
                   
                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateExpWater", "", "");
            }
        }
        protected void ExpWaterFormView_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            try
            {
                // Use the NewMode property to determine the mode to which the 
                // FormView control is transitioning.
                switch (e.NewMode)
                {
                    case FormViewMode.Edit:
                        // Hide the pager row and clear the Label control
                        // when transitioning to edit mode.      
                        if (e.CancelingEdit)
                        {
                            string sampleNumberSearchTerm = ((Label)ExpWaterFormView.FindControl("SampleNumber")).Text;
                            string successLabelMessage = "Canceled " + sampleNumberSearchTerm + ".";
                            string redirect = "EditExpWater.aspx?sampleNumberSearchTerm=" + sampleNumberSearchTerm +
                                               "&successLabelMessage=" + successLabelMessage;

                            ExpWaterFormView.ChangeMode(FormViewMode.Edit);
                            Response.Redirect(redirect, false);
                        }
                        break;
                    case FormViewMode.ReadOnly:
                        // Display the pager row and confirmation message
                        // when transitioning to edit mode.                       
                        break;
                    case FormViewMode.Insert:
                        // Cancel the mode change if the FormView
                        // control attempts to transition to insert 
                        // mode.
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }                
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
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