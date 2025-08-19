<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ArtikliPodGrupe.aspx.vb" Inherits="Web_Shop.ArtikliPodGrupe" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">

<head>
    <title>Bulk.ba</title>

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
    <%Dim idGrupe As Integer = Request.QueryString("id")%>
    <%' Podijeli path po "/"
        Dim segments() As String = Request.Path.Split("/"c)
        Dim idPodGrupe As Integer = 0

        ' Provjeri da li path ima dovoljno segmenata
        If segments.Length > 2 Then
            ' segments(2) je ID podgrupe (43)
            Integer.TryParse(segments(2), idPodGrupe)
        End If

        ' Dobavi ID nadgrupe
        Dim idNadGrupe As Integer = Web_Shop.Komponente.PronadjiIdNadGrupe(idPodGrupe)
%>

    <div class="page-wrapper">

        <header class="header">
            <%=Web_Shop.Komponente.Header() %>
        </header>
        <!-- End .header -->
        <main class="main">
            <nav aria-label="breadcrumb" class="breadcrumb-nav">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span> </li>
                        <li><a title="Idi na kategoriju" href="/grupa/<%= idNadGrupe %>/<%=Web_Shop.Komponente.SrediNaziv(Web_Shop.Komponente.PronadjiNazivNadGrupe(idNadGrupe)) %>/"><%= Web_Shop.Komponente.PronadjiNazivNadGrupe(idNadGrupe) %> </a><span>&raquo;</span></li>
                        <li>
                            <strong><%= Web_Shop.Komponente.PronadjiNazivGrupe(idGrupe) %></strong>
                        </li>
                    </ol>

                </div>
            </nav>
            <div class="container">
                <div class="row row-sm">
                    <div class="col-lg-12 shop-inner podkategorija">
                        <nav class="toolbox">
                            <div class="toolbox-left">
                                <div class="toolbox-item toolbox-sort">
                                    <div class="select-custom" style="cursor: pointer;">
                                        <input type="hidden" name="KategorijaID" value='<%Response.Write(idGrupe)%>' class="hidPodKategorijaID" />
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
                        <div class="row row-sm product-intro divide-line up-effect products-grid shopArt">
                            <%=Web_Shop.Komponente.ArtikliGrupe(idGrupe) %>
                        </div>
                        <nav class="toolbox toolbox-pagination">
                        </nav>
                    </div>
                </div>
            </div>
            <div class="mb-5"></div>
        </main>
        <!-- End .main -->

        <%=Web_Shop.Komponente.Footer() %>
    </div>
    <!-- End .page-wrapper -->
    <div class="mobile-menu-overlay"></div>
    <div class="mobile-menu-container">
        <%=Web_Shop.Komponente.HeaderMobile() %>
    </div>
    <!-- End .mobile-menu-container -->

    <!-- Add Cart Modal -->
    <%=Web_Shop.Komponente.DodajUKosaricu() %>

    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%=Web_Shop.Komponente.FooterScript() %>
    <script src="/assets/js/nouislider.min.js"></script>
</body>
</html>
