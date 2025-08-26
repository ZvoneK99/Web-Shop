Imports System.Data.SqlClient

Public Class IsnertClanka
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()
        Dim formID As Integer = 0
        
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

        'insert procedura
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.Parameters.AddWithValue("@NoviID", formID)
                komanda.Parameters.AddWithValue("@Naslov", txtNaslov)
                komanda.Parameters.AddWithValue("@Aktivno", chkAktivno)
                komanda.Parameters.AddWithValue("@VideoLink", txtVideoLink)
                komanda.Parameters.AddWithValue("@Ukratko", txtUkratko)
                komanda.Parameters.AddWithValue("@Clanak", txtClanak)
                komanda.Parameters("@NoviID").Direction = ParameterDirection.Output
                komanda.CommandText = "InsertClanka"
                komanda.ExecuteNonQuery()
                formID = Convert.ToInt32(komanda.Parameters("@NoviID").Value)
                Dim noviId = komanda.Parameters("@NoviID").Value.ToString()
                Response.Redirect("/CMS/Clanak.aspx?id=" & noviId)
            End Using
        End Using
    End Sub

End Class