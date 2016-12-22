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

namespace RWInbound2.Public
{
    public partial class RawSamplesAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            string qs = "";
            System.Text.StringBuilder QRY = new System.Text.StringBuilder();
            // basic query
            QRY.Append("SELECT [StationNumber], [River], [AquaticModelIndex], [WaterCode], [WaterBodyID], [StationType], [RWWaterShed], [StationQUAD], [Grid], [QuaterSection], [Section], [Range], [Township], [QUADI], [WQCCWaterShed], [HydroUnit], [EcoRegion], [StationName], [StationStatus], [Elevation], [WaterShedRegion], [Longtitude], [Latitude], [County], [State], [NearCity], [Move], [Description], [UTMX], [UTMY], [UserLastModified], [DateLastModified], [UserCreated], [DateCreated], [StateEngineering], [USGS], [Comments], [Region], [StoretUploaded] FROM [Station] ");

            if (ddlRiver.SelectedIndex != 0)
            {


            }

            if(ddlRWWaterShed.SelectedIndex != 0)
            {


            }

            if(tbKitNumber.Text != "")
            {


            }

            if(tbOrgName.Text != "")
            {


            }

            if(tbStationNumber.Text != "")
            {


            }

            if(RadioButtonList1.SelectedIndex != 0)
            {
                if (RadioButtonList2.SelectedIndex != 0)
                {
                    if (RadioButtonList2.SelectedIndex != 0)
                    {



                    }


                }
            }

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

            // 
            QRY.Append("ORDER BY [StationName]");
            //SqlDataSource1.SelectCommand = QRY.ToString();

            //ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rd1);
            //ReportViewer1.LocalReport.Refresh();

            //string test = QRY.ToString();
            //ReportViewer1.DataBind();
            //ReportViewer1.Visible = true;
            

        }
    }
}