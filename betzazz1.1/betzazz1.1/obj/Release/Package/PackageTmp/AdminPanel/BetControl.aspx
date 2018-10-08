<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminPanel.Master" AutoEventWireup="true" CodeBehind="BetControl.aspx.cs" Inherits="betzazz1._1.AdminPanel.BetControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <div class="container-fluid">
            
            <!-- Basic Table -->
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2>
                               <center> Bet Control</center>
                                
                            </h2>
                        </div>
                        <form runat="server">
                            <div class="pull-left" style="padding:20px;padding-left:25px">
                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="lblGetTrnsId" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text="Select multiple rows" Visible="true"></asp:Label><br>
                                <asp:DropDownList ID="ddlRowSelect" runat="server" Height="30px" Width="150px" ForeColor="Black" Font-Size="14px" OnSelectedIndexChanged="ddlRowSelect_SelectedIndexChanged1" AutoPostBack="true">                                     
                                     <asp:listitem>10</asp:listitem>
                                  <%--<asp:listitem>15</asp:listitem>--%>
                                     <asp:listitem>20</asp:listitem>
                                     <asp:listitem>30</asp:listitem>
                                     <asp:listitem>50</asp:listitem>
                                     <asp:listitem>100</asp:listitem>
                                    <asp:listitem>200</asp:listitem>
                                    <asp:listitem>300</asp:listitem>
                                    <asp:listitem>400</asp:listitem>
                                    <asp:listitem>500</asp:listitem>
                                </asp:DropDownList>
                            </div>
                            <div class="pull-right" style="padding:20px;padding-right:25px">
                                <asp:Button ID="btnAddBet" runat="server" CssClass="btn btn-info" Text="Add Bets" OnClick="btnAddBet_Click" BackColor="#ecc74d"  ForeColor="Black"/>
                                <asp:Button ID="btnMangBet" runat="server" CssClass="btn btn-info" Text="Manage Bets" OnClick="btnMangBet_Click" BackColor="#ecc74d" ForeColor="Black"/>
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" Visible="false" ToolTip="Search by Bet Types & Date"
                                 AutoPostBack="true" Height="33px" OnTextChanged="txtSearch_TextChanged"></asp:TextBox></div>

                            <div class="card-body">
                                 <!--GridView Start  from here-->
                        <div class="body table-responsive">
                            <asp:GridView ID="GridView1" runat="server" EmptyDataText="No Records Inserted!!"
                                AutoGenerateColumns="False" class="table table-bordered table-striped table-hover dataTable"
                                 Font-Bold="True" DataKeyNames="UserId" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" 
                                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"  OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound" 
                                  >

                                <Columns>
                                   
                                     <asp:TemplateField HeaderText="Select">
                                       <ItemTemplate> <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox></ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:CheckBox ID="CheckBox2" runat="server"></asp:CheckBox>
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                             

                                    <asp:TemplateField HeaderText ="User Name">
                                        <ItemTemplate >
                                             <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName")%>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="Login Id">
                                        <ItemTemplate >
                                     <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("UserNameRandomGen")%>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText ="Event Id">
                                        <ItemTemplate >
                                             <asp:Label ID="lblEventId" runat="server" Text='<%# Eval("EventId")%>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
   <asp:BoundField DataField="TransectionId" HeaderText="Transaction Id" ItemStyle-Width="50px"  />
                                     <asp:TemplateField HeaderText ="Match Date">
                                        <ItemTemplate >
                                             <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("MatchStartDate")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Match" HeaderText="Match" ItemStyle-Width="250px"  />
                                     <%-- <asp:TemplateField HeaderText ="Match">
                                        <ItemTemplate >
                                             <asp:Label ID="lblMatch" runat="server" Text='<%# Eval("Match")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText ="Transaction Date">
                                        <ItemTemplate >
                                             <asp:Label ID="lblCurentDateTime" runat="server" Text='<%# Eval("CurentDateTime")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="Innings">
                                        <ItemTemplate >
                                             <asp:Label ID="lblTeamName" runat="server" Text='<%# Eval("TeamName")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText ="Match Status">
                                        <ItemTemplate >
                                             <asp:Label ID="lblMatchStatus" runat="server" Text='<%# Eval("MatchStatus")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText ="Bet Type">
                                        <ItemTemplate >                                           
                                             <asp:Label ID="lblBetType" runat="server" Text='<%# Eval("BetType")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:BoundField DataField="OddSide" HeaderText="Selection" ItemStyle-Width="200px"  ItemStyle-Wrap="true" ControlStyle-Width="100px"/>

                                     <asp:TemplateField HeaderText ="Remove">
                                        <ItemTemplate >                                           
                                             <asp:Label ID="lblremove" runat="server" Text='<%# Eval("Remove")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText ="Block">
                                        <ItemTemplate >                                           
                                             <asp:Label ID="lblblock" runat="server" Text='<%# Eval("Block")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 

                                     <asp:BoundField DataField="Odd" HeaderText="Odds" ItemStyle-Width="40px" ControlStyle-Width="50px"/>
                                     <asp:boundfield datafield="amount" headertext="Stake" ItemStyle-Width="40px" ControlStyle-Width="50px"/>
                                     <asp:boundfield datafield="blncreturns" headertext="Total Return" ItemStyle-Width="40px" ControlStyle-Width="50px"/>
                                    <asp:boundfield datafield="lossprofit" headertext="Net Profit " ControlStyle-Width="50px"/>
                                     <asp:TemplateField HeaderText="Result" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result")%>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>                                           
                                            <asp:DropDownList ID="ddlResult" runat="server" SelectedValue='<%# Bind("Result")%>' Width="80px" OnSelectedIndexChanged="ddlResult_SelectedIndexChanged">
                                                <asp:ListItem>pending</asp:ListItem>
                                                <asp:ListItem>Profit</asp:ListItem>
                                                <asp:ListItem>Loss</asp:ListItem>
                                                <asp:ListItem>No Loss/Profit</asp:ListItem>
                                                <asp:ListItem>Cancel</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                     <asp:templatefield headertext="Status">
                                        <itemtemplate>
                                             <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' Width="70px" ></asp:Label>
                                        </itemtemplate>
                                        <edititemtemplate>
                                            <asp:dropdownlist id="ddlStatus" runat="server" SelectedValue='<%# Bind("Status")%>' Width="70px">
                                                <asp:ListItem>Open</asp:ListItem>
                                                <asp:ListItem>Closed</asp:ListItem>
                                                
                                            </asp:dropdownlist>
                                        </edititemtemplate>
                                    </asp:templatefield>

                                  
                                    <asp:CommandField HeaderText="Action"  ButtonType="Link"  ShowDeleteButton="true" ShowSelectButton="false" ShowEditButton="true" ShowCancelButton="true"/>
                                  
                                    </Columns>

                                <EditRowStyle Width="100%"  />
                        <HeaderStyle BackColor="#ecc74d" ForeColor="Black" Width="100%"/>
                                <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="5" />
                        <RowStyle  Width="100%" Height="50px" /> 
                                <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="4" Mode="NumericFirstLast" />
                            </asp:GridView>                         
                        </div>
                                <br><br>
                                <asp:Label ID="Label3" runat="server" Text="Result" Visible="false">                                                                 </asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" Visible="false">
                                  <asp:ListItem>pending</asp:ListItem>
                                                <asp:ListItem>Profit</asp:ListItem>
                                                <asp:ListItem>Loss</asp:ListItem>
                                                <asp:ListItem>No Loss/Profit</asp:ListItem>
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label4" runat="server" Text="Status" Visible="false"></asp:Label>
                                <asp:DropDownList ID="DropDownList2" runat="server" Visible="false">
                                     <asp:ListItem>Open</asp:ListItem>
                                                <asp:ListItem>Closed</asp:ListItem>
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnMulipleUpdates" runat="server" Text="Update" Visible="false" CssClass="btn btn-success" Height="30px" BackColor="#ecc74d" ForeColor="Black"></asp:Button>
                                </div>
                            </form>
                    </div>
                </div>
            </div>
            <!-- #END# Basic Table -->
            
        </div>
    </section>
       
</asp:Content>
