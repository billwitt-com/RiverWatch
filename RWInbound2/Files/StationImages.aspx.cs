using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Files
{
    public partial class StationImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMessages();
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }

        private void SetMessages(string type = "", string message = "")
        {
            switch (type)
            {
                case "Success":
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = message;
                    break;
                case "Error":
                    ErrorLabel.Text = message;
                    SuccessLabel.Text = "";
                    break;
                default:
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "";
                    break;
            }
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {           

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new ByteArrayContent(FileUploadStationImages.FileBytes);//(System.IO.File.ReadAllBytes(fileName));
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            FileName = FileUploadStationImages.FileName,
                            Name = "file"                            
                        };
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(FileUploadStationImages.FileName));
                        content.Add(fileContent);                        
                        string uploadServiceBaseAddress = "http://localhost:21028/blobs/upload/10/test";
                        //var result = client.PostAsync(uploadServiceBaseAddress, content).Result;
                        BlobUploadModel uploadResult = null;
                        //uploadResult = result.Content.ReadAsAsync<BlobUploadModel>().Result;
                        bool _fileUploaded = false;

                        Task taskUpload = client.PostAsync(uploadServiceBaseAddress, content).ContinueWith(task =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                var response = task.Result;

                                if (response.IsSuccessStatusCode)
                                {
                                    var result = response.Content.ReadAsAsync<List<BlobUploadModel>>().Result;
                                                                  
                                    if (result != null && result.Count > 0)
                                    {
                                        _fileUploaded = true;
                                        uploadResult = result[0];
                                    }
                                        

                                    // Read other header values if you want..
                                    foreach (var header in response.Content.Headers)
                                    {
                                        Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                                    }
                                }
                                else
                                {
                                    Debug.WriteLine("Status Code: {0} - {1}", response.StatusCode, response.ReasonPhrase);
                                    Debug.WriteLine("Response Body: {0}", response.Content.ReadAsStringAsync().Result);
                                }
                            }
                            
                        });

                        taskUpload.Wait();
                        if (_fileUploaded)
                        {
                            string successMessage =  uploadResult.FileName + " with length " + uploadResult.FileSizeInBytes
                                            + " has been uploaded at " + uploadResult.FileUrl;
                            SetMessages("Success", successMessage);
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UploadBtn_Click", "", "");
            }
        }        

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForStationName(string prefixText, int count)
        {
            List<string> stationNames = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    stationNames = _db.Stations
                                    .Where(s => s.StationName.StartsWith(prefixText))
                                    .OrderBy(s => s.StationName)
                                    .Select(s => s.StationName)
                                    .Distinct()
                                    .ToList();
                    stationNames.Sort();
                    return stationNames;
                }
            }
            catch (Exception ex)
            {
                StationImages stationImages = new StationImages();
                stationImages.HandleErrors(ex, ex.Message, "SearchForStationName", "", "");
                return stationNames;
            }
        }

        protected void btnSearchStationName_Click(object sender, EventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForStationNumber(string prefixText, int count)
        {
            List<string> stationNumbers = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    stationNumbers = _db.Stations
                                    .Where(s => s.StationNumber.ToString().StartsWith(prefixText))
                                    .OrderBy(s => s.StationNumber)
                                    .Select(s => s.StationNumber.ToString())
                                    .Distinct()
                                    .ToList();
                    stationNumbers.Sort();
                    return stationNumbers;
                }
            }
            catch (Exception ex)
            {
                StationImages stationImages = new StationImages();
                stationImages.HandleErrors(ex, ex.Message, "SearchForStationNumber", "", "");
                return stationNumbers;
            }
        }

        protected void btnSearchStationNumber_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                stationNameSearch.Text = "";
                stationNumberSearch.Text = "";

                string redirect = "StationImages.aspx";

                Response.Redirect(redirect, false);

            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                               string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SetMessages();

            if (ex.GetType().IsAssignableFrom(typeof(DbEntityValidationException)))
            {
                DbEntityValidationException efException = ex as DbEntityValidationException;
                StringBuilder sb = new StringBuilder();

                foreach (var eve in efException.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendFormat("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage + Environment.NewLine);
                    }
                }
                SetMessages("Error", sb.ToString());
            }
            else
            {
                SetMessages("Error", ex.Message);
            }
        }
    }

    public class BlobUploadModel
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSizeInBytes { get; set; }
        public long FileSizeInKb { get { return (long)Math.Ceiling((double)FileSizeInBytes / 1024); } }       
    }
}