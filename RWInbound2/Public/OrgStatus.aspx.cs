using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace RWInbound2.Reports
{
    public partial class OrgStatus : System.Web.UI.Page
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string orgName = string.Empty;
                string cmdStr = string.Empty;

                orgName = orgNameSearch.Text.Trim();
                cmdStr = string.Format("SELECT * FROM OrgStatusView WHERE OrganizationName Like '{0}' order by contractEndDate desc", orgName);

                SqlDataSource1.SelectCommand = cmdStr;
                ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rd1);
                ReportViewer1.LocalReport.Refresh();
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
                orgNameSearch.Text = "";
                string cmdStr = string.Empty;

                cmdStr = string.Format("SELECT * FROM OrgStatusView ORDER BY OrganizationName, contractEndDate desc ");

                SqlDataSource1.SelectCommand = cmdStr;
                ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rd1);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForOrgName(string prefixText, int count)
        {
            List<string> orgNames = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    orgNames = _db.organizations
                                    .Where(c => c.OrganizationName.StartsWith(prefixText))
                                    .Select(c => c.OrganizationName)
                                    .Distinct()
                                    .ToList();
                    return orgNames;
                }
            }
            catch (Exception ex)
            {
                OrgStatus adminRoles = new OrgStatus();
                adminRoles.HandleErrors(ex, ex.Message, "SearchForOrgName", "", "");
                return orgNames;
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