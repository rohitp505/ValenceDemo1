using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ValenceDemo.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult ContactDetails()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}