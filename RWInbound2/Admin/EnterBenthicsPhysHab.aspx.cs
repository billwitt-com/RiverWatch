using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.ModelBinding;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Admin
{
    public partial class EnterBenthicsPhysHab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMessages();
                SetStationData();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }
        
        private void SetStationData(bool showPanel = false, string stationNumber = "", string stationName = "")
        {
            StationDataPanel.Visible = showPanel;
            lblStationNumber.Text = stationNumber;
            lblStationName.Text = stationName;
        }

        private void SetMessages(string type = "", string message = "")
        {
            switch (type)
            {
                case "Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    ((Label)Page.Master.FindControl("lblError")).Text = "";
                    break;
                case "Error":
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    ((Label)Page.Master.FindControl("lblError")).Text = "";
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    ((Label)Page.Master.FindControl("lblError")).Text = "";
                    break;
            }
        }

        public IQueryable<Sample> GetSamples([QueryString]string stationIDSelected = "",
                                             [QueryString]string sampleNumSelected = "",
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

                if (string.IsNullOrEmpty(stationIDSelected) && string.IsNullOrEmpty(sampleNumSelected))
                {
                    SetStationData();
                    return null;
                }

                int stationID = 0;

                if (!string.IsNullOrEmpty(sampleNumSelected))
                {
                    stationID = _db.Samples
                                   .Where(s => s.SampleNumber.Equals(sampleNumSelected))
                                   .Select(s => s.StationID)
                                   .FirstOrDefault();
                }
                else
                {
                    bool stationIDIsInt = Int32.TryParse(stationIDSelected, out stationID);
                }


                if (stationID == 0)
                {
                    SetStationData();
                    return null;
                }

                StationData stationData = (from s in _db.Stations
                                           where s.ID == stationID
                                           select new StationData
                                           {
                                               StationNumber = s.StationNumber.ToString(),
                                               StationName = s.StationName
                                           }).FirstOrDefault();

                SetStationData(true, stationData.StationNumber, stationData.StationName);

                if (!string.IsNullOrEmpty(sampleNumSelected))
                {
                    return _db.Samples
                               .Where(s => s.SampleNumber == sampleNumSelected && s.Valid == true)
                               .OrderBy(s => s.SampleNumber);
                }
                    //if (string.IsNullOrEmpty(stationIDSelected) && string.IsNullOrEmpty(sampleNumSelected))

                IQueryable<Sample> samples = _db.Samples
                                            .Where(s => s.StationID == stationID && s.Valid == true)
                                            .OrderBy(s => s.SampleNumber);               

                // remove
                this.Request.QueryString.Remove("successLabelMessage");
                if (string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", "");
                }
                //this.Request.QueryString.Clear();

                return samples;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetSamples", "", "");
                return null;
            }
        }

        protected void btnSearchStationNumber_Click(object sender, EventArgs e)
        {
            try
            {
                sampleNumberSearch.Text = "";

                int stationNumber = 0;
                bool stationNumberIsInt
                        = Int32.TryParse(stationNumberSearch.Text.Trim(), out stationNumber);
                if (stationNumberIsInt)
                {
                    int stationIDSelected = 0;

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        stationIDSelected = _db.Stations
                                        .Where(s => s.StationNumber == stationNumber)
                                        .Select(s => s.ID)
                                        .FirstOrDefault();
                    }

                    if (stationIDSelected > 0)
                    {
                        string redirect = "EnterBenthicsPhysHab.aspx?stationIDSelected=" + stationIDSelected;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchStationNumber_Click", "", "");
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchForStationNumber(string prefixText, int count)
        {
            List<string> stationNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    stationNumbers = _db.Stations
                                    .Where(s => s.StationNumber.ToString().StartsWith(prefixText))
                                    .OrderBy(s => s.StationNumber.ToString())
                                    .Select(s => s.StationNumber.ToString())
                                    .Distinct()
                                    .ToList();
                    return stationNumbers;
                }
            }
            catch (Exception ex)
            {
                EnterBenthicsPhysHab enterBenthicsPhysHab = new EnterBenthicsPhysHab();
                enterBenthicsPhysHab.HandleErrors(ex, ex.Message, "SearchForStationNumber", "", "");
                return stationNumbers;
            }
        }

        protected void btnSearchSampleNumber_Click(object sender, EventArgs e)
        {
            try
            {
                stationNumberSearch.Text = "";
               
                if (!string.IsNullOrEmpty(sampleNumberSearch.Text.Trim()))
                {
                    string sampleNumSelected = string.Empty;

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        sampleNumSelected = _db.Samples
                                        .Where(s => s.SampleNumber == sampleNumberSearch.Text.Trim())
                                        .Select(s => s.SampleNumber)
                                        .FirstOrDefault();
                    }

                    if(!string.IsNullOrEmpty(sampleNumSelected))
                    {
                        string redirect = "EnterBenthicsPhysHab.aspx?sampleNumSelected=" + sampleNumSelected;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchSampleNumber_Click", "", "");
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchForSampleNumber(string prefixText, int count)
        {
            List<string> sampleNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    sampleNumbers = _db.Samples
                                    .Where(s => s.SampleNumber.ToString().StartsWith(prefixText))
                                    .OrderBy(s => s.SampleNumber.ToString())
                                    .Select(s => s.SampleNumber.ToString())
                                    .Distinct()
                                    .ToList();
                    return sampleNumbers;
                }
            }
            catch (Exception ex)
            {
                EnterBenthicsPhysHab enterBenthicsPhysHab = new EnterBenthicsPhysHab();
                enterBenthicsPhysHab.HandleErrors(ex, ex.Message, "SearchForSampleNumber", "", "");
                return sampleNumbers;
            }
        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                stationNumberSearch.Text = "";
                stationNumberSearch.Text = "";

                string redirect = "EnterBenthicsPhysHab.aspx";

                Response.Redirect(redirect, false);

            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        private class StationData
        {
            public string StationNumber { get; set; }
            public string StationName { get; set; }
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