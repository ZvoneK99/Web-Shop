Public Class Prijava
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.QueryString("a")) = False Then
            If Request.QueryString("a") = "logout" Then
                'ZapisiLog()
                Session("ValjanUser") = False
            End If
            Session.Clear()
            Response.Redirect("/login")
        End If
    End Sub

End Class