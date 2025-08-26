Public Class ArtikliMrezaTablicaIzdvojeni
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1
        Dim naziv As String = 0

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
            naziv = Request.QueryString("naziv")
        End If

        'slovo = Request.QueryString("slovo")

        Dim tablica As String = Web_Shop.CMS.ArtikliMrezaTablicaIzdvojeni(stranica, naziv)
        Response.Write(tablica)
    End Sub

End Class