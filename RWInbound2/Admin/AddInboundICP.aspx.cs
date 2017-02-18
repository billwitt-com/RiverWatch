using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity.Validation;
using System.Reflection;

namespace RWInbound2.Admin
{
    public partial class AddInboundICP : System.Web.UI.Page
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

        public InboundICPFinal GetMetalBarCodeAndSampleID([QueryString]string inboundICPMetalsBarCodeSearchTerm = "",
                                                          [QueryString]string successLabelMessage = "")
        {
            try
            {
                var newInboundICPFinal = new InboundICPFinal();

                if(string.IsNullOrEmpty(inboundICPMetalsBarCodeSearchTerm))
                {
                    return null;
                }

                using (RiverWatchEntities _db = new RiverWatchEntities())
                {

                    var metalsBarCode = _db.MetalBarCodes
                                           .Where(m => m.LabID.Equals(inboundICPMetalsBarCodeSearchTerm))
                                           .FirstOrDefault();

                    if(metalsBarCode == null || metalsBarCode.LabID == null)
                    {
                        string errorMsg = string.Format("This Metals Bar Code {0} does not exist.", inboundICPMetalsBarCodeSearchTerm);
                        SetMessages("Error", errorMsg);
                        return null;
                    }

                    var inboundICPFinal = _db.InboundICPFinals
                                             .Where(i => i.CODE.Equals(inboundICPMetalsBarCodeSearchTerm))
                                             .FirstOrDefault();
                    if(inboundICPFinal != null)
                    {
                        string errorMsg 
                            = string.Format("A new Inbound ICP record already exists for this Metals Bar Code {0}.", 
                                            inboundICPMetalsBarCodeSearchTerm);
                        SetMessages("Error", errorMsg);
                        return null;
                    }

                    newInboundICPFinal.CODE = metalsBarCode.LabID;
                    newInboundICPFinal.tblSampleID = metalsBarCode.SampleID;                    
                }

                PropertyInfo isreadonly
                   = typeof(System.Collections.Specialized.NameValueCollection)
                           .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Clear();

                return newInboundICPFinal;
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "GetMetalBarCodeAndSampleID", "", "");
                return null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string inboundICPMetalsBarCodeSearchTerm = inboundICPMetalsBarCodeSearch.Text;
                string redirect = "AddInboundICP.aspx?inboundICPMetalsBarCodeSearchTerm=" + inboundICPMetalsBarCodeSearchTerm;

                Response.Redirect(redirect, false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearch_Click", "", "");
            }
        }

        protected void btnSearchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddInboundICP.aspx", false);
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "btnSearchRefresh_Click", "", "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchForMetalsBarCode(string prefixText, int count)
        {
            List<string> metalsBarCodes = new List<string>();

            try
            {
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {
                    metalsBarCodes = _db.MetalBarCodes
                                        .Where(m => m.LabID.Contains(prefixText))
                                        .Select(m => m.LabID).ToList();

                    return metalsBarCodes;
                }
            }
            catch (Exception ex)
            {
                AddInboundICP addInboundICP = new AddInboundICP();
                addInboundICP.HandleErrors(ex, ex.Message, "SearchForMetalsBarCode", "", "");
                return metalsBarCodes;
            }
        }

        public void InsertNewInboundICPFinal(InboundICPFinal model)
        {
            try
            {
                SetMessages();
                using (RiverWatchEntities _db = new RiverWatchEntities())
                {                  
                    //var sampleIDinInboundICPTable
                    //        = _db.InboundICPFinals
                    //             .Where(i => i.tblSampleID == model.tblSampleID || i.CODE.Equals(model.CODE))
                    //             .FirstOrDefault();

                    // changed code to only look for barcode 
                    var sampleIDinInboundICPTable
                    = _db.InboundICPFinals
                        .Where(i => i.CODE.ToUpper().Equals(model.CODE.ToUpper()))
                            .FirstOrDefault();

                    if (sampleIDinInboundICPTable != null)
                    {
                        string errorMsg = string.Format("This Bar Code already exists: Code:{0}.", model.CODE);
                        SetMessages("Error", errorMsg);
                    }
                    else
                    {
                        string createdBy = "Unknown";

                        var newInboundICPFinal = new InboundICPFinal();
                        newInboundICPFinal = model;

                        if (this.User != null && this.User.Identity.IsAuthenticated)
                        {
                            createdBy = HttpContext.Current.User.Identity.Name;
                        }

                        newInboundICPFinal.CreatedBy = createdBy;
                        newInboundICPFinal.CreatedDate = DateTime.Now;
                        newInboundICPFinal.Valid = true;
                        newInboundICPFinal.Saved = false; 
                //        newInboundICPFinal.Saved = true;
                        newInboundICPFinal.Edited = false;

                        // added bw to make sure a null did not come from the model, which breaks the check box binding... 
                        if(model.COMPLETE == null)
                        {
                            model.COMPLETE = false; 
                        }
                        var newInboundICPOrigional = new InboundICPOrigional()
                        {
                            CODE = model.CODE,
                            tblSampleID = model.tblSampleID,
                            DUPLICATE = model.DUPLICATE,
                            AL_D = model.AL_D,
                            AL_T = model.AL_T,
                            AS_D = model.AS_D,
                            AS_T = model.AS_T,
                            CA_D = model.CA_D,
                            CA_T = model.CA_T,
                            CD_D = model.CD_D,
                            CD_T = model.CD_T,
                            CU_D = model.CU_D,
                            CU_T = model.CU_T,
                            FE_D = model.FE_D,
                            FE_T = model.FE_T,
                            PB_D = model.PB_D,
                            PB_T = model.PB_T,
                            MG_D = model.MG_D,
                            MG_T = model.MG_T,
                            MN_D = model.MN_D,
                            MN_T = model.MN_T,
                            SE_D = model.SE_D,
                            SE_T = model.SE_T,
                            ZN_D = model.ZN_D,
                            ZN_T = model.ZN_T,
                            NA_D = model.NA_D,
                            NA_T = model.NA_T,
                            K_D = model.K_D,
                            K_T = model.K_T,
                            ANADATE = model.ANADATE,
                            COMPLETE = model.COMPLETE,
                            DATE_SENT = model.DATE_SENT,
                            Comments = model.Comments,
                            CreatedBy = createdBy,
                            CreatedDate = DateTime.Now,
                            Valid = true,
                         //   Saved = true,
                            Saved = false,
                            Edited = false                            
                        };

                        _db.InboundICPFinals.Add(newInboundICPFinal);
                        _db.InboundICPOrigionals.Add(newInboundICPOrigional);
                        _db.SaveChanges();

                        SetMessages("Success", "New InboundICP Successfully Added.");
                    }
                }
            }
            catch (Exception ex)
            {
                HandleErrors(ex, ex.Message, "InsertNewInboundICPFinal", "", "");
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
}