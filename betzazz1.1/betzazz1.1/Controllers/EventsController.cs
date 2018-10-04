using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using betzazz1._1.BusnessLogics;
using betzazz1._1.Models;
using System.Web.Script.Serialization;

namespace betzazz1._1.Controllers
{
    public class EventsController : Controller
    {
        Global GBLClass = new Global();
        LiveEventList Live = new LiveEventList();

        //SqlCommand cmd = new SqlCommand();
        public static int crleagueid { get; set; }
        public static int ftleagueid { get; set; }
        public static string LeagueName { get; set; }

        // Methods for Inplay Cricket and Football
        public ActionResult InPlay()
        {
            test ts=new test();
                GBLClass.InplayCR();

            var model = new ViewModel
            {
                Links = new List<string>
                {
                    //GBLClass.LeagueName,
                    GBLClass.EventId,
                    GBLClass.EventName,
                }
                //LeagueName = GBLClass.LeagueName,
                //Eventid = GBLClass.EventId,
                //EventName = GBLClass.EventName,
            };

            return View(model);
        }

       

        // Methods for PreMatch Cricket and Football
        public ActionResult PreMatch()
        {
           
            return View();
        }

    }
}