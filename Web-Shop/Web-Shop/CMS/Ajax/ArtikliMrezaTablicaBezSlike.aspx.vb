Public Class ArtikliMrezaTablicaBezSlike
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1
        Dim kategorija As String = 0
        Dim naziv As String = 0

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
            kategorija = Convert.ToInt32(Request.QueryString("kategorija"))
            naziv = Request.QueryString("naziv")
        End If

        'slovo = Request.QueryString("slovo")

        Dim tablica As String = igre_ba.CMS.ArtikliMrezaTablicaBezSlike(stranica, kategorija, naziv)
        Response.Write(tablica)
    End Sub

End Class