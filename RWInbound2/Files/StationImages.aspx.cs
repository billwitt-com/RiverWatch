using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Files
{
    public partial class StationImages : System.Web.UI.Page
    {
        private string webApiBaseUrl = WebConfigurationManager.AppSettings["WebApiBaseUrl"];
        private string webApiStationImageController = "StationImage";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMessages();
                StationNamePanel.Visible = false;
                lblStationName.Text = "";
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

        public IQueryable<StationImage> GetStationImages([QueryString]string stationIDSelected = "",
                                                     [QueryString]string successLabelMessage = "")
        {
            try
            {                
                RiverWatchEntities _db = new RiverWatchEntities();

                PropertyInfo isreadonly
                       = typeof(System.Collections.Specialized.NameValueCollection)
                               .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);

                if (!string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", successLabelMessage);
                }

                if (string.IsNullOrEmpty(stationIDSelected))
                {
                    StationNamePanel.Visible = false;
                    lblStationName.Text = "";

                    return null;
                }

                int stationID = 0;
                bool stationIDIsInt = Int32.TryParse(stationIDSelected, out stationID);

                if (!stationIDIsInt)
                {
                    StationNamePanel.Visible = false;
                    lblStationName.Text = "";

                    return null;
                }

                string stationName = _db.Stations
                                    .Where(s => s.ID == stationID)
                                    .Select(s => s.StationName)
                                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(stationName))
                {
                    StationNamePanel.Visible = true;
                    lblStationName.Text = stationName;

                    HiddenStationID.Value = stationID.ToString();
                }

                IQueryable<StationImage> stationImages = _db.StationImages
                                                            .Where(si => si.StationID == stationID);

                // remove
                this.Request.QueryString.Remove("successLabelMessage");
                if (string.IsNullOrEmpty(successLabelMessage))
                {
                    SetMessages("Success", "");
                }               

                return stationImages;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetStationImages", "", "");
                return null;
            }
        }

        public void DeleteStationImage(StationImage model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var stationImageToDelete = _db.StationImages.Find(model.ID);
                    _db.StationImages.Remove(stationImageToDelete);
                    _db.SaveChanges();

                    //string stationName = lblStationName.Text;
                    string successLabelMessage
                            = string.Format("Station File deleted. Station Name: {0} Image: {1}", model.Station.StationName, model.FileName);

                    string redirect = string.Format("StationImages.aspx?stationIDSelected={0}&successLabelMessage={1}",
                                                     HiddenStationID.Value, successLabelMessage);

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteStationImage", "", "");
            }
        }

        protected void AddNewStationImage(object sender, EventArgs e)
        { 
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var content = new MultipartFormDataContent())
                    {
                        var uploadedFile = (FileUpload)StationImagesGridView.FooterRow.FindControl("FileUploadStationImages");

                        bool errors = false;
                        HttpPostedFile file = uploadedFile.PostedFile;
                        if ((file != null) && (file.ContentLength > 0))
                        {
                            if (IsImage(file) == false)
                            {
                                ((Label)StationImagesGridView.FooterRow.FindControl("lblFileUploadStationImagesFileType")).Visible = true;
                                errors = true;
                            }
                        }

                        int iFileSize = file.ContentLength;
                        if (iFileSize > 1048576)  // 1MB
                        {
                            ((Label)StationImagesGridView.FooterRow.FindControl("lblFileUploadStationImagesMaxSize")).Visible = true;
                            errors = true;
                        }                       
                        if (errors)
                        {
                            return;
                        }

                        var fileContent = new ByteArrayContent(uploadedFile.FileBytes);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            FileName = uploadedFile.FileName,
                            Name = "file"                            
                        };

                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(uploadedFile.FileName));
                        content.Add(fileContent);

                        string hiddenStationID = HiddenStationID.Value;
                        int stationID = 0;
                        bool stationIDIsInt = Int32.TryParse(hiddenStationID, out stationID);

                        string createdBy = "Not Found";
                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            createdBy = HttpContext.Current.User.Identity.Name;
                        }

                        string uploadServiceBaseAddress 
                            = string.Format("{0}/{1}/{2}/{3}/{4}", webApiBaseUrl, webApiStationImageController,
                                                                   "PostStationImage", stationID, createdBy);

                        //var result = client.PostAsync(uploadServiceBaseAddress, content).Result;
                        BlobUploadModel uploadResult = null;
                        //uploadResult = result.Content.ReadAsAsync<BlobUploadModel>().Result;
                        bool fileUploaded = false;

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
                                        fileUploaded = true;
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
                        if (fileUploaded)
                        {
                            string successLabelMessage =  uploadResult.FileName + " with length " + uploadResult.FileSizeInBytes
                                            + " has been uploaded at " + uploadResult.FileUrl;
                            //SetMessages("Success", successMessage);

                            string redirect = string.Format("StationImages.aspx?stationIDSelected={0}&successLabelMessage={1}",
                                                     HiddenStationID.Value, successLabelMessage);

                            Response.Redirect(redirect, false);
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UploadBtn_Click", "", "");
            }
        }

        private bool IsImage(HttpPostedFile file)
        {
            //Checks for image type... you could also do filename extension checks and other things
            return ((file != null) && System.Text.RegularExpressions.Regex.IsMatch(file.ContentType, "image/\\S+") && (file.ContentLength > 0));
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
            try
            {
                string stationName = string.Empty;

                stationNumberSearch.Text = "";
                stationName = stationNameSearch.Text.Trim();

                int stationIDSelected = 0;

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    stationIDSelected = _db.Stations
                                    .Where(s => s.StationName.Equals(stationName))
                                    .Select(s => s.ID)
                                    .FirstOrDefault();
                }

                if (stationIDSelected > 0)
                {
                    string redirect = "StationImages.aspx?stationIDSelected=" + stationIDSelected;

                    Response.Redirect(redirect, false);
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchStationName_Click", "", "");
            }
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