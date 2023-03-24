using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.ServiceModel.Activation;


namespace monacos.us_mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                            name: "Standard",
                            url: "{controller}/{action}/{clientid}/{campaignid}/{id}",
                            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                        );


        }
    }
}