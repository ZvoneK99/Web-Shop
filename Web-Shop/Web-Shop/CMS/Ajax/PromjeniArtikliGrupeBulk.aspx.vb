Imports System.Data.SqlClient

Public Class PromjeniArtikliGrupeBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()
        Dim stariNaziv As String = Request.Params("stariNaziv") ' "TL5787_new.jpg"
        Dim noviNaziv As String = Request.Params("noviNaziv") ' "TL5787.jpg"
        Dim slikaID As Integer = Request.Params("slikaID")
        Dim NadGrupaID As Integer = Request.Params("NadGrupaID")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE ArtikliGrupeBulk SET" _
                                    & " Grupa=@Grupa" _
                                    & " WHERE ID=@ID"
                komanda.Parameters.AddWithValue("@ID", slikaID)
                komanda.Parameters.AddWithValue("@Grupa", If(noviNaziv = "", "#", noviNaziv))
                'komanda.Parameters.AddWithValue("@StariNaziv", stariNaziv)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/PodkategorijaBulk.aspx?KategorijaID=" & NadGrupaID)
    End Sub

End Class