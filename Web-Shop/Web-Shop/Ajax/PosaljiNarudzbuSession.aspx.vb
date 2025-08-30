Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports System.Net

Public Class PosaljiNarudzbuSession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("Narudzba")) = False Then
            Dim nacin As String = Request.Form("nacin-placanja")
            Dim napomena As String = Request.Form("napomena")
            Dim NacinDostave As String = "Brza pošta" 'Request.Form("nacindostave")
            Dim n As Narudzba
            n = CType(Session("Narudzba"), Narudzba)
            UnesiKorisnikaUSesiju()
            'MsgBox(nacin)
            UnesiKorisnikaSQL(n, nacin, "1", napomena, NacinDostave)
            Session.Clear()
            Response.Redirect("/pocetna")
        End If
    End Sub

    Private Sub UnesiKorisnikaUSesiju()
        Dim ImePrezime As String = Request.Form("imePrezime")
        Dim Adresa As String = Request.Form("adresa")
        Dim Grad As String = Request.Form("grad")
        Dim ZIP As String = Request.Form("postBr")
        Dim Email As String = Request.Form("email")
        Dim Telefon As String = Request.Form("brTel")
        'Dim Napomena As String = Request.Form("txtNapomena")
        Dim NacinPlacanja As String = Request.Form("nacin-placanja")

        Dim n As Narudzba
        n = CType(Session("Narudzba"), Narudzba)
        n.Ime = ImePrezime
        n.Adresa = Adresa
        n.Mjesto = Grad
        n.zip = ZIP
        n.Mail = Email.Trim.ToLower
        n.Telefon = Telefon
        n.Napomena = "" ' Napomena
        n.Datum = DateTime.Now.Date

    End Sub

    Private Sub UnesiKorisnikaSQL(ByVal n As Narudzba, nacinPlacanja As String, BrojRata As Integer, napomena As String, NacinDostave As String)
        Dim formID As Integer = Convert.ToInt32(Request.Form("ID"))
        Dim putanja As String = Komponente.SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "UnesiKorisnika"
                'komanda.Parameters.AddWithValue("@ID", formID)
                komanda.Parameters.AddWithValue("@ImePrezime", n.Ime)
                komanda.Parameters.AddWithValue("@Adresa", n.Adresa)
                'komanda.Parameters.AddWithValue("@IdBroj", "")
                'komanda.Parameters.AddWithValue("@PdvBroj", "")
                komanda.Parameters.AddWithValue("@Grad", n.Mjesto)
                komanda.Parameters.AddWithValue("@ZIP", n.zip)
                komanda.Parameters.AddWithValue("@Telefon", n.Telefon)
                komanda.Parameters.AddWithValue("@Email", n.Mail)
                komanda.Parameters.AddWithValue("@Lozinka", Replace(n.Ime, " ", ""))
                komanda.Parameters.Add("@TipUnosa", SqlDbType.Int)
                komanda.Parameters("@TipUnosa").Direction = ParameterDirection.Output
                komanda.Parameters.Add("@NoviID", SqlDbType.Int)
                komanda.Parameters("@NoviID").Direction = ParameterDirection.Output
                komanda.ExecuteNonQuery()

                Dim KorisnikID = komanda.Parameters("@NoviID").Value
                Dim tipUnosa As Integer = komanda.Parameters("@TipUnosa").Value
                UnesiNarudzbu(n, KorisnikID, nacinPlacanja, "1", napomena, NacinDostave)
            End Using
        End Using

    End Sub

    Private Sub UnesiNarudzbu(ByVal n As Narudzba, KorisnikID As Integer, NacinPlacanja As String, BrojRata As Integer, napomena As String, NacinDostave As String)
        Dim formID As Integer = Convert.ToInt32(Request.Form("ID"))
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim BesplatnaDostavaZaArtikal As Boolean = Komponente.ProvjeriKosaricuDaLiImaBesplatneDostave(n)

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "UnesiNarudzbu"
                komanda.Parameters.AddWithValue("@KupacID", KorisnikID)
                komanda.Parameters.AddWithValue("@DatumKreiranja", DateAndTime.Now())
                komanda.Parameters.AddWithValue("@NacinPlacanja", NacinPlacanja)
                komanda.Parameters.AddWithValue("@NacinDostave", NacinDostave)
                komanda.Parameters.AddWithValue("@Napomena", napomena)
                komanda.Parameters.AddWithValue("@Gatway", NacinPlacanja)
                komanda.Parameters.AddWithValue("@BrojRata", "1")
                ' komanda.Parameters.AddWithValue("@IpAdresa", Komponente.IpAdresa())
                komanda.Parameters.AddWithValue("@BesplatnaDostava", BesplatnaDostavaZaArtikal)
                'komanda.Parameters.AddWithValue("@Domena", "www.igre.ba")
                komanda.Parameters.Add("@NoviID", SqlDbType.Int)
                komanda.Parameters("@NoviID").Direction = ParameterDirection.Output
                komanda.ExecuteNonQuery()

                Dim NarudzbaID = komanda.Parameters("@NoviID").Value
                UnesiStavke(KorisnikID, NarudzbaID, n, NacinPlacanja)
            End Using
        End Using

    End Sub

    Private Sub UnesiStavke(KorisnikID As Integer, NarudzbaID As Integer, n As Narudzba, NacinPlacanja As String)
        Dim putanja As String = Komponente.SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            For Each a As ArtikalSession In n.Artikli
                Using komanda As New SqlCommand()
                    komanda.Connection = konekcija
                    komanda.CommandType = CommandType.StoredProcedure
                    komanda.CommandText = "UnesiStavke"
                    komanda.Parameters.AddWithValue("@NarudzbaID", NarudzbaID)
                    komanda.Parameters.AddWithValue("@ArtikalID", a.id)
                    komanda.Parameters.AddWithValue("@NazivArtikla", a.naziv)
                    komanda.Parameters.AddWithValue("@Cijena", a.JedCijena)
                    komanda.Parameters.AddWithValue("@Kolicina", a.Kolicina)
                    komanda.ExecuteNonQuery()
                End Using
            Next
        End Using
        If NacinPlacanja = "karticno" Then
            Response.Redirect("/Ajax/SessionPikPayPlacanje.aspx")
        End If

        PosaljiMail(KorisnikID, NarudzbaID, n)
    End Sub


    Private Sub PosaljiMail(KorisnikID As Integer, NarudzbaID As Integer, n As Narudzba)
        Dim poruka As New StringBuilder
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim CijenaDostave As Integer = Komponente.Postavke("CijenaDostava")
        Dim BesplatnaDostava As Integer = Komponente.Postavke("BesplatnaDostava")
        Dim PoreznaStopa1 As Integer = Komponente.Postavke("PoreznaStopa1")
        Dim NacinPlacanja As String = NacinPlacanjaNarudzbe(NarudzbaID)
        Dim NacinDostave As String = NacinDostaveNarudzbe(NarudzbaID)

        Dim NaplataDostave = If(n.Ukupno >= BesplatnaDostava, 0, CijenaDostave)
        Dim BesplatnaDostavaZaArtikal As Boolean = Komponente.ProvjeriKosaricuDaLiImaBesplatneDostave(n)
        If BesplatnaDostavaZaArtikal = True Then
            NaplataDostave = 0
        End If

        If Komponente.Suma(NarudzbaID) > BesplatnaDostava Then
            NaplataDostave = 0
        End If

        If NacinDostave = "Preuzimanje u poslovnici" Then
            NaplataDostave = 0
        End If

        'Kupac
        poruka.Append("<table cellspacing=""0"" cellpadding=""0"" style=""border: none; width: 100%; font-family:Arial;"">")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Korisnici WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", KorisnikID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            poruka.Append("<tr>")
                            poruka.Append("<td style=""width:68%;"">")
                            poruka.Append("<b>Broj narudžbe:</b> " & NarudzbaID & "<br/>")
                            poruka.Append("<b>Datum narudžbe:</b> " & Format(DateAndTime.Now(), "dd.MM.yyyy.") & "<br/>")
                            poruka.Append("<b>Način plaćanja:</b> " & NacinPlacanja & "<br/>")
                            poruka.Append("<b>Način dostave:</b> " & NacinDostave)
                            poruka.Append("</td>")
                            poruka.Append("<td style=""vertical-align:top; text-align:left;"">")
                            poruka.Append("<b>Kupac:</b><br/> ")
                            poruka.Append(citac("ImePrezime") & "<br/>")
                            poruka.Append(citac("Adresa") & "<br/>")
                            poruka.Append(citac("ZIP") & " - " & citac("Grad") & " <br/>")
                            poruka.Append("Tel:  " & citac("Telefon") & " <br/>")
                            poruka.Append("e-mail: " & citac("Email"))
                            poruka.Append("</td>")
                            poruka.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        poruka.Append("</table><br/>")
        'Artikli
        poruka.Append("<table cellspacing=""0"" cellpadding=""0"" style=""border:solid 1px #dfdfdf; padding:3px; width: 100%; font-family:Arial;"">")
        poruka.Append("<tr>")
        poruka.Append("<td style=""border-bottom:dotted 1px #dfdfdf; background-color:#CCCCCC;width: 100px; font-weight:bold;text-align:center;"">Šifra</td>")
        poruka.Append("<td style=""border-bottom:dotted 1px #dfdfdf; background-color:#CCCCCC; font-weight:bold;"">Naziv artikla</td>")
        poruka.Append("<td style=""border-bottom:dotted 1px #dfdfdf; background-color:#CCCCCC;width: 50px; font-weight:bold;text-align:center;"">Kolicina</td>")
        poruka.Append("<td style=""border-bottom:dotted 1px #dfdfdf; background-color:#CCCCCC;width: 50px; font-weight:bold;text-align:center;"">Cijena</td>")
        poruka.Append("<td style=""border-bottom:dotted 1px #dfdfdf; background-color:#CCCCCC;width: 70px; font-weight:bold;text-align:center;"">Iznos</td>")
        poruka.Append("</tr>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "OdaberiStavkeNarudzbe"
                komanda.Parameters.AddWithValue("@NarudzbaID", NarudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            poruka.Append("<tr>")
                            poruka.AppendFormat("<td style=""text-align:center; border-bottom:dotted 1px #dfdfdf; padding: 5px 0px;"">{0}</td>", citac("SifraArtikla"))
                            poruka.AppendFormat("<td style=""text-align:left; border-bottom:dotted 1px #dfdfdf; padding: 5px 0px;"">{0}</td>", citac("NazivArtikla"))
                            poruka.AppendFormat("<td style=""text-align:center; border-bottom:dotted 1px #dfdfdf; padding: 5px 0px;"">{0}</td>", citac("Kolicina"))
                            poruka.AppendFormat("<td style=""text-align:center; border-bottom:dotted 1px #dfdfdf; padding: 5px 0px;"">{0}</td>", Format(citac("Cijena"), "N2"))
                            poruka.AppendFormat("<td style=""text-align:center; border-bottom:dotted 1px #dfdfdf; padding: 5px 0px;"">{0}</td>", Format((citac("Kolicina") * citac("Cijena")), "N2"))
                            poruka.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using

        poruka.Append("<tr>")
        poruka.AppendFormat("<td colspan='3' style='text-align:right;'>Ukupan iznos bez PDV: </td>")
        poruka.AppendFormat("<td colspan='2' style='text-align:right;'>{0} {1}</td>", Format(Komponente.Suma(NarudzbaID) / 1.17, "N2"), Komponente.Postavke("Valuta"))
        poruka.Append("</tr>")

        poruka.Append("<tr>")
        poruka.AppendFormat("<td colspan='3' style='text-align:right;'>Dostava: </td>")
        poruka.AppendFormat("<td colspan='2' style='text-align:right;'>{0} {1}</td>", Format(NaplataDostave, "N2"), Komponente.Postavke("Valuta"))
        poruka.Append("</tr>")

        If NacinPlacanja = "virman" Then
            poruka.Append("<tr>")
            poruka.AppendFormat("<td colspan='3' style='text-align:right;'>Posebni popust: </td>")
            poruka.AppendFormat("<td colspan='2' style='text-align:right;'>-{0} {1}</td>", Komponente.Postavke("PopustZiralno"), "%")
            poruka.Append("</tr>")
        End If

        poruka.Append("<tr>")
        poruka.AppendFormat("<td colspan='3' style='text-align:right;font-weight: bold;padding-top: 10px;font-size: 14px;'>Ukupan iznos za platiti: </td>")
        Dim suma As Decimal = Komponente.Suma(NarudzbaID)
        Dim PopustZiralno As Integer = 0

        Dim sapopustom As Decimal = suma * (100 - PopustZiralno) / 100
        poruka.AppendFormat("<td colspan='2' style='text-align:right;font-weight: bold;padding-top: 10px;font-size: 14px;'>{0} {1}</td>", Format(sapopustom + NaplataDostave, "N2"), Komponente.Postavke("Valuta"))
        poruka.Append("</tr>")

        poruka.Append("</table>")
        If NacinPlacanja = "mikrofin" Then
            poruka.Append("<br/>")
            poruka.Append("Preko Mikrofin mikrokreditne organizacije možete kupiti svu robu iz našeg asortimana do 18 mjesečnih rata (nije uvjet da morate biti zaposleni).")

        End If
        Using konekcijaKomentar As New SqlConnection(Komponente.SQLKonekcija())
            konekcijaKomentar.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcijaKomentar
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Narudzbe WHERE ID=" & NarudzbaID
                Using citacNar As SqlDataReader = komanda.ExecuteReader
                    While citacNar.Read
                        poruka.AppendFormat("<br/>Napomena: ""{0}""", citacNar("Napomena"))
                    End While
                End Using
            End Using
        End Using




        ' SMTP konfiguracija
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim srv As New SmtpClient(Komponente.Postavke("SmtpServer"), Komponente.Postavke("SmtpPort"))
        srv.EnableSsl = True
        srv.UseDefaultCredentials = False
        srv.Credentials = New System.Net.NetworkCredential(Komponente.Postavke("SmtpUser"), Komponente.Postavke("SmtpLozinka"))
        Dim mailFrom As New MailAddress(Komponente.Postavke("SmtpUser"), Komponente.Postavke("Tvrtka"))

        Using konekcija As New SqlConnection(Komponente.SQLKonekcija())
            konekcija.Open()
            Using komanda As New SqlCommand("SELECT Mail FROM AdminMail WHERE Aktivan='1'", konekcija)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            Dim MailPrimatelja As String = citac("Mail").ToString().Trim()
                            Dim mailToFirma As New MailAddress(MailPrimatelja)
                            Dim mlFirma As New MailMessage(mailFrom, mailToFirma)

                            mlFirma.SubjectEncoding = Encoding.UTF8
                            mlFirma.Priority = MailPriority.Normal
                            mlFirma.BodyEncoding = Encoding.UTF8
                            mlFirma.IsBodyHtml = True
                            mlFirma.Subject = "Nova narudžba RescueEquip WebShop " & NarudzbaID
                            mlFirma.Body = String.Format(Dokument("/Ajax/predlozak2.htm"), poruka.ToString)
                            mlFirma.ReplyToList.Add(New MailAddress(n.Mail))

                            Try
                                srv.Send(mlFirma)
                            Catch ex As SmtpException
                                HttpContext.Current.Response.Write("SMTP greška: " & ex.ToString())
                            Finally
                                mlFirma.Dispose()
                            End Try
                        End While
                    End If
                End Using
            End Using
            konekcija.Close()
        End Using

        If n.Mail <> "" Then
            Dim mailKupac As New MailAddress(n.Mail)
            Dim mlKupac As New MailMessage(mailFrom, mailKupac)
            mlKupac.ReplyToList.Add(Komponente.Postavke("MailOdgovor"))
            mlKupac.SubjectEncoding = Encoding.UTF8
            mlKupac.Priority = MailPriority.Normal
            mlKupac.BodyEncoding = Encoding.UTF8
            mlKupac.IsBodyHtml = True
            mlKupac.Subject = Server.HtmlEncode("Vaša narudžba RescueEquip WebShop")


            mlKupac.Body = String.Format(Dokument("/Ajax/predlozak2.htm"), poruka.ToString)
            srv.Send(mlKupac)
            mlKupac.Dispose()
        End If
    End Sub

    Private Function NacinPlacanjaNarudzbe(narudzbaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.SQLKonekcija()

        On Error Resume Next

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT NacinPlacanja FROM Narudzbe WHERE ID=@NarudzbaID"
                komanda.Parameters.AddWithValue("@NarudzbaID", narudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("NacinPlacanja"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

    Private Function NacinDostaveNarudzbe(narudzbaID As Integer) As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.SQLKonekcija()

        On Error Resume Next

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT NacinDostave FROM Narudzbe WHERE ID=@NarudzbaID"
                komanda.Parameters.AddWithValue("@NarudzbaID", narudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("NacinDostave"))
                        End While
                    End If
                End Using
            End Using
        End Using

        Return html.ToString()
    End Function

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

    Private Sub PoslajiObavjestKreiranjaRacuna(KorisnikID As Integer)
        Dim poruka As New StringBuilder
        Dim putanja As String = Komponente.SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Korisnici WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", KorisnikID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            poruka.Append("<hr/>")
                            poruka.AppendFormat("{0},", citac("ImePrezime"))
                            poruka.Append("<br/><br/>")
                            poruka.Append("na WebeShop-u RescueEquip je kreiran korisnički račun")
                            poruka.Append("<br/><br/>")
                            poruka.Append("Pristup korisničkom sučelju RescueEquip:<br/>")
                            poruka.Append("<br/><br/>")
                            poruka.AppendFormat("Vaše korisničko ime: {0}", citac("Email"))
                            poruka.Append("<br/>")
                            poruka.Append("Vaša lozinka: ******** (poznata je samo vama)<br/>")
                            poruka.Append("<br/><br/>")
                            poruka.Append("<strong>Vaš RescueEquip TEAM</strong>")
                            poruka.Append("<hr/><br/><br/>")

                            Dim srv As New SmtpClient
                            Dim mailFrom As New MailAddress(ConfigurationManager.AppSettings("mailFrom"), "RescueEquip WebShop")
                            Dim mailToFirma As New MailAddress(citac("Email"))
                            Dim mlFirma As New MailMessage(mailFrom, mailToFirma)
                            mlFirma.SubjectEncoding = Encoding.UTF8
                            mlFirma.Priority = MailPriority.Normal
                            mlFirma.BodyEncoding = Encoding.UTF8
                            mlFirma.IsBodyHtml = True
                            mlFirma.Subject = HttpContext.Current.Server.HtmlEncode("Kreiran račun RescueEquip WebShop-u")
                            mlFirma.Body = String.Format(Dokument("/Ajax/predlozak2.htm"), poruka.ToString)
                            srv.Send(mlFirma)
                            mlFirma.Dispose()
                            UnesiNewsletter(citac("ImePrezime"), citac("Email"))
                        End While
                    End If
                End Using
            End Using
        End Using

    End Sub

    Private Sub UnesiNewsletter(ImePrezime As String, Email As String)
        Dim putanja As String = Komponente.SQLKonekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.Parameters.AddWithValue("@ImePrezime", ImePrezime)
                komanda.Parameters.AddWithValue("@Email", Email)
                komanda.CommandText = "UnesiNewsletter"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class