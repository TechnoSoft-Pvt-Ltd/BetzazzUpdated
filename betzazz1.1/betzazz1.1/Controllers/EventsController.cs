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
        public static string T20LeagueName { get; set; }
        public static string ODILeagueName { get; set; }

        // Methods for Inplay Cricket and Football
        public ActionResult InPlay(string[] ArrTestData, string[] ArrT20Data, string[] ArrODIData,string[] Evntid, string[] T20EventId, string[] ODIEventId)
        {
            //test ts=new test();
                GBLClass.InplayCR();
           

            string GetTestData = GBLClass.TestData;
            ArrTestData = GetTestData.Split('@');
            int TestCount = ArrTestData.Length;
            ViewBag.TestCount = TestCount;
            ViewBag.ArrTestData = ArrTestData;
            ViewBag.LeagueName = GBLClass.TestLeagueName;
            string id = GBLClass.EventId;
            Evntid = id.Split('@');
            ViewBag.Evntid = Evntid;
            

            string GetT20Data = GBLClass.T20Data;
            ArrT20Data = GetT20Data.Split('@');
            int T20Count = ArrTestData.Length;
            ViewBag.T20Count = T20Count;
            ViewBag.ArrT20Data = ArrT20Data;
            ViewBag.T20LeagueName = GBLClass.T20LeagueName;
            string idT20 = GBLClass.EventId1;
            T20EventId = idT20.Split('@');
            ViewBag.T20EventId = T20EventId;



            string GetODIData = GBLClass.ODIData;
            ArrODIData = GetODIData.Split('@');
            int ODICount = ArrTestData.Length;
            ViewBag.ODICount = ODICount;
            ViewBag.ArrODIData = ArrODIData;
            ViewBag.ODILeagueName = GBLClass.ODILeagueName;
            string ODIid = GBLClass.EventId2;
            ODIEventId = ODIid.Split('@');
            ViewBag.ODIEventId = ODIEventId;

            int CRTotalEvent = TestCount + T20Count + ODICount;
            ViewBag.CRTotalEvent = CRTotalEvent;
            return View();
        }

       

        // Methods for PreMatch Cricket and Football
        public ActionResult PreMatch()
        {
           
            return View();
        }

    }
}