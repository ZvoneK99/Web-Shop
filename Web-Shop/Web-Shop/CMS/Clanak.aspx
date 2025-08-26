<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Clanak.aspx.vb" Inherits="Web_Shop.Clanak1" %>
<%=Web_Shop.CMS.ChekAuth %>
<!doctype html>
<%=Web_Shop.CMS.Uvjeti() %>
<html class="no-js" lang="">
<!--<![endif]-->
<head>
    <title>AVE CMS - AVE Content Management System</title>
    <meta name="description" content="AVE CMS - Dashboard">
    <%=Web_Shop.CMS.ZajednickeMete() %>
    <!-- <link rel="stylesheet" href="assets/css/bootstrap-select.less"> -->
    <!-- <script type="text/javascript" src="https://cdn.jsdelivr.net/html5shiv/3.7.3/html5shiv.min.js"></script> -->

</head>
<body>
    <%=Web_Shop.CMS.LeftPanel() %>
    <div id="right-panel" class="right-panel">
        <%=Web_Shop.CMS.HeaderString() %>
        <div class="breadcrumbs">
            <div class="col-sm-4">
                <div class="page-header float-left">
                    <div class="page-title">
                        <h1>Članak</h1>
                    </div>
                </div>
            </div>
            <div class="col-sm-8">
                <div class="page-header float-right">
                    <div class="page-title">
                        <ol class="breadcrumb text-right">
                            <li class="active">Dashboard</li>
                            <li><a href="Pregled.aspx">Pregled članaka</a></li>
                            <li class="active">Članak</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <div class="content mt-3">
            <div class="animated fadeIn">
                <div class="row">
                    <%=Web_Shop.CMS.IzmjenaClanka() %>
                </div>
            </div>
        </div>
        <!-- .content -->
    </div>

    <%=Web_Shop.CMS.FootScript() %>
</body>
</html>
