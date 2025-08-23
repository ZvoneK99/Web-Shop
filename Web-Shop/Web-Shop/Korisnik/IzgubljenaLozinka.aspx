<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IzgubljenaLozinka.aspx.vb" Inherits="Web_Shop.IzgubljenaLozinka" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">

<head>
    <title>Izgubljena lozinka | RescueEquip</title>
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

        <main class="home main">
            <nav aria-label="breadcrumb" class="breadcrumb-nav" id="breadcrumbs">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                        <li><a title="Idi na prijavu" href="/login">Prijava i Registracija</a><span>&raquo;</span></li>
                        <li><strong>Restart lozinke</strong></li>
                    </ol>
                </div>
            </nav>

            <% If Request.QueryString("msg") = "poslano" Then %>
            <div class="div-kontakt-reset" id="reset-div">
            <h3 style="color: green; text-align: center;" id="restart-msg">Vaša lozinka je poslana na Email!</h3>
            </div>
            <% End If %>

            <div class="col-md-6 registration-div">
                <div class="heading">
                    <h2 class="title">Zaboravili ste lozinku?</h2>
                    <p>Nema problema, unesite Vaš email i poslat ćemo vam novu!</p>
                </div>
                <!-- End .heading -->

                <form novalidate method="post" action="/Ajax/ResetLozinke.aspx" id="resetFormular" autocomplete="off">
                    <label for="emmail_login">Vaš email </label>
                    <input type="email" id="prijavaKorisnickoIme" name="prijavaKorisnickoIme" type="text" class="form-control" required />

                    <div class="form-footer">
                        <button type="submit" class="btn btn-primary">Zatraži</button>
                                <p class="back-to-login"><a href="/login" class="forget-pass">Vratite se na prijavu</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>

                    </div>
                    <!-- End .form-footer -->
                </form>
            </div>
        </main>
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

