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
                SetMessages();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
                //ExpWaterFormView.ChangeMode(FormViewMode.Edit);
            }            
        }

        private void SetMessages(string type = "", string message = "")
        {
            switch (type)
            {
                case "Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    break;
                case "Error":
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    break;
            }
        }

        public IQueryable<NEWexpWater> GetExpWater([QueryString]string sampleNumberSearchTerm = "",
                                                   [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }
                string searchTerm = string.Empty;

                if (!string.IsNullOrEmpty(sampleNumberSearchTerm))
                {
                    searchTerm = sampleNumberSearchTerm;                    
                }

                var expWaters = _db.NEWexpWaters
                                  .Where(e => (e.SampleNumber.Equals(searchTerm) && e.Valid == true));
                // remove
                this.Request.QueryString.Remove("successLabelMessage");

                return expWaters;
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
                                        .Where(e => e.SampleNumber.StartsWith(prefixText) && e.Valid == true)
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

                SetMessages();

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
                    //ErrorLabel.Text = ""; 
                    string successMsg = string.Format("Exp Water Sample Number: {0} Type Code: {1} Updated.", sampleNumberSearchTerm, newExpWater.TypeCode);
                    //SuccessLabel.Text = successMsg;
                    string redirect = "EditExpWater.aspx?sampleNumberSearchTerm=" + sampleNumberSearchTerm +
                                       "&successLabelMessage=" + successMsg;
                   
                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateExpWater", "", "");
            }
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

        protected void ExpWaterFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {
            SetMessages();
        }
    }
}