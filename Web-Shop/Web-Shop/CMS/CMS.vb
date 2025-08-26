Imports System.Data.SqlClient
Imports System.IO

Public Class CMS

    Public Shared Function Postavke(Naziv As String) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

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

    Public Shared Function Uvjeti() As String
        Dim html As New StringBuilder
        If HttpContext.Current.Request.Url.ToString.Contains("localhost") = False Then
            If HttpContext.Current.Request.Url.ToString.Contains("igre.ba") = False Then
                HttpContext.Current.Response.Redirect("http://www.ave-studio.com")
            End If
        End If

        html.Append("<!--[if lt IE 7]>      <html class=""no-js lt-ie9 lt-ie8 lt-ie7"" lang=""""> <![endif]-->")
        html.Append("<!--[if IE 7]>         <html class=""no-js lt-ie9 lt-ie8"" lang=""""> <![endif]-->")
        html.Append("<!--[if IE 8]>         <html class=""no-js lt-ie9"" lang=""""> <![endif]-->")

        Return html.ToString
    End Function

    Public Shared Function ChekAuth()
        If HttpContext.Current.Session("ValjanUser") = False Then
            HttpContext.Current.Response.Redirect("/CMS/?putanja=" & TrenutnaPutanja())
        End If
    End Function

    Public Shared Function ZajednickeMete() As String
        Dim html As New StringBuilder

        html.Append("<meta charset=""utf-8"">")
        html.Append("<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">")
        html.Append("<meta name=""viewport"" content=""width=device-width, initial-scale=1"">")
        html.Append("<link rel=""apple-touch-icon"" href=""apple-icon.png"">")
        html.Append("<link rel=""shortcut icon"" href=""/CMS/favicon.ico"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/normalize.css"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/bootstrap.min.css"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/font-awesome.min.css"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/themify-icons.css"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/flag-icon.min.css"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/cs-skin-elastic.css"">")
        html.Append("<link rel=""stylesheet"" href=""/CMS/assets/css/lib/datatable/dataTables.bootstrap.min.css"">")

        html.AppendFormat("<link rel=""stylesheet"" href=""/CMS/assets/scss/style.css?v={0}"">", Format(DateAndTime.Now(), "HHmmss"))
        html.Append("<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700,800' rel='stylesheet' type='text/css'>")

        Return html.ToString
    End Function

    Public Shared Function TrenutnaPutanja() As String
        Dim html As New StringBuilder

        'Dim url As String = HttpContext.Current.Request.Cookies("url").Value.ToString
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        html.Append(url)

        Return html.ToString
    End Function

    Public Shared Function LeftPanel() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<aside id=""left-panel"" class=""left-panel"">")
        html.Append("<nav class=""navbar navbar-expand-sm navbar-default"">")
        html.Append("<div class=""navbar-header"">")
        html.Append("<button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#main-menu"" aria-controls=""main-menu"" aria-expanded=""false"" aria-label=""Toggle navigation"">")
        html.Append("<i class=""fa fa-bars""></i>")
        html.Append("</button>")
        html.Append("<a class=""navbar-brand"" href=""/CMS/Dashboard.aspx""><img src=""/CMS/images/logo.png"" alt=""Logo""></a>")
        html.Append("<a class=""navbar-brand hidden"" href=""/CMS/Dashboard.aspx""><img src=""/CMS/images/logo2.png"" alt=""Logo""></a>")
        html.Append("</div>") 'navbar-header

        html.Append("<div id=""main-menu"" class=""main-menu collapse navbar-collapse"">")
        html.Append("<ul class=""nav navbar-nav"">")
        html.Append("<li class=""active"">")
        html.Append("<a href=""Dashboard.aspx""> <i class=""menu-icon fa fa-dashboard""></i>Dashboard</a>")
        html.Append("</li>")
        'html.Append("<li>")
        'html.AppendFormat("<span style=""color:white"">{0}</span>", TrenutnaPutanja)
        'html.Append("</li>")

        html.Append(CmsKomponente())


        html.Append("</ul>") 'nav navbar-nav
        html.Append("</div>") 'main-menu

        html.Append("</nav>")
        html.Append("</aside>")

        Return html.ToString
    End Function

    Private Shared Function CmsKomponente() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        'html.Append("<h3 class=""menu-title"">Upravljanje artiklima</h3>")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM CmsKomponente WHERE Aktivno='1' AND AdminLevel<=@AdminLevel ORDER BY Prioritet ASC;"
                komanda.Parameters.AddWithValue("@AdminLevel", 9)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<h3 class=""menu-title"">{0}</h3>", citac("Komponenta"))
                            html.Append(Grupe(citac("ID")))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function Grupe(KomponentaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM CmsGrupe WHERE Aktivno='1' AND KomponentaID=@KomponentaID AND AdminLevel<=@AdminLevel ORDER BY Prioritet ASC;"
                komanda.Parameters.AddWithValue("@KomponentaID", KomponentaID)
                komanda.Parameters.AddWithValue("@AdminLevel", 9)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<li class=""menu-item-has-children dropdown"">")
                            html.AppendFormat("<a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false""> <i class=""menu-icon fa {1}""></i>{0}</a>", citac("Grupa"), citac("Ikona"))
                            html.Append(Elementi(citac("ID")))
                            html.Append("</li>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function Elementi(GrupaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<ul class=""sub-menu children dropdown-menu"">")
        'html.Append("<li><i class=""fa fa-puzzle-piece""></i><a href=""ui-buttons.html"">Buttons</a></li>")
        'html.Append("<li><i class=""fa fa-id-badge""></i><a href=""ui-badges.html"">Badges</a></li>")
        'html.Append("<li><i class=""fa fa-bars""></i><a href=""ui-tabs.html"">Tabs</a></li>")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM CmsElementi WHERE Aktivno='1' AND GrupaID=@GrupaID AND AdminLevel<=@AdminLevel ORDER BY Prioritet ASC"
                komanda.Parameters.AddWithValue("@GrupaID", GrupaID)
                komanda.Parameters.AddWithValue("@AdminLevel", 9)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<li><i class=""fa {1}""></i><a href=""{2}"">{0}</a></li>", citac("Element"), citac("Ikona"), citac("Link"))
                            'html.AppendFormat("<li><i class=""fa fa-bars""></i><a href=""ui-tabs.html"">Tabs</a></li>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</ul>")

        Return html.ToString
    End Function

    Public Shared Function FootScript() As String
        Dim html As New StringBuilder

        html.Append("<script src=""/CMS/assets/js/vendor/jquery-2.1.4.min.js""></script>")
        html.Append("<script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js""></script>")
        html.Append("<script src=""/CMS/assets/js/plugins.js""></script>")
        html.Append("<script src=""/CMS/assets/js/main.js""></script>")

        html.Append("<script src=""/CMS/assets/js/lib/chart-js/Chart.bundle.js""></script>")
        'html.Append("<script src=""/CMS/assets/js/dashboard.js""></script>")
        html.Append("<script src=""/CMS/assets/js/widgets.js""></script>")
        html.Append("<script src=""/CMS/assets/js/lib/vector-map/jquery.vmap.js""></script>")
        html.Append("<script src=""/CMS/assets/js/lib/vector-map/jquery.vmap.min.js""></script>")
        html.Append("<script src=""/CMS/assets/js/lib/vector-map/jquery.vmap.sampledata.js""></script>")
        html.Append("<script src=""/CMS/assets/js/lib/vector-map/country/jquery.vmap.world.js""></script>")
        html.Append("<script src=""/CMS/assets/js/jquery-migrate-1.4.1.min.js""></script>")
        ' html.Append("<script src=""/CMS/assets/js/JQuery.Block.js""</script>")
        html.AppendFormat("<script src=""/CMS/assets/js/Script.js?v={0}""></script>", Format(DateAndTime.Now(), "HHmmss"))
        html.Append("<script src=""/CMS/Tiny/jquery.tinymce.js?v=01"" type=""text/javascript""></script>")


        'html.Append(" <script> ")
        'html.Append(" (function ($) { ")
        'html.Append(" ""use strict""; ")
        'html.Append(" jQuery('#vmap').vectorMap({ ")
        'html.Append(" map:    'world_en', ")
        'html.Append(" backgroundColor: null, ")
        'html.Append(" color() '#ffffff', ")
        'html.Append(" hoverOpacity: 0.7, ")
        'html.Append(" selectedColor:  '#1de9b6', ")
        'html.Append(" enableZoom: true, ")
        'html.Append(" showTooltip: true, ")
        'html.Append(" values: sample_data, ")
        'html.Append(" scaleColors: ['#1de9b6', '#03a9f5'], ")
        'html.Append(" normalizeFunction:  'polynomial' ")
        'html.Append(" }); ")
        'html.Append(" })(jQuery); ")
        'html.Append(" </script> ")

        Return html.ToString
    End Function

    Public Shared Function HeaderString() As String
        Dim html As New StringBuilder

        html.Append("<header id=""header"" class=""header"">")
        html.Append("<div class=""header-menu"">")

        html.Append("<div class=""col-sm-7"">")
        html.Append("<a id=""menuToggle"" class=""menutoggle pull-left""><i class=""fa fa fa-tasks""></i></a>")
        html.Append("<div class=""header-left"">")
        html.Append(Pretraga())
        html.Append(Notification())
        html.Append(Message())
        html.Append("</div>") 'header-left
        html.Append("</div>") 'col-sm-7

        html.Append("<div class=""col-sm-5"">")
        html.Append(UserMenu())
        html.Append(Language())
        html.Append("</div>") 'col-sm-5

        html.Append("</div>") 'header-menu
        html.Append("</header>") 'header

        Return html.ToString
    End Function

    Public Shared Function ImeKorisnika(KorisnikID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT ImePrezime FROM Korisnici WHERE ID=@KorisnikID"
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append(citac("ImePrezime"))
                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString()
    End Function

    Public Shared Function UserMenu() As String
        Dim html As New StringBuilder
        Dim ime As String = ImeKorisnika(Komponente.LogiraniKorisnikID)
        Dim names() As String = ime.Split(" "c)
        Dim incijali As String = String.Join("", Array.ConvertAll(names, Function(n) n.Substring(0, 1).ToUpper))

        html.Append("<div class=""user-area dropdown float-right"">")
        html.Append("<a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">")
        html.AppendFormat("<span>{0}</span>", incijali)
        'html.Append("<img class=""user-avatar rounded-circle"" src=""images/admin.jpg"" alt=""User Avatar"">")
        html.Append("</a>")
        html.Append("<div class=""user-menu dropdown-menu"">")
        'html.Append("<a class=""nav-link"" href=""#""><i class=""fa fa- user""></i>My Profile</a>")
        'html.Append("<a class=""nav-link"" href=""#""><i class=""fa fa- user""></i>Notifications <span class=""count"">13</span></a>")
        'html.Append("<a class=""nav-link"" href=""#""><i class=""fa fa -cog""></i>Settings</a>")
        html.Append("<a class=""nav-link"" href=""/CMS/Default.aspx?a=logout""><i class=""fa fa-power -off""></i>Logout</a>")
        html.Append("</div>") 'user-menu dropdown-menu
        html.Append("</div>") 'user-area dropdown float-right

        Return html.ToString
    End Function

    Public Shared Function Language() As String
        Dim html As New StringBuilder

        html.Append("<div class=""language-select dropdown"" id=""language-select"">")
        'html.Append("<a class=""dropdown-toggle"" href=""#"" data-toggle=""dropdown""  id=""language"" aria-haspopup=""true"" aria-expanded=""true"">")
        'html.Append("<i class=""flag-icon flag-icon-us""></i>")
        'html.Append("</a>")
        'html.Append("<div class=""dropdown-menu"" aria-labelledby=""language"" >")
        'html.Append("<div class=""dropdown-item"">")
        'html.Append("<span class=""flag-icon flag-icon-fr""></span>")
        'html.Append("</div>") 'dropdown-item
        'html.Append("<div class=""dropdown-item"">")
        'html.Append("<i class=""flag-icon flag-icon-es""></i>")
        'html.Append("</div>") 'dropdown-item
        'html.Append("<div class=""dropdown-item"">")
        'html.Append("<i class=""flag-icon flag-icon-us""></i>")
        'html.Append("</div>") 'dropdown-item
        'html.Append("<div class=""dropdown-item"">")
        'html.Append("<i class=""flag-icon flag-icon-it""></i>")
        'html.Append("</div>") 'dropdown-item
        'html.Append("</div>") 'dropdown-menu
        html.Append("</div>") 'language-select dropdown

        Return html.ToString
    End Function

    Public Shared Function Message() As String
        Dim html As New StringBuilder

        html.Append("<div class=""dropdown for-message"">")
        html.Append("<button class=""btn btn-secondary dropdown-toggle"" type=""button"" id = ""message"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">")
        html.Append("<i class=""ti-email""></i>")
        html.AppendFormat("<span class=""count bg-primary"">{0}</span>", "0")
        html.Append("</button>")
        html.Append("<div class=""dropdown-menu"" aria-labelledby=""message"">")
        html.AppendFormat("<p class=""red"">Broj poruka: {0}</p>", "0")
        'html.Append("<a class=""dropdown-item media bg-flat-color-1"" href=""#"">")
        'html.Append("<span class=""photo media-left""><img alt=""avatar"" src=""images/avatar/1.jpg""></span>")
        'html.Append("<span class=""message media-body"">")
        'html.Append("<span class=""name float-left"">Jonathan Smith</span>")
        'html.Append("<span class=""time float-right"">Just now</span>")
        'html.Append("<p>Hello, this is an example msg</p>")
        'html.Append("</span>")
        'html.Append("</a>")
        'html.Append("<a class=""dropdown-item media bg-flat-color-4"" href=""#"">")
        'html.Append("<span class=""photo media-left""><img alt=""avatar"" src=""images/avatar/2.jpg""></span>")
        'html.Append("<span class=""message media-body"">")
        'html.Append("<span class=""name float-left"">Jack Sanders</span>")
        'html.Append("<span class=""time float-right"">5 minutes ago</span>")
        'html.Append("<p>Lorem ipsum dolor sit amet, consectetur</p>")
        'html.Append("</span>")
        'html.Append("</a>")
        html.Append("</div>")
        html.Append("</div>")
        html.AppendFormat(" <span class=""badge badge-dark"">{0}</span>", Postavke("Tvrtka"))

        Return html.ToString
    End Function

    Private Shared Function BrojNeprocitanihObavjesti(idLogiranogKorisnika As Integer) As Object
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(ID) AS Broj FROM Obavjesti WHERE Aktivno='1' AND ID NOT IN (SELECT ObavjestID FROM ObavjestiStatus WHERE ProcitaoID=@KorisnikID)"
                komanda.Parameters.AddWithValue("@KorisnikID", idLogiranogKorisnika)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
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

    Public Shared Function Notification() As String
        Dim html As New StringBuilder
        Dim idLogiranogKorisnika As Integer = Komponente.LogiraniKorisnikID
        Dim broj As Integer = BrojNeprocitanihObavjesti(idLogiranogKorisnika)

        html.Append("<div class=""dropdown for-notification"">")
        html.Append("<button class=""btn btn-secondary dropdown-toggle"" type=""button"" id=""notification"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">")
        html.Append("<i class=""fa fa-bell""></i>")
        'html.AppendFormat("<span class=""count bg-danger"">{0}</span>", broj)
        html.AppendFormat("<span class=""count bg-danger"">{0}</span>", "0")
        html.Append("</button>")
        html.Append("<div class=""dropdown-menu"" aria-labelledby=""notification"">")
        'html.AppendFormat("<p class=""red""><a href=""/CMS/Obavjesti.aspx"">Broj obavjesti: {0}</a></p>", broj)
        html.AppendFormat("<p class=""red""><a >Broj obavjesti: {0}</a></p>", "0")
        ''html.Append("<a class=""dropdown-item media bg-flat-color-1"" href=""#"">")
        ''html.Append("<i class=""fa fa-check""></i>")
        ''html.Append("<p>Server #1 overloaded.</p>")
        ''html.Append("</a>")
        ''html.Append("<a class=""dropdown-item media bg-flat-color-4"" href=""#"">")
        ''html.Append("<i class=""fa fa-info""></i>")
        ''html.Append("<p>Server #2 overloaded.</p>")
        ''html.Append("</a>")
        html.Append("</div>") 'dropdown-menu
        html.Append("</div>") 'dropdown for-notification

        Return html.ToString
    End Function

    Public Shared Function Pretraga() As String
        Dim html As New StringBuilder

        html.Append("<button class=""search-trigger""><i class=""fa fa-search""></i></button>")
        html.Append("<div class=""form-inline"">")
        html.Append("<form class=""search-form"" action=""/CMS/Prtraga.aspx"" method=""get"" enctype=""multipart/form-data"" autocomplete=""off"">")
        html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o"" style=""display: none;""></i> Traži</button> ")
        html.Append("<input class=""form-control mr-sm-2"" name=""pojam"" type=""text"" placeholder=""pojam ..."" aria-label=""pojam"">")
        html.Append("<button class=""search-close"" type=""submit""><i class=""fa fa-close""></i></button>")
        html.Append("</form>")
        html.Append("</div>")

        Return html.ToString
    End Function

    Public Shared Function StatistikaArtikala() As String
        Dim html As New StringBuilder

        'aktivni artikli
        html.Append("<div class=""col-sm-6 col-lg-3"">")
        html.Append("<div class=""card text-white bg-flat-color-1"">")
        html.Append("<div class=""card-body pb-0"">")
        html.Append("<div class=""dropdown float-right"">")
        html.Append("<button class=""btn bg-transparent dropdown-toggle theme-toggle text-light"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"">")
        html.Append("<i class=""fa fa-cog""></i>")
        html.Append("</button>")
        html.Append("<div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">")
        html.Append("<div class=""dropdown-menu-content"">")
        html.Append("<a class=""dropdown-item"" href=""Artikli.aspx"">Pregledaj artikle</a>")
        'html.Append("<a class=""dropdown-item"" href=""#"">Another action</a>")
        'html.Append("<a class=""dropdown-item"" href=""#"">Something else here</a>")
        html.Append("</div>") 'dropdown-menu-content
        html.Append("</div>") 'dropdown-menu
        html.Append("</div>") 'dropdown float-right
        html.Append("<h4 class=""mb-0"">")
        html.AppendFormat("<span class=""count"">{0}</span>", PrebrojiArtikle(1))
        html.Append("</h4>")
        html.Append("<p class=""text-light"">Aktivni artikli</p>")
        'html.Append("<div class=""chart-wrapper px-0"" style=""height: 70px;"" height=""70"">")
        ''html.Append("<canvas id=""widgetChart1""></canvas>")
        'html.Append("</div>") 'chart-wrapper px-0
        html.Append("</div>") 'card-body pb-0
        html.Append("</div>") 'card text-white bg-flat-color-1
        html.Append("</div>") 'col-sm-6 col-lg-3

        'neaktivni artikli
        html.Append("<div class=""col-sm-6 col-lg-3"">")
        html.Append("<div class=""card text-white bg-flat-color-2"">")
        html.Append("<div class=""card-body pb-0"">")
        html.Append("<div class=""dropdown float-right"">")
        html.Append("<button class=""btn bg-transparent dropdown-toggle theme-toggle text-light"" type=""button"" id=""Button1"" data-toggle=""dropdown"">")
        html.Append("<i class=""fa fa-cog""></i>")
        html.Append("</button>")
        html.Append("<div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">")
        html.Append("<div class=""dropdown-menu-content"">")
        html.Append("<a class=""dropdown-item"" href=""ArtikliNa.aspx"">Pregledaj artikle</a>")
        'html.Append("<a class=""dropdown-item"" href=""#"">Another action</a>")
        'html.Append("<a class=""dropdown-item"" href=""#"">Something else here</a>")
        html.Append("</div>")
        html.Append("</div>")
        html.Append("</div>")
        html.Append("<h4 class=""mb-0"">")
        html.AppendFormat("<span class=""count"">{0}</span>", PrebrojiArtikle(0))
        html.Append("</h4>")
        html.Append("<p class=""text-light"">Neaktivni artikli</p>")
        'html.Append("<div class=""chart-wrapper px-0"" style=""height: 70px;"" height=""70"">")
        'html.Append("<canvas id=""widgetChart2""></canvas>")
        'html.Append("</div>")
        html.Append("</div>")
        html.Append("</div>")
        html.Append("</div>") 'col-sm-6 col-lg-3

        'ArtikliNaMinimumuStats
        html.Append("<div class=""col-sm-6 col-lg-3"">")
        html.Append("<div class=""card text-white bg-flat-color-3"">")
        html.Append("<div class=""card-body pb-0"">")
        'html.Append("<div class=""dropdown float-right"">")
        'html.Append("<button class=""btn bg-transparent dropdown-toggle theme-toggle text-light"" type=""button"" id=""Button2"" data-toggle=""dropdown"">")
        'html.Append("<i class=""fa fa-cog""></i>")
        'html.Append("</button>")
        'html.Append("<div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">")
        'html.Append("<div class=""dropdown-menu-content"">")
        'html.Append("<a class=""dropdown-item"" href=""#"">Pregledaj artikle</a>")
        ''html.Append("<a class=""dropdown-item"" href=""#"">Another action</a>")
        ''html.Append("<a class=""dropdown-item"" href=""#"">Something else here</a>")
        'html.Append("</div>") 'dropdown-menu-content
        'html.Append("</div>") 'dropdown-menu
        'html.Append("</div>") 'dropdown float-right
        html.Append("<h4 class=""mb-0"">")
        html.AppendFormat("<span class=""count"">{0}</span>", PrebrojiArtikleRaspon(0, 11))
        html.Append("</h4>")
        html.Append("<p class=""text-light"">Artikli ispod 10 kom</p>")
        html.Append("</div>")
        'html.Append("<div class=""chart-wrapper px-0"" style=""height: 70px;"" height=""70"">")
        'html.Append("<canvas id=""widgetChart3""></canvas>")
        'html.Append("</div>")
        html.Append("</div>")
        html.Append("</div>") 'col-sm-6 col-lg-3

        'ArtikliBezKolicineStats
        html.Append("<div class=""col-sm-6 col-lg-3"">")
        html.Append("<div class=""card text-white bg-flat-color-4"">")
        html.Append("<div class=""card-body pb-0"">")
        'html.Append("<div class=""dropdown float-right"">")
        'html.Append("<button class=""btn bg-transparent dropdown-toggle theme-toggle text-light"" type=""button"" id=""Button3"" data-toggle=""dropdown"">")
        'html.Append("<i class=""fa fa-cog""></i>")
        'html.Append("</button>")
        'html.Append("<div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">")
        'html.Append("<div class=""dropdown-menu-content"">")
        'html.Append("<a class=""dropdown-item"" href=""#"">Pregledaj artikle</a>")
        ''html.Append("<a class=""dropdown-item"" href=""#"">Another action</a>")
        ''html.Append("<a class=""dropdown-item"" href=""#"">Something else here</a>")
        'html.Append("</div>") 'dropdown-menu-content
        'html.Append("</div>") 'dropdown-menu
        'html.Append("</div>") 'dropdown float-right
        html.Append("<h4 class=""mb-0"">")
        html.AppendFormat("<span class=""count"">{0}</span>", PrebrojiArtikleRaspon(-1000000, 1))
        html.Append("</h4>")
        html.Append("<p class=""text-light"">Artikli kojih nema na stanju</p>")
        'html.Append("<div class=""chart-wrapper px-3"" style=""height: 70px;"" height=""70"">")
        'html.Append("<canvas id=""widgetChart4""></canvas>")
        'html.Append("</div>")
        html.Append("</div>")
        html.Append("</div>")
        html.Append("</div>") 'col-sm-6 col-lg-3

        Return html.ToString
    End Function


    Private Shared Function PrebrojiArtikle(AktivanNeaktivan As Boolean) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM Artikli WHERE Aktivno=@AktivanNeaktivan"
                komanda.Parameters.AddWithValue("@AktivanNeaktivan", AktivanNeaktivan)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function PrebrojiArtikleRaspon(minKolicina As Integer, maxKolicina As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM Artikli WHERE Kolicina>@minKolicina AND Kolicina<@maxKolicina"
                'komanda.Parameters.AddWithValue("@AktivanNeaktivan", AktivanNeaktivan)
                komanda.Parameters.AddWithValue("@minKolicina", minKolicina)
                komanda.Parameters.AddWithValue("@maxKolicina", maxKolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function Statistika2() As String
        Dim html As New StringBuilder

        html.Append("<div class=""col-xl-3 col-lg-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-body"">")
        html.Append("<div class=""stat-widget-one"">")
        html.Append("<div class=""stat-icon dib""><i class=""ti-money text-success border-success""></i></div>")
        html.Append("<div class=""stat-content dib"">")
        html.Append("<div class=""stat-text"">Ukupan promet</div>")
        html.AppendFormat("<div class=""stat-digit"">{0} {1}</div>", Format(UkupanPromet(), "N2"), Postavke("Valuta"))
        html.Append("</div>") 'stat-content dib
        html.Append("</div>") 'stat-widget-one
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-xl-3 col-lg-6

        html.Append("<div class=""col-xl-3 col-lg-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-body"">")
        html.Append("<div class=""stat-widget-one"">")
        html.Append("<a href=""NoveNaruzbe.aspx"">")
        html.Append("<div class=""stat-icon dib""><i class=""ti-layout-grid2 text-danger border-danger""></i></div>")
        html.Append("</a>")
        html.Append("<div class=""stat-content dib"">")
        html.Append("<div class=""stat-text"">Nove narudžbe</div>")
        html.AppendFormat("<div class=""stat-digit"">{0}</div>", BrojNarudzbi(1, 0))
        html.Append("</div>") 'stat-content dib
        html.Append("</div>") 'stat-widget-one
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-xl-3 col-lg-6

        html.Append("<div class=""col-xl-3 col-lg-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-body"">")
        html.Append("<div class=""stat-widget-one"">")
        html.Append("<a href=""ZavrseneNarudzbe.aspx"">")
        html.Append("<div class=""stat-icon dib""><i class=""ti-layout-grid2 text-warning border-warning""></i></div>")
        html.Append("</a>")
        html.Append("<div class=""stat-content dib"">")
        html.Append("<div class=""stat-text"">Obrađene narudžbe</div>")
        html.AppendFormat("<div class=""stat-digit"">{0}</div>", BrojNarudzbi(1, 1))
        html.Append("</div>") 'stat-content dib
        html.Append("</div>") 'stat-widget-one
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-xl-3 col-lg-6

        html.Append("<div class=""col-xl-3 col-lg-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-body"">")
        html.Append("<div class=""stat-widget-one"">")
        html.Append("<a href=""Korisnici.aspx"">")
        html.Append("<div class=""stat-icon dib""><i class=""ti-user text-primary border-primary""></i></div>")
        html.Append("</a>")
        html.Append("<div class=""stat-content dib"">")
        html.Append("<div class=""stat-text"">Aktivni korisnika</div>")
        html.AppendFormat("<div class=""stat-digit"">{0}</div>", BrojKomitenata(1))
        html.Append("</div>") 'stat-content dib
        html.Append("</div>") 'stat-widget-one
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-xl-3 col-lg-6

        'html.Append("<div class=""col-xl-3 col-lg-6"">")
        'html.Append("<div class=""card"">")
        'html.Append("<div class=""card-body"">")
        'html.Append("<div class=""stat-widget-one"">")
        'html.Append("<div class=""stat-icon dib""><i class=""ti-user text-danger border-danger""></i></div>")
        'html.Append("<div class=""stat-content dib"">")
        'html.Append("<div class=""stat-text"">Neaktivni korisnika</div>")
        'html.AppendFormat("<div class=""stat-digit"">{0}</div>", BrojKomitenata(0))
        'html.Append("</div>") 'stat-content dib
        'html.Append("</div>") 'stat-widget-one
        'html.Append("</div>") 'card-body
        'html.Append("</div>") 'card
        'html.Append("</div>") 'col-xl-3 col-lg-6

        Return html.ToString
    End Function

    Private Shared Function BrojKomitenata(AktivanNeaktivan As Boolean) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM Korisnici WHERE Aktivan=@AktivanNeaktivan AND AdminLevel>1"
                komanda.Parameters.AddWithValue("@AktivanNeaktivan", AktivanNeaktivan)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function UkupanPromet() As Decimal
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT SUM(dbo.NarudzbeStavke.Kolicina * dbo.NarudzbeStavke.Cijena) AS Ukupno FROM dbo.NarudzbeStavke INNER JOIN dbo.Narudzbe ON dbo.NarudzbeStavke.NarudzbaID = dbo.Narudzbe.ID WHERE (dbo.Narudzbe.Naruceno = '1')"
                'komanda.Parameters.AddWithValue("@AktivanNeaktivan", AktivanNeaktivan)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Ukupno"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Private Shared Function BrojNarudzbi(Naruceno As Boolean, Poslano As Boolean) As Integer
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM Narudzbe WHERE Naruceno=@Naruceno AND Poslano=@Poslano"
                komanda.Parameters.AddWithValue("@Naruceno", Naruceno)
                komanda.Parameters.AddWithValue("@Poslano", Poslano)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Public Shared Function NoviArtikla() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim ArtikalID As Integer = HttpContext.Current.Request.QueryString("id")

        html.Append("<div class=""col-lg-9"">")
        html.Append("<div class=""card artikalDet"">")

        html.Append("<form action=""/CMS/Ajax/InsertArtikla.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

        html.Append("<div class=""card-header""><strong>Izmjena</strong> artikla</div>")
        html.Append("<div class=""card-body card-block"">")
        html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", "0")
        html.AppendFormat("<input type=""hidden"" id=""hidSkladisteId"" name=""hidSkladisteId"" value=""{0}"" class=""form-control"">", "4")

        'html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col col-md-3"">")
        'html.Append("<label for=""disabled-input"" class=""form-control-label"">RB</label>")
        'html.Append("</div>") 'col col-md-3
        'html.Append("<div class=""col-12 col-md-9"">")
        'html.AppendFormat("<input type=""text"" id=""txtRB"" name=""txtRB"" placeholder=""RB"" value=""{0}"" class=""form-control"" disabled="""">", citac("RB"))
        'html.Append("</div>") 'col-12 col-md-9
        'html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Šifra artikla</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtSifraArtikla"" name=""txtSifraArtikla"" value=""{0}"" placeholder=""šifra artikla"" class=""form-control"" />", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Bar Cod</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtBarCod"" name=""txtBarCod"" value=""{0}"" placeholder=""bar cod"" class=""form-control"" />", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Količina u ""ak2""</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtkolicina"" name=""txtkolicina"" value=""{0}"" placeholder=""kolicina"" class=""form-control"" />", "0")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Naziv artikla</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtNazivArtikla"" name=""txtNazivArtikla"" value=""{0}"" placeholder=""Naziv artikla"" class=""form-control"" />", "")
        'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Cijene IGRE.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtCijenaMPC"" name=""txtCijenaMPC"" value=""{0}"" placeholder=""Cijena MPC"" class=""form-control"" />", "0")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Stara cijena</label>")
        html.Append("</div>") 'col col-md-2
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtCijenaAkcija"" name=""txtCijenaAkcija"" value=""{0}"" placeholder=""Akcijska cijena"" class=""form-control"" >", "0")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("<div class=""col col-md-1"" style=""text-align: right;"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">B2B</label>")
        html.Append("</div>") 'col col-md-1
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtAkcija"" name=""txtAkcija"" value=""{0}"" placeholder="" B2B cijena "" class=""form-control"" >", "0")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group bgrBulk"">")
        html.Append("<div class=""col-12 col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Cijene BULK.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtCijenaMPCBulk"" name=""txtCijenaMPCBulk"" value=""{0}"" placeholder=""Cijena MPC Bulk"" class=""form-control"" />", "0")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Stara cijena</label>")
        html.Append("</div>") 'col col-md-2
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtCijenaAkcijaBulk"" name=""txtCijenaAkcijaBulk"" value=""{0}"" placeholder=""Akcijska cijena Bulk"" class=""form-control"" >", "0")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("<div class=""col col-md-1"" style=""text-align: right;"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">B2B</label>")
        html.Append("</div>") 'col col-md-1
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtAkcijaBulk"" name=""txtAkcijaBulk"" value=""{0}"" placeholder="" B2B cijena Bulk"" class=""form-control"" >", "0")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("</div>") 'row form-group

        'html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col-12 col-md-3"">")
        'html.Append("<label for=""text-input"" class=""form-control-label"">Korekcija cijene +-%</label>")
        'html.Append("</div>") 'col col-md-3
        'html.Append("<div class=""col-12 col-md-2"">")
        'html.AppendFormat("<input type=""number"" id=""txtProcenat"" name=""txtProcenat"" value=""{0}"" placeholder="" Procenat +-"" class=""form-control"">", "0")
        'html.Append("</div>") 'col-12 col-md-2
        ''html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
        ''html.Append("<label for=""text-input"" class=""form-control-label"">Stara cijena</label>")
        ''html.Append("</div>") 'col col-md-2
        ''html.Append("<div class=""col-12 col-md-2"">")
        ''html.AppendFormat("<input type=""text"" id=""txtCijenaAkcija"" name=""txtCijenaAkcija"" value=""{0}"" placeholder=""Akcijska cijena"" class=""form-control"" >", citac("AkcijaCijena"))
        ''html.Append("</div>") 'col-12 col-md-2
        ''html.Append("<div class=""col col-md-1"" style=""text-align: right;"">")
        ''html.Append("<label for=""text-input"" class=""form-control-label"">B2B</label>")
        ''html.Append("</div>") 'col col-md-1
        ''html.Append("<div class=""col-12 col-md-2"">")
        ''html.AppendFormat("<input type=""text"" id=""txtAkcija"" name=""txtAkcija"" value=""{0}"" placeholder="" B2B cijena "" class=""form-control"" >", citac("Akcija"))
        ''html.Append("</div>") 'col-12 col-md-2
        'html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label class=""form-control-label"">Oznake IGRE.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col col-md-9"">")
        html.Append("<div class=""form-check-inline form-check"">")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkIzdvojeno"" name=""chkIzdvojeno"" {0}>", "", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>Izdvojeno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkAktivno"" name=""chkAktivno"" {0}>", " checked", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkNaUpit"" name=""chkNaUpit"" {0}>", "", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>Pošaljite upit &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkBesplatnaDostava"" name=""chkBesplatnaDostava"" {0}>", "", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>FREE SHIPPING &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("</div>") '"
        html.Append("</div>") 'col col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group bgrBulk"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label class=""form-control-label"">Oznake BULK.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col col-md-9"">")
        html.Append("<div class=""form-check-inline form-check"">")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkIzdvojenoBulk"" name=""chkIzdvojenoBulk"" {0}>", "", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>Izdvojeno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkAktivnoBulk"" name=""chkAktivnoBulk"" {0}>", " checked", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkNaUpitBulk"" name=""chkNaUpitBulk"" {0}>", "", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>Pošaljite upit &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkBesplatnaDostavaBulk"" name=""chkBesplatnaDostavaBulk"" {0}>", "", "0")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label><label>FREE SHIPPING &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("</div>") '"
        html.Append("</div>") 'col col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Garancija</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-md-3"">")
        html.AppendFormat("<input type=""text"" id=""txtGarancija"" name=""txtGarancija"" value=""{0}"" placeholder="" Garancija"" class=""form-control"" />", "24 mjeseca")
        html.Append("</div>") 'col-md-3
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Isporuka</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-md-3"">")
        html.AppendFormat("<input type=""text"" id=""txtIsporuka"" name=""txtIsporuka"" value=""{0}"" placeholder="" Garancija"" class=""form-control"" />", "Isporuka 1 do 2 radnih dana")
        html.Append("</div>") 'col-md-3
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Video link</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtVideoLink"" name=""txtVideoLink"" value=""{0}"" placeholder="" video link"" class=""form-control"" />", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Kategorija IGRE.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.Append(ddlOdabranaKategorija(0, ""))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Podkategorija IGRE.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.Append(ddlOdabranaPodKategorijaBulk(0, 0, ""))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group bgrBulk"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Kategorija BULK.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.Append(ddlOdabranaKategorijaBulk(0, ""))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group bgrBulk"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Podkategorija BULK.BA</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.Append(ddlOdabranaPodKategorijaBulk(0, 0, ""))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        'If citac("Opis").Contains("<p>") = False And citac("Opis") <> "" Then
        '    html.Append("<div class=""row form-group"">")
        '    html.Append("<div class=""col col-md-3"">")
        '    html.Append("<label for=""textarea-input"" class=""form-control-label"" style=""color:red;font-weight: bold;"">Opis kopiraj i zaljepi dolje u novi!!!</label>")
        '    html.Append("</div>")
        '    html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
        '    html.AppendFormat("<textarea name=""txtOpis1"" id=""txtOpis1"" rows=""9"" placeholder=""opis..."" class=""form-control"" disabled>{0}</textarea>", citac("Opis"))
        '    html.Append("</div>")
        '    html.Append("</div>")
        'End If

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""textarea-input"" class=""form-control-label"">Kratki opis</label>")
        html.Append("</div>")
        html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
        html.AppendFormat("<textarea name=""txtOpisKratki"" id=""txtOpisKratki"" rows=""3"" placeholder="" kratki opis..."" class=""form-control"">{0}</textarea>", "")
        html.Append("</div>")
        html.Append("</div>")

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""textarea-input"" class=""form-control-label"">Opis</label>")
        html.Append("</div>")
        html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
        html.AppendFormat("<textarea name=""txtOpis"" id=""txtOpis"" rows=""9"" placeholder=""opis..."" class=""form-control tiny"">{0}</textarea>", "")
        'html.AppendFormat("<textarea name=""txtOpis"" id=""txtOpis"" rows=""9"" placeholder=""opis..."" class=""form-control"">{0}</textarea>", "")
        html.Append("</div>")
        html.Append("</div>")

        html.Append("</div>") 'card-body card-block

        html.Append("<div class=""card-footer"">")
        html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
        html.Append("<a href=""/CMS/Artikli.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
        html.Append("</div>") 'card-footer

        html.Append("</form>")

        html.Append("</div>") 'card artikalDet
        html.Append("</div>") 'col-lg-9

        html.Append("<div class=""col-lg-3 divSlike"">")
        'html.Append("<div class=""card-header""><strong>Fotografije</strong> artikla</div>")
        'html.Append("<div class=""card"">")
        'html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col-12 col-md-12"">")
        'html.AppendFormat("<form action=""Upload.aspx?id={0}&tag={1}"" method=""post"" enctype=""multipart/form-data"" class=""dropzone dropzone-area dz-clickable"" id=""dpz-remove-thumb"">", ArtikalID, NazivArtikla(ArtikalID))
        'html.Append("<input type=""file"" id=""FileUpload"" name=""FileUpload"" multiple="""" class=""form-control-file"">")
        'html.Append("<button class=""btn btn-success""><i class=""fa fa-magic""></i>&nbsp; Prenesi</button>")
        'html.Append("</form>")
        'html.Append("</div>") 'col-12 col-md-9

        'html.Append("<div class=""col-12 col-md-12""><br/>")
        'html.Append("<label for=""text-input"" class=""form-control-label"">Postojeće slike</label>")
        'html.Append("</div>") 'col-12 col-md-9
        'html.Append("<div class=""col-12 col-md-12 autocompleteSlike"" data-url=""/CMS/Ajax/SelectSlika.aspx"">")
        'html.AppendFormat("<input type=""hidden"" class=""ArtikalID"" name=""ArtikalID"" id=""ArtikalID"" value=""{0}"" />", ArtikalID)
        ''html.AppendFormat("<input type=""hidden"" class=""slikaid"" name=""slikaid"" id=""slikaid"" value=""{0}"" />", "0")
        'html.AppendFormat("<div class=""listaSlika""></div>")
        'html.Append("<input type=""text"" class=""txtSlika naziv"" id=""txtSlika"" placeholder=""naziv slike"" />")
        'html.Append("</div>") 'col-12 col-md-9

        'html.Append("</div>") 'row form-group
        'html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col-12 col-md-12"">")
        'html.Append(SlikeArtikla(ArtikalID))
        'html.Append("</div>") 'col-12 col-md-9
        'html.Append("</div>") 'row form-group
        'html.Append("</div>") 'card
        html.Append("</div>") 'col-lg-3

        Return html.ToString()
    End Function

    Private Shared Function PrebrojiArtikleStranice(Aktivno As Boolean) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsPrebrojiArtikleStranice"
                komanda.Parameters.AddWithValue("@Aktivno", Aktivno)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("BrojStranica") + 1)
                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString()
    End Function

    Public Shared Function tabelaArtikala() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sviArtikli"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-2"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""text"" id=""txtNaziv"" name=""txtNaziv"" value="""" placeholder="" ako ima u nazivu"" class=""form-control txtNaziv"">")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-2
        html.Append("<div class=""col-sm-10 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/> ")
        html.AppendFormat(" od <input type=""text"" value=""{0}"" class=""textP polje"" disabled style=""width: 75px;""/>", PrebrojiArtikleStranice(1))
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-10 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusArtikla"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikli"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        html.Append("<th>Skladište</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ArtikliMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.AppendFormat(" od <input type=""text"" value=""{0}"" class=""textP polje"" disabled style=""width: 75px;""/>", PrebrojiArtikleStranice(1))
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ArtikliMrezaTablica() As String
        Return ArtikliMrezaTablica(1, 0)
    End Function

    Private Shared Function ProvjeriDostupnostSlike(ArtikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT TOP 1 Datoteka FROM ArtikliSlike WHERE ArtikalID=@ArtikalID"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            'html.AppendFormat("<img src=""/thumb2.ashx?i={0}&w=60&h=40"" style=""width: 60px;height: 40px;"" />", citac("Datoteka"))
                            If citac("Datoteka").Contains("http") = False Then
                                html.AppendFormat("<img src=""/Thumb2.ashx?i={0}&w=400&h=400"" alt="""">", citac("Datoteka"))
                            Else
                                html.AppendFormat("<img src=""{0}"" alt="""" width='400px'>", citac("Datoteka"))
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString.Length < 1 Then
            html.Append("")
        Else
            'html.Clear()
            'html.Append("<i class=""fa fa-picture-o""></i>")
        End If

        Return html.ToString()
    End Function

    Public Shared Function ArtikliMrezaTablica(stranica As Integer, naziv As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikala"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@Naziv", naziv)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            html.AppendFormat("<td>{0}</td>", citac("Skladiste"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaArtikalaNa() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sviArtikliNa"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        'html.Append("<div class=""row slovoArtikla"">")
        'html.Append("<div class=""col-sm-12 col-md-12"">")
        'html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        'html.AppendFormat("<input type=""button"" value=""{0}"" class=""dugmic slovo"" id=""{0}"" />", "SVE2")
        'Using konekcija As New SqlConnection(putanja)
        '    konekcija.Open()
        '    Using komanda As New SqlCommand()
        '        komanda.Connection = konekcija
        '        komanda.CommandType = CommandType.Text
        '        komanda.CommandText = "SELECT TOP (100) PERCENT LEFT(NazivArtikla, '1') AS SlovoArtikla FROM dbo.Artikli WHERE Aktivno='0' GROUP BY LEFT(NazivArtikla, '1') ORDER BY SlovoArtikla"
        '        'komanda.Parameters.AddWithValue("@Stranica", stranica)
        '        Using citac As SqlDataReader = komanda.ExecuteReader()
        '            If citac IsNot Nothing Then
        '                While citac.Read()
        '                    html.AppendFormat("<input type=""button"" value=""{0}"" class=""dugmic slovo"" id=""{0}"" />", citac("SlovoArtikla"))
        '                End While
        '            End If
        '        End Using
        '    End Using
        'End Using
        'html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        'html.Append("</div>") 'col-sm-12 col-md-7
        'html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-2"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""text"" id=""txtNaziv"" name=""txtNaziv"" value="""" placeholder="" ako ima u nazivu"" class=""form-control txtNaziv"">")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-2
        html.Append("<div class=""col-sm-10 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.AppendFormat(" od <input type=""text"" value=""{0}"" class=""textP polje"" disabled style=""width: 75px;""/>", PrebrojiArtikleStranice(0))
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-10 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusArtikla"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikli"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        html.Append("<th>Skladište</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ArtikliMrezaTablicaNa())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        'html.Append("<div class=""col-sm-12 col-md-5"">")
        'html.Append("<div class=""dataTables_info"" id=""bootstrap-data-table_info"" role=""status"" aria-live=""polite"">Showing 1 to 10 of 35 entries</div>")
        'html.Append("</div>") 'col-sm-12 col-md-5
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.AppendFormat(" od <input type=""text"" value=""{0}"" class=""textP polje"" disabled style=""width: 75px;""/>", PrebrojiArtikleStranice(0))
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""SVE"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row


        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ArtikliMrezaTablicaNa() As String
        Return ArtikliMrezaTablicaNa(1, 0)
    End Function

    Public Shared Function ArtikliMrezaTablicaNa(stranica As Integer, naziv As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikalaNa"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@Naziv", naziv)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            html.AppendFormat("<td>{0}</td>", citac("Skladiste"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function IzmjenaArtikla() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim ArtikalID As Integer = HttpContext.Current.Request.QueryString("id")

        html.Append("<div class=""col-lg-9"">")
        html.Append("<div class=""card artikalDet"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT dbo.Artikli.*, dbo.Skladista.Skladiste FROM dbo.Artikli INNER JOIN dbo.Skladista ON dbo.Artikli.SkladisteID = dbo.Skladista.ID WHERE dbo.Artikli.ID=@ArtikalID"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<form action=""/CMS/Ajax/UpdateArtikla.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

                            html.Append("<div class=""card-header""><strong>Izmjena</strong> artikla</div>")
                            html.Append("<div class=""card-body card-block"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))
                            html.AppendFormat("<input type=""hidden"" id=""hidSkladisteId"" name=""hidSkladisteId"" value=""{0}"" class=""form-control"">", citac("SkladisteID"))

                            'html.Append("<div class=""row form-group"">")
                            'html.Append("<div class=""col col-md-3"">")
                            'html.Append("<label for=""disabled-input"" class=""form-control-label"">RB</label>")
                            'html.Append("</div>") 'col col-md-3
                            'html.Append("<div class=""col-12 col-md-9"">")
                            'html.AppendFormat("<input type=""text"" id=""txtRB"" name=""txtRB"" placeholder=""RB"" value=""{0}"" class=""form-control"" disabled="""">", citac("RB"))
                            'html.Append("</div>") 'col-12 col-md-9
                            'html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Šifra artikla</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.AppendFormat("<input type=""text"" id=""txtSifraArtikla"" name=""txtSifraArtikla"" value=""{0}"" placeholder=""šifra artikla"" class=""form-control"" disabled="""">", citac("SifraRobe"))
                            html.Append("</div>") 'col-12 col-md-3
                            html.Append("<div class=""col col-md-1"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Skladište</label>")
                            html.Append("</div>") 'col col-md-1
                            html.Append("<div class=""col-12 col-md-1"">")
                            html.AppendFormat("<input type=""text"" id=""txtSkladiste"" name=""txtSkladiste"" value=""{0}"" placeholder="" skladiste"" class=""form-control"" disabled="""">", citac("Skladiste"))
                            html.Append("</div>") 'col-12 col-md-1
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Bar Cod</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtBarCod"" name=""txtBarCod"" value=""{0}"" placeholder=""bar cod"" class=""form-control"" disabled="""">", citac("BarCod"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            If citac("SkladisteID") = 1 Then
                                html.Append("<label for=""text-input"" class=""form-control-label"">Količina u ""ak2""</label>")
                            Else
                                html.Append("<label for=""text-input"" class=""form-control-label"">Količina u dobavlajča</label>")
                            End If
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.AppendFormat("<input type=""text"" id=""txtkolicina"" name=""txtkolicina"" value=""{0}"" placeholder=""kolicina u wand-u"" class=""form-control"" >", citac("Kolicina"))
                            html.Append("</div>") 'col-12 col-md-3
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkFixnaKolicina"" name=""chkFixnaKolicina"" {0}>", If(citac("FixnaKolicina") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Fixna količina &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Naziv artikla</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtNazivArtikla"" name=""txtNazivArtikla"" value=""{0}"" placeholder=""Naziv artikla"" class=""form-control"" >", Replace(citac("Naziv"), """", ""))
                            'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Cijene IGRE.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtCijenaMPC"" name=""txtCijenaMPC"" value=""{0}"" placeholder=""Cijena MPC"" class=""form-control"" >", citac("Cijena")) 'disabled
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Stara cijena</label>")
                            html.Append("</div>") 'col col-md-2
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtCijenaAkcija"" name=""txtCijenaAkcija"" value=""{0}"" placeholder=""Akcijska cijena"" class=""form-control"" >", citac("AkcijaCijena"))
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("<div class=""col col-md-1"" style=""text-align: right;"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">B2B</label>")
                            html.Append("</div>") 'col col-md-1
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtAkcija"" name=""txtAkcija"" value=""{0}"" placeholder="" B2B cijena "" class=""form-control"" >", citac("Akcija"))
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group bgrBulk"">")
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Cijene BULK.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtCijenaMPCBulk"" name=""txtCijenaMPCBulk"" value=""{0}"" placeholder=""Cijena MPC Bulk"" class=""form-control"" >", citac("CijenaBulk")) 'disabled
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Stara cijena</label>")
                            html.Append("</div>") 'col col-md-2
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtCijenaAkcijaBulk"" name=""txtCijenaAkcijaBulk"" value=""{0}"" placeholder=""Akcijska cijena Bulk"" class=""form-control"" >", citac("AkcijaCijenaBulk"))
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("<div class=""col col-md-1"" style=""text-align: right;"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">B2B</label>")
                            html.Append("</div>") 'col col-md-1
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtAkcijaBulk"" name=""txtAkcijaBulk"" value=""{0}"" placeholder="" B2B cijena Bulk"" class=""form-control"" >", citac("AkcijaBulk"))
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("</div>") 'row form-group

                            'html.Append("<div class=""row form-group"">")
                            'html.Append("<div class=""col-12 col-md-3"">")
                            'html.Append("<label for=""text-input"" class=""form-control-label"">Korekcija cijene +-%</label>")
                            'html.Append("</div>") 'col col-md-3
                            'html.Append("<div class=""col-12 col-md-2"">")
                            'html.AppendFormat("<input type=""number"" id=""txtProcenat"" name=""txtProcenat"" value=""{0}"" placeholder="" Procenat +-"" class=""form-control"">", citac("Procenat"))
                            'html.Append("</div>") 'col-12 col-md-2
                            ''html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
                            ''html.Append("<label for=""text-input"" class=""form-control-label"">Stara cijena</label>")
                            ''html.Append("</div>") 'col col-md-2
                            ''html.Append("<div class=""col-12 col-md-2"">")
                            ''html.AppendFormat("<input type=""text"" id=""txtCijenaAkcija"" name=""txtCijenaAkcija"" value=""{0}"" placeholder=""Akcijska cijena"" class=""form-control"" >", citac("AkcijaCijena"))
                            ''html.Append("</div>") 'col-12 col-md-2
                            ''html.Append("<div class=""col col-md-1"" style=""text-align: right;"">")
                            ''html.Append("<label for=""text-input"" class=""form-control-label"">B2B</label>")
                            ''html.Append("</div>") 'col col-md-1
                            ''html.Append("<div class=""col-12 col-md-2"">")
                            ''html.AppendFormat("<input type=""text"" id=""txtAkcija"" name=""txtAkcija"" value=""{0}"" placeholder="" B2B cijena "" class=""form-control"" >", citac("Akcija"))
                            ''html.Append("</div>") 'col-12 col-md-2
                            'html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label class=""form-control-label"">Oznake IGRE.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col col-md-9"">")
                            html.Append("<div class=""form-check-inline form-check"">")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkIzdvojeno"" name=""chkIzdvojeno"" {0}>", If(citac("Izdvojeno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Izdvojeno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkAktivno"" name=""chkAktivno"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkNaUpit"" name=""chkNaUpit"" {0}>", If(citac("NaUpit") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Pošaljite upit &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkBesplatnaDostava"" name=""chkBesplatnaDostava"" {0}>", If(citac("BesplatnaDostava") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>FREE SHIPPING &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkCijenaFixna"" name=""chkCijenaFixna"" {0}>", If(citac("CijenaFixna") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>FXNA CIJENA &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("</div>") '"
                            html.Append("</div>") 'col col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group bgrBulk"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label class=""form-control-label"">Oznake BULK.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col col-md-9"">")
                            html.Append("<div class=""form-check-inline form-check"">")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkIzdvojenoBulk"" name=""chkIzdvojenoBulk"" {0}>", If(citac("IzdvojenoBulk") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Izdvojeno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkAktivnoBulk"" name=""chkAktivnoBulk"" {0}>", If(citac("AktivnoBulk") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkNaUpitBulk"" name=""chkNaUpitBulk"" {0}>", If(citac("NaUpitBulk") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>Pošaljite upit &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkBesplatnaDostavaBulk"" name=""chkBesplatnaDostavaBulk"" {0}>", If(citac("BesplatnaDostavaBulk") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>FREE SHIPPING &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" id=""chkCijenaFixnaBulk"" name=""chkCijenaFixnaBulk"" {0}>", If(citac("CijenaFixnaBulk") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label><label>FXNA CIJENA &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("</div>") '"
                            html.Append("</div>") 'col col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Polje 1</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtPolje1"" name=""txtPolje1"" value=""{0}"" placeholder="" "" class=""form-control"" />", citac("Polje1"))
                            html.Append("</div>") 'col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Polje 2</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtPolje2"" name=""txtPolje2"" value=""{0}"" placeholder="" "" class=""form-control"" />", citac("Polje2"))
                            html.Append("</div>") 'col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Polje 3</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtPolje3"" name=""txtPolje3"" value=""{0}"" placeholder="" "" class=""form-control"" />", citac("Polje3"))
                            html.Append("</div>") 'col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Garancija</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-md-3"">")
                            html.AppendFormat("<input type=""text"" id=""txtGarancija"" name=""txtGarancija"" value=""{0}"" placeholder="" Garancija"" class=""form-control"" />", citac("Garancija"))
                            html.Append("</div>") 'col-md-3
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Isporuka</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-md-3"">")
                            html.AppendFormat("<input type=""text"" id=""txtIsporuka"" name=""txtIsporuka"" value=""{0}"" placeholder="" Garancija"" class=""form-control"" />", citac("Isporuka"))
                            html.Append("</div>") 'col-md-3
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Video link</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtVideoLink"" name=""txtVideoLink"" value=""{0}"" placeholder="" video link"" class=""form-control"" />", citac("VideoLink"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Kategorija IGRE.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlOdabranaKategorija(citac("NadGrupaID"), "")) 'disabled
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Podkategorija IGRE.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlOdabranaPodKategorija(citac("GrupaID"), citac("NadGrupaID"), "")) 'disabled
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group bgrBulk"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Kategorija BULK.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlOdabranaKategorijaBulk(citac("NadGrupaIDBulk"), "")) 'disabled
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group bgrBulk"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Podkategorija BULK.BA</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlOdabranaPodKategorijaBulk(citac("GrupaIDBulk"), citac("NadGrupaIDBulk"), "")) 'disabled
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            'If citac("Opis").Contains("<p>") = False And citac("Opis") <> "" Then
                            '    html.Append("<div class=""row form-group"">")
                            '    html.Append("<div class=""col col-md-3"">")
                            '    html.Append("<label for=""textarea-input"" class=""form-control-label"" style=""color:red;font-weight: bold;"">Opis kopiraj i zaljepi dolje u novi!!!</label>")
                            '    html.Append("</div>")
                            '    html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
                            '    html.AppendFormat("<textarea name=""txtOpis1"" id=""txtOpis1"" rows=""9"" placeholder=""opis..."" class=""form-control"" disabled>{0}</textarea>", citac("Opis"))
                            '    html.Append("</div>")
                            '    html.Append("</div>")
                            'End If

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Kratki opis</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<textarea name=""txtOpisKratki"" id=""txtOpisKratki"" rows=""3"" placeholder="" kratki opis..."" class=""form-control"">{0}</textarea>", citac("OpisKratki"))
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Opis</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<textarea name=""txtOpis"" id=""txtOpis"" rows=""9"" placeholder=""opis..."" class=""form-control tiny"">{0}</textarea>", citac("Opis"))
                            'html.AppendFormat("<textarea name=""txtOpis"" id=""txtOpis"" rows=""9"" placeholder=""opis..."" class=""form-control"">{0}</textarea>", citac("Opis"))
                            html.Append("</div>")
                            html.Append("</div>")

                            'Vezani artikli
                            html.AppendFormat(VezaniArtikliBulk())

                            html.Append("</div>") 'card-body card-block

                            html.Append("<div class=""card-footer"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            html.Append("<a href=""/CMS/Artikli.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</div>") 'card artikalDet

        'POPUSTI
        html.Append("<div class=""card artikalPopusti"">")
        html.Append("<div class=""card-body"" id=""divPopusti"">")
        html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col col-md-12"">")
        'html.AppendFormat("<label for=""text-input"" class=""form-control-label"">KOLIČINSKI POPUSTI</label>")
        'html.Append("</div>") 'col col-md-12
        html.Append("<div class=""col col-md-12"">")
        html.Append("<div class=""tblPopusti"">")
        html.Append(boxPopusti(ArtikalID))
        html.Append("</div>") 'tblVelicine
        html.Append("<div Class=""card-body card-block"">")
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""hidden"" name=""hidArtikalId"" value=""{0}"" class=""form-control hidArtikalId"" />", ArtikalID)
        html.AppendFormat("<input type=""text"" id=""txtKolicina"" name=""txtKolicina"" value=""{0}"" class=""form-control"" placeholder="" kom"" autocomplete=""off""/>", "")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("<div class=""col-12 col-md-2"">")
        html.AppendFormat("<input type=""text"" id=""txtPopust"" name=""txtPopust"" value=""{0}"" class=""form-control"" placeholder="" KM"" autocomplete=""off""/>", "")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("<div class=""col-12 col-md-2"">")
        html.Append("<button type=""button"" class=""btn btn-primary btnSpremiPopust""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
        html.Append("</div>") 'col-12 col-md-2
        html.Append("</div>") 'card-body card-block
        html.Append("</div>") 'col col-md-12
        html.Append("</div>") 'row form-group
        html.Append("</div>") 'card-body card-block
        html.Append("</div>") 'card artikalDet

        html.Append("</div>") 'col-lg-9

        html.Append("<div class=""col-lg-3 divSlike"">")
        html.Append("<div class=""card-header""><strong>Fotografije</strong> artikla</div>")
        html.Append("<div class=""card"">")
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.AppendFormat("<form action=""Upload.aspx?id={0}&tag={1}"" method=""post"" enctype=""multipart/form-data"" class=""dropzone dropzone-area dz-clickable"" id=""dpz-remove-thumb"">", ArtikalID, NazivArtikla(ArtikalID))
        html.Append("<input type=""file"" id=""FileUpload"" name=""FileUpload"" multiple="""" class=""form-control-file"">")
        html.Append("<button class=""btn btn-success""><i class=""fa fa-magic""></i>&nbsp; Prenesi</button>")
        html.Append("</form>")
        html.Append("</div>") 'col-12 col-md-9

        html.Append("<div class=""col-12 col-md-12""><br/>")
        html.Append("<label for=""text-input"" class=""form-control-label"">Postojeće slike</label>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("<div class=""col-12 col-md-12 autocompleteSlike"" data-url=""/CMS/Ajax/SelectSlika.aspx"">")
        html.AppendFormat("<input type=""hidden"" class=""ArtikalID"" name=""ArtikalID"" id=""ArtikalID"" value=""{0}"" />", ArtikalID)
        'html.AppendFormat("<input type=""hidden"" class=""slikaid"" name=""slikaid"" id=""slikaid"" value=""{0}"" />", "0")
        html.AppendFormat("<div class=""listaSlika""></div>")
        html.Append("<input type=""text"" class=""txtSlika naziv"" id=""txtSlika"" placeholder=""naziv slike"" />")
        html.Append("</div>") 'col-12 col-md-9

        html.Append("</div>") 'row form-group
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.Append(SlikeArtikla(ArtikalID))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group
        html.Append("</div>") 'card
        html.Append("</div>") 'col-lg-3

        Return html.ToString()
    End Function


    Public Shared Function VezaniArtikliBulk() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim idArtikla As Integer = HttpContext.Current.Request.QueryString("id")


        html.Append("<div class=""row form-group align-items-center mb-3 autocompleteVezaniArtikli"">")
        html.AppendFormat("<input type='hidden' id='hidArtikalID' class='hidArtikalID' value='{0}'/>", idArtikla)
        html.Append("<div class=""col-md-3"">")
        html.Append("<label for=""txtGrupa"" class=""form-control-label"">Dodavanje vezanih artikala</label>")
        html.Append("</div>")
        html.Append("<div class=""col-md-6"">")
        html.Append("<input type=""text"" id=""txtGrupa"" name=""txtGrupa"" value="""" placeholder=""Šifra ili Naziv"" class=""form-control txtGrupa"" autocomplete=""off"" />")
        html.Append("<div class=""listaGrupe""></div>")
        html.Append("</div>")
        html.Append("</div>")

        html.Append("<label><b>Već dodani artikli:</b></label>")
        html.Append("<hr/>")
        html.Append("<table>")
        html.Append("<tbody>")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsVezaniArtikliBulk"
                komanda.Parameters.AddWithValue("@ArtikalID", idArtikla)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<tr>")
                            html.AppendFormat("<td style='padding: 10px;'>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("Naziv"))
                            html.AppendFormat("<td><a href='/CMS/Ajax/IzbrisiVezaniArtikalBulk.aspx?id={0}' class='btn btn-sm btn-danger obrisi-vezani' style='margin-bottom: 5px;'>Obriši</a></td>", citac("glavniID"))
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        html.Append("</tbody>")
        html.Append("</table>")

        Return html.ToString()
    End Function


    Public Shared Function AutocompleteArtikala() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim Pojam As String = HttpContext.Current.Request.QueryString("Pojam")
        Dim artikalid As Integer = HttpContext.Current.Request.QueryString("artikalid")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "AutocompleteArtikla"
                komanda.Parameters.AddWithValue("@Pojam", Pojam)
                komanda.Parameters.AddWithValue("@artikalid", artikalid)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<div class=""redakT trazeniPojam"" data-id=""{0}"">", citac("ID"))
                            html.AppendFormat("<b>&#10134 {0} &#10134 {1}</b>", citac("SifraRobe"), citac("Naziv"))
                            html.Append("<span class=""ocisti""></span>")
                            html.Append("</div>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function boxPopusti(artikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""card-header""><strong>KOLIČINSKI POPUSTI</strong> artikla</div>")
        html.Append("<div class=""card-body card-block"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliPopusti WHERE ArtikalID=@ArtikalID ORDER BY Kolicina ASC;"
                komanda.Parameters.AddWithValue("@ArtikalID", artikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<div class=""row"" style=""border:  1px dotted #dfdfdf;margin-bottom: 2px; padding: 5px 0px;"">")
                            html.Append("<div class=""col-lg-1"">")
                            html.AppendFormat("<input class=""btn btn-danger btn-sm btnUkloniPopust"" type=""button"" value=""X"" data-id=""{0}"" data-artikal=""{1}"" title=""BRIŠI"" style=""font-size: 10px;""/>", citac("ID"), artikalID)
                            html.Append("</div>") 'col-lg-1
                            html.Append("<div class=""col-lg-2"">")
                            html.AppendFormat("{0} kom", citac("Kolicina"))
                            html.Append("</div>") 'col-lg-2
                            html.Append("<div class=""col-lg-2"">")
                            html.AppendFormat("{0} KM", citac("Popust"))
                            html.Append("</div>") 'col-lg-2
                            html.Append("</div>") 'row
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</div>") 'card-body card-block

        Return html.ToString()
    End Function

    Public Shared Function ddlOdabranaKategorija(KategorijaID As Integer, DisEna As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.AppendFormat("<select name=""ddlKategorija"" id=""ddlKategorija"" class=""form-control ddlKategorija"" data-dis=""{0}"" {0}>", DisEna)
        html.Append("<option value=""0"">Odaberite kategoriju</option>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliNadGrupe ORDER BY NadGrupa ASC"
                'komanda.Parameters.AddWithValue("@SifraKategorije", KlasaSifra)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" {2}>{1}</option>", citac("ID"), citac("NadGrupa"), If(KategorijaID = citac("ID"), " selected", ""))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Public Shared Function ddlOdabranaKategorijaBulk(KategorijaID As Integer, DisEna As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.AppendFormat("<select name=""ddlKategorijaBulk"" id=""ddlKategorijaBulk"" class=""form-control ddlKategorijaBulk"" data-dis=""{0}"" {0}>", DisEna)
        html.Append("<option value=""0"">Odaberite kategoriju</option>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliNadGrupeBulk ORDER BY NadGrupa ASC"
                'komanda.Parameters.AddWithValue("@SifraKategorije", KlasaSifra)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" {2}>{1}</option>", citac("ID"), citac("NadGrupa"), If(KategorijaID = citac("ID"), " selected", ""))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Public Shared Function ddlOdabranaPodKategorija(PodKategorijaID As Integer, KategorijaID As Integer, DisEna As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.AppendFormat("<select name=""ddlPodKategorija"" id=""ddlPodKategorija"" class=""form-control ddlPodKategorija"" data-dis=""{0}"" {0}>", DisEna)
        html.Append("<option value=""0"">Odaberite podkategoriju</option>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupe WHERE NadGrupaID=@KategorijaID ORDER BY Grupa"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}""{2}>{1}</option>", citac("ID"), citac("Grupa"), If(PodKategorijaID = citac("ID"), " selected", ""))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Public Shared Function ddlOdabranaPodKategorijaBulk(PodKategorijaID As Integer, KategorijaID As Integer, DisEna As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.AppendFormat("<select name=""ddlPodKategorijaBulk"" id=""ddlPodKategorijaBulk"" class=""form-control ddlPodKategorijaBulk"" data-dis=""{0}"" {0}>", DisEna)
        html.Append("<option value=""0"">Odaberite podkategoriju</option>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeBulk WHERE NadGrupaID=@KategorijaID ORDER BY Grupa"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}""{2}>{1}</option>", citac("ID"), citac("Grupa"), If(PodKategorijaID = citac("ID"), " selected", ""))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Private Shared Function SlikeArtikla(ArtikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliSlike WHERE ArtikalID=@ArtikalID ORDER BY Zadana DESC"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<div class=""card text-white bg-flat-color-1"">")
                            html.Append("<div class=""card-body pb-0"" style=""padding: 0rem; overflow: hidden;"">")
                            html.Append("<div class=""dropdown float-right"">")
                            html.AppendFormat("{0}", citac("Datoteka"))
                            html.Append("<br/><button class=""btn bg-transparent dropdown-toggle theme-toggle text-light"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"" style=""float: right;"">")
                            html.Append("<i class=""fa fa-cog""></i>")
                            html.Append("</button>")
                            html.Append("<div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">")
                            html.Append("<div class=""dropdown-menu-content"">")
                            html.AppendFormat("<a class=""dropdown-item"" href=""/CMS/Ajax/ZadanaSlika.aspx?SlikaID={0}&ArtikalID={1}"">Postavi kao zadanu</a>", citac("ID"), ArtikalID)
                            html.AppendFormat("<a class=""dropdown-item"" href=""/CMS/Ajax/IzbrisiSliku.aspx?SlikaID={0}&ArtikalID={1}&file={2}"">Izbriši sliku</a>", citac("ID"), ArtikalID, citac("Datoteka"))
                            html.Append("</div>") 'dropdown-menu-content
                            html.Append("</div>") 'dropdown-menu
                            html.Append("</div>") 'dropdown float-right
                            If citac("Datoteka").Contains("http") = False Then
                                html.AppendFormat("<img class=""card-img-top"" src=""/Thumb2.ashx?i={0}&w=400&h=400"" alt="""">", citac("Datoteka"))
                            Else
                                html.AppendFormat("<img class=""card-img-top"" src=""{0}"" alt="""">", citac("Datoteka"))
                            End If
                            'html.AppendFormat("<img class=""card-img-top"" src=""/Datoteke/Artikli/{0}"" alt=""CIAK"">", citac("Datoteka"))
                            'html.Append("<div class=""chart-wrapper px-0"" style=""height: 70px;"" height=""70"">")
                            ''html.Append("<canvas id=""widgetChart1""></canvas>")
                            'html.Append("</div>") 'chart-wrapper px-0
                            html.Append("</div>") 'card-body pb-0
                            html.Append("</div>") 'card text-white bg-flat-color-1
                            'html.AppendFormat("<img class=""card-img-top"" src=""/Datoteke/Artikli/{0}"" alt=""CIAK"">", citac("Datoteka"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function GrupeArtikala() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row GrupeArtikala"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>Kategorija</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        'html.Append("<th>Prioritet</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(KategorijeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtikleGrupe(KategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM [dbo].[Artikli] WHERE NadGrupaID=@KategorijaID"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtikleGrupeDostupne(KategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT SUM(Kolicina) AS Broj FROM [dbo].[Artikli] WHERE NadGrupaID=@KategorijaID AND Kolicina>0"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using
        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Public Shared Function KategorijeMrezaTablica() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliNadGrupe ORDER BY NadGrupa ASC"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraNadgrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Podkategorija.aspx?KategorijaID={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("NadGrupa"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtikleGrupe(citac("ID")), PrebrojiArtikleGrupeDostupne(citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function PodKategorijeArtikala() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row PodKategorijeArtikala"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblPodKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>Podkategorija</th>")
        html.Append("<th>Prioritet</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")
        html.Append(PodKategorijeMrezaTablica(KategorijaID))
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtiklePodGrupe(PodKategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM [dbo].[Artikli] WHERE GrupaID=@PodKategorijaID"
                komanda.Parameters.AddWithValue("@PodKategorijaID", PodKategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtiklePodGrupeDostupne(PodKategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT SUM(Kolicina) AS Broj FROM [dbo].[Artikli] WHERE GrupaID=@PodKategorijaID AND Kolicina>0"
                komanda.Parameters.AddWithValue("@PodKategorijaID", PodKategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using
        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Public Shared Function PodKategorijeMrezaTablica(KategorijaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupe WHERE NadGrupaID=@KategorijaID ORDER BY Prioritet ASC"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraGrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""ArtikliPodKategorijeCMS.aspx?PodKategorijaID={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Grupa"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>")
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetUp"" data-id=""{0}"" data-prioritet=""{1}"" data-nadgrupa=""{2}""><i class=""fa fa-upload""></i></button>", citac("ID"), citac("Prioritet"), KategorijaID)
                            html.AppendFormat("<span class=""badge badge-light"" style=""padding: 10px; margin: 0px 3px;"">{0}</span>", citac("Prioritet"), citac("ID"))
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetDown"" data-id=""{0}"" data-prioritet=""{1}"" data-nadgrupa=""{2}""><i class=""fa fa-download""></i></button>", citac("ID"), citac("Prioritet"), KategorijaID)
                            html.AppendFormat("</td>")
                            html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtiklePodGrupe(citac("ID")), PrebrojiArtiklePodGrupeDostupne(citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaArtikliPodKategorije() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()
        Dim PodKategorijaID As Integer = HttpContext.Current.Request.QueryString("PodKategorijaID")

        html.Append("<div class=""row sviArtikliPodKategorije"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.AppendFormat("<input type=""hidden"" value=""{0}"" class=""polje PodKategorijaID"" />", PodKategorijaID)
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusArtikla"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikli"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        html.Append("<th>Skladište</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(MrezaArtikliPodKategorije())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.AppendFormat("<input type=""hidden"" value=""{0}"" class=""polje PodKategorijaID"" />", PodKategorijaID)
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function MrezaArtikliPodKategorije() As String
        Dim PodKategorijaID As Integer = HttpContext.Current.Request.QueryString("PodKategorijaID")
        Return MrezaArtikliPodKategorije(1, PodKategorijaID)
    End Function

    Public Shared Function MrezaArtikliPodKategorije(stranica As Integer, PodKategorijaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim PodKategorijaID As Integer = HttpContext.Current.Request.QueryString("PodKategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikalaPodKategorije"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@PodKategorijaID", PodKategorijaID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            html.AppendFormat("<td>{0}</td>", citac("Skladiste"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    'BULK Grupe
    Public Shared Function GrupeArtikalaBulk() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row GrupeArtikalaBulk"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>Kategorija</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        'html.Append("<th>Prioritet</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(KategorijeMrezaTablicaBulk())
        html.Append("</tbody>")
        html.Append("<form action=""/CMS/Ajax/DodajKategorijuBulk.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
        html.Append("<tfoot>")
        html.Append("<tr>")
        html.Append("<th><!--<input type=""text"" id=""txtSifraKaregorije"" name=""txtSifraKaregorije"" value="""" placeholder=""šifra"" class=""form-control"">--></th>")
        html.Append("<th><input type=""text"" id=""txtKaregorija"" name=""txtKaregorija"" value="""" placeholder=""Nova kategorija"" class=""form-control""></th>")
        html.Append("<th><button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button></th>")
        html.Append("<th>&nbsp;</th>")
        html.Append("<th>&nbsp;</th>")
        'html.Append("<th>Prioritet</th>")
        html.Append("</tr>")
        html.Append("</tfoot>")
        html.Append("</form>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function


    Private Shared Function PrebrojiArtikleGrupeBulk(KategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM [dbo].[Artikli] WHERE NadGrupaIDBulk=@KategorijaID"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtikleGrupeDostupneBulk(KategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT SUM(Kolicina) AS Broj FROM [dbo].[Artikli] WHERE NadGrupaIDBulk=@KategorijaID AND Kolicina>0"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using
        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Public Shared Function KategorijeMrezaTablicaBulk() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliNadGrupeBulk ORDER BY NadGrupa ASC"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraNadgrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<button type=""button"" class=""btn btn-secondary mb-1"" data-toggle=""modal"" data-target=""#smallmodal{0}""><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button> &nbsp;", citac("ID"))
                            html.AppendFormat("<a href=""PodkategorijaBulk.aspx?KategorijaID={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("NadGrupa"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtikleGrupeBulk(citac("ID")), PrebrojiArtikleGrupeDostupneBulk(citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                            'modal izmjena
                            html.AppendFormat("<div class=""modal fade"" id=""smallmodal{0}"" tabindex=""-1"" role=""dialog"" aria-labelledby=""smallmodalLabel"" aria-hidden=""true"">", citac("ID"))
                            html.Append("<div class=""modal-dialog modal-sm"" role=""document"">")
                            html.Append("<div class=""modal-content modPass"">")
                            html.AppendFormat("<form action=""/CMS/Ajax/PromjeniArtikliNadGrupeBulk.aspx"" id=""add-project-form"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
                            html.Append("<div class=""modal-header"">")
                            html.Append("<h5 class=""modal-title"" id=""smallmodalLabel"">Izmjena naziva kategorije</h5>")
                            html.Append("<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">")
                            html.Append("<span aria-hidden=""true"">&times;</span>")
                            html.Append("</button>")
                            html.Append("</div>") 'modal-header
                            html.Append("<div class=""modal-body"">")
                            html.Append("<p>")
                            html.AppendFormat("<input type=""hidden"" id=""slikaID"" name=""slikaID"" value=""{0}"" class=""form-control"">", citac("ID"))
                            html.AppendFormat("<input type=""hidden"" id=""stariNaziv"" name=""stariNaziv"" value=""{0}"" class=""form-control"">", citac("NadGrupa"))
                            html.AppendFormat("<input type=""text"" id=""noviNaziv"" name=""noviNaziv"" value=""{0}"" placeholder=""unesite novi link"" class=""form-control"" >", citac("NadGrupa"))
                            html.Append("</p>")
                            html.Append("</div>") 'modal-body
                            html.Append("<div class=""modal-footer"">")
                            html.Append("<button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Odustani</button>")
                            html.Append("<button type=""submit"" class=""btn btn-primary btnIzmjeniPass"">Izmjeni</button>")
                            html.Append("</div>") 'modal-footer
                            html.Append("</form>")
                            html.Append("</div>") 'modal-content
                            html.Append("</div>") 'modal-dialog modal-sm
                            html.Append("</div>") 'modal fade
                            'modal izmjena
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function PodKategorijeArtikalaBulk() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row PodKategorijeArtikalaBulk"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblPodKategorijeBulk"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>Podkategorija</th>")
        html.Append("<th>Prioritet</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")
        html.Append(PodKategorijeMrezaTablicaBulk(KategorijaID))
        html.Append("</tbody>")
        html.Append("<form action=""/CMS/Ajax/DodajPodKategorijuBulk.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
        html.Append("<tfoot>")
        html.Append("<tr>")
        html.AppendFormat("<th>")
        html.AppendFormat("<input type=""hidden"" id=""hidKaregorija"" name=""hidKaregorija"" value=""{0}"">", KategorijaID)
        html.AppendFormat("<!--<input type=""text"" id=""txtSifraPodKaregorije"" name=""txtSifraPodKaregorije"" value="""" placeholder=""šifra"" Class=""form-control"">-->")
        html.AppendFormat("</th>")
        html.Append("<th><input type=""text"" id=""txtPodKaregorija"" name=""txtPodKaregorija"" value="""" placeholder=""Nova podkategorija"" Class=""form-control""></th>")
        html.Append("<th><button type=""submit"" Class=""btn btn-primary btn-sm""><i Class=""fa fa-dot-circle-o""></i> Spremi</button></th>")
        html.Append("<th>&nbsp;</th>")
        html.Append("<th>&nbsp;</th>")
        'html.Append("<th>Prioritet</th>")
        html.Append("</tr>")
        html.Append("</tfoot>")
        html.Append("</form>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtiklePodGrupeBulk(PodKategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM [dbo].[Artikli] WHERE GrupaIDBulk=@PodKategorijaID"
                komanda.Parameters.AddWithValue("@PodKategorijaID", PodKategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Private Shared Function PrebrojiArtiklePodGrupeDostupneBulk(PodKategorijaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT SUM(Kolicina) AS Broj FROM [dbo].[Artikli] WHERE GrupaIDBulk=@PodKategorijaID AND Kolicina>0"
                komanda.Parameters.AddWithValue("@PodKategorijaID", PodKategorijaID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using
        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Public Shared Function PodKategorijeMrezaTablicaBulk(KategorijaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeBulk WHERE NadGrupaID=@KategorijaID ORDER BY Prioritet ASC"
                komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraGrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<button type=""button"" class=""btn btn-secondary mb-1"" data-toggle=""modal"" data-target=""#smallmodal{0}""><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button> &nbsp;", citac("ID"))
                            html.AppendFormat("<a href=""ArtikliPodKategorijeCMSBulk.aspx?PodKategorijaID={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Grupa"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>")
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetUp"" data-id=""{0}"" data-prioritet=""{1}"" data-nadgrupa=""{2}""><i class=""fa fa-upload""></i></button>", citac("ID"), citac("Prioritet"), KategorijaID)
                            html.AppendFormat("<span class=""badge badge-light"" style=""padding: 10px; margin: 0px 3px;"">{0}</span>", citac("Prioritet"), citac("ID"))
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetDown"" data-id=""{0}"" data-prioritet=""{1}"" data-nadgrupa=""{2}""><i class=""fa fa-download""></i></button>", citac("ID"), citac("Prioritet"), KategorijaID)
                            html.AppendFormat("</td>")
                            html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtiklePodGrupeBulk(citac("ID")), PrebrojiArtiklePodGrupeDostupneBulk(citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                            'modal izmjena
                            html.AppendFormat("<div class=""modal fade"" id=""smallmodal{0}"" tabindex=""-1"" role=""dialog"" aria-labelledby=""smallmodalLabel"" aria-hidden=""true"">", citac("ID"))
                            html.Append("<div class=""modal-dialog modal-sm"" role=""document"">")
                            html.Append("<div class=""modal-content modPass"">")
                            html.AppendFormat("<form action=""/CMS/Ajax/PromjeniArtikliGrupeBulk.aspx"" id=""add-project-form"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
                            html.Append("<div class=""modal-header"">")
                            html.Append("<h5 class=""modal-title"" id=""smallmodalLabel"">Izmjena naziva kategorije</h5>")
                            html.Append("<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">")
                            html.Append("<span aria-hidden=""true"">&times;</span>")
                            html.Append("</button>")
                            html.Append("</div>") 'modal-header
                            html.Append("<div class=""modal-body"">")
                            html.Append("<p>")
                            html.AppendFormat("<input type=""hidden"" id=""slikaID"" name=""slikaID"" value=""{0}"" class=""form-control"">", citac("ID"))
                            html.AppendFormat("<input type=""hidden"" id=""NadGrupaID"" name=""NadGrupaID"" value=""{0}"" class=""form-control"">", citac("NadGrupaID"))
                            html.AppendFormat("<input type=""hidden"" id=""stariNaziv"" name=""stariNaziv"" value=""{0}"" class=""form-control"">", citac("Grupa"))
                            html.AppendFormat("<input type=""text"" id=""noviNaziv"" name=""noviNaziv"" value=""{0}"" placeholder=""unesite novi link"" class=""form-control"" >", citac("Grupa"))
                            html.Append("</p>")
                            html.Append("</div>") 'modal-body
                            html.Append("<div class=""modal-footer"">")
                            html.Append("<button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Odustani</button>")
                            html.Append("<button type=""submit"" class=""btn btn-primary btnIzmjeniPass"">Izmjeni</button>")
                            html.Append("</div>") 'modal-footer
                            html.Append("</form>")
                            html.Append("</div>") 'modal-content
                            html.Append("</div>") 'modal-dialog modal-sm
                            html.Append("</div>") 'modal fade
                            'modal izmjena
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaArtikliPodKategorijeBulk() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()
        Dim PodKategorijaID As Integer = HttpContext.Current.Request.QueryString("PodKategorijaID")

        html.Append("<div class=""row sviArtikliPodKategorijeBulk"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.AppendFormat("<input type=""hidden"" value=""{0}"" class=""polje PodKategorijaID"" />", PodKategorijaID)
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusArtikla"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikli"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        html.Append("<th>Skladište</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(MrezaArtikliPodKategorijeBulk())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.AppendFormat("<input type=""hidden"" value=""{0}"" class=""polje PodKategorijaID"" />", PodKategorijaID)
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function MrezaArtikliPodKategorijeBulk() As String
        Dim PodKategorijaID As Integer = HttpContext.Current.Request.QueryString("PodKategorijaID")
        Return MrezaArtikliPodKategorijeBulk(1, PodKategorijaID)
    End Function

    Public Shared Function MrezaArtikliPodKategorijeBulk(stranica As Integer, PodKategorijaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim PodKategorijaID As Integer = HttpContext.Current.Request.QueryString("PodKategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikalaPodKategorijeBulk"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@PodKategorijaID", PodKategorijaID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            html.AppendFormat("<td>{0}</td>", citac("Skladiste"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    'BULK KRAJ

    Public Shared Function NoviKorisnik() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""col-lg-6"">")
        html.Append("<div class=""card artikalDet"">")

        html.Append("<form action=""/CMS/Ajax/UpdateKorisnika.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

        html.Append("<div class=""card-header""><strong>Podaci</strong> korisnika</div>")
        html.Append("<div class=""card-body card-block"">")

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label class=""form-control-label"">Aktivan</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col col-md-9"">")
        'html.Append("<div class=""form-check-inline form-check"">")
        'html.Append("<label for=""inline-checkbox1"" class=""form-check-label"">")
        'html.AppendFormat("<input type=""checkbox"" id=""chkAktivan"" name=""chkAktivan"" class=""form-check-input"" />")
        'html.Append("</div>") 'form-check-inline form-check
        html.Append("<div class=""form-check-inline form-check"">")
        html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
        html.Append("<input type=""checkbox"" class=""switch-input"" name=""chkAktivan"" id=""chkAktivan"" checked/>")
        html.Append("<span class=""switch-label""></span>")
        html.Append("<span class=""switch-handle""></span>")
        html.Append("</label>")
        html.Append("</div>") 'form-check-inline form-check
        html.Append("</div>") 'col col-md-9
        html.Append("</div>") 'row form-group)

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Šifra korisnika</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtSifra"" name=""txtSifra"" value=""{0}"" placeholder=""Šifra korisnika"" class=""form-control"" >", "")
        html.Append("<small class=""form-text text-muted"">Šifra korisnika u poslovnaj aplikaciji</small>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Naziv korisnika</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", "0")
        html.AppendFormat("<input type=""text"" id=""txtImePrezime"" name=""txtImePrezime"" value=""{0}"" placeholder=""Naziv korisnika"" class=""form-control"" >", "")
        'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">ID Broj</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtIdBroj"" name=""txtIdBroj"" value=""{0}"" placeholder="" ID Broj"" class=""form-control"" >", "")
        'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group
        'html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">PDV Broj</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtPdvBroj"" name=""txtPdvBroj"" value=""{0}"" placeholder="" PDV Broj"" class=""form-control"" >", "")
        'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group
        'html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Telefon</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtTelefon"" name=""txtTelefon"" value=""{0}"" placeholder=""Telefon"" class=""form-control"">", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Email</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtEmail"" name=""txtEmail"" value=""{0}"" placeholder=""Email"" class=""form-control"" >", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Nivo korisnika</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.Append(ddlAdminLevel(10))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""disabled-input"" class=""form-control-label"">Adresa</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtAdresa"" name=""txtAdresa"" placeholder=""Adresa"" value=""{0}"" class=""form-control"">", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Grad</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-4"">")
        html.AppendFormat("<input type=""text"" id=""txtGrad"" name=""txtGrad"" value=""{0}"" placeholder=""Grad"" class=""form-control"" >", "")
        html.Append("</div>") 'col-12 col-md-4
        html.Append("<div class=""col col-md-1"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">ZIP</label>")
        html.Append("</div>") 'col col-md-1
        html.Append("<div class=""col-12 col-md-4"">")
        html.AppendFormat("<input type=""text"" id=""txtZIP"" name=""txtZIP"" value=""{0}"" placeholder=""ZIP"" class=""form-control"" >", "")
        html.Append("</div>") 'col-12 col-md-4
        html.Append("</div>") 'row form-group

        'html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col-12 col-md-3"">")
        'html.Append("<label for=""text-input"" class=""form-control-label"">Broj aktivnosti</label>")
        'html.Append("</div>") 'col col-md-3
        'html.Append("<div class=""col-12 col-md-3"">")
        'html.AppendFormat("<input type=""text"" id=""txtBrojAktivnosti"" name=""txtBrojAktivnosti"" value=""{0}"" placeholder=""BrojAktivnosti"" class=""form-control"" disabled="""">", "0")
        'html.Append("</div>") 'col-12 col-md-3
        'html.Append("<div class=""col col-md-2"">")
        'html.Append("<label for=""text-input"" class=""form-control-label"">Posljednja</label>")
        'html.Append("</div>") 'col col-md-2
        'html.Append("<div class=""col-12 col-md-4"">")
        'html.AppendFormat("<input type=""text"" id=""txtZadnjaAktivnost"" name=""txtZadnjaAktivnost"" value=""{0}"" placeholder=""ZadnjaAktivnost"" class=""form-control"" disabled="""">", "NIKAD")
        'html.Append("</div>") 'col-12 col-md-4
        'html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""textarea-input"" class=""form-control-label"">Napomena</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<textarea name=""txtNapomena"" id=""txtNapomena"" rows=""9"" placeholder=""napomena..."" class=""form-control"">{0}</textarea>", "")
        html.Append("</div>") 'col col-md-9
        html.Append("</div>") 'row form-group

        html.Append("</div>") 'card-body card-block

        html.Append("<div class=""card-footer"">")
        html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
        html.Append("<a href=""/CMS/Korisnici.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
        html.Append("</div>") 'card-footer

        html.Append("</form>")

        html.Append("</div>") 'card artikalDet
        html.Append("</div>") 'col-lg-6

        'html.Append("<div class=""col-lg-6"">")
        'html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong>Narudžbe</strong> korisnika</div>")
        'html.Append("<div class=""row form-group"">")
        'html.Append("<div class=""col-12 col-md-12"">")
        'html.Append("<table class=""table table-bordered tblNarudzbeKorisnika"">")
        'html.Append("<thead class=""thead-dark"">")
        'html.Append("<tr>")
        'html.Append("<th>ID</th>")
        'html.Append("<th>Datum</th>")
        'html.Append("<th>Iznos</th>")
        'html.Append("<th>&nbsp;</th>")
        'html.Append("</tr>")
        'html.Append("</thead>")
        'html.Append("<tbody class=""tbody"">")
        ''html.Append(NarudzbeKorisnika(KorisnikID))
        'html.Append("</tbody>")
        'html.Append("<table>")
        'html.Append("</div>") 'col-12 col-md-9
        'html.Append("</div>") 'row form-group
        'html.Append("</div>") 'card
        'html.Append("</div>") 'col-lg-6

        Return html.ToString
    End Function

    Public Shared Function Korisnici() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row Korisnici"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblKorisnici"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Broj aktivnosti</th>")
        html.Append("<th>Zadnja aktivnost</th>")
        html.Append("<th>Nivo</th>")
        html.Append("<th>Korisnik</th>")
        html.Append("<th>Telefon</th>")
        html.Append("<th>Email</th>")
        html.Append("<th>Broj narudžbi</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(KorisniciMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Private Shared Function NivoKorisnika(AdminLevel As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Nivo FROM AdminLevel WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", AdminLevel)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Nivo"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function KorisniciMrezaTablica() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Korisnici WHERE AdminLevel>'1' ORDER BY AdminLevel ASC, ImePrezime ASC"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivan") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("BrojAktivnosti"))
                            html.AppendFormat("<td>{0}</td>", If(Format(citac("ZadnjaAktivnost"), "dd.MM.yyyy (HH:mm)") = "01.01.2000 (00:00)", "NIKAD", Format(citac("ZadnjaAktivnost"), "dd.MM.yyyy (HH:mm)")))
                            html.AppendFormat("<td>{0}</td>", NivoKorisnika(citac("AdminLevel")))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""KorisnikCMS.aspx?KorisnikID={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("ImePrezime"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("Telefon"))
                            html.AppendFormat("<td>{0}</td>", citac("Email"))
                            html.AppendFormat("<td>{0}</td>", BrojNarudzbiKorisnika(citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivan") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function BrojNarudzbiKorisnika(KupacID As Integer) As Integer
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT COUNT(*) AS Broj FROM Narudzbe WHERE KupacID=@KupacID AND Naruceno='1'"
                komanda.Parameters.AddWithValue("@KupacID", KupacID)
                'komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Broj"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append("0")
        End If

        Return html.ToString
    End Function

    Public Shared Function IzmjenaKorisnika() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim KorisnikID As Integer = HttpContext.Current.Request.QueryString("KorisnikID")

        html.Append("<div class=""col-lg-6"">")
        html.Append("<div class=""card artikalDet"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Korisnici WHERE ID=@KorisnikID"
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            'modal
                            html.Append("<div class=""modal fade"" id=""smallmodal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""smallmodalLabel"" aria-hidden=""true"">")
                            html.Append("<div class=""modal-dialog modal-sm"" role=""document"">")
                            html.Append("<div class=""modal-content modPass"">")
                            html.Append("<form action=""/CMS/Ajax/UpdateZaporke.aspx"" id=""add-project-form"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
                            html.Append("<div class=""modal-header"">")
                            html.Append("<h5 class=""modal-title"" id=""smallmodalLabel"">Izmjena zaporke</h5>")
                            html.Append("<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">")
                            html.Append("<span aria-hidden=""true"">&times;</span>")
                            html.Append("</button>")
                            html.Append("</div>") 'modal-header
                            html.Append("<div class=""modal-body"">")
                            html.Append("<p>")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))
                            html.Append("<input type=""text"" id=""txtPassword1"" name=""txtPassword1"" placeholder=""unesite zaporku"" class=""form-control"" >")
                            html.Append("<small class=""form-text text-muted"">Unesite novu zaporku</small>")
                            html.Append("<input type=""text"" id=""txtPassword2"" name=""txtPassword2"" placeholder=""ponovite zaporku"" class=""form-control"" >")
                            html.Append("<small class=""form-text text-muted"">Ponovite novu zaporku</small>")
                            html.Append("</p>")
                            html.Append("</div>") 'modal-body
                            html.Append("<div class=""modal-footer"">")
                            html.Append("<button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Odustani</button>")
                            html.Append("<button type=""submit"" class=""btn btn-primary btnIzmjeniPass"">Izmjeni</button>")
                            html.Append("</div>") 'modal-footer
                            html.Append("</form>")
                            html.Append("</div>") 'modal-content
                            html.Append("</div>") 'modal-dialog modal-sm
                            html.Append("</div>") 'modal fade

                            html.Append("<form action=""/CMS/Ajax/UpdateKorisnika.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

                            html.Append("<div class=""card-header"">")
                            html.Append("<strong>Podaci</strong> korisnika")
                            html.Append("<span class=""spnPass""><button type=""button"" class=""btn btn-secondary mb-1"" data-toggle=""modal"" data-target=""#smallmodal"">Izmjena zaporke</button><span>")
                            html.Append("</div>")
                            html.Append("<div class=""card-body card-block"">")

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label class=""form-control-label"">Aktivan</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col col-md-9"">")
                            'html.Append("<div class=""form-check-inline form-check"">")
                            'html.Append("<label for=""inline-checkbox1"" class=""form-check-label"">")
                            'html.AppendFormat("<input type=""checkbox"" id=""chkAktivan"" name=""chkAktivan"" class=""form-check-input"" {1}>", citac("Aktivan"), If(citac("Aktivan") = True, "checked", ""))
                            'html.Append("</div>") '"
                            html.Append("<div class=""form-check-inline form-check"">")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" name=""chkAktivan"" id=""chkAktivan"" data-id=""{1}"" {0} >", If(citac("Aktivan") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</div>") 'form-check-inline form-check
                            html.Append("</div>") 'col col-md-9
                            html.Append("</div>") 'row form-group)

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Šifra korisnika</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtSifra"" name=""txtSifra"" value=""{0}"" placeholder=""Šifra korisnika"" class=""form-control"" >", citac("SifraKorisnika"))
                            html.Append("<small class=""form-text text-muted"">Šifra korisnika u poslovnaj aplikaciji</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Naziv korisnika</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))
                            html.AppendFormat("<input type=""text"" id=""txtImePrezime"" name=""txtImePrezime"" value=""{0}"" placeholder=""Naziv korisnika"" class=""form-control"" >", citac("ImePrezime"))
                            'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group
                            'html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">ID Broj</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtIdBroj"" name=""txtIdBroj"" value=""{0}"" placeholder="" ID Broj"" class=""form-control"" >", citac("IdBroj"))
                            'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group
                            'html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">PDV Broj</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtPdvBroj"" name=""txtPdvBroj"" value=""{0}"" placeholder="" PDV Broj"" class=""form-control"" >", citac("PdvBroj"))
                            'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group
                            'html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Telefon</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtTelefon"" name=""txtTelefon"" value=""{0}"" placeholder=""Telefon"" class=""form-control"">", citac("Telefon"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Email</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtEmail"" name=""txtEmail"" value=""{0}"" placeholder=""Email"" class=""form-control"" >", citac("Email"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Nivo korisnika</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlAdminLevel(citac("AdminLevel")))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""disabled-input"" class=""form-control-label"">Adresa</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtAdresa"" name=""txtAdresa"" placeholder=""Adresa"" value=""{0}"" class=""form-control"">", citac("Adresa"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Grad</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-4"">")
                            html.AppendFormat("<input type=""text"" id=""txtGrad"" name=""txtGrad"" value=""{0}"" placeholder=""Grad"" class=""form-control"" >", citac("Grad"))
                            html.Append("</div>") 'col-12 col-md-4
                            html.Append("<div class=""col col-md-1"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">ZIP</label>")
                            html.Append("</div>") 'col col-md-1
                            html.Append("<div class=""col-12 col-md-4"">")
                            html.AppendFormat("<input type=""text"" id=""txtZIP"" name=""txtZIP"" value=""{0}"" placeholder=""ZIP"" class=""form-control"" >", citac("ZIP"))
                            html.Append("</div>") 'col-12 col-md-4
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Broj aktivnosti</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.AppendFormat("<input type=""text"" id=""txtBrojAktivnosti"" name=""txtBrojAktivnosti"" value=""{0}"" placeholder=""BrojAktivnosti"" class=""form-control"" disabled="""">", citac("BrojAktivnosti"))
                            html.Append("</div>") 'col-12 col-md-3
                            html.Append("<div class=""col col-md-2"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Posljednja</label>")
                            html.Append("</div>") 'col col-md-2
                            html.Append("<div class=""col-12 col-md-4"">")
                            html.AppendFormat("<input type=""text"" id=""txtZadnjaAktivnost"" name=""txtZadnjaAktivnost"" value=""{0}"" placeholder=""ZadnjaAktivnost"" class=""form-control"" disabled="""">", If(Format(citac("ZadnjaAktivnost"), "dd.MM.yyyy (HH:mm)") = "01.01.2000 (00:00)", "NIKAD", Format(citac("ZadnjaAktivnost"), "dd.MM.yyyy (HH:mm)")))
                            html.Append("</div>") 'col-12 col-md-4
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Napomena</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<textarea name=""txtNapomena"" id=""txtNapomena"" rows=""9"" placeholder=""napomena..."" class=""form-control"">{0}</textarea>", citac("Napomena"))
                            html.Append("</div>") 'col col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("</div>") 'card-body card-block

                            html.Append("<div class=""card-footer"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            html.Append("<a href=""/CMS/Korisnici.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</div>") 'card artikalDet
        html.Append("</div>") 'col-lg-6

        html.Append("<div class=""col-lg-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-header""><strong>Narudžbe</strong> korisnika</div>")
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.Append("<table class=""table table-bordered tblNarudzbeKorisnika"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>ID</th>")
        html.Append("<th>Datum / WebShop</th>")
        html.Append("<th>Iznos</th>")
        html.Append("<th>Status</th>")
        html.Append("<th>&nbsp;</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(NarudzbeKorisnika(KorisnikID))
        html.Append("</tbody>")
        html.Append("<table>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group
        html.Append("</div>") 'card
        html.Append("</div>") 'col-lg-6

        Return html.ToString()
    End Function

    Private Shared Function ddlAdminLevel(AdminLevel As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<select name=""ddlAdminLevel"" id=""ddlAdminLevel"" class=""form-control ddlAdminLevel"">")
        html.Append("<option value=""0"">Odaberite kategoriju</option>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM AdminLevel WHERE Aktivno='1' AND ID>'1' ORDER BY Nivo ASC"
                'komanda.Parameters.AddWithValue("@SifraKategorije", KlasaSifra)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" {2}>{1}</option>", citac("ID"), citac("Nivo"), If(AdminLevel = citac("ID"), " selected", ""))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Private Shared Function NarudzbeKorisnika(KorisnikID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim valuta As String = Postavke("Valuta")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Narudzbe WHERE KupacID=@KupacID AND Naruceno='1'"
                komanda.Parameters.AddWithValue("@KupacID", KorisnikID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<tr>")
                            html.AppendFormat("<td>{0}</td>", citac("ID"))
                            html.AppendFormat("<td>{0}</br>{1}</td>", Format(citac("DatumKreiranja"), "dd.MM.yyyy"), citac("Domena"))
                            html.AppendFormat("<td>{0} {1}</td>", SumaNarudzbe(citac("ID")), valuta)
                            If citac("Naplaceno") = True Then
                                html.AppendFormat("<td>{0}</td>", "Naplaceno")
                            Else
                                If citac("Poslano") = True Then
                                    html.AppendFormat("<td>{0}</td>", "Poslano")
                                Else
                                    html.AppendFormat("<td>{0}</td>", "Na čekanju")
                                End If
                            End If
                            html.AppendFormat("<td><a class=""btn btn-primary"" href=""/CMS/Narudzba.aspx?id={0}"" target=""_blank"">ISPIS</a></td>", citac("ID"))
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function SumaNarudzbe(NarudzbaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

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

    Public Shared Function tabelaPretragaArtikala() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sviArtikli"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        'html.Append("<div class=""row slovoArtikla"">")
        'html.Append("<div class=""col-sm-12 col-md-12"">")
        'html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        ''html.AppendFormat("<input type=""button"" value=""{0}"" class=""dugmic slovo"" id=""{0}"" />", "0")
        'Using konekcija As New SqlConnection(putanja)
        '    konekcija.Open()
        '    Using komanda As New SqlCommand()
        '        komanda.Connection = konekcija
        '        komanda.CommandType = CommandType.Text
        '        komanda.CommandText = "SELECT TOP (100) PERCENT LEFT(NazivArtikla, '1') AS SlovoArtikla FROM  dbo.Artikli WHERE Aktivno='1' GROUP BY LEFT(NazivArtikla, '1') ORDER BY SlovoArtikla"
        '        'komanda.Parameters.AddWithValue("@Stranica", stranica)
        '        Using citac As SqlDataReader = komanda.ExecuteReader()
        '            If citac IsNot Nothing Then
        '                While citac.Read()
        '                    html.AppendFormat("<input type=""button"" value=""{0}"" class=""dugmic slovo"" id=""{0}"" />", citac("SlovoArtikla"))
        '                End While
        '            End If
        '        End Using
        '    End Using
        'End Using
        'html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        'html.Append("</div>") 'col-sm-12 col-md-7
        'html.Append("</div>") 'row

        'html.Append("<div class=""row"">")
        'html.Append("<div class=""col-sm-12 col-md-7"">")
        'html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        'html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        'html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        'html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        'html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        'html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        'html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        'html.Append("</div>") 'col-sm-12 col-md-7
        'html.Append("</div>") 'row

        html.Append("<div class=""row statusArtikla"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikli"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ArtikliPretragaMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        'html.Append("<div class=""row"">")
        'html.Append("<div class=""col-sm-12 col-md-7"">")
        'html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        'html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        'html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        'html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        'html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        'html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        'html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        'html.Append("</div>") 'col-sm-12 col-md-7
        'html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ArtikliPretragaMrezaTablica() As String
        Return ArtikliPretragaMrezaTablica(1)
    End Function

    Public Shared Function ArtikliPretragaMrezaTablica(stranica As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim pojam As String = HttpContext.Current.Request.QueryString("pojam")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikalaPretraga"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@pojam", pojam)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaNoveNarudzbe() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row noveNarudzbe"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")


        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusNarudzbePoslano"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblSveNarudzbe"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Narudžba</th>")
        html.Append("<th>Kupac / WebShop</th>")
        html.Append("<th>Datum</th>")
        html.Append("<th>Iznos</th>")
        html.Append("<th>Način</th>")
        html.Append("<th>Poslano</th>")
        html.Append("<th>Ispis</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(NoveNarudzbeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function NoveNarudzbeMrezaTablica() As String
        Return NoveNarudzbeMrezaTablica(1)
    End Function

    Public Shared Function NoveNarudzbeMrezaTablica(stranica As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim valuta As String = Postavke("Valuta")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponNoveNarudzbe"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Poslano") = 0, "", "neaktivno"), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Narudzba.aspx?id={0}"" target=""_blank"">", citac("ID"))
                            html.AppendFormat("{0}", citac("ID"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""KorisnikCMS.aspx?KorisnikID={0}"">", citac("KupacID"))
                            html.AppendFormat("{0}</br>{1}", citac("ImePrezime"), citac("Domena"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", Format(citac("DatumKreiranja"), "dd.MM.yyyy (HH:mm)"))
                            html.AppendFormat("<td>{0} {1}</td>", SumaNarudzbe(citac("ID")), valuta)
                            html.AppendFormat("<td>{0}</td>", citac("NacinPlacanja"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Poslano") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.AppendFormat("<td><a class=""btn btn-primary"" href=""/CMS/Narudzba.aspx?id={0}"" target=""_blank"">ISPIS</a></td>", citac("ID"))
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaZavrseneNarudzbe() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row noveZavrsene"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")


        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusNarudzbeNaplaceno"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblSveNarudzbe"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Narudžba</th>")
        html.Append("<th>Kupac / WebShop</th>")
        html.Append("<th>Datum</th>")
        html.Append("<th>Iznos</th>")
        html.Append("<th>Poslano</th>")
        html.Append("<th>Naplaceno</th>")
        html.Append("<th>Ispis</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ZavrseneNarudzbeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ZavrseneNarudzbeMrezaTablica() As String
        Return ZavrseneNarudzbeMrezaTablica(1)
    End Function

    Public Shared Function ZavrseneNarudzbeMrezaTablica(stranica As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim valuta As String = Postavke("Valuta")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponZavrseneNarudzbe"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Naplaceno") = 0, "", "zavrseno"), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""KorisnikCMS.aspx?KorisnikID={0}"">", citac("KupacID"))
                            html.AppendFormat("{0}</br>{1}", citac("ImePrezime"), citac("Domena"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", Format(citac("DatumKreiranja"), "dd.MM.yyyy (HH:mm)"))
                            html.AppendFormat("<td>{0} {1}</td>", SumaNarudzbe(citac("ID")), valuta)
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input poslano"" data-id=""{1}"" {0}>", If(citac("Poslano") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input naplaceno"" data-id=""{1}"" {0}>", If(citac("Naplaceno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.AppendFormat("<td><a class=""btn btn-primary"" href=""/CMS/Narudzba.aspx?id={0}"" target=""_blank"">ISPIS</a></td>", citac("ID"))
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaObavjesti() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sveObavjesti"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row statusObavjesti"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblObavjesti"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Objavio</th>")
        html.Append("<th>Objavljeno</th>")
        html.Append("<th>Naslov</th>")
        html.Append("<th>Ukratko</th>")
        'html.Append("<th>Količina</th>")
        'html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiAktivneObavjesti"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            'html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("ImePrezime"))
                            'html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", Format(citac("DarumPrikaza"), "dd.MM.yyyy (HH:mm)"))
                            html.AppendFormat("<td><a title=""{0}"" href=""#"">{0}</a></td>", citac("Naslov"))
                            html.AppendFormat("<td>{0}</td>", citac("Ukratko"))
                            'html.Append("<td>")
                            'html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            'html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            'html.Append("<span class=""switch-label""></span>")
                            'html.Append("<span class=""switch-handle""></span>")
                            'html.Append("</label>")
                            'html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function tabelaArtikalaBezSlike() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sviArtikliBezSlike"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-2"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append(ddlKategorija())
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-2
        html.Append("<div class=""col-sm-2"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""text"" id=""txtNaziv"" name=""txtNaziv"" value="""" placeholder="" ako ima u nazivu"" class=""form-control txtNaziv"">")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-2
        html.Append("<div class=""col-8"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-8
        html.Append("</div>") 'row

        html.Append("<div class=""row statusArtiklaBezSlike"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikliBezSlike"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        'html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ArtikliMrezaTablicaBezSlike())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ArtikliMrezaTablicaBezSlike() As String
        Return ArtikliMrezaTablicaBezSlike(1, 0, 0)
    End Function

    Public Shared Function ArtikliMrezaTablicaBezSlike(stranica As Integer, kategorija As Integer, naziv As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikalaBezSlike"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@Kategorija", kategorija)
                komanda.Parameters.AddWithValue("@Naziv", naziv)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            'html.Append("<td>")
                            'html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            'html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            'html.Append("<span class=""switch-label""></span>")
                            'html.Append("<span class=""switch-handle""></span>")
                            'html.Append("</label>")
                            'html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function ddlKategorija() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""input-group mb-3"">")
        html.Append("<select id=""ddlKategorija"" class=""custom-select ddlKategorija"">")
        html.Append("<option value='0' selected>Kategorija</option>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliNadGrupe WHERE ID IN (SELECT NadGrupaID FROM Artikli WHERE Aktivno='1') ORDER BY NadGrupa ASC"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value='{0}' >{1} ({2})</option>", citac("ID"), citac("NadGrupa"), PrebrojiArtikleGrupe(citac("ID")))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")
        html.Append("</div>")

        Return html.ToString
    End Function

    Public Shared Function tabelaIzdvojenihArtikala() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sviArtikliIzdvojeni"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-2"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""text"" id=""txtNaziv"" name=""txtNaziv"" value="""" placeholder="" ako ima u nazivu"" class=""form-control txtNaziv"">")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-2
        html.Append("<div class=""col-sm-10 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        'html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        'html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/> ")
        'html.AppendFormat(" od <input type=""text"" value=""{0}"" class=""textP polje"" disabled style=""width: 75px;""/>", PrebrojiArtikleStranice(1))
        'html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-10 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusArtikla"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblArtikli"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naziv Artikla</th>")
        html.Append("<th>Šifra Artikla</th>")
        html.Append("<th>Bar Cod</th>")
        html.Append("<th>Cijena</th>")
        html.Append("<th>Količina</th>")
        html.Append("<th>Skladište</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ArtikliMrezaTablicaIzdvojeni())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        'html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        'html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        'html.AppendFormat(" od <input type=""text"" value=""{0}"" class=""textP polje"" disabled style=""width: 75px;""/>", PrebrojiArtikleStranice(1))
        'html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ArtikliMrezaTablicaIzdvojeni() As String
        Return ArtikliMrezaTablicaIzdvojeni(1, 0)
    End Function

    Public Shared Function ArtikliMrezaTablicaIzdvojeni(stranica As Integer, naziv As String) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponArtikalaIzdvojeni"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                komanda.Parameters.AddWithValue("@Naziv", naziv)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlike(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Artikal.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naziv"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", citac("SifraRobe"))
                            html.AppendFormat("<td>{0}</td>", citac("BarCod"))
                            html.AppendFormat("<td>{0}</td>", citac("Cijena"), Postavke("Valuta"))
                            html.AppendFormat("<td>{0}</td>", citac("Kolicina"))
                            html.AppendFormat("<td>{0}</td>", citac("Skladiste"))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            'If citac("Aktivno") = True Then
                            '    html.AppendFormat("<td>{0}</td>", "DA")
                            'Else
                            '    html.AppendFormat("<td>{0}</td>", "NE")
                            'End If
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function NazivArtikla(ArtikalID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Naziv FROM Artikli WHERE ID=@ArtikalID"
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Naziv"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function BazaArtikala() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row bazaArtikala"">")

        html.Append("<div class=""col-md-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-header""><strong class=""card-title"">Artikli AK2</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")
        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<strong>Posljednje osvježavanje artikala:</strong> {0}", ProvjeriOsvjezavanje(1))
        html.Append("</div>") 'col-sm-12
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<a href=""/Code/ImportArtikala.aspx"" target=""_blank"" class=""btn btn-warning"">Osvježi odmah</a>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row
        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12

        html.Append("<div class=""col-md-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-header""><strong class=""card-title"">Artikli ComTrade</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")
        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<strong>Posljednje osvježavanje artikala:</strong> {0}", ProvjeriOsvjezavanje(2))
        html.Append("</div>") 'col-sm-12
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<a href=""/WebService/ComTradeArtikli.aspx"" target=""_blank"" class=""btn btn-warning"">Osvježi odmah</a>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row
        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-6

        html.Append("<div class=""col-md-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-header""><strong class=""card-title"">Artikli UniExpert</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")
        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<strong>Posljednje osvježavanje artikala:</strong> {0}", ProvjeriOsvjezavanje(3))
        html.Append("</div>") 'col-sm-12
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<a href=""/WebService/UniExpertArtikliImport.aspx"" target=""_blank"" class=""btn btn-warning"">Osvježi odmah</a>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row
        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-6

        html.Append("<div class=""col-md-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-header""><strong class=""card-title"">Artikli StarTech</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")
        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<strong>Posljednje osvježavanje artikala:</strong> {0}", ProvjeriOsvjezavanje(5))
        html.Append("</div>") 'col-sm-12
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<a href=""/WebService/StarTechArtikliImport.aspx"" target=""_blank"" class=""btn btn-warning"">Osvježi odmah</a>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row
        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-6

        html.Append("<div class=""col-md-6"">")
        html.Append("<div class=""card"">")
        html.Append("<div class=""card-header""><strong class=""card-title"">Artikli Digitalis</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")
        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<strong>Posljednje osvježavanje artikala:</strong> {0}", ProvjeriOsvjezavanje(6))
        html.Append("</div>") 'col-sm-12
        html.Append("<div class=""col-sm-12"">")
        html.AppendFormat("<a href=""/WebService/DigitalisArtikliImport.aspx"" target=""_blank"" class=""btn btn-warning"">Osvježi odmah</a>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row
        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-6

        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Private Shared Function ProvjeriOsvjezavanje(SkladisteID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Datum FROM Brojac WHERE Skladiste=@Skladiste"
                komanda.Parameters.AddWithValue("@Skladiste", SkladisteID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", Format(citac("Datum"), "dd.MM.yyyy (HH:mm)"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    Public Shared Function PostavkeTvrtke() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim ArtikalID As Integer = HttpContext.Current.Request.QueryString("id")

        html.Append("<div class=""col-lg-9"">")
        html.Append("<div class=""card tvrtkaDet"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Postavke"
                'komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<form action=""/CMS/Ajax/UpdateArtikla.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

                            html.Append("<div class=""card-header""><strong>Izmjena</strong> artikla</div>")
                            html.Append("<div class=""card-body card-block"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))

                            'html.Append("<div class=""row form-group"">")
                            'html.Append("<div class=""col col-md-3"">")
                            'html.Append("<label for=""disabled-input"" class=""form-control-label"">RB</label>")
                            'html.Append("</div>") 'col col-md-3
                            'html.Append("<div class=""col-12 col-md-9"">")
                            'html.AppendFormat("<input type=""text"" id=""txtRB"" name=""txtRB"" placeholder=""RB"" value=""{0}"" class=""form-control"" disabled="""">", citac("RB"))
                            'html.Append("</div>") 'col-12 col-md-9
                            'html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Šifra artikla</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtSifraArtikla"" name=""txtSifraArtikla"" value=""{0}"" placeholder=""šifra artikla"" class=""form-control"" disabled="""">", citac("SifraRobe"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Bar Cod</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtBarCod"" name=""txtBarCod"" value=""{0}"" placeholder=""bar cod"" class=""form-control"" disabled="""">", citac("BarCod"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Količina u ""ak2""</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtkolicina"" name=""txtkolicina"" value=""{0}"" placeholder=""kolicina u wand-u"" class=""form-control"" disabled="""">", citac("Kolicina"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Naziv artikla</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtNazivArtikla"" name=""txtNazivArtikla"" value=""{0}"" placeholder=""Naziv artikla"" class=""form-control"" disabled>", Replace(citac("Naziv"), """", ""))
                            'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col-12 col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Cijene</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtCijenaMPC"" name=""txtCijenaMPC"" value=""{0}"" placeholder=""Cijena MPC"" class=""form-control"" disabled>", citac("Cijena"))
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("<div class=""col col-md-2"" style=""text-align: right;"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Akcija (%)</label>")
                            html.Append("</div>") 'col col-md-2
                            html.Append("<div class=""col-12 col-md-2"">")
                            html.AppendFormat("<input type=""text"" id=""txtCijenaAkcija"" name=""txtCijenaAkcija"" value=""{0}"" placeholder=""Akcijska cijena"" class=""form-control"" >", citac("Akcija"))
                            html.Append("</div>") 'col-12 col-md-2
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label class=""form-control-label"">Oznake</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col col-md-9"">")
                            html.Append("<div class=""form-check-inline form-check"">")
                            html.Append("<label for=""inline-checkbox1"" class=""form-check-label"">")
                            html.AppendFormat("<input type=""checkbox"" id=""chkIzdvojeno"" name=""chkIzdvojeno"" class=""form-check-input"" {1}>", citac("Izdvojeno"), If(citac("Izdvojeno") = True, "checked", ""))
                            html.Append("Izdvojeno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("<label for=""inline-checkbox2"" class=""form-check-label"">")
                            html.AppendFormat("<input type=""checkbox"" id=""chkAktivno"" name=""chkAktivno"" class=""form-check-input"" {1}>", citac("Aktivno"), If(citac("Aktivno") = True, "checked", ""))
                            html.Append("Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            'html.Append("<label for=""inline-checkbox3"" class=""form-check-label"">")
                            'html.AppendFormat("<input type=""checkbox"" id=""chkNovo"" name=""chkNovo"" class=""form-check-input"" {1}>", citac("Novo"), If(citac("Novo") = True, "checked", ""))
                            'html.Append("Novo &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("</div>") '"
                            html.Append("</div>") 'col col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Kategorija</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlOdabranaKategorija(citac("NadGrupaID"), "disabled"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Podkategorija</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.Append(ddlOdabranaPodKategorija(citac("GrupaID"), citac("NadGrupaID"), "disabled"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Opis</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<textarea name=""txtOpis"" id=""txtOpis"" rows=""9"" placeholder=""opis..."" class=""form-control"">{0}</textarea>", citac("Opis"))
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("</div>") 'card-body card-block

                            html.Append("<div class=""card-footer"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            html.Append("<a href=""/CMS/Artikli.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</div>") 'card artikalDet
        html.Append("</div>") 'col-lg-9

        html.Append("<div class=""col-lg-3 divSlike"">")
        html.Append("<div class=""card-header""><strong>Fotografije</strong> artikla</div>")
        html.Append("<div class=""card"">")
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.AppendFormat("<form action=""Upload.aspx?id={0}&tag={1}"" method=""post"" enctype=""multipart/form-data"" class=""dropzone dropzone-area dz-clickable"" id=""dpz-remove-thumb"">", ArtikalID, NazivArtikla(ArtikalID))
        html.Append("<input type=""file"" id=""FileUpload"" name=""FileUpload"" multiple="""" class=""form-control-file"">")
        html.Append("<button class=""btn btn-success""><i class=""fa fa-magic""></i>&nbsp; Prenesi</button>")
        html.Append("</form>")
        html.Append("</div>") 'col-12 col-md-9

        html.Append("<div class=""col-12 col-md-12""><br/>")
        html.Append("<label for=""text-input"" class=""form-control-label"">Postojeće slike</label>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("<div class=""col-12 col-md-12 autocompleteSlike"" data-url=""/CMS/Ajax/SelectSlika.aspx"">")
        html.AppendFormat("<input type=""hidden"" class=""ArtikalID"" name=""ArtikalID"" id=""ArtikalID"" value=""{0}"" />", ArtikalID)
        'html.AppendFormat("<input type=""hidden"" class=""slikaid"" name=""slikaid"" id=""slikaid"" value=""{0}"" />", "0")
        html.AppendFormat("<div class=""listaSlika""></div>")
        html.Append("<input type=""text"" class=""txtSlika naziv"" id=""txtSlika"" placeholder=""naziv slike"" />")
        html.Append("</div>") 'col-12 col-md-9

        html.Append("</div>") 'row form-group
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.Append(SlikeArtikla(ArtikalID))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group
        html.Append("</div>") 'card
        html.Append("</div>") 'col-lg-3

        Return html.ToString()
    End Function

    Public Shared Function Slideri() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row Slideri"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblSlideri"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<form action=""Ajax/PrenesiSlider.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"">")
        html.Append("<th style=""background-color:#fff;color: #000;"" colspan=""2""><input type=""file"" id=""file-input"" name=""file-input"" class=""form-control-file""></th>")
        html.Append("<th style=""background-color:#fff;color: #000;"" colspan=""2"">preporučeno: 1170x450px</th>")
        html.Append("<th style=""background-color:#fff;color: #000;""><button type=""submit"" class=""btn btn-primary btn-sm""> <i class=""fa fa-dot-circle-o""></i> Prenesi</button></th>")
        html.Append("</form>")
        html.Append("</tr>")

        html.Append("<tr>")
        html.Append("<th>Slika</th>")
        html.Append("<th>Link</th>")
        html.Append("<th>Slika Mob(Preporučeno: 800x800)</th>")
        html.Append("<th>Prioritet</th>")
        html.Append("<th>Aktivno IGRE.BA</th>")
        html.Append("<th>Aktivno BULK.BA</th>")
        html.Append("<th>Brisanje</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(SlideriLista())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function SlideriLista() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Slider ORDER BY Prioritet ASC"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"" data-id=""{1}"">", If(citac("Aktivno") = 0, "", ""), citac("ID"))
                            html.AppendFormat("<td><img src=""/Thumb.ashx?i={0}&w=250&h=96&f=/Datoteke/Slider/"" /></td>", citac("Slika"))

                            'Link
                            html.Append("<td>")
                            html.AppendFormat("IGRE.BA <button type=""button"" class=""btn btn-secondary mb-1"" data-toggle=""modal"" data-target=""#smallmodal{1}"">{0}</button>", citac("Link"), citac("ID"))
                            html.Append("<br/>")
                            html.AppendFormat("BULK.BA <button type=""button"" class=""btn btn-secondary mb-1"" data-toggle=""modal"" data-target=""#smallmodal{1}"">{0}</button>", citac("LinkBulk"), citac("ID"))
                            html.Append("</td>")

                            ''Slika Mob
                            html.Append("<td class=""tDimagefile"">")
                            If citac("SlikaMob") = "#" Then
                                html.AppendFormat("<input type=""button"" class=""imagefileDodaj"" value=""+"" data-id=""{0}"" style='background: red;' />", citac("ID"))
                                html.AppendFormat("<input type=""file"" class=""imagefile"" style=""display:none;"" data-id=""{0}"" id=""file_{0}"" />", citac("ID"))
                            Else
                                html.AppendFormat("<img src=""/Thumb.ashx?i={0}&w=77&h=77&f=/Datoteke/Slider/"" />", citac("SlikaMob"))
                                html.AppendFormat("<input type=""button"" class=""imagefileUkloni"" value="" UKLONI "" data-id=""{0}"" data-file=""{1}"" />", citac("ID"), citac("SlikaMob"))
                            End If


                            'Prioritet
                            html.AppendFormat("<td>")
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetUp"" data-id=""{0}"" data-prioritet=""{1}""><i class=""fa fa-upload""></i></button>", citac("ID"), citac("Prioritet"))
                            html.AppendFormat("<span class=""badge badge-light"" style=""padding: 10px; margin: 0px 3px;"">{0}</span>", citac("Prioritet"), citac("ID"))
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetDown"" data-id=""{0}"" data-prioritet=""{1}"" ><i class=""fa fa-download""></i></button>", citac("ID"), citac("Prioritet"))
                            html.AppendFormat("</td>")

                            'Aktivno IgreBa
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input actIgre"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")

                            'Aktivno BulkBa
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input actBulk"" data-id=""{1}"" {0}>", If(citac("AktivnoBulk") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")

                            'Brisanje
                            html.Append("</td>")
                            html.AppendFormat("<td><a href=""/CMS/Ajax/IzbrisiSlider.aspx?id={0}&file={1}"" class=""btn btn-danger btn-block"" >Ukloni</a></td>", citac("ID"), citac("Slika"))
                            html.Append("</tr>")
                            'modal izmjena
                            html.AppendFormat("<div class=""modal fade"" id=""smallmodal{0}"" tabindex=""-1"" role=""dialog"" aria-labelledby=""smallmodalLabel"" aria-hidden=""true"">", citac("ID"))
                            html.Append("<div class=""modal-dialog modal-sm"" role=""document"">")
                            html.Append("<div class=""modal-content modPass"">")
                            html.AppendFormat("<form action=""/CMS/Ajax/PromjeniLink.aspx"" id=""add-project-form"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
                            html.Append("<div class=""modal-header"">")
                            html.Append("<h5 class=""modal-title"" id=""smallmodalLabel"">Izmjena naziva</h5>")
                            html.Append("<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">")
                            html.Append("<span aria-hidden=""true"">&times;</span>")
                            html.Append("</button>")
                            html.Append("</div>") 'modal-header
                            html.Append("<div class=""modal-body"">")
                            html.Append("<p>")
                            html.AppendFormat("<input type=""hidden"" id=""slikaID"" name=""slikaID"" value=""{0}"" class=""form-control"">", citac("ID"))
                            html.AppendFormat("<input type=""hidden"" id=""stariNaziv"" name=""stariNaziv"" value=""{0}"" class=""form-control"">", citac("Link"))
                            html.AppendFormat("IGRE.BA <input type=""text"" id=""noviNaziv"" name=""noviNaziv"" value=""{0}"" placeholder=""unesite novi link"" class=""form-control"" >", citac("Link"))
                            html.AppendFormat("BULK.BA <input type=""text"" id=""noviNazivBulk"" name=""noviNazivBulk"" value=""{0}"" placeholder=""unesite novi link Bulk"" class=""form-control"" >", citac("LinkBulk"))
                            html.Append("</p>")
                            html.Append("</div>") 'modal-body
                            html.Append("<div class=""modal-footer"">")
                            html.Append("<button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Odustani</button>")
                            html.Append("<button type=""submit"" class=""btn btn-primary btnIzmjeniPass"">Izmjeni</button>")
                            html.Append("</div>") 'modal-footer
                            html.Append("</form>")
                            html.Append("</div>") 'modal-content
                            html.Append("</div>") 'modal-dialog modal-sm
                            html.Append("</div>") 'modal fade
                            'modal izmjena
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function Partneri() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row Brendovi"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblSlideri"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<form action=""Ajax/PrenesiBrand.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"">")
        html.Append("<th style=""background-color:#fff;color: #000;""><input type=""file"" id=""file-input"" name=""file-input"" class=""form-control-file""></th>")
        html.Append("<th style=""background-color:#fff;color: #000;"" colspan=""2"">preporučeno: 150x124px</th>")
        html.Append("<th style=""background-color:#fff;color: #000;""><button type=""submit"" class=""btn btn-primary btn-sm""> <i class=""fa fa-dot-circle-o""></i> Prenesi</button></th>")
        html.Append("</form>")
        html.Append("</tr>")

        html.Append("<tr>")
        html.Append("<th>Slika</th>")
        html.Append("<th>Prioritet</th>")
        html.Append("<th>Aktivno</th>")
        html.Append("<th>Brisanje</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(PartneriLista())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function PartneriLista() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Brendovi ORDER BY Prioritet ASC"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td><img src=""/Thumb2.ashx?i={0}&w=150&h=124&f=/Datoteke/Brendovi/"" /></td>", citac("Datoteka"))
                            html.AppendFormat("<td>")
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetUp"" data-id=""{0}"" data-prioritet=""{1}""><i class=""fa fa-upload""></i></button>", citac("ID"), citac("Prioritet"))
                            html.AppendFormat("<span class=""badge badge-light"" style=""padding: 10px; margin: 0px 3px;"">{0}</span>", citac("Prioritet"), citac("ID"))
                            html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetDown"" data-id=""{0}"" data-prioritet=""{1}"" ><i class=""fa fa-download""></i></button>", citac("ID"), citac("Prioritet"))
                            html.AppendFormat("</td>")
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.AppendFormat("<td><a href=""/CMS/Ajax/IzbrisiBrand.aspx?id={0}&file={1}"" class=""btn btn-danger btn-block"" >Ukloni</a></td>", citac("ID"), citac("Datoteka"))
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function tabelaNovosti() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row sviClanci"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/> ")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("<div class=""row statusClanci"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblClanci"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp</th>")
        html.Append("<th>Naslov članka</th>")
        'html.Append("<th>Šifra Artikla</th>")
        'html.Append("<th>Bar Cod</th>")
        'html.Append("<th>Cijena</th>")
        html.Append("<th>Aktivan</th>")
        html.Append("<th>Brisanje</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ClanciMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12 col-md-7"">")
        html.Append("<div class=""dataTables_paginate paging_simple_numbers"" id=""bootstrap-data-table_paginate"">")
        html.Append("<input type=""button"" value=""Prethodna"" class=""dugmic prethodna"" />")
        html.Append("<input type=""text"" value=""1"" class=""textP polje stranica"" disabled/>")
        html.Append("<input type=""button"" value=""Slijedeća"" class=""dugmic slijedeca"" />")
        html.Append("<input type=""hidden"" value=""1"" class=""polje stranica"" />")
        html.Append("<input type=""hidden"" value=""0"" class=""polje hidSlovoArtikla"" />")
        html.Append("</div>") 'dataTables_paginate paging_simple_numbers
        html.Append("</div>") 'col-sm-12 col-md-7
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ClanciMrezaTablica() As String
        Return ClanciMrezaTablica(1)
    End Function

    Public Shared Function ClanciMrezaTablica(stranica As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "CmsOdaberiRasponClanaka"
                komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.Append("<td>")
                            html.AppendFormat("{0}", ProvjeriDostupnostSlikeClanka(citac("ID")))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Clanak.aspx?id={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naslov"))
                            html.Append("</a>")
                            html.Append("</td>")
                            'html.AppendFormat("<td>{0}</td>", "")
                            'html.AppendFormat("<td>{0}</td>", "")
                            'html.AppendFormat("<td>{0}</td>", "")
                            'html.AppendFormat("<td>{0}</td>", "")
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.AppendFormat("<td><a href=""/CMS/Ajax/IzbrisiClanak.aspx?id={0}"" class=""btn btn-danger btn-block"" >Ukloni</a></td>", citac("ID"))
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function ProvjeriDostupnostSlikeClanka(ClanakID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT TOP 1 Datoteka FROM NovostiSlike WHERE NovostID=@ClanakID"
                komanda.Parameters.AddWithValue("@ClanakID", ClanakID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<img src=""/thumb2.ashx?i={0}&w=60&h=40&f=/Datoteke/Novosti/"" />", citac("Datoteka"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString.Length < 1 Then
            html.Append("")
        Else
            'html.Clear()
            'html.Append("<i class=""fa fa-picture-o""></i>")
        End If

        Return html.ToString()
    End Function

    Public Shared Function IzmjenaClanka() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim ClanakID As Integer = HttpContext.Current.Request.QueryString("id")

        html.Append("<div class=""col-lg-9"">")
        html.Append("<div class=""card artikalDet"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Novosti WHERE ID=@ClanakID"
                komanda.Parameters.AddWithValue("@ClanakID", ClanakID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<form action=""/CMS/Ajax/UpdateClanka.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

                            html.Append("<div class=""card-header""><strong>Izmjena</strong> članka</div>")
                            html.Append("<div class=""card-body card-block"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""text-input"" class=""form-control-label"">Naziv artikla</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtNaslov"" name=""txtNaslov"" value=""{0}"" placeholder=""Naslov članka"" class=""form-control"">", Replace(citac("Naslov"), """", ""))
                            'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label class=""form-control-label"">Oznake</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col col-md-9"">")
                            html.Append("<div class=""form-check-inline form-check"">")
                            html.Append("<label for=""inline-checkbox2"" class=""form-check-label"">")
                            html.AppendFormat("<input type=""checkbox"" id=""chkAktivno"" name=""chkAktivno"" class=""form-check-input"" {1}>", citac("Aktivno"), If(citac("Aktivno") = True, "checked", ""))
                            html.Append("Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
                            html.Append("</div>") '"
                            html.Append("</div>") 'col col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""select"" class=""form-control-label"">Video link</label>")
                            html.Append("</div>") 'col col-md-3
                            html.Append("<div class=""col-12 col-md-9"">")
                            html.AppendFormat("<input type=""text"" id=""txtVideoLink"" name=""txtVideoLink"" value=""{0}"" placeholder="" video link"" class=""form-control"" />", citac("VideoLink"))
                            html.Append("</div>") 'col-12 col-md-9
                            html.Append("</div>") 'row form-group

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Kratki opis</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<textarea name=""txtUkratko"" id=""txtUkratko"" rows=""3"" placeholder="" Ukratko..."" class=""form-control"">{0}</textarea>", citac("Ukratko"))
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Članak</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
                            'html.AppendFormat("<textarea name=""txtClanak"" id=""txtClanak"" rows=""9"" placeholder=""Clanak..."" class=""form-control tiny"">{0}</textarea>", citac("Clanak"))
                            html.AppendFormat("<textarea name=""txtClanak"" id=""txtClanak"" rows=""9"" placeholder=""Clanak..."" class=""form-control"">{0}</textarea>", citac("Clanak"))
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("</div>") 'card-body card-block

                            html.Append("<div class=""card-footer"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            html.Append("<a href=""/CMS/Pregled.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</div>") 'card artikalDet
        html.Append("</div>") 'col-lg-9

        html.Append("<div class=""col-lg-3 divSlike"">")
        html.Append("<div class=""card-header""><strong>Fotografije</strong> artikla</div>")
        html.Append("<div class=""card"">")
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.AppendFormat("<form action=""UploadSlikeClanka.aspx?id={0}&tag={1}"" method=""post"" enctype=""multipart/form-data"" class=""dropzone dropzone-area dz-clickable"" id=""dpz-remove-thumb"">", ClanakID, NaslovClanka(ClanakID))
        html.Append("<input type=""file"" id=""FileUpload"" name=""FileUpload"" multiple="""" class=""form-control-file"">")
        html.Append("<button class=""btn btn-success""><i class=""fa fa-magic""></i>&nbsp; Prenesi</button>")
        html.Append("</form>")
        html.Append("</div>") 'col-12 col-md-9

        'html.Append("<div class=""col-12 col-md-12""><br/>")
        'html.Append("<label for=""text-input"" class=""form-control-label"">Postojeće slike</label>")
        'html.Append("</div>") 'col-12 col-md-9
        'html.Append("<div class=""col-12 col-md-12 autocompleteSlike"" data-url=""/CMS/Ajax/SelectSlika.aspx"">")
        'html.AppendFormat("<input type=""hidden"" class=""ArtikalID"" name=""ArtikalID"" id=""ArtikalID"" value=""{0}"" />", ClanakID)
        ''html.AppendFormat("<input type=""hidden"" class=""slikaid"" name=""slikaid"" id=""slikaid"" value=""{0}"" />", "0")
        'html.AppendFormat("<div class=""listaSlika""></div>")
        'html.Append("<input type=""text"" class=""txtSlika naziv"" id=""txtSlika"" placeholder=""naziv slike"" />")
        'html.Append("</div>") 'col-12 col-md-9

        html.Append("</div>") 'row form-group
        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col-12 col-md-12"">")
        html.Append(SlikeClanka(ClanakID))
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group
        html.Append("</div>") 'card
        html.Append("</div>") 'col-lg-3

        Return html.ToString()
    End Function

    Private Shared Function SlikeClanka(ClanakID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM NovostiSlike WHERE NovostID=@ClanakID ORDER BY Zadana DESC"
                komanda.Parameters.AddWithValue("@ClanakID", ClanakID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<div class=""card text-white bg-flat-color-1"">")
                            html.Append("<div class=""card-body pb-0"" style=""padding: 0rem;"">")
                            html.Append("<div class=""dropdown float-right"">")
                            html.AppendFormat("{0}", citac("Datoteka"))
                            html.Append("<button class=""btn bg-transparent dropdown-toggle theme-toggle text-light"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"">")
                            html.Append("<i class=""fa fa-cog""></i>")
                            html.Append("</button>")
                            html.Append("<div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">")
                            html.Append("<div class=""dropdown-menu-content"">")
                            html.AppendFormat("<a class=""dropdown-item"" href=""/CMS/Ajax/ZadanaSlikaClanka.aspx?SlikaID={0}&ClanakID={1}"">Postavi kao zadanu</a>", citac("ID"), ClanakID)
                            html.AppendFormat("<a class=""dropdown-item"" href=""/CMS/Ajax/IzbrisiSlikuClanka.aspx?SlikaID={0}&ClanakID={1}&file={2}"">Izbriši sliku</a>", citac("ID"), ClanakID, citac("Datoteka"))
                            html.Append("</div>") 'dropdown-menu-content
                            html.Append("</div>") 'dropdown-menu
                            html.Append("</div>") 'dropdown float-right
                            html.AppendFormat("<img class=""card-img-top"" src=""/Datoteke/Novosti/{0}"" alt=""IGRE.BA"">", citac("Datoteka"))
                            'html.Append("<div class=""chart-wrapper px-0"" style=""height: 70px;"" height=""70"">")
                            ''html.Append("<canvas id=""widgetChart1""></canvas>")
                            'html.Append("</div>") 'chart-wrapper px-0
                            html.Append("</div>") 'card-body pb-0
                            html.Append("</div>") 'card text-white bg-flat-color-1
                            'html.AppendFormat("<img class=""card-img-top"" src=""/Datoteke/Artikli/{0}"" alt=""CIAK"">", citac("Datoteka"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function NaslovClanka(ClanakID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Naslov FROM Novosti WHERE ID=@ClanakID"
                komanda.Parameters.AddWithValue("@ClanakID", ClanakID)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Naslov"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function NoviClanka() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim ClanakID As Integer = HttpContext.Current.Request.QueryString("id")

        html.Append("<div class=""col-lg-9"">")
        html.Append("<div class=""card artikalDet"">")


        html.Append("<form action=""/CMS/Ajax/IsnertClanka.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

        html.Append("<div class=""card-header""><strong>Izmjena</strong> članka</div>")
        html.Append("<div class=""card-body card-block"">")
        html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", "0")

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""text-input"" class=""form-control-label"">Naziv artikla</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtNaslov"" name=""txtNaslov"" value=""{0}"" placeholder=""Naslov članka"" class=""form-control"">", "")
        'html.Append("<small class=""form-text text-muted"">Unesite naziv artikla</small>")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label class=""form-control-label"">Oznake</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col col-md-9"">")
        html.Append("<div class=""form-check-inline form-check"">")
        html.Append("<label for=""inline-checkbox2"" class=""form-check-label"">")
        html.AppendFormat("<input type=""checkbox"" id=""chkAktivno"" name=""chkAktivno"" class=""form-check-input"" {0}>", "checked")
        html.Append("Aktivno &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>")
        html.Append("</div>") '"
        html.Append("</div>") 'col col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.AppendFormat("<label for=""select"" class=""form-control-label"">Video link</label>")
        html.Append("</div>") 'col col-md-3
        html.Append("<div class=""col-12 col-md-9"">")
        html.AppendFormat("<input type=""text"" id=""txtVideoLink"" name=""txtVideoLink"" value=""{0}"" placeholder="" video link"" class=""form-control"" />", "")
        html.Append("</div>") 'col-12 col-md-9
        html.Append("</div>") 'row form-group

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""textarea-input"" class=""form-control-label"">Kratki opis</label>")
        html.Append("</div>")
        html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
        html.AppendFormat("<textarea name=""txtUkratko"" id=""txtUkratko"" rows=""3"" placeholder="" Ukratko..."" class=""form-control"">{0}</textarea>", "")
        html.Append("</div>")
        html.Append("</div>")

        html.Append("<div class=""row form-group"">")
        html.Append("<div class=""col col-md-3"">")
        html.Append("<label for=""textarea-input"" class=""form-control-label"">Članak</label>")
        html.Append("</div>")
        html.Append("<div class=""col-12 col-md-9"" style=""font-size: 1rem;"">")
        'html.AppendFormat("<textarea name=""txtClanak"" id=""txtClanak"" rows=""9"" placeholder=""Clanak..."" class=""form-control tiny"">{0}</textarea>", "")
        html.AppendFormat("<textarea name=""txtClanak"" id=""txtClanak"" rows=""9"" placeholder=""Clanak..."" class=""form-control"">{0}</textarea>", "")
        html.Append("</div>")
        html.Append("</div>")

        html.Append("</div>") 'card-body card-block

        html.Append("<div class=""card-footer"">")
        html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
        html.Append("<a href=""/CMS/Pregled.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
        html.Append("</div>") 'card-footer

        html.Append("</form>")

        html.Append("</div>") 'card artikalDet
        html.Append("</div>") 'col-lg-9



        Return html.ToString()
    End Function

    'Public Shared Function Statika() As String
    '    Dim html As New StringBuilder()
    '    Dim putanja As String = Komponente.conekcija()
    '    Dim id As Integer = HttpContext.Current.Request.QueryString("id")

    '    Using konekcija As New SqlConnection(putanja)
    '        konekcija.Open()
    '        Using komanda As New SqlCommand()
    '            komanda.Connection = konekcija
    '            komanda.CommandType = CommandType.Text
    '            komanda.CommandText = "SELECT * FROM Statika WHERE id=@id"
    '            komanda.Parameters.AddWithValue("@id", id)
    '            Using citac As SqlDataReader = komanda.ExecuteReader()
    '                If citac IsNot Nothing Then
    '                    While citac.Read()
    '                        html.Append("<form action=""/CMS/Ajax/UpdateStatika.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

    '                        html.Append("<div class=""card-header""><strong>Izmjena</strong></div>")
    '                        html.Append("<div class=""card-body card-block"">")
    '                        html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))

    '                        html.Append("<div class=""row form-group"">")
    '                        html.Append("<div class=""col col-md-2"">")
    '                        html.Append("<label for=""textarea-input"" class=""form-control-label"">Sadržaj:</label>")
    '                        html.Append("</div>")
    '                        html.Append("<div class=""col-12 col-md-10"" style=""font-size: 1rem;"">")
    '                        'html.AppendFormat("<textarea name=""txtVrijednost"" id=""txtVrijednost"" rows=""19"" placeholder=""opis..."" class=""form-control tinyMax"">{0}</textarea>", citac("Vrijednost"))
    '                        html.AppendFormat("<textarea name=""txtVrijednost"" id=""txtVrijednost"" rows=""19"" placeholder=""opis..."" class=""form-control"">{0}</textarea>", citac("VrijednostBulk"))
    '                        html.Append("</div>")
    '                        html.Append("</div>")

    '                        html.Append("</div>") 'card-body card-block

    '                        html.Append("<div class=""card-footer"">")
    '                        html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
    '                        html.Append("<a href=""/CMS/Dashboard.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
    '                        html.Append("</div>") 'card-footer

    '                        html.Append("</form>")
    '                    End While
    '                End If
    '            End Using
    '        End Using
    '    End Using

    '    Return html.ToString()
    'End Function

    Public Shared Function OpcePostavke() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row bazaArtikala"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-md-6"">")
        html.Append(Rate())
        html.Append("</div>") 'col-md-6
        html.Append("<div class=""col-md-6"">")
        html.Append(Dostava())
        html.Append("</div>") 'col-md-6
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Private Shared Function Rate() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()



        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM BrojRata WHERE Aktivno='1' ORDER BY Vrijednost ASC"
                'komanda.Parameters.AddWithValue("@ID", 33)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<form id=""frmRate"" action=""/CMS/Ajax/UpdateBrojRata.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
                            'html.Append("<div class=""card-header""><strong>Postavke</strong></div>")
                            html.Append("<div class=""card-body card-block"" style=""border:1px solid #dfdfdf;"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))

                            html.Append("<div class=""row form-group"" style=""margin-bottom: 0;"">")
                            html.Append("<div class=""col col-md-7"">")
                            html.AppendFormat("<label for=""textarea-input"" class=""form-control-label"">{0}:</label>", citac("Opis"))
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-2"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<input type=""text"" id=""txtMarza"" name=""txtMarza"" value=""{0}"" placeholder="""" class=""form-control"">", citac("Marza"))
                            'html.AppendFormat("<textarea name=""txtVrijednost"" id=""txtVrijednost"" rows=""19"" placeholder=""opis..."" class=""form-control tinyMax"">{0}</textarea>", citac("Vrijednost"))
                            html.Append("</div>")
                            html.Append("<div class=""col col-md-1"">")
                            html.Append(" %")
                            html.Append("</div>")
                            html.Append("<div class=""col col-md-2"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button>")
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("</div>") 'card-body card-block

                            'html.Append("<div class=""card-footer"">")
                            'html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            'html.Append("<a href=""/CMS/Dashboard.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            'html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using

        'html.Append("<div class=""card-footer"">")
        'html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
        ''html.Append("<a href=""/CMS/Dashboard.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
        'html.Append("</div>") 'card-footer

        Return html.ToString
    End Function


    Public Shared Function StatikaAll() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row Statika"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblStatika"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>&nbsp;</th>")
        html.Append("<th>Naslov</th>")
        'html.Append("<th>Aktivno</th>")
        'html.Append("<th>Prioritet</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(StatikaAllList())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString()
    End Function

    Public Shared Function StatikaAllList() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Statika;"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class="""" id=""{0}"">", citac("ID"))
                            html.AppendFormat("<td>&nbsp;</td>")
                            html.Append("<td>")
                            html.AppendFormat("<a href=""Statika.aspx?ID={0}"">", citac("ID"))
                            html.AppendFormat("{0}", citac("Naslov"))
                            html.Append("</a>")
                            html.Append("</td>")
                            'html.Append("<td>")
                            'html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            'html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            'html.Append("<span class=""switch-label""></span>")
                            'html.Append("<span class=""switch-handle""></span>")
                            'html.Append("</label>")
                            'html.Append("</td>")
                            'html.AppendFormat("<td>")
                            'html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetUp"" data-id=""{0}"" data-prioritet=""{1}""><i class=""fa fa-upload""></i></button>", citac("ID"), citac("Prioritet"))
                            'html.AppendFormat("<span class=""badge badge-light"" style=""padding: 10px; margin: 0px 3px;"">{0}</span>", citac("Prioritet"), citac("ID"))
                            'html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetDown"" data-id=""{0}"" data-prioritet=""{1}"" ><i class=""fa fa-download""></i></button>", citac("ID"), citac("Prioritet"))
                            'html.AppendFormat("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Public Shared Function Statika() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        Dim id As Integer = HttpContext.Current.Request.QueryString("id")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Statika WHERE id=@id"
                komanda.Parameters.AddWithValue("@id", id)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<form action=""/CMS/Ajax/UpdateStatika.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

                            html.Append("<div class=""card-header""><strong>Izmjena</strong></div>")
                            html.Append("<div class=""card-body card-block"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))

                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-2"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Naslov:</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-10"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<input type=""text"" id=""txtNaslov"" name=""txtNaslov"" value=""{0}"" placeholder="""" class=""form-control"">", citac("Naslov"))
                            html.Append("</div>")
                            html.Append("</div>")

                            'Igre.ba
                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-2"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Igre.ba:</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-10"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<textarea name=""txtVrijednost"" id=""txtVrijednost"" rows=""19"" placeholder=""opis..."" class=""form-control tinyMax"">{0}</textarea>", citac("Vrijednost"))
                            html.Append("</div>")
                            html.Append("</div>")

                            'Bulk.ba
                            html.Append("<div class=""row form-group"">")
                            html.Append("<div class=""col col-md-2"">")
                            html.Append("<label for=""textarea-input"" class=""form-control-label"">Bulk.ba:</label>")
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-10"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<textarea name=""txtVrijednostBulk"" id=""txtVrijednostBulk"" rows=""19"" placeholder=""opis..."" class=""form-control tinyMax"">{0}</textarea>", citac("VrijednostBulk"))
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("</div>") 'card-body card-block

                            html.Append("<div class=""card-footer"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            html.Append("<a href=""/CMS/Informacije.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function Dostava() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Postavke WHERE Naziv IN ('CijenaDostava','BesplatnaDostava','PopustZiralno')"
                komanda.Parameters.AddWithValue("@ID", 33)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<form id=""frmDostava"" action=""/CMS/Ajax/UpdatePostavke.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")

                            'html.Append("<div class=""card-header""><strong>Postavke</strong></div>")
                            html.Append("<div class=""card-body card-block"" style=""border:1px solid #dfdfdf;"">")
                            html.AppendFormat("<input type=""hidden"" id=""hidId"" name=""hidId"" value=""{0}"" class=""form-control"">", citac("ID"))

                            html.Append("<div class=""row form-group"" style=""margin-bottom: 0;"">")
                            html.Append("<div class=""col col-md-3"">")
                            html.AppendFormat("<label for=""textarea-input"" class=""form-control-label"">{0}:</label>", citac("Naziv"))
                            html.Append("</div>")
                            html.Append("<div class=""col-12 col-md-2"" style=""font-size: 1rem;"">")
                            html.AppendFormat("<input type=""text"" id=""txtVrijednost"" name=""txtVrijednost"" value=""{0}"" placeholder="""" class=""form-control"">", citac("Vrijednost"))
                            'html.AppendFormat("<textarea name=""txtVrijednost"" id=""txtVrijednost"" rows=""19"" placeholder=""opis..."" class=""form-control tinyMax"">{0}</textarea>", citac("Vrijednost"))
                            html.Append("</div>")
                            html.Append("<div class=""col col-md-2"">")
                            html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            html.Append("</div>")
                            html.Append("</div>")

                            html.Append("</div>") 'card-body card-block

                            'html.Append("<div class=""card-footer"">")
                            'html.Append("<button type=""submit"" class=""btn btn-primary btn-sm""><i class=""fa fa-dot-circle-o""></i> Spremi</button> ")
                            'html.Append("<a href=""/CMS/Dashboard.aspx"" class=""btn btn-danger btn-sm""><i class=""fa fa-ban""></i> Odustani</a>")
                            'html.Append("</div>") 'card-footer

                            html.Append("</form>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString
    End Function

    'ComTrade
    Public Shared Function ComTradeKategorije() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row ComTradeKategorije"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblComTradeKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>ComTrade Grupa</th>")
        html.Append("<th>IGRE.BA Grupa</th>")
        html.Append("<th>Aktivno IGRE.BA</th>")
        html.Append("<th>BULK.BA Grupa</th>")
        html.Append("<th>Aktivno BULK.BA</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(ComTradeKategorijeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function ComTradeKategorijeMrezaTablica() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeComTrade ORDER BY NazivGrupe ASC"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraGrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""ArtikliPodKategorijeCMS.aspx?PodKategorijaID={0}"">", citac("GrupaUniverzalID"))
                            html.AppendFormat("{0}", citac("NazivGrupe"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", UniverzalGrupeComTrade(citac("GrupaUniverzalID"), citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input btnIgreAct"" data-id=""{1}"" data-grpuni=""{2}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"), citac("GrupaUniverzalID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", BulkGrupeComTrade(citac("GrupaBulkID"), citac("ID")))
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input btnBulkAct"" data-id=""{1}"" data-grpuni=""{2}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"), citac("GrupaBulkID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function UniverzalGrupeComTrade(GrupaUniverzalID As Integer, GrupaComTrade As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<select name=""ddlUniverzalGrupeComTrade"" id=""ddlUniverzalGrupeComTrade"" class=""form-control ddlUniverzalGrupeComTrade"">")
        html.AppendFormat("<option value=""0"" data-id=""{0}"">Odaberite podkategoriju</option>", GrupaComTrade)
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupe ORDER BY Grupa"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" data-id=""{3}"" {2}>{1} ({4})</option>", citac("ID"), citac("Grupa"), If(GrupaUniverzalID = citac("ID"), " selected", ""), GrupaComTrade, NadGrupa(citac("NadGrupaID")))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Private Shared Function BulkGrupeComTrade(GrupaBulkID As Integer, GrupaComTrade As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<select name=""ddlBulkGrupeComTrade"" id=""ddlBulkGrupeComTrade"" class=""form-control ddlBulkGrupeComTrade"">")
        html.AppendFormat("<option value=""0"" data-id=""{0}"">Odaberite podkategoriju</option>", GrupaComTrade)
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeBulk ORDER BY Grupa"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" data-id=""{3}"" {2}>{1} ({4})</option>", citac("ID"), citac("Grupa"), If(GrupaBulkID = citac("ID"), " selected", ""), GrupaComTrade, NadGrupa(citac("NadGrupaID")))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    Private Shared Function NadGrupa(NadGrupaID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT NadGrupa FROM ArtikliNadGrupe WHERE ID=@NadGrupaID"
                komanda.Parameters.AddWithValue("@NadGrupaID", NadGrupaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("NadGrupa"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    'UniExpert
    Public Shared Function UniExpertKategorije() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row UniExpertKategorije"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblUniExpertKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>UniExpert Grupa</th>")
        html.Append("<th>Univerzal gropa</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(UniExpertKategorijeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function UniExpertKategorijeMrezaTablica() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeUniExpert ORDER BY NazivGrupe ASC"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraGrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""ArtikliPodKategorijeCMS.aspx?PodKategorijaID={0}"">", citac("GrupaUniverzalID"))
                            html.AppendFormat("{0}", citac("NazivGrupe"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", UniverzalGrupeUniExpert(citac("GrupaUniverzalID"), citac("ID")))
                            'html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtiklePodGrupe(citac("ID")), PrebrojiArtiklePodGrupeDostupne(citac("ID")))
                            html.AppendFormat("<td></td>", "", "")
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" data-grpuni=""{2}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"), citac("GrupaUniverzalID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function UniverzalGrupeUniExpert(GrupaUniverzalID As Integer, GrupaComTrade As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<select name=""ddlUniverzalGrupeUniExpert"" id=""ddlUniverzalGrupeUniExpert"" class=""form-control ddlUniverzalGrupeUniExpert"">")
        html.AppendFormat("<option value=""0"" data-id=""{0}"">Odaberite podkategoriju</option>", GrupaComTrade)
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupe ORDER BY Grupa"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" data-id=""{3}"" {2}>{1} ({4})</option>", citac("ID"), citac("Grupa"), If(GrupaUniverzalID = citac("ID"), " selected", ""), GrupaComTrade, NadGrupa(citac("NadGrupaID")))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    'StarTech 
    Public Shared Function StarTechKategorije() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row StarTechKategorije"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblStarTechKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>StarTech Grupa</th>")
        html.Append("<th>Univerzal gropa</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(StarTechKategorijeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function StarTechKategorijeMrezaTablica() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeStarTech ORDER BY NazivGrupe ASC"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraGrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""ArtikliPodKategorijeCMS.aspx?PodKategorijaID={0}"">", citac("GrupaUniverzalID"))
                            html.AppendFormat("{0}", citac("NazivGrupe"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", UniverzalGrupeStarTech(citac("GrupaUniverzalID"), citac("ID")))
                            'html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtiklePodGrupe(citac("ID")), PrebrojiArtiklePodGrupeDostupne(citac("ID")))
                            html.AppendFormat("<td></td>", "", "")
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" data-grpuni=""{2}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"), citac("GrupaUniverzalID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function UniverzalGrupeStarTech(GrupaUniverzalID As Integer, GrupaStarTech As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<select name=""ddlUniverzalGrupeStarTech"" id=""ddlUniverzalGrupeStarTech"" class=""form-control ddlUniverzalGrupeStarTech"">")
        html.AppendFormat("<option value=""0"" data-id=""{0}"">Odaberite podkategoriju</option>", GrupaStarTech)
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupe ORDER BY Grupa"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" data-id=""{3}"" {2}>{1} ({4})</option>", citac("ID"), citac("Grupa"), If(GrupaUniverzalID = citac("ID"), " selected", ""), GrupaStarTech, NadGrupa(citac("NadGrupaID")))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function

    'Digitalis 
    Public Shared Function DigitalisKategorije() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row DigitalisKategorije"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblDigitalisKategorije"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<th>Šifra</th>")
        html.Append("<th>Digitalis Grupa</th>")
        html.Append("<th>Univerzal gropa</th>")
        html.Append("<th>Broj artikala</th>")
        html.Append("<th>Aktivno</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(DigitalisKategorijeMrezaTablica())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function DigitalisKategorijeMrezaTablica() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupeDigitalis ORDER BY NazivGrupe ASC"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("SifraGrupe"))
                            html.Append("<td>")
                            html.AppendFormat("<a href=""ArtikliPodKategorijeCMS.aspx?PodKategorijaID={0}"">", citac("GrupaUniverzalID"))
                            html.AppendFormat("{0}", citac("NazivGrupe"))
                            html.Append("</a>")
                            html.Append("</td>")
                            html.AppendFormat("<td>{0}</td>", UniverzalGrupeDigitalis(citac("GrupaUniverzalID"), citac("ID")))
                            'html.AppendFormat("<td>{1}/{0}</td>", PrebrojiArtiklePodGrupe(citac("ID")), PrebrojiArtiklePodGrupeDostupne(citac("ID")))
                            html.AppendFormat("<td></td>", "", "")
                            html.Append("<td>")
                            html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" data-grpuni=""{2}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"), citac("GrupaUniverzalID"))
                            html.Append("<span class=""switch-label""></span>")
                            html.Append("<span class=""switch-handle""></span>")
                            html.Append("</label>")
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Shared Function UniverzalGrupeDigitalis(GrupaUniverzalID As Integer, GrupaDigitalis As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        html.Append("<select name=""ddlUniverzalGrupeDigitalis"" id=""ddlUniverzalGrupeDigitalis"" class=""form-control ddlUniverzalGrupeDigitalis"">")
        html.AppendFormat("<option value=""0"" data-id=""{0}"">Odaberite podkategoriju</option>", GrupaDigitalis)
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM ArtikliGrupe ORDER BY Grupa"
                'komanda.Parameters.AddWithValue("@KategorijaID", KategorijaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<option value=""{0}"" data-id=""{3}"" {2}>{1} ({4})</option>", citac("ID"), citac("Grupa"), If(GrupaUniverzalID = citac("ID"), " selected", ""), GrupaDigitalis, NadGrupa(citac("NadGrupaID")))
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("</select>")

        Return html.ToString()
    End Function



    'Katalozi

    Public Shared Function Katalozi() As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.conekcija()

        html.Append("<div class=""row Katalozi"">")
        html.Append("<div class=""col-md-12"">")
        html.Append("<div class=""card"">")
        'html.Append("<div class=""card-header""><strong class=""card-title"">Artikli</strong></div>")
        html.Append("<div class=""card-body"">")
        html.Append("<div class=""maska"" id=""maska""><div class=""loader""></div></div>")
        html.Append("<div id=""bootstrap-data-table_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4 no-footer"">")

        html.Append("<div class=""row"">")
        html.Append("<div class=""col-sm-12"">")
        html.Append("<table class=""table table-bordered tblKatalozi"">")
        html.Append("<thead class=""thead-dark"">")
        html.Append("<tr>")
        html.Append("<form action=""Ajax/PrenesiKatalog.aspx"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"">")
        html.Append("<th style=""background-color:#fff;color: #000;"" colspan=""2""><input type=""file"" id=""file-input"" name=""file-input"" class=""form-control-file""></th>")
        'html.Append("<th style=""background-color:#fff;color: #000;"" colspan=""2"">preporučeno: 1920x700px</th>") 'preporučeno: 1920x574px
        html.Append("<th style=""background-color:#fff;color: #000;""><button type=""submit"" class=""btn btn-primary btn-sm"" onclick=""document.getElementById('maska').style.display='block';""> <i class=""fa fa-dot-circle-o""></i> Prenesi</button> MAX 15 MB</th>")
        html.Append("</form>")
        html.Append("</tr>")

        html.Append("<tr>")
        html.Append("<th style=""width:65px;"">&nbsp;</th>")
        html.Append("<th>Naziv datoteke</th>")
        html.Append("<th>Opis datoteke</th>")
        html.Append("<th style=""min-width: 120px;"">&nbsp;</th>")
        html.Append("<th>&nbsp;</th>")
        html.Append("<th>Brisanje</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody class=""tbody"">")
        html.Append(KataloziLista())
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>") 'col-sm-12
        html.Append("</div>") 'row

        html.Append("</div>") 'bootstrap-data-table_wrapper
        html.Append("</div>") 'card-body
        html.Append("</div>") 'card
        html.Append("</div>") 'col-md-12
        html.Append("</div>") 'row

        Return html.ToString
    End Function

    Public Shared Function KataloziLista() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()
        'Dim KategorijaID As Integer = HttpContext.Current.Request.QueryString("KategorijaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Katalozi ORDER BY Prioritet ASC, ID DESC;"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<tr class=""{0}"" id=""{1}"">", If(citac("Aktivno") = 0, "neaktivno", ""), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", Komponente.ikonaDatoteke(citac("Datoteka"), citac("Opis")))
                            html.AppendFormat("<td><button type=""button"" class=""btn btn-secondary mb-1"" data-toggle=""modal"" data-target=""#mediumModal{1}"">{0}</button></td>", citac("OrgNazivDatoteke"), citac("ID"))
                            html.AppendFormat("<td>{0}</td>", citac("Opis"))
                            html.AppendFormat("<td>")
                            'html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetUp"" data-id=""{0}"" data-prioritet=""{1}""><i class=""fa fa-upload""></i></button>", citac("ID"), citac("Prioritet"))
                            'html.AppendFormat("<span class=""badge badge-light"" style=""padding: 10px; margin: 0px 3px;"">{0}</span>", citac("Prioritet"), citac("ID"))
                            'html.AppendFormat("<button type=""button"" class=""btn btn-warning btn-sm btnPrioritetDown"" data-id=""{0}"" data-prioritet=""{1}"" ><i class=""fa fa-download""></i></button>", citac("ID"), citac("Prioritet"))
                            html.Append("</td>")
                            html.Append("<td>")
                            'html.Append("<label class=""switch switch-3d switch-primary mr-3"">")
                            'html.AppendFormat("<input type=""checkbox"" class=""switch-input"" data-id=""{1}"" {0}>", If(citac("Aktivno") = 0, " ", " checked"), citac("ID"))
                            'html.Append("<span class=""switch-label""></span>")
                            'html.Append("<span class=""switch-handle""></span>")
                            'html.Append("</label>")
                            html.Append("</td>")
                            html.AppendFormat("<td><a href=""/CMS/Ajax/IzbrisiKatalog.aspx?id={0}&file={1}"" class=""btn btn-danger btn-block"" onclick=""document.getElementById('maska').style.display='block';"" >Ukloni</a></td>", citac("ID"), citac("Datoteka"))
                            html.Append("</tr>")
                            'modal izmjena
                            html.AppendFormat("<div class=""modal fade"" id=""mediumModal{0}"" tabindex=""-1"" role=""dialog"" aria-labelledby=""mediumModalLabel"" aria-hidden=""true"">", citac("ID"))
                            html.Append("<div class=""modal-dialog modal-lg"" role=""document"">")
                            html.Append("<div class=""modal-content modPass"">")
                            html.AppendFormat("<form action=""/CMS/Ajax/PromjeniKatalog.aspx"" id=""add-project-form"" method=""post"" enctype=""multipart/form-data"" class=""form-horizontal"" autocomplete=""off"">")
                            html.Append("<div class=""modal-header"">")
                            html.Append("<h5 class=""modal-title"" id=""smallmodalLabel"">Izmjena naziva i opisa</h5>")
                            html.Append("<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">")
                            html.Append("<span aria-hidden=""true"">&times;</span>")
                            html.Append("</button>")
                            html.Append("</div>") 'modal-header
                            html.Append("<div class=""modal-body"">")
                            html.Append("<p>")
                            html.AppendFormat("<input type=""hidden"" id=""slikaID"" name=""slikaID"" value=""{0}"" class=""form-control"">", citac("ID"))
                            'html.AppendFormat("<input type=""hidden"" id=""stariNaziv"" name=""stariNaziv"" value=""{0}"" class=""form-control"">", citac("OrgNazivDatoteke"))
                            'html.AppendFormat("<input type=""text"" id=""noviNaziv"" name=""noviNaziv"" value=""{0}"" placeholder=""unesite novi link"" class=""form-control"" >", citac("OrgNazivDatoteke"))
                            html.AppendFormat("<br/><textarea name=""opis"" id=""textarea-input"" rows=""9"" placeholder=""Opis..."" class=""form-control"">{0}</textarea>", citac("Opis"))
                            html.Append("</p>")
                            html.Append("</div>") 'modal-body
                            html.Append("<div class=""modal-footer"">")
                            html.Append("<button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Odustani</button>")
                            html.Append("<button type=""submit"" class=""btn btn-primary btnIzmjeniPass"">Izmjeni</button>")
                            html.Append("</div>") 'modal-footer
                            html.Append("</form>")
                            html.Append("</div>") 'modal-content
                            html.Append("</div>") 'modal-dialog modal-sm
                            html.Append("</div>") 'modal fade
                            'modal izmjena
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

End Class
