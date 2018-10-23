using betzazz1._1.BusnessLogics;
using betzazz1._1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace betzazz1._1.Controllers
{
    public class CricketController : Controller
    {
        Global GBLClass = new Global();
        // GET: Cricket
        public ActionResult Inplay()
        {

            return View();
        }
        public ActionResult PreMatch()
        {
            return View();
        }
        public void InPlayCR()
        {
            string[] ArrTestData;
            string[] ArrT20Data;
            string[] ArrODIData;
            string[] Evntid;
            string[] T20EventId;
            string[] ODIEventId;

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



           // FottballInplay = GBLClass.FotballIPGetJson;

            // var deserialized = JsonConvert.DeserializeObject<LiveList>(FottballInplay);

            //var serilezer = new JavaScriptSerializer();



           // LiveEventList LiveFT = serilezer.Deserialize<LiveEventList>(FottballInplay);

            //int count = LiveFT.results.Count();
            //ArrayList newList = new ArrayList();
            //ArrayList newList1 = new ArrayList();
            //ArrayList Arr1 = new ArrayList();
            //for (int i=0;i<count;i++)
            //{
            //    ArrayList ArrList = new ArrayList();
            //    string MatchId= LiveFT.results[i].id;
            //    string Sportid = LiveFT.results[i].sport_id;
            //    string Time= LiveFT.results[i].time;
            //    string TimeStatus= LiveFT.results[i].time_status;
            //    string leagueId= LiveFT.results[i].league.id;
            //    string leagueName= LiveFT.results[i].league.name;
            //    string EventName= LiveFT.results[i].home.name+" VS "+ LiveFT.results[i].away.name;


            //    ArrList.Add(MatchId);
            //    ArrList.Add(Sportid);
            //    ArrList.Add(Time);
            //    ArrList.Add(TimeStatus);
            //    ArrList.Add(leagueName);
            //    ArrList.Add(EventName);

            //    if (leagueName== ArrList[4].ToString())
            //    {
            //        newList.Add(ArrList);
            //    }
            //    else if(leagueName != ArrList[4].ToString())
            //    {
            //        newList1.Add(ArrList);
            //    }

            //   // Arr1.Add(newList);
            //}           
           // ViewBag.LiveFT = LiveFT;

        }
        public ActionResult CricketEvents(string[] ArrPMTestData, string[] PMTestEvntid, string[] ArrPMT20Data, string[] PMT20Evntid, string[] ArrPMODIData, string[] PMODIEvntid)
        {

            // For Inplay  Events List

            InPlayCR();



            // For Pre Match Events List

            GBLClass.PreMatch();

            // For Test Matches.
            string GetTestData = GBLClass.PMTestData;

            string EDate = GBLClass.PMTestEventDate;
            string[] TestEventDate = EDate.Split('@');
            ViewBag.TestEventDate = TestEventDate;

            ArrPMTestData = GetTestData.Split('@');
            int PMTestCount = ArrPMTestData.Length;
            //  ViewBag.TestCount = TestCount;
            Session["PMTestCount"] = PMTestCount;
            ViewBag.ArrPMTestData = ArrPMTestData;
            ViewBag.PMLeagueName = GBLClass.PMTestLeagueName;
            Session["PMLeagueName"] = GBLClass.PMTestLeagueName;
            string id = GBLClass.PMEventId;
            PMTestEvntid = id.Split('@');
            ViewBag.PMTestEvntid = PMTestEvntid;
            //Cricket for Test Event id
            ArrayList Eid = new ArrayList();
            for (int i = 0; i < ArrPMTestData.Length; i++)
            {
                Eid.Add(PMTestEvntid[i]);
                Session["PMEvntid"] = Eid;
            }
            // for Test Event Name
            ArrayList displayDetail1 = new ArrayList();
            for (int j = 0; j < ArrPMTestData.Length; j++)
            {
                displayDetail1.Add(ArrPMTestData[j]);
                Session["PMTstEventName"] = displayDetail1;
            }


            // For Tweenty 20
            string GetT20tData = GBLClass.PMT20Data;
            string EDatet20 = GBLClass.PMT20EventDate;
            string[] T20EventDate = EDatet20.Split('@');
            ViewBag.T20EventDate = T20EventDate;
            ArrPMT20Data = GetT20tData.Split('@');
            int PMT20Count = ArrPMT20Data.Length;
            //  ViewBag.TestCount = TestCount;
            Session["PMT20Count"] = PMT20Count;
            ViewBag.ArrPMT20Data = ArrPMT20Data;
            ViewBag.PMT20LeagueName = GBLClass.PMT20LeagueName;
            Session["PMT20LeagueName"] = GBLClass.PMT20LeagueName;
            string idT20 = GBLClass.PMT20EventId;
            PMT20Evntid = idT20.Split('@');
            ViewBag.PMT20Evntid = PMT20Evntid;
            //Cricket for Test Event id
            ArrayList EidT20 = new ArrayList();
            for (int i = 0; i < ArrPMT20Data.Length; i++)
            {
                EidT20.Add(PMT20Evntid[i]);
                Session["PMEvntidT20"] = EidT20;
            }
            // for Test Event Name
            ArrayList displayDetailT20 = new ArrayList();
            for (int j = 0; j < ArrPMT20Data.Length; j++)
            {
                displayDetailT20.Add(ArrPMT20Data[j]);
                Session["PMT20EventName"] = displayDetailT20;
            }

            // For ODI
            string GetODIData = GBLClass.PMODIData;

            string EDateODI = GBLClass.PMODIEventDate;
            string[] ODIEventDate = EDateODI.Split('@');
            ViewBag.ODIEventDate = ODIEventDate;
            ArrPMODIData = GetODIData.Split('@');
            int PMODICount = ArrPMODIData.Length;
            ViewBag.PMODIEventDate = GBLClass.PMODIEventDate;
            //  ViewBag.TestCount = TestCount;
            Session["PMODICount"] = PMODICount;
            ViewBag.ArrPMODIData = ArrPMODIData;
            ViewBag.PMODILeagueName = GBLClass.PMODILeagueName;
            Session["PMODILeagueName"] = GBLClass.PMODILeagueName;
            string idODI = GBLClass.PMODIEventId;
            PMODIEvntid = idODI.Split('@');
            ViewBag.PMODIEvntid = PMODIEvntid;
            //Cricket for Test Event id
            ArrayList EidODI = new ArrayList();
            for (int i = 0; i < ArrPMODIData.Length; i++)
            {
                EidODI.Add(PMODIEvntid[i]);
                Session["ODIPMEvntid"] = EidODI;
            }
            // for Test Event Name
            ArrayList displayDetailODI = new ArrayList();
            for (int j = 0; j < ArrPMODIData.Length; j++)
            {
                displayDetailODI.Add(ArrPMODIData[j]);
                Session["PMODIEventName"] = displayDetailODI;
            }

            int PMCRTotalEvent = PMTestCount + PMT20Count + PMODICount;
            ViewBag.CRTotalEvent = PMCRTotalEvent;
            Session["PMCRTotalEvent"] = PMCRTotalEvent;
            return View();
        }
    }
}