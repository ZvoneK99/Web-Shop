<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="Web_Shop.Dashboard" %>

<%=Web_Shop.CMS.ChekAuth %>
<!doctype html>
<%=Web_Shop.CMS.Uvjeti() %>
<html class="no-js" lang="">
<!--<![endif]-->
<head>
    <title>CMS</title>
    <meta name="description" content="CMS">
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
                        <h1>Dashboard</h1>
                    </div>
                </div>
            </div>
            <div class="col-sm-8">
                <div class="page-header float-right">
                    <div class="page-title">
                       
                    </div>
                </div>
            </div>
        </div>
        <div class="content mt-3">
            <%=Web_Shop.CMS.StatistikaArtikala() %>

            <%--<%=Web_Shop.CMS.ArtikliAktivniStats() %>
            <%=Web_Shop.CMS.ArtikliNeAktivniStats() %>
            <%=Web_Shop.CMS.ArtikliNaMinimumuStats() %>
            <%=Web_Shop.CMS.ArtikliBezKolicineStats()%>--%>

            <%=Web_Shop.CMS.Statistika2()%>
        </div>
        <!-- .content -->
    </div>
    <!-- /#right-panel -->

    <%=Web_Shop.CMS.FootScript() %>

    <%--<script src="/CMS/assets/js/vendor/jquery-2.1.4.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js"></script>
    <script src="/CMS/assets/js/plugins.js"></script>
    <script src="/CMS/assets/js/main.js"></script>

    <script src="/CMS/assets/js/lib/chart-js/Chart.bundle.js"></script>
    <script src="/CMS/assets/js/dashboard.js"></script>
    <script src="/CMS/assets/js/widgets.js"></script>
    <script src="/CMS/assets/js/lib/vector-map/jquery.vmap.js"></script>
    <script src="/CMS/assets/js/lib/vector-map/jquery.vmap.min.js"></script>
    <script src="/CMS/assets/js/lib/vector-map/jquery.vmap.sampledata.js"></script>
    <script src="/CMS/assets/js/lib/vector-map/country/jquery.vmap.world.js"></script>--%>
    <%--<script>
        (function ($) {
            "use strict";

            jQuery('#vmap').vectorMap({
                map: 'world_en',
                backgroundColor: null,
                color: '#ffffff',
                hoverOpacity: 0.7,
                selectedColor: '#1de9b6',
                enableZoom: true,
                showTooltip: true,
                values: sample_data,
                scaleColors: ['#1de9b6', '#03a9f5'],
                normalizeFunction: 'polynomial'
            });
        })(jQuery);
    </script>--%>
</body>
</html>
