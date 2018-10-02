<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSignIn.aspx.cs" Inherits="betzazz1._1.AdminPanel.AdminSignIn" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Sign In | </title>
    <!-- Favicon-->
    <link rel="icon" href="../../../favicon.ico" type="image/x-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="../../../plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="../../plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../../../plugins/animate-css/animate.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom Css -->
    <link href="css/style.css" rel="stylesheet" />
    
    
</head>

<body class="login-page" style="background-color:#009688" >
    <div class="login-box">
        <div class="logo">
            <a href="javascript:void(0);"><b>Admin  Login</b></a>
            
        </div>
        <form runat="server" >
        <div class="card">
            <div class="body">
               
                    <div class="msg">Sign in to start your session</div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">person</i>
                        </span>
                        <div class="form-inline">
                            <asp:TextBox ID="txtUserName1" runat="server" class="form-control" name="username" runat="server" placeholder="Username" required autofocus></asp:TextBox>
                            
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">lock</i>
                        </span>
                        <div class="form-inline">
                            <asp:TextBox ID="txtPassword1" runat="server" class="form-control" name="password" placeholder="Password" type="password" required></asp:TextBox>
                            
                        </div>
                        </div>

                    <div class="row">
                        <div class="col-xs-8 p-t-5">
                            <input type="checkbox" name="rememberme" id="rememberme" class="filled-in chk-col-pink">
                            <label for="rememberme">Remember Me</label>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnAdminSigin" class="btn btn-info"  runat="server" Text="SIGN IN" OnClick="btnAdminSigin_Click" />
                            
                        </div>
                    </div>

                    <%-- <div class="row m-t-15 m-b--20">
                        <div class="col-xs-6">
                            <a href="AdminSignUp.aspx">Register Now!</a>
                        </div>
                       <div class="col-xs-6 align-right">
                            <asp:LinkButton ID="btnForgetPass" runat="server">Forgot Password?</asp:LinkButton>
                           
                        </div>
                    </div>--%>
              
            </div>
        </div>
            </form>
    </div>

    <!-- Jquery Core Js -->
    <script src=""></script>

    <!-- Bootstrap Core Js -->
    <script src="../../../plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="../../../plugins/node-waves/waves.js"></script>

    <!-- Validation Plugin Js -->
    <script src="../../../plugins/jquery-validation/jquery.validate.js"></script>

    <!-- Custom Js -->
    <script src="../../../js/admin.js"></script>
    <script src="../../../js/pages/examples/sign-in.js"></script>
</body>

</html>
