Imports System.Data.SqlClient

Public Class PodKategorijaStatus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("id"))
        Dim status As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("status"))
        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", id)
                komanda.Parameters.AddWithValue("@Status", status)
                komanda.CommandText = "UPDATE ArtikliGrupe SET Aktivno=@Status WHERE ID=@ID;" ' UPDATE Artikli SET Aktivno=@Status WHERE PodKategorijaID=@ID AND @Status='0'"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class