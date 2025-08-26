Imports System.Data.SqlClient

Public Class IzbrisiVezaniArtikalBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            IzbrisiVezaniArtikalBulk()
        End If
    End Sub

    Private Sub IzbrisiVezaniArtikalBulk()
        Dim idStr As String = Request.QueryString("id")
        If String.IsNullOrEmpty(idStr) Then Exit Sub

        Dim VezaniArtikalID As Integer
        If Not Integer.TryParse(idStr, VezaniArtikalID) Then Exit Sub

        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand("DELETE FROM ArtikliVezaniBulk WHERE ID=@ID", konekcija)
                komanda.Parameters.AddWithValue("@ID", VezaniArtikalID)
                komanda.ExecuteNonQuery()
            End Using
        End Using
        Response.Redirect(Request.UrlReferrer.ToString())
    End Sub
End Class