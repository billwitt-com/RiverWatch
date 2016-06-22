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
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Panel1.Visible = false;
                lstSamples.Visible = false;              
                Session["NEWSAMPLE"] = null;

                TabContainer1.Visible = false;

                // fill radio button list from tlkMetalBarCodeType

                try
                {
                    var types = (from t in RWDE.tlkMetalBarCodeTypes
                                 orderby t.Code
                                 select                                 
                                    t.Code + " " + t.Description
                                 ).ToList();

                    rbListSampleTypes.DataSource = types;  
                    rbListSampleTypes.DataBind();
                }
                catch(Exception ex)
                {
                    string msg = ex.Message; 

                }
            }
        }

        // user has chosen a site number, so get detail
        // this just populates the top section of the page, above the tabs
        protected void btnSiteNumber_Click(object sender, EventArgs e)
        {
            int stationNumber = 0;
            int kitNumber = 0;
            string samplenumber = "";
            string tmpstr = "";
            DateTime currentYear;
            DateTime thisyear = DateTime.Now;
            string date2parse = "";

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
            
            bool success = int.TryParse(tbSite.Text, out stationNumber);
            bool success2 = int.TryParse(tbKitNumber.Text, out kitNumber);

            if (!success | !success2)
            {
                tbSite.Text = "";
                tbKitNumber.Text = ""; 
                Panel1.Visible = false;
                lstSamples.Visible = false;
                return;
            }
            try
            {
                    var RES = from r in RWDE.tblStations
                          join o in RWDE.tblOrganizations on kitNumber equals o.KitNumber // s.OrganizationID equals o.OrganizationID
                          join ts in RWDE.tblStatus on o.OrganizationID equals ts.OrganizationID
                          
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
                              stnID = r.StationID,
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
                    txtAnalyzeDate.Text = "";
                    txtSmpNum.Text = "";
                    txtNumSmp.Text = ""; 
                    return;
                }
                // save some values for later use
                Session["STNID"] = RES.FirstOrDefault().stnID;
                Session["ORGID"] = RES.FirstOrDefault().orgID;
                Session["CURRENTYEAR"] = currentYear; 


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
                Panel1.Visible = false;
                lstSamples.Visible = false;
                string msg = ex.Message;
            }

            // now populate list box of samples 
            samplenumber = string.Format("{0}.", stationNumber);
            List<string> samps = new List<string>();
            lstSamples.Visible = true;

            populateSamplesList(samplenumber, currentYear); // populate ddl on right side of samples page            

            // get drop down list of inbound samples that have this station and kit numbers 

            try
            {
                var query = from i in RWDE.tblInboundSamples
                            where i.KitNum == kitNumber & i.StationNum == stationNumber
                            select
                            i.SampleID.Value;

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
            catch (Exception ex)
            {
                Panel1.Visible = false;
                string msg = ex.Message;
            }            
        }

        // common utility to update values on the samples page
        private void updateSamplesPage(string stationChoice)
        {            
            tblSample TS;   
            try
            {
                TS = (tblSample)(from r in RWDE.tblSamples
                                 where r.NumberSample.StartsWith(stationChoice) 
                                 select r).FirstOrDefault(); 

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
                string msg = ex.Message;
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
            // ADD DATES, ETC WHEN WE HAVE NEW TABLE XXXX
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
                orgTS = (from s in RWDE.tblSamples
                            where s.SampleNumber == txtSmpNum.Text & s.Valid == true
                            select s).FirstOrDefault();

                if (orgTS != null)
                {
                    orgTS.Valid = false;
                    orgTS.UserCreated = User.Identity.Name;
                    orgTS.DateCreated = DateTime.Now; 
                    RWDE.SaveChanges(); // update all records
                }               

                // add new record 
                TS.StationID = stnID;
                TS.OrganizationID = orgID;
                TS.Valid = true;
                TS.UserCreated = User.Identity.Name;
                TS.DateCreated = DateTime.Now;
                TS.DateCollected = DateTime.Parse(txtDateCollected.Text.Trim());
                TS.Comment = txtComment.Text;

                RWDE.tblSamples.Add(TS);
                RWDE.SaveChanges(); // update all records
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
                string result =  (string) (from r in RWDE.tblSamples
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
                

            catch(Exception ex)
            {


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
                var query = from s in RWDE.tblSamples
                            where s.SampleNumber == sampNumber
                            select s.NumberSample;
                            

                if (query != null)
                {
                    txtNumSmp.Text = (string)query.FirstOrDefault();
                    txtSmpNum.Text = sampNumber; 
                }

            }
            catch(Exception ex)
            {

            }

            // now fill in some of the known values

            try
            {
                tblInboundSample TS = (tblInboundSample)(from s in RWDE.tblInboundSamples
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


        //    var RES = from r in RWDE.tblStatus 




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

                       var query = from q in RWDE.tblMetalBarCodes
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

            int SN = (from q in RWDE.tblSamples
                        where q.SampleNumber == smpNum & q.Valid == true
                        select q.SampleID).FirstOrDefault();

            tblMetalBarCode TMB = new tblMetalBarCode(); // make a new entity to fill in wiht data

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

            RWDE.tblMetalBarCodes.Add(TMB);
            RWDE.SaveChanges(); // insert record 
        }
        
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
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchWaterDEV"].ConnectionString;
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
            //string sid = station;  
            //string stationID = station; 
            //int idx = stationID.IndexOf(".");

            //if(idx > 0)
            //{
            //   sid = stationID.Substring(0, idx); // if this was a complete event, get the leading station id
            //}
           
            // use a sql command since it allows easier reading of the not exists and is perhaps faster
            //    " WHERE    a.NumberSample like '{0}.%' " + 
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
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["RiverwatchWaterDEV"].ConnectionString;
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
            var query = from q in RWDE.tblMetalBarCodes
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



            //if (query.Count() > 0)   // code in use
            //{
            //    lblBarcodeUsed.Text = "CODE IN USE!";
            //    lblBarcodeUsed.ForeColor = System.Drawing.Color.Red; 
            //    return;
            //}
            //else
            //{
            //    lblBarcodeUsed.Text = "OK";
            //    lblBarcodeUsed.ForeColor = System.Drawing.Color.Green;  
            //    return;
            //}
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
            //    LIM = (DbSet<tlkLimit>)from r in RWDE.tlkLimits
            //                                              select r;

            try
            {
                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "AL"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.AL_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.AL_T = (decimal)InBound.AL_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "AS"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.AS_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.AS_T = (decimal)InBound.AS_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "CA"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.CA_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.CA_T = (decimal)InBound.CA_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "CD"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.CD_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.CD_T = (decimal)InBound.CD_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "CU"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.CU_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.CU_T = (decimal)InBound.CU_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "FE"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.FE_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.FE_T = (decimal)InBound.FE_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "K"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.K_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.K_T = (decimal)InBound.K_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "MG"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.MG_D = (decimal)RAND.NextDouble() * V * mult; ; // make Total smaller than Disolved 
                InBound.MG_T = (decimal)InBound.MG_D - (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "MN"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.MN_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.MN_T = (decimal)InBound.MN_D + (decimal)RAND.NextDouble() - .5m; ;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "NA"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.NA_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.NA_T = (decimal)InBound.NA_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "PB"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.PB_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.PB_T = (decimal)InBound.PB_D + (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "SE"
                              select z.Reporting.Value).FirstOrDefault();
                InBound.SE_D = (decimal)RAND.NextDouble() * V * mult;
                InBound.SE_T = (decimal)InBound.SE_D - (decimal)RAND.NextDouble() + .5m;

                V = (decimal)(from z in RWDE.tlkLimits
                              where z.Element.ToUpper() == "ZN"
                              select z.Reporting.Value).FirstOrDefault();

                InBound.ZN_D = (decimal)RAND.NextDouble() * V * mult; ; // make Total smaller than Disolved 
                InBound.ZN_T = (decimal)InBound.ZN_D - .5m;


                InBound.Comments = "Created by hand for testing";
                InBound.FailedChems = ""; 

                InBound.Reviewed = false;
                InBound.ANADATE = DateTime.Now;
                InBound.DATE_SENT = DateTime.Now.AddDays(-2);
                InBound.PassValStep = 1;    // this needs to be one so that the origional code will run... BW
                InBound.COMPLETE = true;
                InBound.FailedChems = "From Fake ICP record"; 

                RWDE.tblInboundICPs.Add(InBound);
                RWDE.SaveChanges();
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            // rebuild the data grid for icp creation

       //     FillTabPanelICPdata(txtNumSmp.Text.Trim()); 
        }

        private void populateSamplesList(string sampleNumber, DateTime currentYear)
        {
            string tmpstr = "";
            List<string> samps = new List<string>();
            // get drop down list of samples and numbers from DB, for this current year
            try
            {
                var SMP = from s in RWDE.tblSamples
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
    }    
}