Imports System.Data.SqlClient

Public Class PridruziGrupeStarTech
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim unigrupa As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("unigrupa"))
        Dim comgrupa As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("comgrupa"))
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", comgrupa)
                komanda.Parameters.AddWithValue("@GrupaUniverzalID", unigrupa)
                komanda.CommandText = "UPDATE ArtikliGrupeStarTech SET GrupaUniverzalID=@GrupaUniverzalID WHERE ID=@ID;"
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class