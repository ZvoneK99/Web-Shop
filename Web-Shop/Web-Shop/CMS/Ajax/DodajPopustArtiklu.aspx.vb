Imports System.Data.SqlClient

Public Class DodajPopustArtiklu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()

        Dim txtKolicina As String = HttpContext.Current.Request.Params("kolicina")
        Dim txtPopust As Decimal = HttpContext.Current.Request.Params("popust")
        Dim artikalid As Integer = HttpContext.Current.Request.Params("artikalid")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@Kolicina", txtKolicina)
                komanda.Parameters.AddWithValue("@Popust", txtPopust)
                komanda.Parameters.AddWithValue("@ArtikalID", artikalid)
                komanda.CommandText = "INSERT INTO ArtikliPopusti (ArtikalID, Kolicina, Popust) VALUES (@ArtikalID, @Kolicina, @Popust)"
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Dim podaci As String = CMS.boxPopusti(artikalid)
        HttpContext.Current.Response.Write(podaci)
    End Sub

End Class