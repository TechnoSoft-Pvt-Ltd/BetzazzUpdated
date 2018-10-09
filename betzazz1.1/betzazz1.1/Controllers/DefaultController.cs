using betzazz1._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using betzazz1._1.ViewModels;
using System.Net.Mail;
using System.Net;
using System.Web.UI;
using System.Configuration;

namespace betzazz1._1.Controllers
{
   
    public class DefaultController : Controller
    {
        public static string UName { get; set; }
        public static Double Balance { get; set; }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        // GET: Default
        public ActionResult Index()
        {
            //ViewBag.ShowDiv = false;
            if (Session["UserNameRandomGen"] != null && Session["Password"]!= null)
            {
                ViewBag.Balance = Balance;
                ViewBag.UName = UName;
            }
           
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm1)
        {
            try
            {
                string userid = avm1.account.UserID;
                string userpass = avm1.account.Password;
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select ModlData.UserId, ModlData.UserName,ModlData.UserNameRandomGen,ModlData.CreatePass,UserBlnc.Balance from  ModalData as ModlData " +
                "left join UserBalance as UserBlnc on ModlData.UserId = UserBlnc.UserId  where UserNameRandomGen =@username and CreatePass=@password", con))
                {
                    //string UName = null;
                    //double Balance;
                    cmd.Parameters.AddWithValue("@username", userid.ToString());
                    cmd.Parameters.AddWithValue("@password", userpass.ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        if (userid.Equals(dr["UserNameRandomGen"].ToString()) && userpass.Equals(dr["CreatePass"].ToString()))
                        {
                            Session["UserId"] = dr["UserId"].ToString();
                            Session["UserNameRandomGen"] = dr["UserNameRandomGen"].ToString();
                            UName = dr["Username"].ToString();
                            Session["Balance"] = dr["Balance"].ToString();
                            Balance = Convert.ToDouble(dr["Balance"].ToString());
                            Session["Password"] = dr["CreatePass"].ToString();
                            ViewBag.Balance = Balance;
                            ViewBag.UName = UName;
                            Response.Write("<script>alert('You are successfully login!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Usename Or Password Incorrect');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Usename Or Password Incorrect !');</script>");
                    }
                }

            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            return View("Index");



        }

        public ActionResult SignUp(AccountViewModel avm2)
        {
            try
            {
                string UserName = avm2.account.Username;
                string Currency = avm2.account.currency;
                string Email = avm2.account.emailid;

                //code for random user name genrate
                string username = "0123456789";
                Random r1 = new Random();
                char[] myname = new char[6];
                for (int z = 0; z < 6; z++)
                {
                    myname[z] = username[(int)(10 * r1.NextDouble())];

                }
                string GetUserNAme = new string(myname);

                //code for random paasword genrate
                string pass = "abcdef56789ghijkldefghijklmnopqrstuvwxyzabcmnopqrstuvwxyz1234";
                Random r = new Random();
                char[] mypass = new char[5];
                for (int z = 0; z < 5; z++)
                {
                    mypass[z] = pass[(int)(60 * r.NextDouble())];

                }
                string GenUserPass = new string(mypass);

                //Code here for Mail and Register
                // MailMessage msg = new MailMessage();
                SqlCommand cmd = new SqlCommand();
                string activationurl = string.Empty;
                string emailid = string.Empty;
                string subject = string.Empty;
                string body = string.Empty;

                // SqlConnection con = new SqlConnection(connectionstring);
                cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("username", UserName.ToString());
                cmd.Parameters.AddWithValue("UserNameRandomGen", "BZ" + GetUserNAme.ToString());
                cmd.Parameters.AddWithValue("AccountCuerrcy", Currency.ToString());
                cmd.Parameters.AddWithValue("useremail", Email.ToString());
                cmd.Parameters.AddWithValue("CreatePass", GenUserPass.ToString());

                SqlCommand cmd1 = new SqlCommand("SP_InsertBalanceReg", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@UserId", 566);
                cmd1.Parameters.AddWithValue("@Balance", "0");

                con.Open();

                int i = cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                con.Close();
                if (i < 1)
                {
                    Response.Write("<script>alert('User already registered!!');</script>");

                }
                else
                {
                    Response.Write("<script>alert('Registration Successfully');</script>");


                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    //int i=cmd.executenonquery();



                    subject = "confirmation email for account activation";

                    body = "waZZaaaa, " + UserName.ToString() + "!\n\nNice to see You in the field. Welcome to Betzazz.\n\n" +
                             "So you think you play the numbers as good as the players play the game?\n\n" +
                            "                                      OR                             \n\n" +
                            "Have you got “Betorade” running through your veins?\n\n" +
                            "                                      OR                          \n\n" +
                            "Are you the “Lord Of The Odds” that everyone prays to?\n\n" +
                            "Well then punter - you are in good company!\n\n" +
                            "Add ZAZZ to your sports experience and get ready for the match.\n\n" +
                            "IT'S GAME ON @BETZAZZ\n\nBest\nTeam ZAZZ ";

                    //SendMail(msgsubject, msgbody);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "message", "alert('confirmation link to activate your account has been sent to your email address');", true);

                    MailMessage msg = new MailMessage("accounts@betzazz.com", Email.ToString());
                    msg.Subject = subject.ToString();
                    msg.Body = body;

                    SmtpClient client = new SmtpClient("5.77.50.42", 587);
                    client.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "accounts@betzazz.com",
                        Password = "Password@123"
                    };
                    client.EnableSsl = false;
                    client.Send(msg);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Index");
        }
    }
}