Imports System.Data.SqlClient

Public Class UpdatePostavke
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim hidId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidId"))
        Dim txtVrijednost As String = HttpContext.Current.Request.Params("txtVrijednost")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Postavke SET" _
                                    & " Vrijednost=@Vrijednost" _
                                    & " WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", hidId)
                komanda.Parameters.AddWithValue("@Vrijednost", txtVrijednost)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/OpcePostavke.aspx")

    End Sub

End Class