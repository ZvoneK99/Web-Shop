Imports System.Data.SqlClient

Public Class PromjeniKatalog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()
        Dim opis As String = Request.Params("opis") ' "TL5787.jpg"
        Dim slikaID As Integer = Request.Params("slikaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Katalozi SET" _
                                    & " Opis=@Opis" _
                                    & " WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", slikaID)
                komanda.Parameters.AddWithValue("@Opis", opis)
                'komanda.Parameters.AddWithValue("@StariNaziv", stariNaziv)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/KataloziCms.aspx")
    End Sub

End Class