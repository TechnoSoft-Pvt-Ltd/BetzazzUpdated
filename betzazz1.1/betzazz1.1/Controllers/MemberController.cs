using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using betzazz1._1.ViewModels;
using System.Net.Mail;

namespace betzazz1._1.Controllers
{
    public class MemberController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        // GET: Member
        public ActionResult UserProfile()
        {
            try
            {
                using (SqlCommand cmd1 = new SqlCommand("ModalDataProc", con))
                {
                    cmd1.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd1.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    ViewBag.ds = ds;
                }
            }
            catch (Exception ex1)
            {
                throw ex1;
            }

            return View();
        }
        public ActionResult Change_Password(AccountViewModel avm3)
        {
            try
            {
                string NPass = avm3.account.NPassword;
                string CPass = avm3.account.CPassword;
                string email = avm3.account.emailid;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update Modaldata set createPass='" + NPass.ToString() + "',CreatedDate='" + DateTime.Today.ToShortDateString() + "' where UserId='" + Session["UserId"] + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Update Successfull!');</script>");
                string msgsubject = "New Password";

                string msgbody = "Hello, " + Session["Username"] + "!\nYour Password has been updated successfully.\n" +
                "Your new password is " + NPass.ToString() + "\n\nBest\nTeam ZAZZ ";

                MailMessage msg = new MailMessage("accounts@betzazz.com", email.ToString());
                msg.Subject = msgsubject.ToString();
                msg.Body = msgbody;

                SmtpClient client = new SmtpClient("mail.betzazz.com", 587);
                client.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "accounts@betzazz.com",
                    Password = "Password@123"
                };
                client.EnableSsl = false;
                client.Send(msg);

                UserProfile();

                return View("UserProfile");
            }
            catch (Exception ex2)
            {
                throw ex2;
            }



        }
        public ActionResult Bank()
        {        
            try
            {
                using (SqlCommand cmd1 = new SqlCommand("BalanceHistoryPro", con))
                {
                    cmd1.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd1.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds = new DataSet();
                    da1.Fill(ds);
                    con.Close();
                    ViewBag.ds = ds;
                }
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
            return View();
        }
        public ActionResult Bets()
        {
            try
            {
                using (SqlCommand cmd1 = new SqlCommand("SP_BetHistorty", con))
                {
                    cmd1.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd1.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    con.Close();
                    ViewBag.ds1 = ds1;
                }

                using (SqlCommand cmd2 = new SqlCommand("SP_ActiveBets", con))
                {
                    cmd2.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd2.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    con.Close();
                    ViewBag.ds2 = ds2;
                }

                using (SqlCommand cmd3 = new SqlCommand("SP_WonBets", con))
                {
                    cmd3.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd3.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataSet WB = new DataSet();
                    da3.Fill(WB);
                    con.Close();
                    ViewBag.WB = WB;
                }

                using (SqlCommand cmd4 = new SqlCommand("SP_LoseBets", con))
                {
                    cmd4.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd4.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd4);
                    DataSet LB = new DataSet();
                    da3.Fill(LB);
                    con.Close();
                    ViewBag.LB = LB;
                }
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
            return View();
        }
       
    }
}