Public Class ArtikliDodajStavku
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim n As Narudzba
        If IsNothing(Session("IgreNarudzba")) = True Then
            n = New Narudzba
            Session("IgreNarudzba") = n
        Else
            n = CType(Session("IgreNarudzba"), Narudzba)
        End If
        'Nađi kontrole
        Dim ID As Integer = Request.Form("id")
        Dim kolicina As Integer = 1
        kolicina = Request.Form("kolicina")
        ' Dim besplatnadostava As Boolean = Komponente.ProvjeraNaplateDostave(ID)
        If kolicina > 0 Then
            If IsNothing(n.Artikli.Find(Function(a As ArtikalSession) a.id = ID.ToString)) = True Then
                'n.Artikli.Add(New ArtikalSession(ID.ToString, Komponente.DostavaDaNe(ID), Komponente.NazivArtikal(ID), Komponente.CijenaArtika(ID), kolicina, 0, 0))
                n.Artikli.Add(New ArtikalSession(ID.ToString, Komponente.NazivArtikal(ID), Komponente.CijenaArtika(ID), kolicina, 0, 0, 0))
            Else
                Dim postojeciArtikal As ArtikalSession = n.Artikli.Find(Function(a As ArtikalSession) a.id = ID.ToString)
                postojeciArtikal.Kolicina += kolicina
                'If postojeciArtikal.Kolicina > Komponente.Postavke("MaxBrojGuma") Then
                '    postojeciArtikal.Kolicina = 8
                'End If
            End If
        End If
        UcitajKolicinuArtikala()
    End Sub

    Public Shared Sub UcitajKolicinuArtikala()
        Dim podaci As String = Komponente.PrebrojiArtikle()
        HttpContext.Current.Response.Write(podaci)
    End Sub

End Class