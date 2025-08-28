Imports System.Data.SqlClient

Public Class Komponente

    '----------------------------------------------------------------------------------------------> General functions START <-------------------------------------------------------------------------------------------

    Public Shared Function SQLKonekcija() As String     'Connection string for the database
        Dim html As New StringBuilder()
        html.Append(ConfigurationManager.ConnectionStrings("conZavrsni").ConnectionString)
        Return html.ToString()
    End Function

    Public Shared Function ZajednickeMete() As String   'All the meta tags and links that are common for all pages
        Dim html As New StringBuilder()

        html.Append("<meta charset='UTF-8' />")
        html.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge'/>")
        html.Append("<meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no' />")
        html.Append("<meta name='keywords' content='HTML5 Template'/>")
        html.Append("<meta name='description' content='RescueEquip' />")
        html.Append("<meta name='author' content='SW-THEMES' />")

        'Font-Awesome
        html.Append("<link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'>")
        html.Append("<link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css'>")
        html.Append("<link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css'>")

        'Favicon
        html.Append("<link rel='icon' type='image/x-icon' href='assets/images/icons/favicon.ico' />")
        'Plugins CSS File
        html.Append("<link rel='stylesheet' href='/assets/css/bootstrap.min.css' />")
        'Main CSS File
        html.Append(" <link rel='stylesheet' href='/assets/css/style.css' />")
        html.Append("<link rel='stylesheet' type='text/css' href='/assets/vendor/fontawesome-free/css/all.min.css' />")
        'Moj CSS File
        html.AppendFormat("<link rel='stylesheet' type='text/css' href='/CSS/myStyle.css?v={0}' />", Format(DateAndTime.Now(), "HHmm"))

        'FAVICONS
        ' Apple Touch Icons
        html.Append("<link rel=""apple-touch-icon"" sizes=""57x57"" href=""/apple-icon-57x57.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""60x60"" href=""/apple-icon-60x60.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""72x72"" href=""/apple-icon-72x72.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""76x76"" href=""/apple-icon-76x76.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""114x114"" href=""/apple-icon-114x114.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""120x120"" href=""/apple-icon-120x120.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""144x144"" href=""/apple-icon-144x144.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""152x152"" href=""/apple-icon-152x152.png""/>")
        html.Append("<link rel=""apple-touch-icon"" sizes=""180x180"" href=""/apple-icon-180x180.png""/>")

        ' Favicon Icons
        html.Append("<link rel=""icon"" type=""image/png"" sizes=""192x192"" href=""/android-icon-192x192.png""/>")
        html.Append("<link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""/favicon-32x32.png""/>")
        html.Append("<link rel=""icon"" type=""image/png"" sizes=""96x96"" href=""/favicon-96x96.png""/>")
        html.Append("<link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/favicon-16x16.png""/>")

        ' Web Manifest
        html.Append("<link rel=""manifest"" href=""/manifest.json""/>")

        ' Microsoft Specific
        html.Append("<meta name=""msapplication-TileColor"" content=""#ffffff""/>")
        html.Append("<meta name=""msapplication-TileImage"" content=""/ms-icon-144x144.png""/>")

        ' Theme Color
        html.Append("<meta name=""theme-color"" content=""#ffffff""/>")

        Return html.ToString()
    End Function

    Public Shared Function Header() As String   'All the header components that are common for all pages
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        html.Append("<div class=""header-top header-top-my mob"">")
        html.Append("<div class=""container col"">")
        html.Append("<div class='header-left'>")
        html.Append("<p class='header-kontakt header-text'>Kontaktirajte nas: <a href=""tel:+38763100100""><i class='fa fa-phone fa-2x fa-flip-horizontal wp-viber-header' aria-hidden='true'></i></a></p>")
        html.Append("<a href='https://wa.me/+38763100100' target='_blank'><img class='wp-vb-logo wp-viber wp-viber-header' src='/Datoteke/Logo/WhatsAppLogo.png' alt='WhatsApp' /></a>")
        html.Append("<a href='viber://chat?number=%2B+38763100100' target='_blank'><img class='wp-vb-logo wp-viber wp-viber-header' src='/Datoteke/Logo/ViberLogo.png' alt='Viber' /></a>")
        html.Append("<a href='mailto:info@rescuequip.ba' class='element-footer' target='_blank'><i class='fa fa-envelope fa-2x wp-viber-header' style='font-size: 20px; color: white;'></i></a>")
        html.Append("</div>") 'header-left
        html.Append("</div>") 'container
        html.Append("</div>") 'header-top

        'header-top
        html.Append("<div class=""header-top header-top-my"">")
        html.Append("<div class=""container"">")
        html.Append("<div class='header-left'>")
        html.Append("<p class='header-kontakt header-text'>Imate pitanja? Kontaktirajte nas: <a href=""tel:+38763100100"">+38763100100</a></p>")
        html.Append("<a href='https://wa.me/+38763100100' target='_blank'><img class='wp-vb-logo wp-viber wp-viber-header' src='/Datoteke/Logo/WhatsAppLogo.png' alt='WhatsApp' /></a>")
        html.Append("<a href='viber://chat?number=%2B+38763100100' target='_blank'><img class='wp-vb-logo wp-viber wp-viber-header' src='/Datoteke/Logo/ViberLogo.png' alt='Viber' /></a>")
        html.Append("<a href='mailto:info@rescuequip.ba' class='element-footer' target='_blank'><i class='fa fa-envelope fa-2x wp-viber-header' style='font-size: 20px; color: white;'></i></a>")
        html.Append("</div>") 'header-left

        html.Append("<div class='header-right'>")
        html.Append("<ul>")
        If HttpContext.Current.Session("ValjanUser") = False Then   'If user is not logged in
            html.Append("<li><a href='/login' class='header-text'>Registriraj se</a></li>")
            html.Append("<li><a href='/login' class='header-text'>Prijavi se</a></li>")
        Else
            'Ovdje prikazati korisnikovo ime
            html.Append("<li><a href='/login?a=logout'>Odjavi se</a></li>")
        End If
        html.Append("</ul>")
        html.Append("</div>") 'header-right
        html.Append("</div>") 'container
        html.Append("</div>") 'header-top

        html.Append("<div class=""header-middle sticky-header"">")
        html.Append("<div class=""container"">")

        html.Append("<div class=""header-left"">")
        html.Append("<button class=""mobile-menu-toggler"" type=""button"">")
        html.Append("<i class=""icon-menu""></i>")
        html.Append("</button>")
        html.Append("<a href=""/"" class=""logo logo-rescue"">")
        html.Append("<img src=""/Datoteke/Logo/RescueEquip-logoDark.png"" alt=""SafeEquip""/>")
        html.Append("</a>")
        html.Append("<nav class=""main-nav"">")
        html.Append("<ul class=""menu"">")
        html.Append("<li class='kat-div'><a href=""/"" class='istaknute-kat istaknute-kat-header'>Početna</a></li>") 'Početna

        Dim KupacLogiran As Boolean
        Dim NivoTrenutnogKupca As String
        If HttpContext.Current.Session("ValjanUser") = True Then
            KupacLogiran = HttpContext.Current.Session("ValjanUser")
        Else
            KupacLogiran = False
            NivoTrenutnogKupca = "0"
        End If

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT ID, NadGrupa FROM ArtikliNadGrupe WHERE Aktivno='1' ORDER BY Prioritet"

                'Ispis Kategorija u Header 
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            Dim NadGrupaID As Integer = Convert.ToInt32(citac("ID"))
                            html.Append("<li class='sf-with-ul kat-div'>")
                            html.AppendFormat("<a href=""/grupa/{0}/{1}/"" class='istaknute-kat kat-div istaknute-kat-header'>{2}</a>", citac("ID"), SrediNaziv(citac("NadGrupa")), citac("NadGrupa"))
                            html.Append("<ul>") 'menu

                            'Ispis PodKategorija unutar Kategorija
                            Using konekcijaGrupe As New SqlConnection(putanja)
                                konekcijaGrupe.Open()
                                Using komandaGrupe As New SqlCommand()
                                    komandaGrupe.Connection = konekcijaGrupe
                                    komandaGrupe.CommandType = CommandType.Text
                                    komandaGrupe.CommandText = "SELECT * FROM ArtikliGrupe WHERE NadGrupaID=@ID AND Aktivno='1'"
                                    komandaGrupe.Parameters.AddWithValue("@ID", citac("ID"))

                                    Using citacGrupe As SqlDataReader = komandaGrupe.ExecuteReader()
                                        If citacGrupe IsNot Nothing Then
                                            While citacGrupe.Read()
                                                html.AppendFormat("<li class=''><a href=""/podgrupa/{0}/{1}/"" class=""istaknute-kat kat-div"">{2}</a></li>", citacGrupe("ID"), SrediNaziv(citacGrupe("Grupa")), citacGrupe("Grupa"))
                                            End While
                                        End If
                                    End Using
                                End Using
                            End Using
                            html.Append("</ul>")
                            html.Append("</li>")
                        End While
                    End If
                End Using
            End Using
        End Using

        html.Append("</ul>") ' menu
        html.Append("</nav>")
        html.Append("</div>") ' header-left

        html.Append("<div class=""header-right"">")
        html.Append("<div class=""header-search"">")
        html.Append("<a href=""#"" class=""search-toggle"" role=""button""><i class=""icon-search-3""></i></a>")
        html.Append("<form action=""/pretraga"" method=""get"">")
        html.Append("<div class=""header-search-wrapper"">")
        html.Append("<input type=""search"" class=""form-control"" name=""pojam"" id=""q"" placeholder=""Unesite pojam za pretragu..."" autocomplete='off'>")
        html.Append("<button class=""btn btn-search"" type=""submit""><i class=""icon-search-3""></i></button>")
        html.Append("</div>") ' header-search-wrapper
        html.Append("</form>")
        html.Append("</div>") ' header-search
        html.Append("<a href='/login' class='top-my-account'><i class='icon-user user-icons'></i></a>")

        html.Append("<div class=""dropdown cart-dropdown kosarica-header"">")
        html.Append("<a href=""/kosarica"" class=""dropdown-toggle"" role=""button"" aria-haspopup=""true"" aria-expanded=""false"" data-display=""static"">")
        html.Append("<i class=""minicart-icon""></i>")
        html.AppendFormat("<span class='cart-count spnKol'>{0}</span>", PrebrojiArtikle())
        html.Append("</a>")
        html.Append("</div>") ' dropdown cart-dropdown

        html.Append("</div>") ' header-right

        html.Append("</div>") ' container
        html.Append("</div>") ' header-middle sticky-header

        html.Append("<div class=""header-bottom bottom-header"">")
        html.Append("<div class=""col-lg-4"">")
        html.Append("<div class=""service-widget"">")
        html.Append("<i class=""service-icon icon-shipping""></i>")
        html.Append("<div class=""service-content"">")
        html.Append("<h3 class=""service-title"">BRZA DOSTAVA</h3>")
        html.Append("</div>") ' service-content
        html.Append("</div>") ' service-widget
        html.Append("</div>") ' col-lg-4

        html.Append("<div class=""col-lg-4"">")
        html.Append("<div class=""service-widget"">")
        html.Append("<i class=""service-icon icon-money""></i>")
        html.Append("<div class=""service-content"">")
        html.Append("<h3 class=""service-title"">Jamstvo povrata novca</h3>")
        html.Append("</div>") ' service-content
        html.Append("</div>") ' service-widget
        html.Append("</div>") ' col-lg-4

        html.Append("<div class=""col-lg-4"">")
        html.Append("<div class=""service-widget"">")
        html.Append("<i class=""service-icon icon-support""></i>")
        html.Append("<div class=""service-content"">")
        html.Append("<h3 class=""service-title"">Online podrška 24/7</h3>")
        html.Append("</div>") ' service-content
        html.Append("</div>") ' service-widget
        html.Append("</div>") ' col-lg-4

        html.Append("</div>") ' header-bottom bottom-header

        Return html.ToString()
    End Function

    Public Shared Function Footer() As String   'Function for footer display
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        html.Append("<footer class='footer'>")
        html.Append("<div class='footer-middle footer-middle-my'>")
        html.Append("<div class='container'>")
        html.Append("<div class='row row-sm'>")
        'Logo
        html.Append("<div class='col-md-6 col-lg-4'>")
        html.Append("<a href='/'><img src='/Datoteke/Logo/RescueEquip-logoDark.png' id='footer-logo'/></a>")
        html.Append("</div>") 'col-md-6 col-lg-4
        'Kontakt
        html.Append("<div class='col-md-6 col-lg-2'>")
        html.Append("<div class='widget'>")
        html.Append("<h3 class='widget-title naslov-footer'>Povežite se s nama</h3>")
        html.Append("<div class='widget-content row row-sm'>")
        html.Append("<ul class='col-xl-12'>")
        html.Append("<li><i class='fa fa-envelope' style='font-size: 20px;'></i><a href='mailto:info@rescuequip.ba' class='element-footer' target='_blank'>&nbsp;&nbsp;info@rescuequip.ba</a></li>")
        html.Append("<li><i class='fa fa-phone' style='font-size: 20px;'></i><a href='tel:+38763100100' class='element-footer' target='_blank'>&nbsp;&nbsp; +38763100100</a></li>")
        html.Append("<h3 class='widget-title naslov-footer'>Whatsapp i Viber</h3>")
        html.Append("<li class='whatsapp-item'><img class='wp-vb-logo wp-viber' src='/Datoteke/Logo/WhatsAppLogo.png'/><a href='https://wa.me/38763100100' class='element-footer' target='_blank'>&nbsp;&nbsp;+38763100100</a></li>")
        html.Append("<li class='whatsapp-item'><img class='wp-vb-logo wp-viber' src='/Datoteke/Logo/ViberLogo.png'/><a href='viber://chat?number=%2B38763100100' class='element-footer' target='_blank'>&nbsp;&nbsp;+38763100100</a></li>")
        html.Append("</ul>") 'col-xl-12
        html.Append("</div>") 'widget-content row row-sm
        html.Append("</div>") 'widget
        html.Append("</div>") 'col-md-6 col-lg-2
        'Informacije
        html.Append("<div class='col-md-6 col-lg-2'>")
        html.Append("<div class='widget'>")
        html.Append(" <h3 class='widget-title naslov-footer'>Informacije</h3>")
        html.Append("<div class='widget-content row row-sm'>")
        html.Append("<ul class='col-xl-6'>")
        'html.Append("<li><a href='/onama' class='element-footer'>O Nama</a></li>")
        html.Append("<li><a href='/kontakt' class='element-footer'>Kontakt</a></li>")
        html.Append("</ul>") 'col-xl-6
        html.Append("</div>") 'widget-content row row-sm
        html.Append("</div>") 'widget
        html.Append("</div>") 'col-md-6 col-lg-2

        html.Append("<div class='footer-bottom container'>")
        html.Append("<p>Copyright © <p href='/' style='color:  #4b643b;'>Zvonimir Kožul & Leonardo Misir </p></p>")
        html.Append("</div>") 'footer-bottom container
        html.Append("</footer>")
        html.Append("</footer>")
        Return html.ToString()
    End Function

    Public Shared Function HeaderMobile() As String 'Mobile menu for header
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        html.Append("<div class='mobile-menu-wrapper'>")
        html.Append("<div class='mobile-menu-wrapper'>")
        html.Append("<span class='mobile-menu-close'><i class='icon-retweet'></i></span>")
        html.Append("<nav class='mobile-nav'>")
        html.Append("<ul class='mobile-menu'>")
        html.Append("<li><a href='/' class='mobile-element'>Početna</a></li>")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT TOP 9 ID, NadGrupa FROM ArtikliNadGrupe WHERE Aktivno='1'"

                'Ispis Kategorija u Header
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            Dim NadGrupaID As Integer = Convert.ToInt32(citac("ID"))

                            html.Append("<li>")
                            html.AppendFormat("<a href=""/grupa/{0}/{1}/"" class='mobile-element'>{2}</a>", citac("ID"), SrediNaziv(citac("NadGrupa")), citac("NadGrupa"))
                            html.Append("<ul>")

                            'Ispis PodKategorija unutar Grupe
                            Using konekcijaGrupe As New SqlConnection(putanja)
                                konekcijaGrupe.Open()
                                Using komandaGrupe As New SqlCommand()
                                    komandaGrupe.Connection = konekcijaGrupe
                                    komandaGrupe.CommandType = CommandType.Text
                                    komandaGrupe.CommandText = "SELECT * FROM ArtikliGrupe WHERE NadGrupaID=@ID AND Aktivno='1'"
                                    komandaGrupe.Parameters.AddWithValue("@ID", citac("ID"))

                                    Using citacGrupe As SqlDataReader = komandaGrupe.ExecuteReader()
                                        If citacGrupe IsNot Nothing Then
                                            While citacGrupe.Read()
                                                html.AppendFormat("<li class=''><a href=""/podgrupa/{0}/{1}/"" class='mobile-element-podgrupa'>{2}</a></li>", citacGrupe("ID"), SrediNaziv(citacGrupe("Grupa")), citacGrupe("Grupa"))
                                            End While
                                        End If
                                    End Using
                                End Using
                            End Using
                            html.Append("</ul>")
                            html.Append("</li>")
                        End While
                    End If
                End Using
            End Using
        End Using

        html.Append("<li><a href='/kontakt' class='mobile-element'>Kontakt</a></li>")

        ' Zatvori ul i nav tagove
        html.Append("</ul>") ' mobile-menu
        html.Append("</nav>") ' mobile-nav
        html.Append("</div>") ' mobile-menu-wrapper
        html.Append("</div>") ' mobile-menu-wrapper

        Return html.ToString()
    End Function

    Public Shared Function DodajUKosaricu() As String   'Modal for adding product to cart
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            html.Append("<div class='modal fade' id='addCartModal' tabindex='-1' role='dialog' aria-labelledby='addCartModal' aria-hidden='true'>")
            html.Append("<div class='modal-dialog' role='document'>")
            html.Append("<div class='modal-content'>")
            html.Append("<div class='modal-body add-cart-box text-center'>")
            html.Append("<p>Dodali ste ovaj proizvod u korpu!</p>")
            html.Append("<h4 id='productTitle'></h4>")
            html.Append("<img src='/' id='productImage' width='100' height='100' alt='adding cart image'/>")
            html.Append("<div class='btn-actions'>")
            html.Append("<a href='/kosarica'>")
            html.Append("<button class='btn-primary'>Idite na košaricu!</button></a>")
            html.Append("<a href=''>")
            html.Append("<button class='btn-primary' data-dismiss='modal'>Nastavite!</button></a>")
            html.Append("</div>") 'btn-actions
            html.Append("</div>") 'modal-body add-cart-box text-center
            html.Append("</div>") 'modal-content
            html.Append("</div>") 'modal-dialog
            html.Append("</div>") 'modal fade
        End Using
        Return html.ToString()
    End Function

    Public Shared Function FooterScript() As String 'Footer scripts and links
        Dim html As New StringBuilder

        html.Append("<script src='/assets/js/jquery.min.js'></script>")
        html.Append("<script src='/assets/js/bootstrap.bundle.min.js'></script>")
        html.Append("<script src='/assets/js/plugins.min.js'></script>")
        html.Append("<script src='/assets/js/plugins/isotope-docs.min.js'></script>")
        html.Append(" <script src='/assets/js/main.js'></script>")

        html.Append("<script src='/JS/Script.js'></script>")
        Return html.ToString()
    End Function

    Public Shared Function SrediNaziv(Naziv As String) As String    'Function to format product names for URLs
        Dim putanja As String = SQLKonekcija()
        Naziv = Naziv.ToLower
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Znakovi"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            Naziv = Naziv.Replace(citac("Znak"), citac("NoviZnak"))
                        End While
                    End If
                End Using
            End Using
        End Using
        Naziv = Naziv.Replace(" ", "-")
        Naziv = Naziv.Replace("/", "")
        Naziv = Naziv.Replace("--", "-")
        If Right(Naziv, "1") = "-" Then
            Naziv = Naziv.Substring(0, Naziv.Length - 1)
        End If
        Return Naziv
    End Function

    Public Shared Function ZadanaSlikaArtikla(ArtikalID As Integer) As String   'Function to get the default image of an article
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "ZadanaSlikaArtikla"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Datoteka"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function PrebrojiArtikle() As String  'Function to count the number of articles in the cart
        Dim html As New StringBuilder()

        If IsNothing(HttpContext.Current.Session("Narudzba")) = False Then
            Dim n As Narudzba
            n = CType(HttpContext.Current.Session("Narudzba"), Narudzba)
            Dim brojArtikala = n.BrojArtikala.ToString()
            'html.Append("(" & brojArtikala & ")")
            html.Append(brojArtikala)
        Else
            html.AppendFormat("{0}", "0")
        End If

        Return html.ToString()
    End Function

    Public Shared Function Postavke(Naziv As String) As String
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Postavke WHERE Naziv=@Naziv"
                komanda.Parameters.AddWithValue("@Naziv", Naziv)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Vrijednost"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function ProvjeriKosaricuDaLiImaBesplatneDostave(n As Narudzba) As Boolean
        Dim html As New StringBuilder

        For Each a As ArtikalSession In n.Artikli
            If a.besplatnadostava = False Then
                html.Append(False)
                Exit For
            End If
        Next

        If html.ToString = "" Then
            html.Append(True)
        End If

        Return html.ToString()
    End Function

    Public Shared Function Suma(NarudzbaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()

        On Error Resume Next

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "SumaNarudzbe"
                komanda.Parameters.AddWithValue("@Narudzba", NarudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", Format(citac("Iznos"), "N2"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function NivoLogiranogKorisnika() As String
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT AdminLevel FROM Korisnici WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", LogiraniKorisnikID())
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("AdminLevel"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append("10")
        End If

        Return html.ToString()
    End Function

    Public Shared Function ikonaDatoteke(Datoteka As String, OrgNazivDatoteke As String) As String
        Dim html As New StringBuilder()

        If Right(Datoteka, "3") = "pdf" Then
            html.Append("<img src=""/Datoteke/Icone/pdf.png"" style=""width:50px;"" alt=""pdf"" title=""" & OrgNazivDatoteke & """/>")
        Else
            If Right(Datoteka, "3") = "avi" Then
                html.Append("<img src=""/Datoteke/Icone/avi.png"" style=""width:50px;"" alt=""avi"" title=""" & OrgNazivDatoteke & """/>")
            Else
                If Right(Datoteka, "3") = "doc" Or Right(Datoteka, "4") = "docx" Then
                    html.Append("<img src=""/Datoteke/Icone/doc.png"" style=""width:50px;"" alt=""doc"" title=""" & OrgNazivDatoteke & """/>")
                Else
                    If Right(Datoteka, "3") = "jpg" Or Right(Datoteka, "4") = "jpeg" Then
                        html.Append("<img src=""/Datoteke/Icone/jpeg.png"" style=""width:50px;"" alt=""jpeg"" title=""" & OrgNazivDatoteke & """/>")
                    Else
                        If Right(Datoteka, "3") = "mp3" Then
                            html.Append("<img src=""/Datoteke/Icone/mp3.png"" style=""width:50px;"" alt=""mp3"" title=""" & OrgNazivDatoteke & """/>")
                        Else
                            If Right(Datoteka, "3") = "png" Then
                                html.Append("<img src=""/Datoteke/Icone/png.png"" style=""width:50px;"" alt=""png"" title=""" & OrgNazivDatoteke & """/>")
                            Else
                                If Right(Datoteka, "3") = "rar" Then
                                    html.Append("<img src=""/Datoteke/Icone/rar.png"" style=""width:50px;"" alt=""rar"" title=""" & OrgNazivDatoteke & """/>")
                                Else
                                    If Right(Datoteka, "3") = "zip" Then
                                        html.Append("<img src=""/Datoteke/Icone/zip.png"" style=""width:50px;"" alt=""zip"" title=""" & OrgNazivDatoteke & """/>")
                                    Else
                                        If Right(Datoteka, "3") = "xls" Or Right(Datoteka, "4") = "xlsx" Then
                                            html.Append("<img src=""/Datoteke/Icone/xls.png"" style=""width:50px;"" alt=""xls"" title=""" & OrgNazivDatoteke & """/>")
                                        Else
                                            html.Append("<img src=""/Datoteke/Icone/file.png"" style=""width:50px;"" alt=""file"" title=""" & OrgNazivDatoteke & """/>")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Return html.ToString()
    End Function

    Public Shared Function SumaNarudzbe(narudzbaID As Integer) As Decimal
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()

        'On Error Resume Next

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "SumaNarudzbe"
                komanda.Parameters.AddWithValue("@Narudzba", narudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Iznos"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString()
    End Function

    '----------------------------------------------------------------------------------------------> General functions END <-------------------------------------------------------------------------------------------


    '---------------------------------------------------------------------------------------------> Default.aspx START <------------------------------------------------------------------------------------------------

    Public Shared Function Slider() As String   'Banner slider on Default
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()
        'loop
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Slider WHERE Aktivno='1' ORDER BY Prioritet ASC"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<div class='home-slide slike-slider'>")
                            html.Append("<img src='/Datoteke/Slider/" & citac("Slika") & "' class='slider-img web'/>")
                            html.Append("<img src='/Datoteke/Slider/" & citac("SlikaMob") & "' class='slider-img mob'/>")
                            html.Append("</div>") ' home-slide
                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString()
    End Function

    Public Shared Function IstaknutiProizvodi() As String   'Istaknuti proizvodi na Default
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Dim KupacLogiran As Boolean
        Dim NivoTrenutnogKupca As String
        If HttpContext.Current.Session("ValjanUser") = True Then
            KupacLogiran = HttpContext.Current.Session("ValjanUser")
        Else
            KupacLogiran = False
            NivoTrenutnogKupca = "0"
        End If

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "IstaknutiProizvodi"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()

                            'Istaknuti proizvod
                            html.AppendFormat("<div class='col-6 col-md-4 col-xl-5col div{0}'>", citac("ID"))
                            html.Append("<div class='product-default inner-quickview inner-icon shadow'>")
                            html.Append("<figure>")

                            Dim slika As String = ZadanaSlikaArtikla(citac("ID"))
                            html.AppendFormat("<a href=""/artikal/{1}/{0}/"">", SrediNaziv(citac("Naziv")), citac("ID"))
                            html.AppendFormat("<img src='/Datoteke/SlikeArtikala/{0}' class='{1}' alt='{1}'/>", citac("Slika"), citac("ID"))


                            html.Append("<div class='btn-icon-group dugmicDodaj-artikal'>")
                            html.AppendFormat("<input type=""hidden"" class=""qty {0}"" value=""1"">", citac("ID"))
                            html.AppendFormat("<button type='button' class='btn-icon btn-add-cart dugmicDodaj ' data-toggle='modal' data-id=""{0}"" data-target='#addCartModal' title=""Dodaj u košaricu""><i class='icon-bag'></i></button>", citac("ID"))
                            html.Append("</div>") 'btn-icon-group
                            html.Append("</figure>")
                            html.Append("<div class='product-details'>")

                            Dim NazivNadGrupe As String = Komponente.PronadjiNazivNadGrupe(citac("NadGrupaID"))

                            html.Append("<div class='category-wrap'>")
                            html.Append(" <div class='category-list'>")

                            html.AppendFormat("<a href=""/grupa/{0}/{1}/"" class='product-category nadgrupa'>{2}</a>", citac("NadgrupaId"), SrediNaziv(NazivNadGrupe), NazivNadGrupe)

                            html.Append("</div>") 'category-list
                            html.Append("</div>") 'category-wrap
                            html.Append("<h2 class='product-title naslov-istaknuto'>")
                            html.AppendFormat("<a href='/artikal/{1}/{0}/' title='{2}'>{2}</a>", SrediNaziv(citac("Naziv")), citac("ID"), citac("Naziv"))
                            html.Append("</h2>")


                            'Cijena
                            html.Append("<div class='price-box cijene-istaknuto'>")
                            Dim cijenaSaKalkulacijom As Decimal = citac("Cijena") * (100 + citac("Procenat")) / 100
                            Dim akcijskaCijena As Decimal = citac("AkcijaCijena")

                            If citac("Cijena") < 1 Then
                                html.Append("<span class='product-price nova-cijena-istaknuto'>Cijena na upit</span>")
                            Else
                                If akcijskaCijena > 0 Then
                                    'Ima akcijsku cijenu -> prikaži staru i novu cijenu
                                    html.AppendFormat("<span class='old-price'>{0} KM</span>", Format(akcijskaCijena, "N2"))
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                Else
                                    ' Nema akcije -> prikaži samo redovnu cijenu
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                End If
                            End If
                            html.Append("</div>") 'price-box

                            html.Append("</div>") 'product-details
                            html.Append("</div>") 'product-default inner-quickview inner-icon
                            html.Append("</div>") 'col-6 col-md-4 col-xl-5col
                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString()
    End Function

    Public Shared Function NajNovije() As String    'Novo iz ponude na Default
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()


        Dim KupacLogiran As Boolean
        Dim NivoTrenutnogKupca As String
        If HttpContext.Current.Session("ValjanUser") = True Then
            KupacLogiran = HttpContext.Current.Session("ValjanUser")
        Else
            KupacLogiran = False
            NivoTrenutnogKupca = "0"
        End If

        html.Append("<section class='product-panel'>")
        html.Append("<div class='section-title'>")
        html.Append("<h2>Novo iz ponude</h2>")
        html.Append("</div>") 'section-title
        html.Append("<div class='owl-carousel owl-theme' data-toggle='owl' data-owl-options='{""margin"": 4,""items"": 2,""autoplayTimeout"": 5000, ""dots"": false,""nav"" : true,""responsive"": {""768"": {""items"": 3},""992"" : {""items"" : 4},""1200"": {""items"": 5}}}'>")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM (SELECT TOP 20 * FROM dbo.Artikli WHERE Aktivno='1' AND Kolicina>'0' ORDER BY ID DESC ) AS Last20 ORDER BY NEWID();"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()

                            html.AppendFormat("<div class='product-default inner-quickview inner-icon center-details div{0}'>", citac("ID"))
                            If citac("BesplatnaDostava") = True Then
                                html.AppendFormat("<div class=""free-shipping""></div>")
                            End If
                            html.Append("<figure>")
                            html.AppendFormat("<a href=""/artikal/{1}/{0}/"">", SrediNaziv(citac("Naziv")), citac("ID"))
                            'Slika artikla
                            Dim slika As String = ZadanaSlikaArtikla(citac("ID"))
                            If slika.Contains("http") = False Then 'Or slika.Contains("http://") = False Then
                                html.AppendFormat("<img class=""product-image-photo"" src=""/Thumb2.ashx?i={0}"" alt=""{1}"">", ZadanaSlikaArtikla(citac("ID")), citac("Naziv"))
                            Else
                                html.AppendFormat("<img class=""product-image-photo"" src=""{0}"" alt=""{1}"">", ZadanaSlikaArtikla(citac("ID")), citac("Naziv"))
                            End If
                            html.Append("</a>") '/artikal

                            html.Append("<div class='btn-icon-group'>")
                            html.AppendFormat("<input type=""hidden"" class=""qty {0}"" value=""1"">", citac("ID"))
                            html.AppendFormat("<button type='button' class='btn-icon btn-add-cart dugmicDodaj' data-toggle='modal' data-id=""{0}"" data-target='#addCartModal' title=""Dodaj u košaricu""><i class='icon-bag'></i></button>", citac("ID"))
                            html.Append("</div>") 'btn-icon-group
                            html.Append("</figure>")
                            html.Append("<div class='product-details'>")

                            Dim NazivNadGrupe As String = Komponente.PronadjiNazivNadGrupe(citac("NadGrupaID"))

                            html.Append("<div class='category-wrap'>")
                            html.Append("<div class='category-list'>")
                            html.AppendFormat("<a href=""/grupa/{0}/{1}/"" class='product-category'>{2}</a>", citac("NadgrupaId"), SrediNaziv(NazivNadGrupe), NazivNadGrupe)

                            html.Append("</div>") 'category-list
                            html.Append("</div>") 'category-wrap
                            html.Append("<h2 class='product-title naziv-najnovijih'>")
                            html.AppendFormat("<a href=""/artikal/{1}/{0}/"">", SrediNaziv(citac("Naziv")), citac("ID"))
                            html.AppendFormat("<a href='/artikal/{1}/{0}/' class='naziv-najnovijih' title='{2}'>{2}</a>", SrediNaziv(citac("Naziv")), citac("ID"), citac("Naziv"))
                            html.Append("</h2>") 'product-title

                            'Cijena
                            html.Append("<div class='price-box'>")
                            Dim cijenaSaKalkulacijom As Decimal = citac("Cijena") * (100 + citac("Procenat")) / 100
                            Dim akcijskaCijena As Decimal = citac("AkcijaCijena")

                            If citac("Cijena") < 1 Then
                                html.Append("<span class='product-price'>Cijena na upit</span>")
                            Else
                                If akcijskaCijena > 0 Then
                                    ' Prikaz akcijske i stare cijene
                                    html.AppendFormat("<span class='old-price'>{0} KM</span>", Format(akcijskaCijena, "N2"))
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                Else
                                    ' Prikaz samo redovne cijene
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                End If
                            End If
                            html.Append("</div>") 'price-box

                            html.Append("</div>") 'product-detail
                            html.Append("</div>") 'product-default inner-quickview inner-icon center-details
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</div>") 'section-title
        html.Append("</section>") 'product-panel

        Return html.ToString()
    End Function

    '----------------------------------------------------------------------------------------------> Default.aspx END <------------------------------------------------------------------------------------------------

    '---------------------------------------------------------------------------------------------> ArtikliGrupe.aspx START <---------------------------------------------------------------------------------------------
    Public Shared Function ArtikliGrupee(GrupaID As Integer) As String
        Return ArtikliGrupe(1, GrupaID, "NazivAsc")
    End Function
    Public Shared Function ArtikliNadGrupe(KategorijaID As Integer) As String   'Function for displaying articles in a group
        Return ArtikliNadGrupe(1, KategorijaID, "NazivAsc")
    End Function

    Public Shared Function ArtikliNadGrupe(stranica As Integer, NadGrupaID As Integer, raspored As String) As String    'Function for displaying articles in a group with pagination and sorting
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Dim KupacLogiran As Boolean
        Dim NivoTrenutnogKupca As String
        If HttpContext.Current.Session("ValjanUser") = True Then
            KupacLogiran = HttpContext.Current.Session("ValjanUser")
        Else
            KupacLogiran = False
            NivoTrenutnogKupca = "0"
        End If


        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "OdaberiRasponKat"
                If raspored = "" Or raspored = "NazivAsc" Then
                    komanda.CommandText = "OdaberiRasponKat"
                Else
                    If raspored = "NazivDesc" Then
                        komanda.CommandText = "OdaberiRasponKatNazivDesc"
                    Else
                        If raspored = "CijenaAsc" Then
                            komanda.CommandText = "OdaberiRasponKatCijenaAsc"
                        Else
                            If raspored = "CijenaDesc" Then
                                komanda.CommandText = "OdaberiRasponKatCijenaDesc"
                            End If
                        End If
                    End If
                End If
                komanda.Parameters.AddWithValue("@NadGrupaID", NadGrupaID)
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()

                            'Proizvod
                            html.AppendFormat("<div class='col-6 col-md-4 col-xl-3 div{0}'>", citac("ID"))
                            If citac("BesplatnaDostava") = True Then
                                html.AppendFormat("<div class=""free-shipping""></div>")
                            End If
                            html.Append("<div class='product-default inner-quickview inner-icon'>")
                            html.Append("<figure>")
                            html.AppendFormat("<a href='/artikal/{1}/{0}/'>", SrediNaziv(citac("Naziv")), citac("ID"))

                            html.AppendFormat("<img src='/Datoteke/SlikeArtikala/{0}' class='{1}' alt='{1}'/>", citac("Slika"), citac("ID"))


                            html.Append("</a>") '/artikal/{0}
                            html.Append("<div class='btn-icon-group'>")
                            html.AppendFormat("<input type=""hidden"" class=""qty {0}"" value=""1"">", citac("ID"))
                            html.AppendFormat("<button type='button' class='btn-icon btn-add-cart dugmicDodaj' data-toggle='modal' data-id=""{0}"" data-target='#addCartModal' title=""Dodaj u košaricu""><i class='icon-bag'></i></button>", citac("ID"))
                            html.Append("</div>") 'btn-icon-group
                            html.Append("</figure>")

                            Dim NazivNadGrupe As String = Komponente.PronadjiNazivNadGrupe(citac("NadGrupaID"))

                            html.Append("<div class='product-details'>")
                            html.Append("<div class='category-wrap'>")
                            html.Append(" <div class='category-list'>")
                            html.Append("</div>") 'category-list
                            html.Append("</div>") 'category-wrap

                            'Naziv proizvoda
                            html.Append("<h2 class='product-title'>")

                            html.AppendFormat("<a href='/artikal/{1}/{0}/' title='{2}'>{2}</a>", SrediNaziv(citac("Naziv")), citac("ID"), citac("Naziv"))
                            html.Append("</h2>") 'product-title

                            'Cijena
                            html.Append("<div class='price-box'>")
                            Dim cijenaSaKalkulacijom As Decimal = citac("Cijena") * (100 + citac("Procenat")) / 100
                            Dim akcijskaCijena As Decimal = citac("AkcijaCijena")

                            If citac("Cijena") < 1 Then
                                html.Append("<span class='product-price'>Cijena na upit</span>")
                            Else
                                If akcijskaCijena > 0 Then
                                    ' Prikaz akcijske i stare cijene
                                    html.AppendFormat("<span class='old-price cijene-istaknuto'>{0} KM</span>", Format(akcijskaCijena, "N2"))
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                Else
                                    ' Prikaz samo redovne cijene
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                End If
                            End If
                            html.Append("</div>") 'price-box

                            html.Append("</div>") 'product-details
                            html.Append("</div>") 'product-default inner-quickview inner-icon
                            html.Append("</div>") 'col-6 col-md-4 col-xl-3

                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function PronadjiNazivNadGrupe(NadGrupaID As Integer) As String   'Function to find the name of the main group
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT NadGrupa FROM ArtikliNadGrupe WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", NadGrupaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat(citac("NadGrupa"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    '---------------------------------------------------------------------------------------------> ArtikliGrupe.aspx END <---------------------------------------------------------------------------------------------


    '---------------------------------------------------------------------------------------------> ArtikliPodGrupe.aspx START <---------------------------------------------------------------------------------------------

    Public Shared Function PronadjiIdNadGrupe(GrupaID As Integer) As String 'Function to find the ID of the main group based on the subgroup ID
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT NadGrupaID FROM ArtikliGrupe WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", GrupaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat(citac("NadGrupaID"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function PronadjiNazivGrupe(GrupaID As Integer) As String 'Function to find the name of the subgroup based on its ID
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Grupa FROM ArtikliGrupe WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", GrupaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat(citac("Grupa"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function


    Public Shared Function ArtikliGrupe(GrupaID As Integer) As String
        Return ArtikliGrupe(1, GrupaID, "NazivAsc")
    End Function

    Public Shared Function ArtikliGrupe(stranica As Integer, GrupaID As Integer, raspored As String) As String
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        ' Provjera URL-a i parsiranje GrupaID iz path-a ako postoji
        Dim segments() As String = HttpContext.Current.Request.Path.Split("/"c)
        If segments.Length > 2 Then
            Dim tempID As Integer
            If Integer.TryParse(segments(2), tempID) Then
                GrupaID = tempID
            End If
        End If

        Dim NadGrupaID As Integer = PronadjiIdNadGrupe(GrupaID)

        ' Provjera je li kupac logiran
        Dim KupacLogiran As Boolean
        Dim NivoTrenutnogKupca As String
        If HttpContext.Current.Session("ValjanUser") IsNot Nothing AndAlso HttpContext.Current.Session("ValjanUser") = True Then
            KupacLogiran = True
        Else
            KupacLogiran = False
            NivoTrenutnogKupca = "0"
        End If

        '' Ograničenje pristupa određenim nadgrupama
        'If KupacLogiran = True And NivoTrenutnogKupca = "9" And (NadGrupaID = 7 Or NadGrupaID = 8) Then
        '    HttpContext.Current.Response.Redirect("/")
        'End If

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure

                ' Odabir Stored Procedure prema rasporedu
                Select Case raspored
                    Case "", "NazivAsc"
                        komanda.CommandText = "OdaberiRasponGrupeNazivAsc"
                    Case "NazivDesc"
                        komanda.CommandText = "OdaberiRasponGrupeNazivDesc"
                    Case "CijenaAsc"
                        komanda.CommandText = "OdaberiRasponGrupeCijenaAsc"
                    Case "CijenaDesc"
                        komanda.CommandText = "OdaberiRasponGrupeCijenaDesc"
                End Select

                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@GrupaID", GrupaID)

                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            'Proizvod
                            html.AppendFormat("<div class='col-6 col-md-4 col-xl-3 div{0}'>", citac("ID"))
                            If citac("BesplatnaDostava") = True Then
                                html.AppendFormat("<div class=""free-shipping""></div>")
                            End If
                            html.Append("<div class='product-default inner-quickview inner-icon'>")
                            html.Append("<figure>")
                            html.AppendFormat("<a href=""/artikal/{1}/{0}/"">", SrediNaziv(citac("Naziv")), citac("ID"))

                            html.AppendFormat("<img src='/Datoteke/SlikeArtikala/{0}' class='{1}' alt='{1}'/>", citac("Slika"), citac("ID"))

                            html.Append("</a>")
                            html.Append("<div class='btn-icon-group'>")
                            html.AppendFormat("<input type=""hidden"" class=""qty {0}"" value=""1"">", citac("ID"))
                            html.AppendFormat("<button type='button' class='btn-icon btn-add-cart dugmicDodaj' data-toggle='modal' data-id=""{0}"" data-target='#addCartModal' title=""Dodaj u košaricu""><i class='icon-bag'></i></button>", citac("ID"))
                            html.Append("</div>") 'btn-icon-group
                            html.Append("</figure>")
                            Dim NazivGrupe As String = Komponente.PronadjiNazivGrupe(citac("GrupaID"))
                            html.Append("<div class='product-details'>")
                            html.Append("<div class='category-wrap'>")
                            html.Append("<div class='category-list'>")
                            html.Append("</div>") 'category-list
                            html.Append("</div>") 'category-wrap
                            'Naziv proizvoda
                            html.Append("<h2 class='product-title'>")
                            html.AppendFormat("<a href='/artikal/{1}/{0}/' title='{2}'>{2}</a>", SrediNaziv(citac("Naziv")), citac("ID"), citac("Naziv"))
                            html.Append("</h2>") 'product-title
                            'Cijena proizvoda
                            html.Append("<div class='price-box'>")
                            Dim cijenaSaKalkulacijom As Decimal = citac("Cijena") * (100 + citac("Procenat")) / 100
                            Dim akcijskaCijena As Decimal = citac("AkcijaCijena")

                            If citac("Cijena") < 1 Then
                                html.Append("<span class='product-price'>Cijena na upit</span>")
                            Else
                                If akcijskaCijena > 0 Then
                                    html.AppendFormat("<span class='old-price cijene-istaknuto'>{0} KM</span>", Format(akcijskaCijena, "N2"))
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                Else
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                End If
                            End If
                            html.Append("</div>") 'price-box
                            html.Append("</div>") 'product-details
                            html.Append("</div>") 'product-default inner-quickview inner-icon
                            html.Append("</div>") 'col
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function


    '---------------------------------------------------------------------------------------------> ArtikliPodGrupe.aspx END <---------------------------------------------------------------------------------------------


    Public Shared Function LogiraniKorisnikID() As Integer
        If HttpContext.Current.Request.Cookies("logiraniKorisnikID") Is Nothing Then
            HttpContext.Current.Response.Cookies("logiraniKorisnikID").Value = "0"
        End If

        Dim idStr As String = HttpContext.Current.Request.Cookies("logiraniKorisnikID").Value.ToString()
        Dim idKorisnika As Integer

        If Integer.TryParse(idStr, idKorisnika) Then
            Return idKorisnika
        Else
            Return 0
        End If
    End Function


    Public Shared Function NazivArtikal(ArtikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Naziv FROM Artikli WHERE ID=@ArtikalID"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append(citac("Naziv"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function


    Public Shared Function CijenaArtika(ArtikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()
        Dim KupacLogiran As Boolean = HttpContext.Current.Session("ValjanUser")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Cijena, AkcijaCijena, Procenat FROM Artikli WHERE ID=@ArtikalID"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()

                            html.Append(Format(citac("Cijena"), "N2"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function


    Public Shared Function MojaKosaricaSession(n As Narudzba) As String 'moj originalnii
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()
        Dim CijenaDostava As Decimal = 8
        Dim Valuta As String = "KM"
        Dim KupacLogiran As Boolean = HttpContext.Current.Session("ValjanUser")

        For Each a As ArtikalSession In n.Artikli


            html.AppendFormat("<tr class='product-row' data-id='{0}' data-jedcijena='{1}'>", a.id, a.JedCijena)
            html.Append("<td class='product-col'>")
            html.Append("<figure class='product-image-container'>")

            'Slika
            Dim slika As String = ZadanaSlikaArtikla(a.id)
            If slika.Contains("http") = False Then 'Or slika.Contains("http://") = False Then
                html.AppendFormat("<img src='http://igre.ba/Thumb2.ashx?i={0}' alt='{1}'>", ZadanaSlikaArtikla(a.id), a.naziv)
            Else
                html.AppendFormat("<img src='{0}' alt='{1}'>", ZadanaSlikaArtikla(a.id), a.naziv)
            End If
            html.Append("</figure>") 'product-image-container

            'Naslov
            html.Append("<h2 class='product-title'>")
            html.AppendFormat("<a href='/artikal/{1}/{0}/'>{2}</a>", SrediNaziv(a.naziv), a.id, a.naziv)
            html.Append("</h2>") 'product-title

            html.Append("</td>") 'product-col

            'Cijena
            html.AppendFormat("<td>{0} {1}</td>", a.JedCijena, Valuta)
            html.Append("<td class='input-box select-dropdown'>")
            html.AppendFormat("<input type='text' name='qty' id=""qty{1}"" maxlength=""2"" value=""{1}"" title=""Količina"" class='items-field vertical-quantity form-control input-text qty {1}'>", a.id, a.Kolicina)
            html.Append("</td>")
            html.AppendFormat("<td class='ukupna-cijena'>{0} {1}</td>", a.Kolicina * a.JedCijena, Valuta)
            html.Append("</tr>") 'product-row

            html.Append("<tr class='product-action-row'>")
            html.Append("<td colspan='4' class='clearfix'>")
            html.Append("<div class='float-right'>")
            html.AppendFormat("<i title=""Uklonite artikal iz košarice"" class=""btn-remove btnBrisiArtikal fa fa-trash cart-remove-item"" data-id=""{0}"" style=""cursor:pointer;""></i>", a.id)
            'html.Append("<a href='#' title='Izbrisi proizvod' class='btn-remove btnBrisiArtikal cart-remove-item' data-id=""{0}""><span class='sr-only'>Remove</span></a>", a.id)
            html.Append("</div>") 'float-right
            html.Append("</td>") 'clearfix
            html.Append("</tr>") 'product-action-row
        Next



        Return html.ToString()
    End Function


    '---------------------------------------------------------------------------------------------> Artikal.aspx START <---------------------------------------------------------------------------------------------

    Public Shared Function Domena() As String
        Dim html As New StringBuilder

        html.Append(HttpContext.Current.Request.Url.Host)

        Return html.ToString()
    End Function

    Public Shared Function ProductSingleGallery() As String
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()
        Dim ArtikalID As Integer = 0

        ' Get ID from query string or path
        If Not String.IsNullOrEmpty(HttpContext.Current.Request.QueryString("id")) Then
            Integer.TryParse(HttpContext.Current.Request.QueryString("id"), ArtikalID)
        End If
        If ArtikalID = 0 Then
            Dim segments() As String = HttpContext.Current.Request.Path.Split("/"c)
            If segments.Length > 2 Then
                Integer.TryParse(segments(2), ArtikalID)
            End If
        End If

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "OdabraniArtikal"
                komanda.Parameters.AddWithValue("ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<div class='col-lg-6 col-xl-4 product-single-gallery'>")
                            html.Append("<div class='sticky-slider'>")
                            html.Append("<div class='product-slider-container product-item'>")
                            html.Append("<div class='product-single-carousel owl-carousel owl-theme'>")
                            html.AppendFormat("<img src='/Datoteke/SlikeArtikala/{0}' class='{1}' alt='{1}'/>", citac("Slika"), citac("ID"))

                            'html.AppendFormat(SlikeArtikla(citac("ID"), "product-item", "product-single-image"))
                            html.Append("</div>")
                            html.Append("</div>")
                            html.Append("<div class='prod-thumbnail row owl-dots transparent-dots' id='carousel-custom-dots'>")
                            'html.AppendFormat(SlikeArtikla(citac("ID"), "owl-dot", ""))
                            html.Append("</div>")
                            html.Append("</div>")
                            html.Append("</div>")
                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString()
    End Function

    Public Shared Function ProductSingleDescription() As String
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()
        Dim ArtikalID As Integer = 0

        ' Get ID from query string or path
        If Not String.IsNullOrEmpty(HttpContext.Current.Request.QueryString("id")) Then
            Integer.TryParse(HttpContext.Current.Request.QueryString("id"), ArtikalID)
        End If
        If ArtikalID = 0 Then
            Dim segments() As String = HttpContext.Current.Request.Path.Split("/"c)
            If segments.Length > 2 Then
                Integer.TryParse(segments(2), ArtikalID)
            End If
        End If

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "OdabraniArtikal"
                komanda.Parameters.AddWithValue("ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()

                            html.Append("<div class='col-lg-6 col-xl-8'>")
                            html.Append("<div class='product-single-details'>")
                            html.AppendFormat("<h1 class='product-title naziv-artikla' title='{0}'>{0}</h1>", citac("Naziv"))
                            html.Append("<div class='description'>")
                            html.AppendFormat("<p>{0}</p>", citac("OpisKratki"))
                            html.Append("</div>") 'description

                            html.Append("<div class='price-box'>")
                            Dim cijenaMPC As Decimal = citac("Cijena")
                            Dim cijenaSaKalkulacijom As Decimal = citac("Cijena") * (100 + citac("Procenat")) / 100
                            Dim akcijskaCijena As Decimal = citac("AkcijaCijena")

                            If citac("Cijena") < 1 Then
                                html.Append("<span class='product-price'>Cijena na upit</span>")
                            Else
                                ' Prikaz samo redovne cijene
                                html.AppendFormat("<span class='product-price'>{0} KM</span>", Format(cijenaMPC, "N2"))
                                'End If
                            End If
                            html.Append("</div>") 'price-box

                            html.Append("<div class='product-single-share mb-4 podjeli-proizvod'>")
                            html.Append("<label>Podijeli:</label><br />")
                            html.Append("<div class='addthis_inline_share_toolbox'>")
                            html.Append("<div class='share'>")
                            html.Append("<div class='a2a_kit a2a_kit_size_32 a2a_default_style'>")
                            html.Append("<a class='a2a_button_whatsapp'></a>")
                            html.Append("<a class='a2a_button_viber'></a>")
                            html.Append("<a class='a2a_button_facebook'></a>")
                            html.Append("<a class='a2a_button_facebook_messenger'></a>")
                            html.Append("<a class='a2a_button_sms'></a>")
                            html.Append("<a class='a2a_button_telegram'></a>")
                            html.Append("<a class='a2a_button_twitter'></a>")
                            html.Append("<a class='a2a_button_email'></a>")
                            html.Append("<a class='a2a_button_copy_link'></a>")
                            html.Append("<a class='a2a_dd' href='https://www.addtoany.com/share'></a>")
                            html.Append("</div>") 'a2a_kit a2a_kit_size_32 a2a_default_style
                            html.Append("<script>")
                            html.Append("var a2a_config = a2a_config || {};")
                            html.Append("a2a_config.locale = 'hr';")
                            html.Append("</script>")
                            html.Append("<script async src='https://static.addtoany.com/menu/page.js'></script>")
                            html.Append("</div>") 'share
                            html.Append("</div>") 'addthis_inline_share_toolbox
                            html.Append("</div>") 'product-single-share mb-4 podjeli-proizvod

                            html.AppendFormat("<div class='product-action div{0}'>", citac("ID"))
                            html.Append("<div class='product-single-qty'>")
                            html.AppendFormat("<input class='horizontal-quantity form-control qty {0}' type='text'/>", citac("ID"))
                            html.Append("</div>") 'product-single-qty
                            html.AppendFormat("<a href='#' class='paction add-cart dugmicDodaj dugmicDodaj-artikal' data-id='{0}' data-toggle='modal'data-target='#addCartModal' title='Dodaj u košaricu'>Dodaj u košaricu</a>", citac("ID"))
                            html.Append("</a>") 'paction add-wishlist
                            html.Append("</div>") 'product-action
                            html.Append("</div>") 'product-single-details


                            html.Append("<div class='product-single-tabs'>")

                            ' ----- NAV TABOVI -----
                            html.Append("<ul class='nav nav-tabs' role='tablist'>")

                            'Cijene na rate
                            html.Append("<li class='nav-item'>")
                            html.Append("<a class='nav-link active' id='product-tab-desc' data-toggle='tab' href='#product-desc-content' role='tab' aria-controls='product-desc-content' aria-selected='true'>Cijena na rate</a>")
                            html.Append("</li>")

                            'Detaljni opis
                            html.Append("<li class='nav-item'>")
                            html.Append("<a class='nav-link' id='product-tab-tags' data-toggle='tab' href='#product-tags-content' role='tab' aria-controls='product-tags-content' aria-selected='false'>Detaljni opis</a>")
                            html.Append("</li>")

                            'Recenzije
                            html.Append("<li class='nav-item'>")
                            html.Append("<a class='nav-link' id='product-tab-reviews' data-toggle='tab' href='#product-reviews-content' role='tab' aria-controls='product-reviews-content' aria-selected='false'>Komentari</a>")
                            html.Append("</li>")

                            html.Append("</ul>") ' Kraj nav-tabs


                            html.Append("<div class='tab-content'>")

                            'Cijena na rate
                            'html.Append("<div class='tab-pane fade show active' id='product-desc-content' role='tabpanel' aria-labelledby='product-tab-desc'>")
                            'html.Append("<div class='product-tags-content'>")
                            'html.Append("<table class='ratings-table tablica-rata'>")
                            'html.AppendFormat("<tr><td>2 - 3 rate:</td><td>{0} KM</td><td class='rating'>(već od {1} KM mjesečno)</td></tr>", Format(cijenaSaKalkulacijom * (100 + ProvizijaRate(3)) / 100, "N2"), Format(((cijenaSaKalkulacijom * (100 + ProvizijaRate(3)) / 100) / 3), "N2"))
                            'html.AppendFormat("<tr><td>4 - 6 rata:</td><td>{0} KM</td><td class='rating'>(već od {1} KM mjesečno)</td></tr>", Format(cijenaSaKalkulacijom * (100 + ProvizijaRate(6)) / 100, "N2"), Format(((cijenaSaKalkulacijom * (100 + ProvizijaRate(6)) / 100) / 6), "N2"))
                            'html.AppendFormat("<tr><td>7 - 12 rata:</td><td>{0} KM</td><td class='rating'>(već od {1} KM mjesečno)</td></tr>", Format(cijenaSaKalkulacijom * (100 + ProvizijaRate(12)) / 100, "N2"), Format(((cijenaSaKalkulacijom * (100 + ProvizijaRate(12)) / 100) / 12), "N2"))
                            'html.AppendFormat("<tr><td>18 rata:</td><td>{0} KM</td><td class='rating'>(već od {1} KM mjesečno)</td></tr>", Format(cijenaSaKalkulacijom * (100 + ProvizijaRate(18)) / 100, "N2"), Format(((cijenaSaKalkulacijom * (100 + ProvizijaRate(18)) / 100) / 18), "N2"))
                            'html.Append("</table>")
                            'html.Append("</div>") ' Kraj product-tags-content
                            'html.Append("</div>") ' Kraj tab-pane (Cijena na rate)

                            'Detaljni opis
                            html.Append("<div class='tab-pane fade' id='product-tags-content' role='tabpanel' aria-labelledby='product-tab-tags'>")
                            html.Append("<div class='product-desc-content'>")
                            html.AppendFormat("<p>{0}</p>", citac("Opis"))
                            html.Append("</div>") ' Kraj product-desc-content
                            html.Append("</div>") ' Kraj tab-pane (Detaljni opis)

                            'Komentari
                            html.Append("<div class='tab-pane fade' id='product-reviews-content' role='tabpanel' aria-labelledby='product-tab-reviews'>")
                            html.Append("<div class='product-reviews-content'>")
                            html.Append("<div class='add-product-review'>")

                            'Using konekcijaKomentara As New SqlConnection(putanja)
                            '    konekcijaKomentara.Open()
                            '    Using komandaKomentara As New SqlCommand()
                            '        komandaKomentara.Connection = konekcijaKomentara
                            '        komandaKomentara.CommandType = CommandType.Text
                            '        komandaKomentara.CommandText = "SELECT * FROM Komentari WHERE NarudzbaID IN (SELECT NarudzbaID FROM NarudzbeStavke WHERE ArtikalID=@ArtikalID) AND Odobreno=1 ORDER BY Date DESC;"
                            '        komandaKomentara.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                            '        Using citacKomentara As SqlDataReader = komandaKomentara.ExecuteReader()
                            '            If citacKomentara IsNot Nothing Then
                            '                While citacKomentara.Read()
                            '                    Dim ocjena As Integer = Convert.ToInt32(citacKomentara("Ocjena"))

                            '                    html.Append("<div class='comment-box'>")
                            '                    ' Ime + zvjezdice
                            '                    html.Append("<div class='comment-name-date'>")
                            '                    html.Append(citacKomentara("Ime"))

                            '                    ' Zvjezdice
                            '                    html.Append("<span class='comment-stars'>")
                            '                    For i As Integer = 1 To 5
                            '                        If i <= ocjena Then
                            '                            html.Append("<span class='star active'>&#9733;</span>")
                            '                        Else
                            '                            html.Append("<span class='star'>&#9733;</span>")
                            '                        End If
                            '                    Next
                            '                    html.Append("</span>")
                            '                    html.Append("</div>") ' comment-name-date

                            '                    ' Tekst komentara
                            '                    html.Append("<div class='comment-text'>")
                            '                    html.Append(citacKomentara("Komentar"))
                            '                    html.Append("</div>")

                            '                    html.Append("</div>") ' comment-box
                            '                    html.Append("<hr class='comment-line-my'/>")
                            '                End While
                            '            End If
                            '        End Using
                            '    End Using
                            'End Using

                            html.Append("</div>") ' add-product-review
                            html.Append("</div>") ' product-reviews-content
                            html.Append("</div>") ' Kraj tab-pane (Recenzije)

                            html.Append("</div>") ' Kraj tab-content
                            html.Append("</div>") ' Kraj product-single-tabs
                            html.Append("</div>") 'col-lg-6 col-xl-8

                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString()
    End Function

    Public Shared Function OpisArtikal(ArtikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Opis FROM Artikli WHERE ID=@ArtikalID"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append(citac("Opis"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function


    Public Shared Function PronadjiIdGrupe(ArtikalID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT GrupaID FROM Artikli WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat(citac("GrupaID"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function


    Public Shared Function SlikeArtikla(ArtikalID As Integer, Klasa As String, Velika As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliSlike WHERE ArtikalID=@ArtikalID ORDER BY Zadana DESC;"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            Dim slika As String = citac("Datoteka")
                            html.AppendFormat("<div class='{0}'>", Klasa)
                            If slika.Contains("http") = False Then
                                html.AppendFormat("<img src='http://igre.ba/Thumb2.ashx?i={0}' class='{2}' alt='{1}'/>", slika, citac("Datoteka"), Velika)
                            Else
                                html.AppendFormat("<img src='{0}' class='{2}' alt='{1}'/>", slika, citac("Datoteka"), Velika)
                            End If
                            html.Append("</div>") 'Klasa
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function



    '---------------------------------------------------------------------------------------------> Artikal.aspx END <---------------------------------------------------------------------------------------------


    '---------------------------------------------------------------------------------------------> Kosarica.aspx START <---------------------------------------------------------------------------------------------

    Public Shared Function Kosarica() As String
        Dim html As New StringBuilder
        Dim n As Narudzba
        If IsNothing(HttpContext.Current.Session("Narudzba")) = True Then
            n = New Narudzba
            HttpContext.Current.Session("Narudzba") = n
        Else
            n = CType(HttpContext.Current.Session("Narudzba"), Narudzba)
        End If

        Dim cijenaDostave As Decimal = Postavke("CijenaDostava")

        html.Append("<div class='col-lg-8 tjelo kosarica'>")
        html.Append("<div class='cart-table-container'>")
        html.Append("<table class='table-cart table stavke'>")
        html.Append("<thead>")
        html.Append("<tr>")
        html.Append("<th class='product-col'> Proizvod</th>")
        html.Append("<th class='price-col'>Cijena</th>")
        html.Append("<th class='qty-col'>Količina</th>")
        html.Append("<th>Ukupno</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class='tbStavkelBody'>")
        html.Append(MojaKosaricaSession(n))
        html.Append("</tbody>")
        html.Append("<tfoot>")
        html.Append("<tr>")
        html.Append("<td colspan='4' class='clearfix'>")
        html.Append("<div class='float-left'>")
        html.Append("<a href='/' class='btn btn-outline-secondary'>Vratite se na kupovinu</a>")
        html.Append("</div>") 'float-left
        html.Append("</td>")
        html.Append("</tr>")
        html.Append("</tfoot>")
        html.Append("</table>") 'table table-cart tjelo kosarica
        html.Append("</div>") 'cart-table-container
        html.Append("</div>") 'col-lg-8

        html.Append("<div class='col-lg-4'>")
        html.Append("<div class='cart-summary'>")
        html.Append("<h3>Narudžba</h3>")
        html.Append("<table class='table table-totals'>")
        html.Append("<tbody>")
        html.Append("<tr>")
        html.Append("<td>Cijena narudžbe</td>")
        html.AppendFormat("<td class='ukupna-cijena'>{0} KM</td>", n.Ukupno)
        html.Append("</tr>")
        html.Append("<tr>")
        html.Append("<td>Dostava</td>")
        html.AppendFormat("<td>{0} KM</td>", cijenaDostave)
        html.Append("</tr>")
        html.Append("</tbody>")
        html.Append("<tfoot>")
        html.Append("<tr>")
        html.Append("<td>Ukupna cijena:</td>")
        html.AppendFormat("<td>{0} KM</td>", n.Ukupno + cijenaDostave)
        html.Append("</tr>")
        html.Append("</tfoot>")
        html.Append("</table>") 'table table-totals
        html.Append("<div class='checkout-methods'>")
        html.Append("<a href='/adresa-dostave' class='btn btn-block btn-sm btn-primary'>Nastavi dalje</a>")
        html.Append("</div>") 'checkout-methods
        html.Append("</div>") 'cart-summary
        html.Append("</div>") 'col-lg-4

        Return html.ToString()
    End Function


    '---------------------------------------------------------------------------------------------> Pretraga.aspx START <---------------------------------------------------------------------------------------------

    Public Shared Function Pretraga(pojam As String) As String
        Return ArtikliPretraga(1, pojam)
    End Function


    Public Shared Function ArtikliPretraga(stranica As Integer, pojam As String) As String
        Dim html As New StringBuilder
        Dim putanja As String = SQLKonekcija()

        Dim KupacLogiran As Boolean
        Dim NivoTrenutnogKupca As String
        If HttpContext.Current.Session("ValjanUser") = True Then
            KupacLogiran = HttpContext.Current.Session("ValjanUser")
            NivoTrenutnogKupca = NivoLogiranogKorisnika()
        Else
            KupacLogiran = False
            NivoTrenutnogKupca = "0"
        End If

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "OdaberiPretragu"
                komanda.Parameters.AddWithValue("@Pojam", pojam)
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()

                            'Proizvod
                            html.AppendFormat("<div class='col-6 col-md-4 col-xl-3 div{0}'>", citac("ID"))
                            If citac("BesplatnaDostava") = True Then
                                html.AppendFormat("<div class=""free-shipping""></div>")
                            End If
                            html.Append("<div class='product-default inner-quickview inner-icon'>")
                            html.Append("<figure>")
                            html.AppendFormat("<a href='/artikal/{0}'>", citac("ID"))
                            Dim slika As String = ZadanaSlikaArtikla(citac("ID"))   'Slika
                            If slika.Contains("http") = False Then 'Or slika.Contains("http://") = False Then
                                html.AppendFormat("<img src=""http://igre.ba/Thumb2.ashx?i={0}"" alt=""{1}"">", ZadanaSlikaArtikla(citac("ID")), citac("Naziv"))
                            Else
                                html.AppendFormat("<img src=""{0}"" alt=""{1}"">", ZadanaSlikaArtikla(citac("ID")), citac("Naziv"))
                            End If
                            html.Append("</a>") '/artikal/{0}
                            html.Append("<div class='btn-icon-group'>")
                            html.AppendFormat("<input type=""hidden"" class=""qty {0}"" value=""1"">", citac("ID"))
                            html.AppendFormat("<button type='button' class='btn-icon btn-add-cart dugmicDodaj' data-toggle='modal' data-id=""{0}"" data-target='#addCartModal' title=""Dodaj u košaricu""><i class='icon-bag'></i></button>", citac("ID"))
                            html.Append("</div>") 'btn-icon-group
                            'html.AppendFormat("<a href='/artikal/{0}' class='btn-quickview' title='Vidi proizvod'>Vidi proizvod</a>", citac("ID"))

                            Dim NazivNadGrupe As String = Komponente.PronadjiNazivNadGrupe(citac("NadGrupaID"))
                            Dim NazivGrupe As String = Komponente.PronadjiNazivGrupe(citac("GrupaID"))

                            html.Append("</figure>")
                            html.Append("<div class='product-details'>")
                            html.Append("<div class='category-wrap'>")
                            html.Append(" <div class='category-list'>")
                            html.AppendFormat("<a href=""/grupa/{0}/{1}/"" class='product-category nadgrupa'>{2}</a>", citac("NadgrupaId"), SrediNaziv(NazivNadGrupe), NazivNadGrupe)
                            'html.AppendFormat("<a href='/grupa?id={1}' class='product-category'>{0}</a>", NazivGrupe, citac("GrupaId"))
                            html.Append("</div>") 'category-list
                            'html.Append("<a href='#' class='btn-icon-wish'><i class='icon-heart'></i></a>")
                            html.Append("</div>") 'category-wrap



                            'Naziv proizvoda
                            html.Append("<h2 class='product-title naslov-istaknuto'>")
                            html.AppendFormat("<a href='/artikal/{0}' title='{1}'>{1}</a>", citac("ID"), citac("Naziv"))
                            html.Append("</h2>") 'product-title

                            'Cijena
                            html.Append("<div class='price-box'>")
                            Dim cijenaSaKalkulacijom As Decimal = citac("Cijena") * (100 + citac("Procenat")) / 100
                            Dim akcijskaCijena As Decimal = citac("AkcijaCijena")

                            If citac("Cijena") < 1 Then
                                html.Append("<span class='product-price'>Cijena na upit</span>")
                            Else
                                If akcijskaCijena > 0 Then
                                    ' Prikaz akcijske i stare cijene
                                    html.AppendFormat("<span class='old-price'>{0} KM</span>", Format(akcijskaCijena, "N2"))
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                Else
                                    ' Prikaz samo redovne cijene
                                    html.AppendFormat("<span class='product-price nova-cijena-istaknuto'>{0} KM</span>", Format(cijenaSaKalkulacijom, "N2"))
                                End If
                            End If
                            html.Append("</div>") 'price-box

                            html.Append("</div>") 'product-details
                            html.Append("</div>") 'product-default inner-quickview inner-icon
                            html.Append("</div>") 'col-6 col-md-4 col-xl-3

                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function


    '---------------------------------------------------------------------------------------------> Pretraga.aspx END <---------------------------------------------------------------------------------------------





End Class
