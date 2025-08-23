Imports System.Data.SqlClient

Public Class ZapisiLog
    Inherits System.Web.UI.Page

    Public Shared Sub ZapisiLog(tvrtkaID As String, username As String, pwd As String, opis As String, vrsta As String)
        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "INSERT Log (Datum, TvrtkaID, Korisnik, Zaporka, Vrsta, Opis) VALUES ('" & Format(DateTime.Now(), "yyyy.MM.dd HH:mm:ss") & "', @TvrtkaID, @username, @pwd, @vrsta, @opis);"
                komanda.Parameters.AddWithValue("@TvrtkaID", tvrtkaID)
                komanda.Parameters.AddWithValue("@username", username)
                komanda.Parameters.AddWithValue("@pwd", pwd)
                komanda.Parameters.AddWithValue("@vrsta", vrsta)
                komanda.Parameters.AddWithValue("@opis", opis)
                komanda.Parameters.AddWithValue("@IP", ipArdesaKlijenta())
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function ipArdesaKlijenta() As String
        Dim html As New StringBuilder
        Dim ipAddress As String
        ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress
        html.Append(ipAddress)
        Return html.ToString()
    End Function

End Class