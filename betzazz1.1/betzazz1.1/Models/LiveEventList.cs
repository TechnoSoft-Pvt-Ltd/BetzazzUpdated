using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betzazz1._1.Models
{
    public class LiveEventList
    {
        public string success { get; set; }
        public System.Collections.ObjectModel.Collection<Results> results { get; set; }
    }
    public class Results
    {
        public string id { get; set; }
        public string sport_id { get; set; }
        public string time { get; set; }
        public string time_status { get; set; }
        public League league { get; set; }
        public Home home { get; set; }
        public Away away { get; set; }
    }

    public class League
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Home
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Away
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class FullNewEL
    {
        public string matchId { get; set; }
        public string time { get; set; }
        public string timeStatus { get; set; }
        public string leagueId { get; set; }
        public string leagueName { get; set; }
        public string homeTeamId { get; set; }
        public string homeTeamName { get; set; }
        public string awayTeamId { get; set; }
        public string awayTeamName { get; set; }
        public string sport_id { get; set; }
    }
    public class FullNewELFT
    {
        public string matchId { get; set; }
        public string time { get; set; }
        public string timeStatus { get; set; }
        public string leagueId { get; set; }
        public string leagueName { get; set; }
        public string homeTeamId { get; set; }
        public string homeTeamName { get; set; }
        public string awayTeamId { get; set; }
        public string awayTeamName { get; set; }
        public string sport_id { get; set; }

    }
}