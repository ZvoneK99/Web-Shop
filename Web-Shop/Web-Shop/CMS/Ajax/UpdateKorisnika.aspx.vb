Imports System.Data.SqlClient

Public Class UpdateKorisnika
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.conekcija()
        Dim hidId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidId"))
        Dim chkAktivan As String = HttpContext.Current.Request.Params("chkAktivan")
        If chkAktivan = "on" Then
            chkAktivan = 1
        Else
            chkAktivan = 0
        End If
        Dim txtImePrezime As String = HttpContext.Current.Request.Params("txtImePrezime")
        Dim txtIdBroj As String = HttpContext.Current.Request.Params("txtIdBroj")
        Dim txtPdvBroj As String = HttpContext.Current.Request.Params("txtPdvBroj")
        Dim txtSifra As String = HttpContext.Current.Request.Params("txtSifra")
        Dim txtTelefon As String = HttpContext.Current.Request.Params("txtTelefon")
        Dim txtEmail As String = HttpContext.Current.Request.Params("txtEmail")
        Dim AdminLevelID As Integer = HttpContext.Current.Request.Params("ddlAdminLevel")
        If AdminLevelID = 0 Then
            AdminLevelID = 10
        End If
        Dim txtAdresa As String = HttpContext.Current.Request.Params("txtAdresa")
        Dim txtGrad As String = HttpContext.Current.Request.Params("txtGrad")
        Dim txtZIP As String = HttpContext.Current.Request.Params("txtZIP")
        Dim txtNapomena As String = HttpContext.Current.Request.Params("txtNapomena")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                If hidId = 0 Then
                    komanda.CommandType = CommandType.StoredProcedure
                    komanda.CommandText = "CMSInsertKorisnika"
                    komanda.Parameters.AddWithValue("@NoviID", hidId)
                    komanda.Parameters.AddWithValue("@Aktivan", chkAktivan)
                    komanda.Parameters.AddWithValue("@SifraKorisnika", txtSifra)
                    komanda.Parameters.AddWithValue("@ImePrezime", txtImePrezime)
                    komanda.Parameters.AddWithValue("@IdBroj", txtIdBroj)
                    komanda.Parameters.AddWithValue("@PdvBroj", txtPdvBroj)
                    komanda.Parameters.AddWithValue("@Telefon", txtTelefon)
                    komanda.Parameters.AddWithValue("@Email", txtEmail)
                    komanda.Parameters.AddWithValue("@AdminLevel", AdminLevelID)
                    komanda.Parameters.AddWithValue("@Adresa", txtAdresa)
                    komanda.Parameters.AddWithValue("@Grad", txtGrad)
                    komanda.Parameters.AddWithValue("@ZIP", txtZIP)
                    komanda.Parameters.AddWithValue("@Napomena", txtNapomena)
                    komanda.Parameters("@NoviID").Direction = ParameterDirection.Output
                    komanda.ExecuteNonQuery()
                    hidId = Convert.ToInt32(komanda.Parameters("@NoviID").Value)
                    Dim noviId = komanda.Parameters("@NoviID").Value.ToString()
                    Response.Redirect("/CMS/KorisnikCMS.aspx?KorisnikID=" & noviId)
                Else
                    komanda.CommandType = CommandType.Text
                    komanda.CommandText = "UPDATE Korisnici SET" _
                                    & " Aktivan=@Aktivan," _
                                    & " SifraKorisnika=@SifraKorisnika," _
                                    & " ImePrezime=@ImePrezime," _
                                    & " IdBroj=@IdBroj," _
                                    & " PdvBroj=@PdvBroj," _
                                    & " Telefon=@Telefon," _
                                    & " Email=@Email," _
                                    & " AdminLevel=@AdminLevel," _
                                    & " Adresa=@Adresa," _
                                    & " Grad=@Grad," _
                                    & " ZIP=@ZIP, " _
                                    & " Napomena=@Napomena" _
                                    & " WHERE ID=@ID;"
                    komanda.Parameters.AddWithValue("@Aktivan", chkAktivan)
                    komanda.Parameters.AddWithValue("@ImePrezime", txtImePrezime)
                    komanda.Parameters.AddWithValue("@IdBroj", txtIdBroj)
                    komanda.Parameters.AddWithValue("@PdvBroj", txtPdvBroj)
                    komanda.Parameters.AddWithValue("@SifraKorisnika", txtSifra)
                    komanda.Parameters.AddWithValue("@Telefon", txtTelefon)
                    komanda.Parameters.AddWithValue("@Email", txtEmail)
                    komanda.Parameters.AddWithValue("@AdminLevel", AdminLevelID)
                    komanda.Parameters.AddWithValue("@Adresa", txtAdresa)
                    komanda.Parameters.AddWithValue("@Grad", txtGrad)
                    komanda.Parameters.AddWithValue("@ZIP", txtZIP)
                    komanda.Parameters.AddWithValue("@Napomena", txtNapomena)
                    komanda.Parameters.AddWithValue("@ID", hidId)
                    komanda.ExecuteNonQuery()
                    Response.Redirect("/CMS/KorisnikCMS.aspx?KorisnikID=" & hidId)
                End If
            End Using
        End Using


    End Sub

End Class