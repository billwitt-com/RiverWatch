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

namespace RWInbound2.Reports
{
    public partial class ICPBlanksAndDups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // ReportViewer1.Visible = false;
        }

        // really button RUN
        protected void Button1_Click(object sender, EventArgs e)
        {
            string qry = "";
            ReportViewer1.Visible = true;   
            System.Text.StringBuilder QRY = new System.Text.StringBuilder();

            qry = "SELECT [SampleNumber] ,[Event] ,[TypeCode] ,[WaterShed] ,[RiverName] ,[KitNumber] ,[OrganizationName] ,[StationNumber] ,[StationName] , [SampleDate], [RWWaterShed] " +
         " ,[WQCCWaterShed] ,[WaterCode] ,[WaterBodyID] ,[WaterShedRegion] ,[County] ,[WaterShedGathering] ,[ProjectName] ,[ProjectID] ,[USGS_Flow] ,[PH] ,[TempC] ,[PHEN_ALK] ,[TOTAL_ALK] " +
         " ,[TOTAL_HARD] ,[DO_MGL] ,[DOSAT] ,[AL_D] ,[AL_T] ,[AS_D] ,[AS_T] ,[CA_D] ,[CA_T] ,[CD_D] ,[CD_T] ,[CU_D] ,[CU_T] ,[FE_D] ,[FE_T] ,[MG_D] ,[MG_T] ,[MN_D] ,[MN_T] ,[PB_D] " +
         " ,[PB_T] ,[SE_D] ,[SE_T] ,[ZN_D] ,[ZN_T] ,[NA_D] ,[NA_T] ,[K_D] ,[K_T] ,[Ammonia] ,[Chloride] ,[ChlorophyllA] ,[DOC] ,[NN] ,[OP] ,[Sulfate] ,[totN] ,[totP] ,[TKN] " +
         " ,[orgN] ,[TSS] ,[Valid] FROM [RiverWatch].[dbo].[viewRawSamplesAll] where (typecode = '10' or typecode = '20')";


            SqlDataSource1.SelectCommand = qry; 

            ReportDataSource rd1 = new ReportDataSource("DataSet1", SqlDataSource1);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd1);
            ReportViewer1.LocalReport.Refresh();

            ReportViewer1.DataBind();

        }
    }
}