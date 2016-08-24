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
using RWInbound2.App_Code;

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
                RiverWatchEntities NRWE = new RiverWatchEntities();
                string wrkStr = "";
              // ddlStationStatus
                List<string> l1 = (from q in NRWE.tlkStationStatus
                                   where q.Valid == true
                                   select q.Description).ToList<string>();
                ddlStationStatus.DataSource = l1;
                ddlStationStatus.DataBind(); 

              //  ddlStationType
                List<string> l2 = (from q in NRWE.tlkStationTypes
                                   select q.Description).ToList<string>();
                ddlStationType.DataSource = l2;
                ddlStationType.DataBind(); 

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
                    foreach(var x in l3)
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
                foreach(var x in l4)
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
                List<string> l5 = (from q in NRWE.tlkQUADIs
                                  orderby q.Description
                                  select q.Description).ToList<string>();

                ddlQUADI.DataSource = l5;
                ddlQUADI.DataBind(); 

                // ddlTownship

                List<string> l6 = (from q in NRWE.tlkTownships
                                   orderby q.Description
                                   select q.Description).ToList<string>();
                ddlTownship.DataSource = l6;
                ddlTownship.DataBind();

                // ddlRange
                List<string> l9 = (from q in NRWE.tlkRanges
                                   orderby q.Description
                                   select q.Description).ToList<string>();
                ddlRange.DataSource = l9;
                ddlRange.DataBind(); 
               
                // ddlQuarterSection
                List<string> l7 = (from q in NRWE.tlkQuarterSections
                                   orderby q.Description
                                   select q.Description).ToList<string>();
                ddlQuarterSection.DataSource = l7;
                ddlQuarterSection.DataBind(); 

                // ddlGrid
                List<string> l8 = (from q in NRWE.tlkGrids
                                   orderby q.Description
                                   select q.Description).ToList<string>();
                ddlGrid.DataSource = l8;
                ddlGrid.DataBind();

                // ddlRWWaterShed

                List<string> l10 = (from q in NRWE.tlkRiverWatchWaterSheds
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlRWWaterShed.DataSource = l10;
                ddlRWWaterShed.DataBind(); 

                //ddlWQCCWaterShed
                List<string> l11 = (from q in NRWE.tlkWQCCWaterSheds
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlWQCCWaterShed.DataSource = l11;
                ddlWQCCWaterShed.DataBind(); 

                //ddlState
                List<string> l12 = (from q in NRWE.tlkStates
                                    where q.Description != "Colorado"
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlState.DataSource = l12;
                ddlState.DataBind();
                ddlState.Items.Insert(0, "Colorado"); 

                //ddlHydroUnit
                List<string> l13 = (from q in NRWE.tlkHydroUnits
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlHydroUnit.DataSource = l13;
                ddlHydroUnit.DataBind(); 

                //ddlEcoRegion
                List<string> l14 = (from q in NRWE.tlkEcoRegions
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlEcoRegion.DataSource = l14;
                ddlEcoRegion.DataBind(); 

                // ddlSection
                List<string> l15 = (from q in NRWE.tlkSections
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlSection.DataSource = l15;
                ddlSection.DataBind();

                // ddlRange
                List<string> l16 = (from q in NRWE.tlkRanges
                                orderby q.Description
                            select q.Description).ToList<string>();
                ddlRange.DataSource = l16;
                ddlRange.DataBind(); 

                //ddlCounty
                List<string> l17 = (from q in NRWE.tlkCounties
                                        orderby q.Description
                                    select q.Description).ToList<string>();
                ddlCounty.DataSource = l17;
                ddlCounty.DataBind();

                //ddlWSR
                List<string> l18 = (from q in NRWE.tlkWSRs
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlWSR.DataSource = l18;
                ddlWSR.DataBind(); 

                //ddlRegion
                List<string> l19 = (from q in NRWE.tlkregions
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlRegion.DataSource = l19;
                ddlRegion.DataBind();

                //ddlStationQUAD
                List<string> l20 = (from q in NRWE.tlkStationQUADs
                                    orderby q.Description
                                    select q.Description).ToList<string>();
                ddlStationQUAD.DataSource = l20;
                ddlStationQUAD.DataBind();

                List<string> l21 = (from q in NRWE.Stations
                                    orderby q.River
                                    where q.River != null
                                    select q.River).Distinct().ToList<string>();
                l21.Sort(); 
                ddlRiver.DataSource = l21;
                ddlRiver.DataBind(); 
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
                    conn.ConnectionString = GlobalSite.RiverWatchConnectionString;
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
        }

        protected void btnSelectStnName_Click(object sender, EventArgs e)
        {
            string stationName = tbStationName.Text.Trim();
            Session["NEW"] = false;
            if(stationName.Length < 2)
            {
                lblStatus.Text = "Please select a valid station name";
                tbStationName.Text = "";
                return;
            }

            RiverWatchEntities NRWE = new RiverWatchEntities();
            try
            {
                var R = (from q in NRWE.Stations
                         where q.StationName == stationName
                         select q.StationNumber).FirstOrDefault();
                if(R == null)
                {
                    lblStatus.Text = string.Format("Station Name {0} not found", stationName);
                    tbStationName.Text = "";
                    tbStnNumber.Text = "";
                    return;      
                }

                populateDDLs(R.Value);
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

                }
                else
                {
                    tbStnNumber.Text = "";
                    lblStatus.Text = "Please select a valid station number";
                    return;
                }
                populateDDLs(stnNumber);
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

       

        public void populateDDLs(int stnNumber)
        {
            RiverWatchEntities NRWE = new RiverWatchEntities();
            int listCount = 0; 
           
            try
            {
                var SN = (from q in NRWE.Stations
                          where q.StationNumber == stnNumber
                          select q).FirstOrDefault();    // get the data
                if (SN == null)
                    return;

                resetControls(); // make them all default state so if values below do not match the selected value, they are at default values

                txtStationName.Text = SN.StationName;
                txtStationNumber.Text = SN.StationNumber.ToString();
                txtriver.Text = SN.River ?? "";
                ddlRiver.SelectedValue = SN.River; 
                ddlCounty.SelectedValue = SN.County ?? "";

                ddlWaterCode.Items.Insert(0,SN.WaterCode);
                ddlEcoRegion.SelectedValue = SN.EcoRegion ?? "";
                ddlGrid.SelectedValue = SN.Grid;
                ddlHydroUnit.SelectedValue = SN.HydroUnit;
                ddlQUADI.SelectedValue = SN.QUADI;
                ddlQuarterSection.SelectedValue = SN.QuaterSection;
                ddlRange.SelectedValue = SN.Range;
                ddlRegion.SelectedValue = SN.Region;
                ddlRiver.SelectedValue = SN.River;
                ddlRWWaterShed.SelectedValue = SN.RWWaterShed;
                ddlSection.SelectedValue = SN.Section.ToString();
                ddlState.SelectedValue = SN.State; 
                ddlStationQUAD.SelectedValue = SN.StationQUAD;
                ddlStationStatus.SelectedValue = SN.StationStatus;
                ddlStationType.SelectedValue = SN.StationType;
                ddlTownship.SelectedValue = SN.Township;

                
            //    ddlWaterBodyID.SelectedValue = SN.WaterBodyID;
                listCount = ddlWaterBodyID.Items.Count;
                for (int z = 0; z < listCount; z++)
                {
                    if (ddlWaterBodyID.Items[z].Value.Contains(SN.WaterBodyID))
                    {
                        ddlWaterBodyID.Items[z].Selected = true;
                    }
                }    
           
                listCount = ddlWaterCode.Items.Count;
                for (int z = 0; z < listCount; z++ )
                {
                    if(ddlWaterCode.Items[z].Value.Contains(SN.WaterCode))
                    {
                        ddlWaterCode.Items[z].Selected = true;
                    }
                }                 

                ddlWQCCWaterShed.SelectedValue = SN.WQCCWaterShed;
                ddlWSR.SelectedValue = SN.WaterShedRegion;
                cbStateEngineering.Checked = SN.StateEngineering.Value;
                cbUSGS.Checked = SN.USGS.Value ; 
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

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {

            if (Session["NEW"] == null)
                Response.Redirect("~/timedout.aspx");

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
            if (!success)
            {
                tbStnNumber.Text = "";
                lblStatus.Text = "Please select a valid station number";
            }

            // do some validation here.. .
            tmpString = tbLatitude.Text;
            if(tmpString.Length < 4)
            {
                lblStatus.Text = "Please enter a valid Latitude";
                tbLatitude.Focus();
                return; 
            }

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

                    if (STN != null)
                    {
                        tbStnNumber.Text = "";
                        lblStatus.Text = string.Format("Station Number {0} is used!", stnNumber);
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

                STN.StationName = txtStationName.Text;
                STN.River = txtriver.Text;
                STN.County = ddlCounty.SelectedValue;
                STN.EcoRegion = ddlEcoRegion.SelectedValue;
                STN.Grid = ddlGrid.SelectedValue;
                STN.HydroUnit = ddlHydroUnit.SelectedValue; 
                STN.QUADI = ddlQUADI.SelectedValue ;
                STN.QuaterSection = ddlQuarterSection.SelectedValue ;
                STN.Range = ddlRange.SelectedValue ;
                STN.Region = ddlRegion.SelectedValue ;
                STN.River = ddlRiver.SelectedValue;
                STN.RWWaterShed = ddlRWWaterShed.SelectedValue ;

                if (int.TryParse(ddlSection.SelectedValue, out tempInt))    // XXXX should change this in the data base to string
                    STN.Section = tempInt;

                STN.State = ddlState.SelectedValue;
                STN.StationQUAD = ddlStationQUAD.SelectedValue;
                STN.StationStatus = ddlStationStatus.SelectedValue;
                STN.StationType = ddlStationType.SelectedValue;
                STN.Township = ddlTownship.SelectedValue;

         //       wbid.Add(x.WBID + " : " + x.WATERSHED + " - " + x.SUBBASIN + " - " + wrkStr); 

                tmpString = ddlWaterBodyID.SelectedValue;
                idx = tmpString.IndexOf(":"); 
                if(idx != 0)
                {
                    tmpString = tmpString.Substring(0, idx - 1); 
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
                    tmpString = tmpString.Substring(idx + 1, len);
                    STN.WaterCode = tmpString.Trim(); // ddlWaterCode.SelectedValue;
                }

                STN.WQCCWaterShed = ddlWQCCWaterShed.SelectedValue;
                STN.WaterShedRegion = ddlWSR.SelectedValue;
                STN.StateEngineering = cbStateEngineering.Checked;
                STN.USGS = cbUSGS.Checked;
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

    }
}