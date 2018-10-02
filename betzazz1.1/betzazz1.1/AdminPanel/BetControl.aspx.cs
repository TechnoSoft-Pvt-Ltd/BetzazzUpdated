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
    public partial class BetControl : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter da;
        DataSet ds = new DataSet("table");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BetsControal();
                if (ddlRowSelect.SelectedValue.Equals("10"))
                {
                    BetsControal();
                }
                else
                {
                    GridView1.PageSize = Convert.ToInt16(ddlRowSelect.SelectedValue);
                }

            }

        }
        public void BetsControal()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_BetControlsNew25Aprail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds.Tables[0];
                GridView1.PageSize = Convert.ToInt32(ddlRowSelect.SelectedValue);
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
            BetsControal();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BetsControal();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            BetsControal();

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string tempBook = GridView1.Rows[e.RowIndex].Cells[4].Text;
                int GetTransactionId = Convert.ToInt32(tempBook);
                SqlCommand cmd = new SqlCommand("Delete from PreMatchBet where TransectionId='" + GetTransactionId + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            BetsControal();
        }

        protected void btnMangBet_Click(object sender, EventArgs e)
        {
            txtSearch.Visible = true;
        }

        protected void btnAddBet_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBets.aspx");
            //lblUserId.Visible = true;
            //txtUserId.Visible = true;
            //txtUserName.Visible = true;
            //lblUserName.Visible = true;
            //lblMobile.Visible = true;
            //txtMobileNo.Visible = true;
            //lblblc.Visible = true;
            //txtCurrentBalance.Visible = true;
            //Label5.Visible = true;
            //txtCreditAmount.Visible = true;
            //txtDeposiAmount.Visible = true;
            //Label6.Visible = true;
            //txtCurrentBalance.Visible = true;
            //btnAdd.Visible = true;
            //btnReset.Visible = true;
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string GetResult = string.Empty;
            string GetBalance = string.Empty;
            //string GetBalanceId = string.Empty;


            int _selectedRowIndex = e.RowIndex;
            int _ameintyId = Convert.ToInt32(GridView1.DataKeys[_selectedRowIndex].Value);
            string updatedAmenity = e.NewValues[0].ToString();

            string Result = ((DropDownList)GridView1.Rows[_selectedRowIndex].FindControl("ddlResult")).SelectedItem.ToString();
            string Status = ((DropDownList)GridView1.Rows[_selectedRowIndex].FindControl("ddlStatus")).SelectedItem.ToString();

            TextBox txtTransectionId = (TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0];
            TextBox txtOdd = (TextBox)GridView1.Rows[e.RowIndex].Cells[14].Controls[0];
            TextBox txtStake = (TextBox)GridView1.Rows[e.RowIndex].Cells[15].Controls[0];
            TextBox txtBlnceReturn = (TextBox)GridView1.Rows[e.RowIndex].Cells[16].Controls[0];
            TextBox txtProfitLoss = (TextBox)GridView1.Rows[e.RowIndex].Cells[17].Controls[0];
            TextBox txtSelection = (TextBox)GridView1.Rows[e.RowIndex].Cells[11].Controls[0];
            TextBox txtMatch = (TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0];

            con.Open();
            SqlCommand cmdNew = new SqlCommand("select Result from PrematchBet where TransectionId='" + txtTransectionId.Text + "' ", con);
            //cmdNew.ExecuteNonQuery();
            SqlDataReader reader = cmdNew.ExecuteReader();
            while (reader.Read())
            {
                GetResult = reader[0].ToString();

            }

            con.Close();

            if (GetResult.Equals("pending"))
            {


                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                //  String GetBalance;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update PreMatchBet set Result='" + Result + "',Odd='" + txtOdd.Text + "',Status='" + Status + "' where UserId='" + id.ToString() + "' AND TransectionId='" + txtTransectionId.Text + "' ", con);
                //SqlCommand cmdBlnce = new SqlCommand("select Balance from UserBalance where UserId='"+id.ToString()+"'",con);
                //SqlDataAdapter da = new SqlDataAdapter(cmdBlnce);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //GetBalance = da.ToString();
                //Case 1
                string ProfitLoss = txtProfitLoss.Text;
                double profitLos = 0f;
                double Stake = Convert.ToDouble(txtStake.Text);
                double BalanceReturn = Convert.ToDouble(txtBlnceReturn.Text);
                // double GetBlne = Convert.ToDouble(GetBalance);

                if (Status.Equals("Closed") && Result.Equals("Profit"))
                {

                    profitLos = BalanceReturn - Stake;

                    SqlCommand cmdcase1 = new SqlCommand("Update UserBalance set Balance= Balance +'" + BalanceReturn + "',UpdateDate='" + DateTime.Now + "' where UserId='" + id.ToString() + "'", con);
                    SqlCommand cmdProfitLossUpdate = new SqlCommand("Update PreMatchBet set LossProfit='" + profitLos + "',Odd='" + txtOdd.Text + "',Amount='" + txtStake.Text + "' where UserId='" + id.ToString() + "' AND TransectionId='" + txtTransectionId.Text + "'", con);
                    cmdcase1.ExecuteNonQuery();
                    cmdProfitLossUpdate.ExecuteNonQuery();

                }
                //Case 2
                else if (Status.Equals("Closed") && Result.Equals("Loss"))
                {
                    profitLos = Convert.ToDouble(profitLos) + Stake;
                    SqlCommand cmdPftLoss = new SqlCommand("Update PreMatchBet set LossProfit='" + profitLos + "',Odd='" + txtOdd.Text + "',Amount='" + txtStake.Text + "' where UserId='" + id.ToString() + "' AND TransectionId='" + txtTransectionId.Text + "'", con);
                    cmdPftLoss.ExecuteNonQuery();

                }
                // Case 3
                else if (Status.Equals("Closed") && Result.Equals("No Loss/Profit"))
                {

                    int i = 0;
                    SqlCommand cmdcase3 = new SqlCommand("Update UserBalance set Balance= Balance + '" + Stake + "',UpdateDate='" + DateTime.Now + "' where UserId='" + id.ToString() + "'", con);
                    SqlCommand cmdPftLos2 = new SqlCommand("Update PreMatchBet set LossProfit='" + i + "',Odd='" + txtOdd.Text + "',Amount='" + txtStake.Text + "' where UserId='" + id.ToString() + "' AND TransectionId='" + txtTransectionId.Text + "' ", con);
                    cmdcase3.ExecuteNonQuery();
                    cmdPftLos2.ExecuteNonQuery();

                }
                else if (Status.Equals("Closed") && Result.Equals("Cancel"))
                {

                    int i = 0;
                    SqlCommand cmdcase3 = new SqlCommand("Update UserBalance set Balance= Balance + '" + Stake + "',UpdateDate='" + DateTime.Now + "' where UserId='" + id.ToString() + "'", con);
                    SqlCommand cmdPftLos2 = new SqlCommand("Update PreMatchBet set LossProfit='" + i + "',Odd='" + txtOdd.Text + "',Amount='" + txtStake.Text + "' where UserId='" + id.ToString() + "' AND TransectionId='" + txtTransectionId.Text + "' ", con);
                    cmdcase3.ExecuteNonQuery();
                    cmdPftLos2.ExecuteNonQuery();

                }
                else
                {

                }
                cmd.ExecuteNonQuery();
                GridView1.EditIndex = -1;
                con.Close();
                //Get Updared Balance
                con.Open();
                SqlCommand cmdGetBlnce = new SqlCommand("Select Balance from UserBalance where UserId='" + _ameintyId + "'", con);
                SqlDataReader readerblnce = cmdGetBlnce.ExecuteReader();
                while (readerblnce.Read())
                {

                    GetBalance = readerblnce[0].ToString();

                }
                con.Close();



                //For BalanceHistory

                con.Open();
                SqlCommand cmd3 = new SqlCommand("SP_InsertFrmBlncControlBet", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@DateTime", DateTime.Now);
                cmd3.Parameters.AddWithValue("@UserId", _ameintyId);

                cmd3.Parameters.AddWithValue("@Details", txtMatch.Text);
                if (Result.Equals("Profit"))
                {
                    cmd3.Parameters.AddWithValue("@CrAmount", profitLos);
                    cmd3.Parameters.AddWithValue("@DescriptionResion", "TX" + txtTransectionId.Text);
                    cmd3.Parameters.AddWithValue("@DrAmount", "");
                }
                if (Result.Equals("Loss"))
                {
                    cmd3.Parameters.AddWithValue("@CrAmount", "");
                    cmd3.Parameters.AddWithValue("@DescriptionResion", "TX" + txtTransectionId.Text);
                    cmd3.Parameters.AddWithValue("@DrAmount", profitLos);
                }
                if (Result.Equals("No Loss/Profit"))
                {
                    cmd3.Parameters.AddWithValue("@CrAmount", "");
                    cmd3.Parameters.AddWithValue("@DescriptionResion", "TX" + txtTransectionId.Text);
                    cmd3.Parameters.AddWithValue("@DrAmount", "");
                }
                if (Result.Equals("Cancel"))
                {
                    cmd3.Parameters.AddWithValue("@CrAmount", "");
                    cmd3.Parameters.AddWithValue("@DescriptionResion", "TX" + txtTransectionId.Text);
                    cmd3.Parameters.AddWithValue("@DrAmount", "");
                }
                cmd3.Parameters.AddWithValue("@TotalAmount", GetBalance);
                //cmd3.Parameters.AddWithValue("@BalanceId", GetBalanceId);
                cmd3.ExecuteNonQuery();
                con.Close();


            }
            else
            {
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update PreMatchBet set Odd='" + txtOdd.Text + "',Amount='" + txtStake.Text + "',LossProfit='" + txtProfitLoss.Text + "'," +
                "BlncReturns='" + txtBlnceReturn.Text + "', Result='" + Result + "',Status='" + Status + "',OddSide='" + txtSelection.Text + "'  " +
                " where UserId='" + id.ToString() + "' AND TransectionId='" + txtTransectionId.Text + "' ", con);
                cmd2.ExecuteNonQuery();
                GridView1.EditIndex = -1;
                con.Close();
            }
            BetsControal();




        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.SelectedRow;

                //Session["TransectionId"] = GridView1.SelectedRow.Cells[0].Text;
                //Session["EventId"] = GridView1.SelectedRow.Cells[1].Text;
                //Session["TeamName"] = GridView1.SelectedRow.Cells[2].Text;
                //Session["Odd"] = GridView1.SelectedRow.Cells[3].Text;
                //Session["Match"] = GridView1.SelectedRow.Cells[5].Text;
                //Session["MatchStartDate"] = GridView1.SelectedRow.Cells[6].Text;
                //Session["MatchStatus"] = GridView1.SelectedRow.Cells[7].Text;
                //Session["Amount"] = GridView1.SelectedRow.Cells[8].Text;
                //Session["BlncReturns"] = GridView1.SelectedRow.Cells[9].Text;
                //Session["LossProfit"] = GridView1.SelectedRow.Cells[10].Text;
                //Session["Result"] = GridView1.SelectedRow.Cells[11].Text;
                //Session["Status"] = GridView1.SelectedRow.Cells[12].Text;
                //Session["BetType"] = GridView1.SelectedRow.Cells[13].Text;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv["Result"].ToString().Equals("Profit"))
                {
                    e.Row.Attributes.CssStyle.Value = "background-color: #b4f7bf; color: Black";
                }
                if (drv["Result"].ToString().Equals("Loss"))
                {
                    e.Row.Attributes.CssStyle.Value = "background-color: #fc8f97; color: Black";
                }

            }
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {


                con.Open();
                DropDownList ddlprod = (DropDownList)e.Row.FindControl("ddlResult");
                Label lblstatus = (Label)e.Row.FindControl("lblStatus");
                DropDownList ddlprod1 = (DropDownList)e.Row.FindControl("ddlStatus");
                SqlCommand cmd = new SqlCommand("select Result,Status from PreMatchBet where UserId='TransectionId'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                //ddlprod.DataSource = ds;
                //ddlprod.DataTextField = "Result";
                //ddlprod.DataValueField = "Result";
                //ddlprod.DataBind();
                //ddlprod.Items.Insert(0, new ListItem("Pending", "0"));
                //ddlprod.Items.Insert(1, new ListItem("Loss", "1"));
                //ddlprod.Items.Insert(2, new ListItem("Profit", "2"));
                //ddlprod.Items.Insert(3, new ListItem("No Profit/Loss", "3"));
                //ddlprod1.Items.Insert(0, new ListItem("Open", "0"));
                //ddlprod1.Items.Insert(1, new ListItem("Closed", "1"));   

            }

            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex != e.Row.RowIndex)
            {
                (e.Row.Cells[20].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this record?')";
            }




        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}
            //cmd = new SqlCommand(" SELECT PMB.TransectionId,PMB.UserId,MDLDT.UserNameRandomGen,MDLDT.UserName,PMB.EventId,PMB.TeamName,PMB.Odd,PMB.CurentDateTime,"+
            //        "PMB.Match, PMB.MatchStartDate, PMB.MatchStatus, PMB.Amount, PMB.BlncReturns, PMB.LossProfit,"+
            //        "PMB.Result, PMB.[Status], PMB.BetType, PMB.OddSide from PrematchBet as PMB"+
            //        "INNER JOIN ModalData as MDLDT on PMB.UserId = MDLDT.UserId"+
            //        "where  MDLDT.UserId LIKE '%" + txtSearch.Text + "%' or UserName LIKE '%" + txtSearch.Text + "%' or UserNameRandomGen LIKE '%" + txtSearch.Text + "%' or" +
            //       " TransectionId LIKE '%" + txtSearch.Text + "%' or CurentDateTime LIKE '%" + txtSearch.Text + "%' or " +
            //        "Match LIKE '%" + txtSearch.Text + "%' or Result LIKE '%" + txtSearch.Text + "%' or [Status] LIKE '%" + txtSearch.Text + "%' or" +
            //       " BetType LIKE '%" + txtSearch.Text + "%' or OddSide LIKE '%" + txtSearch.Text + "%' ", con);
            //da = new SqlDataAdapter(cmd);
            //ds.Clear();
            //da.Fill(ds, "table");
            //GridView1.DataSource = ds.Tables["table"];
            //GridView1.DataBind();
            //cmd.Dispose();
            //con.Close();
            // try
            //{
            con.Open();
            int a = 0;
            bool t = int.TryParse(txtSearch.Text, out a);
            if (!t)
            {
                a = 0;
            }
            SqlCommand cmd = new SqlCommand("SP_BETCONTROLSEARCH", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", txtSearch.Text);
            cmd.Parameters.AddWithValue("@UserNameRandomGen", txtSearch.Text);
            cmd.Parameters.AddWithValue("@UserId", a);
            cmd.Parameters.AddWithValue("@TransectionId", a);
            cmd.Parameters.AddWithValue("@CurentDateTime", txtSearch.Text);
            cmd.Parameters.AddWithValue("@Match", txtSearch.Text);
            cmd.Parameters.AddWithValue("@MatchStartDate", txtSearch.Text);
            cmd.Parameters.AddWithValue("@Result", txtSearch.Text);
            cmd.Parameters.AddWithValue("@Status", txtSearch.Text);
            cmd.Parameters.AddWithValue("@BetType", txtSearch.Text);
            cmd.Parameters.AddWithValue("@OddSide", txtSearch.Text);
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds, "table");
            GridView1.DataSource = ds.Tables["table"];
            GridView1.DataBind();
            cmd.Dispose();
            con.Close();
            //}
            // catch (Exception ex)
            // {
            //   throw ex;
            //}


        }

        protected void ddlResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlReult = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddlReult.Parent.Parent;
            //int idx = row.RowIndex;
        }



        protected void ddlRowSelect_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {

                Label1.Text = ddlRowSelect.SelectedValue;
                int val = Convert.ToInt32(Label1.Text);

                con.Open();
                SqlCommand rowslctcmd15 = new SqlCommand("sp_selectmultiplerow", con);
                rowslctcmd15.CommandType = CommandType.StoredProcedure;
                rowslctcmd15.Parameters.AddWithValue("@sectvalue", val);
                SqlDataAdapter da = new SqlDataAdapter(rowslctcmd15);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds.Tables[0];
                //gridview1.datasource = ds.tables[1];
                GridView1.PageSize = Convert.ToInt32(ddlRowSelect.SelectedValue);
                GridView1.DataBind();
                if (ddlRowSelect.SelectedValue.Equals("10"))
                {
                    BetsControal();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}