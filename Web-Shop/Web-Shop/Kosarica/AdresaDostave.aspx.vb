Imports System.Data.SqlClient

Public Class AdresaDostave
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                        If citac IsNot Nothing Then
                            While citac.Read()
                                Me.imePrezime.Value = citac("ImePrezime")
                                Me.email.Value = citac("Email")
                                Me.adresa.Value = citac("Adresa")
                                Me.grad.Value = citac("Grad")
                                Me.postBr.Value = citac("ZIP")
                                Me.brTel.Value = citac("Telefon")
                            End While
                        End If
                    End Using
                End Using
            End Using

        End If
    End Sub

End Class