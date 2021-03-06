﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using betzazz1._1.BusnessLogics;
using betzazz1._1.Models;
using System.Web.Script.Serialization;
using System.Collections;
using System.Web.Security;
using betzazz1._1.ViewModels;
using System.Net.Mail;
using Newtonsoft.Json;

namespace betzazz1._1.Controllers
{
    public class EventsController : Controller
    {
        public string FottballInplay { get; set; }

        public static string UName { get; set; }
        public static Double Balance { get; set; }
        public static string Currency { get; set; }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        string msgsubject = string.Empty;
        string msgbody = string.Empty;
        Global GBLClass = new Global();
        LiveEventList Live = new LiveEventList();

        //SqlCommand cmd = new SqlCommand();
        public static int crleagueid { get; set; }
        public static int ftleagueid { get; set; }
        public static string LeagueName { get; set; }
        public static string T20LeagueName { get; set; }
        public static string ODILeagueName { get; set; }

        // Methods for Inplay Cricket and Football
        public ActionResult InPlay(string[] ArrTestData, string[] ArrT20Data, string[] ArrODIData,string[] Evntid, string[] T20EventId, string[] ODIEventId)
        {
            //test ts=new test();
                GBLClass.InplayCR();
           

            string GetTestData = GBLClass.TestData;
            ArrTestData = GetTestData.Split('@');
            int TestCount = ArrTestData.Length;
          //  ViewBag.TestCount = TestCount;
            Session["TestCount"] = TestCount;
            ViewBag.ArrTestData = ArrTestData;                       
            ViewBag.LeagueName = GBLClass.TestLeagueName;
            Session["LeagueName"] = GBLClass.TestLeagueName; 
            string id = GBLClass.EventId;
            Evntid = id.Split('@');
            ViewBag.Evntid = Evntid;         
            //Cricket for Test Event id
            ArrayList Eid = new ArrayList();
            for (int i = 0; i < ArrTestData.Length; i++)
            {
                Eid.Add(Evntid[i]);
                Session["Evntid"] = Eid;
            }
            // for Test Event Name
            ArrayList displayDetail1 = new ArrayList();
            for (int j = 0; j < ArrTestData.Length; j++)
            {
                displayDetail1.Add(ArrTestData[j]);
                Session["TstEventName"] = displayDetail1;
            }




            string GetT20Data = GBLClass.T20Data;
            ArrT20Data = GetT20Data.Split('@');
            int T20Count = ArrT20Data.Length;
            //ViewBag.T20Count = T20Count;
            Session["T20Count"] = T20Count;
            ViewBag.ArrT20Data = ArrT20Data;
            ViewBag.T20LeagueName = GBLClass.T20LeagueName;
            Session["T20LeagueName"] = GBLClass.T20LeagueName;
            string idT20 = GBLClass.EventId1;
            T20EventId = idT20.Split('@');
            ViewBag.T20EventId = T20EventId;
            //Cricket for T20 Event id
            ArrayList T20Eid = new ArrayList();
            for (int k = 0; k < ArrT20Data.Length; k++)
            {
                T20Eid.Add(T20EventId[k]);
                Session["T20Evntid"] = T20Eid;
            }
            // for Test Event Name
            ArrayList T20EventName = new ArrayList();
            for (int l = 0; l < ArrT20Data.Length; l++)
            {
                T20EventName.Add(ArrT20Data[l]);
                Session["T20EventName"] = T20EventName;
            }


            string GetODIData = GBLClass.ODIData;
            ArrODIData = GetODIData.Split('@');
            int ODICount = ArrODIData.Length;
            Session["ODICount"] = ODICount;      
            ViewBag.ArrODIData = ArrODIData;
            ViewBag.ODILeagueName = GBLClass.ODILeagueName;
            Session["ODILeagueName"] = GBLClass.ODILeagueName;
            string ODIid = GBLClass.EventId2;
            ODIEventId = ODIid.Split('@');
            ViewBag.ODIEventId = ODIEventId;

            //Cricket for ODI Event id
            ArrayList ODIEid = new ArrayList();
            for (int k = 0; k < ArrODIData.Length; k++)
            {
                ODIEid.Add(ODIEventId[k]);
                Session["ODIEvntid"] = ODIEid;
            }
            // for Test Event Name
            ArrayList ODIEventName = new ArrayList();
            for (int l = 0; l < ArrODIData.Length; l++)
            {
                ODIEventName.Add(ArrODIData[l]);
                Session["ODIEventName"] = ODIEventName;
            }



            int CRTotalEvent = TestCount + T20Count + ODICount;
            ViewBag.CRTotalEvent = CRTotalEvent;
            Session["CRTotalEvent"] = CRTotalEvent;



            FottballInplay = GBLClass.FotballIPGetJson;

           // var deserialized = JsonConvert.DeserializeObject<LiveList>(FottballInplay);

            var serilezer = new JavaScriptSerializer();



            LiveEventList LiveFT = serilezer.Deserialize<LiveEventList>(FottballInplay);
            
            //int count = LiveFT.results.Count();
            //ArrayList newList = new ArrayList();
            //ArrayList newList1 = new ArrayList();
            //ArrayList Arr1 = new ArrayList();
            //for (int i=0;i<count;i++)
            //{
            //    ArrayList ArrList = new ArrayList();
            //    string MatchId= LiveFT.results[i].id;
            //    string Sportid = LiveFT.results[i].sport_id;
            //    string Time= LiveFT.results[i].time;
            //    string TimeStatus= LiveFT.results[i].time_status;
            //    string leagueId= LiveFT.results[i].league.id;
            //    string leagueName= LiveFT.results[i].league.name;
            //    string EventName= LiveFT.results[i].home.name+" VS "+ LiveFT.results[i].away.name;

                
            //    ArrList.Add(MatchId);
            //    ArrList.Add(Sportid);
            //    ArrList.Add(Time);
            //    ArrList.Add(TimeStatus);
            //    ArrList.Add(leagueName);
            //    ArrList.Add(EventName);
                
            //    if (leagueName== ArrList[4].ToString())
            //    {
            //        newList.Add(ArrList);
            //    }
            //    else if(leagueName != ArrList[4].ToString())
            //    {
            //        newList1.Add(ArrList);
            //    }

            //   // Arr1.Add(newList);
            //}           
            ViewBag.LiveFT = LiveFT;
            return View();

        }

       

        // Methods for PreMatch Cricket and Football
        public ActionResult PreMatch(string[] ArrPMTestData,string[] PMTestEvntid, string[] ArrPMT20Data, string[] PMT20Evntid, string[] ArrPMODIData, string[] PMODIEvntid)
        {
            GBLClass.PreMatch();

            // For Test Matches.
            string GetTestData = GBLClass.PMTestData;

            string EDate = GBLClass.PMTestEventDate;
            string[] TestEventDate = EDate.Split('@');
            ViewBag.TestEventDate = TestEventDate;

            ArrPMTestData = GetTestData.Split('@');
            int PMTestCount = ArrPMTestData.Length;
            //  ViewBag.TestCount = TestCount;
            Session["PMTestCount"] = PMTestCount;
            ViewBag.ArrPMTestData = ArrPMTestData;
            ViewBag.PMLeagueName = GBLClass.PMTestLeagueName;
            Session["PMLeagueName"] = GBLClass.PMTestLeagueName;
            string id = GBLClass.PMEventId;
            PMTestEvntid = id.Split('@');
            ViewBag.PMTestEvntid = PMTestEvntid;
            //Cricket for Test Event id
            ArrayList Eid = new ArrayList();
            for (int i = 0; i < ArrPMTestData.Length; i++)
            {
                Eid.Add(PMTestEvntid[i]);
                Session["PMEvntid"] = Eid;
            }
            // for Test Event Name
            ArrayList displayDetail1 = new ArrayList();
            for (int j = 0; j < ArrPMTestData.Length; j++)
            {
                displayDetail1.Add(ArrPMTestData[j]);
                Session["PMTstEventName"] = displayDetail1;
            }


            // For Tweenty 20
            string GetT20tData = GBLClass.PMT20Data;
            string EDatet20 = GBLClass.PMT20EventDate;
            string[] T20EventDate = EDatet20.Split('@');
            ViewBag.T20EventDate = T20EventDate;
            ArrPMT20Data = GetT20tData.Split('@');
            int PMT20Count = ArrPMT20Data.Length;
            //  ViewBag.TestCount = TestCount;
            Session["PMT20Count"] = PMT20Count;
            ViewBag.ArrPMT20Data = ArrPMT20Data;
            ViewBag.PMT20LeagueName = GBLClass.PMT20LeagueName;
            Session["PMT20LeagueName"] = GBLClass.PMT20LeagueName;
            string idT20 = GBLClass.PMT20EventId;
            PMT20Evntid = idT20.Split('@');
            ViewBag.PMT20Evntid = PMT20Evntid;
            //Cricket for Test Event id
            ArrayList EidT20 = new ArrayList();
            for (int i = 0; i < ArrPMT20Data.Length; i++)
            {
                EidT20.Add(PMT20Evntid[i]);
                Session["PMEvntidT20"] = EidT20;
            }
            // for Test Event Name
            ArrayList displayDetailT20 = new ArrayList();
            for (int j = 0; j < ArrPMT20Data.Length; j++)
            {
                displayDetailT20.Add(ArrPMT20Data[j]);
                Session["PMT20EventName"] = displayDetailT20;
            }

            // For ODI
            string GetODIData = GBLClass.PMODIData;

            string EDateODI = GBLClass.PMODIEventDate;
            string[] ODIEventDate = EDateODI.Split('@');
            ViewBag.ODIEventDate = ODIEventDate;
            ArrPMODIData = GetODIData.Split('@');
            int PMODICount = ArrPMODIData.Length;
            ViewBag.PMODIEventDate = GBLClass.PMODIEventDate;
            //  ViewBag.TestCount = TestCount;
            Session["PMODICount"] = PMODICount;
            ViewBag.ArrPMODIData = ArrPMODIData;
            ViewBag.PMODILeagueName = GBLClass.PMODILeagueName;
            Session["PMODILeagueName"] = GBLClass.PMODILeagueName;
            string idODI = GBLClass.PMODIEventId;
            PMODIEvntid = idODI.Split('@');
            ViewBag.PMODIEvntid = PMODIEvntid;
            //Cricket for Test Event id
            ArrayList EidODI = new ArrayList();
            for (int i = 0; i < ArrPMODIData.Length; i++)
            {
                EidODI.Add(PMODIEvntid[i]);
                Session["ODIPMEvntid"] = EidODI;
            }
            // for Test Event Name
            ArrayList displayDetailODI = new ArrayList();
            for (int j = 0; j < ArrPMODIData.Length; j++)
            {
                displayDetailODI.Add(ArrPMODIData[j]);
                Session["PMODIEventName"] = displayDetailODI;
            }

            int PMCRTotalEvent = PMTestCount + PMT20Count + PMODICount;
            ViewBag.CRTotalEvent = PMCRTotalEvent;
            Session["PMCRTotalEvent"] = PMCRTotalEvent;

            return View();
        }




        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Inplay", "Events");
        }


        //Login
        [HttpPost]
        public ActionResult Login(AccountViewModel avm1)
        {
            try
            {
                string userid = avm1.account.UserID;
                string userpass = avm1.account.Password;
                if(userid!=null || userpass!=null)
                { 
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select ModlData.UserId, ModlData.UserName,ModlData.UserNameRandomGen,ModlData.CreatePass,ModlData.AccountCuerrcy,UserBlnc.Balance from  ModalData as ModlData " +
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
                            Session["UserName"] = dr["Username"].ToString();
                            UName = Session["UserName"].ToString();
                            Session["Balance"] = dr["Balance"].ToString();
                            Balance = Convert.ToDouble(dr["Balance"].ToString());
                            Session["Password"] = dr["CreatePass"].ToString();
                            Session["Currency"] = dr["AccountCuerrcy"].ToString();
                            Currency = Session["Currency"].ToString();
                            ViewBag.Balance = Balance;
                            ViewBag.UName = UName;
                            ViewBag.Currency = Currency;
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
                //else
                //{
                //    ViewBag.ErrorMassage = "Please enter Userid and Password";
                //}
            }
            catch (Exception ex)
            {
                con.Close();
               // throw ex;
            }
            return RedirectToAction("InPlay");


        }

        //Code for Forgot Password...
        public ActionResult Submit(AccountViewModel avm)
        {
            string email = avm.account.emailid;
            string password = avm.account.Password;
            try
            {
                //code for random genrate paasword for Forgot Password
                string pass = "abcdeABCDEFfghijklmn56789GHIJKopqrstuvwxyzLMNOPQR01234STUVWXYZ";
                Random r = new Random();
                char[] mypass = new char[5];
                for (int z = 0; z < 5; z++)
                {
                    mypass[z] = pass[(int)(60 * r.NextDouble())];

                }
                password = new string(mypass);

                con.Open();
                SqlCommand cmd = new SqlCommand("Update Modaldata set createPass='" + password.ToString() + "',CreatedDate='" + DateTime.Today.ToShortDateString() + "' where UserEmail='" + email.ToString() + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('New Password has been send to your email address!')</script>");
                msgsubject = "Forget Password";

                msgbody = "Hello,!\nYour Password has been Updated.\n" +
                          "Your new password is:- " + password.ToString() + "\n\nBest\nTeam ZAZZ ";

                string subject = msgsubject;
                string body = msgbody;
                MailMessage msg = new MailMessage("accounts@betzazz.com", email.ToString());
                msg.Subject = subject.ToString();
                msg.Body = body;

                SmtpClient client = new SmtpClient("mail.betzazz.com", 587);
                client.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "accounts@betzazz.com",
                    Password = "Password@123"
                };
                client.EnableSsl = false;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("InPlay");
        }

        //SignUp
        public void SignUp(AccountViewModel avm2)
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
            Response.Redirect("InPlay");
           // return RedirectToAction("");
        }

      

    }


}