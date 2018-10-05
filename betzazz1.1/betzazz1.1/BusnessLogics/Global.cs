using betzazz1._1.Models;
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
        public static string connectionString=ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<string> FailedProductIdsList { get; set; }

        public string CricketIPGetJson { get; set; }

        public string FotballIPGetJson { get; set; }

        public string CricketPMGetJson { get; set; }

        public string FotballPMGetJson { get; set; }


        public static int crleagueid { get; set; }
        public  int ftleagueid { get; set; }

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
                using (SqlConnection concr=new SqlConnection(connectionString))
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

        // Funtion for Get Pre Match Event JSON String from Data Base.
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
                               if (TestData == null && EventId==null)
                               {
                                TestLeagueName = FulLive.leagueName;
                                EventId = EventId + CRLiveList.results[i].id;
                                TestData = TestData + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                               }
                               else
                               {
                                EventId = EventId +"@"+ CRLiveList.results[i].id;
                                TestData = TestData + "@" + FulLive.homeTeamName + " VS " + FulLive.awayTeamName;
                               }                              
                                LocalLeaguid = 0;
                            }
                            else if((FulLive.leagueName.Contains("T20") && crleagueid == LocalLeaguid))
                            {
                              if (T20Data == null && EventId1 == null)
                              {
                                T20LeagueName= FulLive.leagueName;
                                EventId1= EventId1 + CRLiveList.results[i].id;
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
                                ODILeagueName= FulLive.leagueName;
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

                    //LiveEventList FTLiveList = serializer.Deserialize<LiveEventList>(ftipjson);
                    //int matchcountFt = FTLiveList.results.Count();
                    //if (FTLiveList.success == "1" && FTLiveList.results != null)
                    //{
                    //    for (int j = 0; j < matchcountFt; j++)
                    //    {
                    //        FullNewELFT FTEventList = new FullNewELFT();
                    //        FTEventList.sport_id = FTLiveList.results[j].sport_id;
                    //        FTEventList.matchId = FTLiveList.results[j].id;
                    //        FTEventList.time = FTLiveList.results[j].time;
                    //        FTEventList.timeStatus = FTLiveList.results[j].time_status;
                    //        FTEventList.leagueId = FTLiveList.results[j].league.id;
                    //        FTEventList.leagueName = FTLiveList.results[j].league.name;
                    //        FTEventList.homeTeamId = FTLiveList.results[j].home.id;
                    //        FTEventList.homeTeamName = FTLiveList.results[j].home.name;
                    //        FTEventList.awayTeamId = FTLiveList.results[j].away.id;
                    //        FTEventList.awayTeamName = FTLiveList.results[j].away.name;
                    //        int LocalLeaguid = Convert.ToInt32(FTEventList.leagueId);
                    //        ftleagueid = Convert.ToInt32(FTEventList.leagueId);

                    //        if (FTEventList.leagueName.Contains("Test") && ftleagueid == LocalLeaguid)
                    //        {
                    //            LocalLeaguid = 0;
                    //        }

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

    }
}