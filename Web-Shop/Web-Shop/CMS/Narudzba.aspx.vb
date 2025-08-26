Imports System.Data.SqlClient

Public Class Narudzba
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function PrukaziPodatke() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim NarudzbaID = HttpContext.Current.Request.QueryString("id")

        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "PrintNarudzbeZaglavlje"
                komanda.Parameters.AddWithValue("@NarudzbaID", NarudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<div class=""divTbl"">")
                            html.Append("<table class=""tblZaglavlje"">")
                            html.Append("<tr>")
                            html.Append("<td>")
                            html.Append("<div class=""kupac"">")
                            html.Append("<b>Kupac:</b><br/>")
                            html.AppendFormat("{0} ({1})<br/>", citac("ImePrezime"), citac("SifraKorisnika"))
                            html.AppendFormat("{0}<br/>", citac("Adresa"))
                            html.AppendFormat("{0} - {1}<br/>", citac("ZIP"), citac("Grad"))
                            html.AppendFormat("Tel: {0}<br/>", citac("Telefon"))
                            html.AppendFormat("E-mail: {0}", citac("Email"))
                            html.Append("</div>") 'kupac
                            html.Append("</td>")
                            html.Append("<td style=""text-align:right;"">")
                            html.Append("<div class=""prodavac"">")
                            html.AppendFormat("<b style=""font-size:20px;"">{0}</b><br />", Komponente.Postavke("Tvrtka"))
                            html.AppendFormat("{0}<br />", Komponente.Postavke("Slogan"))
                            html.AppendFormat("{0} {1}, {2}<br />", Komponente.Postavke("ZIP"), Komponente.Postavke("Grad"), Komponente.Postavke("Adresa"))
                            'html.AppendFormat("PDV br. {0}<br />", Komponente.Postavke("PdvBroje"))
                            html.AppendFormat("ID br. {0}<br />", Komponente.Postavke("IdBroj"))
                            html.AppendFormat("Tel. {0}<br />", Komponente.Postavke("KontakTtel1"))
                            html.AppendFormat("E-mail: {0}", Komponente.Postavke("KontaktMail"))
                            html.Append("</div>") 'prodavac
                            html.Append("</td>")
                            html.Append("</tr>")
                            html.Append("</table>")
                            html.Append("</div>") 'divTbl

                            html.Append("<div class=""divTbl"" style=""text-align:center; margin:20px 0px;"">")
                            html.AppendFormat("<b style=""font-size:20px;"">Narudžba br. {0}</b><br/>", citac("ID"))
                            html.AppendFormat("Datum: {0}", Format(citac("DatumKreiranja"), "dd.MM.yyyy"))
                            html.Append("</div>") 'divTbl
                        End While
                    End If
                End Using
            End Using
        End Using

        html.Append("<table class=""tblStavke"">")
        html.Append("<tr>")
        html.Append("<th>")
        html.Append("ŠIFRA")
        html.Append("</th>")
        html.Append("<th style=""text-align:left;"">")
        html.Append("NAZIV ARTIKLA")
        html.Append("</th>")
        html.Append("<th>")
        html.Append("KOLIČINA")
        html.Append("</th>")
        html.Append("<th>")
        html.Append("CIJENA")
        html.Append("</th>")
        html.Append("<th>")
        html.Append("IZNOS")
        html.Append("</th>")
        html.Append("</tr>")
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "PrintNarudzbeStavke"
                komanda.Parameters.AddWithValue("@NarudzbaID", NarudzbaID)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.Append("<tr>")
                            html.Append("<td>")
                            html.AppendFormat("{0}", citac("SifraArtikla"))
                            html.Append("</td>")
                            html.Append("<td style=""text-align:left;"">")
                            html.AppendFormat("{0}", citac("NazivArtikla"))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("{0}", citac("Kolicina"))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("{0}", Format(citac("Cijena"), "N2"))
                            html.Append("</td>")
                            html.Append("<td>")
                            html.AppendFormat("{0}", Format(citac("Kolicina") * citac("Cijena"), "N2"))
                            html.Append("</td>")
                            html.Append("</tr>")
                        End While
                    End If
                End Using
            End Using
        End Using
        html.Append("<tr style=""border-top:1px solid #dfdfdf;"">")
        html.Append("<td>")
        html.Append("&nbsp;")
        html.Append("</td>")
        html.Append("<td style=""text-align:left;"">")
        html.Append("&nbsp;")
        html.Append("</td>")
        html.Append("<td>")
        html.Append("Ukupno:")
        html.Append("</td>")
        html.Append("<td colspan=""2"" style=""text-align:right;padding-right:10px;"">")
        html.AppendFormat("{0} KM", Format(Komponente.SumaNarudzbe(NarudzbaID), "N2"))
        html.Append("</td>")
        html.Append("</tr>")
        html.Append("</table>") 'tblStavke

        html.Append("<button type=""button"" onclick=""javascript: window.print();"" class=""noprint"">PRINT</button>")
        html.Append("<button type=""button"" onclick=""javascript:window.close()"" class=""noprint"">Zatvori prozor</button>")

        Return html.ToString()
    End Function

End Class