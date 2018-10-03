using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace betzazz1._1.BusnessLogics
{
    public class Global
    {
      // Configuration of connection string.
         public static string connectionString=ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<string> FailedProductIdsList { get; set; }

        public string CricketIPGetJson { get; set; }

        public string FotballIPGetJson { get; set; }

        public string CricketPMGetJson { get; set; }

        public string FotballPMGetJson { get; set; }



        // Funtion for Get Inplay Event JSON String from Data Base.
        public void GetIPEventList()
        {
            try
            {
                // string GetJson = null;
               // Cricket Inplay Event List JSON String from Data Base
                using (SqlConnection concr=new SqlConnection(connectionString))
                { 
                   using (SqlCommand cmdcr = new SqlCommand("SP_cricketInplayEvntList", concr))
                   {
                      concr.Open();
                      cmdcr.Connection = concr;
                      cmdcr.CommandType = System.Data.CommandType.StoredProcedure;
                      using (var reader = cmdcr.ExecuteReader())
                      {
                        reader.Read();
                        CricketIPGetJson = reader.GetString(0).ToString();
                      }
                      concr.Close();
                   }
                }

                //  Football Inplay Eevnt List JSON String
                using (SqlConnection conft = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdft = new SqlCommand("SP_FootballInplayEvntList", conft))
                    {
                        conft.Open();
                        cmdft.Connection = conft;
                        cmdft.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdft.ExecuteReader())
                        {
                            reader.Read();
                            FotballIPGetJson = reader.GetString(0).ToString();
                        }
                        conft.Close();
                    }
                }
            }
            catch (Exception ew)
            {
                throw ew;
            }
        
        }

        // Funtion for Get Pre Match Event JSON String from Data Base.
        public void GetPMEventList()
        {
            try
            {
                // string GetJson = null;
                // Cricket Inplay Event List JSON String from Data Base
                using (SqlConnection concr = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdcr = new SqlCommand("SP_CricketPMEventList", concr))
                    {
                        concr.Open();
                        cmdcr.Connection = concr;
                        cmdcr.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdcr.ExecuteReader())
                        {
                            reader.Read();
                            CricketPMGetJson = reader.GetString(0).ToString();
                        }
                        concr.Close();
                    }
                }

                //  Football Inplay Eevnt List JSON String
                using (SqlConnection conft = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmdft = new SqlCommand("SP_FootballPMEventList", conft))
                    {
                        conft.Open();
                        cmdft.Connection = conft;
                        cmdft.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmdft.ExecuteReader())
                        {
                            reader.Read();
                            FotballPMGetJson = reader.GetString(0).ToString();
                        }
                        conft.Close();
                    }
                }
            }
            catch (Exception ew)
            {
                throw ew;
            }

        }

    }
}