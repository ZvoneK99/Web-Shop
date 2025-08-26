Imports System.Data.SqlClient

Public Class KategorijaStatusDigitalis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("id"))
        Dim status As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("status"))
        Dim grpuni As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("grpuni"))
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", id)
                komanda.Parameters.AddWithValue("@Status", status)
                komanda.Parameters.AddWithValue("@grpuni", grpuni)
                komanda.Parameters.AddWithValue("@SkladisteID", 5)
                If status = 1 Then
                    komanda.CommandText = "UPDATE ArtikliGrupeDigitalis SET Aktivno=@Status WHERE ID=@ID; UPDATE Artikli SET Aktivno='1' WHERE GrupaID=@grpuni AND SkladisteID=@SkladisteID;"
                Else
                    komanda.CommandText = "UPDATE ArtikliGrupeDigitalis SET Aktivno=@Status WHERE ID=@ID; UPDATE Artikli SET Aktivno='0' WHERE GrupaID=@grpuni AND SkladisteID=@SkladisteID;"
                End If
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class