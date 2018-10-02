<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddBets.aspx.cs" Inherits="betzazz1._1.AdminPanel.AddBets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="container-fluid">
            
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2 style="padding-top:20px">
                               <center>Add Bet</center>
                                
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
                                            
                                            <td> <asp:Label ID="Label11" runat="server" Text="Login Id" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                            <td> <asp:TextBox ID="txtLoginId" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtLoginId" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;</td>
                                            <td> <asp:Label ID="lblUserID" runat="server" Text="User Id" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                            <td> <asp:TextBox ID="txtUserId" runat="server"  ReadOnly="true" ></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtUserId" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;</td>
                                            <td> <asp:Label ID="lblEventId" runat="server" Text="Event Id" Font-Bold="true" ></asp:Label>&nbsp;</td>
                                            <td><asp:TextBox ID="txtEventId" runat="server"  ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtEventId" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;</td>
                                           
                                         </tr>
                                             <tr><td><br /></td></tr>

                                        <tr>
                                            <td> <asp:Label ID="Label2" runat="server" Text="Match Date"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                            <td>  <asp:TextBox ID="txtMatchSrtDate" runat="server"  type="Date" Height="27px" Width="182px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtMatchSrtDate" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                            </td>

                                            <td> <asp:Label ID="Label1" runat="server" Text="Match " Font-Bold="true"></asp:Label>&nbsp;</td>
                                            <td> <asp:TextBox ID="txtMatch" runat="server" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtMatch" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                            </td>
                                            <td> <asp:Label ID="lblblc" runat="server" Text="Innings" Font-Bold="true"></asp:Label>&nbsp;</td>
                                            <td> <asp:TextBox ID="txtTeamName" runat="server"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                ForeColor="Red" ControlToValidate="txtTeamName" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;  </td>
                                                                                
                                         </tr>
                                            <tr><td><br /></td></tr>
                                
                                         <tr>
                                             <td> <asp:Label ID="Label3" runat="server" Text="Match Status" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                             <td><asp:TextBox ID="txtMatchStatus" runat="server" Height="27px" Width="182px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtMatchStatus" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                 &nbsp;</td>
                                             <td><asp:Label ID="Label10" runat="server" Text="Bet Type"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                             <td> <asp:DropDownList ID="ddlBetType" runat="server"  Height="27px" Width="182px">
                                             <asp:listitem>To win the match</asp:listitem>
                                             <asp:listitem>To Win The Toss</asp:listitem>
                                             <asp:listitem>A Fifty To Be Scored In The Match</asp:listitem>
                                             <asp:listitem>A Hundred To Be Scored In The Match</asp:listitem>
                                             <asp:listitem>Runs To Be Scored In  6 Over</asp:listitem>
                                             <asp:listitem>Runs To Be Scored In  12 Over</asp:listitem>
                                             <asp:listitem>Runs To Be Scored In  20 Over</asp:listitem>
                                             <asp:listitem>Closed</asp:listitem>
                                             </asp:DropDownList>&nbsp;&nbsp;</td>

                                             <td> <asp:Label ID="lblSelection" runat="server" Text="Selection"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                             <td>  <asp:TextBox ID="txtSelection" runat="server"  Height="27px" Width="182px" ></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtSelection" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                 &nbsp;&nbsp;</td>
                                            &nbsp;&nbsp;
                                        </tr> 
                                        <tr><td><br /></td></tr>

                                         <tr>
                                             <td> <asp:Label ID="Label5" runat="server" Text="Odds"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                             <td>  <asp:TextBox ID="txtOdd" runat="server" ></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtOdd" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                             <td> <asp:Label ID="Label4" runat="server" Text="Stake " Font-Bold="true"></asp:Label>&nbsp;</td>
                                             <td> <asp:TextBox ID="txtAmount" runat="server" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtAmount" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                             </td>
                                             <td> <asp:Label ID="Label7" runat="server" Text="Total Return"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                             <td>  <asp:TextBox ID="txtBalncReturn" runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtBalncReturn" ValidationGroup="btnAddBet"></asp:RequiredFieldValidator>
                                                 &nbsp;&nbsp;</td>                                
                                                                            
                                        </tr> 
                                             <tr><td><br /></td></tr>
                                        <tr>
                                            <td> <asp:Label ID="Label6" runat="server" Text="Net Profit"  Font-Bold="true" ></asp:Label>&nbsp;</td>
                                             <td>  <asp:TextBox ID="txtLossProfit" runat="server"  Height="27px" Width="182px" Text="Open" ReadOnly="true"></asp:TextBox> 
                                                 
                                                 &nbsp;&nbsp;</td>  
                                             <td>  <asp:Label ID="Label8" runat="server" Text="Result" Font-Bold="true"  ></asp:Label>&nbsp;</td>
                                             <td> <asp:TextBox ID="txtResult" runat="server"  Height="27px" Width="182px" Text="pending" ReadOnly="true"></asp:TextBox> &nbsp;&nbsp;</td>
                                                 <%--<asp:DropDownList ID="ddlResult" runat="server" Height="27px" Width="182px">
                                             <asp:listitem>pending</asp:listitem>
                                             <asp:listitem>Loss</asp:listitem>
                                             <asp:listitem>Profit</asp:listitem>
                                             <asp:listitem>No Loss/Profit</asp:listitem>
                                             </asp:DropDownList>--%>
                                                
                                             <td> <asp:Label ID="Label9" runat="server" Text="Status" Font-Bold="true"></asp:Label>&nbsp;</td>
                                             <td><asp:TextBox ID="txtStatus" runat="server"  Height="27px" Width="182px" Text="Open" ReadOnly="true"></asp:TextBox>
                                                 <%--<asp:DropDownList ID="ddlStatus" runat="server"  Height="27px" Width="182px">
                                             <asp:listitem>Open</asp:listitem>
                                             <asp:listitem>Closed</asp:listitem>
                                             </asp:DropDownList>--%>

                                             </td>
                                         </tr> 
                                        
                                         <tr><td><br /></td></tr>
                                        
                                     
                                        <tr style="height:50px"><td></td></tr>

                                        <tr>
                                            <td></td><td></td>
                                            <td style="text-align:center;padding-right:20px;">
                                                <asp:Button ID="btnAdd" runat="server" Class="btn btn-success" ForeColor="Black" BackColor="#ecc74d" Width="100px" Text="Add Bet" OnClick="btnAdd_Click"  ValidationGroup="btnAddBet"/></td>
                                           &nbsp;  &nbsp;  &nbsp;
                                             <td>   <asp:Button ID="btnReset" runat="server" class="btn btn-warning" ForeColor="Black" BackColor="#ecc74d" Width="80px" Text="Reset" OnClick="btnReset_Click" />
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
