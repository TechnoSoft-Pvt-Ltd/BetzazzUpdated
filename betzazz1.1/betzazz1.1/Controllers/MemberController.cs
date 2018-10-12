using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace betzazz1._1.Controllers
{
    public class MemberController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        // GET: Member
        public ActionResult UserProfile()
        {
           
            return View();
        }
        public ActionResult Bank()
        {
          

            try
            {
                using (SqlCommand cmd = new SqlCommand("BalanceHistoryPro", con))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    ViewBag.ds = ds;
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public ActionResult Bets()
        {
            return View();
        }
       
    }
}