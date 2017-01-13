using Newtonsoft.Json;
using RWInbound2.HelperModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private string webApiBaseUrl = WebConfigurationManager.AppSettings["WebAPI_BaseUrl"];
        private static string webApi_username = WebConfigurationManager.AppSettings["WebAPI_UserName"];
        private static string webApi_password = WebConfigurationManager.AppSettings["WebAPI_Password"];

        byte[] byteArray = Encoding.ASCII.GetBytes(webApi_username + ":" + webApi_password);

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

        public IQueryable BindStationImageTypes()
        {
            RiverWatchEntities _db = new RiverWatchEntities();
            var imageTypes = (from sit in _db.StationImageTypes
                              orderby sit.Type
                              select sit);

            return imageTypes;
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
                //if (string.IsNullOrEmpty(successLabelMessage))
                //{
                //    SetMessages("Success", "");
                //}               

                return stationImages;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetStationImages", "", "");
                return null;
            }
        }


        protected void AddNewStationImage(object sender, EventArgs e)
        {
            try
            {
                SetMessages();
                string errorMessage = string.Empty;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    using (var content = new MultipartFormDataContent())
                    {
                        var gridView = StationImagesGridView.Controls[0].Controls[0].FindControl("btnAdd") == null ?
                                    StationImagesGridView.FooterRow : StationImagesGridView.Controls[0].Controls[0];

                        var uploadedFile = (FileUpload)gridView.FindControl("FileUploadStationImages");

                        bool errors = false;
                        string fileErrorMessage = "New image errors: ";

                        HttpPostedFile file = uploadedFile.PostedFile;
                        if ((file != null) && (file.ContentLength > 0))
                        {
                            if (IsImage(file) == false)
                            {
                                fileErrorMessage += "<br>Invalid Image file type! ";
                                errors = true;
                            }
                        }

                        string image = string.Empty;
                        //bool primaryImageExists = false;
                        bool primaryChecked = false;

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            string hiddenStatID = HiddenStationID.Value;
                            int statID = 0;
                            bool statIDIsInt = Int32.TryParse(hiddenStatID, out statID);

                            image = _db.StationImages
                                        .Where(s => s.FileName.Equals(file.FileName) && s.StationID == statID)
                                        .Select(s => s.FileName)
                                        .FirstOrDefault();
                        }
                        if (!string.IsNullOrEmpty(image))
                        {
                            fileErrorMessage += "<br>File already exists for this station! ";
                            errors = true;
                        }

                        int iFileSize = file.ContentLength;
                        if (iFileSize > 1048576)  // 1MB
                        {
                            fileErrorMessage += "<br>Image exceeds maximum Image size! ";
                            errors = true;
                        }
                        if (errors)
                        {
                            SetMessages("Error", fileErrorMessage);
                            ((Button)gridView.FindControl("btnAdd")).Focus();
                        }
                        else
                        {
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

                            primaryChecked = ((CheckBox)gridView.FindControl("NewCheckBoxPrimary")).Checked;

                            string imageTypeType = ((DropDownList)gridView.FindControl("dropDownNewStationImageTypes")).Text;
                            var imageType = ((DropDownList)gridView.FindControl("dropDownNewStationImageTypes")).SelectedValue;
                            int imageTypeID = 0;
                            bool imageTypeIDIsInt = Int32.TryParse(imageType, out imageTypeID);
                            string description = ((TextBox)gridView.FindControl("NewDescription")).Text;

                            string uploadStationImageBaseAddress
                                = string.Format("{0}/{1}/{2}", webApiBaseUrl, webApiStationImageController,
                                                                       "PostStationImage");

                            StationImageUploadModel uploadResult = null;

                            bool fileUploaded = false;

                            var stationImageUploadModel = new StationImageUploadModel
                            {
                                StationID = stationID,
                                User = createdBy,
                                Primary = primaryChecked,
                                ImageTypeID = imageTypeID,
                                ImageTypeType = imageTypeType,
                                Description = description,
                                PhysHabYear = 0
                            };
                            // Serialize our concrete class into a JSON String
                            var stringUploadModel = JsonConvert.SerializeObject(stationImageUploadModel);

                            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                            var jsonHttpContent = new StringContent(stringUploadModel, Encoding.UTF8, "application/json");
                            content.Add(jsonHttpContent, "stationImageUploadModel");

                            Task taskUpload = client.PostAsync(uploadStationImageBaseAddress, content).ContinueWith(task =>
                            {
                                if (task.Status == TaskStatus.RanToCompletion)
                                {
                                    var response = task.Result;

                                    if (response.IsSuccessStatusCode)
                                    {
                                        var result = response.Content.ReadAsAsync<List<StationImageUploadModel>>().Result;

                                        if (result != null && result.Count > 0)
                                        {
                                            fileUploaded = true;
                                            uploadResult = result[0];
                                        }
                                        else
                                        {
                                            SetMessages("Error", "Erroring Saving Image.");
                                        }

                                        // Read other header values if you want..
                                        foreach (var header in response.Content.Headers)
                                        {
                                            Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                                        }
                                    }
                                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                                    {
                                        var result = response.Content.ReadAsAsync<DeleteStationImageModel>().Result;
                                        errorMessage = result.ErrorMessage;
                                    }
                                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                                    {
                                        errorMessage = "Unauthorized";
                                    }
                                    else
                                    {
                                        string responseError
                                            = string.Format("Unable to save the image. Contact an administrator if the problem consists.\r\n Status Code: {0}",
                                                        response.StatusCode);
                                        string logError
                                            = string.Format("Unable to save the image. \r\n Status Code: {0} - {1}\r\n Response Body: {2}",
                                                        response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);

                                        SetMessages("Error", responseError);

                                        LogError LE = new LogError();
                                        LE.logError(logError, "StationImage_UploadBtn_Click_error_Saving_Message", "", createdBy, "");
                                    }
                                }
                                if (task.IsFaulted || task.IsCanceled)
                                {
                                    var exception = task.Exception;
                                    //should catch connection issues
                                    if (exception.InnerException.InnerException != null)
                                    {
                                        errorMessage = exception.InnerException.InnerException.Message;
                                    }
                                    else
                                    {
                                        throw task.Exception;
                                    }
                                }
                            });

                            taskUpload.Wait();
                            if (fileUploaded)
                            {
                                string successLabelMessage
                                    = string.Format("Station Image Added.  Station: {0},  Image: {1}", lblStationName.Text, uploadResult.FileName);

                                string redirect = string.Format("StationImages.aspx?stationIDSelected={0}&successLabelMessage={1}",
                                                         HiddenStationID.Value, successLabelMessage);

                                Response.Redirect(redirect, false);
                            }
                            else
                            {
                                SetMessages("Error", errorMessage);
                            }
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

        public void UpdateStationImage(StationImage model)
        {
            SetMessages();
            if (!ModelState.IsValid)
            {
                SetMessages("Error", "Correct all input errors");
            }
            else
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                        string updateStationImageBaseAddress
                                = string.Format("{0}/{1}/{2}", webApiBaseUrl, webApiStationImageController,
                                                                       "UpdateStationImage");

                        string hiddenStatID = HiddenStationID.Value;
                        int statID = 0;
                        bool statIDIsInt = Int32.TryParse(hiddenStatID, out statID);

                        string modifiedBy = "Not Found";
                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            modifiedBy = HttpContext.Current.User.Identity.Name;
                        }

                        UpdatePrimaryStationImageModel updatePrimaryStationImageModel = new UpdatePrimaryStationImageModel()
                        {
                            ID = model.ID,
                            StationID = statID,
                            Primary = model.Primary,
                            ModifiedBy = modifiedBy,
                            ImageTypeID = model.ImageType,
                            Description = model.Description,
                            PhysHabYear = 0,
                            Updated = true,
                            ErrorMessage = ""
                        };

                        UpdatePrimaryStationImageModel updatePrimaryStationImageModelResult = null;

                        bool fileUpdated = false;
                        string errorMessage = string.Empty;

                        Task taskUpdateStationImage = client.PostAsJsonAsync(updateStationImageBaseAddress, updatePrimaryStationImageModel).ContinueWith(task =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                var response = task.Result;

                                if (response.IsSuccessStatusCode)
                                {
                                    var result = response.Content.ReadAsAsync<UpdatePrimaryStationImageModel>().Result;

                                    if (result != null)
                                    {
                                        fileUpdated = true;
                                        updatePrimaryStationImageModelResult = result;
                                    }

                                    // Read other header values if you want..
                                    foreach (var header in response.Content.Headers)
                                    {
                                        Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                                    }
                                }
                                else if (response.StatusCode == HttpStatusCode.BadRequest)
                                {
                                    var result = response.Content.ReadAsAsync<DeleteStationImageModel>().Result;
                                    errorMessage = result.ErrorMessage;
                                }
                                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                                {
                                    errorMessage = "Unauthorized";
                                }
                                else
                                {
                                    errorMessage = string.Format("There was a problem Updating the Image. Station: {0}, Image: {1}, Status Code: {2} - {3}",
                                                                lblStationName.Text, model.FileName, response.StatusCode, response.ReasonPhrase);
                                }
                            }
                            if (task.IsFaulted || task.IsCanceled)
                            {
                                var exception = task.Exception;
                                //should catch connection issues
                                if (exception.InnerException.InnerException != null)
                                {
                                    errorMessage = exception.InnerException.InnerException.Message;
                                }
                                else
                                {
                                    throw task.Exception;
                                }
                            }
                        });

                        taskUpdateStationImage.Wait();
                        if (fileUpdated)
                        {
                            string successLabelMessage
                                = string.Format("Station Image Updated. Station: {0}, Image: {1}", lblStationName.Text, model.FileName);

                            string redirect = string.Format("StationImages.aspx?stationIDSelected={0}&successLabelMessage={1}",
                                                     HiddenStationID.Value, successLabelMessage);

                            Response.Redirect(redirect, false);
                        }
                        else
                        {
                            SetMessages("Error", errorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "UpdateStationImage", "", "");
                }
            }
        }

        public void DeleteStationImage(StationImage model)
        {
            try
            {
                SetMessages();

                if (!ModelState.IsValid)
                {
                    SetMessages("Error", "Correct all input errors");
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                        string deleteStationImageBaseAddress
                                = string.Format("{0}/{1}/{2}", webApiBaseUrl, webApiStationImageController,
                                                                       "DeleteStationImage");

                        DeleteStationImageModel deleteStationImageModel = new DeleteStationImageModel()
                        {
                            ID = model.ID,
                            FileName = model.FileName,
                            FileUrl = model.FileUrl,
                            Deleted = true,
                            ErrorMessage = ""
                        };

                        DeleteStationImageModel deleteStationImageModelResult = null;

                        bool fileDeleted = false;
                        string errorMessage = string.Empty;

                        Task taskDeleteStationImage = client.PostAsJsonAsync(deleteStationImageBaseAddress, deleteStationImageModel).ContinueWith(task =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                var response = task.Result;

                                if (response.IsSuccessStatusCode)
                                {
                                    var result = response.Content.ReadAsAsync<DeleteStationImageModel>().Result;

                                    if (result != null)
                                    {
                                        fileDeleted = true;
                                        deleteStationImageModelResult = result;
                                    }

                                    // Read other header values if you want..
                                    foreach (var header in response.Content.Headers)
                                    {
                                        Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                                    }
                                }
                                else if (response.StatusCode == HttpStatusCode.BadRequest)
                                {
                                    var result = response.Content.ReadAsAsync<DeleteStationImageModel>().Result;
                                    errorMessage = result.ErrorMessage;
                                }
                                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                                {
                                    errorMessage = "Unauthorized";
                                }
                                else
                                {
                                    errorMessage = string.Format("There was a problem Updating the Image. Station: {0}, Image: {1}, Status Code: {2} - {3}",
                                                                lblStationName.Text, model.FileName, response.StatusCode, response.ReasonPhrase);
                                }
                            }
                            if (task.IsFaulted || task.IsCanceled)
                            {
                                var exception = task.Exception;
                                //should catch connection issues
                                if (exception.InnerException.InnerException != null)
                                {
                                    errorMessage = exception.InnerException.InnerException.Message;
                                }
                                else
                                {
                                    throw task.Exception;
                                }
                            }
                        });

                        taskDeleteStationImage.Wait();
                        if (fileDeleted)
                        {
                            string successLabelMessage
                                = string.Format("Station Image Deleted. Station: {0}, Image: {1}", lblStationName.Text, deleteStationImageModelResult.FileName);

                            string redirect = string.Format("StationImages.aspx?stationIDSelected={0}&successLabelMessage={1}",
                                                     HiddenStationID.Value, successLabelMessage);

                            Response.Redirect(redirect, false);
                        }
                        else
                        {
                            SetMessages("Error", errorMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "DeleteStationImage", "", "");
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
            try
            {
                stationNameSearch.Text = "";

                int stationNumber = 0;

                bool stationNumberIsInt
                        = Int32.TryParse(stationNumberSearch.Text.Trim(), out stationNumber);
                if (stationNumberIsInt)
                {
                    int stationIDSelected = 0;

                    using (RiverWatchEntities _db = new RiverWatchEntities())
                    {
                        stationIDSelected = _db.Stations
                                        .Where(s => s.StationNumber == stationNumber)
                                        .Select(s => s.ID)
                                        .FirstOrDefault();
                    }

                    if (stationIDSelected > 0)
                    {
                        string redirect = "StationImages.aspx?stationIDSelected=" + stationIDSelected;

                        Response.Redirect(redirect, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchStationNumber_Click", "", "");
            }
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

        protected void StationImagesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetMessages();
        }

        protected void StationImagesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetMessages();
        }

        protected void Show(object sender, EventArgs e)
        {
            if (lblStationName != null && !string.IsNullOrEmpty(lblStationName.Text) &&
                !StationImagesGridView.Controls[0].Controls[0].FindControl("NoResultsPanel").Visible)
            {
                StationImagesGridView.Controls[0].Controls[0].FindControl("NoResultsPanel").Visible = true;
                StationImagesGridView.Height = new Unit("29px");
            }
        }

        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).AsyncPostBackErrorMessage = e.Exception.Message;
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
}