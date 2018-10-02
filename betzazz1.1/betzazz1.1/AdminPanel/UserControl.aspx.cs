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
    public partial class UserControl : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter da;
        DataSet ds = new DataSet("table");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserControal();
            }
            //if (Session["Type"].ToString() != "adminname")
            //{
            //    Session.Abandon();
            //    Response.Redirect("AdminSignIn.aspx");

            //}

        }

        public void UserControal()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SP_USERCONTROAL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            UserControal();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            UserControal();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            UserControal();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                SqlCommand cmd = new SqlCommand("Delete from ModalData where UserId='" + id.ToString() + "'", con);
                SqlCommand cmd1 = new SqlCommand("Delete from UserBalance where UserId='" + id.ToString() + "'", con);
                con.Open();
                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            UserControal();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //try
            //{
            //    GridViewRow row = GridView1.SelectedRow;

            //    txtUserId.Text = GridView1.DataKeys[row.RowIndex].Value.ToString();
            //    txtUserName.Text = GridView1.SelectedRow.Cells[0].Text;
            //    txtMobileNo.Text = GridView1.SelectedRow.Cells[1].Text;
            //    txtCurrentBalance.Text = GridView1.SelectedRow.Cells[4].Text;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }



        protected void btnManageUser_Click(object sender, EventArgs e)
        {
            txtSearch.Visible = true;
        }

        protected void btnmAddUser_Click(object sender, EventArgs e)
        {

            lblUserId.Visible = true;

            txtUserName.Visible = true;
            lblUserName.Visible = true;
            lblMobile.Visible = true;
            txtEmailId.Visible = true;
            lblblc.Visible = true;
            ddlCurrenccy.Visible = true;
            Label5.Visible = true;
            txtPassword.Visible = true;
            //txtCreateDate.Visible = true;
            //Label6.Visible = true;
            txtLoginId.Visible = true;
            btnAdd.Visible = true;
            btnReset.Visible = true;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtUserName = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
            TextBox txtUserNameRdmGen = (TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox txtEmail = (TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0];
            TextBox txtCurrency = (TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0];
            TextBox txtPas = (TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0];
            TextBox txtDate = (TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0];

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Update ModalData set UserName='" + txtUserName.Text + "',UserNameRandomGen='" + txtUserNameRdmGen.Text + "',UserEmail='" + txtEmail.Text + "', AccountCuerrcy='" + txtCurrency.Text + "',CreatePass='" + txtPas.Text + "',CreatedDate='" + txtDate.Text + "' where UserId='" + id.ToString() + "' ", con);
            //cmd.Parameters.AddWithValue("@UserName", UserName.Text);
            //cmd.Parameters.AddWithValue("@UserNameRandomGen", UserNameRandomGen.Text);
            //cmd.Parameters.AddWithValue("@UserEmail", UserEmail.Text); 
            //cmd.Parameters.AddWithValue("@AccountCuerrcy", AccountCuerrcy.Text);
            //cmd.Parameters.AddWithValue("@CreatePass", CreatePass.Text);
            //cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate.Text);
            //cmd.Parameters.AddWithValue("@UserID",UserId.ToString());                  
            cmd.ExecuteNonQuery();
            GridView1.EditIndex = -1;
            con.Close();
            UserControal();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("username", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("UserNameRandomGen", txtLoginId.Text.Trim());
                cmd.Parameters.AddWithValue("AccountCuerrcy", ddlCurrenccy.Text.Trim());
                cmd.Parameters.AddWithValue("useremail", txtEmailId.Text.Trim());
                cmd.Parameters.AddWithValue("CreatePass", txtPassword.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('User Added Successfully');window.location='UserHistory.aspx'</script>");
                clear_controls();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void clear_controls()
        {
            txtUserName.Focus();
            txtUserName.Text = string.Empty;
            txtLoginId.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            //ddlCurrenccy.Text = string.Empty;
            txtPassword.Text = string.Empty;
            //txtCreateDate.Focus();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear_controls();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand("select * from ModalData where UserNameRandomGen like '%" + txtSearch.Text + "%'or UserEmail like '%" + txtSearch.Text + "%'"
                                       + " or CreatedDate like '%" + txtSearch.Text + "%' or UserName like '%" + txtSearch.Text + "%'", con);
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds, "table");
            GridView1.DataSource = ds.Tables["table"];
            GridView1.DataBind();
            cmd.Dispose();
            con.Close();

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex != e.Row.RowIndex)
            {
                (e.Row.Cells[7].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this record?');";
            }
        }
    }
}