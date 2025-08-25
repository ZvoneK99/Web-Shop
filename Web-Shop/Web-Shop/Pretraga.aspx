<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Pretraga.aspx.vb" Inherits="Web_Shop.Pretraga" %>

<% 
    ' Dohvati pojam iz QueryString-a
    Dim pojam As String = ""
    If Not String.IsNullOrEmpty(Request.QueryString("pojam")) Then
        pojam = Request.QueryString("pojam")
    End If
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">

<head>
    <title>Rezultat pretrage za "<%= pojam %>" | RescueEquip</title>

    <script type="text/javascript">
        WebFontConfig = {
            google: { families: ['Open+Sans:300,400,600,700,800', 'Poppins:300,400,500,600,700', 'Segoe Script:300,400,500,600,700'] }
        };
        (function (d) {
            var wf = d.createElement('script'), s = d.scripts(0);
            wf.src = 'assets/js/webfont.js';
            wf.async = true;
            s.parentNode.insertBefore(wf, s);
        })(document);
    </script>

    <%= Web_Shop.Komponente.ZajednickeMete() %>
</head>

<body>
    <div class="page-wrapper">
        <header class="header">
            <%= Web_Shop.Komponente.Header() %>
        </header>

        <main class="main">
            <!-- Breadcrumb -->
            <nav aria-label="breadcrumb" class="breadcrumb-nav" id="breadcrumbs">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                        <li><strong>Rezultat pretrage za "<%= pojam %>"</strong></li>
                    </ol>
                </div>
            </nav>

            <div class="container shopArt">
                <div class="row row-sm">
                    <div class="col-lg-12 shop-inner kategorija">

                        <!-- Rezultati pretrage -->
                        <div class="row row-sm product-intro divide-line up-effect products-grid">
                            <%= Web_Shop.Komponente.Pretraga(pojam) %>
                        </div>

                        <!-- Brojevi stranice -->
                        <nav class="toolbox toolbox-pagination">
                            <div class="pagination-area">
                                <input type="button" value="Prethodna" class="dugmic prethodna" />
                                <input type="text" value="1" class="textP polje stranica" disabled />
                                <input type="button" value="Slijedeća" class="dugmic slijedeca" />
                                <input type="hidden" name="Pojam" value='<%= pojam %>' class="hidPojam" />
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </main>

        <%= Web_Shop.Komponente.Footer() %>
    </div>

    <div class="mobile-menu-overlay"></div>

    <div class="mobile-menu-container">
        <%= Web_Shop.Komponente.HeaderMobile() %>
    </div>

    <%= Web_Shop.Komponente.DodajUKosaricu() %>

    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%= Web_Shop.Komponente.FooterScript() %>
    <script src="/assets/js/nouislider.min.js"></script>
</body>
</html>