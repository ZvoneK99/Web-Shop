Imports System.Data.SqlClient

Public Class ZadanaSlika
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim SlikaID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("SlikaID"))
        Dim ArtikalID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("ArtikalID"))
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE ArtikliSlike SET Zadana='0' WHERE ArtikalID=@ArtikalID; UPDATE ArtikliSlike SET Zadana='1' WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", SlikaID)
                komanda.Parameters.AddWithValue("@ArtikalID", ArtikalID)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/Artikal.aspx?id=" & ArtikalID)
    End Sub

End Class