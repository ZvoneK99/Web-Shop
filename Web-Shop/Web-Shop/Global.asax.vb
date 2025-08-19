Imports System.Web.SessionState
Imports System.Data.SqlClient

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim URL_path As String = HttpContext.Current.Request.Path

        If URL_path.Contains("/pocetna") Then
            Context.RewritePath(URL_path.Replace("/pocetna", "/Default.aspx"))
        End If
        If URL_path.Contains("/grupa") Then
            Context.RewritePath(URL_path.Replace("/grupa", "/ArtikliGrupe.aspx"))
        End If
        If URL_path.Contains("/podgrupa") Then
            Context.RewritePath(URL_path.Replace("/podgrupa", "/ArtikliPodGrupe.aspx"))
        End If
        If URL_path.Contains("/artikal") Then
            Context.RewritePath(URL_path.Replace("/artikal", "/Artikal.aspx"))
        End If
        If URL_path.Contains("/registracija") Then
            Context.RewritePath(URL_path.Replace("/registracija", "/Korisnik/Prijava.aspx"))
        End If
        If URL_path.Contains("/login") Then
            Context.RewritePath(URL_path.Replace("/login", "/Korisnik/Prijava.aspx"))
        End If
        If URL_path.Contains("/izgubljena-lozinka") Then
            Context.RewritePath(URL_path.Replace("/izgubljena-lozinka", "/Korisnik/IzgubljenaLozinka.aspx"))
        End If
        If URL_path.Contains("/MojRacun") Then
            Context.RewritePath(URL_path.Replace("/MojRacun", "/"))
        End If
        If URL_path.Contains("/kosarica") Then
            Context.RewritePath(URL_path.Replace("/kosarica", "/Kosarica/Kosarica.aspx"))
        End If
        If URL_path.Contains("/hvala") Then
            Context.RewritePath(URL_path.Replace("/hvala", "/Kosarica/Hvala.aspx"))
        End If
        If URL_path.Contains("/greska") Then
            Context.RewritePath(URL_path.Replace("/greska", "/404.aspx"))
        End If
        If URL_path.Contains("/GoPay") Then
            Context.RewritePath(URL_path.Replace("/GoPay", "/Ajax/PosaljiNarudzbuSession.aspx"))
        End If
        If URL_path.Contains("/recenzija") Then
            Context.RewritePath(URL_path.Replace("/recenzija", "/Korisnik/Recenzija.aspx"))
        End If
        If URL_path.Contains("/adresa-dostave") Then
            Context.RewritePath(URL_path.Replace("/adresa-dostave", "/Kosarica/AdresaDostave.aspx"))
        End If
        If URL_path.Contains("/kontakt") Then
            Context.RewritePath(URL_path.Replace("/kontakt", "/Kontakt.aspx"))
        End If
        If URL_path.Contains("/onama") Then
            Context.RewritePath(URL_path.Replace("/onama", "/ONama.aspx"))
        End If
        If URL_path.Contains("/blog") Then
            Context.RewritePath(URL_path.Replace("/blog", "/Novosti.aspx"))
        End If
        If URL_path.Contains("/katalozi") Then
            Context.RewritePath(URL_path.Replace("/katalozi", "/Katalozi.aspx"))
        End If
        If URL_path.Contains("/statika") Then
            Context.RewritePath(URL_path.Replace("/statika", "/Statika.aspx"))
        End If
        If URL_path.Contains("/pretraga") Then
            Context.RewritePath(URL_path.Replace("/pretraga", "/Pretraga.aspx"))
        End If
        If URL_path.Contains("/SuccessPikPay") Then
            Context.RewritePath(URL_path.Replace("/SuccessPikPay", "/PikPaySuccess.aspx"))
        End If
        If URL_path.Contains("/CancelPikPay") Then
            Context.RewritePath(URL_path.Replace("/CancelPikPay", "/PikPayCancel.aspx"))
        End If
        If URL_path.Contains("/EpaySuccess") Then
            Context.RewritePath(URL_path.Replace("/EpaySuccess", "/EpaySuccess.aspx"))
        End If
        If URL_path.Contains("/EpayCancel") Then
            Context.RewritePath(URL_path.Replace("/EpayCancel", "/EpayCancel.aspx"))
        End If

        'Using konekcija As New SqlConnection(putanja)
        '    konekcija.Open()
        '    Using komanda As New SqlCommand()
        '        komanda.Connection = konekcija
        '        komanda.CommandType = CommandType.Text
        '        komanda.CommandText = "SELECT * FROM Novosti"
        '        Using citac As SqlDataReader = komanda.ExecuteReader()
        '            If citac IsNot Nothing Then
        '                While citac.Read()
        '                    If URL_path.Contains("/clanak/" & Komponente.SrediNaziv(citac("Naslov"))) Then
        '                        Context.RewritePath(URL_path.Replace("/clanak/" & Komponente.SrediNaziv(citac("Naslov")), "/Clanak.aspx?id=" & citac("ID")))
        '                        'Context.RewritePath(URL_path.Replace("/clanak/" & Komponente.SrediNaziv(citac("Naslov")), "/Default.aspx"))
        '                    End If
        '                End While
        '            End If
        '        End Using
        '    End Using
        'End Using

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Artikli"
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            If URL_path.Contains("/artikal/" & citac("ID")) Then
                                Context.RewritePath(URL_path.Replace("/artikal/" & citac("ID"), "/Artikal.aspx?id=" & citac("ID")))
                                'Context.RewritePath(URL_path.Replace("/artikal/" & Komponente.SrediNaziv(citac("Naziv")), "/Default.aspx"))
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT ID, PrivremeniID FROM Korisnici"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            If URL_path.Contains("/account/" & citac("ID") & "/" & citac("PrivremeniID")) Then
                                Context.RewritePath(URL_path.Replace("/account/" & citac("ID") & "/" & citac("PrivremeniID"), "/Korisnik/Aktivacija.aspx?id=" & citac("ID") & "&ssid=" & citac("PrivremeniID")))
                            End If
                            'If URL_path.Contains("/account/") Then
                            '    Context.RewritePath(URL_path.Replace("/account/", "/Korisnik/Aktivacija.aspx?id=" & citac("ID")))
                            'End If
                        End While
                    End If
                End Using
            End Using
        End Using

        'If URL_path.Contains("/account") Then
        '    Context.RewritePath(URL_path.Replace("/account", "/Korisnik/Aktivacija.aspx"))
        'End If

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError()

        ' Logiranje greške (po želji)
        ' System.IO.File.AppendAllText(Server.MapPath("~/log.txt"), ex.ToString())

        ' Preusmjeravanje
        'Response.Clear()
        'Server.ClearError()
        'Response.Redirect("/greska")

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class