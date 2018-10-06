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
using System.Collections;

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
          //  ViewBag.TestCount = TestCount;
            Session["TestCount"] = TestCount;
            ViewBag.ArrTestData = ArrTestData;                       
            ViewBag.LeagueName = GBLClass.TestLeagueName;
            Session["LeagueName"] = GBLClass.TestLeagueName; 
            string id = GBLClass.EventId;
            Evntid = id.Split('@');
            ViewBag.Evntid = Evntid;         
            //Cricket for Test Event id
            ArrayList Eid = new ArrayList();
            for (int i = 0; i < ArrTestData.Length; i++)
            {
                Eid.Add(Evntid[i]);
                Session["Evntid"] = Eid;
            }
            // for Test Event Name
            ArrayList displayDetail1 = new ArrayList();
            for (int j = 0; j < ArrTestData.Length; j++)
            {
                displayDetail1.Add(ArrTestData[j]);
                Session["TstEventName"] = displayDetail1;
            }




            string GetT20Data = GBLClass.T20Data;
            ArrT20Data = GetT20Data.Split('@');
            int T20Count = ArrT20Data.Length;
            //ViewBag.T20Count = T20Count;
            Session["T20Count"] = T20Count;
            ViewBag.ArrT20Data = ArrT20Data;
            ViewBag.T20LeagueName = GBLClass.T20LeagueName;
            Session["T20LeagueName"] = GBLClass.T20LeagueName;
            string idT20 = GBLClass.EventId1;
            T20EventId = idT20.Split('@');
            ViewBag.T20EventId = T20EventId;
            //Cricket for T20 Event id
            ArrayList T20Eid = new ArrayList();
            for (int k = 0; k < ArrT20Data.Length; k++)
            {
                T20Eid.Add(T20EventId[k]);
                Session["T20Evntid"] = T20Eid;
            }
            // for Test Event Name
            ArrayList T20EventName = new ArrayList();
            for (int l = 0; l < ArrT20Data.Length; l++)
            {
                T20EventName.Add(ArrT20Data[l]);
                Session["T20EventName"] = T20EventName;
            }


            string GetODIData = GBLClass.ODIData;
            ArrODIData = GetODIData.Split('@');
            int ODICount = ArrODIData.Length;
            Session["ODICount"] = ODICount;      
            ViewBag.ArrODIData = ArrODIData;
            ViewBag.ODILeagueName = GBLClass.ODILeagueName;
            Session["ODILeagueName"] = GBLClass.ODILeagueName;
            string ODIid = GBLClass.EventId2;
            ODIEventId = ODIid.Split('@');
            ViewBag.ODIEventId = ODIEventId;

            //Cricket for ODI Event id
            ArrayList ODIEid = new ArrayList();
            for (int k = 0; k < ArrODIData.Length; k++)
            {
                ODIEid.Add(ODIEventId[k]);
                Session["ODIEvntid"] = ODIEid;
            }
            // for Test Event Name
            ArrayList ODIEventName = new ArrayList();
            for (int l = 0; l < ArrODIData.Length; l++)
            {
                ODIEventName.Add(ArrODIData[l]);
                Session["ODIEventName"] = ODIEventName;
            }



            int CRTotalEvent = TestCount + T20Count + ODICount;
            ViewBag.CRTotalEvent = CRTotalEvent;
            Session["CRTotalEvent"] = CRTotalEvent;
            return View();
        }

       

        // Methods for PreMatch Cricket and Football
        public ActionResult PreMatch()
        {
           
            return View();
        }

    }
}