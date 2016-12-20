using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Providers.Entities;
// XXXX added code to turn off button create if the incoming field data has an existing sample number.
// in that case, we just populate and do not make a new sample 

namespace RWInbound2.Samples
{
    public partial class NewSamples2 : System.Web.UI.Page
    {
        //dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2();
        // NewRiverwatchEntities NRWDE = new NewRiverwatchEntities();
        RiverWatchEntities NRWDE = new RiverWatchEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime currentYear;
            DateTime thisyear = DateTime.Now;
            string date2parse;
            bool allowed = false;

            allowed = App_Code.Permissions.Test(Page.ToString(), "PAGE");
            if (!allowed)
                Response.Redirect("~/index.aspx");

            if (!IsPostBack)
            {
                Panel1.Visible = false;
                lstSamples.Visible = false;
                Session["NEWSAMPLE"] = null;
                Session["INBOUNDSAMPLEID"] = null; 
                TabContainer1.Visible = false;
                tbAnalyzeDate.Text = DateTime.Now.ToShortDateString();
                lblLogDate.Text = DateTime.Now.ToShortDateString();
                btnCreate.BackColor = System.Drawing.Color.Blue;

                // fill radio button list from tlkMetalBarCodeType
                // the order should be better, most used first id normal and then filtered
                // hand coded 06/30 bwitt

                List<string> types = new List<string>();
                types.Add("00 Normal-NonFiltered");
                types.Add("04 Normal-Filtered");
                types.Add("03 Normal-NonFilteredOnly");
                types.Add("01 Normal-FilteredOnly");
                types.Add("10 Blank-NonFiltered");
                types.Add("14 Blank-Filtered");
                types.Add("13 Blank-NonFilteredOnly");
                types.Add("11 Blank-FilteredOnly");
                types.Add("20 Duplicate-NonFiltered");
                types.Add("24 Duplicate-Filtered");
                types.Add("23 Duplicate-NonFilteredOnly");
                types.Add("21 Duplicate-FilteredOnly");

                rbListSampleTypes.DataSource = types;
                rbListSampleTypes.DataBind();

                // build the current year value, from Barb, ie the year ending on June 30th.
                // XXXX may not want to do this
                if (thisyear.Month >= 7) // we are in new current year, so don't adjust the year date
                {
                    date2parse = string.Format("{0}/07/01", thisyear.Year);
                    currentYear = DateTime.Parse(date2parse);
                }
                else
                {
                    date2parse = string.Format("{0}/07/01", thisyear.Year - 1);
                    currentYear = DateTime.Parse(date2parse);
                }

                // XXXX but will do this:
                date2parse = string.Format("{0}/07/01", thisyear.Year);
                currentYear = DateTime.Parse(date2parse);

                Session["CURRENTYEAR"] = currentYear;

                // populate year list drop down, we won't select from samples table since we don't really know what samples are used yet
                // go back 20 years to this year

                List<int> yrsList = new List<int>();
                int yrs = DateTime.Now.Year;
                for (int x = 0; x < 60; x++)
                {
                    yrsList.Add(yrs--);
                }
                ddlYears.DataSource = yrsList;
                ddlYears.DataBind();
                ddlYears.ToolTip = "Year starting YYYY/07/01 and ending YYYY+1/06/30 and referred to as status year YYYY";
                hideTabs(); // so user can't choose something that is not real
            }
        }

        // user has chosen a site number, so get detail
        // this just populates the top section of the page, above the tabs
        // 06/24 Added org select list too... 
        // 07/25 added status year ddl 
        // 07/27 add org status update - create new table entry if there is none, and let user fill out.

        protected void btnSiteNumber_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int LocaLkitNumber = 0;
            string NumberSamplePrefix = "";
            DateTime currentYear;
            DateTime thisyear = DateTime.Now;
            string orgName = "";
            int orgID = 0; 
            int stnID = 0;
            bool active = false;
            DateTime? startDate = null;
            DateTime? endDate = null;
            bool noCurrentStatus = false; 

            // moved current year calc to page_load 

            LocaLkitNumber = -1; // no real kit number yet
            bool success = int.TryParse(tbSite.Text, out stationNumber);
            bool success2 = int.TryParse(tbKitNumber.Text, out LocaLkitNumber);

            orgName = tbOrg.Text;   // this may be empty if kit number is entered

            if (!success)   // no valid station number, empty all text boxes and return
            {
                tbSite.Text = "";
                tbKitNumber.Text = "";
                tbOrg.Text = "";
                Panel1.Visible = false;
                lstSamples.Visible = false;
                return;
            }
            if (!success2)  // no or bad kit number in box
            {
                try
                {
                    if (orgName.Length > 2)   // there is an org name
                    {
                        var KN = (from k in NRWDE.organizations
                                  where k.OrganizationName == orgName
                                  select k.KitNumber).FirstOrDefault();

                        if (KN != null)
                        {
                            LocaLkitNumber = KN.Value;  // we now have good kit number and org name
                        }
                    }
                }

                catch (Exception ex)
                {
                    Panel1.Visible = false; // clean up and then report error
                    lstSamples.Visible = false;

                    string nam = "";
                    if (User.Identity.Name.Length < 3)
                        nam = "Not logged in";
                    else
                        nam = User.Identity.Name;
                    string msg = ex.Message;
                    LogError LE = new LogError();
                    LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                }
            }

            if (LocaLkitNumber == -1)
            {
                tbSite.Text = "";
                tbKitNumber.Text = "";
                tbOrg.Text = "";
                Panel1.Visible = false;
                lstSamples.Visible = false;
                return;
            }

            // we have good kit number, so fill in the text box, just for....

            tbKitNumber.Text = LocaLkitNumber.ToString();

            try
            {
                // is there a status for this org for this year...
                if (Session["CURRENTYEAR"] == null)
                    Response.Redirect("~/timedout.aspx");

                currentYear = (DateTime)Session["CURRENTYEAR"];

                var os = from s in NRWDE.OrgStatus
                         join og in NRWDE.organizations on s.OrganizationID equals og.ID //LocaLkitNumber equals og.KitNumber
                         where s.ContractStartDate.Value.Year == currentYear.Year
                         select s;

                if (os.Count() == 0) // there is no current status for this org
                {
                    noCurrentStatus = true; // will use this later for org tab
                    LblStartDate.Text = string.Format("No OrgStatus for year {0}", currentYear.Year);
                    lblEndDate.Text = string.Format("No OrgStatus for year {0}", currentYear.Year);
                }

                // need to be careful here as there may not be any org status for this org. 
                var O = (from s in NRWDE.OrgStatus
                        join og in NRWDE.organizations on s.OrganizationID equals og.ID
                        where LocaLkitNumber == og.KitNumber
                        select new
                            {
                                orgName = og.OrganizationName,
                                active = og.Active,
                                orgID = og.ID,
                                sDate = s.ContractStartDate,
                                eDate = s.ContractEndDate
                            }).OrderByDescending  (s => s.eDate) ;
                if(O.Count() != 0)  // has org status
                {
                    orgName = O.FirstOrDefault().orgName;
                    orgID = O.FirstOrDefault().orgID;
                    active = O.FirstOrDefault().active;
                    startDate = O.FirstOrDefault().sDate;
                    endDate = O.FirstOrDefault().eDate;
                }
                else
                {
                    var OO = from og in NRWDE.organizations                            
                            where LocaLkitNumber == og.KitNumber
                            select new
                            {
                                orgName = og.OrganizationName,
                                active = og.Active,
                                orgID = og.ID,
                            };
                    if(OO.Count() != 0)
                    {
                        orgName = OO.FirstOrDefault().orgName;
                        orgID = OO.FirstOrDefault().orgID;
                        active = OO.FirstOrDefault().active; 
                    }
                }

                // get station detail
                var RE = from r in NRWDE.Stations
                         where r.StationNumber == stationNumber
                         select new
                         {
                             stnName = r.StationName,
                             riverName = r.River,
                             watershed = r.RWWaterShed,
                             stnID = r.ID,
                         };
                if (RE.Count() == 0)    // grave error, blow out of here... 
                {
                    Panel1.Visible = false;
                    lstSamples.Visible = false;
                    tbSite.Text = "";
                    tbKitNumber.Text = "";
                    ddlInboundSamplePick.Items.Clear();
                    txtDateCollected.Text = "";
                    tbAnalyzeDate.Text = "";
                    txtSmpNum.Text = "";
                    txtNumSmp.Text = "";
                    return;
                }
                stnID = RE.FirstOrDefault().stnID;
                tbOrg.Text = orgName;  // orgName;   // to make it 'nice'
                Panel1.Visible = true;

                lblStationNumber.Text = string.Format("Station Number: {0}", stationNumber);
                if (startDate != null)
                    LblStartDate.Text = string.Format("Start Date: {0:M/d/yyyy}", startDate);
                if (endDate != null)
                    lblEndDate.Text = string.Format("End Date: {0:M/d/yyyy}", endDate);

                lblOrganization.Text = string.Format("Organization: {0}", orgName);
                lblRiver.Text = string.Format("River: {0}", RE.FirstOrDefault().riverName);
                lblBlankForNow.Text = string.Format("Active: {0}", active.ToString());
                lblWatershed.Text = string.Format("Watershed: {0}", RE.FirstOrDefault().watershed);
                lblStationDescription.Text = string.Format("Description: {0}", RE.FirstOrDefault().stnName);
                TabContainer1.Visible = true;
                // save some values for later use
                Session["STNID"] = stnID;
                Session["ORGID"] = orgID;
                Session["STATIONNUMBER"] = stationNumber;
                //  }
                //else   // there is status, so fill in the status parts
                //{   // we have valid org status, so fill in the blanks
                //    // where s.ContractSignedDate.Value.Year == currentYear.Year 
                //    // this query fails at times, and I am not sure why, so I will break it up into two 

                //    var RES = 
                //              from R in NRWDE.organizations // on LocaLkitNumber equals o.KitNumber // s.OrganizationID equals o.OrganizationID
                //              join ts in NRWDE.OrgStatus on R.ID equals ts.OrganizationID
                //              where R.ID == 
                //              select new
                //              {
                //                  stnName = r.StationName,
                //                  orgName = o.OrganizationName,
                //                  riverName = r.River,
                //                  startDate = ts.ContractStartDate,
                //                  endDate = ts.ContractEndDate,
                //                  active = o.Active,
                //                  watershed = r.RWWaterShed,
                //                  stnID = r.ID,
                //                  orgID = o.ID
                //              };
                //    if (RES.Count() == 0)
                //    {
                //        Panel1.Visible = false;
                //        lstSamples.Visible = false;
                //        tbSite.Text = "";
                //        tbKitNumber.Text = "";
                //        ddlInboundSamplePick.Items.Clear();
                //        txtDateCollected.Text = "";
                //        tbAnalyzeDate.Text = "";
                //        txtSmpNum.Text = "";
                //        txtNumSmp.Text = "";
                //        return;
                //    }

                //tbOrg.Text = orgName; // orgName;   // to make it 'nice'
                //Panel1.Visible = true;
                //lblStationNumber.Text = string.Format("Station Number: {0}", stationNumber);
                //lblEndDate.Text = string.Format("End Date: {0:M/d/yyyy}", RES.FirstOrDefault().endDate);
                //LblStartDate.Text = string.Format("Start Date: {0:M/d/yyyy}", RES.FirstOrDefault().startDate);
                //lblOrganization.Text = string.Format("Organization: {0}", RES.FirstOrDefault().orgName);
                //lblRiver.Text = string.Format("River: {0}", RES.FirstOrDefault().riverName);

                //lblBlankForNow.Text = string.Format("Active: {0}", RES.FirstOrDefault().active.ToString());
                //lblWatershed.Text = string.Format("Watershed: {0}", RES.FirstOrDefault().watershed);
                //lblStationDescription.Text = string.Format("Description: {0}", RES.FirstOrDefault().stnName);
                //TabContainer1.Visible = true;
                //// save some values for later use
                //Session["STNID"] = RES.FirstOrDefault().stnID;
                //Session["ORGID"] = RES.FirstOrDefault().orgID;
                //Session["STATIONNUMBER"] = stationNumber;

            }
            catch (Exception ex)
            {
                Panel1.Visible = false; // clean up and then report error
                lstSamples.Visible = false;

                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            // now populate list box of samples 
            NumberSamplePrefix = string.Format("{0}", stationNumber);   // removed period and put it in the method
            List<string> samps = new List<string>();
            lstSamples.Visible = true;

            // populate the ddl for status year, now that we know 
            // XXXX calling this from ddlYear choice also
            // current year is created one time in page load

            currentYear = (DateTime)Session["CURRENTYEAR"];
            populateSamplesList(NumberSamplePrefix, currentYear); // populate ddl on right side of samples page      

            if (Session["ORGID"] == null)
                Response.Redirect("~/timedout.aspx");
            int locorgid = (int)Session["ORGID"];
            DateTime locstatusdate = (DateTime)Session["CURRENTYEAR"];
            populateOrgStatus(locorgid, locstatusdate); 
            populateInboundSamplesList(LocaLkitNumber, stationNumber);

        }

        public void populateInboundSamplesList(int LocaLkitNum, int stationNumber)
        {
            // get drop down list of inbound samples that have this station and kit numbers 
            // added valid bit 09/11
            try
            {
                var query = from i in NRWDE.InboundSamples
                            where i.KitNum == LocaLkitNum & i.StationNum == stationNumber & i.Valid == true
                            select
                            i.SampleID;

                if (query.Count() > 0)
                {
                    List<string> ls = new List<string>();
                    ls.Add("Choose from below");
                    string tmps = "";
                    foreach (string val in query)
                    {
                        if (val != null)    // real value to consider
                        {

                            var q = from s in NRWDE.Samples
                                    where s.SampleNumber == val
                                    select s.NumberSample;

                            if (q.Count() < 1)  // not found in samples table
                            {
                                tmps = val; 
                                ls.Add(tmps);
                            }
                        }
                    }

                    if (ls.Count == 1)   // there are no 'real' entries, so notify user
                    {
                        ls.Clear();
                        ls.Add("No incoming samples");
                    }
                    ddlInboundSamplePick.Items.Clear();
                    ddlInboundSamplePick.DataSource = ls;
                    ddlInboundSamplePick.DataBind();
                }
            }
            catch (Exception ex)
            {
                Panel1.Visible = false;
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        // utility to update org status page
        // will create new entry if one does not exist for this status year
        private void populateOrgStatus(int orgID, DateTime statusYear)
        {
            // save the page number so we can get back here...
            int page = FormView1.PageIndex;
            // first, check to see if there is a status for this year
            DateTime locStatusYear = statusYear;    // will be like 201X/07/01 which is start date of status year 
            int locOrgID = orgID;
            int newStatusID = 0;
            DataSourceSelectArguments ARGS = new DataSourceSelectArguments();

            // see if there is a status entry for this year..
            // just compare years

            var QQ = from q in NRWDE.OrgStatus
                     where q.ContractStartDate.Value.Year == locStatusYear.Year & q.OrganizationID == locOrgID
                     select q;

            if (QQ.Count() == 0) // there are no entries
            {
                OrgStatu OS = new OrgStatu();   // not sure why this name lacks trailing 'S' XXXX 

                OS.OrganizationID = locOrgID;
                OS.ContractStartDate = DateTime.Now.AddYears(-50); // make not believeable    //locStatusYear;
                OS.NoteComment = "Created by Samples Update in code";
                OS.DateCreated = DateTime.Now;
                // load bools that cause issue with formview
                OS.ContractSigned = false;
                OS.SiteVisited = false;
                OS.BugCollected = false;
                OS.VolunteerTimeSheet1 = false;
                OS.VolunteerTimeShee2 = false;
                OS.VolunteerTimeSheet3 = false;
                OS.VolunteerTimeSheet4 = false;
                OS.DataEnteredElectronically1 = false;
                OS.DataEnteredElectronically2 = false;
                OS.DataEnteredElectronically3 = false;
                OS.DataEnteredElectronically4 = false;
                OS.SampleShipped1 = false;
                OS.SampleShipped2 = false;
                OS.SampleShipped3 = false;
                OS.SampleShipped4 = false;
                OS.Nutrient1Collected = false;
                OS.Nutrient2Collected = false;
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                OS.UserCreated = nam;

                NRWDE.OrgStatus.Add(OS);
                NRWDE.SaveChanges();
                newStatusID = OS.ID; // capture new id we just created
            }
            else
            {
                newStatusID = QQ.FirstOrDefault().ID;
            }

            Session["NEWSTATUSID"] = newStatusID;
            // now, set up query for org status tab

            SqlDataSourceOrgStatus.SelectCommand = string.Format("SELECT * FROM [OrgStatus] where ID = {0}", newStatusID);
            SqlDataSourceOrgStatus.Select(ARGS);
            //   FormView1.PageIndex = page; // restore page
        }

        // common utility to update values on the samples page
        private void updateSamplesPage(string EventNumber)
        {
            //Sample TS;
      //      string tstr = stationSample + "."; // we need the period so 51 is not the same as 512, etc.
            string tstr = EventNumber;  // + "."; // we need the period so 51 is not the same as 512, etc.
            try
            {
                //TS = (Sample)(from r in NRWDE.Samples
                //              where r.NumberSample.StartsWith(tstr) & r.Valid == true
                //              select r).FirstOrDefault();   // get most recent

                var TS = (from r in NRWDE.Samples
                              where r.NumberSample == tstr & r.Valid == true
                              select r).FirstOrDefault();   // get most recent

                // populate the screen 
                txtSmpNum.Text = TS.SampleNumber;
                txtNumSmp.Text = TS.NumberSample;
                txtDateCollected.Text = TS.DateCollected.ToShortDateString();
                // calc time collected, which is not in table... What a deal
                // it is in last 4 digits of samplenumber

                string sampHours = TS.SampleNumber.Substring(TS.SampleNumber.Length - 4, 2); // get first 2 chars of last 4 chars
                string sampMins = TS.SampleNumber.Substring(TS.SampleNumber.Length - 2);
                txtTimeCollected.Text = string.Format("{0:D2}:{1:D2}", sampHours, sampMins);

                chkCOC.Checked = TS.ChainOfCustody;
                chkDataSheet.Checked = TS.DataSheetIncluded;
                chkBugs.Checked = TS.Bug;
                chkBlkMtls.Checked = TS.BlankMetals ?? false;
                chkBugQA.Checked = TS.BugsQA ?? false;
                chkCS.Checked = TS.ChlorideSulfate ?? false;
                chkCSDupe.Checked = TS.DuplicatedCS ?? false;
                chkDupeMtls.Checked = TS.DuplicatedMetals ?? false;
                chkMDSR.Checked = TS.MissingDataSheetReceived;
                chkNoMtls.Checked = TS.NoMetals;
                chkNoNut.Checked = TS.NoNutrient;
                chkNP.Checked = TS.NitratePhosphorus;
                chkNPDupe.Checked = TS.DuplicatedNP;
                chkPhysHab.Checked = TS.PhysicalHabitat;
                chkTSS.Checked = TS.TotalSuspendedSolids;
                chkTSSDupe.Checked = TS.DuplicatedTSS;
                if (TS.MissingDataSheetReqDate == null)
                {
                    txtMDSR.Text = "";
                }
                else
                {
                    txtMDSR.Text = TS.MissingDataSheetReqDate.Value.ToShortDateString();
                }
                txtComment.Text = TS.Comment;
            }
            catch (Exception ex)
            {
                Panel1.Visible = false;
                string nam;
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            FillTabPanelICPdata(EventNumber);
            FillTabPanelBarcode(EventNumber);

            // clean up barcode page - and nutrient page too XXXX
            lblBarcodeUsed.Text = "";
            lblCodeInUse.Text = "";
            tbBarcode.Text = "";
        }

        // user has chosen a current sample
        // need to get year of this sample
        protected void lstSamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            string locStnNumber;
            int index = 0;
            locStnNumber = lstSamples.SelectedItem.Value;
            // select out the station event which is all digits to the left of the colon

            index = locStnNumber.IndexOf(":");
            locStnNumber = locStnNumber.Substring(0, index); // all to left of colon
            locStnNumber = locStnNumber.Trim();             // remove any spaces, etc.
            updateSamplesPage(locStnNumber);

            if (Session["ORGID"] == null)
                Response.Redirect("~/timedout.aspx");
            int locorgid = (int)Session["ORGID"];

            if(Session["CURRENTYEAR"] == null)
            {
                Response.Redirect("~/timedout.aspx");
            }
            DateTime locstatusdate = (DateTime)Session["CURRENTYEAR"];

            populateOrgStatus(locorgid, locstatusdate); 
            showTabs();
            // update org status tab too
        }

        // save or update - but we never overwrite so we always create with a valid flag 
        // decided not to clear page upon save as there may be more to do... 
        // added inboundsample code to invalidate inbound sample if it was used to populate sample
        protected void btnSaveSample_Click(object sender, EventArgs e)
        {
            Sample orgTS;
            int stationNumber = 0;
            int stnID = 0;
            int orgID = 0;
            int orgNumber = 0;
            int kitNumber = 0;
            string SampleNumber = txtSmpNum.Text.Trim();
            NEWexpWater NEW = null ; 
            bool existingRecord = false; 

            // make sure we have good basic input, like sample number, etc.  
            lblWarning.Visible = false;

            if ((txtSmpNum.Text == "") | (txtSmpNum.Text.Length < 2))
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Please make sure there is valid sample detail";
                lblWarning.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if ((txtNumSmp.Text == "") | (txtNumSmp.Text.Length < 2))
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Please make sure there is valid sample detail";
                lblWarning.ForeColor = System.Drawing.Color.Red;
                return;
            }

            bool success = int.TryParse(tbSite.Text, out stationNumber);

            bool success2 = int.TryParse(tbKitNumber.Text, out kitNumber);

            if (!success | !success2)   // bad input, go back 
            {
                tbSite.Text = "";
                tbKitNumber.Text = "";
                Panel1.Visible = false;
                lstSamples.Visible = false;
                return;
            }

            // get our saved values from most recent query
            if (Session["STNID"] != null)
            {
                stnID = (int)Session["STNID"];
                orgID = (int)Session["ORGID"];
            }
            else
            {
                Response.Redirect("timedout.aspx"); // send user somewhere valid to restart work
            }

            string sampHours = txtTimeCollected.Text.Substring(0, 2);
            string sampMins = txtTimeCollected.Text.Substring(3, 2);
            double hrs = double.Parse(sampHours);
            double mins = double.Parse(sampMins);

            // scrape the screen
            // ADD DATES, ETC WHEN WE HAVE newWater TABLE XXXX
            Sample TS = new Sample();
            TS.StationID = stationNumber;
            TS.OrganizationID = orgNumber;
            TS.DateCollected = DateTime.Parse(txtDateCollected.Text);
            TS.TimeCollected = TS.DateCollected.AddHours(hrs).AddMinutes(mins);
            TS.SampleNumber = txtSmpNum.Text.Trim();
            TS.NumberSample = txtNumSmp.Text.Trim();
            TS.ChainOfCustody = chkCOC.Checked;
            TS.DataSheetIncluded = chkDataSheet.Checked;
            TS.Bug = chkBugs.Checked;
            TS.BlankMetals = chkBlkMtls.Checked;
            TS.BugsQA = chkBugQA.Checked;
            TS.ChlorideSulfate = chkCS.Checked;
            TS.DuplicatedCS = chkCSDupe.Checked;
            TS.DuplicatedMetals = chkDupeMtls.Checked;
            TS.MissingDataSheetReceived = chkMDSR.Checked;
            TS.NoMetals = chkNoMtls.Checked;
            TS.NoNutrient = chkNoNut.Checked;
            TS.NitratePhosphorus = chkNP.Checked;
            TS.DuplicatedNP = chkNPDupe.Checked;
            TS.PhysicalHabitat = chkPhysHab.Checked;
            TS.TotalSuspendedSolids = chkTSS.Checked;
            TS.DuplicatedTSS = chkTSSDupe.Checked;

            if (txtMDSR.Text.Length < 5)    // no date here... 
            {
                TS.MissingDataSheetReqDate = null;
            }
            else
            {
                TS.MissingDataSheetReqDate = DateTime.Parse(txtMDSR.Text);
            }

            TS.Comment = txtComment.Text;

            // now decide if this is an update or is new, and update accordingly 
            try
            {
                // if there is an old record, update old record by setting valid = false
                orgTS = (from s in NRWDE.Samples
                         where s.SampleNumber == txtSmpNum.Text & s.Valid == true
                         select s).FirstOrDefault();

                if (orgTS != null)
                {
                    orgTS.Valid = false;
                    orgTS.UserCreated = User.Identity.Name;
                    orgTS.DateCreated = DateTime.Now;
                    NRWDE.SaveChanges(); // update all records
                }

                // add new record 
                TS.StationID = stnID;
                TS.OrganizationID = orgID;
                TS.Valid = true;
                TS.UserCreated = User.Identity.Name;
                TS.DateCreated = DateTime.Now;
                TS.DateCollected = DateTime.Parse(txtDateCollected.Text.Trim());
                TS.Comment = txtComment.Text;

                // here we save sample, get the new identity and save that value in sampleID in case we want to use it later... 
                NRWDE.Samples.Add(TS);
                NRWDE.SaveChanges(); // update 
                TS.SampleID = TS.ID;    // update old sampleID column, which is not an identity column, but we may want to use it...  just in case
                NRWDE.SaveChanges(); // update 

                // clear fields for next sample
                // we decided no to clear bwitt 09/11
                //tbSite.Text = "";
                //tbKitNumber.Text = "";
                //tbOrg.Text = "";
                //Panel1.Visible = false;
                //lstSamples.Visible = false;
              // XXXX why  return;
            }
            catch (Exception ex)
            {
                Panel1.Visible = false;
                string nam;
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            // XXXX added 11/13/ bwitt to update newExpWater table with either a new record or an update to existing record. 

            try
            {
                //NEWexpWater TEST = (from t in NewRWE.NEWexpWaters
                //                    where t.tblSampleID == sID & t.Valid == true & t.MetalsBarCode == barCode
                //                    select t).FirstOrDefault();

                NEWexpWater TEST = (from t in NRWDE.NEWexpWaters
                                    where t.Valid == true & t.SampleNumber == SampleNumber
                                    select t).FirstOrDefault();
                if (TEST != null)
                {
                    // skip these as they are not our business to insert into a row that already exists
                    // items like kit number, etc. will be here already as a result of inserting field or nutrient data earlier
                    NEW = TEST; // keep the name common to this method
                    existingRecord = true; // flag for later
                }
                else
                {
                    NEW = new NEWexpWater(); // create new entity as there is not one yet
                    existingRecord = false; // to make sure
                }

                // no existing record, so we are first
                if (!existingRecord)
                {
                    // not sure why we are setting all the nulls... 
                    NEW.BadDuplicate = false;
                    NEW.BadSample = false;
                    NEW.BenthicsComments = null;
                    NEW.BugsBarCode = null;
                    NEW.BugsComments = null;
                    NEW.Chloride = null;
                    NEW.ChlorophyllA = null;
                    NEW.CreateDate = DateTime.Now;
                    NEW.CreatedBy = User.Identity.Name;                  
                    
                    NEW.DO_MGL = null;
                    NEW.DOC = null;
                    NEW.DOSAT = null;
                    NEW.FieldBarCode = null;
                    NEW.FieldComment = null;

                    NEW.TypeCode = null;
                    NEW.MetalsComment = null;
                    NEW.MetalsBarCode = null;

                    NEW.tblSampleID = TS.ID;        // FK to tblSample
                    NEW.KitNumber = kitNumber;
                    NEW.Event = TS.NumberSample;    // string like above, 10.095    
                    NEW.SampleNumber = TS.SampleNumber; // this is the big string of station id + date time - build at sample entry

                    NEW.NutrientBarCode = null;
                    NEW.NutrientComment = null;
                    NEW.OP = null;

                    NEW.orgN = null;
                    NEW.PH = null;
                    NEW.PHEN_ALK = null;
                    NEW.SampleDate = TS.DateCollected; // ts.DateCollected; // this is date part only, no time and may be junk XXXX
                    if (TS.TimeCollected.Value.Year > 1970)  // likely a real value - otherwise, leave blank
                    {
                        NEW.SampleDate.Value.AddHours(TS.TimeCollected.Value.Hour); // add in pieces
                        NEW.SampleDate.Value.AddMinutes(TS.TimeCollected.Value.Minute);
                    }

                    NEW.StationNumber = (short)stationNumber;
                    var STN = (from s in NRWDE.Stations
                               where s.ID == TS.StationID
                               select s).FirstOrDefault();

                    NEW.StationName = STN.StationName;

                    NEW.RiverName = STN.River;
                    NEW.WaterShed = STN.RWWaterShed; 

                    var ORG = (from o in NRWDE.organizations
                               where o.ID == orgID
                               select o).FirstOrDefault();

                    NEW.OrganizationName = ORG.OrganizationName;
                    NEW.OrganizationID = orgID;

                    // zero out filed data 
                    NEW.Sulfate = null;
                    NEW.TempC = null;
                    NEW.TKN = null;
                    NEW.TOTAL_ALK = null;
                    NEW.TOTAL_HARD = null;
                    NEW.BadBlank = false;
                    NEW.Valid = true;
                }
                else    // we have existing newEXPwater record, so update with new sample data 
                {
                    NEW.CreateDate = DateTime.Now;
                    NEW.CreatedBy = User.Identity.Name;
                    NEW.tblSampleID = TS.ID;        // FK to tblSample
                    NEW.KitNumber = kitNumber;
                    NEW.SampleNumber = TS.SampleNumber; 
                    NEW.Event = TS.NumberSample;    
                    NEW.OrganizationID = orgID;
                    NEW.StationNumber = (short)stationNumber;     

                }

                if(existingRecord == true)
                {
                    NRWDE.SaveChanges();
                }
                else
                {
                    NRWDE.NEWexpWaters.Add(NEW);
                    NRWDE.SaveChanges();
                }
            }


            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            if (Session["CURRENTYEAR"] != null)
            {
                DateTime currentYear = (DateTime)Session["CURRENTYEAR"];
                string NumberSamplePrefix = txtNumSmp.Text.Trim();

                int index = NumberSamplePrefix.IndexOf(".");
                if (index > 0)
                {
                    NumberSamplePrefix = NumberSamplePrefix.Substring(0, index);
                }
                populateSamplesList(NumberSamplePrefix, currentYear); // populate ddl on right side of samples page   
                if (Session["ORGID"] == null)
                    Response.Redirect("~/timedout.aspx");
                int locorgid = (int)Session["ORGID"];
                DateTime locstatusdate = (DateTime)Session["CURRENTYEAR"];
                populateOrgStatus(locorgid, locstatusdate);

            }

            // check to see if this was inboundsample data, and if so, invalidate sample      
            // XXXX BW = this was a bad idea. Setting not valid keeps validation from seeing it    
            // I thought this would keep sample from being reused in drop down list
            // but in reality, the fact that it is recorded in the samples table is enough
             if(Session["INBOUNDSAMPLEID"] != null)
             {
                 int inboundSampleID = (int)Session["INBOUNDSAMPLEID"];
                 try
                 {
                     InboundSamples S = (from s in NRWDE.InboundSamples
                                        where s.ID == inboundSampleID & s.Valid == true
                                        select s).FirstOrDefault(); 
                     if(S != null)
                     {
                         //S.Valid = false;
                         //NRWDE.SaveChanges();
                         Session["INBOUNDSAMPLEID"] = null; 
                     }
                 }
                 catch (Exception ex)
                 {
                     Panel1.Visible = false;
                     string nam;
                     if (User.Identity.Name.Length < 3)
                         nam = "Not logged in";
                     else
                         nam = User.Identity.Name;
                     string msg = ex.Message;
                     LogError LE = new LogError();
                     LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
                 }

                 // incorrect we need station nummber not id
                // populateInboundSamplesList(kitNumber, stnID);
                 populateInboundSamplesList(kitNumber, stationNumber);
             }
        }

        // we are making a new, fresh sample set with new dates, etc.
        // or we have one already, and we just need to update 10/03 bwitt XXXX
        // we can now enable the other tabs
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            DateTime newdate;
            string sampnum = "";
            string timestr = "";
            int hours = 0;
            int mins = 0;
            string newEvent = "";

            // see if we have an event, if so, just show tabs
            bool success = DateTime.TryParse(txtDateCollected.Text, out newdate);
            if (!success)
            {
                txtDateCollected.Text = "";
                return;
            }

            timestr = txtTimeCollected.Text;
            if (timestr.Length < 4)
            {
                txtTimeCollected.Text = "";
                return;
            }

            success = int.TryParse(timestr.Substring(0, 2), out hours);
            if (!success)
            {
                txtTimeCollected.Text = "";
                return;
            }

            success = int.TryParse(timestr.Substring(3, 2), out mins);
            if (!success)
            {
                txtTimeCollected.Text = "";
                return;
            }

            TabContainer1.Visible = true;
            // make string for sample number

            sampnum = string.Format("{0}{1}{2:D2}{3:D2}{4:D2}{5:D2}",
                tbSite.Text,
                newdate.Year,
                newdate.Month,
                newdate.Day,
                hours,
                mins);

            txtSmpNum.Text = sampnum;

            // now build Event number (number sample in current db table)

            try
            {
                string result = (string)(from r in NRWDE.Samples
                                         where r.NumberSample.StartsWith(tbSite.Text + ".") & r.Valid == true
                                         orderby r.NumberSample descending
                                         select r.NumberSample).Take(1).FirstOrDefault();

                if (result == null) // no entry yet, first sample
                {

                    newEvent = string.Format("{0}.{1:D3}", tbSite.Text, 000);   // build 'fresh' event number
                    txtNumSmp.Text = newEvent;
                }
                else
                {
                    int idx = result.IndexOf('.');
                    string site = result.Substring(0, idx);
                    string number = result.Substring(++idx);
                    int stp = int.Parse(number) + 1;
                    newEvent = string.Format("{0}.{1:D3}", site, stp);
                    txtNumSmp.Text = newEvent;
                }

            }


            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            showTabs();
        }

        // from create icp data tab
        protected void btnCheckBarCode2Create_Click(object sender, EventArgs e)
        {

        }

        // user chose an item from the list of inboundsamples that had kit and station numbers
        // XXXX if we have time, change data type from long? to string. at sampNumber
        // XXXX we have not accepted this inbound sample yet, so hide tabs and make user
        // use the create button


        protected void ddlInboundSamplePick_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sampNumber = ddlInboundSamplePick.SelectedItem.Text;
            long sNum = long.Parse(sampNumber);
            int inBoundSampleID = 0;

            hideTabs(); // tabs not valid yet
            TabPanelSample.Enabled = false; // samples tab too... 
            TabPanelSample.Visible = false;

            try
            {
                InboundSamples TS = (InboundSamples)(from s in NRWDE.InboundSamples
                                                   where s.SampleID == sampNumber
                                                   select s).FirstOrDefault();
                if (TS == null)
                    return;
                inBoundSampleID = TS.ID;
                Session["INBOUNDSAMPLEID"] = inBoundSampleID; // save and as flag
                // populate the screen 
                // add check to see if this is already in samples table

                var S = (from s in NRWDE.Samples
                           where s.SampleNumber == sampNumber
                           select s).FirstOrDefault();

                if (S == null)
                    txtNumSmp.Text = "Unknown";
                else
                {
                    txtNumSmp.Text = S.NumberSample;
                }


                txtSmpNum.Text = TS.SampleID; 
                // txtNumSmp.Text = TS.NumberSample;
                chkCOC.Checked = TS.ChainOfCustody ?? false;
                chkDataSheet.Checked = TS.DataSheetIncluded ?? false;
                chkBugs.Checked = TS.Bugs ?? false;
                chkBlkMtls.Checked = TS.MetalsBlnk ?? false;
                chkBugQA.Checked = TS.BugsQA ?? false;
                chkCS.Checked = TS.CS ?? false;
                chkCSDupe.Checked = TS.CSDupe ?? false;
                chkDupeMtls.Checked = TS.MetalsDupe ?? false;
                chkMDSR.Checked = TS.MissingDataSheetReceived ?? false;
                chkNoMtls.Checked = false; // seems to be a missing field
                chkNoNut.Checked = false; // TS.NoNutrient;
                chkNP.Checked = TS.NP ?? false;
                chkNPDupe.Checked = TS.NPDupe ?? false;
                chkPhysHab.Checked = false; // TS.PhysicalHabitat;
                chkTSS.Checked = TS.TSS ?? false;
                chkTSSDupe.Checked = TS.TSSDupe ?? false;
                tbComments.Text = TS.Comments;
                txtDateCollected.Text = TS.Date.Value.ToShortDateString();
                // now chop up time and put in correct format hh:mm 24 hour

                string ds = TS.Time.Value.ToString("D4"); // assume 4 digits 
                if (ds.Length == 4)
                {
                    ds = ds.Insert(2, ":");
                    txtTimeCollected.Text = ds;
                }

                if (TS.MissingDataSheetReqDate == null)
                {
                    txtMDSR.Text = "";
                }
                else
                {
                    txtMDSR.Text = TS.MissingDataSheetReqDate.Value.ToShortDateString();
                }
                txtComment.Text = TS.Comments;

            }
            catch (Exception ex)
            {
                Panel1.Visible = false; // clean up and then report error
                lstSamples.Visible = false;

                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
            if (txtNumSmp.Text == "Unknown")
            {
                btnCreate.Focus();
                btnCreate.Visible = true; 
                btnCreate.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                btnCreate.Visible = false; 
            }
        }

        // fill in org stat tab
        private void updateOrg()
        {

        }
        // update data table with new results OR create new sample 
        protected void btnSaveMetals_Click(object sender, EventArgs e)
        {
            // scrape table
            string barcode = tbBarcode.Text.Trim();
            string type = rbListSampleTypes.SelectedValue; // get user choice for type
            string smpNum = "";
            bool filtered = true;
            bool normal = true;
            bool blank = true;
            bool duplicate = true;
            string typ = "";


            // check to see if barcode is in use, if so, warn user and return to page

            var query = from q in NRWDE.MetalBarCodes
                        where q.LabID.ToUpper() == barcode 
                        select q;

            if (query.Count() > 0)   // code in use
            {
                // lblCodeInUse
                lblBarcodeUsed.Text = string.Format("Barcode: {0} NOT SAVED - IN USE!", barcode);
                lblBarcodeUsed.ForeColor = System.Drawing.Color.Red;
                lblCodeInUse.Text = lblBarcodeUsed.Text; // "NOT SAVED !! CODE IN USE!";
                lblCodeInUse.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblBarcodeUsed.Text = "SAVED - CODE OK!";
                lblBarcodeUsed.ForeColor = System.Drawing.Color.Green;
                lblCodeInUse.Text = "SAVED - CODE OK!";
                lblCodeInUse.ForeColor = System.Drawing.Color.Green;
            }

            // calc the sample type from the user input Type two characters
            filtered = type.Substring(1, 1) == "1" | type.Substring(1, 1) == "4";
            normal = type.Substring(0, 1) == "0";
            blank = type.Substring(0, 1) == "1";
            duplicate = type.Substring(0, 1) == "2";

            smpNum = txtSmpNum.Text.Trim();

            if (normal)
                typ = "N";
            if (blank)
                typ = "B";
            if (duplicate)
                typ = "D";

            // get sample number from tblSample 

            int SN = (from q in NRWDE.Samples
                      where q.SampleNumber == smpNum & q.Valid == true
                      select q.SampleID).FirstOrDefault();

            MetalBarCode TMB = new MetalBarCode(); // make a new entity to fill in with data

            TMB.DateCreated = DateTime.Now;
            TMB.Filtered = filtered;
            TMB.LabID = barcode;        // XXXX review as so much of this record is redundant 
            TMB.Code = type.Substring(0, 2);
            TMB.LogDate = DateTime.Now;
            TMB.NumberSample = txtNumSmp.Text.Trim();
            TMB.SampleNumber = txtSmpNum.Text.Trim();
            TMB.Type = typ;
            TMB.UserCreated = Environment.UserName; // XXXX change when we authenicate
            TMB.ContainMetal = false; // from data base where these all seem to be false XXXX
            TMB.BoxNumber = tbBoxNumber.Text.Trim();
            TMB.Verified = cbVerified.Checked;
            TMB.SampleID = SN;
            NRWDE.MetalBarCodes.Add(TMB);
            NRWDE.SaveChanges(); // insert record 
            // update barcode grid at bottom of tab

            FillTabPanelBarcode(txtNumSmp.Text.Trim());
            FillTabPanelICPdata(txtNumSmp.Text.Trim());
        }

        // add link button 
        // removed [Riverwatch].[dbo].
        protected void FillTabPanelBarcode(string EventNumber)
        {

            string queryString = string.Format("SELECT [LabID] ,[Code] ,[Type] ,[Filtered] ,[BoxNumber] " +

                        " FROM [MetalBarCode] " +
                        " where NumberSample like '{0}'", EventNumber.Trim());


            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  //GlobalSite.RiverWatchDev;
                        cmd.CommandText = queryString;
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                GridViewBarCodes.DataSource = rdr;
                                GridViewBarCodes.DataBind();
                                GridViewBarCodes.Visible = true;
                            }
                            else
                            {
                                GridViewBarCodes.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Panel1.Visible = false; // clean up and then report error
                lstSamples.Visible = false;

                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        // protected void FillTabPanelICPdata(string station_Sample)
        protected void FillTabPanelICPdata(string EventNumber)
        {
            // quick fix:

            string sid = EventNumber.Trim(); // txtNumSmp.Text.Trim(); // scrape page
            // query for barcodes that have been entered but not analyzed
            // thus are in newEXPWater (final output) nor in inboundicpfinal and NOT IN METALBARCODES      
            // removed  [RiverWatch].[dbo].

            string queryString = "";
            queryString = string.Format(
                " SELECT LabID as [Barcode], Code as [Sample Type] " +
                " FROM  [MetalBarCode] AS a " +
                " WHERE    a.NumberSample like '{0}' " +
                " and   NOT EXISTS (SELECT * FROM [NEWexpWater] AS b " +
                " WHERE b.tblSampleID = a.ID) " +
                " and  Not Exists (Select * From [InboundICPFinal] as c " +
                " Where a.LabID  =  c.CODE) " +
                " order by SampleID desc", sid);
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                        cmd.CommandText = queryString;
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                GridView1.DataSource = rdr;
                                GridView1.DataBind();
                                GridView1.Visible = true;
                            }
                            else
                            {
                                GridView1.Visible = false;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Panel1.Visible = false; // clean up and then report error
                lstSamples.Visible = false;

                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        // user has entered a barcode on page, and now wants to see if it is unique... 
        protected void btnCheckBarcode_Click(object sender, EventArgs e)
        {
            // scrape table
            string barcode = tbBarcode.Text.Trim().ToUpper();
           // string type = rbListSampleTypes.SelectedValue; // get user choice for type

            var query = from q in NRWDE.MetalBarCodes
                        where q.LabID.ToUpper() == barcode // & q.Code == type.Substring(0, 2)
                        select q;

            if (query.Count() > 0)   // code in use
            {
                // lblCodeInUse
                lblBarcodeUsed.Text = string.Format("Barcode: {0} IN USE!", barcode);
                lblBarcodeUsed.ForeColor = System.Drawing.Color.Red;
                lblCodeInUse.Text = "";
            }
            else
            {
                lblBarcodeUsed.Text = "CODE OK!";
                lblBarcodeUsed.ForeColor = System.Drawing.Color.Green;
                lblCodeInUse.Text = "";
            }
        }

        // user wants to update incomingICP table with the checked barcodes        
        protected void btnICPSave_Click(object sender, EventArgs e)
        {
            int rows = GridView1.Rows.Count;
            GridView GV = sender as GridView;
            string barcode = "";
            string type = "";
            int tblSampleID = 0;

            for (int x = 0; x < rows; x++)
            {
                CheckBox cb = GridView1.Rows[x].FindControl("cbSelectBarcode") as CheckBox; // get the checkbox for this row
                if (cb.Checked)
                {
                    bool success = int.TryParse(GridView1.Rows[x].Cells[1].Text, out tblSampleID);

                    barcode = GridView1.Rows[x].Cells[1].Text;
                    type = GridView1.Rows[x].Cells[2].Text;
                    makeICPInbound(barcode, type);
                    GridView1.Rows[x].Cells.Clear();
                }
            }

            FillTabPanelICPdata(txtNumSmp.Text.Trim());
            FillTabPanelBarcode(txtNumSmp.Text.Trim());
        }

        /// <summary>
        /// method to scrape page and create new 'fake' record in tblInboundICP
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="type"></param>
        /// <param name="tblSampleID"></param>
        private void makeICPInbound(string barcode, string type)
        {
            DateTime? newDate;
            Random RAND = new Random();
            InboundICPFinal INB = new InboundICPFinal(); // create new data entity
            InboundICPOrigional OR = new InboundICPOrigional();

            string numberSample = txtNumSmp.Text.Trim(); // really bad name, perhaps we will have time to correct           

            decimal mult = 0.0m;
            decimal V = 0;

            if (type.Substring(0, 1) == "0")  // normal sample
            {
                mult = 100;
            }
            if (type.Substring(0, 1) == "1")  // blank
            {
                mult = .001m;
            }

            if (type.Substring(0, 1) == "2")  // duplicate 
            {
                mult = 125;
            }

            try
            {
                int SID = (from s in NRWDE.Samples                  // get sample table id 
                           where s.NumberSample == numberSample
                           select s.ID).FirstOrDefault();

                INB.CODE = barcode;  // make new barcode
                INB.DUPLICATE = type; // this is poorly named
                INB.tblSampleID = SID;

                OR.CODE = barcode;  // make new barcode
                OR.DUPLICATE = type; // this is poorly named
                OR.tblSampleID = SID;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "AL"
                              select z.Reporting.Value).FirstOrDefault();

                INB.AL_D = (decimal)RAND.NextDouble() * V * mult;
                INB.AL_T = (decimal)INB.AL_D + (decimal)RAND.NextDouble() + .5m;

                OR.AL_D = INB.AL_D;
                OR.AL_T = INB.AL_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "AS"
                              select z.Reporting.Value).FirstOrDefault();

                INB.AS_D = (decimal)RAND.NextDouble() * V * mult;
                INB.AS_T = (decimal)INB.AS_D + (decimal)RAND.NextDouble() + .5m;

                OR.AS_D = INB.AS_D;
                OR.AS_T = INB.AS_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "CA"
                              select z.Reporting.Value).FirstOrDefault();

                INB.CA_D = (decimal)RAND.NextDouble() * V * mult;
                INB.CA_T = (decimal)INB.CA_D + (decimal)RAND.NextDouble() + .5m;

                OR.CA_D = INB.CA_D;
                OR.CA_T = INB.CA_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "CD"
                              select z.Reporting.Value).FirstOrDefault();

                INB.CD_D = (decimal)RAND.NextDouble() * V * mult;
                INB.CD_T = (decimal)INB.CD_D + (decimal)RAND.NextDouble() + .5m;

                OR.CD_D = INB.CD_D;
                OR.CD_T = INB.CD_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "CU"
                              select z.Reporting.Value).FirstOrDefault();
                INB.CU_D = (decimal)RAND.NextDouble() * V * mult;
                INB.CU_T = (decimal)INB.CU_D + (decimal)RAND.NextDouble() + .5m;

                OR.CU_D = INB.CU_D;
                OR.CU_T = INB.CU_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "FE"
                              select z.Reporting.Value).FirstOrDefault();
                INB.FE_D = (decimal)RAND.NextDouble() * V * mult;
                INB.FE_T = (decimal)INB.FE_D + (decimal)RAND.NextDouble() + .5m;

                OR.FE_D = INB.FE_D;
                OR.FE_T = INB.FE_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "K"
                              select z.Reporting.Value).FirstOrDefault();
                INB.K_D = (decimal)RAND.NextDouble() * V * mult;
                INB.K_T = (decimal)INB.K_D + (decimal)RAND.NextDouble() + .5m;

                OR.K_D = INB.K_D;
                OR.K_T = INB.K_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "MG"
                              select z.Reporting.Value).FirstOrDefault();
                INB.MG_D = (decimal)RAND.NextDouble() * V * mult; ; // make Total_Dups smaller than Dissolved_Dups 
                INB.MG_T = (decimal)INB.MG_D - (decimal)RAND.NextDouble() + .5m;

                OR.MG_D = INB.MG_D;
                OR.MG_T = INB.MG_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "MN"
                              select z.Reporting.Value).FirstOrDefault();
                INB.MN_D = (decimal)RAND.NextDouble() * V * mult;
                INB.MN_T = (decimal)INB.MN_D + (decimal)RAND.NextDouble() - .5m; ;

                OR.MN_D = INB.MN_D;
                OR.MN_T = INB.MN_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "NA"
                              select z.Reporting.Value).FirstOrDefault();
                INB.NA_D = (decimal)RAND.NextDouble() * V * mult;
                INB.NA_T = (decimal)INB.NA_D + (decimal)RAND.NextDouble() + .5m;

                OR.NA_D = INB.NA_D;
                OR.NA_T = INB.NA_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "PB"
                              select z.Reporting.Value).FirstOrDefault();
                INB.PB_D = (decimal)RAND.NextDouble() * V * mult;
                INB.PB_T = (decimal)INB.PB_D + (decimal)RAND.NextDouble() + .5m;

                OR.PB_D = INB.PB_D;
                OR.PB_T = INB.PB_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "SE"
                              select z.Reporting.Value).FirstOrDefault();
                INB.SE_D = (decimal)RAND.NextDouble() * V * mult;
                INB.SE_T = (decimal)INB.SE_D - (decimal)RAND.NextDouble() + .5m;

                OR.SE_D = INB.SE_D;
                OR.SE_T = INB.SE_T;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "ZN"
                              select z.Reporting.Value).FirstOrDefault();

                INB.ZN_D = (decimal)RAND.NextDouble() * V * mult; ; // make Total_Dups smaller than Dissolved_Dups 
                INB.ZN_T = (decimal)INB.ZN_D - .5m;

                OR.ZN_D = INB.ZN_D;
                OR.ZN_T = INB.ZN_T;

                INB.Comments = "Created by hand for testing";
                OR.Comments = INB.Comments;

                INB.ANADATE = DateTime.Now;
                OR.ANADATE = INB.ANADATE;

                newDate = DateTime.Now.AddDays(-2);

                INB.DATE_SENT = newDate;
                OR.DATE_SENT = newDate;

                INB.CreatedBy = "Test System";
                OR.CreatedBy = INB.CreatedBy;

                newDate = DateTime.Now.AddDays(-6);
                INB.CreatedDate = newDate.Value;
                OR.CreatedDate = newDate.Value;

                INB.COMPLETE = true;
                OR.COMPLETE = true;

                INB.Saved = false;
                INB.Edited = false;
                INB.Valid = true;

                OR.Saved = false;
                OR.Edited = false;
                OR.Valid = true;

                NRWDE.InboundICPFinals.Add(INB);
                NRWDE.SaveChanges(); // update

                NRWDE.InboundICPOrigionals.Add(OR);
                NRWDE.SaveChanges();

                //RWDE.tblInboundICPs.Add(INB);   // no longer used
                //RWDE.SaveChanges();
            }

            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            // rebuild the data grid for icp creation 
            // XXXX why is this commented out? bw 06/30 
            // FillTabPanelICPdata(txtNumSmp.Text.Trim()); 
        }


        private void populateSamplesList(string NumberSamplePrefix, DateTime currentYear)
        {
            string tmpstr = "";
            List<string> samps = new List<string>();
            DateTime nextYear = currentYear.AddYears(1);
            // get drop down list of samples and numbers from DB, for this current year
            try
            {
                var SMP = from s in NRWDE.Samples
                          where s.NumberSample.StartsWith(NumberSamplePrefix + ".") & s.DateCollected >= currentYear & s.DateCollected <= nextYear & s.Valid == true
                          orderby s.DateCollected descending
                          select new
                          {
                              SampNumber = s.NumberSample,
                              SampDate = s.DateCollected
                          };
                if (SMP.Count() < 1)
                {
                    lstSamples.Items.Clear();
                    lstSamples.Visible = false; 
                    return;
                }

                SMP = SMP.OrderByDescending(q => q.SampNumber);
                foreach (var s in SMP)
                {
                    tmpstr = string.Format("{0} : {1:M/d/yyyy}", s.SampNumber, s.SampDate);
                    samps.Add(tmpstr); // fill in list
                }

                lstSamples.Items.Clear();
                lstSamples.Visible = true; 
                lstSamples.DataSource = samps;
                lstSamples.DataBind();
            }
            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        // check to see if there is a nutrient bar code already and print message 
        protected void btnBarcodeSearch_Click(object sender, EventArgs e)
        {
            // scrape barcode from page

            string barcode = tbNutrientCode.Text.Trim().ToUpper();
            if (barcode.Length < 3)
            {
                lblNutBarcodeMsg.Text = "Please enter a valid bar code";
                tbNutrientCode.Text = "";
                return;
            }

            try
            {
                string R = (string)(from r in NRWDE.NutrientBarCodes
                                    where r.LabID.ToUpper() == barcode
                                    select r.LabID).FirstOrDefault();
                if (R == null)   // no existing barcode
                {
                    lblNutBarcodeMsg.Text = "Unused Bar Code!";
                    lblNutBarcodeMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblNutBarcodeMsg.Text = string.Format("Barcode {0} is used", barcode);
                    lblNutBarcodeMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        // should we filter bar codes for junk chars
        // must scrape page to save as these controls are not data bound
        protected void btnSaveNutrient_Click(object sender, EventArgs e)
        {
            DateTime anaDate;
            // scrape barcode from page
            string barcode = tbNutrientCode.Text.Trim().ToUpper();  // scrape page
            if (barcode.Length < 3)
            {
                lblNutBarcodeMsg.Text = "Please enter a valid bar code";    // quick check, not sure what else we can do
                tbNutrientCode.Text = "";
                return;
            }
            string sampNum = txtSmpNum.Text.Trim();
            string numSamp = txtNumSmp.Text.Trim();
            // we presume good barcode here, so get to it... 

            NutrientBarCode NBC = new NutrientBarCode(); // create an empty data set
            NBC.LabID = barcode;

            int SN = (from q in NRWDE.Samples
                      where q.SampleNumber == sampNum & q.Valid == true
                      select q.SampleID).FirstOrDefault();

            if (SN > 0)
            {
                NBC.SampleID = SN;
            }

            // loop througth chksNutrients to see which are checked
            NBC.Ammonia = chksNutrients.Items.FindByText("Ammonia").Selected;
            NBC.ChlorA = chksNutrients.Items.FindByValue("ChlorA").Selected;
            NBC.Chloride = chksNutrients.Items.FindByValue("Chloride").Selected;
            NBC.DOC = chksNutrients.Items.FindByValue("DOC").Selected;
            NBC.NitrateNitrite = chksNutrients.Items.FindByValue("NitrateNitrite").Selected;
            NBC.OrthoPhos = chksNutrients.Items.FindByValue("OrthoPhos").Selected;
            NBC.Sulfate = chksNutrients.Items.FindByValue("Sulfate").Selected;
            NBC.TotalNitro = chksNutrients.Items.FindByValue("TotalNitro").Selected;
            NBC.TotalPhos = chksNutrients.Items.FindByValue("TotalPhos").Selected;
            NBC.TSS = chksNutrients.Items.FindByValue("TSS").Selected;

            NBC.DateCreated = DateTime.Now;
            NBC.LogDate = DateTime.Now;

            string nam = "";
            if (User.Identity.Name.Length < 3)
                nam = "Not logged in";
            else
                nam = User.Identity.Name;
            NBC.UserCreated = nam;

            bool success = DateTime.TryParse(tbAnalyzeDate.Text, out anaDate);
            if (!success)
            {
                anaDate = DateTime.Now;
            }
            NBC.AnalyzeDate = anaDate;
            NBC.SampleNumber = sampNum;

            // test to see if the code exists, if so, don't save
            try
            {
                int? R = (from r in NRWDE.NutrientBarCodes
                          where r.LabID.ToUpper() == barcode
                          select r.ID).FirstOrDefault();

                if (R <= 0)  // no existing bar code but will be 0 if no results 
                {
                    NRWDE.NutrientBarCodes.Add(NBC);
                    NRWDE.SaveChanges();

                    lblNutBarcodeMsg.Text = "Bar Code Saved!";
                    lblNutBarcodeMsg.ForeColor = System.Drawing.Color.Green;
                }
                else    // there is an existing bar code, so do nothing
                {
                    lblNutrientBCSave.Text = string.Format("NOT SAVED - Barcode {0} is used", barcode);
                    lblNutrientBCSave.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (DbEntityValidationException ex)  // pretty sure that exceptions will be sql ones
            {
                string n = "";
                if (User.Identity.Name.Length < 3)
                    n = "Not logged in";
                else
                    n = User.Identity.Name;
                string msg = ex.Message;
                string comment = string.Format("SQL valication failure: {0}", ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), n, comment);
            }
        }

        // used to search on org name 
        // moved this from working code in fielddata.cs
        // no help, copied and pasted aspx code, no help
        // retyped the docorations 


        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]

        public static List<string> SearchOrgs(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString; // GlobalSite.RiverWatchDev;

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from Organization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                customers.Add(sdr["OrganizationName"].ToString());
                            }
                        }
                        conn.Close();
                        return customers;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return customers;
            }
        }

        // user has chosen a new index year from list, so change ddl of years, events
        protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
        {

            int CY = int.Parse(ddlYears.SelectedValue);

            DateTime currentYear = new DateTime(CY, 6, 30);
            int stationNumber = (int)Session["STATIONNUMBER"]; // from tbSite
            string NumberSamplePrefix = stationNumber.ToString();

            Session["CURRENTYEAR"] = currentYear; // save for later
            populateSamplesList(NumberSamplePrefix, currentYear); // populate ddl on right side of samples page 
            // now populate org status tab
            if (Session["ORGID"] == null)
                Response.Redirect("~/timedout.aspx");
            int locorgid = (int)Session["ORGID"];
            DateTime locstatusdate = (DateTime)Session["CURRENTYEAR"];
            populateOrgStatus(locorgid, locstatusdate);
      //      populateInboundSamplesList();
            showTabs();

        }

        // user has updated org status detail and we want to return to the same but updated form
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Session["NEWSTATUSID"] == null)
                Response.Redirect("~/timedout.aspx");
            DataSourceSelectArguments ARGS = new DataSourceSelectArguments();
            int sID = (int)Session["NEWSTATUSID"];
            SqlDataSourceOrgStatus.SelectCommand = string.Format("SELECT * FROM [OrgStatus] where ID = {0}", sID);
            SqlDataSourceOrgStatus.Select(ARGS);
        }

        public void showTabs()
        {
            // now enable tabs
            TabICPdata.Enabled = true;
            TabICPdata.Visible = true;
            TabMetalsBarcode.Visible = true;
            TabMetalsBarcode.Enabled = true;
            TabNutrientBarcode.Enabled = true;
            TabNutrientBarcode.Visible = true;
            TabUpdateOrg.Visible = true;
            TabUpdateOrg.Enabled = true;
            TabPanelSample.Enabled = true;
            TabPanelSample.Visible = true;
        }

        public void hideTabs()
        {
            // now enable tabs
            TabICPdata.Enabled = false;
            TabICPdata.Visible = false;
            TabMetalsBarcode.Visible = false;
            TabMetalsBarcode.Enabled = false;
            TabNutrientBarcode.Enabled = false;
            TabNutrientBarcode.Visible = false;
            TabUpdateOrg.Visible = false;
            TabUpdateOrg.Enabled = false;
        }
    }
}