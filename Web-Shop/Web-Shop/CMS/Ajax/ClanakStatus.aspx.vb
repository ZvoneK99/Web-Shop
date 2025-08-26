Imports System.Data.SqlClient

Public Class ClanakStatus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("id"))
        Dim status As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("status"))
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", id)
                komanda.Parameters.AddWithValue("@Status", status)
                komanda.CommandText = "UPDATE Novosti SET Aktivno=@Status WHERE ID=@ID"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class