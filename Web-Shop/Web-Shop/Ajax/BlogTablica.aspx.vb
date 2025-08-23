Public Class BlogTablica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
        End If

        '  Dim tablica As String = Web_Shop.Komponente.Blog(stranica)
        ' Response.Write(tablica)
    End Sub

End Class