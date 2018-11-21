using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Routing.Constraints;
using UrlsAndRouters.Infrastructure;

namespace UrlsAndRouters
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();//turn on atributs routing
            //var 1
            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute", myRoute);

            //var 2 first a specific roule - than general
            routes.MapRoute("ShopSchema2", "Shop/OldAction", new { controller = "Home", action = "Index" });
            routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });
            routes.MapRoute("MyRoute1", "X{controller}/{action}"); //for variable elements can't use defoults segments
            routes.MapRoute("MyRoute2", "Public/{controller}/{action}", new { controller = "Customer", action = "List" }); //statics elements
            //routes.MapRoute("MyRoute4", "Home/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "UrlsAndRouters.Controllers" });
            //routes.MapRoute("MyRoute5", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "UrlsAndRouters.Controllers.AdditionalControllers" });

            //routes.MapRoute("MyRoute3", "{controller}/{action}", new { controller="Home", action="Index"});

            //Route myRoute = routes.MapRoute("MyRoute6","{controller}/{action}/{id}/{*catchall}", new{controller="Home",action="Index",id=UrlParameter.Optional},
            //    new[] {"UrlsAndRouters.Controllers"});
            //myRoute.DataTokens["UseNamespaceFallback"] = false; //turn of searching in other spaces

            routes.MapRoute("Limit_1", "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new
                {
                    controller = "^H.*",
                    action = "^Index$|^About$", //prohibit all controllers don't begining on "H"
                    httpMethod = new HttpMethodConstraint("GET"),
                    //id = new RangeRouteConstraint(10,20)
                    id=new CompoundRouteConstraint(new IRouteConstraint[] {
                    new AlphaRouteConstraint(),
                    new MinLengthRouteConstraint(6)
                    })
                },
                new[] { "UrlsAndRouters.Controllers" });

            routes.MapRoute("ChromeRoute", "{controller}/{action}/{*catchall}",
                new { controller = "Home", action = "Index" },
                new { customConstaint = new UserAgentConstraint("Windows") },
                new[] { "UrlsAndRouters.Controllers.AdditionalControllers" }
            );
        }
    }
}
