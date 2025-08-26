Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports System.Net

Public Class ResetLozinke
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim Email As String = HttpContext.Current.Request.Params("prijavaKorisnickoIme")

        KreirajNovuLozinku(Email)

        Dim poruka As New StringBuilder
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Korisnici WHERE Email=@Email"
                komanda.Parameters.AddWithValue("@Email", Email)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            poruka.Append("<hr/>")
                            poruka.AppendFormat("{0},", citac("ImePrezime"))
                            poruka.Append("<br/>")
                            poruka.Append("Vaši podaci za pristup WebShop-u RescueEquip se nalaze ispod:")
                            poruka.Append("<br/><br/>")
                            poruka.AppendFormat("Vaše korisničko ime: {0}", citac("Email"))
                            poruka.Append("<br/>")
                            poruka.AppendFormat("Vaša lozinka: {0}", citac("Lozinka"))
                            poruka.Append("<br/><br/>")
                            poruka.Append("<strong>Vaš RESCUEEQUPI TEAM</strong>")
                            poruka.Append("<hr/><br/><br/>")



                            ' SMTP konfiguracija
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                            Dim srv As New SmtpClient(Komponente.Postavke("SmtpServer"), Komponente.Postavke("SmtpPort"))
                            srv.EnableSsl = True
                            srv.UseDefaultCredentials = False
                            srv.Credentials = New System.Net.NetworkCredential(Komponente.Postavke("SmtpUser"), Komponente.Postavke("SmtpLozinka"))
                            Dim mailFrom As New MailAddress(Komponente.Postavke("SmtpUser"), Komponente.Postavke("Tvrtka"))
                            Dim MailPrimatelja As String = citac("Email").ToString().Trim()
                            Dim mailToFirma As New MailAddress(MailPrimatelja)
                            Dim mlFirma As New MailMessage(mailFrom, mailToFirma)

                            mlFirma.SubjectEncoding = Encoding.UTF8
                            mlFirma.Priority = MailPriority.Normal
                            mlFirma.BodyEncoding = Encoding.UTF8
                            mlFirma.IsBodyHtml = True
                            mlFirma.Subject = "Nova lozinka"
                            mlFirma.Body = String.Format(Dokument("/Ajax/predlozak2.htm"), poruka.ToString)
                            mlFirma.ReplyToList.Add(New MailAddress(Komponente.Postavke("MailOdgovor")))

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
        Response.Redirect("/izgubljena-lozinka?msg=poslano")

        'Response.Redirect("/login")

    End Sub

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

    Private Sub KreirajNovuLozinku(Email As String)
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim Lozinka As String = GenerirajPassword()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Korisnici SET Lozinka=@Lozinka WHERE Email=@Email"
                komanda.Parameters.AddWithValue("@Email", Email)
                komanda.Parameters.AddWithValue("@Lozinka", Lozinka)
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function GenerirajPassword() As String
        Return "RescueEquip" & DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function


End Class