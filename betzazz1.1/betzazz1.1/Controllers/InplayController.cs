using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace betzazz1._1.Controllers
{
    public class InplayController : Controller
    {
        // GET: Inplay
        public ActionResult Cricket()
        {
            ViewBag.ShowDiv = false;
            return View();
        }
        public ActionResult Football()
        {
            return View();
        }
        public ActionResult Sport()
        {
            return View();
        }
    }
}