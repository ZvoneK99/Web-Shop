Imports System.Data.SqlClient

Public Class CmsAutorizacija
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim username As String = HttpContext.Current.Request.Params("Email")
        Dim pwd As String = HttpContext.Current.Request.Params("Password")
        Dim strConn As String
        strConn = Komponente.SQLKonekcija()
        Dim Conn As New SqlConnection(strConn)
        Conn.Open()
        Dim sqlUserName As String
        'sqlUserName = "SELECT * FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1' AND Logiran='0')"
        sqlUserName = "SELECT * FROM Korisnici WHERE (Email=@Email AND Lozinka=@Lozinka AND Aktivan='1')"   ' AND AdminLevel<'5'
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
                Response.Redirect("/CMS/Dashboard.aspx")
            End If
        Else
            Session("ValjanUser") = False
            Response.Redirect("/CMS/Default.aspx?s=false")
        End If
        Conn.Close()
    End Sub



End Class