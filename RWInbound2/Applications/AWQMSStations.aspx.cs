using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;


namespace RWInbound2.Applications
{
    public partial class AWQMSStations : System.Web.UI.Page
    {
        DataTable DTout = new DataTable(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSendFile.Visible = false;
                lblDownload.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DTout.Columns.Add(new DataColumn("StationNumber", typeof(string)));
            DTout.Columns.Add(new DataColumn("StationName", typeof(string)));
            DTout.Columns.Add(new DataColumn("StationType", typeof(string)));
            DTout.Columns.Add(new DataColumn("Latitude", typeof(string)));
            DTout.Columns.Add(new DataColumn("Longitude", typeof(string)));
            DTout.Columns.Add(new DataColumn("Coordinate System", typeof(string)));
            DTout.Columns.Add(new DataColumn("Horizontal Collection Method", typeof(string)));
            DTout.Columns.Add(new DataColumn("State", typeof(string)));

            DTout.Columns.Add(new DataColumn("Description", typeof(string)));
            DTout.Columns.Add(new DataColumn("Elevation", typeof(string)));
            DTout.Columns.Add(new DataColumn("Vertical Units", typeof(string)));
            DTout.Columns.Add(new DataColumn("Vertical Reference", typeof(string)));
            DTout.Columns.Add(new DataColumn("Vertical Collection Method", typeof(string)));
            DTout.Columns.Add(new DataColumn("Scale", typeof(string)));
            DTout.Columns.Add(new DataColumn("Waterbody Name", typeof(string)));
            DTout.Columns.Add(new DataColumn("County", typeof(string)));
            DTout.Columns.Add(new DataColumn("HydroUnit", typeof(string)));

            DTout.Columns.Add(new DataColumn("HUC 12", typeof(string)));


            DTout.Columns.Add(new DataColumn("Country", typeof(string)));


            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();

                var S = from s in RWE.Stations
                        select s;

                foreach (Station stn in S)
                {
                    System.Data.DataRow DR = DTout.NewRow();
                    if (stn.StationNumber.HasValue)
                        DR["StationNumber"] = stn.StationNumber;
                    else
                        DR["StationNumber"] = "";
                    DR["StationName"] = stn.StationName ?? "";
                    DR["StationType"] = "River/Stream";
                    if (stn.Latitude.HasValue)
                        DR["Latitude"] = stn.Latitude;
                    else
                        DR["Latitude"] = "";

                    if (stn.Longtitude.HasValue)
                        DR["Longitude"] = stn.Latitude;
                    else
                        DR["Longitude"] = "";

                    DR["Coordinate System"] = "NAD83";
                    DR["State"] = stn.State ?? "";
                    DR["County"] = stn.County ?? "";
                    DR["Description"] = stn.Description ?? "";
                    if (stn.Elevation.HasValue)
                        DR["Elevation"] = stn.Elevation;
                    else
                        DR["Elevation"] = "";
                    DR["Vertical Units"] = "FT";
                    DR["Vertical Reference"] = "SEALV";
                    DR["Scale"] = "1:1600000";
                    DR["Waterbody Name"] = stn.River ?? "";
                    DR["HydroUnit"] = stn.HydroUnit ?? "";
 //                   DR["Move"] = "";
                    DR["HUC 12"] = "";
                    DR["Horizontal Collection Method"] = "Interpolation-Map";
                    DR["Vertical Collection Method"] = "Topographic Map Interpolation";
                    DR["Country"] = "US";

                    DTout.Rows.Add(DR);
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
            // we have full data table, so write to file and allow user to download

            string app_DataDirectory = HttpContext.Current.Server.MapPath("~/App_Data");
            if (!Directory.Exists(app_DataDirectory))
            {
                Directory.CreateDirectory(app_DataDirectory);
            }

            string[] txtList = Directory.GetFiles(app_DataDirectory, "AWQMS_Stations*.csv");
            foreach (string f in txtList)
            {
                File.Delete(f);
            }

            string outFile = "~/App_Data/AWQMS_Stations." + DateTime.Now.ToString("MM.dd.yyyy") + ".csv";
            outFile = HttpContext.Current.Server.MapPath(outFile);

            FileInfo FI = new FileInfo(outFile);    // delete if exists
            if (FI.Exists)
                FI.Delete();

            Session["OUTFILE"] = outFile;

            using (StreamWriter writer = new StreamWriter(outFile))
            {
                OurWriter.WriteDataTable(DTout, writer, true);
            }

            FileInfo FII = new FileInfo(outFile);
            string shortFileName = FII.Name;
            btnSendFile.Visible = true;
            btnSendFile.Text = "DownLoad Results";
            lblDownload.Visible = true;
            lblDownload.Text = string.Format("Download {0}", shortFileName); 
        }

        protected void btnSendFile_Click(object sender, EventArgs e)
        {
            string outFile = "";
            string Cheader = "";
            string contentDisposition = "";

            lblDownload.Text = "";
            if (Session["OUTFILE"] != null)
            {
                outFile = (string)Session["OUTFILE"];

                //   outFile = @"\App_Data\AWQMSMetalsandNutrient.02.16.2017.csv"; 

                try
                {
                    FileStream LiveFileStream = new FileStream(outFile, FileMode.Open, FileAccess.Read);
                    byte[] filebuffer = new byte[(int)LiveFileStream.Length + 1];

                    int result = LiveFileStream.Read(filebuffer, 0, (int)LiveFileStream.Length);
                    //  BufferedStream   fileBuffer = new BufferedStream( LiveFileStream, (int)LiveFileStream.Length); 
                    //   LiveFileStream.Read(fileBuffer, 0, (int)LiveFileStream.Length); 
                    LiveFileStream.Close();
                    Response.Clear();

                    Response.Charset = "utf-8";
                    //  Response.ContentType = "text/plain";
                    Response.ContentType = "application/x-csv";
                    Response.AddHeader("Content-Length", filebuffer.Length.ToString());
                    Cheader = string.Format("attachment; filename={0}", outFile) ; //"\\App_Data\\AWQMSMetalsandNutrient.02.16.2017.csv");
                    contentDisposition = "Content-Disposition";
                    Response.AddHeader(contentDisposition, Cheader);
                    Response.BinaryWrite(filebuffer);
                    Response.End();
                }
                catch (System.Exception exx)
                // file IO errors
                {
                    // we seem to be getting here via aborted thread. Seems OK as the response is closed in code
                    string msg = exx.Message;
                    lblDownload.Text = "";
                }
            }
        }
    }




    public static class OurWriter
    {
        public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                IEnumerable<String> headerValues = sourceTable.Columns
                    .OfType<DataColumn>().Select(column => QuoteValue(column.ColumnName));

                writer.WriteLine(String.Join(",", headerValues));
            }

            IEnumerable<String> items = null;

            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString()));
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }

        private static string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }
    }
}