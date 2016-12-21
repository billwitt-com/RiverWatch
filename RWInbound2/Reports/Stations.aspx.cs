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
using System.Drawing;
using Microsoft.Reporting.WebForms; 
// modified to add drop down lists and multi selection using sql queries
// lots of code copied from stn editor in admin tab


namespace RWInbound2.Reports
{
    public partial class Stations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                clearDDLselections();
                loadDropDownLists();
                ReportViewer1.Visible = false;
            }
        }

        public void loadDropDownLists()
        {
            RiverWatchEntities NRWE = new RiverWatchEntities();
            bool isNewStation = true; // reused
            clearDDLselections(); // just in case
            try
            {
                // ddlStationStatus
                //List<string> l1 = (from q in NRWE.tlkStationStatus
                //                   where q.Valid == true
                //                   select q.Code).ToList<string>();


                var l3 = (from q in NRWE.tblWatercodes
                          where q.OBSOLETE != true 
                          
                          select new
                          { 
                              q.WATERCODE
                          }).AsEnumerable();
                foreach (var x in l3)
                {
                    ListItem LI = new ListItem(x.WATERCODE, x.WATERCODE);
                    ddlWaterCode.Items.Add(LI);
                }
                if (isNewStation)
                {
                    ListItem LI = new ListItem("ALL");
                    ddlWaterCode.Items.Insert(0, LI);
                }

                // ****************
                ddlStationStatus.Items.Clear();
                var l1 = (from q in NRWE.tlkStationStatus
                          where q.Valid == true
                          select new
                          {
                              q.Description,
                              q.Code,
                          });
                foreach (var v in l1)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlStationStatus.Items.Add(LI);
                }
                if (isNewStation)
                {
                    ListItem LI = new ListItem("ALL");
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
                    ListItem LI = new ListItem("ALL");
                    ddlStationType.Items.Insert(0, LI);
                }
              

                ddlWaterBodyID.Items.Clear();
                List<string> wbid = new List<string>();
                var l4 = (from q in NRWE.tblWBKeys
                          orderby q.WBID
                          select new
                          {
                              //q.WATERSHED,
                              //q.SUBBASIN,
                              q.WBID,
                              //q.SegDesc
                          }).AsEnumerable();
                foreach (var x in l4)
                {

                    ListItem LI = new ListItem(x.WBID, x.WBID);
                    ddlWaterBodyID.Items.Add(LI); 

                }
                if (isNewStation)
                {
                    ListItem LI = new ListItem("ALL");
                    ddlWaterBodyID.Items.Insert(0, LI);
                }
                //ddlWaterBodyID.DataSource = wbid;
                //ddlWaterBodyID.DataBind();

                //// ddlQUADI
                //ddlQUADI.Items.Clear();
                //var l5 = (from q in NRWE.tlkQUADIs
                //          select new
                //          {
                //              q.Description,
                //              q.Code
                //          });

                //foreach (var v in l5)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlQUADI.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlQUADI.Items.Insert(0, LI);
                //    ddlQUADI.BackColor = System.Drawing.Color.White;
                //}

                //                   orderby q.Description
                //                   select q.Description).ToList<string>();

                //ddlQUADI.DataSource = l5;
                //ddlQUADI.DataBind();

                // ddlTownship

                //ddlTownship.Items.Clear();
                //var l6 = (from q in NRWE.tlkTownships
                //          select new
                //          {
                //              q.Description,
                //              q.Code
                //          });

                //foreach (var v in l6)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlTownship.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlTownship.Items.Insert(0, LI);
                //}


                //    (from q in NRWE.tlkTownships
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlTownship.DataSource = l6;
                //ddlTownship.DataBind();

                // ddlRange
                //ddlRange.Items.Clear();
                //var l9 = (from q in NRWE.tlkRanges
                //          select new
                //          {
                //              q.Description,
                //              q.Code
                //          });

                //foreach (var v in l9)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlRange.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlRange.Items.Insert(0, LI);
                //}


                //List<string> l9 = (from q in NRWE.tlkRanges
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlRange.DataSource = l9;
                //ddlRange.DataBind();


                // ddlQuarterSection
                //ddlQuarterSection.Items.Clear();
                //var l7 = (from q in NRWE.tlkQuarterSections
                //          select new
                //          {
                //              q.Description,
                //              q.Code
                //          });

                //foreach (var v in l7)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlQuarterSection.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlQuarterSection.Items.Insert(0, LI);
                //}

                //List<string> l7 = (from q in NRWE.tlkQuarterSections
                //                   orderby q.Description
                //                   select q.Description).ToList<string>();
                //ddlQuarterSection.DataSource = l7;
                //ddlQuarterSection.DataBind();

                // ddlGrid
                //ddlGrid.Items.Clear();
                //var l8 = (from q in NRWE.tlkGrids
                //          select new
                //          {
                //              q.Description,
                //              q.Code
                //          });

                //foreach (var v in l8)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlGrid.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlGrid.Items.Insert(0, LI);
                //}

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
                    ListItem LI = new ListItem("ALL");
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
                    ListItem LI = new ListItem("ALL");
                    ddlWQCCWaterShed.Items.Insert(0, LI);
                }
                //ddlWQCCWaterShed.DataSource = l11;
                //ddlWQCCWaterShed.DataBind();

                //ddlState
                //ddlState.Items.Clear();
                //var l12 = (from q in NRWE.tlkStates
                //           where q.Description.ToUpper() != "COLORADO"
                //           orderby q.Description
                //           select new
                //           {
                //               q.Description,
                //               q.Code
                //           });
                //foreach (var v in l12)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlState.Items.Add(LI);
                //}
                //ListItem LI100 = new ListItem("COLORADO", "CO");    // put this at top of list
                //ddlState.Items.Insert(0, LI100);

                //List<string> l12 = (from q in NRWE.tlkStates
                //                    where q.Description.ToUpper() != "COLORADO"
                //                    orderby q.Code
                //                    select q.Code).ToList<string>();
                //ddlState.DataSource = l12;
                //ddlState.DataBind();
                //ddlState.Items.Insert(0, "COLORADO");

                ////ddlHydroUnit
                //ddlHydroUnit.Items.Clear();
                //var l13 = (from q in NRWE.tlkHydroUnits
                //           orderby q.Description
                //           select new
                //           {
                //               q.Description,
                //               q.Code
                //           });
                //foreach (var v in l13)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlHydroUnit.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlHydroUnit.Items.Insert(0, LI);
                //}


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
                    ListItem LI = new ListItem("ALL");
                    ddlEcoRegion.Items.Insert(0, LI);
                }

                //ddlEcoRegion.DataSource = l14;
                //ddlEcoRegion.DataBind(); 

                // ddlSection
                //ddlSection.Items.Clear();
                //var l15 = (from q in NRWE.tlkSection
                //           orderby q.Description
                //           select new
                //           {
                //               q.Description,
                //               q.Code
                //           });

                //foreach (var v in l15)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlSection.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlSection.Items.Insert(0, LI);
                //}
                //List<string> l15 = (from q in NRWE.tlkSections
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlSection.DataSource = l15;
                //ddlSection.DataBind();

                // ddlRange
                //ddlRange.Items.Clear();
                //var l16 = (from q in NRWE.tlkRanges
                //           orderby q.Description
                //           select new
                //           {
                //               q.Description,
                //               q.Code
                //           });

                //foreach (var v in l16)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlRange.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlRange.Items.Insert(0, LI);
                //}

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
                    ListItem LI = new ListItem("ALL");
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
                    ListItem LI = new ListItem("ALL");
                    ddlWSR.Items.Insert(0, LI);
                }

                //List<string> l18 = (from q in NRWE.tlkWSRs
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlWSR.DataSource = l18;
                //ddlWSR.DataBind();

                //ddlRegion
                //ddlRegion.Items.Clear();
                //var l19 = (from q in NRWE.tlkregions
                //           orderby q.Description
                //           select new
                //           {
                //               q.Description,
                //               q.Code
                //           });

                //foreach (var v in l19)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlRegion.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlRegion.Items.Insert(0, LI);
                //}
                //List<string> l19 = (from q in NRWE.tlkregions
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlRegion.DataSource = l19;
                //ddlRegion.DataBind();

                //ddlStationQUAD
                //ddlStationQUAD.Items.Clear();
                //var l20 = (from q in NRWE.tlkStationQUADs
                //           orderby q.Description
                //           select new
                //           {
                //               q.Description,
                //               q.Code
                //           });

                //foreach (var v in l20)
                //{
                //    ListItem LI = new ListItem(v.Description, v.Code);
                //    ddlStationQUAD.Items.Add(LI);
                //}
                //if (isNewStation)
                //{
                //    ListItem LI = new ListItem("null");
                //    ddlStationQUAD.Items.Insert(0, LI);
                //}
                //List<string> l20 = (from q in NRWE.tlkStationQUADs
                //                    orderby q.Description
                //                    select q.Description).ToList<string>();
                //ddlStationQUAD.DataSource = l20;
                //ddlStationQUAD.DataBind();
                ddlRiver.Items.Clear();
                List<string> l21 = (from q in NRWE.Stations
                                    orderby q.River
                                    where q.River != null & q.River.Length > 1
                                    select q.River).Distinct().ToList<string>();
                l21.Sort();
                ddlRiver.DataSource = l21;
                ddlRiver.DataBind();
                if (isNewStation)
                {
                    ListItem LI = new ListItem("ALL");
                    ddlRiver.Items.Insert(0, LI);
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
                LE.logError(msg, this.Page.Request.AppRelativeCurrentExecutionFilePath, ex.StackTrace.ToString(), nam, "Admin Stations");
            }
        }

        public void clearDDLselections()
        {
            ddlCounty.ClearSelection();
            ddlEcoRegion.ClearSelection();
            //ddlGrid.ClearSelection();
            //ddlHydroUnit.ClearSelection();
            //ddlQUADI.ClearSelection();
            //ddlQuarterSection.ClearSelection();
            //ddlRange.ClearSelection();
            //ddlRegion.ClearSelection();
            ddlRiver.ClearSelection();
            ddlRWWaterShed.ClearSelection();
            //ddlSection.ClearSelection();
            //ddlState.ClearSelection();
            //ddlStationQUAD.ClearSelection();
            ddlStationStatus.ClearSelection();
            ddlStationType.ClearSelection();
            //ddlTownship.ClearSelection();
            ddlWaterBodyID.ClearSelection();
            ddlWaterCode.ClearSelection();
            ddlWQCCWaterShed.ClearSelection();
            ddlWSR.ClearSelection();
        }

        protected void btnGO_Click(object sender, EventArgs e)
        {
            bool Addedto = false;
            bool isFirst = true;
            string qs = ""; 
            System.Text.StringBuilder QRY = new System.Text.StringBuilder();
            // basic query
            QRY.Append("SELECT [StationNumber], [River], [AquaticModelIndex], [WaterCode], [WaterBodyID], [StationType], [RWWaterShed], [StationQUAD], [Grid], [QuaterSection], [Section], [Range], [Township], [QUADI], [WQCCWaterShed], [HydroUnit], [EcoRegion], [StationName], [StationStatus], [Elevation], [WaterShedRegion], [Longtitude], [Latitude], [County], [State], [NearCity], [Move], [Description], [UTMX], [UTMY], [UserLastModified], [DateLastModified], [UserCreated], [DateCreated], [StateEngineering], [USGS], [Comments], [Region], [StoretUploaded] FROM [Station] ");

            if(ddlCounty.SelectedIndex != 0)
            {
                if(isFirst)
                {
                     QRY.Append(" WHERE ");
                    isFirst = false; 
                }
                if(Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true; 
                }
                qs = string.Format(" [County] = '{0}' ", ddlCounty.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 
            }
            if (ddlEcoRegion.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [EcoRegion] = '{0}' ", ddlEcoRegion.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }
            if (ddlRiver.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [River] = '{0}' ", ddlRiver.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 
            }

            if (ddlRWWaterShed.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [RWWaterShed] = '{0}' ", ddlRWWaterShed.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }       
            if (ddlStationStatus.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [StationStatus] = '{0}' ", ddlStationStatus.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }
            if (ddlStationType.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [StationType] = '{0}' ", ddlStationType.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 
            }

            if (ddlWaterBodyID.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [WaterBodyID] = '{0}' ", ddlWaterBodyID.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }
            if (ddlWaterCode.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [WaterCode] = '{0}' ", ddlWaterCode.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }
            if (ddlWQCCWaterShed.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [WQCCWaterShed] = '{0}' ", ddlWQCCWaterShed.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }
            if (ddlWSR.SelectedIndex != 0)
            {
                if (isFirst)
                {
                    QRY.Append(" WHERE ");
                    isFirst = false;
                }
                if (Addedto)
                {
                    QRY.Append(" And ");
                    Addedto = true;
                }
                qs = string.Format(" [WaterShedRegion] = '{0}' ", ddlWSR.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 
            }

            QRY.Append("ORDER BY [StationName]");
            SqlDataSource1.SelectCommand = QRY.ToString();

            ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            ReportViewer1.LocalReport.Refresh();

            string test = QRY.ToString(); 
            ReportViewer1.DataBind();
            ReportViewer1.Visible = true;
        }
    }
}