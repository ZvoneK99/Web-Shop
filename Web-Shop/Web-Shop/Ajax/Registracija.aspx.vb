Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports System.Net

Public Class RegistracijaAjax
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ImePrezime As String = HttpContext.Current.Request.Params("ImePrezime")
        Dim Email As String = HttpContext.Current.Request.Params("Email")
        Dim Adresa As String = HttpContext.Current.Request.Params("Adresa")
        Dim Grad As String = HttpContext.Current.Request.Params("Grad")
        Dim ZIP As String = HttpContext.Current.Request.Params("ZIP")
        Dim Telefon As String = HttpContext.Current.Request.Params("Telefon")
        Dim lozinka1 As String = HttpContext.Current.Request.Params("lozinka1")
        Dim lozinka2 As String = HttpContext.Current.Request.Params("lozinka2")
        Dim datumSesije = Date.UtcNow()
        Dim SessionID = HttpContext.Current.Session.SessionID
        Dim PrivremeniID As String = Format(datumSesije, "yyyyMMdd") & SessionID & Format(datumSesije, "hhMMss")

        Dim putanja As String = Komponente.SQLKonekcija()

        Dim formID As Integer = Convert.ToInt32(Request.Form("ID"))
        If lozinka1.Trim = lozinka2.Trim Then
            If lozinka1.Trim <> "" Then
                Using konekcija As New SqlConnection(putanja)
                    konekcija.Open()
                    Using komanda As New SqlCommand()
                        komanda.Connection = konekcija
                        komanda.CommandType = CommandType.StoredProcedure
                        komanda.CommandText = "UnesiKorisnika"
                        'komanda.Parameters.AddWithValue("@ID", formID)
                        komanda.Parameters.AddWithValue("@ImePrezime", ImePrezime)
                        komanda.Parameters.AddWithValue("@Email", Email)
                        komanda.Parameters.AddWithValue("@Adresa", Adresa)
                        komanda.Parameters.AddWithValue("@Grad", Grad)
                        komanda.Parameters.AddWithValue("@ZIP", ZIP)
                        komanda.Parameters.AddWithValue("@Telefon", Telefon)
                        komanda.Parameters.AddWithValue("@Lozinka", (lozinka1.Trim))
                        komanda.Parameters.AddWithValue("@PrivremeniID", PrivremeniID)
                        komanda.Parameters.Add("@TipUnosa", SqlDbType.Int)
                        komanda.Parameters("@TipUnosa").Direction = ParameterDirection.Output
                        komanda.Parameters.Add("@NoviID", SqlDbType.Int)
                        komanda.Parameters("@NoviID").Direction = ParameterDirection.Output
                        komanda.ExecuteNonQuery()

                        Dim KorisnikID = komanda.Parameters("@NoviID").Value
                        Response.Redirect("/registracija?msg=uspjesnaprijava")
                    End Using
                End Using
            Else
                Response.Redirect("/registracija?msg=pass&ImePrezime=" & ImePrezime & "&Adresa=" & Adresa & "&Grad=" & Grad & "&ZIP=" & ZIP & "&Telefon=" & Telefon & "&Email=" & Email)
            End If
        Else
            Response.Redirect("/registracija?msg=pass&ImePrezime=" & ImePrezime & "&Adresa=" & Adresa & "&Grad=" & Grad & "&ZIP=" & ZIP & "&Telefon=" & Telefon & "&Email=" & Email)
        End If
    End Sub

    'Private Sub PosaljiMailKorisnikuZaPotvrdu(KorisnikID As Integer)
    '    Dim poruka As New StringBuilder
    '    Dim putanja As String = Komponente.SQLKonekcija()

    '    poruka.Append("<table cellspacing=""0"" cellpadding=""0"" style=""border: none; width: 100%; font-family:Arial;"">")

    '    Using konekcija As New SqlConnection(putanja)
    '        konekcija.Open()
    '        Using komanda As New SqlCommand()
    '            komanda.Connection = konekcija
    '            komanda.CommandType = CommandType.Text
    '            komanda.CommandText = "SELECT * FROM Korisnici WHERE ID=@ID"
    '            komanda.Parameters.AddWithValue("@ID", KorisnikID)
    '            Using citac As SqlDataReader = komanda.ExecuteReader()
    '                If citac IsNot Nothing Then
    '                    While citac.Read()
    '                        'naziv kupca
    '                        poruka.Append("<tr>")
    '                        poruka.Append("<td style=""vertical-align:top;"">")
    '                        'If n.tvrtka <> "-" Then
    '                        '    poruka.Append("<b>Tvrtka:</b> " & n.tvrtka & "<br/>")
    '                        '    poruka.Append("<b>PDV broj:</b> " & n.oib & "<br/>")
    '                        'End If
    '                        poruka.Append("<b>Ime i prezime:</b><br/> " & citac("ImePrezime") & "<br/>" & citac("Adresa") & ",<br/> " & citac("ZIP") & " - " & citac("Grad") & ", ")
    '                        poruka.Append("<br/>Tel: " & citac("Telefon") & " <br/>e-mail: " & citac("Email") & "")
    '                        poruka.Append("</td>")
    '                    End While
    '                End If
    '            End Using
    '        End Using
    '    End Using

    '    'broj narudzbe
    '    'poruka.Append("<td><td rowspan=""3"" valign=""center"" align=""center"" style=""font-size:22px;""><b>Predračun br.&nbsp;" & Format(citacNar("DatumSlanja"), "yyyy") & "-" & citacNar("KupacID") & "-" & citacNar("ID") & "</b></td></tr>")
    '    'datum narudzbe
    '    poruka.Append("<tr><td><b>Datum:</b> " & Format(DateAndTime.Now(), "dd.MM.yyyy") & "</td></tr>")

    '    poruka.Append("</table><br/>")

    '    Using konekcija As New SqlConnection(putanja)
    '        konekcija.Open()
    '        Using komanda As New SqlCommand()
    '            komanda.Connection = konekcija
    '            komanda.CommandType = CommandType.Text
    '            komanda.CommandText = "SELECT * FROM Korisnici WHERE ID=@KorisnikID"
    '            komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID)
    '            Using citac As SqlDataReader = komanda.ExecuteReader()
    '                If citac IsNot Nothing Then
    '                    While citac.Read()
    '                        poruka.Append("<hr/>")
    '                        poruka.AppendFormat("{0},", citac("ImePrezime"))
    '                        poruka.Append("<br/><br/>")
    '                        poruka.Append("još samo jedan klik do registracije!")
    '                        poruka.Append("<br/><br/>")
    '                        ' poruka.AppendFormat("Kliknite na poveznicu ispod i aktivirajte svoj korisnički račun na RescueEquip.ba<br/>")
    '                        ' If HttpContext.Current.Request.Url.ToString.Contains("localhost") = False Then
    '                        'poruka.AppendFormat("<a href=""https://www.bulk.ba/account/{0}"">www.bulk.ba/account/{0}</a>", citac("ID"))
    '                        'poruka.AppendFormat("<a href=""https://www.bulk.ba/Korisnik/Aktivacija.aspx?id={0}&ssid={1}"">Aktiviraj račun</a>", citac("ID"), citac("PrivremeniID"))
    '                        ' Else
    '                        'poruka.AppendFormat("<a href=""http://localhost:53919/account/{0}"">www.bulk.ba/account/{0} </a>", citac("ID"))
    '                        'poruka.AppendFormat("<a href=""http://localhost:53919/Korisnik/Aktivacija.aspx?id={0}&ssid={1}"">Aktiviraj račun</a>", citac("ID"), citac("PrivremeniID"))
    '                        'End If'
    '                        poruka.Append("<br/><br/>")
    '                        'poruka.Append("Kliknuli ste? Uživajte u kupnji, napunite zalihe i uštedite vrijeme!")
    '                        'poruka.Append("<br/><br/>")
    '                        poruka.AppendFormat("Vaše korisničko ime: {0}", citac("Email"))
    '                        poruka.Append("<br/>")
    '                        poruka.Append("Vaša lozinka: ******** (poznata je samo vama)")
    '                        poruka.Append("<br/><br/>")
    '                        poruka.Append("<strong>Vaš RESCUEEQUIOP.ba Team</strong>")
    '                        poruka.Append("<hr/><br/><br/>")
    '                    End While
    '                End If
    '            End Using
    '        End Using
    '    End Using

    '    poruka.Append("<div style=""font-size: 10px;"">")
    '    poruka.Append("Ova elektronicka poruka i/ili bilo koji privitak ovoj poruci mogu<br/>")
    '    poruka.Append("sadrzavati povjerljive informacije. Otkrivanje njihova sadrzaja drugim<br/>")
    '    poruka.Append("osobama moguce je samo uz prethodno odobrenje. Ova poruka je namijenjena<br/>")
    '    poruka.Append("samo osobi/osobama kojima je adresirana. Ako vi niste osoba kojoj je ova<br/>")
    '    poruka.Append("poruka namijenjena, molim vas da je odmah izbrisete.")
    '    poruka.Append("</div>")


    '    Dim MailKupca As New MailAddress("zvonimir.kozul@fsre.sum.ba")
    '    ' SMTP konfiguracija
    '    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

    '    ' Mailtrap SMTP podaci
    '    Dim srv As New SmtpClient("smtp.mailtrap.io", 2525) 'port prema Mailtrap inboxu
    '    srv.EnableSsl = True
    '    srv.UseDefaultCredentials = False
    '    srv.Credentials = New System.Net.NetworkCredential("TVOJ_USERNAME", "TVOJ_PASSWORD") 'zamijeni sa Mailtrap credentials

    '    Dim mailFrom As New MailAddress("webshop@test.com", "WebShop Test")
    '    Dim MailPrimatelja As String = MailKupca.ToString()
    '    Dim mailToFirma As New MailAddress(MailPrimatelja)
    '    Dim mlFirma As New MailMessage(mailFrom, mailToFirma)

    '    mlFirma.SubjectEncoding = Encoding.UTF8
    '    mlFirma.Priority = MailPriority.Normal
    '    mlFirma.BodyEncoding = Encoding.UTF8
    '    mlFirma.IsBodyHtml = True
    '    mlFirma.Subject = "Aktivacija Vašeg računa"
    '    mlFirma.Body = String.Format(Dokument("/Ajax/predlozak2.htm"), poruka.ToString)
    '    mlFirma.ReplyToList.Add(New MailAddress("reply@test.com"))

    '    Try
    '        srv.Send(mlFirma)
    '    Catch ex As SmtpException
    '        HttpContext.Current.Response.Write("SMTP greška: " & ex.ToString())
    '    Finally
    '        mlFirma.Dispose()
    '    End Try



    '    ''mail kupca
    '    'Dim MailKupca As New MailAddress("zvonimir.kozul@fsre.sum.ba")
    '    '' SMTP konfiguracija
    '    'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '    'Dim srv As New SmtpClient(Komponente.Postavke("SmtpServer"), Komponente.Postavke("SmtpPort"))
    '    'srv.EnableSsl = False
    '    'srv.UseDefaultCredentials = False
    '    'srv.Credentials = New System.Net.NetworkCredential(Komponente.Postavke("SmtpUser"), Komponente.Postavke("SmtpLozinka"))
    '    'Dim mailFrom As New MailAddress("web@bulk.ba", Komponente.Postavke("Tvrtka"))

    '    'Dim MailPrimatelja As String = MailKupca.ToString()
    '    'Dim mailToFirma As New MailAddress(MailPrimatelja)
    '    'Dim mlFirma As New MailMessage(mailFrom, mailToFirma)

    '    'mlFirma.SubjectEncoding = Encoding.UTF8
    '    'mlFirma.Priority = MailPriority.Normal
    '    'mlFirma.BodyEncoding = Encoding.UTF8
    '    'mlFirma.IsBodyHtml = True
    '    'mlFirma.Subject = "Aktivacija Vašeg računa"
    '    'mlFirma.Body = String.Format(Dokument("/Ajax/predlozak2.htm"), poruka.ToString)
    '    'mlFirma.ReplyToList.Add(New MailAddress(Komponente.Postavke("MailOdgovor")))

    '    'Try
    '    '    srv.Send(mlFirma)
    '    'Catch ex As SmtpException
    '    '    HttpContext.Current.Response.Write("SMTP greška: " & ex.ToString())
    '    'Finally
    '    '    mlFirma.Dispose()
    '    'End Try


    'End Sub

    Private Function Dokument(ByVal p As String) As String
        Dim s As String = String.Empty
        p = Server.MapPath(p)
        Dim sr As StreamReader
        sr = File.OpenText(p)
        s = sr.ReadToEnd
        sr.Close()
        sr.Dispose()

        Return s
    End Function

    Private Function MailNovogKupca(KorisnikID As Integer) As String
        Dim html As New StringBuilder
        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT Email FROM Korisnici WHERE ID=@KorisnikID"
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Email"))
                        End While
                    End If
                End Using
            End Using
        End Using
        Return html.ToString
    End Function

End Class