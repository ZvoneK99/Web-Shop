Imports System.Data.SqlClient

Public Class PosaljiRecenziju
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ime As String = Request.Form("ime")
            Dim komentar As String = Request.Form("komentar")
            Dim ocjena As Integer = Request.Form("rating")
            Dim narudzbaID As Integer = Request.Form("narudzbaID")


            If Not String.IsNullOrEmpty(Request.Form("rating")) Then
                Integer.TryParse(Request.Form("rating"), ocjena)
            End If

            If ime <> "" AndAlso komentar <> "" AndAlso ocjena > 0 Then
                Dim putanja As String = Komponente.SQLKonekcija()

                Using konekcija As New SqlConnection(putanja)
                    konekcija.Open()

                    'Dim sqlNarudzba As String = "SELECT TOP 1 ID FROM Narudzbe ORDER BY ID DESC"

                    'Using cmdNarudzba As New SqlCommand(sqlNarudzba, konekcija)
                    '    Dim result As Object = cmdNarudzba.ExecuteScalar()
                    '    If result IsNot Nothing Then
                    '        narudzbaID = Convert.ToInt32(result)
                    '    End If
                    'End Using

                    Dim sqlKomentar As String = "INSERT INTO Komentari (NarudzbaID, Ime, Komentar, Ocjena, Date, Odobreno) " &
                                                "VALUES (@NarudzbaID, @Ime, @Komentar, @Ocjena, @Date, 0)"

                    Using cmdKomentar As New SqlCommand(sqlKomentar, konekcija)
                        cmdKomentar.Parameters.AddWithValue("@NarudzbaID", narudzbaID)
                        cmdKomentar.Parameters.AddWithValue("@Ime", ime)
                        cmdKomentar.Parameters.AddWithValue("@Komentar", komentar)
                        cmdKomentar.Parameters.AddWithValue("@Ocjena", ocjena)
                        cmdKomentar.Parameters.AddWithValue("@Date", DateTime.Now)
                        cmdKomentar.ExecuteNonQuery()
                    End Using
                End Using

                'Response.Redirect("hvala.aspx")
            Else
                'lblGreska.Text = "Molimo ispunite sva polja i ocijenite proizvod."
            End If

        End If
        Response.Redirect("/")
    End Sub

End Class
