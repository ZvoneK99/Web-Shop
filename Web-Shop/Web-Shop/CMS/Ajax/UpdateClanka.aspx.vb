Imports System.Data.SqlClient

Public Class UpdateClanka
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()
        Dim hidId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidId"))
        'Dim txtRB As String = HttpContext.Current.Request.Params("txtRB")
        'Dim txtNazivArtikla As String = HttpContext.Current.Request.Params("txtNazivArtikla")

        Dim chkAktivno As String = HttpContext.Current.Request.Params("chkAktivno")
        If chkAktivno = "on" Then
            chkAktivno = 1
        Else
            chkAktivno = 0
        End If
        Dim txtNaslov As String = HttpContext.Current.Request.Params("txtNaslov")
        Dim txtVideoLink As String = HttpContext.Current.Request.Params("txtVideoLink")
        Dim txtUkratko As String = HttpContext.Current.Request.Params("txtUkratko")
        Dim txtClanak As String = HttpContext.Current.Request.Params("txtClanak")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Novosti SET" _
                                    & " Aktivno=@Aktivno, " _
                                    & " Naslov=@Naslov, " _
                                    & " Ukratko=@Ukratko, " _
                                    & " VideoLink=@VideoLink, " _
                                    & " Clanak=@Clanak " _
                                    & " WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", hidId)
                komanda.Parameters.AddWithValue("@Aktivno", chkAktivno)
                komanda.Parameters.AddWithValue("@Naslov", txtNaslov)
                komanda.Parameters.AddWithValue("@Ukratko", txtUkratko)
                komanda.Parameters.AddWithValue("@Clanak", txtClanak)
                komanda.Parameters.AddWithValue("@VideoLink", txtVideoLink)
                'komanda.Parameters.AddWithValue("@ZadnjeAzuriranje", DateAndTime.Now())
                'komanda.Parameters.AddWithValue("@ZadnjiAzurirao", igre_ba.Komponente.LogiraniKorisnikID)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/Clanak.aspx?id=" & hidId)

    End Sub

End Class