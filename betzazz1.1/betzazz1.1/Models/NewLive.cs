using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betzazz1._1.Models
{
    public class NewLive
    {
        public string success { get; set; }
        public System.Collections.ObjectModel.Collection<ResultsLive> results { get; set; }
    }

    public class ResultsLive
    {
        public TimeLine timeline { get; set; }
        // public Dictionary<string, SportsBookMarket> sportsBookMarket { get; set; }

        public System.Collections.ObjectModel.Collection<Markets> markets { get; set; }
        public Event Event { get; set; }
        public string markets_updated_at { get; set; }
        //public Competitions competitions { get; set; }
    }
    public class Event
    {
        public string eventId { get; set; }
        public string name { get; set; }
        public string openDate { get; set; }
    }
    //public class Competitions
    //{
    //    public string name { get; set; }
    //    public string competitionId { get; set; }
    //    public string eventid { get; set; }
    //}

    #region Markets
    public class Markets
    {
        public string bettingType { get; set; }
        public string marketId { get; set; }
        public string marketStatus { get; set; }              //INACTIVE , OPEN , CLOSED , SUSPENDED
        public string inPlay { get; set; }  //true , false

        public System.Collections.ObjectModel.Collection<RunnerDetails> runnerDetails { get; set; }
        //public System.Collections.ObjectModel.Collection<Market> market { get; set; }
        public Market market { get; set; }
    }
    public class RunnerDetails
    {
        public string runnerStatus { get; set; }
        public string selectionId { get; set; }
        public dynamic runnerOdds { get; set; }
    }
    public class RunnerOdds
    {
        public DecimalDisplayOdds decimalDisplayOdds { get; set; }
    }
    public class DecimalDisplayOdds
    {
        public string decimalOdds { get; set; }
    }
    public class Market
    {
        public string bettingType { get; set; }
        public string eventId { get; set; }
        public string inPlay { get; set; }
        public string marketId { get; set; }
        public string marketType { get; set; }
        public string marketName { get; set; }
        public string marketStatus { get; set; }
        public string marketTime { get; set; }
        public System.Collections.ObjectModel.Collection<Runners> runners { get; set; }
    }
    public class Runners
    {
        public string runnerStatus { get; set; }
        public string runnerName { get; set; }
        public string selectionId { get; set; }
        // public Result result { get; set; }
        public string handicap { get; set; }

    }
    
    public class TimeLine
    {
        public string status { get; set; }    //like IN_PLAY
        public string inPlayMatchStatus { get; set; }    //Like InPlay , BetweenInnings
        public Score score { get; set; }
        public string matchStatus { get; set; }

    }
    public class Score
    {
        public AwayLive away { get; set; }
        public HomeLive home { get; set; }

    }
    public class AwayLive
    {
        public string highlight { get; set; }     //true -- betting  , false -- fielding
        public string name { get; set; }
        public InningAway inning1 { get; set; }
        public InningAway2 inning2 { get; set; }
    }
    public class InningAway
    {
        public string overs { get; set; }
        public string wickets { get; set; }
        public string runs { get; set; }
    }
    public class InningAway2
    {
        public string overs { get; set; }
        public string wickets { get; set; }
        public string runs { get; set; }
    }
    public class HomeLive
    {
        public string highlight { get; set; }
        public string name { get; set; }
        public InningHome inning1 { get; set; }
        public InningHome2 inning2 { get; set; }
    }
    public class InningHome
    {
        public string overs { get; set; }
        public string wickets { get; set; }
        public string runs { get; set; }
    }
    public class InningHome2
    {
        public string overs { get; set; }
        public string wickets { get; set; }
        public string runs { get; set; }
    }

    #endregion

    public class FullNewLive
    {
        public string success { get; set; }
        public string TLStatus { get; set; }
        public string TLInPlayMatchStatus { get; set; }
        public string TLAwayHighLight { get; set; }
        public string TLAwayName { get; set; }
        public string TLAwayOvers { get; set; }
        public string TLAwayRuns { get; set; }
        public string TLAwayWickets { get; set; }
        public string TLHomeHighLight { get; set; }
        public string TLHomeName { get; set; }
        public string TLHomeOvers { get; set; }
        public string TLHomeRuns { get; set; }
        public string TLHomeWickets { get; set; }
        public string checkInning1 { get; set; }
        public string checkInning2 { get; set; }

        public string sbMarketId { get; set; }
        public string sbMarketStatus { get; set; }
        public string sbMarketName { get; set; }
        public string sbMarketType { get; set; }
        public string sbInPlay { get; set; }

        public string sbRunnerStatus1 { get; set; }
        public string sbRunnerName1 { get; set; }
        public string sbSelectionId1 { get; set; }
        public string sbTypeHomeAway1 { get; set; }
        public string sbRunnerStatus2 { get; set; }
        public string sbRunnerName2 { get; set; }
        public string sbSelectionId2 { get; set; }
        public string sbTypeHomeAway2 { get; set; }
    }

}