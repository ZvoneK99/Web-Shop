<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdresaDostave.aspx.vb" Inherits="Web_Shop.AdresaDostave" %>

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
    <div class="page-wrapper">
        <header class="header">
            <%=Web_Shop.Komponente.Header() %>
        </header>

        <nav aria-label="breadcrumb" class="breadcrumb-nav" id="breadcrumbs">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                    <li class="home"><a title="Idi na košaricu" href="/kosarica">Košarica</a><span>&raquo;</span></li>
                    <li><strong>Adresa i plaćanje</strong></li>
                </ol>
            </div>
        </nav>
        <div class="container">
            <ul class="checkout-progress-bar">
                <li>
                    <span>Košarica</span>
                </li>
                <li class="active">
                    <span>Adresa &amp; Plaćanje</span>
                </li>
            </ul>
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="step-title">Adresa za dostavu</h2>
                </div>
                <br />
                <br />
                <br />
                <div class="col-md-6 ">
                    <div class="heading">
                        <h2 class="title">Prijavljeni korisnik?</h2>
                        <%--<p>Ako već imate račun kod nas, prijavite se.</p>--%>
                    </div>
                    <!-- End .heading -->
                    <form novalidate method="post" action="/Ajax/KorisnikPrijava.aspx?putanja=/adresa-dostave" id="prijavaFormular">

                        <label for="prijavaKorisnickoIme">Email</label>
                        <input type="email" id="prijavaKorisnickoIme" name="prijavaKorisnickoIme" class="form-control" required />

                        <label for="passw-prijava">Lozinka</label>
                        <input type="password" id="prijavaLozinka" name="prijavaLozinka" class="form-control" required />

                        <div class="form-footer">
                            <button type="submit" class="btn btn-primary">PRIJAVA</button>
                            <p class="forgot-pass"><a href="/izgubljena-lozinka" class="forget-pass">Zaboravljena lozinka?</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                            <%--<a href="#" class="forget-pass">Nemate račun? Registrirajte se!</a>--%>
                        </div>
                    </form>
                </div>

                <div class="col-md-6 registration-div">
                    <div class="heading">
                        <h2 class="title" id="/registracija">Naručujete kao GOST? - Ispunite podatke!</h2>
                        <!--<p>Molimo Vas ispunite podatke.</p>-->
                    </div>
                    <!-- End .heading -->

                    <form novalidate method="post" action="/GoPay" id="registracijaFormular" autocomplete="off">
                        <label>Ime i Prezime/Naziv tvrtke:</label>
                        <input type="text" runat="server" id="imePrezime" name="imePrezime" class="form-control" required />

                        <label>ID Broj</label>
                        <input type="text" runat="server" id="IdBroj" name="IdBroj" class="form-control"/>

                        <label>PDV Broj</label>
                        <input type="text" runat="server" id="PdvBroj" name="PdvBroj" class="form-control"/>

                        <label>Vaš email</label>
                        <input type="email" runat="server" name="email" id="email" class="form-control" required />

                        <label>Adresa</label>
                        <input type="text" runat="server" id="adresa" name="adresa" class="form-control" required />

                        <label>Grad</label>
                        <input type="text" runat="server" id="grad" name="grad" class="form-control" required />

                        <label>Poštanski broj</label>
                        <input type="text" runat="server" id="postBr" name="postBr" class="form-control" required />

                        <label>Broj telefona</label>
                        <input type="text" runat="server" id="brTel" name="brTel" class="form-control" required />

                        <label for="nacin-placanja">Način plaćanja</label>
                        <select name="nacin-placanja" class="form-control">
                        <option value="Virman">Virman</option>
                        <option value="poduzecem">Poduzećem</option>
                        <option value="karticno">Kartično</option>
                        <%--<option value="mikrofin">MKD Mikrofin</option>--%>
                        </select>

                        <label for="Napomena"></label>
                        <textarea name="Napomena" class="form-control" placeholder="Dodatna napomena..."></textarea>

                        <div class="form-footer">
                            <button type="submit" class="btn btn-primary">Naruči</button>
                        </div>
                    </form>
                </div>
            
                <!-- End .col-md-6 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .main -->
        <%=Web_Shop.Komponente.Footer() %>
    </div>

    <div class="mobile-menu-overlay"></div>

    <div class="mobile-menu-container">

        <%=Web_Shop.Komponente.HeaderMobile() %>
    </div>

    <!-- End .page-wrapper -->
    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%=Web_Shop.Komponente.FooterScript() %>
</body>
</html>

