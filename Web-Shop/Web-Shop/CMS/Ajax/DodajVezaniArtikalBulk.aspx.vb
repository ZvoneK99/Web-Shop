Imports System.Data.SqlClient

Public Class DodajVezaniArtikalBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DodajVezani()
    End Sub

    Private Sub DodajVezani()
        Dim artikalIDStr As String = Request.QueryString("id")
        Dim vezaniIDStr As String = Request.QueryString("vezani")

        Dim artikalID As Integer
        Dim vezaniID As Integer

        If Not Integer.TryParse(artikalIDStr, artikalID) Then Exit Sub
        If Not Integer.TryParse(vezaniIDStr, vezaniID) Then Exit Sub

        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand("INSERT INTO ArtikliVezaniBulk (ArtikalID, VezaniArtikalID) VALUES (@ArtikalID, @VezaniID)", konekcija)
                komanda.Parameters.AddWithValue("@ArtikalID", artikalID)
                komanda.Parameters.AddWithValue("@VezaniID", vezaniID)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        If Request.UrlReferrer IsNot Nothing Then
            Response.Redirect(Request.UrlReferrer.ToString())
        End If
    End Sub
End Class
