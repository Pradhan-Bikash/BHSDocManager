using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace BPWEBAccessControl
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            
            // This is stabdard one - 
            //routes.EnableFriendlyUrls(settings);

            // This is my modified one - B.Sinha
            routes.EnableFriendlyUrls(settings, new bplib.BPWebFormsFriendlyUrlResolver());
        }
    }
}
