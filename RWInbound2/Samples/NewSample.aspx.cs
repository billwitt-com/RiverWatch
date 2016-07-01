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

namespace RWInbound2.Samples
{
    public partial class NewSample : System.Web.UI.Page
    {
        dbRiverwatchWaterDataEntities2 RWDE = new dbRiverwatchWaterDataEntities2();
        NewRiverwatchEntities NRWDE = new NewRiverwatchEntities();       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Panel1.Visible = false;
                lstSamples.Visible = false;              
                Session["NEWSAMPLE"] = null;

                TabContainer1.Visible = false;
                
           //     CalendarExtenderAnalyzeDate.SelectedDate = DateTime.Now; // set for today so there is something there... 
                tbAnalyzeDate.Text = DateTime.Now.ToShortDateString(); 

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
            }
        }

        // user has chosen a site number, so get detail
        // this just populates the top section of the page, above the tabs
        // 06/24 Added org select list too... 
        protected void btnSiteNumber_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            string samplenumber = "";
            string tmpstr = "";
            DateTime currentYear;
            DateTime thisyear = DateTime.Now;
            string date2parse = "";
            string orgName = ""; 

            //TabPanelSample.Focus(); // put user on sample tab
            //TabPanelSample.BackColor = System.Drawing.Color.Beige;
            //TabPanelSample.Focus();
            
            // build the current year value, from Barb, ie the year ending on June 30th.
            if(thisyear.Month >= 7) // we are in new current year, so don't adjust the year date
            {
                date2parse = string.Format("{0}/07/01", thisyear.Year-1);
                currentYear = DateTime.Parse(date2parse); 
            }
            else
            {
                date2parse = string.Format("{0}/07/01", thisyear.Year-2);
                currentYear = DateTime.Parse(date2parse); 
            }

            kitNumber = -1; // no real kit number yet
            bool success = int.TryParse(tbSite.Text, out stationNumber);
            bool success2 = int.TryParse(tbKitNumber.Text, out kitNumber);

            orgName = tbOrg.Text;            

            if (!success )
            {
                tbSite.Text = "";
                tbKitNumber.Text = "";
                tbOrg.Text = ""; 
                Panel1.Visible = false;
                lstSamples.Visible = false;
                return;
            }
            if (!success2)  // first, is there a kit number in the box, if not... 
            {
                if (orgName.Length > 2)   // there is an org name
                {
                    var KN = (from k in NRWDE.Organizations
                              where k.OrganizationName == orgName
                              select k.KitNumber).FirstOrDefault(); 

                    if (KN != null)
                    {
                        kitNumber = KN.Value; 
                    }
                }
            }

            if(kitNumber == -1)
            {
                tbSite.Text = "";
                tbKitNumber.Text = "";
                tbOrg.Text = "";
                Panel1.Visible = false;
                lstSamples.Visible = false;
                return;            
            }

            // we have good kit number, so fill in the text box, just for....
            tbKitNumber.Text = kitNumber.ToString(); 

            try
            {
                    var RES = from r in NRWDE.Stations
                          join o in NRWDE.Organizations on kitNumber equals o.KitNumber // s.OrganizationID equals o.OrganizationID
                          join ts in NRWDE.OrgStatus on o.OrganizationID equals ts.OrganizationID                          
                          where r.StationNumber == stationNumber & o.KitNumber == kitNumber
                          select new
                          {
                              stnName = r.StationName,
                              orgName = o.OrganizationName,
                              riverName = r.River,
                              startDate = ts.ContractStartDate,
                              endDate = ts.ContractEndDate,
                              active = o.Active,
                              watershed = r.RWWaterShed,
                              stnID = r.ID,
                              orgID = o.OrganizationID
                          };
                if (RES.Count() == 0)
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
                // save some values for later use
                Session["STNID"] = RES.FirstOrDefault().stnID;
                Session["ORGID"] = RES.FirstOrDefault().orgID;
                Session["CURRENTYEAR"] = currentYear;

                tbOrg.Text = orgName;   // to make it 'nice'
                Panel1.Visible = true;
                lblStationNumber.Text = string.Format("Station Number: {0}", stationNumber);
                lblEndDate.Text = string.Format("End Date: {0:M/d/yyyy}", RES.FirstOrDefault().endDate);
                lblOrganization.Text = string.Format("Organization: {0}", RES.FirstOrDefault().orgName);
                lblRiver.Text = string.Format("River: {0}", RES.FirstOrDefault().riverName);
                LblStartDate.Text = string.Format("Start Date: {0:M/d/yyyy}", RES.FirstOrDefault().startDate);
                lblBlankForNow.Text = string.Format("Active: {0}", RES.FirstOrDefault().active.ToString());
                lblWatershed.Text = string.Format("Watershed: {0}", RES.FirstOrDefault().watershed);
                lblStationDescription.Text = string.Format("Description: {0}", RES.FirstOrDefault().stnName);
                TabContainer1.Visible = true; 
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }


            // now populate list box of samples 
            samplenumber = string.Format("{0}.", stationNumber);
            List<string> samps = new List<string>();
            lstSamples.Visible = true;

            populateSamplesList(samplenumber, currentYear); // populate ddl on right side of samples page            

            // get drop down list of inbound samples that have this station and kit numbers 

            try
            {
                var query = from i in NRWDE.InboundSamples
                            where i.KitNum == kitNumber & i.StationNum == stationNumber
                            select
                            i.SampleID.Value;

                if (query != null)
                {
                    List<string> ls = new List<string>();
                    string tmps = "";
                    foreach (long? val in query)
                    {
                        if (val == null)
                            tmps = "";
                        else
                            tmps = val.Value.ToString();
                        ls.Add(tmps);
                    }
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }            
        }

        // common utility to update values on the samples page
        // XXXX take a look at this, I don't think this is difinitive 
        private void updateSamplesPage(string stationChoice)
        {            
            tblSample TS;   
            try
            {
                TS = (tblSample)(from r in NRWDE.tblSamples
                                 where r.NumberSample.StartsWith(stationChoice)
                                 select r).LastOrDefault(); // get most recent? 

                // populate the screen 
                txtSmpNum.Text = TS.SampleNumber;
                txtNumSmp.Text = TS.NumberSample;
                txtDateCollected.Text = TS.DateCollected.ToShortDateString();
                // calc time collected, which is not in table... What a deal
                // it is in last 4 digits of samplenumber

                string sampHours = TS.SampleNumber.Substring(TS.SampleNumber.Length - 4,2); // get first 2 chars of last 4 chars
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            FillTabPanelICPdata(stationChoice);
            FillTabPanelBarcode(stationChoice); 

            // clean up barcode page - and nutrient page too XXXX
            lblBarcodeUsed.Text = "";
            lblCodeInUse.Text = "";
            tbBarcode.Text = ""; 

        }

        // user has chosen a current sample
        protected void lstSamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stationChoice;
            int index = 0;
            stationChoice = lstSamples.SelectedItem.Value;
            // select out the station event which is all digits to the left of the colon

            index = stationChoice.IndexOf(":");
            stationChoice = stationChoice.Substring(0, index); // all to left of colon
            stationChoice = stationChoice.Trim();             // remove any spaces, etc.
            updateSamplesPage(stationChoice); 
        }

        // save or update - but we never overwrite so we always create with a valid flag 
        protected void btnSaveSample_Click(object sender, EventArgs e)
        {
             tblSample orgTS ; 
            bool isNew = false;
            int stationNumber = 0;
            int stnID = 0;
            int orgID = 0;
            DateTime datecollected;
            DateTime timecollected; 
          
            int orgNumber = 0; 
            // make sure we have good basic input, like sample number, etc.  

            // check to see if this is new sample or update 

            if ((txtSmpNum.Text == "") | (txtSmpNum.Text.Length < 2))
                // do something here... XXXX
                return;

            if ((txtNumSmp.Text == "") | (txtNumSmp.Text.Length < 2))
                // do something here... XXXX
                return; 

            bool success = int.TryParse(tbSite.Text, out stationNumber);
            bool success2 = int.TryParse(tbKitNumber.Text, out orgNumber);

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
                // what to do XXXX

            }

          //  txtDateCollected.Text = TS.DateCollected.ToShortDateString();
            // calc time collected, which is not in table... What a deal
            // it is in last 4 digits of samplenumber

            string sampHours = txtTimeCollected.Text.Substring(0, 2);
            string sampMins = txtTimeCollected.Text.Substring(3, 2);
            double hrs = double.Parse(sampHours);
            double mins = double.Parse(sampMins);           
         
            // scrape the screen
            // ADD DATES, ETC WHEN WE HAVE newWater TABLE XXXX
            tblSample TS = new tblSample();
            TS.StationID = stationNumber;
            TS.OrganizationID = orgNumber;
            TS.DateCollected = DateTime.Parse(txtDateCollected.Text);
            TS.TimeCollected = TS.DateCollected.AddHours(hrs).AddMinutes(mins); 
            TS.SampleNumber =  txtSmpNum.Text.Trim() ;
            TS.NumberSample = txtNumSmp.Text.Trim();
            TS.ChainOfCustody = chkCOC.Checked;
            TS.DataSheetIncluded = chkDataSheet.Checked;
            TS.Bug = chkBugs.Checked;
            TS.BlankMetals = chkBlkMtls.Checked;
            TS.BugsQA = chkBugQA.Checked;
            TS.ChlorideSulfate = chkCS.Checked ;
            TS.DuplicatedCS = chkCSDupe.Checked   ;
            TS.DuplicatedMetals = chkDupeMtls.Checked  ;
            TS.MissingDataSheetReceived = chkMDSR.Checked;
            TS.NoMetals = chkNoMtls.Checked;
            TS.NoNutrient = chkNoNut.Checked;
            TS.NitratePhosphorus = chkNP.Checked;
            TS.DuplicatedNP = chkNPDupe.Checked;
            TS.PhysicalHabitat = chkPhysHab.Checked;
            TS.TotalSuspendedSolids = chkTSS.Checked;
            TS.DuplicatedTSS = chkTSSDupe.Checked;            

            if(txtMDSR.Text.Length < 5)    // no date here... 
            {
                TS.MissingDataSheetReqDate = null; 
            }
            else
            {
                TS.MissingDataSheetReqDate = DateTime.Parse(txtMDSR.Text); 
            }
            
            TS.Comment = txtComment.Text ;

            // now decide if this is an update or is new, and update accordingly 
            try
            {
               
                // if there is an old record, update old record by setting valid = false
                orgTS = (from s in NRWDE.tblSamples
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

                NRWDE.tblSamples.Add(TS);
                NRWDE.SaveChanges(); // update all records
            }
            catch(Exception ex)
            {
                string msg = ex.Message; 
            }

            if(Session["CURRENTYEAR"] != null)
            {
                DateTime currentYear = (DateTime)Session["CURRENTYEAR"]; 
                string numsample = txtNumSmp.Text.Trim(); 

                int index = numsample.IndexOf(".");
                if(index > 0)
                {
                    numsample = numsample.Substring(0,index); 
                }
                populateSamplesList(numsample, currentYear); // populate ddl on right side of samples page   
            }
        }

        // we are making a new, fresh sample set with new dates, etc.
        protected void btnCreate_Click(object sender, EventArgs e)
        {           
            DateTime newdate;
            string sampnum = ""; 
            string timestr = "";
            int hours = 0;
            int mins = 0;
            string newEvent = "";

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
            if(!success)
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
                mins );

            txtSmpNum.Text = sampnum; 

            // now build Event number (number sample in current db table)

            try
            {
                string result =  (string) (from r in NRWDE.tblSamples
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }
        }

        // from create icp data tab
        protected void btnCheckBarCode2Create_Click(object sender, EventArgs e)
        {

        }

        // user chose an item from the list of inboundsamples that had kit and station numbers

        protected void ddlInboundSamplePick_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sampNumber = ddlInboundSamplePick.SelectedItem.Text;
            long sNum = long.Parse(sampNumber); 

            try
            {
                var query = from s in NRWDE.tblSamples
                            where s.SampleNumber == sampNumber
                            select s.NumberSample;
                            

                if (query != null)
                {
                    txtNumSmp.Text = (string)query.FirstOrDefault();
                    txtSmpNum.Text = sampNumber; 
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

            // now fill in some of the known values

            try
            {
                InboundSample TS = (InboundSample)(from s in NRWDE.InboundSamples
                                                        where s.SampleID.Value == sNum
                                                        select s).FirstOrDefault();


                // populate the screen 
                //txtSmpNum.Text = TS.SampleID.Value.ToString(); 
                //txtNumSmp.Text = TS.NumberSample;
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
            catch(Exception ex)
            {

            }
        }
        
        // fill in org stat tab
        private void updateOrg()
        {


        //    var RES = from r in NRWDE.tblStatus 




        }
        // update data table with new results OR create new sample 
        protected void btnSaveMetals_Click(object sender, EventArgs e)
        {
            // scrape table
           string barcode = tbBarcode.Text.Trim();
            string type = rbListSampleTypes.SelectedValue ; // get user choice for type
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
                lblBarcodeUsed.Text = "NOT SAVED !! CODE IN USE!";
                lblBarcodeUsed.ForeColor = System.Drawing.Color.Red;
                lblCodeInUse.Text = "NOT SAVED !! CODE IN USE!";
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
            filtered = type.Substring(1,1) == "1" | type.Substring(1,1) == "4"; 
            normal = type.Substring(0,1) == "0" ; 
            blank = type.Substring(0,1) == "1" ;  
            duplicate = type.Substring(0,1) == "2" ;  

            smpNum = txtSmpNum.Text.Trim();

            if(normal)
                typ = "N";
            if(blank)
                typ = "B";
            if(duplicate)
                typ = "D";            

            // get sample number from tblSample 

            int SN = (from q in NRWDE.tblSamples
                        where q.SampleNumber == smpNum & q.Valid == true
                        select q.SampleID).FirstOrDefault();

            MetalBarCode TMB = new MetalBarCode(); // make a new entity to fill in with data

            TMB.DateCreated = DateTime.Now;
            TMB.Filtered = filtered;
            TMB.LabID = barcode;        // XXXX review as so much of this record is redundant 
            TMB.Code = type.Substring(0,2); 
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
        
        // XXXX is this correct? Looks like we would get all barcodes for a station, 
        protected void FillTabPanelBarcode(string station)
        {
            string sid = station;   // number sample in this current context. Will change name to EventType or something XXXX
            string stationID = station;
            int idx = stationID.IndexOf(".");

            if (idx > 0)
            {
                sid = stationID.Substring(0, idx); // if this was a complete event, get the leading station id
            }

            string queryString = string.Format("SELECT [LabID] ,[Code] ,[Type] ,[Filtered] ,[BoxNumber] " +

                        " FROM [dbRiverwatchWaterData].[dbo].[tblMetalBarCode] " +
                        " where NumberSample like '{0}'", stationID);
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
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
                string msg = ex.Message;
            }
        }

        protected void FillTabPanelICPdata(string station)
        {
            // quick fix:

            string sid = txtNumSmp.Text.Trim(); // scrape page
           // query for barcodes that have been entered but not analyzed
            // thus are in tblsample BUT NOT IN METALBARCODES        
           
            // use a sql command since it allows easier reading of the not exists and is perhaps faster
            string queryString = "";
            queryString = string.Format(
                " SELECT    [SampleID] , LabID as [Barcode], Code as [Sample Type] " +
                " FROM      [dbRiverwatchWaterData].[dbo].[tblMetalBarCode] AS a " +
                " WHERE    a.NumberSample like '{0}' " + 
                " and   NOT EXISTS (SELECT * FROM[dbRiverwatchWaterData].[dbo].[expStnMetal] AS b " +
                " WHERE b.SampleID = a.SampleID) " +
                 " and  Not Exists (Select * From [dbRiverwatchWaterData].[dbo].[tblInboundICP] as c " +
                 " Where a.LabID  =  c.CODE) " +
                " order by SampleID desc", sid);
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
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
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }

        // user has entered a barcode on page, and now wants to see if it is unique... 
        protected void btnCheckBarcode_Click(object sender, EventArgs e)
        {
            // scrape table
            string barcode = tbBarcode.Text.Trim().ToUpper();
            var query = from q in NRWDE.MetalBarCodes
                        where q.LabID.ToUpper() == barcode
                        select q;
             
            if (query.Count() > 0)   // code in use
            {
                // lblCodeInUse
                lblBarcodeUsed.Text = "CODE IN USE!";
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

            for(int x = 0; x < rows; x++)
            {
                CheckBox cb = GridView1.Rows[x].FindControl("cbSelectBarcode") as CheckBox; // get the checkbox for this row
                if(cb.Checked)
                {
                    bool success = int.TryParse( GridView1.Rows[x].Cells[1].Text, out tblSampleID);

                    barcode = GridView1.Rows[x].Cells[2].Text;
                    type  = GridView1.Rows[x].Cells[3].Text;                 
                    makeICPInbound(barcode, type, tblSampleID);
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
        private void makeICPInbound(string barcode, string type, int tblSampleID)
        {
            Random RAND = new Random();
            tblInboundICP InBound = new tblInboundICP(); // create new data entity
            InBound.tblSampleID = tblSampleID;
            InBound.CODE = barcode; //prefix + digits.ToString(); // make new barcode
            InBound.DUPLICATE = type; // this is poorly named
            decimal mult = 0.0m;
            decimal V = 0;

            if (type.Substring(0, 1) == "0")  // normal sample
            {
                mult = 100;
            }
            if (type.Substring(0, 1) == "1")  // blank
            {
                mult = .1m;
            }

            if (type.Substring(0, 1) == "2")  // duplicate 
            {
                mult = 125;
            }

            // create queriable data set so we only do one db hit, I hope... 

            //DbSet<tlkLimit>
            //    LIM = (DbSet<tlkLimit>)from r in NRWDE.tlkLimits
            //                                              select r;

            try
            {
                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "AL"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.AL_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.AL_T = (decimal)InBound.AL_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "AS"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.AS_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.AS_T = (decimal)InBound.AS_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "CA"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.CA_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.CA_T = (decimal)InBound.CA_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "CD"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.CD_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.CD_T = (decimal)InBound.CD_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "CU"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.CU_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.CU_T = (decimal)InBound.CU_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "FE"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.FE_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.FE_T = (decimal)InBound.FE_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "K"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.K_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.K_T = (decimal)InBound.K_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "MG"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.MG_D = (decimal)RAND.NextDouble() * V * mult; ; // make Total_Dups smaller than Disolved_Dups 
                InBound.MG_T = (decimal)InBound.MG_D - (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "MN"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.MN_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.MN_T = (decimal)InBound.MN_D + (decimal)RAND.NextDouble() - .5m; ;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "NA"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.NA_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.NA_T = (decimal)InBound.NA_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "PB"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.PB_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.PB_T = (decimal)InBound.PB_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "SE"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.SE_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.SE_T = (decimal)InBound.SE_D - (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in NRWDE.tlkLimits
                              where z.Element.ToUpper() == "ZN"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.ZN_D = (decimal)RAND.NextDouble() * V * mult; ; // make Total_Dups smaller than Disolved_Dups 
                InBound.ZN_T = (decimal)InBound.ZN_D - .5m;


                InBound.Comments = "Created by hand for testing";
                InBound.FailedChems = ""; 

                InBound.Reviewed = false;
                InBound.ANADATE = DateTime.Now;
                InBound.DATE_SENT = DateTime.Now.AddDays(-2);
                InBound.PassValStep = 1;    // this needs to be one so that the origional code will run... BW
                InBound.COMPLETE = true;
                InBound.FailedChems = "From Fake ICP record"; 

                // XXXX since this is a temp workaround for not having incoming data, we will put this in old db, where sproc will move it to local tables
                RWDE.tblInboundICPs.Add(InBound);   
                RWDE.SaveChanges();
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            // rebuild the data grid for icp creation 
            // XXXX why is this commented out? bw 06/30
       //     FillTabPanelICPdata(txtNumSmp.Text.Trim()); 
        }

        private void populateSamplesList(string sampleNumber, DateTime currentYear)
        {
            string tmpstr = "";
            List<string> samps = new List<string>();
            // get drop down list of samples and numbers from DB, for this current year
            try
            {
                var SMP = from s in NRWDE.tblSamples
                          where s.NumberSample.StartsWith(sampleNumber) & s.DateCollected >= currentYear & s.Valid == true
                          orderby s.DateCollected descending
                          select new
                          {
                              SampNumber = s.NumberSample,
                              SampDate = s.DateCollected
                          };

                foreach (var s in SMP)
                {
                    tmpstr = string.Format("{0} : {1:M/d/yyyy}", s.SampNumber, s.SampDate);
                    samps.Add(tmpstr); // fill in list
                }

                lstSamples.DataSource = samps;
                lstSamples.DataBind();
            }
            catch (Exception ex)
            {
                Panel1.Visible = false;
                string msg = ex.Message;
            }
        }

        // approved method of getting data for the autocomplete extender. Can reuse for other tables... 
        /// <summary>
        /// used to populate text box asking for org name
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count) 
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchDEV"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select OrganizationName from Organization where OrganizationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        List<string> customers = new List<string>();

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
                string msg = ex.Message;    // XXXX need to build an error log file and logging code
                return null;
            }
        }

        // check to see if there is a nutrient bar code already and print message 
        protected void btnBarcodeSearch_Click(object sender, EventArgs e)
        {
            // scrape barcode from page

            string barcode = tbNutrientCode.Text.Trim().ToUpper(); 
            if(barcode.Length < 3)
            {
                lblNutBarcodeMsg.Text = "Please enter a valid bar code";
                tbNutrientCode.Text = "";
                return;
            }

            try
            {
                string R = (string) (from r in NRWDE.NutrientBarCodes
                        where r.LabID.ToUpper() == barcode
                        select r.LabID).FirstOrDefault();
                if(R == null)   // no existing barcode
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
                LogErrror LE = new LogErrror();
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
                var R = (from r in NRWDE.NutrientBarCodes
                              where r.LabID.ToUpper() == barcode
                              select r.ID).FirstOrDefault();
                if ((R == null) | (R < 0))
                {
                    lblNutBarcodeMsg.Text = "Bar Code Saved!";
                    lblNutBarcodeMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblNutrientBCSave.Text = string.Format("NOT SAVED - Barcode {0} is used", barcode);
                    lblNutrientBCSave.ForeColor = System.Drawing.Color.Red;
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }



            try
            {
                int R = (int)(from r in NRWDE.NutrientBarCodes
                              where r.LabID.ToUpper() == barcode
                              select r.ID).FirstOrDefault();
                if ((R == null) | (R < 0))
                {
                    lblNutBarcodeMsg.Text = "Bar Code Saved!";
                    lblNutBarcodeMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblNutrientBCSave.Text = string.Format("Barcode {0} is used", barcode);
                    lblNutrientBCSave.ForeColor = System.Drawing.Color.Red;
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
                LogErrror LE = new LogErrror();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "");
            }

        }
    }    
}