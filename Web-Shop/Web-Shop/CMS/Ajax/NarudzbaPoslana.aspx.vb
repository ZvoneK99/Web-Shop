Imports System.Data.SqlClient

Public Class NarudzbaPoslana
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
                If status = 1 Then
                    komanda.CommandText = "UPDATE Narudzbe SET Poslano=@Status WHERE ID=@ID"
                Else
                    komanda.CommandText = "UPDATE Narudzbe SET Poslano=@Status, Naplaceno='0' WHERE ID=@ID"
                End If

                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class