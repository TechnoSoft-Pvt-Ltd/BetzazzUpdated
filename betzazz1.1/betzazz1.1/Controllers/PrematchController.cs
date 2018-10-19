using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using betzazz1._1.Models;

namespace betzazz1._1.Controllers
{
    public class PrematchController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        NewLive Obj_PreMatch = new NewLive();
        public string Eventid { get; set; }
        public string json { get; set; }
        // GET: Prematch
        public ActionResult Cricket()
        {
            GetJson();
            Market_display();

            return View();
        }
        public ActionResult Football()
        {
            return View();
           
        }
        public ActionResult Sport()
        {
            return View();
        }
        public void btn_Onclick()
        {
            //string gg = Data;
            Eventid = Request.QueryString["Eventid"];          
            Response.Redirect("Cricket?eventid=" + Eventid);
            //return View("InPlay");
        }
        public void GetJson()
        {
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Select jsonPMCROdds from CrPreMatchOdds  where  EventId=" + Request.QueryString["Eventid"] + " ", con);
                cmd.Connection = con;
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    json = reader.GetString(0);
                }
                con.Close();

            }
            catch (Exception er)
            {

            }
        }

        // Main funtion
        MatchOdds md = new MatchOdds();
        public ActionResult Market_display()
        {
            try
            {
                if (json != null)
                {

                    var serlizer = new JavaScriptSerializer();
                    Obj_PreMatch = serlizer.Deserialize<NewLive>(json);
                    string EventName = Obj_PreMatch.results[0].Event.name;
                    ViewBag.EventName = EventName;
                    MatchOdds.EventNAme = EventName;
                    TowWintheToss.EventNAme = EventName;
                    DrawNoBet.EventNAme = EventName;
                    DBLChance.EventNAme = EventName;
                    //string MarketTime = Obj_PreMatch.results[0].market.marketTime;

                    foreach (var Obj in Obj_PreMatch.results[0].markets)
                    {
                        string Innings = "";
                           if (Obj_PreMatch.results[0].timeline != null)
                           {
                              if (Obj_PreMatch.results[0].timeline.score.home.inning1 != null || Obj_PreMatch.results[0].timeline.score.away.inning1 != null)
                              {
                                  Innings = "First Innings";
                              }
                              else if (Obj_PreMatch.results[0].timeline.score.home.inning2 != null || Obj_PreMatch.results[0].timeline.score.away.inning2 != null)
                              {
                                 Innings = "Second Innings";
                              }
                           }

                        if (Obj.market.marketName.Equals("Match Odds"))//HAMW Match Odds Being End Here   
                        {
                                //MatchOdds.Visible = true;
                                //PeriodRow.Visible = false;
                                string MrktidMatchOdds = Obj.market.marketId;
                                string MrktNameMatchOdds = Obj.market.marketName;
                                ViewBag.MrktNameMatchOdds = MrktNameMatchOdds;
                                MatchOdds.MarketName= MrktNameMatchOdds;
                                ViewData["md"] = md;
                                string eventid = Obj.market.eventId;
                                string MarketSatus = Obj.marketStatus;
                                string MarketTime = Obj.market.marketTime;
                                ViewBag.MarketTime = MarketTime;
                                MatchOdds.MarketTime = MarketTime;
                                string team1Name = Obj.market.runners[0].runnerName;
                                string team2Name = Obj.market.runners[1].runnerName;
                                string epp = Obj_PreMatch.results[0].markets_updated_at;
                                long epoch = Convert.ToInt64(epp);
                                long baseTicks = 621355968000000000;
                                long tickResolution = 10000000;
                                long epochTicks = (epoch * tickResolution) + baseTicks;
                                var dtr = new DateTime(epochTicks, DateTimeKind.Utc).AddMinutes(1);
                                DateTime currentDateime = DateTime.UtcNow;
                                int x;
                                x = Obj.runnerDetails.Count;
                                if (Obj.marketStatus.Equals("SUSPENDED") )
                                {
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                MatchOdds.Team1 = "SUSPENDED";
                                MatchOdds.Team2 = "SUSPENDED";
                                    if (x == 3)
                                    {
                                        //lblMOT3.Text = "SUSPENDED";
                                        //btnMatchOdds3.Visible = false;
                                        //PeriodRow.Visible = true;
                                    }
                                    //btnMatchOdds1.Visible = false;
                                    //btnMatchOdds2.Visible = false;
                                }
                                else
                                {
                                    //MatchOdds.Visible = true;
                                    //lblMatchOdds.Text = MrktNameMatchOdds;
                                    foreach (var deciOdds in Obj.runnerDetails[0].runnerOdds.Values)//for index 0 Runner Odds
                                    {
                                        foreach (var GetOdd in deciOdds)
                                        {
                                            if (GetOdd.Key.Equals("decimalOdds"))
                                            {
                                                //if (btnMatchOdds1.Text == "0")
                                                //{
                                                    string newodds = Convert.ToString(GetOdd.Value);
                                                    if (newodds.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                                    { }
                                                    else
                                                    {
                                                        string T1Data = null;
                                                        double Odds0 = Convert.ToDouble(newodds);
                                                        if (Odds0 <= 10)
                                                        {
                                                            if (Odds0 <= 1.06)
                                                            {
                                                                  ViewBag.T1Name = "SUSPENDED";
                                                                  ViewBag.T2Name = "SUSPENDED";
                                                                  ViewBag.T3Name = "SUSPENDED";
                                                                 //btnMatchOdds1.Visible = false;
                                                                 //btnMatchOdds2.Visible = false;
                                                                 //btnMatchOdds3.Visible = false;
                                                            }
                                                            else
                                                            {
                                                               T1Data = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                               string T1Name = team1Name;
                                                               MatchOdds.Team1 = T1Name;
                                                               ViewBag.T1Name = T1Name;
                                                               string Odds1 = Convert.ToString(Odds0);
                                                               MatchOdds.Odds1 = Odds1;
                                                                ViewBag.Odds1 = Odds1;
                                                        }
                                                            }
                                                        else
                                                        {
                                                    
                                                              string Odds00 = "10.0";
                                                              T1Data = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings;
                                                              string T1Name = team1Name;
                                                              MatchOdds.Team1 = T1Name;
                                                              ViewBag.T1Name = T1Name;
                                                               string Odds1 = Convert.ToString(Odds00);
                                                              ViewBag.Odds1 = Odds1;
                                                              MatchOdds.Odds1 = Odds1;

                                                        }
                                                    }
                                                //}
                                            }
                                        }
                                    }
                                    //for index 1 Runner o
                                    foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                    {
                                        string T2Data = null;
                                        foreach (var GetOdd0 in deciOdds0)
                                        {
                                            if (GetOdd0.Key.Equals("decimalOdds"))
                                            {
                                                string newodds0 = Convert.ToString(GetOdd0.Value);
                                                if (newodds0.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                                { }
                                                else
                                                {
                                                    double OddsT21 = Convert.ToDouble(newodds0);
                                                    if (OddsT21 <= 10)
                                                    {
                                                        if (OddsT21 <= 1.06)
                                                        {
                                                             ViewBag.T1Name = "SUSPENDED";
                                                             ViewBag.T2Name = "SUSPENDED";
                                                             ViewBag.T3Name = "SUSPENDED";
                                                            //btnMatchOdds1.Visible = false;
                                                            //btnMatchOdds2.Visible = false;
                                                            //btnMatchOdds3.Visible = false;
                                                        }
                                                        else
                                                        {
                                                        T2Data = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + OddsT21 + "@" + team2Name + "@" + Innings;
                                                        string T2Name = team2Name;
                                                        MatchOdds.Team2 = T2Name;
                                                        ViewBag.T2Name = T2Name;
                                                        OddsT21 = Convert.ToInt64(OddsT21);
                                                        MatchOdds.Odds2 =Convert.ToString( OddsT21);
                                                        ViewBag.OddsT21 = Convert.ToString(OddsT21);
                                                        }
                                                    }
                                                    else
                                                    {
                                                    //if (lblMOT2.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds10 = "10.0";
                                                    //    //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings;
                                                    //    //lblMOT2.Text = team2Name;
                                                    //    //btnMatchOdds2.Text = Odds10;
                                                    //}
                                                    string T2Odds10 = "10.0";
                                                    T2Data = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + T2Odds10 + "@" + team2Name + "@" + Innings;
                                                    string T2Name = team2Name;
                                                    MatchOdds.Team2 = T2Name;
                                                    ViewBag.T2Name = T2Name;
                                                    OddsT21 = Convert.ToInt64(T2Odds10);
                                                    MatchOdds.Odds2 =Convert.ToString( OddsT21);
                                                    ViewBag.OddsT21 = Convert.ToString(OddsT21);
                                                }
                                                }
                                            }
                                        }
                                    }

                                    if (x == 3)
                                    {
                                        string team3Name = Obj.market.runners[2].runnerName;
                                    //btnMatchOdds3.Visible = true;
                                    //lblMOT3.Visible = true;
                                    //PeriodRow.Visible = true;
                                    //for index 2 Runner Odds
                                    string T3Data = null;
                                    foreach (var deciOdds0 in Obj.runnerDetails[2].runnerOdds.Values)
                                        {
                                            foreach (var GetOdd0 in deciOdds0)
                                            {
                                                if (GetOdd0.Key.Equals("decimalOdds"))
                                                {
                                                    string newodds0 = Convert.ToString(GetOdd0.Value);
                                                    if (newodds0.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                                    { }
                                                    else
                                                    {
                                                        double Odds2 = Convert.ToDouble(newodds0);
                                                        if (Odds2 <= 10)
                                                        {
                                                            if (Odds2 <= 1.06)
                                                            {

                                                            ViewBag.T1Name = "SUSPENDED";
                                                            ViewBag.T2Name = "SUSPENDED";
                                                            ViewBag.T3Name = "SUSPENDED";
                                                            //btnMatchOdds1.Visible = false;
                                                            //btnMatchOdds2.Visible = false;
                                                            //btnMatchOdds3.Visible = false;
                                                        }
                                                            else
                                                            {

                                                            //hfMatchOdds3.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds2 + "@" + team3Name + "@" + Innings;
                                                            //lblMOT3.Text = team3Name;
                                                            //btnMatchOdds3.Text = Convert.ToString(Odds2);
                                                            T3Data = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds2 + "@" + team3Name + "@" + Innings;
                                                            string T3Name = team3Name;
                                                            ViewBag.T3Name = T3Name;
                                                            Odds2 = Convert.ToInt64(Odds2);
                                                            ViewBag.OddsT31 = Odds2;
                                                        }
                                                        }
                                                        else
                                                        {
                                                        //if (lblMOT3.Text != "SUSPENDED")
                                                        //{
                                                        //    string Odds20 = "10.0";
                                                        //    hfMatchOdds3.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds20 + "@" + team3Name + "@" + Innings;
                                                        //    lblMOT3.Text = team3Name;
                                                        //    btnMatchOdds3.Text = Odds20;
                                                        //}
                                                        string Odds20 = "10.0";
                                                        T3Data = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds20 + "@" + team3Name + "@" + Innings;
                                                        string T3Name = team3Name;
                                                        ViewBag.T3Name = T3Name;
                                                        ViewBag.OddsT31 = Odds20;

                                                    }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }//HAMW Market Being End Here    
                        if (Obj.market.marketName.Equals("To Win the Toss"))//HAMW Match Odds Being End Here   
                        {
                            //MatchOdds.Visible = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string TowinMarketName = Obj.market.marketName;
                            ViewBag.TowinMarketName = TowinMarketName;
                            TowWintheToss.MarketName = TowinMarketName;
                            string eventid = Obj.market.eventId;
                            string MarketSatus = Obj.marketStatus;
                            string MarketTime = Obj.market.marketTime;
                            ViewBag.MarketTime = MarketTime;
                            string team1Name = Obj.market.runners[0].runnerName;
                            string team2Name = Obj.market.runners[1].runnerName;
                            string epp = Obj_PreMatch.results[0].markets_updated_at;
                            long epoch = Convert.ToInt64(epp);
                            long baseTicks = 621355968000000000;
                            long tickResolution = 10000000;
                            long epochTicks = (epoch * tickResolution) + baseTicks;
                            var dtr = new DateTime(epochTicks, DateTimeKind.Utc).AddMinutes(1);
                            DateTime currentDateime = DateTime.UtcNow;
                            int x;
                            x = Obj.runnerDetails.Count;
                            if (Obj.marketStatus.Equals("SUSPENDED"))
                            {
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                //lblMOT1.Text = "SUSPENDED";
                                //lblMOT2.Text = "SUSPENDED";
                                if (x == 3)
                                {
                                    //lblMOT3.Text = "SUSPENDED";
                                    //btnMatchOdds3.Visible = false;
                                    //PeriodRow.Visible = true;
                                }
                                //btnMatchOdds1.Visible = false;
                                //btnMatchOdds2.Visible = false;
                            }
                            else
                            {
                                //MatchOdds.Visible = true;
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                foreach (var deciOdds in Obj.runnerDetails[0].runnerOdds.Values)//for index 0 Runner Odds
                                {
                                    foreach (var GetOdd in deciOdds)
                                    {
                                        if (GetOdd.Key.Equals("decimalOdds"))
                                        {
                                            string TowT1Data = null;
                                            //if (btnMatchOdds1.Text == "0")
                                            //{
                                            string newodds = Convert.ToString(GetOdd.Value);
                                            if (newodds.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds0 = Convert.ToDouble(newodds);
                                                if (Odds0 <= 10)
                                                {
                                                    if (Odds0 <= 1.06)
                                                    {
                                                        TowWintheToss.Team1 = "SUSPENDED";
                                                        TowWintheToss.Team2 = "SUSPENDED";
                                                        //lblMOT3.Text = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds1.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                        //lblMOT1.Text = team1Name;
                                                        //btnMatchOdds1.Text = Convert.ToString(Odds0);
                                                        TowT1Data = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                        string M1T1Name = team1Name;
                                                        ViewBag.M1T1Name = M1T1Name;
                                                        TowWintheToss.Team1 = M1T1Name;
                                                        ViewBag.OddsTowT1 = Odds0;
                                                        TowWintheToss.Odds1 =Convert.ToString( Odds0);


                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT1.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds00 = "10.0";
                                                    //    hfMatchOdds1.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings;
                                                    //    lblMOT1.Text = team1Name;
                                                    //    btnMatchOdds1.Text = Odds00;
                                                    //}
                                                    string Odds00 = "10.0";
                                                    TowT1Data = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings;
                                                    string M1T1Name = team1Name;
                                                    ViewBag.M1T1Name = M1T1Name;
                                                    TowWintheToss.Team1 = M1T1Name;
                                                    // string Odds0 = Convert.ToString(Odds0);
                                                    ViewBag.OddsTowT1 = Odds00;
                                                    TowWintheToss.Odds1 = Convert.ToString(Odds00);
                                                }
                                            }
                                            //}
                                        }
                                    }
                                }
                                //for index 1 Runner o
                                foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                {
                                    string TowT2Data = null;
                                    foreach (var GetOdd0 in deciOdds0)
                                    {
                                        if (GetOdd0.Key.Equals("decimalOdds"))
                                        {
                                            string newodds0 = Convert.ToString(GetOdd0.Value);
                                            if (newodds0.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds1 = Convert.ToDouble(newodds0);
                                                if (Odds1 <= 10)
                                                {
                                                    if (Odds1 <= 1.06)
                                                    {
                                                        TowWintheToss.Team1 = "SUSPENDED";
                                                        TowWintheToss.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        TowT2Data = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        string M1T2Name = team2Name;
                                                        ViewBag.M1T2Name = M1T2Name;
                                                        TowWintheToss.Team2 = M1T2Name;
                                                        ViewBag.OddsTowT2 = Odds1;
                                                        TowWintheToss.Odds2 =Convert.ToString( Odds1);
                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT2.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds10 = "10.0";
                                                    //    //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings;
                                                    //    //lblMOT2.Text = team2Name;
                                                    //    //btnMatchOdds2.Text = Odds10;
                                                    //}
                                                    string Odds10 = "10.0";
                                                    TowT2Data = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                    string M1T2Name = team2Name;
                                                    ViewBag.M1T2Name = M1T2Name;
                                                    TowWintheToss.Team2 = M1T2Name;
                                                    ViewBag.OddsTowT2 = Odds10;
                                                    TowWintheToss.Odds2 = Convert.ToString(Odds10);

                                                }
                                            }
                                        }
                                    }
                                }

                              

                            }
                        }//To Win The Toss Being End Here
                        if (Obj.market.marketName.Equals("Draw No Bet"))//HAMW Match Odds Being End Here   
                        {
                            //MatchOdds.Visible = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string DraNBetMarketName = Obj.market.marketName;
                            ViewBag.drawNBetMarketName = DraNBetMarketName;
                            DrawNoBet.MarketName = DraNBetMarketName;
                            string eventid = Obj.market.eventId;
                            string MarketSatus = Obj.marketStatus;
                            string MarketTime = Obj.market.marketTime;
                            ViewBag.MarketTime = MarketTime;
                            DrawNoBet.MarketTime = MarketTime;
                            string team1Name = Obj.market.runners[0].runnerName;
                            string team2Name = Obj.market.runners[1].runnerName;          
                            string epp = Obj_PreMatch.results[0].markets_updated_at;
                            long epoch = Convert.ToInt64(epp);
                            long baseTicks = 621355968000000000;
                            long tickResolution = 10000000;
                            long epochTicks = (epoch * tickResolution) + baseTicks;
                            var dtr = new DateTime(epochTicks, DateTimeKind.Utc).AddMinutes(1);
                            DateTime currentDateime = DateTime.UtcNow;
                            int x;
                            x = Obj.runnerDetails.Count;
                            if (Obj.marketStatus.Equals("SUSPENDED") )
                            {
                                DrawNoBet.Team1 = "SUSPENDED";
                                DrawNoBet.Team2 = "SUSPENDED";
                                if (x == 3)
                                {
                                    //lblMOT3.Text = "SUSPENDED";
                                    //btnMatchOdds3.Visible = false;
                                    //PeriodRow.Visible = true;
                                }
                                //btnMatchOdds1.Visible = false;
                                //btnMatchOdds2.Visible = false;
                            }
                            else
                            {
                                //MatchOdds.Visible = true;
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                foreach (var deciOdds in Obj.runnerDetails[0].runnerOdds.Values)//for index 0 Runner Odds
                                {
                                    string DRNT1Data = null;
                                    foreach (var GetOdd in deciOdds)
                                    {
                                        if (GetOdd.Key.Equals("decimalOdds"))
                                        {
                                            //if (btnMatchOdds1.Text == "0")
                                            //{
                                            string newodds = Convert.ToString(GetOdd.Value);
                                            if (newodds.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds0 = Convert.ToDouble(newodds);
                                                if (Odds0 <= 10)
                                                {
                                                    if (Odds0 <= 1.06)
                                                    {
                                                        DrawNoBet.Team1 = "SUSPENDED";
                                                        DrawNoBet.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds1.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                        //lblMOT1.Text = team1Name;
                                                        //btnMatchOdds1.Text = Convert.ToString(Odds0);
                                                        DRNT1Data = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                        string DRNT1Name = team1Name;
                                                        ViewBag.DRNT1Name = DRNT1Name;
                                                        DrawNoBet.Team1 = DRNT1Name;
                                                        ViewBag.OddsDRNT1 = Odds0;
                                                        DrawNoBet.Odds1 =Convert.ToString(Odds0);
                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT1.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds00 = "10.0";
                                                    //    hfMatchOdds1.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings;
                                                    //    lblMOT1.Text = team1Name;
                                                    //    btnMatchOdds1.Text = Odds00;
                                                    //}
                                                    string Odds00 = "10.0";
                                                    DRNT1Data = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings;
                                                    string DRNT1Name = team1Name;
                                                    ViewBag.DRNT1Name = DRNT1Name;
                                                    DrawNoBet.Team1 = DRNT1Name;
                                                    ViewBag.OddsDRNT1 = Odds00;
                                                    DrawNoBet.Odds1 = Convert.ToString(Odds00);
                                                }
                                            }
                                            //}
                                        }
                                    }
                                }
                                //for index 2 Runner o
                                foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                {
                                    string DRNT2Data = null;
                                    foreach (var GetOdd0 in deciOdds0)
                                    {
                                        if (GetOdd0.Key.Equals("decimalOdds"))
                                        {
                                            string newodds0 = Convert.ToString(GetOdd0.Value);
                                            if (newodds0.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds1 = Convert.ToDouble(newodds0);
                                                if (Odds1 <= 10)
                                                {
                                                    if (Odds1 <= 1.06)
                                                    {
                                                        DrawNoBet.Team1 = "SUSPENDED";
                                                        DrawNoBet.Team2 = "SUSPENDED";
                                                        //lblMOT3.Text = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        DRNT2Data = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        string DRNT2Name = team2Name;
                                                        ViewBag.DRNT2Name = DRNT2Name;
                                                        DrawNoBet.Team2 = DRNT2Name;
                                                        ViewBag.OddsDRNT2 = Odds1;
                                                        DrawNoBet.Odds2 =Convert.ToString( Odds1);
                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT2.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds10 = "10.0";
                                                    //    //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings;
                                                    //    //lblMOT2.Text = team2Name;
                                                    //    //btnMatchOdds2.Text = Odds10;
                                                    //}
                                                    string Odds10 = "10.0";
                                                    DRNT2Data = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                    string DRNT2Name = team2Name;
                                                    ViewBag.DRNT2Name = DRNT2Name;
                                                    DrawNoBet.Team2 = DRNT2Name;
                                                    ViewBag.OddsDRNT2 = Odds10;
                                                    DrawNoBet.Odds2 = Convert.ToString(Odds10);
                                                }
                                            }
                                        }
                                    }
                                }
                                //for index 1 Runner o                               

                            }
                        }//Draw Not Bet Being End Here  
                        if (Obj.market.marketName.Equals("Double Chance"))//HAMW Match Odds Being End Here   
                        {
                            //MatchOdds.Visible = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string DBlChnceMarketName = Obj.market.marketName;
                            ViewBag.DBlChnceMarketName = DBlChnceMarketName;
                            DBLChance.MarketName = DBlChnceMarketName;
                            string eventid = Obj.market.eventId;
                            string MarketSatus = Obj.marketStatus;
                            string MarketTime = Obj.market.marketTime;
                            ViewBag.MarketTime = MarketTime;
                            DBLChance.MarketTime = MarketTime;
                            string team1Name = Obj.market.runners[0].runnerName;
                            string team2Name = Obj.market.runners[1].runnerName;
                            string team3Name = Obj.market.runners[2].runnerName;
                            string epp = Obj_PreMatch.results[0].markets_updated_at;
                            long epoch = Convert.ToInt64(epp);
                            long baseTicks = 621355968000000000;
                            long tickResolution = 10000000;
                            long epochTicks = (epoch * tickResolution) + baseTicks;
                            var dtr = new DateTime(epochTicks, DateTimeKind.Utc).AddMinutes(1);
                            DateTime currentDateime = DateTime.UtcNow;
                            int x;
                            x = Obj.runnerDetails.Count;
                            if (Obj.marketStatus.Equals("SUSPENDED"))
                            {
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                DBLChance.Team1 = "SUSPENDED";
                                DBLChance.Team2 = "SUSPENDED";
                               
                            }
                            else
                            {
                                //MatchOdds.Visible = true;
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                foreach (var deciOdds in Obj.runnerDetails[0].runnerOdds.Values)//for index 0 Runner Odds
                                {
                                    string DBLT1Data = null;
                                    foreach (var GetOdd in deciOdds)
                                    {
                                        if (GetOdd.Key.Equals("decimalOdds"))
                                        {
                                            //if (btnMatchOdds1.Text == "0")
                                            //{
                                            string newodds = Convert.ToString(GetOdd.Value);
                                            if (newodds.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds0 = Convert.ToDouble(newodds);
                                                if (Odds0 <= 10)
                                                {
                                                    if (Odds0 <= 1.06)
                                                    {
                                                        DBLChance.Team1 = "SUSPENDED";
                                                        DBLChance.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds1.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                        //lblMOT1.Text = team1Name;
                                                        //btnMatchOdds1.Text = Convert.ToString(Odds0);
                                                        DBLT1Data = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                        string DBlT1Name = team1Name;
                                                        ViewBag.DBlT1Name = DBlT1Name;
                                                        DBLChance.Team1 = DBlT1Name;
                                                        ViewBag.OddsDBLT1 = Odds0;
                                                        DBLChance.Odds1 = Convert.ToString(Odds0);
                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT1.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds00 = "10.0";
                                                    //    hfMatchOdds1.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings;
                                                    //    lblMOT1.Text = team1Name;
                                                    //    btnMatchOdds1.Text = Odds00;
                                                    //}
                                                    string Odds00 = "10.0";
                                                    DBLT1Data = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings;
                                                    string DBlT1Name = team1Name;
                                                    ViewBag.DBlT1Name = DBlT1Name;
                                                    DBLChance.Team1 = DBlT1Name;
                                                    ViewBag.OddsDBLT1 = Odds00;
                                                    DBLChance.Odds1 = Convert.ToString(Odds00);

                                                }
                                            }
                                            //}
                                        }
                                    }
                                }
                                //for index 1 Runner o
                                foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                {
                                    string DBLT2data = null;
                                    foreach (var GetOdd0 in deciOdds0)
                                    {
                                        if (GetOdd0.Key.Equals("decimalOdds"))
                                        {
                                            string newodds0 = Convert.ToString(GetOdd0.Value);
                                            if (newodds0.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds1 = Convert.ToDouble(newodds0);
                                                if (Odds1 <= 10)
                                                {
                                                    if (Odds1 <= 1.06)
                                                    {
                                                         DBLChance.Team1 = "SUSPENDED";
                                                         DBLChance.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        DBLT2data = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        string DBlT2Name = team2Name;
                                                        ViewBag.DBlT2Name = DBlT2Name;
                                                        DBLChance.Team2 = DBlT2Name; 
                                                        ViewBag.OddsDBLT2 = Odds1;
                                                        DBLChance.Odds2 = Convert.ToString(Odds1);
                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT2.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds10 = "10.0";
                                                    //    //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings;
                                                    //    //lblMOT2.Text = team2Name;
                                                    //    //btnMatchOdds2.Text = Odds10;
                                                    //}
                                                    string Odds10 = "10.0";
                                                    DBLT2data = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings;
                                                    string DBlT2Name = team2Name;
                                                    ViewBag.DBlT2Name = DBlT2Name;
                                                    DBLChance.Team2 = DBlT2Name;
                                                    ViewBag.OddsDBLT2 = Odds10;
                                                    DBLChance.Odds2 = Convert.ToString(Odds10);
                                                }
                                            }
                                        }
                                    }
                                }

                                //for index 1 Runner o
                                foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                {
                                    string DBLT3data = null;
                                    foreach (var GetOdd0 in deciOdds0)
                                    {
                                        if (GetOdd0.Key.Equals("decimalOdds"))
                                        {
                                            string newodds0 = Convert.ToString(GetOdd0.Value);
                                            if (newodds0.Equals("System.Collections.Generic.Dictionary`2[System.String,System.Object]"))
                                            { }
                                            else
                                            {
                                                double Odds1 = Convert.ToDouble(newodds0);
                                                if (Odds1 <= 10)
                                                {
                                                    if (Odds1 <= 1.06)
                                                    {
                                                        //lblMOT1.Text = "SUSPENDED";
                                                        //lblMOT2.Text = "SUSPENDED";
                                                        //lblMOT3.Text = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        DBLT3data = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team3Name + "@" + Innings;
                                                        string DBlT3Name = team3Name;
                                                        ViewBag.DBlT3Name = DBlT3Name;
                                                        ViewBag.OddsDBLT3 = Odds1;
                                                    }
                                                }
                                                else
                                                {
                                                    //if (lblMOT2.Text != "SUSPENDED")
                                                    //{
                                                    //    string Odds10 = "10.0";
                                                    //    //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings;
                                                    //    //lblMOT2.Text = team2Name;
                                                    //    //btnMatchOdds2.Text = Odds10;
                                                    //}
                                                    string Odds10 = "10.0";
                                                    DBLT3data = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team3Name + "@" + Innings;
                                                    string DBlT3Name = team2Name;
                                                    ViewBag.DBlT3Name = DBlT3Name;
                                                    ViewBag.OddsDBLT3 = Odds10;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }//Draw Not Bet Being End Here  


                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Cricket");
        }

        Betslip bt = new Betslip();
        [HttpPost]
        public ActionResult btn1MatchOdds_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

             Betslip.EventName = MatchOdds.EventNAme;
             Betslip.MarketNnme = MatchOdds.MarketName;
             Betslip.Selection = MatchOdds.Team1;
             Betslip.Rate = MatchOdds.Odds1;
             return RedirectToAction("Cricket");


        }
        public ActionResult btn2MatchOdds_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = MatchOdds.EventNAme;
            Betslip.MarketNnme = MatchOdds.MarketName;
            Betslip.Selection = MatchOdds.Team2;
            Betslip.Rate = MatchOdds.Odds2;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn1ToWnToss_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = TowWintheToss.EventNAme;
            Betslip.MarketNnme = TowWintheToss.MarketName;
            Betslip.Selection = TowWintheToss.Team1;
            Betslip.Rate = TowWintheToss.Odds1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2ToWnToss_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = TowWintheToss.EventNAme;
            Betslip.MarketNnme = TowWintheToss.MarketName;
            Betslip.Selection = TowWintheToss.Team2;
            Betslip.Rate = TowWintheToss.Odds2;
            return RedirectToAction("Cricket");


        }

        // Draw No Bet

        public ActionResult btn1DRNT_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = DrawNoBet.EventNAme;
            Betslip.MarketNnme = DrawNoBet.MarketName;
            Betslip.Selection = DrawNoBet.Team1;
            Betslip.Rate = DrawNoBet.Odds1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2DRNT_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = DrawNoBet.EventNAme;
            Betslip.MarketNnme = DrawNoBet.MarketName;
            Betslip.Selection = DrawNoBet.Team2;
            Betslip.Rate = DrawNoBet.Odds2;
            return RedirectToAction("Cricket");


        }
        // Draw No Bet

        public ActionResult btn1DBL_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = DBLChance.EventNAme;
            Betslip.MarketNnme = DBLChance.MarketName;
            Betslip.Selection = DBLChance.Team1;
            Betslip.Rate = DBLChance.Odds1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2DBL_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = DBLChance.EventNAme;
            Betslip.MarketNnme = DBLChance.MarketName;
            Betslip.Selection = DBLChance.Team2;
            Betslip.Rate = DBLChance.Odds2;
            return RedirectToAction("Cricket");


        }
    }
}