using betzazz1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace betzazz1._1.Controllers
{
    public class TennisController : Controller
    {
        // GET: Tennis
        public ActionResult Inplay()
        {
            return View();
        }
        public ActionResult PreMatch()
        {
            return View();
        }
        public ActionResult Index()
        {
            var model = new ViewModel
            {
                Links = new List<string>
                    {
                        "Hello",
                        "Goodbye",
                        "Seeya"
                    }
            };
            return View(model);
        }
        public ActionResult TennisEvents()
        {
            return View();
        }
    }
}