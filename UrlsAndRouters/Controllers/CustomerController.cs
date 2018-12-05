using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRouters.Controllers
{
    //[RoutePrefix("Global")]
    [RouteArea("Services")]
    public class CustomerController : Controller
    {
        [Route("~/Test",Name ="AddTest")]
        public ActionResult Index(){
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult List() {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "List";
            return View("ActionName");
        }

        [Route("Users/Add/{user}/{id:int}",Name ="Vasja")] //parametrs type "id" had limited
        public string Create(string user, int id) {
            return string.Format("User: {0}, ID {1}", user, id);
        }

        [Route("Users/Add/{user}/{password:alpha:length(6)}")]
        public string ChangePass(string user, string password) {
            return string.Format("ChangePass Method. User: {0}, Password: {1}", user, password);
        }
    }
}
