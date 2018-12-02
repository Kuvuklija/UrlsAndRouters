using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRouters.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id=123)
        {
            return View();
        }
    }
}