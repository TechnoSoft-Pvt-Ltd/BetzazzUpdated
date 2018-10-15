using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace betzazz1._1.Controllers
{
    public class InplayController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        public static string Eventid { get; set; }
        public string json { get; set; }
        // GET: Inplay
        public ActionResult Cricket()
        {
            ViewBag.ShowDiv = false;
            GetJson();
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

        // Funtion for get JSON String from Data Base where Event id=Eventid?
        public  void GetJson()
        {
            try
            {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select jsoncripodds from cripodds  where  EventsId=" + Request.QueryString["Eventid"] + " ", con);
                    cmd.Connection = con;
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        json = reader.GetString(0);
                    }
                   con.Close();
                
            }
            catch(Exception er)
            {

            }
        }
    }
}