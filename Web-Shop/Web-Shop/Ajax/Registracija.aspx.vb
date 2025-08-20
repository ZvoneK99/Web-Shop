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