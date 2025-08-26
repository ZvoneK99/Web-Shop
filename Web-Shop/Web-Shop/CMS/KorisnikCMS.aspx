<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="KorisnikCMS.aspx.vb" Inherits="igre_ba.KorisnikCMS" %>

<%=igre_ba.CMS.ChekAuth %>
<!doctype html>
<%=igre_ba.CMS.Uvjeti() %>
<html class="no-js" lang="">
<!--<![endif]-->
<head>
    <title>AVE CMS - AVE Content Management System</title>
    <meta name="description" content="AVE CMS - Dashboard">
    <%=igre_ba.CMS.ZajednickeMete() %>
    <!-- <link rel="stylesheet" href="assets/css/bootstrap-select.less"> -->
    <!-- <script type="text/javascript" src="https://cdn.jsdelivr.net/html5shiv/3.7.3/html5shiv.min.js"></script> -->

</head>
<body>
    <%=igre_ba.CMS.LeftPanel() %>
    <div id="right-panel" class="right-panel">
        <%=igre_ba.CMS.HeaderString() %>
        <div class="breadcrumbs">
            <div class="col-sm-4">
                <div class="page-header float-left">
                    <div class="page-title">
                        <h1>Korisnik</h1>
                    </div>
                </div>
            </div>
            <div class="col-sm-8">
                <div class="page-header float-right">
                    <div class="page-title">
                        <ol class="breadcrumb text-right">
                            <li class="active">Dashboard</li>
                            <li><a href="Korisnici.aspx">Korisnici</a></li>
                            <li class="active">Korisnik</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <div class="content mt-3">
            <% Dim msg As String = Request.QueryString("msg")%>
            <% If msg = "zaporkauspjesno" Then%>
            <div class="sufee-alert alert with-close alert-success alert-dismissible fade show" id="alert-success-zaporka">
                <span class="badge badge-pill badge-success">Uspješno</span>
                Zaporka je uspješno promjenjena 
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <% End If%>
            <% If msg = "zaporkaneuspjesno" Then%>
            <div class="sufee-alert alert with-close alert-danger alert-dismissible fade show" id="alert-danger-zaporka">
                <span class="badge badge-pill badge-danger">Neuspješno</span>
                Zaporka je nije promjenjena! Zaporke se ne podudaraju ili ste upisali zabranjene znakove. 
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <% End If%>
            <% If msg = "korisnikuspjesno" Then%>
            <div class="sufee-alert alert with-close alert-success alert-dismissible fade show" id="alert-success-korisnik">
                <span class="badge badge-pill badge-success">Uspješno</span>
                Korisnik je uspješno promjenjen 
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <% End If%>
            <div class="animated">
                <div class="row">
                    <%=igre_ba.CMS.IzmjenaKorisnika() %>
                </div>
            </div>
        </div>
        <!-- .content -->
    </div>



    <%=igre_ba.CMS.FootScript() %>
</body>
</html>
