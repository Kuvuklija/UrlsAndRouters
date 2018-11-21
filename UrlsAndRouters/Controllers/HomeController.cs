using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRouters.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult CustomVariable(string id="DefaultID") {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            // ViewBag.CustomVariable = RouteData.Values["id"]; //получаем значение маршрута в контроллере, если id не в виде параметра
            //ViewBag.CustomVariable = id ?? "<no value>"; //если у параметра id нет значения по умолчанию 
            ViewBag.CustomVariable = id;
            return View();
        }
    }
}