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
    public partial class BalanceControl : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter da;
        DataSet ds = new DataSet("table");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BlncControal();
            }
        }

        public void BlncControal()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SP_BalanceControal", con);
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
            BlncControal();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BlncControal();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BlncControal();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                SqlCommand cmd = new SqlCommand("Delete from UserBalance where BalanceId='" + id.ToString() + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            BlncControal();
        }

        protected void btnMangBalance_Click(object sender, EventArgs e)
        {
            txtSearch.Visible = true;
        }

        protected void btnAddBalance_Click(object sender, EventArgs e)
        {
            lblUserId.Visible = true;
            txtBalanceId.Visible = true;
            txtAmount.Visible = true;
            lblUserName.Visible = true;
            // lblMobile.Visible = true;
            //txtUpdateDate.Visible = true;
            lblblc.Visible = true;
            // txtCurrentBalance.Visible = true;
            Label5.Visible = true;
            txtComment.Visible = true;
            ddlManageAccount.Visible = true;
            txtBalance.Visible = true;
            Label6.Visible = true;
            // txtCurrentBalance.Visible = true;
            btnAdd.Visible = true;
            btnReset.Visible = true;
            lblUserName1.Visible = true;
            txtUserName.Visible = true;

        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //TextBox txtUserId = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
            //TextBox txtBalance = (TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0];
            //TextBox txtUpdateDate = (TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0];


            //int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            //con.Open();
            //SqlCommand cmd = new SqlCommand("Update UserBalance set UserId='"+txtUserId.Text+"', Balance='"+txtBalance.Text+ "',UpdateDate='"+ txtUpdateDate .Text+ "'  where BalanceId='" + id.ToString() + "' ", con);
            ////cmd.Parameters.AddWithValue("@UserName", UserName.Text);
            ////cmd.Parameters.AddWithValue("@UserNameRandomGen", UserNameRandomGen.Text);
            ////cmd.Parameters.AddWithValue("@UserEmail", UserEmail.Text); 
            ////cmd.Parameters.AddWithValue("@AccountCuerrcy", AccountCuerrcy.Text);
            ////cmd.Parameters.AddWithValue("@CreatePass", CreatePass.Text);
            ////cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate.Text);
            ////cmd.Parameters.AddWithValue("@UserID",UserId.ToString());                  
            //cmd.ExecuteNonQuery();
            //GridView1.EditIndex = -1;
            //con.Close();
            //BlncControal();

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.SelectedRow;
                lblGetUserId.Text = GridView1.SelectedRow.Cells[1].Text;
                txtBalanceId.Text = GridView1.SelectedRow.Cells[0].Text;
                txtUserName.Text = GridView1.SelectedRow.Cells[2].Text;
                txtBalance.Text = GridView1.SelectedRow.Cells[4].Text;
                // txtMobileNo.Text = GridView1.SelectedRow.Cells[1].Text;
                // txtCurrentBalance.Text = GridView1.SelectedRow.Cells[4].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {


                SqlCommand cmd = new SqlCommand("SP_BalanceRecordHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@BalanceId", txtBalanceId.Text);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);

                if (ddlManageAccount.SelectedValue.Equals("Credit"))
                {
                    cmd.Parameters.AddWithValue("@Credit", ddlManageAccount.Text);
                    cmd.Parameters.AddWithValue("@Adjustment", "");
                    cmd.Parameters.AddWithValue("@Debit", "");
                }
                if (ddlManageAccount.SelectedValue.Equals("Debit"))
                {
                    cmd.Parameters.AddWithValue("@Adjustment", "");
                    cmd.Parameters.AddWithValue("@Credit", "");
                    cmd.Parameters.AddWithValue("@Debit", ddlManageAccount.Text);
                }
                if (ddlManageAccount.SelectedValue.Equals("Adjustment"))
                {
                    cmd.Parameters.AddWithValue("@Adjustment", ddlManageAccount.Text);
                    cmd.Parameters.AddWithValue("@Credit", "");
                    cmd.Parameters.AddWithValue("@Debit", "");
                }

                cmd.Parameters.AddWithValue("@ActionAmount", txtAmount.Text);
                cmd.Parameters.AddWithValue("@Comments", txtComment.Text);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("Update UserBalance set  Balance='" + txtBalance.Text + "',UpdateDate='" + DateTime.Now + "' where BalanceId='" + txtBalanceId.Text + "' ", con);
                cmd1.ExecuteNonQuery();

                //For BalanceHistory 
                SqlCommand cmd2 = new SqlCommand("SP_InsertFrmBlncControl", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@BalanceId", txtBalanceId.Text);
                cmd2.Parameters.AddWithValue("@UserId", lblGetUserId.Text);
                cmd2.Parameters.AddWithValue("@DateTime", DateTime.Now);

                if (ddlManageAccount.SelectedValue.Equals("Credit"))
                {
                    cmd2.Parameters.AddWithValue("@DescriptionResion", ddlManageAccount.Text);
                    cmd2.Parameters.AddWithValue("@CrAmount", txtAmount.Text);
                    cmd2.Parameters.AddWithValue("@DrAmount", " ");
                }
                if (ddlManageAccount.SelectedValue.Equals("Debit"))
                {
                    cmd2.Parameters.AddWithValue("@DescriptionResion", ddlManageAccount.Text);
                    cmd2.Parameters.AddWithValue("@DrAmount", txtAmount.Text);
                    cmd2.Parameters.AddWithValue("@CrAmount", " ");
                }
                cmd2.Parameters.AddWithValue("@Details", txtComment.Text);
                cmd2.Parameters.AddWithValue("@TotalAmount", txtBalance.Text);
                cmd2.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Balance added Successfully!!');window.location='BalanceControl.aspx'</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            //    try
            //    {
            if (ddlManageAccount.SelectedValue.Equals("Credit"))
            {

                if (txtAmount.Text.Length > 0)
                {
                    double CurrntBlnce = Convert.ToDouble(txtBalance.Text);
                    double CrditAmount = Convert.ToDouble(txtAmount.Text);
                    CurrntBlnce += CrditAmount;
                    txtBalance.Text = Convert.ToString(CurrntBlnce);
                }
                else
                {
                    txtAmount.Text = "0";
                }

            }
            else if (ddlManageAccount.SelectedValue.Equals("Debit"))
            {
                if (txtAmount.Text.Length > 0)
                {
                    double CurrntBlnceD = Convert.ToDouble(txtBalance.Text);
                    double DebtAmount = Convert.ToDouble(txtAmount.Text);
                    CurrntBlnceD -= DebtAmount;
                    txtBalance.Text = Convert.ToString(CurrntBlnceD);
                }
                else
                {
                    txtAmount.Text = "0";
                }
            }
            else if (ddlManageAccount.SelectedValue.Equals("Adjustment"))
            {

                double adjustmentAmt = 0f;
                bool amt = double.TryParse(txtAmount.Text.ToString(), out adjustmentAmt);

                if (!amt)
                {
                    //write code for error message other than numeric value
                    return;
                }
                if (adjustmentAmt > 0)
                {
                    double CurrntBlnceAD = Convert.ToDouble(txtBalance.Text);
                    double CrditAmount = Convert.ToDouble(txtAmount.Text);
                    CurrntBlnceAD += CrditAmount;
                    txtBalance.Text = Convert.ToString(CurrntBlnceAD);
                }
                if (adjustmentAmt < 0)
                {
                    double CurrntBlnceAD1 = Convert.ToDouble(txtBalance.Text);
                    double DebtAmount = Convert.ToDouble(txtAmount.Text);
                    CurrntBlnceAD1 += DebtAmount;
                    txtBalance.Text = Convert.ToString(CurrntBlnceAD1);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("BalanceTransection.aspx");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            int a = 0;
            bool t = int.TryParse(txtSearch.Text, out a);
            if (!t)
            {
                a = 0;
            }
            SqlCommand cmd = new SqlCommand("SP_BALANCECONTRLSEARCH", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", txtSearch.Text);
            cmd.Parameters.AddWithValue("@UserNameRandomGen", txtSearch.Text);
            cmd.Parameters.AddWithValue("@UserId", a);
            cmd.Parameters.AddWithValue("@BalanceId", a);
            cmd.Parameters.AddWithValue("@UpdateDate", txtSearch.Text);
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
                (e.Row.Cells[6].Controls[0] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this record?');";
            }
        }
    }
}