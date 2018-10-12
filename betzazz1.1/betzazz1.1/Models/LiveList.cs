using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace betzazz1._1.Models
{
    public class LiveList
    {
        public IEnumerable<ftResult> results { get; set; }
    }
    public class ftResult
    {
        public string id { get; set; }
        public string sport_id { get; set; }
        public string time { get; set; }
        public string time_status { get; set; }
        public LeagueName league { get; set; }
        public T1Home home { get; set; }
        public T2Away away { get; set; }

    }
    public class LeagueName
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class T1Home
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class T2Away
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}