using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StormChaseCheckIn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CheckMeIn",
                url: "check-me-in/",
                defaults: new { controller = "Home", action = "CheckMeIn", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MyCheckIns",
                url: "my-checkins/",
                defaults: new { controller = "Home", action = "MyCheckIns", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Settings",
                url: "settings/",
                defaults: new { controller = "Home", action = "Settings", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Logoff",
                url: "logoff/",
                defaults: new { controller = "Account", action = "LogOff", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "login/",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
