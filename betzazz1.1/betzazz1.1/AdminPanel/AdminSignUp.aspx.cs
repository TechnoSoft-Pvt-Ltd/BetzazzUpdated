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
    public partial class AdminSignUp : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AdminSiginUp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdminName", txtUserName1.Text.Trim());
                cmd.Parameters.AddWithValue("@AdminEmail", txtEmail1.Text.Trim());
                cmd.Parameters.AddWithValue("@AdminPass", txtPassword1.Text.Trim());
                cmd.Parameters.AddWithValue("@ConfirmPass", txtConfirmPassword1.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", txtUserName1.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Registration Successfully');window.location='AdminSignIn.aspx'</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}