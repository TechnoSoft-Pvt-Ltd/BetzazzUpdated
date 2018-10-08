<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="UserControl.aspx.cs" Inherits="betzazz1._1.AdminPanel.UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="content">
        <div class="container-fluid">
            
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2>
                               <center> User Control</center>
                                
                            </h2>
                        </div>
                        <form runat="server">
                            <div class="pull-right" style="padding:20px;padding-right:25px">
                                <asp:Button ID="btnmAddUser" runat="server" CssClass="btn btn-info" Text="Add User" OnClick="btnmAddUser_Click" BackColor="#ecc74d"  ForeColor="Black"/>
                                <asp:Button ID="btnManageUser" runat="server" CssClass="btn btn-info" Text="Manage User" OnClick="btnManageUser_Click" BackColor="#ecc74d" ForeColor="Black"/>
                            <asp:TextBox ID="txtSearch" runat="server" placeHolder="Search" Visible="false" ToolTip="Search by User Id Email & Date"  
                                 AutoPostBack="true" Height="33px" OnTextChanged="txtSearch_TextChanged"></asp:TextBox></div>
                            <div class="card-body">
                                 <!--GridView Start  from here-->
                        <div class="body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Records Inserted!!" 
                                AutoGenerateColumns="False" class="table table-bordered table-striped table-hover dataTable" 
                                 Font-Bold="True" DataKeyNames="UserId" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                 OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowUpdating="GridView1_RowUpdating" AllowSorting="True" OnRowDataBound="GridView1_RowDataBound" 
                               
                                  >
                                <Columns>
                                  <asp:BoundField  DataField="UserId" HeaderText="User Id" />
                                    <asp:BoundField  DataField="UserName" HeaderText="User Name" />
                                    <asp:BoundField DataField="UserNameRandomGen" HeaderText="Login Id"/>
                                    <asp:BoundField DataField="UserEmail" HeaderText="Email ID"/>
                                    <asp:BoundField DataField="AccountCuerrcy" HeaderText="Currency "/>
                                    <asp:BoundField DataField="CreatePass" HeaderText="Password "/>                                    
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date"/>

                                    <asp:CommandField HeaderText="Action" ButtonType="Link"  ShowDeleteButton="true" ShowSelectButton="false" ShowEditButton="true" ShowCancelButton="true"/>
                                    </Columns>

                                <EditRowStyle Width="100%"  />
                        <HeaderStyle BackColor="#ecc74d" ForeColor="Black" Width="100%"/>
                                <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="4" Mode="NumericFirstLast" />
                        <RowStyle Width="100%"/> 

                            </asp:GridView>                         
                        </div>
                                <!--GridView End from here-->
                                <div class="row">
                                    <div class="col-sm-12">
                                <div class="body table-responsive">
                                <div class="form-group">
                                    <table class="table-responsive">
                                        <tr>
                                   <td><asp:Label ID="lblUserId" runat="server" Text="User Name" Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                   <td><asp:TextBox ID="txtUserName" runat="server"   Visible="false"></asp:TextBox> &nbsp;&nbsp; </td>                           
                                   <td> <asp:Label ID="lblUserName" runat="server" Text="Login Id" Font-Bold="true" Visible="false" ></asp:Label>&nbsp;</td>
                                   <td> <asp:TextBox ID="txtLoginId" runat="server"  Visible="false" > </asp:TextBox>&nbsp;&nbsp;</td>
                                   <td> <asp:Label ID="lblMobile" runat="server" Text="Email Id" Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                   <td><asp:TextBox ID="txtEmailId" runat="server" Visible="false" ></asp:TextBox>&nbsp;&nbsp;</td></tr>
                                        <tr><td><br /></td></tr>
                                   <tr>
                                   <td> <asp:Label ID="lblblc" runat="server" Text="Currency " Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                   <td> <asp:DropDownList ID="ddlCurrenccy" runat="server" Visible="false" Width="182px" Height="27px" >
                                     <asp:ListItem>Select Currency</asp:ListItem>
                                           <asp:ListItem>INR</asp:ListItem>
                                           <asp:ListItem>USD</asp:ListItem>
                                           <asp:ListItem>EUR</asp:ListItem>
                                           <asp:ListItem>GBP</asp:ListItem>
                                           <asp:ListItem>AED</asp:ListItem>
                                           <asp:ListItem>CAD</asp:ListItem>
                                           <asp:ListItem>AUD</asp:ListItem>
                                        </asp:DropDownList> &nbsp;&nbsp;</td>
                                   <td> <asp:Label ID="Label5" runat="server" Text="Password"  Font-Bold="true" Visible="false"></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtPassword" runat="server"  AutoPostBack="True" Visible="false"  ></asp:TextBox>&nbsp;&nbsp;</td>
                                  <%--<td>  <asp:Label ID="Label6" runat="server" Text="Created Date" Font-Bold="true" Visible="false" ></asp:Label>&nbsp;</td>
                                  <td>  <asp:TextBox ID="txtCreateDate" runat="server" type="Date" Visible="false" 
                                  AutoPostBack="True"  Height="27px" Width="182px" >&nbsp;&nbsp;</td>--%>
                                       </tr> 
                                        <tr><td><br /></td></tr>
                                        <tr>
                                            <td></td><td></td>
                                            <td style="text-align:center">
                                                <asp:Button ID="btnAdd" runat="server" Class="btn btn-success" Width="80px" Text="Add" Visible="false" OnClick="btnAdd_Click"/>
                                           &nbsp;  &nbsp;  &nbsp;
                                                <asp:Button ID="btnReset" runat="server" class="btn btn-warning" Width="80px" Text="Reset" Visible="false" OnClick="btnReset_Click" />
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
