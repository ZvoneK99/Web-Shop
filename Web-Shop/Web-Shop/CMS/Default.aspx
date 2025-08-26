<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Web_Shop._Default1" %>

<!doctype html>
<%=Web_Shop.CMS.Uvjeti() %>
<html class="no-js" lang="">
<!--<![endif]-->
<head>
    <title>AVE CMS - AVE Content Management System</title>
    <meta name="description" content="AVE CMS - AVE Content Management System">
    <%=Web_Shop.CMS.ZajednickeMete() %>
    <!-- <link rel="stylesheet" href="assets/css/bootstrap-select.less"> -->
    <!-- <script type="text/javascript" src="https://cdn.jsdelivr.net/html5shiv/3.7.3/html5shiv.min.js"></script> -->

</head>
<body class="bg-dark">
    <div class="sufee-login d-flex align-content-center flex-wrap">
        <div class="container">
            <div class="login-content">
                <div class="login-logo">
                    <%--<a href="index.html">--%>
                        <img class="align-content" src="/CMS/images/logo.png" alt="">
                    <%--</a>--%>
                </div>
                <div class="login-form">
                    <%--<form method="post" action="/CMS/Ajax/Autorizacija.aspx">--%>
                    <form runat="server" id="frmMain" method="post">
                        <div class="form-group">
                            <label>Email adresa</label>
                            <input type="email" class="form-control" name="Email" placeholder="Email">
                        </div>
                        <div class="form-group">
                            <label>Zaporka</label>
                            <input type="password" class="form-control" name="Password" placeholder="Password">
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox">
                                Upamti me
                           
                            </label>
                            <label class="pull-right">
                                <a href="#">Zaboravljena zaporka?</a>
                            </label>

                        </div>
                        <button type="submit" class="btn btn-success btn-flat m-b-30 m-t-30">Prijavi se</button>
                        <%--<div class="social-login-content">
                            <div class="social-button">
                                <button type="button" class="btn social facebook btn-flat btn-addon mb-3"><i class="ti-facebook"></i>Prijavite se putem facebooka</button>
                                <button type="button" class="btn social twitter btn-flat btn-addon mt-2"><i class="ti-twitter"></i>Prijavite se s Twitterom</button>
                            </div>
                        </div>--%>
                        <div class="register-link m-t-15 text-center">
                            <p>Nemate račun? <a href="#">Prijavite se ovdje</a></p>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <script src="/CMS/assets/js/vendor/jquery-2.1.4.min.js"></script>
    <script src="/CMS/assets/js/popper.min.js"></script>
    <script src="/CMS/assets/js/plugins.js"></script>
    <script src="/CMS/assets/js/main.js"></script>

</body>
</html>
