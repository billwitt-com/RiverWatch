using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2.Edit
{
    public partial class EditBioResultsType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validate initially to force asterisks
                // to appear before the first roundtrip.
                Validate();
            }
        }

        public IQueryable<tlkBioResultsType> GetBioResultsTypes()
        {
            try
            {
                RiverWatchEntities _db = new RiverWatchEntities();

                IQueryable<tlkBioResultsType> bioResultsTypes = _db.tlkBioResultsTypes;

                return bioResultsTypes;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetBioResultsTypes", "", "");
                return null;
            }
        }

        public void UpdateBioResultsType(tlkBioResultsType model)
        {
            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    var bioResultsTypeToUpdate = _db.tlkBioResultsTypes.Find(model.ID);

                    bioResultsTypeToUpdate.Code = model.Code;
                    bioResultsTypeToUpdate.Description = model.Description;
                    //activityCategoryToUpdate.Valid = 
                    if (this.User != null && this.User.Identity.IsAuthenticated)
                    {
                        bioResultsTypeToUpdate.UserLastModified
                            = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        bioResultsTypeToUpdate.UserLastModified = "Unknown";
                    }

                    bioResultsTypeToUpdate.DateLastModified = DateTime.Now;
                    _db.SaveChanges();

                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "BioResults Type Updated";
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "UpdateBioResultsType", "", "");
            }
        }

        public void DeleteBioResultsType(tlkBioResultsType model)
        {
            using (RiverWatchEntities _db = new RiverWatchEntities())
            {
                try
                {
                    var bioResultsTypeToDelete = _db.tlkBioResultsTypes.Find(model.ID);
                    _db.tlkBioResultsTypes.Remove(bioResultsTypeToDelete);
                    _db.SaveChanges();
                    ErrorLabel.Text = "";
                    SuccessLabel.Text = "BioResults Type Deleted";
                }
                catch (Exception ex)
                {
                    HandleErrors(ex, ex.Message, "DeleteBioResultsType", "", "");
                }
            }
        }

        public void AddNewBioResultsType(object sender, EventArgs e)
        {
            try
            {
                string strCode = ((TextBox)BioResultsTypeGridView.FooterRow.FindControl("NewCode")).Text;
                string description = ((TextBox)BioResultsTypeGridView.FooterRow.FindControl("NewDescription")).Text;

                if (string.IsNullOrEmpty(strCode))
                {
                    SuccessLabel.Text = "";
                    ErrorLabel.Text = "Code field is required.";
                }
                else
                {
                    int code;
                    bool convertToInt = int.TryParse(strCode, out code);
                    if (!convertToInt)
                    {
                        SuccessLabel.Text = "";
                        ErrorLabel.Text = "Code field must be an integer number.";
                    }
                    else
                    {
                        var newBioResultsType = new tlkBioResultsType()
                        {
                            Code = code,
                            Description = description,
                            Valid = true,
                            DateLastModified = DateTime.Now
                        };

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            newBioResultsType.UserLastModified
                                = HttpContext.Current.User.Identity.Name;
                        }
                        else
                        {
                            newBioResultsType.UserLastModified = "Unknown";
                        }

                        using (RiverWatchEntities _db = new RiverWatchEntities())
                        {
                            _db.tlkBioResultsTypes.Add(newBioResultsType);
                            _db.SaveChanges();
                            ErrorLabel.Text = "";
                            SuccessLabel.Text = "New BioResults Type Added";
                            Response.Redirect("EditBioResultsType.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "AddNewBioResultsType", "", "");
            }
        }

        private void HandleErrors(Exception ex, string msg, string fromPage,
                                                string nam, string comment)
        {
            LogError LE = new LogError();
            LE.logError(msg, fromPage, ex.StackTrace.ToString(), nam, comment);

            SuccessLabel.Text = "";
            ErrorLabel.Text = ex.Message;
        }
    }
}