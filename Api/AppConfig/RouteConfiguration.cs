using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api
{
    public class RouteConfiguration
    {
        public static void Register(HttpConfiguration config)
        {
            // Use attribute routing
            config.MapHttpAttributeRoutes();

            // Default route
            config.Routes.MapHttpRoute(
                name: "AllCustomers",
                routeTemplate: "customers",
                defaults: new { controller = "Customers", action = "Get" }
                );
        }
    }
}