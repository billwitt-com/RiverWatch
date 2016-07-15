using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace RWInbound2
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();             // removed these lines since they were not in the origional code, that works
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls();
        }
    }
}
