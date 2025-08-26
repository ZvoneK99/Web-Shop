Imports System.Data.SqlClient

Public Class UpdateStatika
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()
        Dim hidId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidId"))
        Dim txtNaslov As String = HttpContext.Current.Request.Params("txtNaslov")
        Dim txtVrijednost As String = HttpContext.Current.Request.Params("txtVrijednost")
        Dim txtVrijednostBulk As String = HttpContext.Current.Request.Params("txtVrijednostBulk")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Statika SET" _
                                    & " Naslov=@Naslov," _
                                    & " Vrijednost=@Vrijednost," _
                                    & " VrijednostBulk=@VrijednostBulk" _
                                    & " WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", hidId)
                komanda.Parameters.AddWithValue("@Naslov", txtNaslov)
                komanda.Parameters.AddWithValue("@Vrijednost", txtVrijednost)
                komanda.Parameters.AddWithValue("@VrijednostBulk", txtVrijednostBulk)

                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/Statika.aspx?id=" & hidId)

    End Sub

End Class