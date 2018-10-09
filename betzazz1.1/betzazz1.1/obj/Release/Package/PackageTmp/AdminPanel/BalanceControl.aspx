<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="BalanceControl.aspx.cs" Inherits="betzazz1._1.AdminPanel.BalanceControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content">
        <div class="container-fluid">
            
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2>
                               <center> Balance Control</center>
                                <asp:Label ID="lblGetUserId" runat="server" Text="Label" Visible="false"></asp:Label>
                            </h2>
                        </div>
                        <form runat="server">
                            <div class="pull-right" style="padding:20px;padding-right:25px">
                                  <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Text="Balance Transection"  BackColor="#ecc74d"  ForeColor="Black" OnClick="Button1_Click"/>
                                 <%-- <asp:Button ID="btnAddnewalance" runat="server" CssClass="btn btn-info" Text="Add Balance"  BackColor="#ecc74d"  ForeColor="Black"/>--%>
                                  <asp:Button ID="btnAddBalance" runat="server" CssClass="btn btn-info" Text="Update Balance" OnClick="btnAddBalance_Click" BackColor="#ecc74d"  ForeColor="Black"/>
                                  <asp:Button ID="btnMangBalance" runat="server" CssClass="btn btn-info" Text="Manage Balance" OnClick="btnMangBalance_Click" BackColor="#ecc74d" ForeColor="Black"/>
                                  <asp:TextBox ID="txtSearch" runat="server" placeHolder="Search" Visible="false" ToolTip="Search by User Name & Date"
                                  AutoPostBack="true" Height="33px" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                            </div>
                            <div class="card-body">
                                 <!--GridView Start  from here-->
                        <div class="body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Records Inserted!!" 
                                AutoGenerateColumns="False" class="table table-bordered table-striped table-hover dataTable"
                                 Font-Bold="True" DataKeyNames="BalanceId" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" 
                                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"  OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" 
                                  >
                                <Columns>
                                      
                                    <asp:BoundField  DataField="BalanceId" HeaderText="Balance Id" />
                                    <asp:BoundField DataField="UserId" HeaderText="User Id"/> 
                                    <asp:BoundField DataField="UserName" HeaderText="User Name"/> 
                                    <asp:BoundField DataField="UserNameRandomGen" HeaderText="Login Name"/>                                    
                                    <asp:BoundField DataField="Balance" HeaderText="Balance"/>
                                    <asp:BoundField DataField="UpdateDate" HeaderText="Update Date"/>


                                   <%-- <asp:TemplateField HeaderText ="Curent Date Time">
                                        <ItemTemplate >
                                             <asp:Label ID="lblBalanceId" runat="server" Text='<%# Eval("BalanceId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:CommandField HeaderText="Action" ButtonType="Link"  ShowDeleteButton="true" ShowSelectButton="true" ShowEditButton="false" ShowCancelButton="false"/>
                                    </Columns>

                                <EditRowStyle Width="100%"  />
                        <HeaderStyle BackColor="#ecc74d" ForeColor="Black" Width="100%"/>
                                <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="5" Mode="NumericFirstLast" />
                        <RowStyle Width="100%"/> 
                                 
                            </asp:GridView>                         
                        </div>
                                <!--GridView End from here-->
                                <div class="row">
                                    <div class="col-sm-12">
                                <div class="body table-responsive">
                                <div class="form-group">
                                    <table class="table-responsive">
                                        <tr><td>
                                     
                                            </td></tr>
                                         
                                        <tr><td></td></tr>
                                        <tr>
                                   <td><asp:Label ID="lblUserId" runat="server" Text="Balance Id" Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                   <td><asp:TextBox ID="txtBalanceId" runat="server"  ReadOnly="True"  Visible="false"></asp:TextBox> &nbsp;&nbsp; </td> 
                                            <td> <asp:Label ID="lblUserName1" runat="server" Text="User Name" Font-Bold="true" Visible="false" ></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtUserName" runat="server" ReadOnly="True" Visible="false"> </asp:TextBox>&nbsp;&nbsp;</td>                          
                                   <td> <asp:Label ID="lblUserName" runat="server" Text="Balance" Font-Bold="true" Visible="false" ></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtBalance" runat="server" ReadOnly="True" Visible="false"> </asp:TextBox>&nbsp;&nbsp;</td>
                                                                                   
                                   &nbsp;&nbsp;</tr>
                                        <tr><td><br /></td></tr>
                                   <tr>
                                      <td> <asp:Label ID="Label5" runat="server" Text="Action"  Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                      <td> <asp:DropDownList ID="ddlManageAccount" runat="server" Height="27px" width="182px" Visible="false">
                                            <asp:ListItem>Select Action</asp:ListItem>
                                           <asp:ListItem>Credit</asp:ListItem>
                                           <asp:ListItem>Debit</asp:ListItem>                              
                                           <asp:ListItem>Adjustment</asp:ListItem>

                                </asp:DropDownList>&nbsp;&nbsp;</td>
                                   <td> <asp:Label ID="lblblc" runat="server" Text="Amount" Font-Bold="true" Visible="false" ></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtAmount" runat="server"  required  Visible="false" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"> </asp:TextBox>&nbsp;&nbsp;</td>
                                   
                                 
                                  <td>  <asp:Label ID="Label6" runat="server" Text="Comment" Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtComment" runat="server" Visible="false" 
                                   ></asp:TextBox>&nbsp;&nbsp;</td>
                                       </tr> 
                                        <tr><td><br /></td></tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Class="btn btn-success" Text="Add" Visible="false" Width="80px" BackColor="#ecc74d" ForeColor="Black" OnClick="btnAdd_Click"/>
                                           &nbsp;  &nbsp;  &nbsp;
                                                <asp:Button ID="btnReset" runat="server" class="btn btn-warning" Width="100px" Text="Reset" Visible="false"  BackColor="#ecc74d" ForeColor="Black" />
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
