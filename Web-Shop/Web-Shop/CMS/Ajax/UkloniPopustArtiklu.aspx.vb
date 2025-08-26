Imports System.Data.SqlClient

Public Class UkloniPopustArtiklu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()

        Dim popustid As Integer = HttpContext.Current.Request.Params("popustid")
        Dim artikalid As Integer = HttpContext.Current.Request.Params("artikalid")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", popustid)
                komanda.CommandText = "DELETE ArtikliPopusti WHERE ID=@ID"
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Dim podaci As String = CMS.boxPopusti(artikalid)
        HttpContext.Current.Response.Write(podaci)
    End Sub

End Class