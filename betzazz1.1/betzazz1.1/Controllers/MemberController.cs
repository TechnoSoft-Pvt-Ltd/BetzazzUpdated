using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace betzazz1._1.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult Bank()
        {
            return View();
        }
        public ActionResult Bets()
        {
            return View();
        }
       
    }
}