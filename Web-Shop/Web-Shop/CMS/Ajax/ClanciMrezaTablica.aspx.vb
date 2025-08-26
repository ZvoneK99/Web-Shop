Public Class ClanciMrezaTablica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
        End If

        'slovo = Request.QueryString("slovo")

        Dim tablica As String = Web_Shop.CMS.ClanciMrezaTablica(stranica)
        Response.Write(tablica)
    End Sub

End Class