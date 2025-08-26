Imports System.Data.SqlClient

Public Class PridruziSlikuArtiklu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Response.Clear()
        Dim slika As String = Request.Params("slika")
        Dim artikalid As Integer = Request.Params("artikalid")
        'MsgBox(artikalid)

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE ArtikliSlike SET Zadana='0' WHERE ArtikalID=@ArtikalId; INSERT INTO ArtikliSlike (ArtikalID, Datoteka, Zadana) VALUES (@ArtikalId, @Slika, '1')"
                komanda.Parameters.AddWithValue("@ArtikalId", artikalid)
                komanda.Parameters.AddWithValue("@Slika", slika)
                komanda.ExecuteNonQuery()
            End Using
        End Using

    End Sub

End Class