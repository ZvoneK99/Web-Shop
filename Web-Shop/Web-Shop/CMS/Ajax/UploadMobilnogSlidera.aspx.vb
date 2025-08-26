Imports System
Imports System.Web
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO

Public Class UploadMobilnogSlidera
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FolderTvrtke As String = Server.MapPath("~/Datoteke/Slider")
        If Not Directory.Exists(FolderTvrtke) Then
            Directory.CreateDirectory(FolderTvrtke)
        End If
        Dim fluFilePath As HttpPostedFile = Request.Files("fileInput")
        Dim BanerID As Integer = Request.Params("id")
        Dim FilePath As String = GetFileName(fluFilePath.FileName.ToLower.Replace(" ", "-"))
        Dim FullPath As String = Server.MapPath("~/Datoteke/Slider" & FilePath)
        Dim virtualnaPutanja As String = Server.MapPath("~/Datoteke/Slider/" & FilePath)
        'MsgBox(BanerID)
        fluFilePath.SaveAs(virtualnaPutanja)

        ZapisiUBazu(FilePath, BanerID)

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
        FileName = FileName.ToLower
        Return FileName.Substring(FileName.LastIndexOf("\") + 1)
    End Function

    Private Sub ZapisiUBazu(FilePath As String, BanerID As Integer)
        Dim IdLogiranog As Integer = Komponente.LogiraniKorisnikID()
        'Dim logiraniKorisnik As String = Komponente.LogiraniKorisnikIme(IdLogiranog)

        Dim putanja As String = Komponente.SQLKonekcija()
        Using konekcija As New SqlConnection(putanja)
            konekcija.Open()
            Using komanda As New SqlCommand()
                komanda.Connection = konekcija
                komanda.CommandType = CommandType.Text
                komanda.Parameters.AddWithValue("@DatotekaMob", FilePath)
                komanda.Parameters.AddWithValue("@BanerID", BanerID)

                komanda.CommandText = "UPDATE Slider SET SlikaMob=@DatotekaMob WHERE ID=@BanerID "
                komanda.ExecuteNonQuery()
                ' ZapisiLog.ZapisiLog(1, logiraniKorisnik, "***", "dodao datoteku &quot;" & FilePath & "&quot;", "add")
            End Using
        End Using

    End Sub

End Class