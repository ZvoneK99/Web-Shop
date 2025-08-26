Public Class ArtikliMrezaTablicaPodKategorije
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim stranica As Integer = 1
        Dim PodKategorijaID As Integer

        If Request.QueryString("stranica") IsNot Nothing Then
            stranica = Convert.ToInt32(Request.QueryString("stranica"))
        End If

        PodKategorijaID = Request.QueryString("PodKategorijaID")

        Dim tablica As String = Web_Shop.CMS.MrezaArtikliPodKategorije(stranica, PodKategorijaID)
        Response.Write(tablica)
    End Sub

End Class