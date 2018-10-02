<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSignUp.aspx.cs" Inherits="betzazz1._1.AdminPanel.AdminSignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" />
 <meta charset="UTF-8" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <title>Sign Up | Admin</title>
    <!-- Favicon-->
    <link rel="icon" href="../../../favicon.ico" type="image/x-icon" />

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css" />

    <!-- Bootstrap Core Css -->
    <link href="../../../plugins/bootstrap/css/bootstrap.css" rel="stylesheet" />

    <!-- Waves Effect Css -->
    <link href="../../../../plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../../../plugins/animate-css/animate.css" rel="stylesheet" />
<link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom Css -->
<link href="css/style.css" rel="stylesheet" />
    
<body class="signup-page" style="background-color:#009688">
    <div class="signup-box">
        <div class="logo">
            <a href="javascript:void(0);">Admin<b>Sign Up</b></a>&nbsp;
        </div>
        <div class="card">
            <div class="body">
                <form id="sign_up" runat="server">
                    <div class="msg">Register a new membership</div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">person</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox id="txtUserName1" type="text" class="form-control" name="namesurname" placeholder="User Name" required autofocus runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">email</i>
                        </span>
                        <div class="form-line">
                         <asp:TextBox id="txtEmail1" type="email" class="form-control" name="email" placeholder="Email Address" required runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">lock</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox id="txtPassword1" type="password" class="form-control" name="password" minlength="6" placeholder="Password" required runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">lock</i>
                        </span>
                        <div class="form-line">
                           
                            <asp:TextBox id="txtConfirmPassword1" type="password" class="form-control" name="confirm" minlength="6" placeholder="Confirm Password" required runat="server"   ></asp:TextBox>
                            
                        </div>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password dose not match!!" ControlToCompare="txtPassword1" ControlToValidate="txtConfirmPassword1" ForeColor="Red"></asp:CompareValidator>
                    </div>
                    <div class="form-group">
                        <input type="checkbox" name="terms" id="chbxterms" class="filled-in chk-col-pink" runat="server" />
                        <label for="terms">I read and agree to the <a href="javascript:void(0);">terms of usage</a>.</label>
                    </div>
                    <asp:Button ID="btnSignUp" runat="server"   class="btn btn-info" Text="Sign Up" OnClick="btnSignUp_Click" />
                         
                                        <div class="m-t-25 m-b--5 align-center">
                        <a href="AdminSignIn.aspx">You already have register ?</a>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Jquery Core Js -->
    <script src="../../../plugins/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="../../../plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="../../../plugins/node-waves/waves.js"></script>

    <!-- Validation Plugin Js -->
    <script src="../../../plugins/jquery-validation/jquery.validate.js"></script>

    <!-- Custom Js -->
    <script src="../../../js/admin.js"></script>
    <script src="../../../js/pages/examples/sign-up.js"></script>
</body>
</html>


