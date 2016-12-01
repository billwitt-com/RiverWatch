using System;
using System.Data.Entity.Validation;
using System.Text;

namespace RWInbound2.Reports
{
    public partial class ParticipantsPrimaryContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string cmdStr = string.Empty;
               
                //cmdStr = string.Format("SELECT * FROM [ParticipantsView] WHERE Active=1 AND PrimaryContact=1 ORDER BY FirstName");

                //SqlDataSource1.SelectCommand = cmdStr;
                //ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

                //ReportViewer1.LocalReport.DataSources.Clear();
                //ReportViewer1.LocalReport.DataSources.Add(rd1);
                //ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearch_Click", "", "");
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