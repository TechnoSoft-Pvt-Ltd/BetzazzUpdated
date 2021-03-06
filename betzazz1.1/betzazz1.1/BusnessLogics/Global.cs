﻿using betzazz1._1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace betzazz1._1.BusnessLogics
{
    public class Global
    {
        LiveEventList Live = new LiveEventList();
        // Configuration of connection string.
        public static string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<string> FailedProductIdsList { get; set; }

        public string CricketIPGetJson { get; set; }

        public string FotballIPGetJson { get; set; }

        public string CricketPMGetJson { get; set; }

        public string FotballPMGetJson { get; set; }


        public static int crleagueid { get; set; }
        public int ftleagueid { get; set; }

        public string TestLeagueName { get; set; }

        public string TestData { get; set; }
        public string EventId { get; set; }
        public string EventId1 { get; set; }
        public string EventId2 { get; set; }
        public string EventI3 { get; set; }

        public string T20Data { get; set; }
        public string T20LeagueName { get; set; }


        public string ODIData { get; set; }
        public string ODILeagueName { get; set; }


        public static string FootballLeageName { get; set; }
        public string FTData { get; set; }
        public string FTEventId { get; set; }
        //public string  LeagueName { get; set; }
        //public string EventId { get; set; }
        //public string EventName { get; set; }


        // Funtion for Get Inplay Event JSON String from Data Base.
        public void GetIPEventList()
        {
            try
            {
                // string GetJson = null;
                // Cricket Inplay Event List JSON String from Data Base
                using (SqlConnection concr = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdcr = new SqlCommand("SP_cricketInplayEvntList", concr))
                    {
                        concr.Open();
                        cmdcr.Connection = concr;
                        cmdcr.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdcr.ExecuteReader())
                        {
                            reader.Read();
                            CricketIPGetJson = reader.GetString(0).ToString();
                        }
                        concr.Close();
                    }
                }

                //  Football Inplay Eevnt List JSON String
                using (SqlConnection conft = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdft = new SqlCommand("SP_FootballInplayEvntList", conft))
                    {
                        conft.Open();
                        cmdft.Connection = conft;
                        cmdft.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdft.ExecuteReader())
                        {
                            reader.Read();
                            FotballIPGetJson = reader.GetString(0).ToString();
                        }
                        conft.Close();
                    }
                }
            }
            catch (Exception ew)
            {
                throw ew;
            }

        }

        // Funtion for Get Inplay Event JSON String from Data Base.
      
        public void InplayCR()
        {
            //bool Doprocessing = true;
            //while (Doprocessing)
            //{
            try
            {
                GetIPEventList();
                string cripjson = CricketIPGetJson.ToString();
                string ftipjson = FotballIPGetJson.ToString();
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
                            if (TestData == null && EventId == null)
                            {
                                TestLeagueName = FulLive.leagueName;
                                EventId = EventId + CRLiveList.results[i].id;
                                TestData = TestData + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                            }
                            else
                            {
                                EventId = EventId + "@" + CRLiveList.results[i].id;
                                TestData = TestData + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                            }
                            LocalLeaguid = 0;
                        }
                        else if ((FulLive.leagueName.Contains("T20") && crleagueid == LocalLeaguid))
                        {
                            if (T20Data == null && EventId1 == null)
                            {
                                T20LeagueName = FulLive.leagueName;
                                EventId1 = EventId1 + CRLiveList.results[i].id;
                                T20Data = T20Data + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                            }
                            else
                            {
                                EventId1 = EventId1 + "@" + CRLiveList.results[i].id;
                                T20Data = T20Data + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                            }
                            LocalLeaguid = 0;
                        }
                        else if ((FulLive.leagueName.Contains("ODI") && crleagueid == LocalLeaguid))
                        {
                            if (ODIData == null && EventId2 == null)
                            {
                                ODILeagueName = FulLive.leagueName;
                                EventId2 = EventId2 + CRLiveList.results[i].id;
                                ODIData = ODIData + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                            }
                            else
                            {
                                EventId2 = EventId2 + "@" + CRLiveList.results[i].id;
                                ODIData = ODIData + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                            }
                            LocalLeaguid = 0;
                        }
                    }
                }
                // For football Inplay event Lis
                // LiveEventList FTLiveList = serializer.Deserialize<LiveEventList>(ftipjson);
                // int matchcountFt = FTLiveList.results.Count();
                // if (FTLiveList.success == "1" && FTLiveList.results != null)
                // for (int j = 0; j < matchcountFt; j++)
                //{
                //    FullNewELFT FTEventList = new FullNewELFT();
                //    FTEventList.sport_id = FTLiveList.results[j].sport_id;
                //    FTEventList.matchId = FTLiveList.results[j].id;
                //    FTEventList.time = FTLiveList.results[j].time;
                //    FTEventList.timeStatus = FTLiveList.results[j].time_status;
                //    FTEventList.leagueId = FTLiveList.results[j].league.id;
                //    FTEventList.leagueName = FTLiveList.results[j].league.name;
                //    FTEventList.homeTeamId = FTLiveList.results[j].home.id;
                //    FTEventList.homeTeamName = FTLiveList.results[j].home.name;
                //    FTEventList.awayTeamId = FTLiveList.results[j].away.id;
                //    FTEventList.awayTeamName = FTLiveList.results[j].away.name;
                //    int LocalLeaguid = Convert.ToInt32(FTEventList.leagueId);
                //    ftleagueid = Convert.ToInt32(FTEventList.leagueId);
                //    string Lname = FTEventList.leagueName;
                //    if (FTEventList.leagueName.Equals(Lname) && ftleagueid == LocalLeaguid && FTEventList.leagueName!= FootballLeageName)
                //    {
                //        if (FTData == null && FTEventId == null)
                //        {
                //            FootballLeageName = FTEventList.leagueName;
                //            FTEventId = EventId + CRLiveList.results[j].id;
                //            FTData = TestData + FTEventList.homeTeamName + " VS " + FTEventList.awayTeamName;
                //        }
                //        else
                //        {
                //            EventId = EventId + "@" + CRLiveList.results[j].id;
                //            TestData = TestData + "@" + FTEventList.homeTeamName + " VS " + FTEventList.awayTeamName;
                //        }
                //        LocalLeaguid = 0;
                //        Lname = "";
                //    }
                //    else if ((FTEventList.leagueName.Equals(FTEventList.leagueName) && ftleagueid == LocalLeaguid))
                //    {
                //        if (T20Data == null && EventId1 == null)
                //        {
                //            T20LeagueName = FTEventList.leagueName;
                //            EventId1 = EventId1 + CRLiveList.results[j].id;
                //            T20Data = T20Data + FTEventList.homeTeamName + " VS " + FTEventList.awayTeamName;
                //        }
                //        else
                //        {
                //            EventId1 = EventId1 + "@" + CRLiveList.results[j].id;
                //            T20Data = T20Data + "@" + FTEventList.homeTeamName + " VS " + FTEventList.awayTeamName;
                //        }
                //        LocalLeaguid = 0;
                //    }
                //    else if ((FTEventList.leagueName.Equals(FTEventList.leagueName) && ftleagueid == LocalLeaguid))
                //    {
                //        if (ODIData == null && EventId2 == null)
                //        {
                //            ODILeagueName = FTEventList.leagueName;
                //            EventId2 = EventId2 + CRLiveList.results[j].id;
                //            ODIData = ODIData + FTEventList.homeTeamName + " VS " + FTEventList.awayTeamName;
                //        }
                //        else
                //        {
                //            EventId2 = EventId2 + "@" + CRLiveList.results[j].id;
                //            ODIData = ODIData + "@" + FTEventList.homeTeamName + " VS " + FTEventList.awayTeamName;
                //        }
                //        LocalLeaguid = 0;
                //    }

                //}

            }
            catch (Exception et)
            {
                throw et;
            }
            // Thread.Sleep(1000);
            // }


        }


        //  Funtion for Get Pre Match Event JSON String from Data Base.

        public void GetPMEventList()
        {
            try
            {
                // string GetJson = null;
                // Cricket Inplay Event List JSON String from Data Base
                using (SqlConnection concr = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdcr = new SqlCommand("SP_CricketPMEventList", concr))
                    {
                        concr.Open();
                        cmdcr.Connection = concr;
                        cmdcr.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdcr.ExecuteReader())
                        {
                            reader.Read();
                            CricketPMGetJson = reader.GetString(0).ToString();
                        }
                        concr.Close();
                    }
                }

                //  Football Inplay Eevnt List JSON String
                using (SqlConnection conft = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdft = new SqlCommand("SP_FootballPMEventList", conft))
                    {
                        conft.Open();
                        cmdft.Connection = conft;
                        cmdft.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdft.ExecuteReader())
                        {
                            reader.Read();
                            FotballPMGetJson = reader.GetString(0).ToString();
                        }
                        conft.Close();
                    }
                }
            }
            catch (Exception ew)
            {
                throw ew;
            }

        }
        // For Test League
        public string PMTestData { get; set; }
        public string PMEventId { get; set; }
        public string PMTestLeagueName { get; set; }
        public string PMTestEventDate { get; set; }

        // For T20 League
        public string PMT20Data { get; set; }
        public string PMT20EventId { get; set; }
        public string PMT20LeagueName { get; set; }
        public string PMT20EventDate { get; set; }

        // For ODi League
        public string PMODIData { get; set; }
        public string PMODIEventId { get; set; }
        public string PMODILeagueName { get; set; }
        public string PMODIEventDate { get; set; }

        // For Premier League
        public string PMPLData { get; set; }
        public string PMPLEventId { get; set; }
        public string PMPLLeagueName { get; set; }
        public string PMPLEventDate { get; set; }


        public void PreMatch()
        {
            try
            {
                GetPMEventList();
                string cripjson = CricketPMGetJson.ToString();
                string ftipjson = FotballPMGetJson.ToString();
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
                            if (PMTestData == null && PMEventId == null)
                            {
                                PMTestLeagueName = "Test Matches";
                                PMEventId = PMEventId + CRLiveList.results[i].id;
                                PMTestData = PMTestData + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMTestEventDate =Convert.ToString(dtr);
                            }
                            else
                            {
                                PMEventId = PMEventId + "@" + CRLiveList.results[i].id;
                                PMTestData = PMTestData + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMTestEventDate = PMTestEventDate+"@"+Convert.ToString(dtr);

                            }
                            LocalLeaguid = 0;
                        }
                        else if ((FulLive.leagueName.Contains("T20") && crleagueid == LocalLeaguid) || (FulLive.leagueName.Contains("Twenty20 ") && crleagueid == LocalLeaguid))
                        {
                            if (PMT20Data == null && PMT20EventId == null)
                            {
                                PMT20LeagueName = "Twenty20 ";
                                PMT20EventId = PMT20EventId + CRLiveList.results[i].id;
                                PMT20Data = PMT20Data + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMT20EventDate = Convert.ToString(dtr);
                            }
                            else
                            {
                                PMT20EventId = PMT20EventId + "@" + CRLiveList.results[i].id;
                                PMT20Data = PMT20Data + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMT20EventDate = PMT20EventDate+"@"+Convert.ToString(dtr);
                            }
                            LocalLeaguid = 0;
                        }
                        else 
                        if ((FulLive.leagueName.Contains("ODI") && crleagueid == LocalLeaguid) || (FulLive.leagueName.Contains("One-Day") && crleagueid == LocalLeaguid)
                            || (FulLive.leagueName.Contains("One Day Internationals") && crleagueid == LocalLeaguid) || (FulLive.leagueName.Contains("One Day") && crleagueid == LocalLeaguid))
                        {
                            if (PMODIData == null && PMODIEventId == null)
                            {
                                PMODILeagueName = "One Day Internationals";
                                PMODIEventId = PMODIEventId + CRLiveList.results[i].id;
                                PMODIData = PMODIData + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMODIEventDate = Convert.ToString(dtr);
                               
                            }
                            else
                            {
                                PMODIEventId = PMODIEventId + "@" + CRLiveList.results[i].id;
                                PMODIData = PMODIData + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMODIEventDate = PMODIEventDate+"@"+Convert.ToString(dtr);
                            }
                            LocalLeaguid = 0;
                        }
                        else 
                        if ((FulLive.leagueName.Contains("Premier League") && crleagueid == LocalLeaguid))
                        {
                            if (PMPLData == null && PMODIEventId == null)
                            {
                                PMPLLeagueName = FulLive.leagueName;
                                PMPLEventId = PMPLEventId + CRLiveList.results[i].id;
                                PMPLData = PMPLData + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMPLEventDate = Convert.ToString(dtr);
                               
                            }
                            else
                            {
                                PMPLEventId = PMPLEventId + "@" + CRLiveList.results[i].id;
                                PMPLData = PMPLData + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                                string epp = CRLiveList.results[i].time;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc);
                                PMPLEventDate = PMPLEventDate+"@"+Convert.ToString(dtr);
                            }
                            LocalLeaguid = 0;
                        }
                    }
                }
            }
            catch (Exception rt)
            {
                throw rt;
            }


        }
    }
}