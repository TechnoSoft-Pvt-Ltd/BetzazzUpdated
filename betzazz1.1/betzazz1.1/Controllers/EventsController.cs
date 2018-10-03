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
            bool Doprocessing = true;
            while(Doprocessing)
            {
                try
                {
                    GBLClass.GetIPEventList();
                    string cripjson = GBLClass.CricketIPGetJson.ToString();
                    string ftipjson = GBLClass.FotballIPGetJson.ToString();
                    var serializer = new JavaScriptSerializer();

                    LiveEventList CRLiveList = serializer.Deserialize<LiveEventList>(cripjson);
                    int matchCount = CRLiveList.results.Count();
                    if (CRLiveList.success == "1" && CRLiveList.results != null)
                    {
                        for (int i = 0; i < matchCount; i++)
                        {
                            FullNewEL FulLive = new FullNewEL();
                            FulLive.matchId = CRLiveList.results[1].id;
                            FulLive.sport_id = CRLiveList.results[i].sport_id;
                            FulLive.time = CRLiveList.results[i].time;
                            FulLive.timeStatus = CRLiveList.results[i].time_status;
                            FulLive.leagueId = CRLiveList.results[i].league.id;
                            FulLive.leagueName = CRLiveList.results[i].league.name;
                            FulLive.homeTeamId = CRLiveList.results[i].home.id;
                            FulLive.homeTeamName = CRLiveList.results[i].home.name;
                            FulLive.awayTeamId = CRLiveList.results[i].away.id;
                            FulLive.awayTeamName = CRLiveList.results[i].away.name;
                            int LocalLeaguid = Convert.ToInt32(FulLive.leagueId);
                            crleagueid = Convert.ToInt32(FulLive.leagueId);
                            if (FulLive.leagueName.Contains("Test") && crleagueid == LocalLeaguid)
                            {
                                //test ts = new test();
                                //LeagueName = FulLive.leagueName;
                                //List<string> list = new List<string>();
                                //list.Add(LeagueName);
                                //LocalLeaguid = 0;
                            }
                            else
                            {
                                LocalLeaguid = 0;
                            }
                        }
                    }

                    LiveEventList FTLiveList = serializer.Deserialize<LiveEventList>(ftipjson);
                    int matchcountFt = FTLiveList.results.Count();
                    if (FTLiveList.success == "1" && FTLiveList.results != null)
                    {
                        for (int j = 0; j < matchcountFt; j++)
                        {
                            FullNewELFT FTEventList = new FullNewELFT();
                            FTEventList.sport_id = FTLiveList.results[j].sport_id;
                            FTEventList.matchId = FTLiveList.results[j].id;
                            FTEventList.time = FTLiveList.results[j].time;
                            FTEventList.timeStatus = FTLiveList.results[j].time_status;
                            FTEventList.leagueId = FTLiveList.results[j].league.id;
                            FTEventList.leagueName = FTLiveList.results[j].league.name;
                            FTEventList.homeTeamId = FTLiveList.results[j].home.id;
                            FTEventList.homeTeamName = FTLiveList.results[j].home.name;
                            FTEventList.awayTeamId = FTLiveList.results[j].away.id;
                            FTEventList.awayTeamName = FTLiveList.results[j].away.name;
                            int LocalLeaguid = Convert.ToInt32(FTEventList.leagueId);
                            ftleagueid = Convert.ToInt32(FTEventList.leagueId);

                            if (FTEventList.leagueName.Contains("Test") && ftleagueid == LocalLeaguid)
                            {
                                LocalLeaguid = 0;
                            }

                        }
                    }
                }
                catch (Exception et)
            {
                throw et;
            }
            Thread.Sleep(1000);
            }
            

            return View();
        }

        // Methods for PreMatch Cricket and Football
        public ActionResult PreMatch()
        {
            bool DoProcessing = true;
            while(DoProcessing)
            {
                GBLClass.GetPMEventList();
                string crpmjson = GBLClass.CricketPMGetJson.ToString();
                string ftpmjson = GBLClass.FotballPMGetJson.ToString();

                Thread.Sleep(1000);
            }
            return View();
        }

    }
}