Imports System.Data.SqlClient

Public Class DopuniVezaniArtikal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim Id As Integer = Request.Form("Id")
        Dim artikalid As Integer = Request.Form("artikalid")

        'MsgBox("Korisnik: " & KorisnikID & " - Barnd: " & BrandID)

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@ArtikalID", artikalid)
                komanda.Parameters.AddWithValue("@ID", Id)

                komanda.CommandText = "INSERT INTO ArtikliVezaniBulk (ArtikalID, ArtikalVezaniID) VALUES (@ArtikalID, @Id)"
                komanda.ExecuteNonQuery()

            End Using
        End Using
        Dim podaci As String = "DODANO"
        Response.Write(podaci)
    End Sub

    'Private Sub DopuniVezaniArtikal_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    '    If Session("ValjanUser") = False Then
    '        Response.Redirect("/cms/")
    '    Else
    '        If Admin.NivoLogiranogKorisnika() = "suradnik" Then
    '            Response.Redirect("/cms/nastavnici.aspx")
    '        End If
    '    End If
    'End Sub
End Class