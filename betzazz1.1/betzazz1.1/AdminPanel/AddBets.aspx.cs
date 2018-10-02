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
    public partial class AddBets : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {


            if (txtAmount.Text.Length > 0)
            {
                double GetOdds = Convert.ToDouble(txtOdd.Text);
                double GetStake = Convert.ToDouble(txtAmount.Text);
                double GetTotalReturn = GetOdds * GetStake;
                txtBalncReturn.Text = Convert.ToString(GetTotalReturn);
            }
            else
            {
                txtAmount.Text = "0";
                txtBalncReturn.Text = "0";
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string GetBalance = string.Empty;
            //try
            //{

            con.Open();
            SqlCommand cmdGetBlnce = new SqlCommand("Select Balance from UserBalance where UserId='" + txtUserId.Text + "'", con);
            SqlDataReader readerblnce = cmdGetBlnce.ExecuteReader();
            while (readerblnce.Read())
            {

                GetBalance = readerblnce[0].ToString();


            }
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_BetInsert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", txtUserId.Text.Trim());
            cmd.Parameters.AddWithValue("@EventId", txtEventId.Text.Trim());
            cmd.Parameters.AddWithValue("@TeamName", txtTeamName.Text.Trim());
            cmd.Parameters.AddWithValue("@Odd", txtOdd.Text.Trim());
            cmd.Parameters.AddWithValue("@Match", txtMatch.Text.Trim());
            cmd.Parameters.AddWithValue("@MatchStartDate", txtMatchSrtDate.Text.Trim());
            cmd.Parameters.AddWithValue("@MatchStatus", txtMatchStatus.Text.Trim());
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
            cmd.Parameters.AddWithValue("@BlncReturns", txtBalncReturn.Text.Trim());
            cmd.Parameters.AddWithValue("@Result", txtResult.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", txtStatus.Text.Trim());
            cmd.Parameters.AddWithValue("@BetType", ddlBetType.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@LossProfit", txtLossProfit.Text.Trim());
            cmd.Parameters.AddWithValue("@OddSide", txtSelection.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();

            double NewGetBalance = Convert.ToDouble(GetBalance.ToString());
            double Balance = Convert.ToDouble(txtAmount.Text);
            double NetBalance = NewGetBalance - Balance;

            con.Open();
            SqlCommand cmdcase1 = new SqlCommand("Update UserBalance set Balance='" + NetBalance + "',UpdateDate='" + DateTime.Now + "' where UserId='" + txtUserId.Text + "'", con);
            cmdcase1.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Bet Place Successfully!!');window.location='AddBets.aspx'</script>");
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtLoginId.Text.Length > 0)
            {
                string GetLoginId;

                con.Open();
                SqlCommand cmdNew = new SqlCommand("select UserId from ModalData where UserNameRandomGen='" + txtLoginId.Text + "'", con);
                SqlDataReader reader = cmdNew.ExecuteReader();
                while (reader.Read())
                {
                    GetLoginId = reader[0].ToString();
                    txtUserId.Text = GetLoginId;
                }
                con.Close();
            }
            else
            {
                txtUserId.Text = "";
            }


        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtLoginId.Text = string.Empty;
            txtUserId.Text = string.Empty;
            txtEventId.Text = string.Empty;
            txtMatchSrtDate.Text = string.Empty;
            txtMatch.Text = string.Empty;
            txtTeamName.Text = string.Empty;
            txtMatchStatus.Text = string.Empty;
            txtSelection.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtBalncReturn.Text = string.Empty;
            txtOdd.Text = string.Empty;
        }
    }
}