Public Class ArtikliDodajStavku
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim n As Narudzba
        If IsNothing(Session("Narudzba")) = True Then
            n = New Narudzba
            Session("Narudzba") = n
        Else
            n = CType(Session("Narudzba"), Narudzba)
        End If
        'Nađi kontrole
        Dim ID As Integer = 0
        If Not Integer.TryParse(Request.Form("id"), ID) Then
            ' ako nema ID-a ili nije broj, prekini s radom
            Exit Sub
        End If

        Dim kolicina As Integer = 1
        Integer.TryParse(Request.Form("kolicina"), kolicina)

        kolicina = Request.Form("kolicina")
        If kolicina > 0 Then
            If IsNothing(n.Artikli.Find(Function(a As ArtikalSession) a.id = ID.ToString)) = True Then
                n.Artikli.Add(New ArtikalSession(ID.ToString, Komponente.NazivArtikal(ID), Komponente.CijenaArtika(ID), kolicina, 0, 0, 0))
            Else
                Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = ID.ToString)
                postojeciArtikal.Kolicina += kolicina
            End If
        End If
        UcitajKolicinuArtikala()
    End Sub

    Public Shared Sub UcitajKolicinuArtikala()
        Dim podaci As String = Komponente.PrebrojiArtikle()
        HttpContext.Current.Response.Write(podaci)
    End Sub

End Class