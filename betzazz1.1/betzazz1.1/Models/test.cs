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
}