using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace betzazz1._1.AdminPanel
{
    public partial class AdminPanel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminname"] != null)
            {
                lblAdminName.Text = Convert.ToString(Session["adminname"]);
            }
            else
            {
                Response.Redirect("AdminSignIn.aspx");
            }
            //if (Session["Type"].ToString() != "adminname")
            //{
            //    Session.Abandon();
            //    Response.Redirect("AdminSignIn.aspx");

            //}
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("AdminSignIn.aspx");
        }

    }
}