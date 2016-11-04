﻿using System;
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
// 09/23 Added code to add all stations to project 1 in projectstations

namespace RWInbound2.Admin
{
    public partial class AdminStations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // fill in drop down lists... Fun
            if(!IsPostBack)
            {
                Session["NEW"] = false; 
                lblStatus.Visible = false;
            //    pnlTable.Visible = false; // hide for now     
                loadDropDownLists(); 
            }
        }
        public void resetControls()
        {
            // reset the drop down controls and clear text boxes
          
            txtStationName.Text = "";
            txtStationNumber.Text = "";
            txtriver.Text = "";
            ddlCounty.ClearSelection();
            ddlEcoRegion.ClearSelection();
            ddlGrid.ClearSelection();
            ddlHydroUnit.ClearSelection();
            ddlQUADI.ClearSelection();
            ddlQuarterSection.ClearSelection();
            ddlRange.ClearSelection();
            ddlRegion.ClearSelection();
            ddlRiver.ClearSelection();
            ddlRWWaterShed.ClearSelection();
            ddlSection.ClearSelection();
            ddlState.ClearSelection();
            ddlStationQUAD.ClearSelection();
            ddlStationStatus.ClearSelection();
            ddlStationType.ClearSelection();
            ddlTownship.ClearSelection();
            ddlWaterBodyID.ClearSelection();
            ddlWaterCode.ClearSelection();
            ddlWQCCWaterShed.ClearSelection();
            ddlWSR.ClearSelection();
            cbStateEngineering.Checked = false;
            cbUSGS.Checked = false;
        }
        protected void btnNewStation_Click(object sender, EventArgs e)
        {
            // reset the drop down controls and clear text boxes
            pnlInput.Visible = false;
            resetControls(); 
            txtStationName.Focus();
            txtStationName.BackColor = System.Drawing.Color.Bisque;
            Session["NEW"] = true;
          //  loadDropDownLists();
        }

        protected void btnSelectStnName_Click(object sender, EventArgs e)
        {
            string stationName = tbStationName.Text.Trim();
            Session["NEW"] = false;
            if(stationName.Length < 2)
            {
                lblStatus.Text = "Please select a valid station name";
                lblStatus.Visible = true;
                tbStationName.Text = "";
                return;
            }

            RiverWatchEntities NRWE = new RiverWatchEntities();
            try
            {
                var R = (from q in NRWE.Stations
                         where q.StationName == stationName
                         select q.StationNumber).FirstOrDefault();

                lblStatus.Visible = false; 

                if(R == null)
                {
                    lblStatus.Text = string.Format("Station Name {0} not found", stationName);
                    lblStatus.Visible = true;
                    tbStationName.Text = "";
                    tbStnNumber.Text = "";
                    return;      
                }
           //     pnlTable.Visible = true; 
                populatePage(R.Value);
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Admin Stations");
            }
        }

        protected void btnSelectStnNumber_Click(object sender, EventArgs e)
        {
            bool success = false;
            int stnNumber = 0;
            Session["NEW"] = false;
            try
            {
                // scrape station number from form
                string stn = tbStnNumber.Text;
                success = int.TryParse(stn, out stnNumber);
                if (success)
                {
                    RiverWatchEntities NRWE = new RiverWatchEntities();
                    string SN = (from q in NRWE.Stations
                                 where q.StationNumber == stnNumber
                                 select q.StationName).FirstOrDefault();
                    if (SN.Length > 0)
                    {
                        tbStationName.Text = SN;
                    }
                    lblStatus.Visible = false; 
                }
                else
                {
                    tbStnNumber.Text = "";
                    lblStatus.Text = "Please select a valid station number";
                    lblStatus.Visible = true;
                    return;
                }

           //     pnlTable.Visible = true;
                populatePage(stnNumber);
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Admin Stations");
            }
        }

        protected void ddlRiver_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void populatePage(int stnNumber)
        {
            //loadDropDownLists();
           // clearDDLselections(); 
            RiverWatchEntities NRWE = new RiverWatchEntities();
            int listCount = 0; 
           
            try
            {
                var SN = (from q in NRWE.Stations
                          where q.StationNumber == stnNumber
                          select q).FirstOrDefault();    // get the data
                if (SN == null)
                    return;

             //   resetControls(); // make them all default state so if values below do not match the selected value, they are at default values

                txtStationName.Text = SN.StationName;
                txtStationNumber.Text = SN.StationNumber.ToString();
                txtriver.Text = SN.River ?? "";
                tbdescription.Text = SN.Description ?? ""; 
                tbLongtitude.Text = SN.Longtitude.ToString();
                tbLatitude.Text = SN.Latitude.ToString();
                tbAquaticModelIndex.Text = SN.AquaticModelIndex ?? "";
                tbUTMX.Text = SN.UTMX.ToString();
                tbUTMY.Text = SN.UTMY.ToString();
                tbNearCity.Text = SN.NearCity ?? "";
                tbMove.Text = SN.Move ?? "";
                tbElevation.Text = SN.Elevation.ToString(); 
                cbStateEngineering.Checked = SN.StateEngineering.Value;
                cbUSGS.Checked = SN.USGS.Value; 

                // do ddls

                ddlRiver.SelectedValue = SN.River;
                ddlCounty.SelectedValue = SN.County ?? "";

                ////////ddlWaterCode.Items.Insert(0,SN.WaterCode);
                /////////  ddlEcoRegion.SelectedItem.Value = SN.EcoRegion; // ?? "";
                
                if (SN.EcoRegion.Length > 1)
                    ddlEcoRegion.SelectedValue = SN.EcoRegion; // ?? "";
                else
                    ddlEcoRegion.SelectedIndex = 0; 

                ddlGrid.SelectedValue = SN.Grid;
                ddlHydroUnit.SelectedValue = SN.HydroUnit;
                ddlQUADI.SelectedValue = SN.QUADI;
                ddlQuarterSection.SelectedValue = SN.QuaterSection;
                ddlRange.SelectedValue = SN.Range;
                ddlRegion.SelectedValue = SN.Region;

                ///////////////////                     ddlRWWaterShed.SelectedItem.Value = SN.RWWaterShed;
                if (SN.RWWaterShed.Length > 0)
                    ddlRWWaterShed.SelectedValue = SN.RWWaterShed;
                else
                    ddlRWWaterShed.SelectedIndex = 0;

                ddlSection.SelectedValue = SN.Section.ToString();
                ddlState.SelectedValue = SN.State;
                ddlStationQUAD.SelectedValue = SN.StationQUAD;

                ddlStationStatus.SelectedValue = SN.StationStatus;

                if (SN.StationType.Length > 0)
                    ddlStationType.SelectedItem.Value = SN.StationType;
                else
                    ddlStationType.SelectedIndex = 0;

                ddlTownship.SelectedValue = SN.Township;


                listCount = ddlWaterBodyID.Items.Count;
                for (int z = 0; z < listCount; z++)
                {
                    if (ddlWaterBodyID.Items[z].Value.Contains(SN.WaterBodyID))
                    {
                        ddlWaterBodyID.Items[z].Selected = true;
                        break;
                    }
                }

                listCount = ddlWaterCode.Items.Count;
                for (int z = 0; z < listCount; z++)
                {
                    if (ddlWaterCode.Items[z].Value.Contains(SN.WaterCode))
                    {
                        ddlWaterCode.Items[z].Selected = true;
                        break; 
                    }
                }

                ddlWQCCWaterShed.SelectedValue = SN.WQCCWaterShed;
                ddlWSR.SelectedValue = SN.WaterShedRegion;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                // do special case here, where DDL is unhappy
                if(msg.Contains("SelectedValue"))   // we have a ddl unhappy here
                {
                    int second = 0;
                    second = msg.IndexOf("'", 1);

                    string ddlName = msg.Substring(1, second -1);
                  //  DropDownList DL = this.Controls[0].FindControl(ddlName) as DropDownList;
                    DropDownList DL = Page.FindControl(ddlName) as DropDownList;

                    DL.SelectedIndex = 0;
                    return;
                }
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Admin Stations");
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string stationName = "";
            if (Session["NEW"] == null)
                Response.Redirect("~/timedout.aspx");
            lblStatus.Visible = false; 

            bool isNewStation = false;
            bool success = false;
            int stnNumber = 0;
            string stnNumString = "";
            isNewStation = (bool)Session["NEW"];
            int tempInt = 0;
            string tmpString = "";
            int idx = 0;

            stnNumString = txtStationNumber.Text.Trim();
            success = int.TryParse(stnNumString, out stnNumber);

            lblStatus.Visible = false; 

            if (!success)
            {
                tbStnNumber.Text = "";
                lblStatus.Visible = true;
                lblStatus.Text = "Please select a valid station number";
                return;
            }

            // do some validation here.. .
            tmpString = tbLatitude.Text;
            if(tmpString.Length < 2)
            {
                lblStatus.Text = "Please enter a valid Latitude";
                lblStatus.Visible = true;
                tbLatitude.Focus();
                return; 
            }

            tmpString = tbLongtitude.Text;
            if (tmpString.Length < 2)
            {
                lblStatus.Text = "Please enter a valid Longtitude";
                lblStatus.Visible = true;
                tbLatitude.Focus();
                return;
            }

            tmpString = txtStationName.Text;                 //tbStationName.Text;
            if (tmpString.Length < 2)   // changed this to 2 since there are some very short station names
            {
                lblStatus.Text = "Please enter a valid StationName";
                lblStatus.Visible = true;
                tbLatitude.Focus();
                return;
            }
            stationName = tmpString; 

            Station STN;
            RiverWatchEntities NRWE = new RiverWatchEntities();
            try
            {
                if (isNewStation)
                {
                    // check to see if stn number is currently used
                    STN = (from q in NRWE.Stations
                           where q.StationNumber == stnNumber
                           select q).FirstOrDefault();
                    lblStatus.Visible = false; 
                    if (STN != null)
                    {
                        tbStnNumber.Text = "";
                        lblStatus.Text = string.Format("Station Number {0} is used!", stnNumber);
                        lblStatus.Visible = true; 
                        return;
                    }
                    // good to go here 
                    //  STN = null; 
                    STN = new Station();
                }
                else  // not new, so get the origional
                {
                    STN = (from q in NRWE.Stations
                           where q.StationNumber == stnNumber
                           select q).FirstOrDefault();
                }

                if (isNewStation)
                {
                   STN.StationNumber = stnNumber;
                }

                STN.StationName = stationName;  //txtStationName.Text;
                STN.River = txtriver.Text;
                STN.County = ddlCounty.SelectedValue;

                STN.EcoRegion = ddlEcoRegion.SelectedItem.Value;                 //.SelectedValue;
                STN.Grid = ddlGrid.SelectedValue;
                STN.HydroUnit = ddlHydroUnit.SelectedValue; 
                STN.QUADI = ddlQUADI.SelectedValue ;
                STN.QuaterSection = ddlQuarterSection.SelectedValue ;
                STN.Range = ddlRange.SelectedValue ;
                STN.Region = ddlRegion.SelectedValue ;
                STN.River = ddlRiver.SelectedValue;

                STN.RWWaterShed = ddlRWWaterShed.SelectedItem.Value;                //.SelectedValue ;

                if (int.TryParse(ddlSection.SelectedValue, out tempInt))    // XXXX should change this in the data base to string
                    STN.Section = tempInt;

                STN.State = ddlState.SelectedValue.Trim().Substring(0,2);
                STN.StationQUAD = ddlStationQUAD.SelectedValue;
                STN.StationStatus = ddlStationStatus.SelectedValue;

                STN.StationType = ddlStationType.SelectedItem.Value; // ddlStationType.SelectedValue;
                STN.Township = ddlTownship.SelectedValue;

         //       wbid.Add(x.WBID + " : " + x.WATERSHED + " - " + x.SUBBASIN + " - " + wrkStr); 

                tmpString = ddlWaterBodyID.SelectedValue;
                idx = tmpString.IndexOf(":"); 
                if(idx != 0)
                {
                    tmpString = tmpString.Substring(0, idx -1 ); 
                }
                STN.WaterBodyID = tmpString.Trim(); // ddlWaterBodyID.SelectedValue;

                // wcList.Add(x.WATERNAME + " - " + x.LOCATION + " : " + x.WATERCODE); 
                idx = 0;
                tmpString = ""; // just in case... 

                tmpString = ddlWaterCode.SelectedValue;
                int len = tmpString.Length; 
                idx = tmpString.IndexOf(":");
                if (idx != 0)
                {
                    tmpString = tmpString.Substring(idx + 1);              //(idx + 1, len - idx);
                    STN.WaterCode = tmpString.Trim(); // ddlWaterCode.SelectedValue;
                }

                STN.WQCCWaterShed = ddlWQCCWaterShed.SelectedItem.Value;                //.SelectedValue;
                STN.WaterShedRegion = ddlWSR.SelectedValue;
                STN.StateEngineering = cbStateEngineering.Checked;
                STN.USGS = cbUSGS.Checked;

                string nam = "";
                    if (User.Identity.Name.Length < 3)
                        nam = "Not logged in";
                    else
                        nam = User.Identity.Name;
                    
                STN.UserCreated = nam;
                STN.DateCreated = DateTime.Now;
                NRWE.SaveChanges();

                lblStatus.Visible = false; 

                if(isNewStation)
                {
                    NRWE.Stations.Add(STN);
                    // add station to projects 
                    ProjectStation PS = new ProjectStation();
                    PS.ProjectID = 1;
                    PS.StationNumber = STN.StationNumber;
                    PS.DateCreated = DateTime.Now;                     
                    PS.UserCreated = nam;
                    PS.Valid = true; 
                    NRWE.ProjectStations.Add(PS);                 
                    NRWE.SaveChanges();
                }

                lblStatus.Text = string.Format("Station {0} saved", stationName);
                lblStatus.Visible = true; 
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Admin Stations");
                lblStatus.Text = string.Format("Data Not Saved");
                lblStatus.Visible = true;
                return;
            }
        }

        private string selectedValue; 
        protected void ddl_DataBinding(object sender, EventArgs e)
        {

            DropDownList theDropDownList = (DropDownList)sender;
            theDropDownList.DataBinding -= new EventHandler(ddl_DataBinding);
            theDropDownList.AppendDataBoundItems = true;

            selectedValue = "";
            try
            {
                theDropDownList.DataBind();
            }
            catch (ArgumentOutOfRangeException)
            {
              //  theDropDownList.Items.Clear();
                theDropDownList.Items.Insert(0, new ListItem("Please select:", ""));
                theDropDownList.SelectedValue = "";               
            }
        }
        public void loadDropDownLists()
        {
            RiverWatchEntities NRWE = new RiverWatchEntities();
            string wrkStr = "";
            try
            {
                // ddlStationStatus
                //List<string> l1 = (from q in NRWE.tlkStationStatus
                //                   where q.Valid == true
                //                   select q.Code).ToList<string>();

                var l1 = (from q in NRWE.tlkStationStatus
                          where q.Valid == true
                          select new
                          {
                              q.Description,
                              q.Code,
                          }); 
                foreach(var v in l1)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlStationStatus.Items.Add(LI); 
                }

                //  ddlStationType
                var l2 = (from q in NRWE.tlkStationTypes
                          select new
                          {
                              q.Description,
                              q.Code
                          });
                foreach (var v in l2)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlStationType.Items.Add(LI);
                }

                //ddlStationType.DataSource = l2;
                //ddlStationType.DataBind(); 

                // ddlWaterCode
                List<string> wcList = new List<string>();
                var l3 = (from q in NRWE.tblWatercodes
                          where q.OBSOLETE != true
                          orderby q.WATERNAME
                          select new
                          {
                              q.WATERNAME,
                              q.WATERCODE,
                              q.LOCATION
                          }).AsEnumerable();
                foreach (var x in l3)
                {
                    wcList.Add(x.WATERNAME + " - " + x.LOCATION + " : " + x.WATERCODE);
                }
                ddlWaterCode.DataSource = wcList;
                ddlWaterCode.DataBind();

                // ddlWaterBodyID
                List<string> wbid = new List<string>();
                var l4 = (from q in NRWE.tblWBKeys
                          orderby q.WBID
                          select new
                          {
                              q.WATERSHED,
                              q.SUBBASIN,
                              q.WBID,
                              q.SegDesc
                          }).AsEnumerable();
                foreach (var x in l4)
                {
                    wrkStr = x.SegDesc;
                    int len = wrkStr.Length;
                    if (len > 80)
                        len = 80; // no more than 80 chars
                    wrkStr = wrkStr.Substring(0, len);

                    wbid.Add(x.WBID + " : " + x.WATERSHED + " - " + x.SUBBASIN + " - " + wrkStr);
                }
                ddlWaterBodyID.DataSource = wbid;
                ddlWaterBodyID.DataBind();

                // ddlQUADI
                var l5 = (from q in NRWE.tlkQUADIs
                          select new
                          {
                              q.Description,
                              q.Code
                          });

                foreach (var v in l5)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlQUADI.Items.Add(LI);
                }

                //                   orderby q.Description
                //                   select q.Description).ToList<string>();

                //ddlQUADI.DataSource = l5;
                //ddlQUADI.DataBind();

                // ddlTownship

                var l6 = (from q in NRWE.tlkTownships
                          select new
                          {
                              q.Description,
                              q.Code
                          });

                foreach (var v in l6)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlTownship.Items.Add(LI);
                }
                    
                    
                //    (from q in NRWE.tlkTownships
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlTownship.DataSource = l6;
                //ddlTownship.DataBind();

                // ddlRange

                var l9 = (from q in NRWE.tlkRanges
                          select new
                          {
                              q.Description,
                              q.Code
                          });

                foreach (var v in l9)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlRange.Items.Add(LI);
                }


                //List<string> l9 = (from q in NRWE.tlkRanges
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlRange.DataSource = l9;
                //ddlRange.DataBind();


                // ddlQuarterSection
                var l7 = (from q in NRWE.tlkQuarterSections
                          select new
                          {
                              q.Description,
                              q.Code
                          });

                foreach (var v in l7)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlQuarterSection.Items.Add(LI);
                }

                //List<string> l7 = (from q in NRWE.tlkQuarterSections
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlQuarterSection.DataSource = l7;
                //ddlQuarterSection.DataBind();

                // ddlGrid
                var l8 = (from q in NRWE.tlkGrids
                          select new
                          {
                              q.Description,
                              q.Code
                          });

                foreach (var v in l8)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlGrid.Items.Add(LI);
                }



                var l10 = (from q in NRWE.tlkRiverWatchWaterSheds
                           orderby q.Code
                           select new
                           {
                               q.Code,
                               q.Description
                           });
                foreach (var v in l10)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlRWWaterShed.Items.Add(LI);
                }



                //ddlWQCCWaterShed
                var l11 = (from q in NRWE.tlkWQCCWaterSheds
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });
                foreach (var v in l11)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlWQCCWaterShed.Items.Add(LI);
                }
                //ddlWQCCWaterShed.DataSource = l11;
                //ddlWQCCWaterShed.DataBind();

                //ddlState

                var l12 = (from q in NRWE.tlkStates
                           where q.Description.ToUpper() != "COLORADO"
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });
                foreach (var v in l12)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlState.Items.Add(LI);
                }
                ListItem LI100 = new ListItem("COLORADO", "CO");    // put this at top of list
                ddlState.Items.Insert(0, LI100); 

                //List<string> l12 = (from q in NRWE.tlkStates
                //                    where q.Description.ToUpper() != "COLORADO"
                //                    orderby q.Code
                //                    select q.Code).ToList<string>();
                //ddlState.DataSource = l12;
                //ddlState.DataBind();
                //ddlState.Items.Insert(0, "COLORADO");

                //ddlHydroUnit

                var l13 = (from q in NRWE.tlkHydroUnits
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });
                foreach (var v in l13)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlHydroUnit.Items.Add(LI);
                }


                //List<string> l13 = (from q in NRWE.tlkHydroUnits
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlHydroUnit.DataSource = l13;
                //ddlHydroUnit.DataBind();

                //ddlEcoRegion
                var l14 = (from q in NRWE.tlkEcoRegions
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           }); 

                foreach (var v in l14)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlEcoRegion.Items.Add(LI);
                }

                //ddlEcoRegion.DataSource = l14;
                //ddlEcoRegion.DataBind(); 

                // ddlSection

                var l15 = (from q in NRWE.tlkSection
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });

                foreach (var v in l15)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlSection.Items.Add(LI);
                }
                //List<string> l15 = (from q in NRWE.tlkSections
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlSection.DataSource = l15;
                //ddlSection.DataBind();

                // ddlRange

                var l16 = (from q in NRWE.tlkRanges
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });

                foreach (var v in l16)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlRange.Items.Add(LI);
                }


                //List<string> l16 = (from q in NRWE.tlkRanges
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlRange.DataSource = l16;
                //ddlRange.DataBind();

                //ddlCounty

                var l17 = (from q in NRWE.tlkCounties
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });

                foreach (var v in l17)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlCounty.Items.Add(LI);
                }

                //List<string> l17 = (from q in NRWE.tlkCounties
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlCounty.DataSource = l17;
                //ddlCounty.DataBind();

                //ddlWSR

                var l18 = (from q in NRWE.tlkWSRs
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });

                foreach (var v in l18)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlWSR.Items.Add(LI);
                }

                //List<string> l18 = (from q in NRWE.tlkWSRs
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlWSR.DataSource = l18;
                //ddlWSR.DataBind();

                //ddlRegion

                var l19 = (from q in NRWE.tlkregions
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });

                foreach (var v in l19)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlRegion.Items.Add(LI);
                }

                //List<string> l19 = (from q in NRWE.tlkregions
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlRegion.DataSource = l19;
                //ddlRegion.DataBind();

                //ddlStationQUAD

                var l20 = (from q in NRWE.tlkStationQUADs
                           orderby q.Description
                           select new
                           {
                               q.Description,
                               q.Code
                           });

                foreach (var v in l20)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlStationQUAD.Items.Add(LI);
                }
                //List<string> l20 = (from q in NRWE.tlkStationQUADs
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlStationQUAD.DataSource = l20;
                //ddlStationQUAD.DataBind();

                List<string> l21 = (from q in NRWE.Stations
                                    orderby q.River
                                    where q.River != null
                                    select q.River).Distinct().ToList<string>();
                l21.Sort();
                ddlRiver.DataSource = l21;
                ddlRiver.DataBind();
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Admin Stations");
            }
        }

        [System.Web.Script.Services.ScriptMethod]
        [System.Web.Services.WebMethod]
        public static List<string> SearchStations(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())    // make single instance of these, so we don't have to worry about closing connections
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RiverWatchDev"].ConnectionString;  // GlobalSite.RiverWatchDev;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select StationName from Station where StationName like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                customers.Add(sdr["StationName"].ToString());
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
        public void clearDDLselections()
        {

            ddlCounty.ClearSelection();
            ddlEcoRegion.ClearSelection();
            ddlGrid.ClearSelection();
            ddlHydroUnit.ClearSelection();
            ddlQUADI.ClearSelection();
            ddlQuarterSection.ClearSelection();
            ddlRange.ClearSelection();
            ddlRegion.ClearSelection();
            ddlRiver.ClearSelection();
            ddlRWWaterShed.ClearSelection();
            ddlSection.ClearSelection();
            ddlState.ClearSelection();
            ddlStationQUAD.ClearSelection();
            ddlStationStatus.ClearSelection();
            ddlStationType.ClearSelection();
            ddlTownship.ClearSelection();
            ddlWaterBodyID.ClearSelection();
            ddlWaterCode.ClearSelection();
            ddlWQCCWaterShed.ClearSelection();
            ddlWSR.ClearSelection();

        }
    }
}