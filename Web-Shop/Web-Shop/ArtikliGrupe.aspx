<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ArtikliGrupe.aspx.vb" Inherits="Web_Shop.ArtikliGrupe" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">

<head>
    <title>RescueEquip | Rescue - Ready Gear</title>

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
    <%=Web_Shop.Komponente.GoogleTagManager() %>
    <%Dim idNadGrupe As Integer = Request.QueryString("id")%>

    <script language='javascript'>
       <%-- window.onload = function () {
            document.getElementById('a<%Response.Write(idNadGrupe)%>').classList.add("active");
        };--%>
    </script>

    <div class="page-wrapper">

        <header class="header">
            <%=Web_Shop.Komponente.Header() %>
        </header>
        <!-- End .header -->

        <main class="main">

            <nav aria-label="breadcrumb" class="breadcrumb-nav" id="breadcrumbs">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                        <li><strong><%=Web_Shop.Komponente.PronadjiNazivNadGrupe(idNadGrupe)%></strong></li>
                    </ol>
                </div>
            </nav>
            <div class="container shopArt">
                <div class="row row-sm">
                    <div class="col-lg-12 shop-inner kategorija">

                        <!--Sortiranje-->
                        <nav class="toolbox">
                            <div class="toolbox-left">
                                <div class="toolbox-item toolbox-sort">
                                    <div class="select-custom" style="cursor: pointer;">
                                        <label id="sortiranje">Sortiraj:</label>
                                        <select id="ddlSortiraj" name="orderby" class="form-control ddlSortiraj" runat="server" style="cursor: pointer;">
                                            <option value="NazivAsc" selected="selected">Nazivu uzlazno</option>
                                            <option value="NazivDesc">Nazivu silazno</option>
                                            <option value="CijenaAsc">Cijeni manjoj</option>
                                            <option value="CijenaDesc">Cijeni većoj</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </nav>
                        <!--Artikli-->
                        <div class="row row-sm product-intro divide-line up-effect products-grid">
                            <%=Web_Shop.Komponente.ArtikliNadGrupe(idNadGrupe) %>
                        </div>

                        <!--Brojevi stranice-->
                        <nav class="toolbox toolbox-pagination">
                            <div class="pagination-area ">
                                <input type="button" value="Prethodna" class="dugmic prethodna" data-kat='<%Response.Write(idNadGrupe)%>' />
                                <input type="text" value="1" class="textP polje stranica" disabled />
                                <input type="button" value="Slijedeća" class="dugmic slijedeca" data-kat='<%Response.Write(idNadGrupe)%>' />
                                <input type="hidden" name="KategorijaID" value='<%Response.Write(idNadGrupe)%>' class="hidKategorijaID" />
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </main>
        <%=Web_Shop.Komponente.Footer() %>
    </div>

    <div class="mobile-menu-overlay"></div>

    <div class="mobile-menu-container">

        <%=Web_Shop.Komponente.HeaderMobile() %>
    </div>
    <!-- Dodavanje u košaricu -->
    <%=Web_Shop.Komponente.DodajUKosaricu() %>

    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%=Web_Shop.Komponente.FooterScript() %>
    <script src="/assets/js/nouislider.min.js"></script>
</body>
</html>
