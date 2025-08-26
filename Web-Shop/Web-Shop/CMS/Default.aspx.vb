Public Class _Default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.QueryString("putanja")) = False Then
            Me.frmMain.Attributes.Add("action", "/CMS/Ajax/Autorizacija.aspx?putanja=" & Request.QueryString("putanja"))
        Else
            Me.frmMain.Attributes.Add("action", "/CMS/Ajax/Autorizacija.aspx")
        End If

        If IsNothing(Request.QueryString("a")) = False Then
            If Request.QueryString("a") = "logout" Then
                'ZapisiLog()
                Session("ValjanUser") = False
                Session.Clear()
            End If
            Response.Redirect("/CMS/")
        End If
    End Sub

End Class