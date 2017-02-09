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


//  view: CREATE VIEW [dbo].[viewRawSamplesAll]
namespace RWInbound2.Public
{
    public partial class RawSamplesAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewer1.Visible = false; 
                RiverWatchEntities NRWE = new RiverWatchEntities();

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
                ListItem L = new ListItem("ALL");
                ddlRWWaterShed.Items.Insert(0, L);

                ddlRiver.Items.Clear();
                List<string> l21 = (from q in NRWE.Stations
                                    orderby q.River
                                    where q.River != null & q.River.Length > 1
                                    select q.River).Distinct().ToList<string>();
                l21.Sort();
                ddlRiver.DataSource = l21;
                ddlRiver.DataBind();                   
                ListItem La = new ListItem("ALL");
                ddlRiver.Items.Insert(0, La);

                // ddlWQCCWaterShed

                ddlWQCCWaterShed.Items.Clear();
                var l2 = (from q in NRWE.tlkWQCCWaterSheds
                            orderby q.Description
                            select new
                            {
                                q.Code,
                                q.Description
                            });
                foreach (var v in l2)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlWQCCWaterShed.Items.Add(LI);
                }
                ListItem Lc = new ListItem("ALL");
                ddlWQCCWaterShed.Items.Insert(0, Lc);

                // ddlWaterCode
                ddlWaterCode.Items.Clear();
                var l3 = (from q in NRWE.tblWatercodes
                            orderby q.WATERCODE
                            select new
                            {
                                q.WATERCODE,    
                            });
                foreach (var v in l3)
                {
                    ListItem L3 = new ListItem(v.WATERCODE, v.WATERCODE);
                    ddlWaterCode.Items.Add(L3);

                }
                ListItem Ld = new ListItem("ALL");
                ddlWaterCode.Items.Insert(0, Ld);

                // ddlWaterBodyID
                ddlWaterBodyID.Items.Clear();
                List<string> wbid = new List<string>();
                var l4 = (from q in NRWE.tblWBKeys
                            orderby q.WBID
                            select new                              
                            {   
                                q.WBID,
                            }).AsEnumerable();
                foreach (var v in l4)
                {
                    ListItem LI = new ListItem(v.WBID, v.WBID);
                    ddlWaterBodyID.Items.Add(LI);

                }
                ListItem Lb = new ListItem("ALL");
                ddlWaterBodyID.Items.Insert(0, Lb);

                // ddlWSR
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

                ListItem Lf = new ListItem("ALL");
                ddlWSR.Items.Insert(0, Lf);
 

            //   ddlWSG - water shed gathering from orgs, not stations 

                ddlWSG.Items.Clear();
                var l28 = (from q in NRWE.tlkWSGs
                            orderby q.Description
                            select new
                            {
                                q.Description,
                                q.Code
                            });

                foreach (var v in l28)
                {
                    ListItem LI = new ListItem(v.Description, v.Code);
                    ddlWSG.Items.Add(LI);
                }

                ListItem Lg = new ListItem("ALL");
                ddlWSG.Items.Insert(0, Lg); 

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

                ListItem Le = new ListItem("ALL");
                ddlCounty.Items.Insert(0, Le);

            // ddlProject
                var P = (from p in NRWE.Project

                            select new
                            {
                                p.ProjectName,
                                p.ProjectDescription
                            }).AsEnumerable();
                foreach (var x in P)
                {
                    ListItem LI = new ListItem(x.ProjectName, x.ProjectName);
                    ddlProject.Items.Add(LI);
                }

                //ListItem Lq = new ListItem("ALL");
                //ddlProject.Items.Insert(0, Lq);

                RadioButtonList1.SelectedIndex = 0;
                RadioButtonList2.SelectedIndex = 0;
                RadioButtonList3.SelectedIndex = 0; 
                    

            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchOrgs(string prefixText, int count)
        {
            List<string> orgs = new List<string>();
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
                                orgs.Add(sdr["OrganizationName"].ToString());
                            }
                        }
                        conn.Close();
                        return orgs;
                    }
                }
            }
            catch (Exception ex)
            {
                string nam = "Not Known - in method";
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method, no page related detail", ex.StackTrace.ToString(), nam, "");
                return orgs;
            }
        }

        protected void btnGO_Click(object sender, EventArgs e)
        {

            bool Addedto = false;
            bool isFirst = true;
            bool isOrderBy = false; 
            string qs = "";
            System.Text.StringBuilder QRY = new System.Text.StringBuilder();
            // basic query
        //    QRY.Append("SELECT [StationNumber], [River], [AquaticModelIndex], [WaterCode], [WaterBodyID], [StationType], [RWWaterShed], [StationQUAD], [Grid], [QuaterSection], [Section], [Range], [Township], [QUADI], [WQCCWaterShed], [HydroUnit], [EcoRegion], [StationName], [StationStatus], [Elevation], [WaterShedRegion], [Longtitude], [Latitude], [County], [State], [NearCity], [Move], [Description], [UTMX], [UTMY], [UserLastModified], [DateLastModified], [UserCreated], [DateCreated], [StateEngineering], [USGS], [Comments], [Region], [StoretUploaded] FROM [Station] ");

             QRY.Append("SELECT [SampleNumber] ,[Event] ,[TypeCode] ,[WaterShed] ,[RiverName] ,[KitNumber] ,[OrganizationName] ,[StationNumber] ,[StationName] , [SampleDate], [RWWaterShed] " +
          " ,[WQCCWaterShed] ,[WaterCode] ,[WaterBodyID] ,[WaterShedRegion] ,[County] ,[WaterShedGathering] ,[ProjectName] ,[ProjectID] ,[USGS_Flow] ,[PH] ,[TempC] ,[PHEN_ALK] ,[TOTAL_ALK] " +
          " ,[TOTAL_HARD] ,[DO_MGL] ,[DOSAT] ,[AL_D] ,[AL_T] ,[AS_D] ,[AS_T] ,[CA_D] ,[CA_T] ,[CD_D] ,[CD_T] ,[CU_D] ,[CU_T] ,[FE_D] ,[FE_T] ,[MG_D] ,[MG_T] ,[MN_D] ,[MN_T] ,[PB_D] " +
          " ,[PB_T] ,[SE_D] ,[SE_T] ,[ZN_D] ,[ZN_T] ,[NA_D] ,[NA_T] ,[K_D] ,[K_T] ,[Ammonia] ,[Chloride] ,[ChlorophyllA] ,[DOC] ,[NN] ,[OP] ,[Sulfate] ,[totN] ,[totP] ,[TKN] " +
          " ,[orgN] ,[TSS] ,[Valid] FROM [RiverWatch].[dbo].[viewRawSamplesAll]");

             qs = QRY.ToString(); // just for testing
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
                qs = string.Format(" [RiverName] = '{0}' ", ddlRiver.SelectedValue);
                QRY.Append(qs);
                Addedto = true; 

            }

            if(ddlRWWaterShed.SelectedIndex != 0)
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

            // ddlWQCCWaterShed
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


            //ddlWaterCode
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
            
            //ddlWaterBodyID
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

            // ddlWSR

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

            //ddlWSG
            if (ddlWSG.SelectedIndex != 0)
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
                qs = string.Format(" [WaterShedGathering] = '{0}' ", ddlWSG.SelectedValue);
                QRY.Append(qs);
                Addedto = true;
            }

            // ddlCounty
            if (ddlCounty.SelectedIndex != 0)
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
                qs = string.Format(" [County] = '{0}' ", ddlCounty.SelectedValue);
                QRY.Append(qs);
                Addedto = true;
            }

            // ddlProject

            //if (ddlProject.SelectedIndex != 0)
            //{
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
                qs = string.Format(" [ProjectName] = '{0}' ", ddlProject.SelectedValue);
                QRY.Append(qs);
                Addedto = true;
       //     }

            if(tbKitNumber.Text != "")
            {
                int kn = 0;
                if (int.TryParse(tbKitNumber.Text, out kn))
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
                    qs = string.Format(" [KitNumber] = '{0}' ", kn);
                    QRY.Append(qs);
                    Addedto = true;
                    MSGLabel.Text  = "";
                    MSGLabel.BackColor = System.Drawing.Color.White; 
                }
                else
                {
                    MSGLabel.Text = "Please enter a valid number for kit number";
                    MSGLabel.BackColor = System.Drawing.Color.Red;
                    tbKitNumber.Focus(); 
                }
            }

            if(tbOrgName.Text != "")
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
                qs = string.Format(" [OrganizationName] = '{0}' ", tbOrgName.Text);
                QRY.Append(qs);
                Addedto = true;
                MSGLabel.Text = "";
                MSGLabel.BackColor = System.Drawing.Color.White; 

            }

            if (tbStationNumber.Text != "")
            {
                int sn = 0;
                if (int.TryParse(tbStationNumber.Text, out sn))
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
                    qs = string.Format(" [StationNumber] = '{0}' ", sn);
                    QRY.Append(qs);
                    Addedto = true;
                    MSGLabel.Text = "";
                    MSGLabel.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    MSGLabel.Text = "Please enter a valid number for Station Number";
                    MSGLabel.BackColor = System.Drawing.Color.Red;
                    tbStationNumber.Focus();
                }
            }

            if (isFirst)
            {
                QRY.Append(" WHERE ");
                Addedto = true;
            }
            if (Addedto)
            {
                QRY.Append(" And typecode <> '10' and typecode <> '20' ");
            }

            string oblist = "";
            if(RadioButtonList1.SelectedIndex != 0)
            {
                if(isOrderBy == false)
                {
                    QRY.Append("ORDER BY ");    // note space at end of string...
                    isOrderBy = true;
                    QRY.Append(RadioButtonList1.SelectedValue); 

                }
                if (RadioButtonList2.SelectedIndex != 0)
                {
                    oblist = string.Format(" ,{0} ", RadioButtonList2.SelectedValue);   // note spaces in string, important
                    QRY.Append(oblist);
                    if (RadioButtonList3.SelectedIndex != 0)
                    {
                        oblist = string.Format(" ,{0} ", RadioButtonList3.SelectedValue);
                        QRY.Append(oblist);
                    }
                }
            }

            // add last filter for only showing type '00'           
           

            qs = QRY.ToString();

            SqlDataSource1.SelectCommand = QRY.ToString();

            ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            ReportViewer1.LocalReport.Refresh();

            string test = QRY.ToString();
            ReportViewer1.DataBind();
            ReportViewer1.Visible = true;           

        }

        protected void btnFAQ_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/content/RWdatainfodownload.pdf");
            // touch
        }
    }
}