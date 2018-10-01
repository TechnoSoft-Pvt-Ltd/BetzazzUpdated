using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace betzazz1._1.Controllers
{
    public class CricketController : Controller
    {
        // GET: Cricket
        public ActionResult Inplay()
        {
            return View();
        }
        public ActionResult PreMatch()
        {
            return View();
        }
    }
}