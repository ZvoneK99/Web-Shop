Imports System.Data.SqlClient

Public Class DodajKategorijuBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()

        Dim txtKaregorija As String = HttpContext.Current.Request.Params("txtKaregorija")
        Dim txtSifraKaregorije As String = HttpContext.Current.Request.Params("txtKaregorija") ' HttpContext.Current.Request.Params("txtSifraKaregorije")

        'insert procedura
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@Naziv", IspraviZnakove(txtKaregorija))
                komanda.Parameters.AddWithValue("@SifraNadgrupe", IspraviZnakove(txtSifraKaregorije))
                komanda.Parameters.AddWithValue("@Prioritet", ProvjeriPrioritetKategorije() + 1)
                komanda.CommandText = "INSERT INTO ArtikliNadGrupeBulk (SifraNadgrupe, NadGrupa, Prioritet, Aktivno) VALUES (@Prioritet, @Naziv, @Prioritet, '1')"
                komanda.ExecuteNonQuery()
                Response.Redirect("/CMS/GrupeBulk.aspx")
            End Using
        End Using
    End Sub

    Private Function ProvjeriPrioritetKategorije() As Integer
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.conekcija()

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "SELECT TOP 1 Prioritet FROM ArtikliNadGrupeBulk ORDER BY Prioritet DESC"
                'komanda.Parameters.AddWithValue("@Stranica", stranica)
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

    Private Function IspraviZnakove(txtKaregorija As String) As String
        txtKaregorija = txtKaregorija.Replace("""", "&quot;")
        Return txtKaregorija
    End Function

End Class