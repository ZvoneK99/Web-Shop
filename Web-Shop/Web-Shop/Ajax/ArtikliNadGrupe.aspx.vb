Public Class ArtikliNadGrupe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1
        Dim kategorija As Integer = 0
        Dim raspored As String

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
        End If

        kategorija = Convert.ToInt32(Request.QueryString("kategorija"))
        raspored = Request.QueryString("raspored")

        Dim tablica As String = Web_Shop.Komponente.ArtikliNadGrupe(stranica, kategorija, raspored)
        Response.Write(tablica)
    End Sub

End Class