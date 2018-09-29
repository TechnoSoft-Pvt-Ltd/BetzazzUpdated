using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace betzazz1._1.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        public ActionResult UserControl()
        {
            return View();
        }
        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult ManageUser()
        {
            return View();
        }
        public ActionResult BetControl()
        {
            return View();
        }
        public ActionResult AddBets()
        {
            return View();
        }
        public ActionResult ManageBets()
        {
            return View();
        }
        public ActionResult BalanceControl()
        {
            return View();

        }
        public ActionResult BalanceTransaction()
        {
            return View();
        }
        public ActionResult UpdateBalance()
        {
            return View();
        }
        public ActionResult ManageBalance()
        {
            return View();
        }

    }
}