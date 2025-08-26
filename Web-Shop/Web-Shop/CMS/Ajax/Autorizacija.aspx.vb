Imports System.Data.SqlClient

Public Class CmsAutorizacija
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim username As String = HttpContext.Current.Request.Params("Email")
        Dim pwd As String = HttpContext.Current.Request.Params("Password")
        Dim strConn As String
        strConn = Komponente.conekcija()
        Dim Conn As New SqlConnection(strConn)
        Conn.Open()
        Dim sqlUserName As String
        'sqlUserName = "SELECT * FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1' AND Logiran='0')"
        sqlUserName = "SELECT * FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1' AND AdminLevel<'5')"
        Dim com As New SqlCommand(sqlUserName, Conn)
        com.Parameters.AddWithValue("@Email", username)
        com.Parameters.AddWithValue("@Lozinka", Komponente.Encrypt(pwd))
        Dim CurrentName As String
        CurrentName = CStr(com.ExecuteScalar)
        If CurrentName <> "" Then
            Session("ValjanUser") = True
            ZapisiCookie(username)
            ZapisiLog.ZapisiLog("0", username, "***", "neuspješno logiran", "login")
            If IsNothing(Request.QueryString("putanja")) = False Then
                Response.Redirect(Request.QueryString("putanja"))
            Else
                Response.Redirect("/CMS/Dashboard.aspx")
            End If
        Else
            Session("ValjanUser") = False
            ZapisiLog.ZapisiLog("0", username, pwd, "neuspješno logiran", "login")
            Response.Redirect("/CMS/Default.aspx?s=false")
        End If
        Conn.Close()
    End Sub

    Private Sub ZapisiCookie(username As String)
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT * FROM Korisnici WHERE Email=@Email;"
                komanda.Parameters.AddWithValue("@Email", username)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            Session("Tvrtka") = citac("TvrtkaID")
                            Response.Cookies("logiraniValjanUser").Value = "ValjanUser"
                            Response.Cookies("logiraniKorisnikID").Value = Komponente.Encrypt(citac("ID"))
                            Response.Cookies("logiraniKorisnik").Value = Komponente.Encrypt(citac("Email"))
                            Response.Cookies("logiraniKorisnikNivo").Value = Komponente.Encrypt(citac("AdminLevel"))
                            Response.Cookies("logiraniKorisnikTvrtka").Value = Komponente.Encrypt(citac("TvrtkaID"))
                            ZapisiLog.ZapisiLog(citac("TvrtkaID"), username, "***", "uspješno logiran", "login")
                            ZapisiZadnjiLogin(citac("ID"))
                        End While
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub ZapisiZadnjiLogin(KorisnikID As Integer)
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Korisnici SET Logiran='1', ZadnjaAktivnost=@Datum, BrojAktivnosti=BrojAktivnosti+1, ZadnjiLogin=@Datum WHERE ID=@KorisnikID;"
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID)
                komanda.Parameters.AddWithValue("@Datum", DateTime.Now())
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class