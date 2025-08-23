Public Class ArtikliDodajKolicinu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As Integer = Convert.ToInt32(Request.Form("id"))
        Dim formKolicina As String = Request.Form("kolicina")
        Dim formTrenutna As String = Request.Form("trenutna")
        'Dim putanja As String = Conti.conekcijaVibar

        Dim n As Narudzba
        n = CType(Session("Narudzba"), Narudzba)
        If formKolicina = "1" Then
            Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = id.ToString)
            postojeciArtikal.Kolicina += 1
        Else
            If formTrenutna = "1" Then
                Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = id.ToString)
                n.Artikli.Remove(postojeciArtikal)
            Else
                Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = id.ToString)
                postojeciArtikal.Kolicina -= 1
            End If
        End If

        UcitajKolicinuArtikala(n)
    End Sub

    Public Shared Sub UcitajKolicinuArtikala(ByVal n As Narudzba)

        Dim redci As String = Komponente.MojaKosaricaSession(n)
        HttpContext.Current.Response.Write(redci)

    End Sub

End Class