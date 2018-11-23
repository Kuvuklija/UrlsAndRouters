using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRouters.Controllers.AdditionalControllers
{
    public class HomeOldController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Controller = "Additional Controllers - Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }
    }
}