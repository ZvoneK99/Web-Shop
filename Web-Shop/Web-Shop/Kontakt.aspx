<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Kontakt.aspx.vb" Inherits="Web_Shop.Kontakt" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="hr-BA" xml:lang="hr-BA">

<head>
    <title>Kontakt | RescueEquip</title>
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
                        <li><strong>Kontakt</strong></li>
                    </ol>
                </div>
            </nav>

            <!--Poruka uspješno poslana-->
            <% If Request.QueryString("msg") = "poslano" Then %>
            <div class="div-kontakt-reset" id="reset-div">
                <h3 style="color: green; text-align: center;" id="kontakt-msg">Vaš upit je uspješno poslan!</h3>
            </div>
            <% End If %>
            <div class="col-md-6 registration-div">
                <div class="heading">
                    <h2 class="title" id="/registracija">Kontakt</h2>
                    <p>Za sva dodatna pitanja, slobodno nas kontaktirajte</p>
                </div>

                <form method="post" class="dzForm" action="/Ajax/PosaljiMail.aspx?putanja=/pocetna" autocomplete="off" id="myForm">
                    <label>Ime i Prezime</label>
                    <input type="text" id="imePrezime" name="ImePrezime" class="form-control" required autocomplete="off" />

                    <label>Email</label>
                    <input type="email" name="Email" id="email" class="form-control" required autocomplete="off" />

                    <label>Broj telefona</label>
                    <input type="text" id="br-tel" name="Telefon" class="form-control" required autocomplete="off" />

                    <label>Poruka</label>
                    <textarea class="form-control input-sm" name="Poruka" rows="7" id="message"></textarea>

                    <div class="form-footer">
                        <button type="submit" class="btn btn-primary "><i class="fa fa-send"></i>&nbsp; <span>Pošaljite poruku</span></button>
                    </div>
                </form>
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
