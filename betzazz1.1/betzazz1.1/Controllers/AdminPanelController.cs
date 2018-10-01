using betzazz1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace betzazz1._1.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        public ActionResult UserControl()
        {
            option_247betEntities p = new option_247betEntities();
            var UC = p.SP_USERCONTROAL().ToList();
            ViewBag.userdetails = UC;
            return View();

        }
        public ActionResult AddUser()
        {
            shakebEntities2 r = new shakebEntities2();
            var data = r.sp_s_Reg().ToList();
            ViewBag.userdetails = data;
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