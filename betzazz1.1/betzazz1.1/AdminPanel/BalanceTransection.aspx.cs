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
    public partial class BalanceTransection : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter da;
        DataSet ds = new DataSet("table");
        protected void Page_Load(object sender, EventArgs e)
        {
            GetBalnceRecords();
        }
        public void GetBalnceRecords()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SP_BalanceStatment", con);
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
        protected void btnManageBlneHistry_Click1(object sender, EventArgs e)
        {
            txtSearch.Visible = true;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetBalnceRecords();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand("select * from BlnceRecord where ActionId like '%" + txtSearch.Text + "%' or UpdatedDate like '%" + txtSearch.Text + "%' or UserName like '%" + txtSearch.Text + "%' "
                + " or Adjustment like '%" + txtSearch.Text + "%' or Debit like '%" + txtSearch.Text + "%' or Credit like '%" + txtSearch.Text + "%' ", con);
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
                (e.Row.Cells[9].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this record?');";
                if (e.Row.Cells[3].Text == "Credit")
                {
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Green;

                }
                if (e.Row.Cells[4].Text == "Debit")
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[5].Text == "Adjustment")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.IndianRed;
                }

            }
        }
    }
}