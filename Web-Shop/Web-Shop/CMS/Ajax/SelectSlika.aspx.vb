Imports System.Data.SqlClient

Public Class SelectSlika
    Inherits System.Web.UI.Page

    Public Shared Function SlikeHtml() As String
        Dim html As New StringBuilder()
        Dim putanja As String = Komponente.SQLKonekcija()
        Dim Pojam As String = HttpContext.Current.Request.QueryString("Pojam")

        Using konekcija As New SqlConnection(putanja)
            'Dim rabat As Integer = rabatKupca()
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.StoredProcedure
                komanda.CommandText = "SelectSlikaAutoComplete"
                komanda.Parameters.AddWithValue("@Pojam", Pojam)
                Using citac As SqlDataReader = komanda.ExecuteReader()
                    If citac IsNot Nothing Then
                        While citac.Read()
                            html.AppendFormat("<div class=""redak"" data-slikaid=""{0}"" data-naziv=""{1}"">", citac("ID"), citac("Datoteka"))
                            html.AppendFormat("<img src=""/thumb.ashx?i={0}&w=50&h=50"" />", citac("Datoteka"))
                            html.AppendFormat("<b>{0}</b>", citac("MetaTag"))
                            html.Append("<span class=""ocisti""></span>")
                            html.Append("</div>")
                        End While
                    End If
                End Using
            End Using
        End Using

        If html.ToString = "" Then
            html.Append("<div class=""redak"" data-korisniciid=""0"" style=""font-size:12px;""> ne postoji slika pod treženim pojmom</div>")
        End If

        Return html.ToString()
    End Function

End Class