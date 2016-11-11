using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditSubPara : System.Web.UI.Page
    {
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

        public IQueryable<tlkSubPara> GetSubParas([QueryString]string parameterNameSearchSearchTerm = "",
                                                  [QueryString]string successLabelMessage = "")
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (!string.IsNullOrEmpty(parameterNameSearchSearchTerm))
                {
                    return _db.tlkSubPara
                              .Where(p => p.ParameterName.Equals(parameterNameSearchSearchTerm))
                              .OrderBy(p => p.ParameterName);
                }

                IQueryable<tlkSubPara> SubParas = _db.tlkSubPara
                                                      .OrderBy(p => p.ParameterName);
                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return SubParas;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetSubParas", "", "");
                return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForParameterName(string prefixText, int count)
        {
            List<string> parameters = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    parameters = _db.tlkSubPara
                                    .Where(p => p.ParameterName.StartsWith(prefixText))
                                    .OrderBy(p => p.ParameterName)
                                    .Select(p => p.ParameterName).ToList();

                    return parameters;
                }
            }
            catch (Exception ex)
            {
                EditSubPara editSubPara = new EditSubPara();
                editSubPara.HandleErrors(ex, ex.Message, "SearchForParameterName", "", "");
                return parameters;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string parameterNameSearchSearchTerm = parameterNameSearch.Text;
                string redirect = "EditSubPara.aspx?parameterNameSearchSearchTerm=" + parameterNameSearchSearchTerm;

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
                Response.Redirect("EditSubPara.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        public void UpdateSubPara(tlkSubPara model)
        {
            try
            {
                SetMessages();

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var subParaToUpdate = _db.tlkSubPara.Find(model.SubParaID);
                    TryUpdateModel(subParaToUpdate);

                    _db.SaveChanges();
                    string successMsg = string.Format("Sub Para Updated for Parameter Name: {0}", subParaToUpdate.ParameterName);
                    SetMessages("Success", successMsg);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateSubPara", "", "");
            }
        }

        public void DeleteSubPara(tlkSubPara model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var subParaToDelete = _db.tlkSubPara.Find(model.SubParaID);
                    _db.tlkSubPara.Remove(subParaToDelete);
                    _db.SaveChanges();
                    string successMsg = string.Format("Sub Para Deleted for Parameter Name: {0} ", subParaToDelete.ParameterName);
                    SetMessages("Success", successMsg);
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteSubPara", "", "");
                }
            }
        }

        public void InsertSubPara(tlkSubPara model)
        {
            try
            {
                SetMessages();
                TryUpdateModel(model);

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    _db.tlkSubPara.Add(model);
                    _db.SaveChanges();

                    string successLabelText = "New Sub Para Added for Parameter Name: " + model.ParameterName;
                    string redirect = "EditSubPara.aspx?successLabelMessage=" + successLabelText;

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "InsertSubPara", "", "");
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
    }
}