Imports System.Data.SqlClient

Public Class AdresaDostave
    Inherits System.Web.UI.Page

   Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
        If Session("ValjanUser") = True Then
            Dim KupacId As Integer = Komponente.LogiraniKorisnikID
            Dim putanja As String = Komponente.SQLKonekcija()

            Using konekcija As New SqlConnection(putanja)
                konekcija.Open()
                Using komanda As New SqlCommand()
                    komanda.Connection = konekcija
                    komanda.CommandType = CommandType.Text
                    komanda.CommandText = "SELECT * FROM Korisnici WHERE ID=@KorisnikId"
                    komanda.Parameters.AddWithValue("@KorisnikId", KupacId)
                    Using citac As SqlDataReader = komanda.ExecuteReader()
                        If citac IsNot Nothing AndAlso citac.Read() Then
                            imePrezime.Value = citac("ImePrezime").ToString()
                            email.Value = citac("Email").ToString()
                            adresa.Value = citac("Adresa").ToString()
                            grad.Value = citac("Grad").ToString()
                            postBr.Value = citac("ZIP").ToString()
                            brTel.Value = citac("Telefon").ToString()
                        End If
                    End Using
                End Using
            End Using
        End If
    End If
End Sub


End Class