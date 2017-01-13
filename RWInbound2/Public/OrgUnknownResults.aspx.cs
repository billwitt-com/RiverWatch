using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace RWInbound2.Reports
{
    public partial class OrgUnknownResults : System.Web.UI.Page
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

        protected void btnSearchOrgName_Click(object sender, EventArgs e)
        {
            try
            {
                string orgName = string.Empty;
                string cmdStr = string.Empty;

                kitNumberSearch.Text = "";
                orgName = orgNameSearch.Text.Trim();
                cmdStr = string.Format("SELECT * FROM UnknownResultsForOrgView WHERE OrganizationName = '{0}' ORDER BY [DateSent] desc", orgName);

                SqlDataSource1.SelectCommand = cmdStr;
                ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rd1);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchOrgName_Click", "", "");
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
                                    .Where(o => o.OrganizationName.StartsWith(prefixText))
                                    .OrderBy(o => o.OrganizationName)
                                    .Select(o => o.OrganizationName)
                                    .Distinct()
                                    .ToList();
                    orgNames.Sort();
                    return orgNames;
                }
            }
            catch (Exception ex)
            {
                OrgUnknownResults orgUnknownResults = new OrgUnknownResults();
                orgUnknownResults.HandleErrors(ex, ex.Message, "SearchForOrgName", "", "");
                return orgNames;
            }
        }

        protected void btnSearchKitNumber_Click(object sender, EventArgs e)
        {
            try
            {
                string kitNumber = string.Empty;
                string cmdStr = string.Empty;

                orgNameSearch.Text = "";
                kitNumber = kitNumberSearch.Text.Trim();
                cmdStr = string.Format("SELECT * FROM UnknownResultsForOrgView WHERE Kitnumber = {0} ORDER BY [DateSent] desc", kitNumber);

                SqlDataSource1.SelectCommand = cmdStr;
                ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rd1);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchKitNumber_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForkitNumber(string prefixText, int count)
        {
            List<string> kitNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    kitNumbers = _db.organizations
                                    .Where(o => o.KitNumber.ToString().StartsWith(prefixText))
                                    .OrderBy(o => o.OrganizationName)
                                    .Select(o => o.KitNumber.ToString())                                    
                                    .Distinct()
                                    .ToList();
                    return kitNumbers;
                }
            }
            catch (Exception ex)
            {
                OrgUnknownResults orgUnknownResults = new OrgUnknownResults();
                orgUnknownResults.HandleErrors(ex, ex.Message, "SearchForkitNumber", "", "");
                return kitNumbers;
            }
        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                orgNameSearch.Text = "";
                kitNumberSearch.Text = "";
                string cmdStr = string.Empty;

                cmdStr = string.Format("SELECT * FROM UnknownResultsForOrgView ORDER BY OrganizationName");

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