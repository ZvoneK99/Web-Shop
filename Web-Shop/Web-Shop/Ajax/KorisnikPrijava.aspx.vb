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
        sqlUserName = "SELECT COUNT(*) FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1' AND Iskljucen='0')"
        Dim com As New SqlCommand(sqlUserName, Conn)
        com.Parameters.AddWithValue("@Email", username)
        com.Parameters.AddWithValue("@Lozinka", pwd)
        Dim CurrentName As String
        CurrentName = CStr(com.ExecuteScalar)
        If CurrentName <> "" Then
            Session("ValjanUser") = True
            If IsNothing(Request.QueryString("putanja")) = False Then
                Response.Redirect(Request.QueryString("putanja"))
            Else
                Response.Redirect("/")
            End If
        Else
            Session("ValjanUser") = False
            Response.Redirect("/login?msg-passw=pogreska")
            Response.Redirect("/login?s=false")
        End If
        Conn.Close()
    End Sub


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