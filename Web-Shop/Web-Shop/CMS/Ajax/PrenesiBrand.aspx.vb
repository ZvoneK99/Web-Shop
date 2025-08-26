Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class PrenesiBrand
    Inherits System.Web.UI.Page

    Private FileDeleted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        umetniSliku()
        Response.Redirect("/CMS/Partneri.aspx")
    End Sub

    Private Sub umetniSliku()

        If Request.Files.Count > 0 Then
            'Dim zadanaSirina = "100" 'ConfigurationManager.AppSettings("ThumbWidth")
            Dim zadaniThumb = "150" 'ConfigurationManager.AppSettings("NovaSirinaSlike")
            Dim i As Integer
            Dim datoteke As New List(Of Datoteka)()
            Dim folderThumb As String = "/Datoteke/Brendovi/Thumb/"
            Dim folderPreview As String = "/Datoteke/Brendovi/"
            Dim folderThumbServer As String = Server.MapPath(String.Format("~{0}", folderThumb))
            Dim folderPreviewServer As String = Server.MapPath(String.Format("~{0}", folderPreview))

            For i = 0 To Request.Files.Count - 1
                Dim datoteka As HttpPostedFile = Request.Files(i)
                Dim orginalNaziv As String = OcistiNazivDatotekeOrginal(datoteka.FileName)
                Dim naziv As String = OcistiNazivDatoteke(datoteka.FileName)
                Dim putanjaThumb As String = String.Format("{0}{1}", folderThumbServer, naziv)
                Dim putanjaPreview As String = String.Format("{0}{1}", folderPreviewServer, naziv)
                Dim velicina As Single = datoteka.ContentLength / 1024


                If velicina > 0 Then
                    Dim tip As String = datoteka.ContentType.ToLower()
                    If DatotekaJeSlika(tip) = True Then
                        Using image As Image = image.FromStream(datoteka.InputStream)
                            Dim sirina As Integer = image.Width
                            Dim visina As Integer = image.Height
                            'Dim autor As String = Encoding.UTF8.GetString(image.PropertyItems(1).Value)
                            'Thumb
                            'Dim maxThumb As Integer = zadanaSirina
                            Dim maxPreview As Integer = zadaniThumb
                            Dim izlazno As Size
                            If sirina > visina Then
                                'izlazno = Thumb(image, maxThumb, 0, putanjaThumb)
                                Thumb(image, maxPreview, 0, putanjaPreview)
                            Else
                                'izlazno = Thumb(image, 0, maxThumb, putanjaThumb)
                                Thumb(image, 0, maxPreview, putanjaPreview)
                            End If
                            'Dodaj u listu
                            Dim zapis As New Datoteka()
                            zapis.PutanjaOriginal = String.Format("{0}{1}", folderThumb, naziv)
                            zapis.PutanjaPreview = String.Format("{0}{1}", folderPreview, naziv)
                            'zapis.Autor = autor
                            zapis.Tip = tip
                            zapis.Velicina = CInt(velicina)
                            zapis.Sirina = CInt(izlazno.Width)
                            zapis.Visina = CInt(izlazno.Height)
                            datoteke.Add(zapis)
                            SpremuuBazu(naziv, orginalNaziv)
                            'Me.dslSlike.DataBind()
                        End Using
                    End If
                End If
            Next
        End If

    End Sub

    Private Sub SpremuuBazu(naziv As String, orginalNaziv As String)
        Dim konekcija As SqlConnection
        konekcija = New SqlConnection(Komponente.conekcija())
        Dim InsertKomanda As New SqlCommand
        InsertKomanda.Connection = konekcija
        InsertKomanda.CommandText = "UPDATE Brendovi SET Prioritet=Prioritet+1;INSERT INTO Brendovi (Brend, Datoteka, Prioritet, Aktivno, Link) Values ('0', '" & naziv & "', '1', '1', '#')"
        konekcija.Open()
        InsertKomanda.ExecuteNonQuery()
        konekcija.Close()
    End Sub

    Private Function OcistiNazivDatoteke(naziv As String) As String
        naziv = naziv.ToLower()
        naziv = naziv.Replace(" ", "_")
        naziv = naziv.Replace("š", "s")
        naziv = naziv.Replace("đ", "dj")
        naziv = naziv.Replace("ž", "z")
        naziv = naziv.Replace("č", "c")
        naziv = naziv.Replace("ć", "c")
        naziv = naziv.Replace("+", "")
        Return naziv
    End Function

    Private Function OcistiNazivDatotekeOrginal(naziv As String) As String
        naziv = naziv.ToLower()
        naziv = naziv.Replace(" ", "_")
        naziv = naziv.Replace("š", "s")
        naziv = naziv.Replace("đ", "dj")
        naziv = naziv.Replace("ž", "z")
        naziv = naziv.Replace("č", "c")
        naziv = naziv.Replace("ć", "c")
        naziv = naziv.Replace("+", "")
        Return naziv
    End Function

    Private Function DatotekaJeSlika(tip As String) As Boolean
        Dim istina As Boolean = False
        Select Case tip
            Case "image/jpeg"
                istina = True
                Exit Select
            Case "image/png"
                istina = True
                Exit Select
            Case "image/gif"
                istina = True
                Exit Select
        End Select
        Return istina
    End Function

    Public Shared Function Thumb(sirina As Integer, visina As Integer, putanja As String, selekcija As Rectangle) As Size
        Dim image__1 As Image = Image.FromFile(putanja)
        Return Thumb(image__1, sirina, visina, putanja, selekcija)
    End Function

    Private Shared Function Thumb(image As Image, sirina As Integer, visina As Integer, putanja As String) As Size
        Return Thumb(image, sirina, visina, putanja, New Rectangle())
    End Function

    Private Shared Function Thumb(image As Image, sirina As Integer, visina As Integer, putanja As String, selekcija As Rectangle) As Size
        'Definiraj povratnu veličinu
        Dim velicina As New Size()

        'Crop
        'If selekcija.Width > 0 Then
        '    image = Crop(image, selekcija)
        'End If

        'Računaj širinu i visinu    
        Dim izvornaSirina As Integer = image.Width
        Dim izvornaVisina As Integer = image.Height

        'Računaj visinu - proslijeđena je širina
        If sirina > 0 AndAlso visina = 0 Then
            visina = CInt(CSng(izvornaVisina) * CSng(sirina) / CSng(izvornaSirina))
        End If

        'Računaj širinu - proslijeđena je visina
        If sirina = 0 AndAlso visina > 0 Then
            sirina = CInt(CSng(izvornaSirina) * CSng(visina) / CSng(izvornaVisina))
        End If

        'Postavi povratnu veličinu
        velicina.Width = sirina
        velicina.Height = visina

        'Crtaj bitmap
        Using bitmap As New Bitmap(sirina, visina)
            Using graphics As Graphics = graphics.FromImage(bitmap)
                Dim odrediste As New Rectangle(0, 0, sirina, visina)
                Dim izvor As New Rectangle(0, 0, izvornaSirina, izvornaVisina)
                graphics.DrawImage(image, odrediste, izvor, GraphicsUnit.Pixel)

                If selekcija.Width > 0 Then
                    putanja = putanja.Replace("/Datoteke/Brendovi/", String.Format("/Datoteke/Brendovi/{0}x{1}", sirina, visina))
                    Dim putanjaFoldera As String = putanja.Substring(0, putanja.LastIndexOf("\"))
                    If Directory.Exists(putanjaFoldera) = False Then
                        Directory.CreateDirectory(putanjaFoldera)
                    End If
                    bitmap.Save(putanja)
                Else
                    bitmap.Save(putanja)
                End If
            End Using
        End Using

        Return velicina
    End Function

    Public Class Datoteka
        Public Property PutanjaOriginal() As String
            Get
                Return m_PutanjaOriginal
            End Get
            Set(value As String)
                m_PutanjaOriginal = value
            End Set
        End Property
        Private m_PutanjaOriginal As String
        Public Property PutanjaPreview() As String
            Get
                Return m_PutanjaPreview
            End Get
            Set(value As String)
                m_PutanjaPreview = value
            End Set
        End Property
        Private m_PutanjaPreview As String
        Public Property Tip() As String
            Get
                Return m_Tip
            End Get
            Set(value As String)
                m_Tip = value
            End Set
        End Property
        Private m_Tip As String
        Public Property Velicina() As Integer
            Get
                Return m_Velicina
            End Get
            Set(value As Integer)
                m_Velicina = value
            End Set
        End Property
        Private m_Velicina As Integer
        Public Property Sirina() As Integer
            Get
                Return m_Sirina
            End Get
            Set(value As Integer)
                m_Sirina = value
            End Set
        End Property
        Private m_Sirina As Integer
        Public Property Visina() As Integer
            Get
                Return m_Visina
            End Get
            Set(value As Integer)
                m_Visina = value
            End Set
        End Property
        Private m_Visina As Integer
        Public Property Autor() As String
            Get
                Return m_Autor
            End Get
            Set(value As String)
                m_Autor = value
            End Set
        End Property
        Private m_Autor As String
    End Class

End Class