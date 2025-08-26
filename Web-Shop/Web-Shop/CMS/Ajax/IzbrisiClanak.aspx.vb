Imports System.Data.SqlClient

Public Class IzbrisiClanak
    Inherits System.Web.UI.Page

    Private Property FileDeleted As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim ClanakID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("id"))
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", ClanakID)
                komanda.CommandText = "DELETE Novosti WHERE ID=@ID"
                komanda.ExecuteNonQuery()
                IzbrisiSlike(ClanakID)
            End Using
        End Using


        'Dim file As String = HttpContext.Current.Request.Params("file")
        'Try
        '    IO.File.Delete(Server.MapPath("~/Datoteke/Slider/" & file))
        '    'IO.File.Delete(Server.MapPath("~/Datoteke/Slider/Thumb/" & file))
        '    FileDeleted = True
        '    IzbrisiSlikuuBazi(ClanakID)
        'Catch ex As Exception
        '    FileDeleted = False
        '    Exit Sub
        'End Try
        Response.Redirect("/CMS/Pregled.aspx")
    End Sub

    Private Sub IzbrisiSlike(ClanakID As Integer)
        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ClanakID", ClanakID)
                komanda.CommandText = "DELETE NovostiSlike WHERE NovostID=@ClanakID"
                komanda.ExecuteNonQuery()
                'IzbrisiSlikeFizicki(ClanakID)
            End Using
        End Using
    End Sub

End Class