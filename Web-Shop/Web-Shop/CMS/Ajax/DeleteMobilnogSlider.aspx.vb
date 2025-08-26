Imports System.Data.SqlClient

Public Class DeleteMobilnogSlider
    Inherits System.Web.UI.Page
    Private Property FileDeleted As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim BanerID As Integer = Request.QueryString("id")
        Dim file As String = Request.QueryString("file")
        Dim IdLogiranog As Integer = Komponente.LogiraniKorisnikID()
        'Dim logiraniKorisnik As String = Komponente.LogiraniKorisnikIme(IdLogiranog)
        Try
            IO.File.Delete(Server.MapPath("~/Datoteke/Slider/" & file))
            FileDeleted = True
            ' ZapisiLog.ZapisiLog(1, logiraniKorisnik, "***", "izbrisao datoteku &quot;" & file & "&quot;", "delete")
            IzbrisiIzBaze(BanerID, file)
        Catch ex As Exception
            FileDeleted = False
            Exit Sub
        End Try
    End Sub

    Private Sub IzbrisiIzBaze(BanerID As Integer, file As String)
        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@DatotekaMob", file)
                komanda.Parameters.AddWithValue("@BanerID", BanerID)

                komanda.CommandText = "UPDATE Slider SET SlikaMob='#' WHERE ID=@BanerID AND SlikaMob=@DatotekaMob "
                komanda.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class