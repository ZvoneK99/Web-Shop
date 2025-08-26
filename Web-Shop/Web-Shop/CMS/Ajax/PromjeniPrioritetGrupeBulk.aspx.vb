Imports System.Data.SqlClient

Public Class PromjeniPrioritetGrupeBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("id"))
        Dim prioritet As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("prioritet"))
        Dim nadgrupa As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("nadgrupa"))
        Dim vrijednost As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("vrijednost"))
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ID", id)
                komanda.Parameters.AddWithValue("@Vrijednost", vrijednost)
                komanda.Parameters.AddWithValue("@Prioritet", prioritet)
                komanda.Parameters.AddWithValue("@PrioritetNovi", prioritet + vrijednost)
                komanda.Parameters.AddWithValue("@PrioritetStari", prioritet - vrijednost)
                komanda.Parameters.AddWithValue("@NadgrupaID", nadgrupa)
                'komanda.Parameters.AddWithValue("@Vrijednost", vrijednost)
                If prioritet + vrijednost > 0 Then
                    komanda.CommandText = "UPDATE ArtikliGrupeBulk SET Prioritet=Prioritet-@Vrijednost WHERE NadgrupaID=@NadgrupaID AND Prioritet=@Prioritet+@Vrijednost; UPDATE ArtikliGrupeBulk SET Prioritet=@PrioritetNovi WHERE ID=@ID;"
                    komanda.ExecuteNonQuery()
                    Dim podaci As String = CMS.PodKategorijeMrezaTablicaBulk(nadgrupa)
                    Response.Write(podaci)
                Else
                    Response.Write("")
                End If

            End Using
        End Using


    End Sub

End Class