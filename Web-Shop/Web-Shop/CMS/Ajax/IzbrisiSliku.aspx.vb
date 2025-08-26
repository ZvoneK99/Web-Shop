Imports System.Data.SqlClient

Public Class IzbrisiSliku
    Inherits System.Web.UI.Page

    Private FileDeleted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim putanja As String = Komponente.SQLKonekcija()
        Dim SlikaID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("SlikaID"))
        Dim ArtikalID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("ArtikalID"))
        Dim file As String = HttpContext.Current.Request.Params("file")
        IzbrisiSlikuuBazi(SlikaID)
        Response.Redirect("/CMS/Artikal.aspx?id=" & ArtikalID)
        'Try
        '    IO.File.Delete(Server.MapPath("~/Datoteke/Artikli/" & file))
        '    IO.File.Delete(Server.MapPath("~/Datoteke/Artikli/Thumb/" & file))
        '    FileDeleted = True
        '    IzbrisiSlikuuBazi(SlikaID)
        '    Response.Redirect("/CMS/Artikal.aspx?id=" & ArtikalID)
        'Catch ex As Exception
        '    FileDeleted = False
        '    Exit Sub
        'End Try
    End Sub

    Private Sub IzbrisiSlikuuBazi(SlikaID As Integer)
        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", SlikaID)
                komanda.CommandText = "DELETE ArtikliSlike WHERE ID=@ID"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class