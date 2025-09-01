Public Class PopuniKupcaSession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ime As String = Request.Form("ime")
        Dim mail As String = Request.Form("mail")
        Dim adresa As String = Request.Form("adresa")
        Dim zip As String = Request.Form("zip")
        Dim grad As String = Request.Form("grad")
        Dim tel As String = Request.Form("telefon")
        Dim napomena As String = Request.Form("napomena")

        Dim n As Narudzba
        n = CType(Session("Narudzba"), Narudzba)
        n.Ime = ime
        n.Adresa = adresa
        n.Mjesto = grad
        n.zip = zip
        n.Telefon = tel
        n.Mail = mail.Trim.ToLower
        n.Datum = DateTime.Now.Date
        n.Napomena = napomena
    End Sub

End Class