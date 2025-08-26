Imports System.Data.SqlClient

Public Class ZadanaSlikaClanka
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim SlikaID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("SlikaID"))
        Dim ClanakID As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("ClanakID"))
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE NovostiSlike SET Zadana='0' WHERE NovostID=@ClanakID; UPDATE NovostiSlike SET Zadana='1' WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", SlikaID)
                komanda.Parameters.AddWithValue("@ClanakID", ClanakID)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/Clanak.aspx?id=" & ClanakID)
    End Sub

End Class