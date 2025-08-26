Imports System.Data.SqlClient

Public Class IzbrisiSlikuClanka
    Inherits System.Web.UI.Page

    Private FileDeleted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim putanja As String = Komponente.conekcija()
        Dim SlikaID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("SlikaID"))
        Dim ClanakID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("ClanakID"))
        Dim file As String = HttpContext.Current.Request.Params("file")
        'IzbrisiSlikuuBazi(SlikaID)
        'Response.Redirect("/CMS/Clanak.aspx?id=" & ClanakID)
        Try
            IzbrisiSlikuuBazi(SlikaID)
            IO.File.Delete(Server.MapPath("~/Datoteke/Novosti/" & file))
            IO.File.Delete(Server.MapPath("~/Datoteke/Novosti/Thumb/" & file))
            FileDeleted = True
            Response.Redirect("/CMS/Clanak.aspx?id=" & ClanakID)
        Catch ex As Exception
            FileDeleted = False
            Response.Redirect("/CMS/Clanak.aspx?id=" & ClanakID)
            Exit Sub
        End Try
        'Response.Redirect("/CMS/Clanak.aspx?id=" & ClanakID)
    End Sub

    Private Sub IzbrisiSlikuuBazi(SlikaID As Integer)
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", SlikaID)
                komanda.CommandText = "DELETE NovostiSlike WHERE ID=@ID"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class