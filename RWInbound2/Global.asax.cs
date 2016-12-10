using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using RWInbound2;
using System.Web.UI;
using RWInbound2.Logic;
using System.Web.UI.WebControls;

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

        //void Application_PostMapRequestHandler(object sender, EventArgs e)
        //{
        //    Page aux = this.Context.Handler as Page;
        //    Exception exc = Server.GetLastError();
        //    if (aux != null)
        //    {
        //        //string exceptionMSG = exc.Message;
        //        aux.Error += new EventHandler(Application_Error);
        //        if (string.IsNullOrEmpty(aux.ErrorPage))
        //        {
        //            aux.ErrorPage = "~/ErrorPage.aspx?handler=Application_Error%20-%20Global.asax";
        //        }
        //    }
        //}

        void page_Error(object sender, EventArgs e)
        {
            //log error as you want
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                string test = string.Empty;
            }
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
                string errorHandler = "Application_Error - Global.asax";
                ExceptionUtility.LogException(exc, errorHandler);
                Session["Error"] = exc;
                Response.Redirect("~/ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                    false);
                Server.ClearError();
                //Server.Transfer("~/ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                //    true);               
            }            
        }
    }
}
