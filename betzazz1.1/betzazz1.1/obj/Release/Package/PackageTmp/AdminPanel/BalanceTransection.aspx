<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="BalanceTransection.aspx.cs" Inherits="betzazz1._1.AdminPanel.BalanceTransection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content">
        <div class="container-fluid">
            
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2>
                               <center> Balance Transection</center>
                                
                            </h2>
                        </div>
                        <form runat="server">
                            <div class="pull-right" style="padding:20px;padding-right:25px">
                                <asp:Button ID="btnAddBlne" runat="server" CssClass="btn btn-info" Text="Add Bets"  BackColor="#ecc74d" Visible="false" ForeColor="Black"/>
                                <asp:Button ID="btnManageBlneHistry" runat="server" CssClass="btn btn-info" Text="Manage transection"  BackColor="#ecc74d" ForeColor="Black" 
                                    OnClick="btnManageBlneHistry_Click1" />
                            <asp:TextBox ID="txtSearch" runat="server" placeHolder="Search" ToolTip="Search by User Name,UpdatedDate,Adjustment,Credit,Debit & Date" Visible="false"  Height="33px" AutoPostBack="true" 
                                OnTextChanged="txtSearch_TextChanged"></asp:TextBox></div>
                            <div class="card-body">
                                 <!--GridView Start  from here-->
                        <div class="body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" 
                                AutoGenerateColumns="False" class="table table-bordered table-striped table-hover dataTable"
                                 Font-Bold="True" EmptyDataText="No Records" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowDataBound="GridView1_RowDataBound"  
                               
                                  >
                                <Columns>
                                  
                                    <asp:BoundField  DataField="BalanceId" HeaderText="Balance Id" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name"/>
                                    <asp:BoundField DataField="Balance" HeaderText="Balance"/>
                                    <asp:BoundField DataField="Credit" HeaderText="Credit " />
                                    <asp:BoundField DataField="Debit" HeaderText="Debit"/>                                    
                                    <asp:BoundField DataField="Adjustment" HeaderText="Adjustment"/>
                                    <asp:BoundField DataField="Comments" HeaderText="Comments"/>
                                    <asp:BoundField DataField="ActionAmount" HeaderText="Transaction Amount"/>
                                    <asp:BoundField DataField="UpdatedDate" HeaderText="Transaction Date"/>
                             

                                    <asp:CommandField HeaderText="Action" ButtonType="Link"  ShowDeleteButton="true" ShowSelectButton="false" ShowEditButton="true" ShowCancelButton="true"/>
                                    </Columns>

                                <EditRowStyle Width="100%"  />
                        <HeaderStyle BackColor="#ecc74d" ForeColor="Black" Width="100%"/>
                               <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="4" Mode="NumericFirstLast" />
                        <RowStyle Width="100%"/> 
                                 
                            </asp:GridView>                         
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
