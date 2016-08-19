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
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        // see if sometime I can find the code to get date from a compiled file
        protected void Page_Load(object sender, EventArgs e)
        {
            // get date and time from RWInbound2.dll 
           // string filename = "RWInbound2.dll";
           // string locDir = System.IO.Directory.GetCurrentDirectory();
           // string fullDir = locDir + "\\bin\\" + filename;

           //DateTime writeTime =  System.IO.Directory.GetLastAccessTime(fullDir);

          
        }
        // this works to control menu items, so can be used to hide or deactivate menu items depending on user permissions 
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