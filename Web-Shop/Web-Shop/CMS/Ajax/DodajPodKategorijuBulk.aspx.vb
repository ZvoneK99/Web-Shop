Imports System.Data.SqlClient

Public Class DodajPodKategorijuBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()

        Dim txtSifraPodKaregorije As String = HttpContext.Current.Request.Params("txtPodKaregorija") ' HttpContext.Current.Request.Params("txtSifraPodKaregorije")
        Dim txtPodKaregorija As String = HttpContext.Current.Request.Params("txtPodKaregorija")
        Dim hidKaregorija As Integer = HttpContext.Current.Request.Params("hidKaregorija")

        'insert procedura
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@SifraGrupe", IspraviZnakove(txtSifraPodKaregorije))
                komanda.Parameters.AddWithValue("@Naziv", IspraviZnakove(txtPodKaregorija))
                komanda.Parameters.AddWithValue("@Prioritet", ProvjeriPrioritetPodKategorije(hidKaregorija) + 1)
                komanda.Parameters.AddWithValue("@KategorijaID", hidKaregorija)
                komanda.CommandText = "INSERT INTO ArtikliGrupeBulk (NadGrupaID, SifraGrupe, Grupa, Prioritet, Aktivno) VALUES (@KategorijaID, @SifraGrupe, @Naziv, @Prioritet, '1')"
                komanda.ExecuteNonQuery()
                Response.Redirect("/CMS/PodkategorijaBulk.aspx?KategorijaID=" & hidKaregorija)
            End Using
        End Using
    End Sub

    Private Function ProvjeriPrioritetPodKategorije(hidKaregorija As Integer) As Integer
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT TOP 1 Prioritet FROM ArtikliGrupeBulk WHERE NadGrupaID=@KategorijaID ORDER BY Prioritet DESC"
                komanda.Parameters.AddWithValue("@KategorijaID", hidKaregorija)
                'komanda.Parameters.AddWithValue("@SlovoArtikla", SlovoArtikla.ToString)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("{0}", citac("Prioritet"))
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append(0)
        End If

        Return html.ToString()
    End Function

    Private Function IspraviZnakove(txtPodKaregorija As String) As String
        txtPodKaregorija = txtPodKaregorija.Replace("""", "&quot;")
        Return txtPodKaregorija
    End Function

End Class