Public Class ArtikliGrupe2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1
        Dim kategorija As Integer = 0

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
        End If

        kategorija = Convert.ToInt32(Request.QueryString("kategorija"))

        Dim tablica As String = Web_Shop.Komponente.ArtikliGrupee(kategorija)
        Response.Write(tablica)
    End Sub

End Class