    <%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Artikal.aspx.vb" Inherits="Web_Shop.Artikal" %>

<%
Dim idArtikla As Integer = 0

' Try query string first
If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
    Integer.TryParse(Request.QueryString("id"), idArtikla)
End If

' If still zero, try path segments
If idArtikla = 0 Then
    Dim segments() As String = Request.Path.Split("/"c)
    If segments.Length > 2 Then
        Integer.TryParse(segments(2), idArtikla)
    End If
End If
%>


<%Dim domena As String = Web_Shop.Komponente.Domena()%>
<%Dim OpisArtikla As String = Regex.Replace(Web_Shop.Komponente.OpisArtikal(idArtikla), "<.*?>", "")%>
<%Dim idGrupe As Integer = Web_Shop.Komponente.PronadjiIdGrupe(idArtikla)%>
<%Dim idNadGrupe As Integer = Web_Shop.Komponente.PronadjiIdNadGrupe(idGrupe)%>
<%Dim NazivNadGrupe As String = Web_Shop.Komponente.PronadjiNazivNadGrupe(idNadGrupe)%>
<%Dim NazivGrupe As String = Web_Shop.Komponente.PronadjiNazivGrupe(idGrupe)%>
<%Dim NazivArtikla As String = Web_Shop.Komponente.NazivArtikal(idArtikla)%>

    
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">
<head>
    <title><%=NazivArtikla%> <%=NazivArtikla %> | RescueEquip</title>

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

    <!--Za Podjeliti-->
    <meta name="keywords" content="<%=NazivArtikla%>, <%=NazivNadGrupe%>, <%=NazivGrupe%>" />
    <meta name="description" content="<%=OpisArtikla%>" />
    <meta property="og:type" content="website" />
    <meta property="og:url" content="http://<%=domena%>/artikal/<%=idArtikla%>/<%=Web_Shop.Komponente.SrediNaziv(NazivArtikla)%>/" />
    <meta property="og:title" content="<%=NazivArtikla%>" />
    <meta property="og:description" content="<%=OpisArtikla%>" />
    <meta property="og:image" content="<%=Web_Shop.Komponente.ZadanaSlikaArtikla(idArtikla) %> " />
    <meta property="twitter:card" content="summary_large_image" />
    <meta property="twitter:url" content="http://<%=domena%>/artikal/<%=idArtikla%>/<%=Web_Shop.Komponente.SrediNaziv(NazivArtikla)%>/" />
    <meta property="twitter:title" content="<%=NazivArtikla%>" />
    <meta property="twitter:description" content="<%=OpisArtikla%>" />
    <meta property="twitter:image" content="<%=Web_Shop.Komponente.ZadanaSlikaArtikla(idArtikla) %>" />

    <%=Web_Shop.Komponente.ZajednickeMete() %>
</head>
<body class="shopArt">
    <div class="page-wrapper">
        <header class="header">
            <%=Web_Shop.Komponente.Header() %>
        </header>
        <!-- End .header -->

        <main class="main">

            <nav aria-label="breadcrumb" class="breadcrumb-nav container">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                        <li><a title="Idi na kategoriju" href="/grupa/<%= idNadGrupe %>/<%= Web_Shop.Komponente.SrediNaziv(Web_Shop.Komponente.PronadjiNazivNadGrupe(idNadGrupe)) %>/"><%= Web_Shop.Komponente.PronadjiNazivNadGrupe(idNadGrupe) %></a><span>&raquo;</span></li>
                        <li><a title="Idi na podkategoriju" href="/podgrupa/<%= idGrupe %>/<%= Web_Shop.Komponente.SrediNaziv(Web_Shop.Komponente.PronadjiNazivGrupe(idGrupe)) %>/"><%= Web_Shop.Komponente.PronadjiNazivGrupe(idGrupe) %></a><span>&raquo;</span></li>
                        <li><strong><%= NazivArtikla %></strong></li>
                    </ol>
                </div>
            </nav>

            <div class="container">
                <div class="product-single-container product-single-default">
                    <div class="row">

                        <%=Web_Shop.Komponente.ProductSingleGallery() %>


                        <%=Web_Shop.Komponente.ProductSingleDescription() %>
                    </div>
                </div>
            </div>

            <div class="with-border-top">

                <%=Web_Shop.Komponente.NajNovije() %>
            </div>
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
    <%--<%=Web_Shop.Komponente.DodajUKosaricu() %>--%>
    <div class="modal fade" id="addCartModal" tabindex="-1" role="dialog" aria-labelledby="addCartModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body add-cart-box text-center">
                    <p>
                        Proizvod je dodan u košaricu<br>
                    </p>
                    <h4 id="productTitle"></h4>
                    <%--<img src="" id="productImage" width="100" height="100" alt="adding cart image">--%>
                    <div class="btn-actions">
                        <a href="/kosarica">
                            <button class="btn-primary">Idite na košaricu!</button></a>
                        <a href="#">
                            <button class="btn-primary" data-dismiss="modal">Nastavite!</button></a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%=Web_Shop.Komponente.FooterScript() %>
    <!-- www.addthis.com share plugin -->
    <script src="https://s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5b927288a03dbde6"></script>

    <script src="https://s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5b927288a03dbde6"></script>
</body>
</html>

