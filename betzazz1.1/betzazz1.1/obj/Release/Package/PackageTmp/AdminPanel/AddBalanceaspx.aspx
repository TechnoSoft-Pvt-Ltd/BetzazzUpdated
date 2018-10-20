<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddBalanceaspx.aspx.cs" Inherits="betzazz1._1.AdminPanel.AddBalanceaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content">
        <div class="container-fluid">
            
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2 style="padding-top:20px">
                               <center>Add Balance</center>
                                
                            </h2>
                        </div>
                        <form runat="server">
                            
                            <div class="card-body">
                                 <!--GridView Start  from here-->
                       
                                <!--GridView End from here-->
                                <div class="row" style="padding-top:40px;border:1px solid">
                                    <div class="col-sm-12" >
                                <div class="body table-responsive">
                                <div class="form-group">
                                    <table class="table-responsive">
                                        <tr>
                                   <%--<td><asp:Label ID="lblUserId" runat="server" Text="Transection Id" Font-Bold="true" ></asp:Label>&nbsp;</td>
                                   <td><asp:TextBox ID="txtUserName" runat="server"  ></asp:TextBox> &nbsp;&nbsp; </td>      --%>                     
                                   <td> <asp:Label ID="lblUserName" runat="server" Text="User Id" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>&nbsp;&nbsp;</td>
                                   <td> <asp:Label ID="lblMobile" runat="server" Text="Event Id" Font-Bold="true" ></asp:Label>&nbsp;</td>
                                   <td><asp:TextBox ID="txtEventId" runat="server" ></asp:TextBox>&nbsp;&nbsp;</td>
                                            <td> <asp:Label ID="lblblc" runat="server" Text="Team Name " Font-Bold="true"></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtTeamName" runat="server"  ></asp:TextBox></td>
                                        </tr>
                                        <tr><td><br /></td></tr>
                                   <tr>
                                   
                                   <td> <asp:Label ID="Label5" runat="server" Text="Odd"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtOdd" runat="server"   ></asp:TextBox>&nbsp;&nbsp;</td>
                                  <%--<td>  <asp:Label ID="Label6" runat="server" Text="Current Date" Font-Bold="true" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtCurrentDate" runat="server" Height="27px" type="Date" Width="182px" ></asp:TextBox>&nbsp;&nbsp;</td>--%>
                                       <td> <asp:Label ID="Label1" runat="server" Text="Match " Font-Bold="true"></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtMatch" runat="server"  ></asp:TextBox></td>
                                       <td> <asp:Label ID="Label2" runat="server" Text="Match Start Date"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtMatchSrtDate" runat="server"  type="Date" Height="27px" Width="182px"></asp:TextBox>&nbsp;&nbsp;</td>
                                       </tr> 
                                        <tr><td><br /></td></tr>

                                         <tr>
                                   
                                   
                                  <td>  <asp:Label ID="Label3" runat="server" Text="Match Status" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtMatchStatus" runat="server" Height="27px" Width="182px" ></asp:TextBox>&nbsp;&nbsp;</td>
                                             <td> <asp:Label ID="Label4" runat="server" Text="Amount " Font-Bold="true"></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtAmount" runat="server"  ></asp:TextBox></td>
                                              <td> <asp:Label ID="Label7" runat="server" Text="Balance Return"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtBalncReturn" runat="server"  ></asp:TextBox>&nbsp;&nbsp;</td>
                                       </tr> 
                                         <tr><td><br /></td></tr>
                                        <tr>
                                   
                                  
                                  <td>  <asp:Label ID="Label8" runat="server" Text="Result" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                  <td>  <asp:DropDownList ID="ddlResult" runat="server" Height="27px" Width="182px">
                                        <asp:listitem>Pending</asp:listitem>
                                        <asp:listitem>Loss</asp:listitem>
                                        <asp:listitem>Profit</asp:listitem>
                                        <asp:listitem>No Profit/Loss</asp:listitem>
                                        </asp:DropDownList>&nbsp;&nbsp;</td>
                                            <td> <asp:Label ID="Label9" runat="server" Text="Status" Font-Bold="true"></asp:Label>&nbsp;</td>
                                   <td> <asp:DropDownList ID="ddlStatus" runat="server"  Height="27px" Width="182px">
                                       <asp:listitem>Open</asp:listitem>
                                        <asp:listitem>Closed</asp:listitem>
                                        </asp:DropDownList></td>
                                             <td> <asp:Label ID="Label10" runat="server" Text="Bet Type"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtBetType" runat="server"  Height="27px" Width="182px"></asp:TextBox>&nbsp;&nbsp;</td>
                                       </tr> 
                                        
                                         <tr><td><br /></td></tr>
                                        <tr>
                                             <td> <asp:Label ID="Label6" runat="server" Text="Loss Profit"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtLossProfit" runat="server"  Height="27px" Width="182px"></asp:TextBox>&nbsp;&nbsp;</td>
                                        </tr>
                                        <tr><td></td></tr>
                                     
                                        <tr style="height:50px"><td></td></tr>

                                        <tr>
                                            <td></td><td></td>
                                            <td style="text-align:center;padding-right:20px;">
                                                <asp:Button ID="btnAdd" runat="server" Class="btn btn-success" ForeColor="Black" BackColor="#ecc74d" Width="100px" Text="Add Bet"  /></td>
                                           &nbsp;  &nbsp;  &nbsp;
                                             <td>   <asp:Button ID="btnReset" runat="server" class="btn btn-warning" ForeColor="Black" BackColor="#ecc74d" Width="80px" Text="Reset"  />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                  </div>
                                    </div>
                                    </div>
                                </div>
                            </form>
                    </div>
                </div>
            </div>
            <!-- #END# Basic Table -->
            
        </div>
    </section>
</asp:Content>
