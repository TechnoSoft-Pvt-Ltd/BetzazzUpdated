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
    public class EventsController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        //SqlCommand cmd = new SqlCommand();
        // GET: Events
        public ActionResult InPlay()
        {
            string GetJson = null;
            con.Open();
            using(SqlCommand cmd=new SqlCommand("select  JSONString from BTExchaneEvntList", con))
            {
                cmd.Connection = con;
                using (var reader =cmd.ExecuteReader())
                {
                    reader.Read();
                    GetJson = reader.GetString(0).ToString();
                }
                con.Close();
            }

                return View();
        }
        public ActionResult PreMatch()
        {
            return View();
        }
    }
}