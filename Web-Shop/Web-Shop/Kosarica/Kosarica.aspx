<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Kosarica.aspx.vb" Inherits="Web_Shop.Kosarica" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">
<head>
    <title>Kosarica | RescueEquip</title>
    <script type="text/javascript">
        WebFontConfig = {
            google: { families: ['Open+Sans:300,400,600,700,800', 'Poppins:300,400,500,600,700', 'Segoe Script:300,400,500,600,700'] }
        };
        (function (d) {
            var wf = d.createElement('script'), s = d.scripts[0];
            wf.src = 'assets/js/webfont.js';
            wf.async = true;
            s.parentNode.insertBefore(wf, s);
        })(document);
    </script>
    <%=Web_Shop.Komponente.ZajednickeMete() %>
</head>

<body>
    <div class="page-wrapper shopArt">
        <header class="header">
            <%=Web_Shop.Komponente.Header() %>
        </header>
        <main class="main">
            <nav aria-label="breadcrumb" class="breadcrumb-nav">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                        <li><strong>Košarica</strong></li>
                    </ol>
                </div>
            </nav>
            <div class="container">
                <ul class="checkout-progress-bar">
                    <li class="active">
                        <span>Košarica</span>
                    </li>
                    <li>
                        <span>Adresa &amp; Plaćanje</span>
                    </li>
                </ul>
                <div class="row">
                    <%=Web_Shop.Komponente.Kosarica() %>
                </div>
            </div>
        </main>
        <%=Web_Shop.Komponente.Footer() %>
    </div>
    <div class="mobile-menu-overlay"></div>
    <div class="mobile-menu-container">
        <%=Web_Shop.Komponente.HeaderMobile() %>
    </div>
    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%=Web_Shop.Komponente.FooterScript() %>
</body>
</html>
