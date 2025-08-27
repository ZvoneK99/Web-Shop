Imports System.Data.SqlClient

Public Class UpdateZaporke
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim hidId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidId"))
        Dim txtLozinka1 As String = HttpContext.Current.Request.Params("txtPassword1")
        Dim txtLozinka2 As String = HttpContext.Current.Request.Params("txtPassword2")

        If txtLozinka1 = txtLozinka2 And txtLozinka1 <> "" Then
            Using konekcija As New SqlConnection(putanja)
                konekcija.Open()
                Using komanda As New SqlCommand()
                    komanda.Connection = konekcija
                    komanda.CommandType = CommandType.Text
                    komanda.CommandText = "UPDATE Korisnici SET" _
                                        & " Lozinka=@Lozinka" _
                                        & " WHERE ID=@ID;"
                    komanda.Parameters.AddWithValue("@ID", hidId)
                    komanda.Parameters.AddWithValue("@Lozinka", txtLozinka1)
                    komanda.ExecuteNonQuery()
                End Using
            End Using
            Response.Redirect("/CMS/KorisnikCMS.aspx?KorisnikID=" & hidId & "&msg=zaporkauspjesno")
        Else
            Response.Redirect("/CMS/KorisnikCMS.aspx?KorisnikID=" & hidId & "&msg=zaporkaneuspjesno")
        End If
    End Sub

End Class