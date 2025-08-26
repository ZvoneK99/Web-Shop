Imports System.Data.SqlClient
Public Class InsertArtikla
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim formID As Integer = 0
        Dim hidSkladisteId As Integer = HttpContext.Current.Request.Params("hidSkladisteId")
        Dim txtSifraArtikla As String = HttpContext.Current.Request.Params("txtSifraArtikla")
        Dim txtBarCod As String = HttpContext.Current.Request.Params("txtBarCod")
        Dim txtkolicina As String = HttpContext.Current.Request.Params("txtkolicina")
        Dim txtNazivArtikla As String = HttpContext.Current.Request.Params("txtNazivArtikla")

        Dim CijenaMPC As Decimal = 0
        If HttpContext.Current.Request.Params("txtCijenaMPC") = "" Then
            CijenaMPC = 0
        Else
            CijenaMPC = HttpContext.Current.Request.Params("txtCijenaMPC")
        End If

        Dim CijenaMPCBulk As Decimal = 0
        If HttpContext.Current.Request.Params("txtCijenaMPCBulk") = "" Then
            CijenaMPCBulk = 0
        Else
            CijenaMPCBulk = HttpContext.Current.Request.Params("txtCijenaMPCBulk")
        End If

        Dim CijenaAkcija As Decimal = 0
        If HttpContext.Current.Request.Params("txtCijenaAkcija") = "" Then
            CijenaAkcija = 0
        Else
            CijenaAkcija = HttpContext.Current.Request.Params("txtCijenaAkcija")
        End If

        Dim CijenaAkcijaBulk As Decimal = 0
        If HttpContext.Current.Request.Params("txtCijenaAkcijaBulk") = "" Then
            CijenaAkcijaBulk = 0
        Else
            CijenaAkcijaBulk = HttpContext.Current.Request.Params("txtCijenaAkcijaBulk")
        End If

        Dim Akcija As Decimal = 0
        If HttpContext.Current.Request.Params("txtAkcija") = "" Then
            Akcija = 0
        Else
            Akcija = HttpContext.Current.Request.Params("txtAkcija")
        End If

        Dim AkcijaBulk As Decimal = 0
        If HttpContext.Current.Request.Params("txtAkcijaBulk") = "" Then
            AkcijaBulk = 0
        Else
            AkcijaBulk = HttpContext.Current.Request.Params("txtAkcijaBulk")
        End If

        'Dim Procenat As Decimal = 0
        'If HttpContext.Current.Request.Params("txtProcenat") = "" Then
        '    Procenat = 0
        'Else
        '    Procenat = HttpContext.Current.Request.Params("txtProcenat")
        'End If

        Dim chkIzdvojeno As String = HttpContext.Current.Request.Params("chkIzdvojeno")
        If chkIzdvojeno = "on" Then
            chkIzdvojeno = 1
        Else
            chkIzdvojeno = 0
        End If

        Dim chkIzdvojenoBulk As String = HttpContext.Current.Request.Params("chkIzdvojenoBulk")
        If chkIzdvojenoBulk = "on" Then
            chkIzdvojenoBulk = 1
        Else
            chkIzdvojenoBulk = 0
        End If

        Dim chkAktivno As String = HttpContext.Current.Request.Params("chkAktivno")
        If chkAktivno = "on" Then
            chkAktivno = 1
        Else
            chkAktivno = 0
        End If

        Dim chkAktivnoBulk As String = HttpContext.Current.Request.Params("chkAktivnoBulk")
        If chkAktivnoBulk = "on" Then
            chkAktivnoBulk = 1
        Else
            chkAktivnoBulk = 0
        End If

        Dim chkNaUpit As String = HttpContext.Current.Request.Params("chkNaUpit")
        If chkNaUpit = "on" Then
            chkNaUpit = 1
        Else
            chkNaUpit = 0
        End If

        Dim chkNaUpitBulk As String = HttpContext.Current.Request.Params("chkNaUpitBulk")
        If chkNaUpitBulk = "on" Then
            chkNaUpitBulk = 1
        Else
            chkNaUpitBulk = 0
        End If

        Dim chkBesplatnaDostava As String = HttpContext.Current.Request.Params("chkBesplatnaDostava")
        If chkBesplatnaDostava = "on" Then
            chkBesplatnaDostava = 1
        Else
            chkBesplatnaDostava = 0
        End If

        Dim chkBesplatnaDostavaBulk As String = HttpContext.Current.Request.Params("chkBesplatnaDostavaBulk")
        If chkBesplatnaDostavaBulk = "on" Then
            chkBesplatnaDostavaBulk = 1
        Else
            chkBesplatnaDostavaBulk = 0
        End If

        Dim txtGarancija As String = HttpContext.Current.Request.Params("txtGarancija")
        Dim txtIsporuka As String = HttpContext.Current.Request.Params("txtIsporuka")
        Dim txtVideoLink As String = HttpContext.Current.Request.Params("txtVideoLink")
        Dim ddlKategorija As Integer = HttpContext.Current.Request.Params("ddlKategorija")
        Dim ddlPodKategorija As Integer = HttpContext.Current.Request.Params("ddlPodKategorija")
        Dim ddlKategorijaBulk As Integer = HttpContext.Current.Request.Params("ddlKategorijaBulk")
        Dim ddlPodKategorijaBulk As Integer = HttpContext.Current.Request.Params("ddlPodKategorijaBulk")
        Dim txtOpisKratki As String = HttpContext.Current.Request.Params("txtOpisKratki")
        Dim txtOpis As String = HttpContext.Current.Request.Params("txtOpis")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.Parameters.AddWithValue("@NoviID", formID)
                komanda.Parameters.AddWithValue("@SkladisteID", hidSkladisteId)
                komanda.Parameters.AddWithValue("@SifraRobe", txtSifraArtikla)
                komanda.Parameters.AddWithValue("@Barcod", txtBarCod)
                komanda.Parameters.AddWithValue("@Kolicina", txtkolicina)
                komanda.Parameters.AddWithValue("@Naziv", txtNazivArtikla.Trim)
                komanda.Parameters.AddWithValue("@Cijena", CijenaMPC)
                komanda.Parameters.AddWithValue("@CijenaBulk", CijenaMPCBulk)
                komanda.Parameters.AddWithValue("@AkcijaCijena", CijenaAkcija)
                komanda.Parameters.AddWithValue("@AkcijaCijenaBulk", CijenaAkcijaBulk)
                komanda.Parameters.AddWithValue("@Akcija", Akcija)
                komanda.Parameters.AddWithValue("@AkcijaBulk", AkcijaBulk)
                komanda.Parameters.AddWithValue("@Izdvojeno", chkIzdvojeno)
                komanda.Parameters.AddWithValue("@IzdvojenoBulk", chkIzdvojenoBulk)
                komanda.Parameters.AddWithValue("@Aktivno", chkAktivno)
                komanda.Parameters.AddWithValue("@AktivnoBulk", chkAktivnoBulk)
                komanda.Parameters.AddWithValue("@NaUpit", chkNaUpit)
                komanda.Parameters.AddWithValue("@NaUpitBulk", chkNaUpitBulk)
                komanda.Parameters.AddWithValue("@BesplatnaDostava", chkBesplatnaDostava)
                komanda.Parameters.AddWithValue("@BesplatnaDostavaBulk", chkBesplatnaDostavaBulk)
                komanda.Parameters.AddWithValue("@Garancija", txtGarancija)
                komanda.Parameters.AddWithValue("@Isporuka", txtIsporuka)
                komanda.Parameters.AddWithValue("@VideoLink", txtVideoLink)
                komanda.Parameters.AddWithValue("@NadGrupaID", ddlKategorija)
                komanda.Parameters.AddWithValue("@GrupaID", ddlPodKategorija)
                komanda.Parameters.AddWithValue("@NadGrupaIDBulk", ddlKategorijaBulk)
                komanda.Parameters.AddWithValue("@GrupaIDBulk", ddlPodKategorijaBulk)
                komanda.Parameters.AddWithValue("@OpisKratki", txtOpisKratki)
                komanda.Parameters.AddWithValue("@Opis", txtOpis)
                komanda.Parameters("@NoviID").Direction = ParameterDirection.Output
                komanda.CommandText = "InsertArtikla"
                komanda.ExecuteNonQuery()
                formID = Convert.ToInt32(komanda.Parameters("@NoviID").Value)
                Dim noviId = komanda.Parameters("@NoviID").Value.ToString()
                Response.Redirect("/CMS/Artikal.aspx?id=" & noviId)
            End Using
        End Using
    End Sub

End Class