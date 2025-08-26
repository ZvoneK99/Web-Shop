Imports System.Data.SqlClient

Public Class UpdateArtikla
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim hidId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidId"))
        Dim hidSkladisteId As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("hidSkladisteId"))
        Dim txtNazivArtikla As String = HttpContext.Current.Request.Params("txtNazivArtikla")

        Dim Kolicina As Integer = 0
        If HttpContext.Current.Request.Params("txtkolicina") = "" Then
            Kolicina = 0
        Else
            Kolicina = HttpContext.Current.Request.Params("txtkolicina")
        End If
        Dim chkFixnaKolicina As String = HttpContext.Current.Request.Params("chkFixnaKolicina")
        If chkFixnaKolicina = "on" Then
            chkFixnaKolicina = 1
        Else
            chkFixnaKolicina = 0
        End If

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
            Akcija = HttpContext.Current.Request.Params("txtAkcijaBulk")
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

        Dim chkCijenaFixna As String = HttpContext.Current.Request.Params("chkCijenaFixna")
        If chkCijenaFixna = "on" Then
            chkCijenaFixna = 1
        Else
            chkCijenaFixna = 0
        End If

        Dim chkCijenaFixnaBulk As String = HttpContext.Current.Request.Params("chkCijenaFixnaBulk")
        If chkCijenaFixnaBulk = "on" Then
            chkCijenaFixnaBulk = 1
        Else
            chkCijenaFixnaBulk = 0
        End If

        Dim ddlKategorija As Integer = HttpContext.Current.Request.Params("ddlKategorija")
        Dim ddlPodKategorija As Integer = HttpContext.Current.Request.Params("ddlPodKategorija")
        Dim ddlKategorijaBulk As Integer = HttpContext.Current.Request.Params("ddlKategorijaBulk")
        Dim ddlPodKategorijaBulk As Integer = HttpContext.Current.Request.Params("ddlPodKategorijaBulk")
        Dim txtOpisKratki As String = HttpContext.Current.Request.Params("txtOpisKratki")
        Dim txtOpis As String = HttpContext.Current.Request.Params("txtOpis")
        Dim txtVideoLink As String = HttpContext.Current.Request.Params("txtVideoLink")
        Dim txtPolje1 As String = HttpContext.Current.Request.Params("txtPolje1")
        Dim txtPolje2 As String = HttpContext.Current.Request.Params("txtPolje2")
        Dim txtPolje3 As String = HttpContext.Current.Request.Params("txtPolje3")
        Dim txtGarancija As String = HttpContext.Current.Request.Params("txtGarancija")
        Dim txtIsporuka As String = HttpContext.Current.Request.Params("txtIsporuka")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.CommandText = "UPDATE Artikli SET" _
                                    & " Naziv=@Naziv, " _
                                    & " Cijena=@Cijena, " _
                                    & " CijenaBulk=@CijenaBulk, " _
                                    & " Kolicina=@Kolicina, " _
                                    & " FixnaKolicina=@FixnaKolicina, " _
                                    & " AkcijaCijena=@CijenaAkcija, " _
                                    & " AkcijaCijenaBulk=@CijenaAkcijaBulk, " _
                                    & " Akcija=@Akcija, " _
                                    & " AkcijaBulk=@AkcijaBulk, " _
                                    & " Izdvojeno=@Izdvojeno, " _
                                    & " IzdvojenoBulk=@IzdvojenoBulk, " _
                                    & " Aktivno=@Aktivno, " _
                                    & " AktivnoBulk=@AktivnoBulk, " _
                                    & " NaUpit=@NaUpit, " _
                                    & " GrupaID=@GrupaID, " _
                                    & " GrupaIDBulk=@GrupaIDBulk, " _
                                    & " BesplatnaDostava=@BesplatnaDostava, " _
                                    & " BesplatnaDostavaBulk=@BesplatnaDostavaBulk, " _
                                    & " CijenaFixna=@CijenaFixna, " _
                                    & " CijenaFixnaBulk=@CijenaFixnaBulk, " _
                                    & " OpisKratki=@OpisKratki, " _
                                    & " NadGrupaID=@NadGrupaID, " _
                                    & " NadGrupaIDBulk=@NadGrupaIDBulk, " _
                                    & " Opis=@Opis, " _
                                    & " ZadnjeAzuriranje=@ZadnjeAzuriranje, " _
                                    & " VideoLink=@VideoLink, " _
                                    & " Garancija=@Garancija, " _
                                    & " Polje1=@Polje1, " _
                                    & " Polje2=@Polje2, " _
                                    & " Polje3=@Polje3, " _
                                    & " Isporuka=@Isporuka, " _
                                    & " ZadnjiAzurirao=@ZadnjiAzurirao" _
                                    & " WHERE ID=@ID;"
                komanda.Parameters.AddWithValue("@ID", hidId)
                komanda.Parameters.AddWithValue("@Naziv", txtNazivArtikla.Trim)
                komanda.Parameters.AddWithValue("@Cijena", CijenaMPC)
                komanda.Parameters.AddWithValue("@CijenaBulk", CijenaMPCBulk)
                komanda.Parameters.AddWithValue("@Kolicina", Kolicina)
                komanda.Parameters.AddWithValue("@FixnaKolicina", chkFixnaKolicina)
                komanda.Parameters.AddWithValue("@CijenaAkcija", CijenaAkcija)
                komanda.Parameters.AddWithValue("@CijenaAkcijaBulk", CijenaAkcijaBulk)
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
                komanda.Parameters.AddWithValue("@CijenaFixna", chkCijenaFixna)
                komanda.Parameters.AddWithValue("@CijenaFixnaBulk", chkCijenaFixnaBulk)
                komanda.Parameters.AddWithValue("@OpisKratki", txtOpisKratki)
                komanda.Parameters.AddWithValue("@Opis", txtOpis)
                komanda.Parameters.AddWithValue("@VideoLink", txtVideoLink)
                komanda.Parameters.AddWithValue("@Polje1", txtPolje1)
                komanda.Parameters.AddWithValue("@Polje2", txtPolje2)
                komanda.Parameters.AddWithValue("@Polje3", txtPolje3)
                komanda.Parameters.AddWithValue("@Garancija", txtGarancija)
                komanda.Parameters.AddWithValue("@Isporuka", txtIsporuka)
                komanda.Parameters.AddWithValue("@ZadnjeAzuriranje", DateAndTime.Now())
                komanda.Parameters.AddWithValue("@ZadnjiAzurirao", Web_Shop.Komponente.LogiraniKorisnikID)
                komanda.Parameters.AddWithValue("@NadGrupaID", ddlKategorija)
                komanda.Parameters.AddWithValue("@GrupaID", ddlPodKategorija)
                komanda.Parameters.AddWithValue("@NadGrupaIDBulk", ddlKategorijaBulk)
                komanda.Parameters.AddWithValue("@GrupaIDBulk", ddlPodKategorijaBulk)
                komanda.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("/CMS/Artikal.aspx?id=" & hidId)

    End Sub

End Class