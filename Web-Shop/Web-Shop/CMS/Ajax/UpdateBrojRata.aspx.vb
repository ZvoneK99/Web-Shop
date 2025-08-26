Imports System.Data.SqlClient

Public Class UpdateBrojRata
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim hidId As Integer = HttpContext.Current.Request.Params("hidId")
        Dim txtMarza As Decimal = HttpContext.Current.Request.Params("txtMarza")

        'MsgBox(hidId & ":" & txtMarza)

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE BrojRata SET" _
                                    & " Marza=@Marza" _
                                    & " WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", hidId)
                komanda.Parameters.AddWithValue("@Marza", txtMarza)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/OpcePostavke.aspx")

    End Sub

End Class