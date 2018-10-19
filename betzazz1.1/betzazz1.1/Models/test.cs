using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betzazz1._1.Models
{
    public class test
    {
        public string LeagueName { get; set; }
        public string  Eventid { get; set; }
        public string EventName { get; set; }

    }
    public class ViewModel
    {
        public dynamic list { get; set; }      
        public IList<string> Links { get; set; }
    }
    public class List
    {
        public string LeagueName { get; set; }
        public string Eventid { get; set; }
        public string EventName { get; set; }
    }
    //Class for Betslip

    public  class Betslip
    {

        public static string EventName = "";/* { get; set; }*/
        public static string MarketNnme { get; set; }
        public static string Selection { get; set; }
        public static string Rate { get; set; }
    }


    public class MatchOdds
    {
        public static string EventNAme { get; set; }
        public static string MarketName { get; set; }
        public static string MarketTime { get; set; }
        public static string Team1 { get; set; }

        public static string Odds1 { get; set; }

        public static string Team2 { get; set; }
        public static string Odds2 { get; set; }
    }

    // For To win the toss
    public class TowWintheToss
    {
        public static string EventNAme { get; set; }
        public static string MarketName { get; set; }
        public static string MarketTime { get; set; }
        public static string Team1 { get; set; }

        public static string Odds1 { get; set; }

        public static string Team2 { get; set; }
        public static string Odds2 { get; set; }
    }

    // For Draw No Bet Market
    public class DrawNoBet
    {
        public static string EventNAme { get; set; }
        public static string MarketName { get; set; }
        public static string MarketTime { get; set; }
        public static string Team1 { get; set; }

        public static string Odds1 { get; set; }

        public static string Team2 { get; set; }
        public static string Odds2 { get; set; }
    }

    // For Double Chance
    public class DBLChance
    {
        public static string EventNAme { get; set; }
        public static string MarketName { get; set; }
        public static string MarketTime { get; set; }
        public static string Team1 { get; set; }

        public static string Odds1 { get; set; }

        public static string Team2 { get; set; }
        public static string Odds2 { get; set; }
    }
}