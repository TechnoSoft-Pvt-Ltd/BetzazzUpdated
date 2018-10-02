using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace betzazz1._1.AdminPanel
{
    public partial class AdminSignIn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { //do something }
            }
        }

        protected void btnAdminSigin_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select adminname,adminemail,adminpass from adminlogin where adminemail =@username and adminpass=@password", con);
                cmd.Parameters.AddWithValue("@username", txtUserName1.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (txtPassword1.Text.Trim() == dr["adminpass"].ToString())
                        {
                            Session["adminname"] = dr["adminname"].ToString();
                            Session["adminemail"] = dr["adminemail"].ToString();


                        }
                        else
                        {

                        }
                        Response.Redirect("UserControl.aspx");

                    }
                else
                {
                    Response.Write("<script>alert('username and password does not exist');window.location='AdminSignIn.aspx'</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}