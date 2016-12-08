using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using RWInbound2;

namespace RWInbound2
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
          //  AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            //string ers = "Error Here";
            //System.Type TS = sender.GetType(); 
            //System.Type TE = e.GetType(); 

            // Get last error from the server
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                if (exc.InnerException != null)
                {
                    exc = new Exception(exc.InnerException.Message);
                    Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                        true);
                }
            }
        }
    }
}
