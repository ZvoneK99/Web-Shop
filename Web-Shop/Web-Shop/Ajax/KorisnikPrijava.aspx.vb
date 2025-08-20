Imports System.Data.SqlClient

Public Class KorisnikPrijava
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim username As String = HttpContext.Current.Request.Params("prijavaKorisnickoIme")
        Dim pwd As String = HttpContext.Current.Request.Params("prijavaLozinka")
        Dim urlnext As String = HttpContext.Current.Request.Params("urlnext")
        Dim strConn As String
        strConn = Komponente.SQLKonekcija()
        Dim Conn As New SqlConnection(strConn)
        Conn.Open()
        Dim sqlUserName As String
        'sqlUserName = "SELECT * FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1' AND Logiran='0')"
        sqlUserName = "SELECT COUNT(*) FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1' AND Iskljucen='0')"
        Dim com As New SqlCommand(sqlUserName, Conn)
        com.Parameters.AddWithValue("@Email", username)
        com.Parameters.AddWithValue("@Lozinka", pwd)
        Dim CurrentName As String
        CurrentName = CStr(com.ExecuteScalar)
        If CurrentName <> "" Then
            Session("ValjanUser") = True
            'ZapisiCookie(username)
            If IsNothing(Request.QueryString("putanja")) = False Then
                Response.Redirect(Request.QueryString("putanja"))
            Else
                Response.Redirect("/")
            End If
            'ZapisiLog(username, "***", "uspješno logiran", "login")
            'Response.Redirect("/")
        Else
            Session("ValjanUser") = False
            'ZapisiLog.ZapisiLog("0", username, pwd, "neuspješno logiran", "login")
            Response.Redirect("/login?msg-passw=pogreska")
            Response.Redirect("/login?s=false")
        End If
        Conn.Close()
    End Sub

    'Private Sub ZapisiCookie(username As String)
    '    Dim putanja As String = Komponente.SQLKonekcija()
    '    Using konekcija As New SqlConnection(putanja)
    '        konekcija.Open()
    '        Using komanda As New SqlCommand()
    '            komanda.Connection = konekcija
    '            komanda.CommandType = CommandType.Text
    '            komanda.CommandText = "SELECT * FROM Korisnici WHERE Email=@Email;"
    '            komanda.Parameters.AddWithValue("@Email", username)
    '            Using citac As SqlDataReader = komanda.ExecuteReader()
    '                If citac IsNot Nothing Then
    '                    While citac.Read()
    '                        Session("Tvrtka") = citac("TvrtkaID")
    '                        Response.Cookies("logiraniValjanUser").Value = "ValjanUser"
    '                        Response.Cookies("logiraniKorisnikID").Value = Komponente.Encrypt(citac("ID"))
    '                        Response.Cookies("logiraniKorisnik").Value = Komponente.Encrypt(citac("Email"))
    '                        Response.Cookies("logiraniKorisnik").Value = Komponente.Encrypt(citac("AdminLevel"))
    '                        Response.Cookies("logiraniKorisnikTvrtka").Value = Komponente.Encrypt(citac("TvrtkaID"))
    '                        ZapisiLog.ZapisiLog(citac("TvrtkaID"), username, "***", "uspješno logiran", "login")
    '                        ZapisiZadnjiLogin(citac("ID"))
    '                    End While
    '                End If
    '            End Using
    '        End Using
    '    End Using
    'End Sub

    Private Sub ZapisiZadnjiLogin(KorisnikID As Integer)
        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Korisnici SET Logiran='1' WHERE ID=@KorisnikID;"
                komanda.Parameters.AddWithValue("@KorisnikID", KorisnikID)
                komanda.Parameters.AddWithValue("@Datum", DateTime.Now())
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class