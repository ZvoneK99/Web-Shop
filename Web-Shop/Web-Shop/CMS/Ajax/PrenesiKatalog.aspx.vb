Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class PrenesiKatalog
    Inherits System.Web.UI.Page

    Private FileDeleted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fluFilePath As HttpPostedFile = Request.Files("file-input")
        'Dim ClanakID As Integer = Request.Params("id")
        Dim FilePath As String = GetFileName(fluFilePath.FileName.Replace(" ", "-"))
        Dim FullPath As String = Server.MapPath("~/Datoteke/Katalozi/" & FilePath)
        'Dim datumSesije = Date.UtcNow()
        Dim formatDatoteke As String = Path.GetExtension(FullPath)
        'Dim virtualnaPutanja As String = Server.MapPath(FolderTvrtke & "/" & FilePath)
        Dim OrginalNaziv As String = Path.GetFileNameWithoutExtension(fluFilePath.FileName)
        'Dim PrepravljeniOrginalNaziv As String = GetFileName(OrginalNaziv.ToLower.Replace(" ", "-"))

        fluFilePath.SaveAs(FullPath)

        ZapisiUBazu(FilePath, OrginalNaziv)
        Response.Redirect("/CMS/KataloziCms.aspx")
    End Sub

    Private Function GetFileName(ByVal FileName As String) As String
        FileName = FileName.Replace("/", "\")
        FileName = FileName.Replace(" ", "_")
        FileName = FileName.Replace("š", "s")
        FileName = FileName.Replace("đ", "dj")
        FileName = FileName.Replace("ž", "z")
        FileName = FileName.Replace("č", "c")
        FileName = FileName.Replace("ć", "c")
        FileName = FileName.Replace("+", "")
        FileName = FileName.Replace("--", "-")
        FileName = FileName.ToLower
        Dim datumDatoteke As String = Format(DateAndTime.Now(), "yyyy-MM-dd-HH-mm_")
        Return datumDatoteke & FileName.Substring(FileName.LastIndexOf("\") + 1)
        'Return FileName.Substring(FileName.LastIndexOf("\") + 1)
    End Function

    Private Sub ZapisiUBazu(FilePath As String, OrgNazivDatoteke As String)

        Dim putanja As String = Komponente.conekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@Datoteka", FilePath)
                komanda.Parameters.AddWithValue("@OrgNazivDatoteke", OrgNazivDatoteke)
                komanda.Parameters.AddWithValue("@Datum", DateAndTime.Now())

                komanda.CommandText = "INSERT INTO Katalozi (Datum, Datoteka, Opis, OrgNazivDatoteke, Aktivno, Prioritet) Values (@Datum, @Datoteka, @OrgNazivDatoteke, @Datoteka, '1', '1') "
                komanda.ExecuteNonQuery()
            End Using
        End Using

    End Sub

End Class