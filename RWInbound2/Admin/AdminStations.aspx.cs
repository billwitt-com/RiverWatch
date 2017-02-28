using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
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
                RiverWatchEntities RWE = new RiverWatchEntities();
                Session["NEW"] = false; 
                lblStatus.Visible = false;
            //    pnlTable.Visible = false; // hide for now     
           //     loadDropDownLists(false);
                lblNextStnNumber.Visible = false;
                btnOK.Visible = false;
                pnlTable.Visible = false;

                // new 12/15/'16 bwitt added lookup table to make water code list smaller when renered as ddl 
                var A1 = from a in RWE.WaterCodeDrainages
                         where a.Valid == true
                         select new
                         {
                             a.Description,
                             a.Code,
                         };

                foreach (var a in A1)
                {
                    ListItem LI = new ListItem(a.Description, a.Code);
                    ddlDrainage.Items.Add(LI);
                }
                ddlDrainage.Items.Insert(0, "Choose One"); 
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
            tbdescription.Text = "";
            txtriver.Text = "";
            tbAquaticModelIndex.Text = "";
            tbComment.Text = "";
            tbElevation.Text = "";
            tbLatitude.Text = "";
            tbLongtitude.Text = "";
            tbMove.Text = "";
            tbNearCity.Text = "";
            tbUTMX.Text = "";
            tbUTMY.Text = "";
            lblNextStnNumber.Visible = false; 

        }
        protected void btnNewStation_Click(object sender, EventArgs e)
        {
            // reset the drop down controls and clear text boxes
            pnlInput.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false; 
            resetControls(); 
            txtStationName.Focus();
            txtStationName.BackColor = System.Drawing.Color.Bisque;
            Session["NEW"] = true;
            tbStationName.Focus();
            int nextStnNumber = 0;
            int last = 0;

            string drainage = "";
            if (ddlDrainage.SelectedIndex == 0) // no choice yet
            {
                lblStatus.Text = "Please select a Drainage!";
                lblStatus.Visible = true;
                ddlDrainage.Focus();
                return;
            }
            drainage = ddlDrainage.SelectedValue;
            Session["DRAINAGE"] = drainage;

            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();

                //int LKN = (int)(from c in RWE.organizations
                //                select c.KitNumber).Max();

                //lblKitNumber.Text = LKN.ToString();

                IEnumerable<int> L = (from c in RWE.Stations
                                      orderby c.StationNumber 
                                     select                                     
                                      c.StationNumber.Value
                                      ) ; 
               
                foreach (int i in L)
                {
                    if (i - last > 1)    // is the next number a jump in sequence?
                    {
                        nextStnNumber = i - 1;  // go back to the one that was not there
                        break;
                    }
                    last = i;
                }

                lblNextStnNumber.Text = nextStnNumber.ToString();
                lblNextStnNumber.Visible = true;
                //lblLastUsedText.Visible = true;
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

            pnlTable.Visible = true;          
            
            loadDropDownLists(true);    // mark true to add null to the top of all lists
        }

        protected void btnSelectStnName_Click(object sender, EventArgs e)
        {

            string stationName = tbStationName.Text.Trim();
            Session["NEW"] = false;
            lblStatus.Visible = false; // hide until needed
            
            string drainage = "";
            if (ddlDrainage.SelectedIndex == 0) // no choice yet
            {
                lblStatus.Text = "Please select a Drainage!";
                lblStatus.Visible = true;
                ddlDrainage.Focus();
                return; 
            }
            drainage = ddlDrainage.SelectedValue;
            Session["DRAINAGE"] = drainage;

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
                pnlTable.Visible = true; 
                populatePage(R.Value);
                loadDropDownLists(false);
                
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
            lblStatus.Visible = false; // hide if not needed

            string drainage = "";
            if (ddlDrainage.SelectedIndex == 0) // no choice yet
            {
                lblStatus.Text = "Please select a Drainage!";
                lblStatus.Visible = true;
                ddlDrainage.Focus();
                return;
            }
            drainage = ddlDrainage.SelectedValue;
            Session["DRAINAGE"] = drainage;

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

              pnlTable.Visible = true;
              populatePage(stnNumber);
              loadDropDownLists(false); 
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
         //   loadDropDownLists();
           // clearDDLselections(); 
            RiverWatchEntities NRWE = new RiverWatchEntities();
            int listCount = 0;
            int z = 0; 
           
               var SN = (from q in NRWE.Stations
                          where q.StationNumber == stnNumber
                          select q).FirstOrDefault();    // get the data
                if (SN == null)
                    return;
            try
            {            

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

                tbComment.Text = SN.Comments ?? ""; 
            }
             
            catch (Exception ex)
            {
                string msg = ex.Message;                
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Populating Text Boxes");
            }

                // do ddls
            try
            {
                if (SN.WaterCode != null)
                {
                    if (SN.WaterCode.Length > 0)
                    {
                        listCount = ddlWaterCode.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlWaterCode.Items[z].Value.Contains(": " + SN.WaterCode))
                            {
                                ddlWaterCode.SelectedIndex = z;
                                //    ddlWaterCode.Items[z].Selected = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlWaterCode.Items.Insert(0, LI);
                    ddlWaterCode.SelectedIndex = 0;
                    ddlWaterCode.BackColor = Color.Yellow;
                    ddlWaterCode.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.River != null)
                {
                    if (SN.River.Length > 0)
                    {
                        listCount = ddlRiver.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlRiver.Items[z].Value.Contains(SN.River))
                            {
                                ddlRiver.SelectedIndex = z;
                                ddlRiver.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlRiver.BackColor = Color.Yellow;
                                ddlRiver.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlRiver.Items.Insert(0, LI);
                    ddlRiver.SelectedIndex = 0;
                    ddlRiver.BackColor = Color.Yellow;
                    ddlRiver.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.StationType != null)
                {
                    if (SN.StationType.Length > 0)
                    {
                        listCount = ddlStationType.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            // if (ddlStationType.Items[z].Value.Contains(SN.StationType))

                                if (ddlStationType.Items[z].Value.ToUpper().Equals(SN.StationType.ToUpper()))

                            {
                                ddlStationType.SelectedIndex = z;
                                ddlStationType.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlStationType.BackColor = Color.Yellow;
                                ddlStationType.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlStationType.Items.Insert(0, LI);
                    ddlStationType.SelectedIndex = 0;
                    ddlStationType.BackColor = Color.Yellow;
                    ddlStationType.ToolTip = "Invalid or Empty  value in data base";
                }
                      //  ddlStationType.SelectedItem.Value = SN.StationType;


                if (SN.StationStatus != null)
                {
                    if (SN.StationStatus.Length > 0)
                        listCount = ddlStationStatus.Items.Count;
                    for (z = 0; z < listCount; z++)
                    {
                        if (ddlStationStatus.Items[z].Value.Contains(SN.StationStatus))
                        {
                            ddlStationStatus.SelectedIndex = z;
                            ddlStationStatus.BackColor = Color.White;
                            break;
                        }
                        else
                        {
                            ddlStationStatus.BackColor = Color.Yellow;
                            ddlStationStatus.ToolTip = "Invalid or Empty  value in data base"; 
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlStationStatus.Items.Insert(0, LI);
                    ddlStationStatus.SelectedIndex = 0;
                    ddlStationStatus.BackColor = Color.Yellow;
                    ddlStationStatus.ToolTip = "Invalid or Empty  value in data base";
                }
                      //  ddlStationStatus.SelectedValue = SN.StationStatus;

            } 
            catch (Exception ex)
            {
                string msg = ex.Message;                
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Populating DDLs Line 336");
            }
            try
            {
                if (SN.WaterBodyID
                    != null)
                {
                    if (SN.WaterBodyID.Length > 0)
                    {
                        listCount = ddlWaterBodyID.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlWaterBodyID.Items[z].Value.Contains(SN.WaterBodyID))
                            {
                                ddlWaterBodyID.SelectedIndex = z;
                                //  ddlWaterBodyID.Items[z].Selected = true;
                                ddlWaterBodyID.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlWaterBodyID.BackColor = Color.Yellow;
                                ddlWaterBodyID.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    } 
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlWaterBodyID.Items.Insert(0, LI);
                    ddlWaterBodyID.SelectedIndex = 0;
                    ddlWaterBodyID.BackColor = Color.Yellow;
                    ddlWaterBodyID.ToolTip = "Invalid or Empty  value in data base";
                }

            }            
    
            catch (Exception ex)
            {
                string msg = ex.Message;                
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Populating Drop Downs L. 376");
            }
            try{
                if (SN.QUADI != null)
                {
                    if (SN.QUADI.Length > 0)
                    {
                        listCount = ddlQUADI.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlQUADI.Items[z].Value.Contains(SN.QUADI))
                            {
                                ddlQUADI.SelectedIndex = z;
                                ddlQUADI.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlQUADI.BackColor = Color.Yellow;
                                ddlQUADI.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlQUADI.Items.Insert(0, LI);
                    ddlQUADI.SelectedIndex = 0;
                    ddlQUADI.BackColor = Color.Yellow;
                    ddlQUADI.ToolTip = "Invalid or Empty  value in data base";
                }
                       // ddlQUADI.SelectedValue = SN.QUADI;

                if (SN.Township != null)
                {
                    if (SN.Township.Length > 0)
                    {
                        listCount = ddlTownship.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlTownship.Items[z].Value.Contains(SN.Township))
                            {
                                ddlTownship.SelectedIndex = z;
                                ddlTownship.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlTownship.BackColor = Color.Yellow;
                                ddlTownship.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlTownship.Items.Insert(0, LI);
                    ddlTownship.SelectedIndex = 0;
                    ddlTownship.BackColor = Color.Yellow;
                    ddlTownship.ToolTip = "Invalid or Empty  value in data base";
                }
                
                //if (SN.Township != null)
                //    if (SN.Township.Length > 0)
                //        ddlTownship.SelectedValue = SN.Township;


                if (SN.Range != null)
                {
                    if (SN.Range.Length > 0)
                    {
                        listCount = ddlRange.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlRange.Items[z].Value.Contains(SN.Range))
                            {
                                ddlRange.SelectedIndex = z;
                                ddlRange.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlRange.BackColor = Color.Yellow;
                                ddlRange.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlRange.Items.Insert(0, LI);
                    ddlRange.SelectedIndex = 0;
                    ddlRange.BackColor = Color.Yellow;
                    ddlRange.ToolTip = "Invalid or Empty  value in data base";
                }

                //if (SN.Range != null)
                //    if (SN.Range.Length > 0)
                //        ddlRange.SelectedValue = SN.Range;


                if (SN.Section != null)
                {
                    if (SN.Section >= 0)
                    {
                        listCount = ddlSection.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlSection.Items[z].Value.Contains(SN.Section.ToString()))
                            {
                                ddlSection.SelectedIndex = z;
                                ddlSection.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlSection.BackColor = Color.Yellow;
                                ddlSection.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlSection.Items.Insert(0, LI);
                    ddlSection.SelectedIndex = 0;
                    ddlSection.BackColor = Color.Yellow;
                    ddlSection.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.QuaterSection != null)
                {
                    if (SN.QuaterSection.Length > 0)
                    {
                        listCount = ddlQuarterSection.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlQuarterSection.Items[z].Value.Contains(SN.QuaterSection))
                            {
                                ddlQuarterSection.SelectedIndex = z;
                                ddlQuarterSection.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlQuarterSection.BackColor = Color.Yellow;
                                ddlQuarterSection.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlQuarterSection.Items.Insert(0, LI);
                    ddlQuarterSection.SelectedIndex = 0;
                    ddlQuarterSection.BackColor = Color.Yellow;
                    ddlQuarterSection.ToolTip = "Invalid or Empty  value in data base";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;                
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Populating Drop Downs L. 517");
            }

            try
            {
                //if (SN.QuaterSection != null)
                //    if (SN.QuaterSection.Length > 0)
                //        ddlQuarterSection.SelectedValue = SN.QuaterSection;

                if (SN.Grid != null)
                {
                    if (SN.Grid.Length > 0)
                    {
                        listCount = ddlGrid.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlGrid.Items[z].Value.Contains(SN.Grid))
                            {
                                ddlGrid.SelectedIndex = z;
                                ddlGrid.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlGrid.BackColor = Color.Yellow;
                                ddlGrid.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlGrid.Items.Insert(0, LI);
                    ddlGrid.SelectedIndex = 0;
                    ddlGrid.BackColor = Color.Yellow;
                    ddlGrid.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.StationQUAD != null)
                {
                    if (SN.StationQUAD.Length > 0)
                    {
                        listCount = ddlStationQUAD.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlStationQUAD.Items[z].Value.Contains(SN.StationQUAD))
                            {
                                ddlStationQUAD.SelectedIndex = z;
                                ddlStationQUAD.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlStationQUAD.BackColor = Color.Yellow;
                                ddlStationQUAD.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlStationQUAD.Items.Insert(0, LI);
                    ddlStationQUAD.SelectedIndex = 0;
                    ddlStationQUAD.BackColor = Color.Yellow;
                    ddlStationQUAD.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.RWWaterShed != null)
                {
                    if (SN.RWWaterShed.Length > 0)
                    {
                        listCount = ddlRWWaterShed.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlRWWaterShed.Items[z].Value.Contains(SN.RWWaterShed))
                            {
                                ddlRWWaterShed.SelectedIndex = z;
                                ddlRWWaterShed.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlRWWaterShed.BackColor = Color.Yellow;
                                ddlRWWaterShed.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlRWWaterShed.Items.Insert(0, LI);
                    ddlRWWaterShed.SelectedIndex = 0;
                    ddlRWWaterShed.BackColor = Color.Yellow;
                    ddlRWWaterShed.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.WQCCWaterShed != null)
                {
                    if (SN.WQCCWaterShed.Length > 0)
                    {
                        listCount = ddlWQCCWaterShed.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlWQCCWaterShed.Items[z].Value.Contains(SN.WQCCWaterShed))
                            {
                                ddlWQCCWaterShed.SelectedIndex = z;
                                ddlWQCCWaterShed.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlWQCCWaterShed.BackColor = Color.Yellow;
                                ddlWQCCWaterShed.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlWQCCWaterShed.Items.Insert(0, LI);
                    ddlWQCCWaterShed.SelectedIndex = 0;
                    ddlWQCCWaterShed.BackColor = Color.Yellow;
                    ddlWQCCWaterShed.ToolTip = "Invalid or Empty  value in data base";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;                
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Populating Drop Downs L. 639");
            }
                //if (SN.WQCCWaterShed != null)
                //    if (SN.WQCCWaterShed.Length > 0)
                //        ddlWQCCWaterShed.SelectedValue = SN.WQCCWaterShed;
            try
            {
                if (SN.HydroUnit != null)
                {
                    if (SN.HydroUnit.Length > 0)
                    {
                        listCount = ddlHydroUnit.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlHydroUnit.Items[z].Value.Contains(SN.HydroUnit))
                            {
                                ddlHydroUnit.SelectedIndex = z;
                                ddlHydroUnit.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlHydroUnit.BackColor = Color.Yellow;
                                ddlHydroUnit.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlHydroUnit.Items.Insert(0, LI);
                    ddlHydroUnit.SelectedIndex = 0;
                    ddlHydroUnit.BackColor = Color.Yellow;
                    ddlHydroUnit.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.EcoRegion != null)
                {
                    if (SN.EcoRegion.Length > 0)
                    {
                        listCount = ddlEcoRegion.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlEcoRegion.Items[z].Value.Contains(SN.EcoRegion))
                            {
                                ddlEcoRegion.SelectedIndex = z;
                                ddlEcoRegion.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlEcoRegion.BackColor = Color.Yellow;
                                ddlEcoRegion.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlEcoRegion.Items.Insert(0, LI);
                    ddlEcoRegion.SelectedIndex = 0;
                    ddlEcoRegion.BackColor = Color.Yellow;
                    ddlEcoRegion.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.WaterShedRegion != null)
                {
                    if (SN.WaterShedRegion.Length > 0)
                    {
                        listCount = ddlWSR.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlWSR.Items[z].Value.Contains(SN.WaterShedRegion))
                            {
                                ddlWSR.SelectedIndex = z;
                                ddlWSR.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlWSR.BackColor = Color.Yellow;
                                ddlWSR.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlWSR.Items.Insert(0, LI);
                    ddlWSR.SelectedIndex = 0;
                    ddlWSR.BackColor = Color.Yellow;
                    ddlWSR.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.County != null)
                {
                    if (SN.County.Length > 0)
                    {
                        listCount = ddlCounty.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlCounty.Items[z].Value.Contains(SN.County))
                            {
                                ddlCounty.SelectedIndex = z;
                                ddlCounty.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlCounty.BackColor = Color.Yellow;
                                ddlCounty.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlCounty.Items.Insert(0, LI);
                    ddlCounty.SelectedIndex = 0;
                    ddlCounty.BackColor = Color.Yellow;
                    ddlCounty.ToolTip = "Invalid or Empty  value in data base";
                }

                if (SN.State != null)
                {
                    if (SN.State.Length > 0)
                    {
                        listCount = ddlState.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlState.Items[z].Value.Contains(SN.State))
                            {
                                ddlState.SelectedIndex = z;
                                ddlState.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlState.BackColor = Color.Yellow;
                                ddlState.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlState.Items.Insert(0, LI);
                    ddlState.SelectedIndex = 0;
                    ddlState.BackColor = Color.Yellow;
                    ddlState.ToolTip = "Invalid or Empty  value in data base";
                }
                //if(SN.State != null)
                //    if(SN.State.Length > 0)
                //        ddlState.SelectedValue = SN.State;

                if (SN.Region != null)
                {
                    if (SN.Region.Length > 0)
                    {
                        listCount = ddlRegion.Items.Count;
                        for (z = 0; z < listCount; z++)
                        {
                            if (ddlRegion.Items[z].Value.Contains(SN.Region))
                            {
                                ddlRegion.SelectedIndex = z;
                                ddlRegion.BackColor = Color.White;
                                break;
                            }
                            else
                            {
                                ddlRegion.BackColor = Color.Yellow;
                                ddlRegion.ToolTip = "Invalid or Empty  value in data base"; 
                            }
                        }
                    }
                }
                else
                {
                    ListItem LI = new ListItem("null");
                    ddlRegion.Items.Insert(0, LI);
                    ddlRegion.SelectedIndex = 0; 
                    ddlRegion.BackColor = Color.Yellow;
                    ddlRegion.ToolTip = "Invalid or Empty  value in data base"; 
                }
                //if(SN.Region != null)
                //    if(SN.Region.Length > 0)
                //        ddlRegion.SelectedValue = SN.Region;

                ////////ddlWaterCode.Items.Insert(0,SN.WaterCode);
                /////////  ddlEcoRegion.SelectedItem.Value = SN.EcoRegion; // ?? "";

                ///////////////////                     ddlRWWaterShed.SelectedItem.Value = SN.RWWaterShed;
            

            }
            catch (Exception ex)
            {
                string msg = ex.Message;                
                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;
              //  string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Populating Drop Downs L. 815");
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string stationName = "";

            if (Session["NEW"] == null)
                Response.Redirect("~/timedout.aspx");
            
            lblStatus.Visible = false;

            bool isNewStation = false;
            bool successlat = false;
            int stnNumber = 0;
            string stnNumString = "";
            isNewStation = (bool)Session["NEW"];
            int tempInt = 0;
            string tmpString = "";
            int idx = 0;
            int savedChanges = 0;
            double lon = 0;
            double lat = 0;
            bool successlon = false;
            bool success = false;
            double elevation = 0;
            double utmx = 0;
            double utmy = 0;


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
            if (tmpString.Length < 2)
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

            successlon = double.TryParse(tbLongtitude.Text, out lon);
            if (!successlon)
            {
                lblStatus.Text = "Please enter a valid Longtitude";
                lblStatus.Visible = true;
                tbLatitude.Focus();
                return;
            }

            successlat = double.TryParse(tbLatitude.Text, out lat);
            if (!successlat)
            {
                lblStatus.Text = "Please enter a valid Latitude";
                lblStatus.Visible = true;
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
                // see if we have a valid lat long

                if (successlon)
                    STN.Longtitude = lon;
                if (successlat)
                    STN.Latitude = lat;

                success = double.TryParse(tbUTMX.Text.Trim(), out utmx);
                if (success)
                {
                    STN.UTMX = utmx;
                }
                else
                    STN.UTMX = null; 


                success = double.TryParse(tbUTMY.Text.Trim(), out utmy); 
                if (success)
                {
                    STN.UTMY = utmy;
                }
                else
                    STN.UTMY = null; 


                string tmpText = tbElevation.Text.Trim();
                success = double.TryParse(tmpText, out elevation);
                if (success)
                    STN.Elevation = elevation;
                else
                    STN.Elevation = null;

                STN.StationName = stationName;  //txtStationName.Text;
                STN.River = txtriver.Text;
                if (ddlCounty.SelectedValue != "null")
                    STN.County = ddlCounty.SelectedValue;
                if (ddlEcoRegion.SelectedItem.Value != "null")
                    STN.EcoRegion = ddlEcoRegion.SelectedItem.Value;                 //.SelectedValue;
                if (ddlGrid.SelectedValue != "null")
                    STN.Grid = ddlGrid.SelectedValue;
                if (ddlHydroUnit.SelectedValue != "null")
                    STN.HydroUnit = ddlHydroUnit.SelectedValue;
                if (ddlQUADI.SelectedValue != "null")
                    STN.QUADI = ddlQUADI.SelectedValue;
                if (ddlQuarterSection.SelectedValue != "null")
                    STN.QuaterSection = ddlQuarterSection.SelectedValue;
                if (ddlRange.SelectedValue != "null")
                    STN.Range = ddlRange.SelectedValue;
                if (ddlRegion.SelectedValue != "null")
                    STN.Region = ddlRegion.SelectedValue;
                STN.River = txtriver.Text.Trim();    // ddlRiver.SelectedValue;
                STN.NearCity = tbNearCity.Text.Trim();
                STN.Move = tbMove.Text.Trim();
                STN.Description = tbdescription.Text.Trim();
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Updating Stations L 987");
                //lblStatus.Text = string.Format("Data Not Saved");
                //lblStatus.Visible = true;
                return;
            }

            try
            {
                if (ddlRWWaterShed.SelectedItem.Value != "null")    // skip if the word null is here
                {
                    STN.RWWaterShed = ddlRWWaterShed.SelectedItem.Value.Trim().Substring(0, 2); 
                }

              //  STN.RWWaterShed = ddlRWWaterShed.SelectedItem.Value;                //.SelectedValue ;
                if (ddlRWWaterShed.SelectedItem.Value != "null")
                    STN.WQCCWaterShed = ddlRWWaterShed.SelectedItem.Value;

                if (int.TryParse(ddlSection.SelectedValue, out tempInt))    // XXXX should change this in the data base to string
                    STN.Section = tempInt;

                STN.State = ddlState.SelectedValue.Trim().Substring(0, 2);  // we are OK here as this value is 'forced' 

                if (ddlStationQUAD.SelectedValue != "null")
                    STN.StationQUAD = ddlStationQUAD.SelectedValue;
                if (ddlStationStatus.SelectedValue != "null")
                    STN.StationStatus = ddlStationStatus.SelectedValue;
                if (ddlStationType.SelectedItem.Value != "null")
                    STN.StationType = ddlStationType.SelectedItem.Value; // ddlStationType.SelectedValue;

                if (ddlTownship.SelectedValue != "null")
                    STN.Township = ddlTownship.SelectedValue;
                STN.AquaticModelIndex = tbAquaticModelIndex.Text.Trim();

                // could be a value here, or if no value, should be "null" from loading ddl
                tmpString = ddlWaterBodyID.SelectedValue; // wbid.Add(x.WBID + " : " + x.WATERSHED + " - " + x.SUBBASIN + " - " + wrkStr); 
                if (tmpString.Length > 5)
                {
                    idx = tmpString.IndexOf(":");
                    if (idx != 0)
                    {
                        tmpString = tmpString.Substring(0, idx - 1);
                    }
                    STN.WaterBodyID = tmpString.Trim(); // ddlWaterBodyID.SelectedValue;
                }

                idx = 0;
                tmpString = ""; // just in case... 

                tmpString = ddlWaterCode.SelectedValue; // wcList.Add(x.WATERNAME + " - " + x.LOCATION + " : " + x.WATERCODE); 
                if (tmpString.Length > 5)
                {
                    int len = tmpString.Length;
                    idx = tmpString.IndexOf(":");
                    if (idx != 0)
                    {
                        tmpString = tmpString.Substring(idx + 1);              //(idx + 1, len - idx);
                        STN.WaterCode = tmpString.Trim(); // ddlWaterCode.SelectedValue;
                    }
                }

                if (ddlWQCCWaterShed.SelectedItem.Value != "null")
                    STN.WQCCWaterShed = ddlWQCCWaterShed.SelectedItem.Value;                //.SelectedValue;
                if (ddlWSR.SelectedValue != "null")
                    STN.WaterShedRegion = ddlWSR.SelectedValue;
                STN.StateEngineering = cbStateEngineering.Checked;
                STN.USGS = cbUSGS.Checked;
                STN.Comments = tbComment.Text.Trim();

                string nam = "";
                if (User.Identity.Name.Length < 3)
                    nam = "Not logged in";
                else
                    nam = User.Identity.Name;

                STN.UserCreated = nam;
                STN.DateCreated = DateTime.Now;
                lblStatus.Visible = false;
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Updating Stations L 1055");
                //lblStatus.Text = string.Format("Data Not Saved");
                //lblStatus.Visible = true;
                return;
            }
            try
            {
                if (isNewStation)
                {
                    try
                    {
                        NRWE.Stations.Add(STN);
                        // add station to projects 
                        ProjectStation PS = new ProjectStation();
                        PS.ProjectID = 1;
                        PS.StationNumber = STN.StationNumber;
                        PS.DateCreated = DateTime.Now;
                        PS.UserCreated = User.Identity.Name;
                        PS.Valid = true;
                        NRWE.ProjectStations.Add(PS);
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
                        LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Updating Stations L. 1085");

                        return;
                    }
                    //  NRWE.SaveChanges();
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Updating Stations L. 1108");
                lblStatus.Text = string.Format("Data Not Saved");
                lblStatus.Visible = true;
                return;
            }

            try
            {
                savedChanges = NRWE.SaveChanges();
                lblStatus.Text = string.Format("Station {0} saved", stationName);
                lblStatus.Visible = true;
                btnOK.Visible = true;
                pnlTable.Visible = false;
                pnlInput.Visible = false;
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Updating Stations L. 1126");
                lblStatus.Text = string.Format("Data Not Saved");
                lblStatus.Visible = true;
                return;
            }
        }
        // not currently used
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
        public void loadDropDownLists(bool isNewStation)
        {
            RiverWatchEntities NRWE = new RiverWatchEntities();
            string wrkStr = "";
            clearDDLselections(); // just in case
            try
            {
                // ddlStationStatus
                //List<string> l1 = (from q in NRWE.tlkStationStatus
                //                   where q.Valid == true
                //                   select q.Code).ToList<string>();


                ddlStationStatus.Items.Clear(); 
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
                if(isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlStationStatus.Items.Insert(0, LI); 
                }

                //  ddlStationType
                ddlStationType.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlStationType.Items.Insert(0, LI);
                }
                //ddlStationType.DataSource = l2;
                //ddlStationType.DataBind(); 

                // ddlWaterCode

                //DateTime ST = DateTime.Now; 
                //LogError LE1 = new LogError();
                //string msg1 = string.Format("Started loading ddlWaterCode at {0} ", DateTime.Now);
                //LE1.logError(msg1, "Loading Dlls", "", "DEV", "Profiling");

             //   Session["DRAINAGE"] = drainage;
                if (Session["DRAINAGE"] == null)
                {
                    Response.Redirect("timedout.aspx"); 
                }

                if (Session["DRAINAGE"] != null)
                {
                    string drainage = (string)Session["DRAINAGE"];
                    List<string> wcList = new List<string>();
                    ddlWaterCode.Items.Clear();
                    var l3 = (from q in NRWE.tblWatercodes
                              where q.OBSOLETE != true & q.DRAINAGE == drainage
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
                        //  wcList.Add(x.WATERNAME +  " : " + x.WATERCODE);
                    }
                    if (isNewStation)
                    {
                        wcList.Insert(0, "null");
                    }


                    ddlWaterCode.DataSource = wcList;
                    ddlWaterCode.DataBind();
                }
                //DateTime ET = DateTime.Now;

                //TimeSpan TS = ET - ST; 
                //msg1 = string.Format("Loaded dll in  {0}:{1}:{2}.{3}", TS.Hours, TS.Minutes, TS.Seconds, TS.Milliseconds);
                //LE1.logError(msg1, "Loading Dlls", "", "DEV", "Profiling");

              //  // // ddlWaterBodyID

                ddlWaterBodyID.Items.Clear(); 
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
                if (isNewStation)
                {
                    wbid.Insert(0, "null");
                }
                ddlWaterBodyID.DataSource = wbid;
                ddlWaterBodyID.DataBind();

                // ddlQUADI
                ddlQUADI.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlQUADI.Items.Insert(0, LI);
                    ddlQUADI.BackColor = System.Drawing.Color.White; 
                }

                //                   orderby q.Description
                //                   select q.Description).ToList<string>();

                //ddlQUADI.DataSource = l5;
                //ddlQUADI.DataBind();

                // ddlTownship

                ddlTownship.Items.Clear(); 
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlTownship.Items.Insert(0, LI);
                }
                    
                    
                //    (from q in NRWE.tlkTownships
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlTownship.DataSource = l6;
                //ddlTownship.DataBind();

                // ddlRange
                ddlRange.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlRange.Items.Insert(0, LI);
                }


                //List<string> l9 = (from q in NRWE.tlkRanges
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlRange.DataSource = l9;
                //ddlRange.DataBind();


                // ddlQuarterSection
                ddlQuarterSection.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlQuarterSection.Items.Insert(0, LI);
                }

                //List<string> l7 = (from q in NRWE.tlkQuarterSections
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlQuarterSection.DataSource = l7;
                //ddlQuarterSection.DataBind();

                // ddlGrid
                ddlGrid.Items.Clear(); 
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlGrid.Items.Insert(0, LI);
                }

                ddlRWWaterShed.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlRWWaterShed.Items.Insert(0, LI);
                }



                //ddlWQCCWaterShed
                ddlWQCCWaterShed.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlWQCCWaterShed.Items.Insert(0, LI);
                }
                //ddlWQCCWaterShed.DataSource = l11;
                //ddlWQCCWaterShed.DataBind();

                //ddlState
                ddlState.Items.Clear(); 
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
                ddlHydroUnit.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlHydroUnit.Items.Insert(0, LI);
                }


                //List<string> l13 = (from q in NRWE.tlkHydroUnits
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlHydroUnit.DataSource = l13;
                //ddlHydroUnit.DataBind();

                //ddlEcoRegion
                ddlEcoRegion.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlEcoRegion.Items.Insert(0, LI);
                }

                //ddlEcoRegion.DataSource = l14;
                //ddlEcoRegion.DataBind(); 

                // ddlSection
                ddlSection.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlSection.Items.Insert(0, LI);
                }
                //List<string> l15 = (from q in NRWE.tlkSections
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlSection.DataSource = l15;
                //ddlSection.DataBind();

                // ddlRange
                ddlRange.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlRange.Items.Insert(0, LI);
                }

                //List<string> l16 = (from q in NRWE.tlkRanges
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlRange.DataSource = l16;
                //ddlRange.DataBind();

                //ddlCounty
                ddlCounty.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlCounty.Items.Insert(0, LI);
                }
               

                //List<string> l17 = (from q in NRWE.tlkCounties
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlCounty.DataSource = l17;
                //ddlCounty.DataBind();

                //ddlWSR
                ddlWSR.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlWSR.Items.Insert(0, LI);
                }

                //List<string> l18 = (from q in NRWE.tlkWSRs
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlWSR.DataSource = l18;
                //ddlWSR.DataBind();

                //ddlRegion
                ddlRegion.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlRegion.Items.Insert(0, LI);
                }
                //List<string> l19 = (from q in NRWE.tlkregions
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlRegion.DataSource = l19;
                //ddlRegion.DataBind();

                //ddlStationQUAD
                ddlStationQUAD.Items.Clear();
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
                if (isNewStation)
                {
                    ListItem LI = new ListItem("null");
                    ddlStationQUAD.Items.Insert(0, LI);
                }
                //List<string> l20 = (from q in NRWE.tlkStationQUADs
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlStationQUAD.DataSource = l20;
                //ddlStationQUAD.DataBind();
                ddlRiver.Items.Clear();
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

        protected void btnOK_Click(object sender, EventArgs e)
        {            
            int stnNumber; 
            lblStatus.Text = "";
            lblStatus.Visible = false;
            btnOK.Visible = false;
            pnlTable.Visible = true;
            pnlInput.Visible = true;

            string stnNumString = txtStationNumber.Text.Trim();
            bool success = int.TryParse(stnNumString, out stnNumber);

            if (!success)
            {
                tbStnNumber.Text = "";
                lblStatus.Visible = true;
                lblStatus.Text = "Please select a valid station number";
                return;
            }
            populatePage(stnNumber); 

        }

        protected void btnSelectRiver_Click(object sender, EventArgs e)
        {
            txtriver.Text = ddlRiver.SelectedValue; 
        }

        // we must check to see if this station has any entries in samples table, if so, warn user and do not allow

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int stnNumber = -1; 
            string stnNumString = txtStationNumber.Text.Trim();
            bool success = int.TryParse(stnNumString, out stnNumber);

            if (!success)
            {
                tbStnNumber.Text = "";
                lblStatus.Visible = true;
                lblStatus.Text = "Please select a valid station number";
                return;
            }

            RiverWatchEntities RWE = new RiverWatchEntities();
            Station S = (from sn in RWE.Stations
                            where sn.StationNumber == stnNumber
                            select sn).FirstOrDefault();

            var results = from s in RWE.Samples
                          where s.StationID == S.ID
                          select s; 

            if(results.Count() > 0) // we have entries in samples table
            {
                lblStatus.Text = "There are existing samples linked to this station, you may not delete it now";
                lblStatus.Visible = true;
                btnOK.Visible = true;
                return;
            }

            lblStatus.Text = string.Format("Deleted Station Number {0}", S.StationNumber);
            lblStatus.Visible = true;
            pnlInput.Visible = false;
            pnlTable.Visible = false;
            btnOK.Visible = true;
            RWE.Stations.Remove(S);
            RWE.SaveChanges(); 
        }


    }
}