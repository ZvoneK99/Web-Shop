Public Class ArtikliPromjeniKolicinu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim ID As Integer = Convert.ToInt32(Request.Form("id"))
        Dim kolicina As Integer = 0
        kolicina = Convert.ToInt32(Request.Form("kolicina"))
        Dim n As Narudzba
        n = CType(Session("Narudzba"), Narudzba)

        If kolicina < 1 Then
            Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = ID.ToString)
            n.Artikli.Remove(postojeciArtikal)
        Else
            Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = ID.ToString)
            postojeciArtikal.Kolicina = kolicina
        End If
        

        UcitajKolicinuArtikala(n)
    End Sub

    Public Shared Sub UcitajKolicinuArtikala(ByVal n As Narudzba)

        Dim redci As String = Komponente.MojaKosaricaSession(n)
        HttpContext.Current.Response.Write(redci)

    End Sub

End Class