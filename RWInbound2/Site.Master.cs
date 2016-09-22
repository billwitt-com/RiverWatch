using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWInbound2
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    // just log this and move on, don't know why this happens now, did  not happen before 09/09/2016

                    string nam = "Not Known - in method";
                    string msg = "Validation of Anti-XSRF token failed.";
                    LogError LE = new LogError();
                    LE.logError(msg, "In master page, in XSRF detection code", "Viewstate user name: " + (string)ViewState[AntiXsrfUserNameKey], nam, "");
                  //  throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        // see if sometime I can find the code to get date from a compiled file
        protected void Page_Load(object sender, EventArgs e)
        {
            int role = 1;
            RiverWatchEntities RWE = new RiverWatchEntities();
            string con = RWE.Database.Connection.ConnectionString;
            string ds = "Data Source"; 
            int idx = con.IndexOf(ds);
            string dSource = con.Substring(idx + 3 + ds.Length, 10); 

            string filepath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            const string URIPrefx = "file:///";
            if (filepath.StartsWith(URIPrefx)) filepath = filepath.Substring(URIPrefx.Length);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filepath);
            string VerMsg = fileInfo.LastWriteTime.ToString("yyyy-MM-dd");

            lblVersion.Text = "                                 VER: " + VerMsg + "      DS: " + dSource;    

      // now manage menu items using users role and 

            if (Session["Role"] != null)
            {
                role = (int)Session["Role"];    // get users role
            }
            else
                role = 1; 

          

            var R = from r in RWE.ControlPermissions
                    where r.PageName.ToUpper() == "MASTER"
                    select r;

            int? Q = (from r in R
                     where r.ControlID.ToUpper() == "SAMPLES"
                     select r.RoleValue).FirstOrDefault();

            if (Q != null)
            {
                if (role >= Q.Value)
                    Samples.Visible = true;

                else               
                    Samples.Visible = false;               
            }

            int? Q1 = (from r in R
                      where r.ControlID.ToUpper() == "VALIDATION"
                      select r.RoleValue).FirstOrDefault();

            if (Q1 != null)
            {
                if (role >= Q1.Value)
                    Validation.Visible = true;

                else
                    Validation.Visible = false;
            }

            // allow data tab to be seen by all

            //int? Q2 = (from r in R
            //           where r.ControlID.ToUpper() == "DATA"
            //           select r.RoleValue).FirstOrDefault(); 

            //if (Q2 != null)
            //{
            //    if (role >= Q2.Value)
            //        Data.Visible = true;

            //    else
            //        Data.Visible = false;
            //}

            int? Q3 = (from r in R
                       where r.ControlID.ToUpper() == "Reports".ToUpper()
                       select r.RoleValue).FirstOrDefault();

            if (Q3 != null)
            {
                if (role >= Q3.Value)
                    Reports.Visible = true;

                else
                    Reports.Visible = false;
            }

            int? Q4 = (from r in R
                       where r.ControlID.ToUpper() == "Applications".ToUpper()
                       select r.RoleValue).FirstOrDefault();

            if (Q4 != null)
            {
                if (role >= Q4.Value)
                    Applications.Visible = true;

                else
                    Applications.Visible = false;
            }

            int? Q5 = (from r in R
                       where r.ControlID.ToUpper() == "Edit".ToUpper()
                       select r.RoleValue).FirstOrDefault();

            if (Q5 != null)
            {
                if (role >= Q5.Value)
                    Edit.Visible = true;

                else
                    Edit.Visible = false;
            }

            int? Q6 = (from r in R
                       where r.ControlID.ToUpper() == "Admin".ToUpper()
                       select r.RoleValue).FirstOrDefault();

            if (Q6 != null)
            {
                if (role >= Q6.Value)
                    Admin.Visible = true;

                else
                    Admin.Visible = false;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //string DupsUniqueID = this.Controls[0].UniqueID; 
            ////FormView1.Controls[0].UniqueID; // CC[0].UniqueID;   // get the whole name as made up by ASP web page

            //string homeMenu = DupsUniqueID + "$" + "Home";

            //HyperLink HL = this.FindControl(homeMenu) as HyperLink;
            //HL.ResolveUrl("~/index.aspx");
            //HL.NavigateUrl = "~/index.aspx";    // set link in code!
         
            //Label1.Text = homeMenu;6

        }
    }
}