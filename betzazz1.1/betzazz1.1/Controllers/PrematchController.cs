using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using betzazz1._1.Models;
using betzazz1._1.ViewModels;

namespace betzazz1._1.Controllers
{
    public class PrematchController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        NewLive Obj_PreMatch = new NewLive();
        public static string Eventid { get; set; }
        public string json { get; set; }
        // GET: Prematch
        public ActionResult Cricket()
        {
            ViewBag.MatchOddsMarket = false;
            ViewBag.MatchOddsMarket3 = false;
            ViewBag.TWTossMarket = false;
            ViewBag.DNBetMarket = false;
            ViewBag.DBLChangeMarket = false;
            ViewBag.Runsin1stOver = false;
            ViewBag.HSIN15Overs = false;           
            ViewBag.HalfCenturyScIn1stInnings = false;
            ViewBag.CenturyScIn1stInnings = false;


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
            Eventid = Request.QueryString["Eventid"];          
            Response.Redirect("Cricket?eventid=" + Eventid);
            //return View("InPlay");
        }
        public void GetJson()
        {
            try
            {
             

                con.Open();
                SqlCommand cmd = new SqlCommand("Select jsonPMCROdds from CrPreMatchOdds  where  EventId=" + Eventid.ToString() + " ", con);
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
                    Session["json"] = json;
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
                        string Innings = "None";
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
                                ViewBag.MatchOddsMarket = true;
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
                                if (Obj.marketStatus.Equals("SUSPENDED") || dtr < currentDateime)
                                {
                                //lblMatchOdds.Text = MrktNameMatchOdds;
                                MatchOdds.Team1 = "SUSPENDED";
                                MatchOdds.Team2 = "SUSPENDED";
                                ViewBag.MatchOddsMarketbtn1 = false;
                                ViewBag.MatchOddsMarketbtn2 = false;
                               
                                    if (x == 3)
                                    {
                                    MatchOdds.Team3 = "SUSPENDED";
                                    ViewBag.MatchOddsMarketbtn3 = false;
                                    //btnMatchOdds3.Visible = false;
                                    ViewBag.MatchOddsMarket3 = true;
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
                                                               MatchOdds.MatchOddsDAta1  = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                              MatchOdds.MatchOddsDAta1 = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                        //string T2Data = null;
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
                                                        MatchOdds.MatchOddsDAta2 = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + OddsT21 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                                                    MatchOdds.MatchOddsDAta2 = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + T2Odds10 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                                    ViewBag.MatchOddsMarket3 = true;
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

                                                            MatchOdds.Team1 = "SUSPENDED";
                                                            MatchOdds.Team1 = "SUSPENDED";
                                                            MatchOdds.Team1 = "SUSPENDED";
                                                            //btnMatchOdds1.Visible = false;
                                                            //btnMatchOdds2.Visible = false;
                                                            //btnMatchOdds3.Visible = false;
                                                        }
                                                            else
                                                            {

                                                            //hfMatchOdds3.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds2 + "@" + team3Name + "@" + Innings;
                                                            //lblMOT3.Text = team3Name;
                                                            //btnMatchOdds3.Text = Convert.ToString(Odds2);
                                                            MatchOdds.MatchOddsDAta3 = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds2 + "@" + team3Name + "@" + Innings+"@"+ EventName;
                                                            string T3Name = team3Name;
                                                            ViewBag.T3Name = T3Name;
                                                            MatchOdds.Team3 = T3Name;
                                                            Odds2 = Convert.ToInt64(Odds2);
                                                            ViewBag.OddsT31 = Odds2;
                                                            MatchOdds.Odds3 = Convert.ToString(Odds2);
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
                                                        MatchOdds.MatchOddsDAta3 = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds20 + "@" + team3Name + "@" + Innings + "@" + EventName;
                                                        string T3Name = team3Name;
                                                        MatchOdds.Team3 = T3Name;
                                                        ViewBag.OddsT31 = Odds20;
                                                        MatchOdds.Odds3 = Convert.ToString(Odds20);
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
                            ViewBag.TWTossMarket = true;
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
                                                        TowWintheToss.TWTOddsDAta1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                    TowWintheToss.TWTOddsDAta1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                        TowWintheToss.TWTOddsDAta2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                                                    TowWintheToss.TWTOddsDAta2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                        }//To Win The Toss Being start Here
                        if (Obj.market.marketName.Equals("First Over Runs"))//HAMW Match Odds Being End Here   
                        {
                            ViewBag.Runsin1stOver = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string TowinMarketName = Obj.market.marketName;
                            ViewBag.TowinMarketName = TowinMarketName;
                            Runsin1stOver.MarketName = TowinMarketName;
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
                                                        Runsin1stOver.Team1 = "SUSPENDED";
                                                        Runsin1stOver.Team2 = "SUSPENDED";
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
                                                        Runsin1stOver.Runsin1stOverData1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                        string M1T1Name = team1Name;
                                                        ViewBag.M1T1Name = M1T1Name;
                                                        Runsin1stOver.Team1 = M1T1Name;
                                                        ViewBag.OddsTowT1 = Odds0;
                                                        Runsin1stOver.Odds1 = Convert.ToString(Odds0);


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
                                                    Runsin1stOver.Runsin1stOverData1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                    string M1T1Name = team1Name;
                                                    ViewBag.M1T1Name = M1T1Name;
                                                    Runsin1stOver.Team1 = M1T1Name;
                                                    // string Odds0 = Convert.ToString(Odds0);
                                                    ViewBag.OddsTowT1 = Odds00;
                                                    Runsin1stOver.Odds1 = Convert.ToString(Odds00);
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
                                                        Runsin1stOver.Team1 = "SUSPENDED";
                                                        Runsin1stOver.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        Runsin1stOver.Runsin1stOverData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                        string M1T2Name = team2Name;
                                                        ViewBag.M1T2Name = M1T2Name;
                                                        Runsin1stOver.Team2 = M1T2Name;
                                                        ViewBag.OddsTowT2 = Odds1;
                                                        Runsin1stOver.Odds2 = Convert.ToString(Odds1);
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
                                                    Runsin1stOver.Runsin1stOverData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                    string M1T2Name = team2Name;
                                                    ViewBag.M1T2Name = M1T2Name;
                                                    Runsin1stOver.Team2 = M1T2Name;
                                                    ViewBag.OddsTowT2 = Odds10;
                                                    Runsin1stOver.Odds2 = Convert.ToString(Odds10);

                                                }
                                            }
                                        }
                                    }
                                }



                            }
                        }//First Over Runs Being Star Here
                        if (Obj.market.marketName.Equals("Highest Score First 15 Overs"))//HAMW Match Odds Being End Here   
                        {
                            ViewBag.HSIN15Overs = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string TowinMarketName = Obj.market.marketName;
                            ViewBag.TowinMarketName = TowinMarketName;
                            HSIN15Overs.MarketName = TowinMarketName;
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
                                HSIN15Overs.Team1 = "SUSPENDED";
                                HSIN15Overs.Team2 = "SUSPENDED";
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
                                                        HSIN15Overs.Team1 = "SUSPENDED";
                                                        HSIN15Overs.Team2 = "SUSPENDED";
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
                                                        HSIN15Overs.HSIN15OversData1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                        string M1T1Name = team1Name;
                                                        ViewBag.M1T1Name = M1T1Name;
                                                        HSIN15Overs.Team1 = M1T1Name;
                                                        ViewBag.OddsTowT1 = Odds0;
                                                        HSIN15Overs.Odds1 = Convert.ToString(Odds0);


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
                                                    HSIN15Overs.HSIN15OversData1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                    string M1T1Name = team1Name;
                                                    ViewBag.M1T1Name = M1T1Name;
                                                    HSIN15Overs.Team1 = M1T1Name;
                                                    // string Odds0 = Convert.ToString(Odds0);
                                                    ViewBag.OddsTowT1 = Odds00;
                                                    HSIN15Overs.Odds1 = Convert.ToString(Odds00);
                                                }
                                            }
                                            //}
                                        }
                                    }
                                }
                                //for index 1 Runner o
                                foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                {
                                    //string TowT2Data = null;
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
                                                        HSIN15Overs.Team1 = "SUSPENDED";
                                                        HSIN15Overs.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        HSIN15Overs.HSIN15OversData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                        string M1T2Name = team2Name;
                                                        ViewBag.M1T2Name = M1T2Name;
                                                        HSIN15Overs.Team2 = M1T2Name;
                                                        ViewBag.OddsTowT2 = Odds1;
                                                        HSIN15Overs.Odds2 = Convert.ToString(Odds1);
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
                                                    HSIN15Overs.HSIN15OversData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                    string M1T2Name = team2Name;
                                                    ViewBag.M1T2Name = M1T2Name;
                                                    HSIN15Overs.Team2 = M1T2Name;
                                                    ViewBag.OddsTowT2 = Odds10;
                                                    HSIN15Overs.Odds2 = Convert.ToString(Odds10);

                                                }
                                            }
                                        }
                                    }
                                }



                            }
                        }//Highest Score First 15 Overs Being Start Here
                        if (Obj.market.marketName.Equals("50 Scored? - 1st Innings"))//50 Scored? - 1st Innings Being Start Here   
                        {
                            ViewBag.HalfCenturyScIn1stInnings = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string TowinMarketName = Obj.market.marketName;
                            ViewBag.TowinMarketName = TowinMarketName;
                            HalfCenturyScIn1stInnings.MarketName = TowinMarketName;
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
                                HalfCenturyScIn1stInnings.Team1 = "SUSPENDED";
                                HalfCenturyScIn1stInnings.Team2 = "SUSPENDED";
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
                                            //string TowT1Data = null;
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
                                                        HalfCenturyScIn1stInnings.Team1 = "SUSPENDED";
                                                        HalfCenturyScIn1stInnings.Team2 = "SUSPENDED";
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
                                                        HalfCenturyScIn1stInnings.HlfCentryIn1InnsData1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                        string M1T1Name = team1Name;
                                                        ViewBag.M1T1Name = M1T1Name;
                                                        HalfCenturyScIn1stInnings.Team1 = M1T1Name;
                                                        ViewBag.OddsTowT1 = Odds0;
                                                        HalfCenturyScIn1stInnings.Odds1 = Convert.ToString(Odds0);


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
                                                    HalfCenturyScIn1stInnings.HlfCentryIn1InnsData1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                    string M1T1Name = team1Name;
                                                    ViewBag.M1T1Name = M1T1Name;
                                                    HalfCenturyScIn1stInnings.Team1 = M1T1Name;
                                                    // string Odds0 = Convert.ToString(Odds0);
                                                    ViewBag.OddsTowT1 = Odds00;
                                                    HalfCenturyScIn1stInnings.Odds1 = Convert.ToString(Odds00);
                                                }
                                            }
                                            //}
                                        }
                                    }
                                }
                                //for index 1 Runner o
                                foreach (var deciOdds0 in Obj.runnerDetails[1].runnerOdds.Values)
                                {
                                    //string TowT2Data = null;
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
                                                        HalfCenturyScIn1stInnings.Team1 = "SUSPENDED";
                                                        HalfCenturyScIn1stInnings.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        HalfCenturyScIn1stInnings.HlfCentryIn1InnsData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                        string M1T2Name = team2Name;
                                                        ViewBag.M1T2Name = M1T2Name;
                                                        HalfCenturyScIn1stInnings.Team2 = M1T2Name;
                                                        ViewBag.OddsTowT2 = Odds1;
                                                        HalfCenturyScIn1stInnings.Odds2 = Convert.ToString(Odds1);
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
                                                    HalfCenturyScIn1stInnings.HlfCentryIn1InnsData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                    string M1T2Name = team2Name;
                                                    ViewBag.M1T2Name = M1T2Name;
                                                    HalfCenturyScIn1stInnings.Team2 = M1T2Name;
                                                    ViewBag.OddsTowT2 = Odds10;
                                                    HalfCenturyScIn1stInnings.Odds2 = Convert.ToString(Odds10);

                                                }
                                            }
                                        }
                                    }
                                }



                            }
                        }//50 Score First Innins Being End Here
                        if (Obj.market.marketName.Equals("Century Scored? - 1st Innings"))//50 Scored? - 1st Innings Being Start Here   
                        {
                            ViewBag.CenturyScIn1stInnings = true;
                            //PeriodRow.Visible = false;
                            string MrktidMatchOdds = Obj.market.marketId;
                            string TowinMarketName = Obj.market.marketName;
                            ViewBag.TowinMarketName = TowinMarketName;
                            CenturyScIn1stInnings.MarketName = TowinMarketName;
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
                                CenturyScIn1stInnings.Team1 = "SUSPENDED";
                                CenturyScIn1stInnings.Team2 = "SUSPENDED";
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
                                                        CenturyScIn1stInnings.Team1 = "SUSPENDED";
                                                        CenturyScIn1stInnings.Team2 = "SUSPENDED";
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
                                                        CenturyScIn1stInnings.Centuryin1stInnsDat1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                        string M1T1Name = team1Name;
                                                        ViewBag.M1T1Name = M1T1Name;
                                                        CenturyScIn1stInnings.Team1 = M1T1Name;
                                                        ViewBag.OddsTowT1 = Odds0;
                                                        CenturyScIn1stInnings.Odds1 = Convert.ToString(Odds0);


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
                                                    CenturyScIn1stInnings.Centuryin1stInnsDat1 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
                                                    string M1T1Name = team1Name;
                                                    ViewBag.M1T1Name = M1T1Name;
                                                    CenturyScIn1stInnings.Team1 = M1T1Name;
                                                    // string Odds0 = Convert.ToString(Odds0);
                                                    ViewBag.OddsTowT1 = Odds00;
                                                    CenturyScIn1stInnings.Odds1 = Convert.ToString(Odds00);
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
                                                        CenturyScIn1stInnings.Team1 = "SUSPENDED";
                                                        CenturyScIn1stInnings.Team2 = "SUSPENDED";
                                                        //btnMatchOdds1.Visible = false;
                                                        //btnMatchOdds2.Visible = false;
                                                        //btnMatchOdds3.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        //hfMatchOdds2.Value = eventid + "@" + MrktNameMatchOdds + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings;
                                                        //lblMOT2.Text = team2Name;
                                                        //btnMatchOdds2.Text = Convert.ToString(Odds1);
                                                        CenturyScIn1stInnings.Centuryin1stInnsData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                        string M1T2Name = team2Name;
                                                        ViewBag.M1T2Name = M1T2Name;
                                                        CenturyScIn1stInnings.Team2 = M1T2Name;
                                                        ViewBag.OddsTowT2 = Odds1;
                                                        CenturyScIn1stInnings.Odds2 = Convert.ToString(Odds1);
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
                                                    CenturyScIn1stInnings.Centuryin1stInnsData2 = eventid + "@" + TowinMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
                                                    string M1T2Name = team2Name;
                                                    ViewBag.M1T2Name = M1T2Name;
                                                    CenturyScIn1stInnings.Team2 = M1T2Name;
                                                    ViewBag.OddsTowT2 = Odds10;
                                                    CenturyScIn1stInnings.Odds2 = Convert.ToString(Odds10);

                                                }
                                            }
                                        }
                                    }
                                }



                            }
                        }//100 Score First Innings  Being End Here
                        if (Obj.market.marketName.Equals("Draw No Bet"))//HAMW Match Odds Being End Here   
                        {
                            ViewBag.DNBetMarket = true;
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
                                                        DrawNoBet.DNBOddsDAta1 = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                    DrawNoBet.DNBOddsDAta1 = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds00 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                        DrawNoBet.DNBOddsDAta2 = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                                                    DrawNoBet.DNBOddsDAta2 = eventid + "@" + DraNBetMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                            ViewBag.DBLChangeMarket = true;
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
                                                        DBLChance.DBCOddsDAta1 = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                    DBLChance.DBCOddsDAta1 = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds0 + "@" + team1Name + "@" + Innings + "@" + EventName;
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
                                                        DBLChance.DBCOddsDAta2 = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds1 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                                                    DBLChance.DBCOddsDAta2 = eventid + "@" + DBlChnceMarketName + "@" + MarketSatus + "@" + MarketTime + "@" + Odds10 + "@" + team2Name + "@" + Innings + "@" + EventName;
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
                                

                            }
                        }//Double Change Bet Being End Here  



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
        //Account ac = new Account();


        // Insert Bet Into Data base
        public int InsertBet()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("InsertPreBet", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        int userId = Convert.ToInt32(Session["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        //cmd.Parameters.AddWithValue("@UserId", 57);
                        cmd.Parameters.AddWithValue("@EventId", Convert.ToInt32(LiveData.eventId));
                        cmd.Parameters.AddWithValue("@TeamName", LiveData.TeamName);
                        cmd.Parameters.AddWithValue("@Odd", LiveData.betOdd);
                        cmd.Parameters.AddWithValue("@CurrentDateTime", System.DateTime.Now);
                        cmd.Parameters.AddWithValue("@Match", LiveData.matchName);
                        cmd.Parameters.AddWithValue("@MatchStartDate", LiveData.matchStartDateTime);
                        cmd.Parameters.AddWithValue("@MatchStatus", LiveData.matchStatus);
                        cmd.Parameters.AddWithValue("@Amount", LiveData.stake);
                        cmd.Parameters.AddWithValue("@BlncReturns", Convert.ToDouble(LiveData.balanceReturn));
                        cmd.Parameters.AddWithValue("@LossProfit", "Open");
                        cmd.Parameters.AddWithValue("@Result", "pending");
                        cmd.Parameters.AddWithValue("@Json", LiveData.json);
                        cmd.Parameters.AddWithValue("@BetType", LiveData.MarketName);
                        cmd.Parameters.AddWithValue("@Status", "Open");
                        cmd.Parameters.AddWithValue("@OddSide", LiveData.BetName);
                        cmd.Parameters.AddWithValue("@remove", 0);
                        cmd.Parameters.AddWithValue("@block", 0);
                        //cmd.ExecuteNonQuery();
                        // DivHAMWBTSLP.Visible = false;
                        if (LiveData.json.Contains(LiveData.MarketName))
                        {
                            int status = cmd.ExecuteNonQuery();
                            return status;
                        }
                        else
                        {
                            Response.Write("<script>alert('This market has been rejected by the system as the market price has changed or the market may be suspended/inactive currently. Please try again.');</script>");
                        }
                        return 0;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
            }
        }
        // Balance Updates
        private int BlockUserBalance(double userUpdatedBalance)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                    SqlCommand cmd = new SqlCommand("UserUpdateBalance", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Balance", userUpdatedBalance);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();

                    return i;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        // Function for Get Currency
        private string GetCurrencyCheck(int v)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("UserCurrencyCheck", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", v);
                    conn.Open();

                    var currency = cmd.ExecuteScalar();

                    return currency.ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //Funtion for Get Amount
        private double GetAmountCheck(int v)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("UserAmountCheck", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", v);
                    conn.Open();

                    var amount = cmd.ExecuteScalar();

                    return Convert.ToDouble(amount);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //Function for Check Amonut
        private bool CheckAmount(string currncy, int stake)
        {
            bool amtCheck = true;

            if (currncy == "USD")
            {
                if (stake < 15)
                {
                    string Massage = "Min Bet Size is 15 USD";
                    ViewBag.Massage = Massage;
                   // lblMassge.ForeColor = System.Drawing.Color.Red;
                    amtCheck = false;

                }
            }
            if (currncy == "INR")
            {
                if (stake < 1000)
                {
                    string Massage = "Min Bet Size is 1000 INR";
                    ViewBag.Massage = Massage;
                    //lblMassge.ForeColor = System.Drawing.Color.Red;
                    amtCheck = false;
                }
            }
            if (currncy == "EUR")
            {
                if (stake < 13)
                {
                    string Massage = "Min Bet Size is 13 EUR";
                    ViewBag.Massage = Massage;
                    amtCheck = false;
                }
            }
            if (currncy == "GBP")
            {
                if (stake < 11)
                {
                    string Massage = "Min Bet Size is 11 GBP";
                    ViewBag.Massage = Massage;
                    amtCheck = false;
                }
            }
            if (currncy == "AED")
            {
                if (stake < 65)
                {
                    string Massage = "Min Bet Size is 65 AED";
                    ViewBag.Massage = Massage;
                    amtCheck = false;
                }

            }
            if (currncy == "CAD")
            {
                if (stake < 20)
                {
                    string Massage = "Min Bet Size is 20 CAD";
                    ViewBag.Massage = Massage;
                    amtCheck = false;
                }
            }
            if (currncy == "AUD")
            {
                if (stake < 20)
                {
                    string Massage = "Min Bet Size is 20 AUD";
                    ViewBag.Massage = Massage;
                    amtCheck = false;
                }
            }
            return amtCheck;
        }

        [HttpPost]
        // Function for Bet Place Button OnClick     
        public ActionResult btn_PlaceBet(AccountViewModel gt)
        {
            string GetLiveBetData = Betslip.LiveData;
            string[] LiveBetData = GetLiveBetData.Split('@');
            LiveData.eventId = LiveBetData[0];
            LiveData.MarketName = LiveBetData[1];
            LiveData.matchStatus = LiveBetData[2];
            LiveData.betOdd = LiveBetData[4];
            LiveData.BetName = LiveBetData[5];
            LiveData.matchStartDateTime = LiveBetData[3];
            LiveData.TeamName = LiveBetData[6];
            LiveData.matchName = LiveBetData[7];
            LiveData.stake = gt.GetBetslipData.Stake;
            LiveData.balanceReturn = gt.GetBetslipData.Totalreturn;
            LiveData.json = Session["json"].ToString();
            //check Amounts for for
          if (LiveData.stake == null)
          {
                string Massage = "Please enter the Amount to bet";
                ViewBag.Massage = Massage;
          }
          else
          { 
            // check User Login
            if (Session["UserNameRandomGen"] == null && Session["Password"] == null)
            {
                string Massage = "Please Login to Place Bet";
                ViewBag.Massage = Massage;
            }
            else
            {
              int stake = 0;
              double calutateAmount = 0f;
              bool Stake = Int32.TryParse(LiveData.stake, out stake);
              if (Stake != false)
              {
                //int userId = Convert.ToInt32(Session["UserId"].ToString());
                int userId = Convert.ToInt32(Session["UserId"].ToString());
                string currncy = GetCurrencyCheck(userId);

                bool amtCheck = CheckAmount(currncy, stake);
                if (amtCheck == false)
                {
                    // return;
                }
                else
                {
                    calutateAmount = stake * Convert.ToDouble(LiveData.betOdd);

                    double userAmt = GetAmountCheck(Convert.ToInt32(Session["UserId"].ToString()));
                    if (userAmt < stake)
                    {
                        string Massage = "Insufficient balance";
                        ViewBag.Massage = Massage;
                        //return;
                    }
                    // bool haBlock = true;
                    //bool fr6Block = true;
                    //bool fr10Block = true;
                    // bool fr12Block = true;
                    // bool fr20Block = true;
                    //bool roeBlock = true;
                    //bool ro20Block3W = true;
                    //CheckBlock(out haBlock, out fr6Block, out fr10Block, out fr12Block, out fr20Block, out roeBlock, out ro20Block3W);
                    //if (haBlock == true)
                    //{
                    //    lblMassge.Text = "Bet Not Place, Try Again";
                    //    lblMassge.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                    int status = InsertBet();
                    if (status == 1)
                    {
                        double userUpdatedBalance = userAmt - stake;
                        int i = BlockUserBalance(userUpdatedBalance);
                        if (i == 1)
                        {
                            //lblMassge.Text = "";
                            string Massage = "Bet Placed Successfully";
                            ViewBag.Massage = Massage;
                            //txtHAMWReturn.BackColor = System.Drawing.Color.Silver;
                            //txtHAMWStake.BackColor = System.Drawing.Color.Silver;
                            //txtMatchOddsRate1.BackColor = System.Drawing.Color.Silver;
                            //lblMassge.Text = message;
                            //lblMassge.ForeColor = System.Drawing.Color.Green;
                            //txtHAMWReturn.ReadOnly = true;

                            //btnHAMWBetPlace.Enabled = false;
                            //btnHAMWBetPlace.Visible = false;
                            //txtHAMWStake.ReadOnly = true;
                            //btnHAMWCancel.Text = "Back To Main";
                        }
                        else
                        {
                            //lblMassge.Text = "";
                            string message = "Please try again.your Bet Is Not Placed";
                            ViewBag.Massage = message;
                            //lblMassge.ForeColor = System.Drawing.Color.Red;
                            //return;
                        }
                    }
                    else
                    {
                        //lblMassge.Text = "";
                        string message = "Please try again. your Bet Is Not Placed";
                        ViewBag.Massage = message;
                        //lblMassge.Text = message;
                        //lblMassge.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
              else
            {

                //lblMassge.Text = "";
                string message = "Please enter Numeric Value";
                ViewBag.Massage = message;

                //lblMassge.Text = message;
                //lblMassge.ForeColor = System.Drawing.Color.Red;

            }
            }
          }
            Cricket();
            return View("Cricket");
        }


        public ActionResult btn1MatchOdds_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

             Betslip.EventName = MatchOdds.EventNAme;
             Betslip.MarketNnme = MatchOdds.MarketName;
             Betslip.Selection = MatchOdds.Team1;
             Betslip.Rate = MatchOdds.Odds1;
             Betslip.LiveData = MatchOdds.MatchOddsDAta1;
            //Response.Redirect(Request.RawUrl);
            // return RedirectToAction("Cricket");
            //Response.Redirect("Cricket");
            //Response.Redirect(Request.RawUrl);
           // Market_display();
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2MatchOdds_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = MatchOdds.EventNAme;
            Betslip.MarketNnme = MatchOdds.MarketName;
            Betslip.Selection = MatchOdds.Team2;
            Betslip.Rate = MatchOdds.Odds2;
            Betslip.LiveData = MatchOdds.MatchOddsDAta2;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn3MatchOdds_Onclick()
        {
            Betslip.EventName = MatchOdds.EventNAme;
            Betslip.MarketNnme = MatchOdds.MarketName;
            Betslip.Selection = MatchOdds.Team3;
            Betslip.Rate = MatchOdds.Odds3;
            Betslip.LiveData = MatchOdds.MatchOddsDAta3;
            return RedirectToAction("Cricket");


        }

        // To win the Toss
        public ActionResult btn1ToWnToss_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = TowWintheToss.EventNAme;
            Betslip.MarketNnme = TowWintheToss.MarketName;
            Betslip.Selection = TowWintheToss.Team1;
            Betslip.Rate = TowWintheToss.Odds1;
            Betslip.LiveData = TowWintheToss.TWTOddsDAta1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2ToWnToss_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = TowWintheToss.EventNAme;
            Betslip.MarketNnme = TowWintheToss.MarketName;
            Betslip.Selection = TowWintheToss.Team2;
            Betslip.Rate = TowWintheToss.Odds2;
            Betslip.LiveData = TowWintheToss.TWTOddsDAta2;
            return RedirectToAction("Cricket");


        }

        // Runs in 1st Over
        public ActionResult btn1_Runsin1stOver()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = Runsin1stOver.EventNAme;
            Betslip.MarketNnme = Runsin1stOver.MarketName;
            Betslip.Selection = Runsin1stOver.Team1;
            Betslip.Rate = Runsin1stOver.Odds1;
            Betslip.LiveData = Runsin1stOver.Runsin1stOverData1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2_Runsin1stOver()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = Runsin1stOver.EventNAme;
            Betslip.MarketNnme = Runsin1stOver.MarketName;
            Betslip.Selection = Runsin1stOver.Team2;
            Betslip.Rate = Runsin1stOver.Odds2;
            Betslip.LiveData = Runsin1stOver.Runsin1stOverData2;
            return RedirectToAction("Cricket");


        }

        // Runs in heigset in 15th Over
        public ActionResult btn1_HSIN15Overs()
        {
            //MatchOdds mo = new MatchOdds();
            Betslip.EventName = HSIN15Overs.EventNAme;
            Betslip.MarketNnme = HSIN15Overs.MarketName;
            Betslip.Selection = HSIN15Overs.Team1;
            Betslip.Rate = HSIN15Overs.Odds1;
            Betslip.LiveData = HSIN15Overs.HSIN15OversData1;
            return RedirectToAction("Cricket");
        }
        public ActionResult btn2_HSIN15Overs()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = HSIN15Overs.EventNAme;
            Betslip.MarketNnme = HSIN15Overs.MarketName;
            Betslip.Selection = HSIN15Overs.Team2;
            Betslip.Rate = HSIN15Overs.Odds2;
            Betslip.LiveData = HSIN15Overs.HSIN15OversData2;
            return RedirectToAction("Cricket");


        }

        // Runs in Half Century Scored In 1st Innings in
        public ActionResult btn1_HalfCenturyScIn1stInnings()
        {
            //MatchOdds mo = new MatchOdds();
            Betslip.EventName = HalfCenturyScIn1stInnings.EventNAme;
            Betslip.MarketNnme = HalfCenturyScIn1stInnings.MarketName;
            Betslip.Selection = HalfCenturyScIn1stInnings.Team1;
            Betslip.Rate = HalfCenturyScIn1stInnings.Odds1;
            Betslip.LiveData = HalfCenturyScIn1stInnings.HlfCentryIn1InnsData1;
            return RedirectToAction("Cricket");
        }
        public ActionResult btn2_HalfCenturyScIn1stInnings()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = HalfCenturyScIn1stInnings.EventNAme;
            Betslip.MarketNnme = HalfCenturyScIn1stInnings.MarketName;
            Betslip.Selection = HalfCenturyScIn1stInnings.Team2;
            Betslip.Rate = HalfCenturyScIn1stInnings.Odds2;
            Betslip.LiveData = HalfCenturyScIn1stInnings.HlfCentryIn1InnsData2;
            return RedirectToAction("Cricket");


        }

        // Runs in Half Century Scored In 1st Innings in
        public ActionResult btn1_CenturyScIn1stInnings()
        {
            //MatchOdds mo = new MatchOdds();
            Betslip.EventName = CenturyScIn1stInnings.EventNAme;
            Betslip.MarketNnme = CenturyScIn1stInnings.MarketName;
            Betslip.Selection = CenturyScIn1stInnings.Team1;
            Betslip.Rate = CenturyScIn1stInnings.Odds1;
            Betslip.LiveData = CenturyScIn1stInnings.Centuryin1stInnsDat1;
            return RedirectToAction("Cricket");
        }
        public ActionResult btn2_CenturyScIn1stInnings()
        {
            //MatchOdds mo = new MatchOdds();
            Betslip.EventName = CenturyScIn1stInnings.EventNAme;
            Betslip.MarketNnme = CenturyScIn1stInnings.MarketName;
            Betslip.Selection = CenturyScIn1stInnings.Team2;
            Betslip.Rate = CenturyScIn1stInnings.Odds2;
            Betslip.LiveData = CenturyScIn1stInnings.Centuryin1stInnsData2;
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
            Betslip.LiveData= DrawNoBet.DNBOddsDAta1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2DRNT_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = DrawNoBet.EventNAme;
            Betslip.MarketNnme = DrawNoBet.MarketName;
            Betslip.Selection = DrawNoBet.Team2;
            Betslip.Rate = DrawNoBet.Odds2;
            Betslip.LiveData = DrawNoBet.DNBOddsDAta2;
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
            Betslip.LiveData= DBLChance.DBCOddsDAta1;
            return RedirectToAction("Cricket");


        }
        public ActionResult btn2DBL_Onclick()
        {
            //MatchOdds mo = new MatchOdds();

            Betslip.EventName = DBLChance.EventNAme;
            Betslip.MarketNnme = DBLChance.MarketName;
            Betslip.Selection = DBLChance.Team2;
            Betslip.Rate = DBLChance.Odds2;
            Betslip.LiveData = DBLChance.DBCOddsDAta2;
            return RedirectToAction("Cricket");


        }
    }
}