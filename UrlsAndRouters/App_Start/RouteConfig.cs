using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRouters
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var 1
            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute", myRoute);

            //var 2 first a specific roule - than general
            routes.MapRoute("ShopSchema2", "Shop/OldAction", new { controller = "Home", action = "Index" });
            routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });  
            routes.MapRoute("MyRoute1", "X{controller}/{action}"); //for variable elements can't use defoults segments
            routes.MapRoute("MyRoute2", "Public/{controller}/{action}", new { controller ="Customer", action ="List" }); //statics elements
            routes.MapRoute("MyRoute4", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            //routes.MapRoute("MyRoute3", "{controller}/{action}", new { controller="Home", action="Index"});
        }
    }
}
