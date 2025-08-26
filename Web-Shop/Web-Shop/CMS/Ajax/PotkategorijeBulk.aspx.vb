Public Class PotkategorijeBulk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Clear()

        Dim KategorijaID As Integer = Request.Params("KategorijaID")
        Dim DisEna As String = Request.Params("DisEna")
        Dim opcije As String = igre_ba.CMS.ddlOdabranaPodKategorijaBulk(0, KategorijaID, DisEna)

        Response.Write(opcije)
        Response.End()
    End Sub

End Class