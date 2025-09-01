<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Web_Shop._Default" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">

<head>
    <!--Naslov i Kljucne rijeci-->
    <title>RescueEquip | Rescue - Ready Gear</title>

    <meta name="keywords" content="planinarska oprema, spasilačka oprema, popust na količinu, sigurnosna oprema BiH, web shop za planinarsku opremu" />
    <meta name="description" content="Demo web shop za završni rad – planinarska i spasilačka oprema s količinskim popustima. Primjer funkcionalnosti web trgovine, prikaz akcija i popusta. Ovaj web shop služi isključivo kao projekt za završni rad." />

    <meta property="og:type" content="website" />
    <meta property="og:url" content="https://rescueequip.ba" />
    <meta property="og:title" content="RescueEquip" />
    <meta property="og:description" content="Primjer web trgovine izrađene za završni rad. Fokus na planinarskoj i spasilačkoj opremi, s mogućnošću prikaza popusta i akcija." />

    <meta property="twitter:card" content="summary_large_image" />
    <meta property="twitter:url" content="https://rescueequip.ba" />
    <meta property="twitter:title" content="RescueEquip" />
    <meta property="twitter:description" content="Primjer web shop projekta za završni rad – planinarska i   spasilačka oprema s količinskim popustima." />


    <script type="text/javascript">
        WebFontConfig = {
            google: { families: ['Open+Sans:300,400,600,700,800', 'Poppins:300,400,500,600,700', 'Segoe Script:300,400,500,600,700'] }
        };
        (function (d) {
            var wf = d.createElement('script'), s = d.scripts[0];
            wf.src = '/assets/js/webfont.js';
            wf.async = true;
            s.parentNode.insertBefore(wf, s);
        })(document);
    </script>

    <!--Put all the css files in function ZajednickeMete-->
    <%=Web_Shop.Komponente.ZajednickeMete() %>

</head>
<body>
    <div class="page-wrapper">
        <header class="header">
            <%=Web_Shop.Komponente.Header() %>
        </header>
        <main class="home main shopArt">

            <!--Slider slika-->
            <div class="top-slider owl-carousel owl-theme" data-toggle="owl" data-owl-options="{
                'items' : 1,
                'margin' : 0,
                'nav': true,
                'dots': false
            }">
                <%=Web_Shop.Komponente.Slider() %>
                <!--Banner slider on Default-->
            </div>

            <div class="container">
                <nav class="toolbox">
                    <div class="toolbox-left">
                        <div class="toolbox-item toolbox-sort">
                            <h1 id="naslov-istaknuti">ISTAKNUTI PROIZVODI</h1>
                            <a href="#" class="sorter-btn" title="Set Ascending Direction"><span class="sr-only">Set Ascending Direction</span></a>
                        </div>
                    </div>
                </nav>
                <div class="row row-sm">
                    <%=Web_Shop.Komponente.IstaknutiProizvodi() %>
                    <!--Istaknuti proizvodi na Default-->
                </div>
                <nav class="toolbox toolbox-pagination">
                </nav>
            </div>

            <div class="container">
                <%=Web_Shop.Komponente.NajNovije() %>
                <!--Novo iz ponude na Default-->
            </div>
        </main>
        <!--Function for footer display-->
        <%=Web_Shop.Komponente.Footer() %>
    </div>
    <div class="mobile-menu-overlay"></div>


    <div class="mobile-menu-container">
        <!--Mobile menu for header-->
        <%=Web_Shop.Komponente.HeaderMobile() %>
    </div>


    <%=Web_Shop.Komponente.DodajUKosaricu() %>
    <!--Modal for adding product to cart-->

    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <!--Footer scripts and links-->
    <%=Web_Shop.Komponente.FooterScript() %>
    <script src="/assets/js/nouislider.min.js"></script>
</body>
</html>
