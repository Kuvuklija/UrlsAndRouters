using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace UrlsAndRouters.Infrastructure
{
    public class LegacyRoute : RouteBase {
        private string[] urls;

        public  LegacyRoute(params string[] targetUrls) {
            urls = targetUrls;
        }
        //incoming urls
        public override RouteData GetRouteData(HttpContextBase httpContext){
        
            RouteData result = null;

            string requestedUrl = httpContext.Request.AppRelativeCurrentExecutionFilePath;

            if (urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase)) {
                    result = new RouteData(this, new MvcRouteHandler());
                    result.Values.Add("controller", "Legacy");
                    result.Values.Add("action", "GetLegacyURL");
                //при открытии корневой страницы (когда регятся маршруты в RouteConfige) принимает ~/.Нужно, чтоб сразу на legacy странице
                //сайт стартовал--->хрень какая-то, коментим условие
                    result.Values.Add("legacyURL", requestedUrl);
                }
            return result;
        }

        //outcoming urls
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;

            if(values.ContainsKey("legacyURL") && urls.Contains((string)values["legacyURL"], StringComparer.OrdinalIgnoreCase)) {

                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyURL"]).Substring(1));
            }
            return result;
        }

    }
}