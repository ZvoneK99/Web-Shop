Imports System.Data.SqlClient
Public Class IzbrisiKatalog
    Inherits System.Web.UI.Page

    Private Property FileDeleted As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SlikaID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("id"))
        Dim file As String = HttpContext.Current.Request.Params("file")
        Try
            IO.File.Delete(Server.MapPath("~/Datoteke/Katalozi/" & file))
            'IO.File.Delete(Server.MapPath("~/Datoteke/Slider/Thumb/" & file))
            FileDeleted = True
            IzbrisiSlikuuBazi(SlikaID)
        Catch ex As Exception
            FileDeleted = False
            Exit Sub
        End Try
        Response.Redirect("/CMS/KataloziCms.aspx")
    End Sub

    Private Sub IzbrisiSlikuuBazi(SlikaID As Integer)
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", SlikaID)
                komanda.CommandText = "DELETE Katalozi WHERE ID=@ID"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class