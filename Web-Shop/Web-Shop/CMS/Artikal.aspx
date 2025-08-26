<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Artikal.aspx.vb" Inherits="igre_ba.Artikal1" ValidateRequest="false" %>

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
                        <h1>Artikal</h1>
                    </div>
                </div>
            </div>
            <div class="col-sm-8">
                <div class="page-header float-right">
                    <div class="page-title">
                        <ol class="breadcrumb text-right">
                            <li class="active">Dashboard</li>
                            <li><a href="Artikli.aspx">Artikli</a></li>
                            <li class="active">Artikal</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <div class="content mt-3">
            <div class="animated fadeIn">
                <div class="row">
                    <%=igre_ba.CMS.IzmjenaArtikla() %>
                </div>
            </div>
        </div>
        <!-- .content -->
    </div>

    <%=igre_ba.CMS.FootScript() %>
</body>
</html>
