Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail
Imports System.IO
Imports System.Text

Public Class PosaljiMail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Provjeri je li forma poslana
        If Request.HttpMethod = "POST" Then
            Try
                ' Dohvati podatke iz forme
                Dim senderName As String = Request.Form("ImePrezime")
                Dim senderEmail As String = Request.Form("Email")
                Dim senderPhone As String = Request.Form("Telefon")
                Dim senderMessage As String = Request.Form("Poruka")

                If String.IsNullOrWhiteSpace(senderName) OrElse
                   String.IsNullOrWhiteSpace(senderEmail) OrElse
                   String.IsNullOrWhiteSpace(senderMessage) Then

                    Response.Write("<script>alert('Molimo ispunite sva obavezna polja.');</script>")
                    Return
                End If

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
                                    mlFirma.Subject = "Nova poruka s kontakt forme"
                                    mlFirma.Body = $"Ime: {senderName}<br>Email: {senderEmail}<br>Telefon: {senderPhone}<br><br>Poruka:<br>{senderMessage}"
                                    mlFirma.ReplyToList.Add(New MailAddress(senderEmail))

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

                ' Obavijest korisniku

                Response.Redirect("/kontakt?msg=poslano")
                'Response.Write("<script>alert('Poruka je uspješno poslana!');</script>")
                'Response.Redirect("/")
            Catch ex As Exception
                Response.Write("Greška: " & ex.Message.ToString())
            End Try
        End If
    End Sub

End Class
