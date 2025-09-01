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
        If URL_path.Contains("/registracija") Then
            Context.RewritePath(URL_path.Replace("/registracija", "/Korisnik/Prijava.aspx"))
        End If
        If URL_path.Contains("/login") Then
            Context.RewritePath(URL_path.Replace("/login", "/Korisnik/Prijava.aspx"))
        End If
        If URL_path.Contains("/izgubljena-lozinka") Then
            Context.RewritePath(URL_path.Replace("/izgubljena-lozinka", "/Korisnik/IzgubljenaLozinka.aspx"))
        End If
        If URL_path.Contains("/kosarica") Then
            Context.RewritePath(URL_path.Replace("/kosarica", "/Kosarica/Kosarica.aspx"))
        End If
        If URL_path.Contains("/GoPay") Then
            Context.RewritePath(URL_path.Replace("/GoPay", "/Ajax/PosaljiNarudzbuSession.aspx"))
        End If
        If URL_path.Contains("/adresa-dostave") Then
            Context.RewritePath(URL_path.Replace("/adresa-dostave", "/Kosarica/AdresaDostave.aspx"))
        End If
        If URL_path.Contains("/pretraga") Then
            Context.RewritePath(URL_path.Replace("/pretraga", "/Pretraga.aspx"))
        End If
        If URL_path.Contains("/kontakt") Then
            Context.RewritePath(URL_path.Replace("/kontakt", "/Kontakt.aspx"))
        End If

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
                                If URL_path.Contains("/artikal/") Then
                                    Dim segments() As String = URL_path.Split("/"c)
                                    If segments.Length > 2 AndAlso IsNumeric(segments(2)) Then
                                        Context.RewritePath("/Artikal.aspx?id=" & segments(2))
                                    End If
                                End If
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
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            If URL_path.Contains("/account/" & citac("ID") & "/" & citac("PrivremeniID")) Then
                                Context.RewritePath(URL_path.Replace("/account/" & citac("ID") & "/" & citac("PrivremeniID"), "/Korisnik/Aktivacija.aspx?id=" & citac("ID") & "&ssid=" & citac("PrivremeniID")))
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError()
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class