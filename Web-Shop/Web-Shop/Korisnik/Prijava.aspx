<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Prijava.aspx.vb" Inherits="Web_Shop.Prijava" %>

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

        <main class="main">
            <nav aria-label="breadcrumb" class="breadcrumb-nav" id="breadcrumbs">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="home"><a title="Idi na početnu" href="/">Početna</a><span>&raquo;</span></li>
                        <li><strong>Prijava i Registracija</strong></li>
                    </ol>
                </div>
            </nav>

            <div class="page-header">
                <div class="container">
                    <h1>Prijavite se  ili Kreirajte račun</h1>
                </div>
                <!-- End .container -->
            </div>
            <!-- End .page-header -->

            <div class="container">
                <div class="row">
                    <div class="col-md-6 ">
                        <div class="heading">
                            <h2 class="title">Prijavite se</h2>
                            <p>Ako već imate račun kod nas, prijavite se.</p>
                        </div>
                        <!-- End .heading -->
                        <form novalidate method="post" action="/Ajax/KorisnikPrijava.aspx" id="prijavaFormular">

                            <label for="prijavaKorisnickoIme">Vaša e-pošta ili korisničko ime</label>
                            <input type="email" id="prijavaKorisnickoIme" name="prijavaKorisnickoIme" class="form-control" required />

                            <label for="passw-prijava">Lozinka</label>
                            <input type="password" id="prijavaLozinka" name="prijavaLozinka" class="form-control" required />

                            <% If Request.QueryString("msg-passw") = "pogreska" Then %>
                                <h3 style="color: red; text-align: center; font-size:12px; font-weight: 400;" id="reset-div">Pogrešna lozinka ili email!</h3>
                            <% End If %>

                            <div class="form-footer">
                                <button type="submit" class="btn btn-primary">PRIJAVA</button>
                                <p class="forgot-pass back-to-login"><a href="/izgubljena-lozinka" class="forget-pass">Zaboravljena lozinka?</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                                <%--<a href="#" class="forget-pass">Nemate račun? Registrirajte se!</a>--%>
                            </div>
                        </form>
                    </div>

                    <%Dim msg As String = Request.QueryString("msg")%>
                    <%If msg = "" Or msg = "pass" Then%>

                    <div class="col-md-6 registration-div">
                        <div class="heading">
                            <h2 class="title" id="/registracija">KREIRAJTE RAČUN</h2>
                            <p>Ako nemate račun, registrirajte se.</p>
                        </div>
                        <!-- End .heading -->

                        <form novalidate method="post" action="/Ajax/Registracija.aspx" id="registracijaFormular" autocomplete="off">
                            <%If msg = "pass" Then%>
                            <span style="color: red;">GREŠKA LOZINKE</span>
                            <% End If%>
                            <label>Ime i Prezime</label>
                            <input type="text" id="imePrezime" name="ImePrezime" class="form-control" required />

                            <label>Vaš email</label>
                            <input type="email" name="Email" id="email" class="form-control" required />

                            <label>Vaša adresa</label>
                            <input type="text" id="adresa" name="Adresa" class="form-control" required />

                            <label>Grad</label>
                            <input type="text" id="grad" name="Grad" class="form-control" required />

                            <label>Poštanski broj</label>
                            <input type="text" id="post-br" name="ZIP" class="form-control" required />

                            <label>Broj telefona</label>
                            <input type="text" id="br-tel" name="Telefon" class="form-control" required />

                            <label>Vaša lozinka</label>
                            <input name="lozinka1" type="password" id="password" class="form-control" required />

                            <label>Potvrda lozinke</label>
                            <input name="lozinka2" type="password" id="password-confirm" class="form-control" required />

                            <div class="form-footer">
                                <button type="submit" class="btn btn-primary">Kreiraj račun</button>
                            </div>
                        </form>
                    </div>
                    <%Else%>
                    <div class="col-md-6 registration-div">
                        <div class="heading">
                            <h2>Vaša registracija je uspješna</h2>
                        </div>
                    </div>

                    <% End If%>
                </div>
            </div>

        </main>
        <%=Web_Shop.Komponente.Footer() %>
    </div>

    <div class="mobile-menu-overlay"></div>

    <div class="mobile-menu-container">

        <%=Web_Shop.Komponente.HeaderMobile() %>
    </div>

    <!-- End .page-wrapper -->
    <a id="scroll-top" href="#top" title="Top" role="button"><i class="icon-angle-up"></i></a>

    <%=Web_Shop.Komponente.FooterScript() %>
    <script>
        window.addEventListener('DOMContentLoaded', function () {
            const idBrojInput = document.getElementById('IdBroj');

            if (idBrojInput) {
                // Resetuj vrijednost odmah
                idBrojInput.value = '';

                // Spriječi autofill - trik s vremenskim odmakom
                setTimeout(() => {
                    idBrojInput.setAttribute('autocomplete', 'off');
                    idBrojInput.value = '';
                }, 100);

                // Dodatno: kada korisnik klikne, obriši još jednom (za tvrdoglavi Chrome)
                idBrojInput.addEventListener('focus', () => {
                    idBrojInput.value = '';
                });
            }

            const lozinkaInput = document.getElementById("password");

            if (lozinkaInput) {
                lozinkaInput.value = '';
            }
            setTimeout(() => {
                lozinkaInput.setAttribute('autocomplete', 'off');
                lozinkaInput.value = '';
            }, 100);

            lozinkaInput.addEventListener('focus', () => {
                lozinkaInput.value = '';
            });
        });
    </script>

</body>
</html>

