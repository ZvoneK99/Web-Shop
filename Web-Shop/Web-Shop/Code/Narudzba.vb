Public Class Narudzba

    Private _ime As String
    Private _mail As String
    Private _datum As DateTime
    Private _adresa As String
    Private _mjesto As String
    Private _zip As String
    Private _telefon As String
    Private _napomena As String
    Private _artikli As List(Of ArtikalSession)

    Public Property Ime() As String
        Get
            Return _ime
        End Get
        Set(ByVal value As String)
            _ime = value.Trim
        End Set
    End Property

    Public Property Mail() As String
        Get
            Return _mail
        End Get
        Set(ByVal value As String)
            _mail = value.Trim
        End Set
    End Property

    Public Property Datum() As DateTime
        Get
            Return _datum
        End Get
        Set(ByVal value As DateTime)
            _datum = value
        End Set
    End Property

    Public Property Adresa() As String
        Get
            Return _adresa
        End Get
        Set(ByVal value As String)
            _adresa = value.Trim
        End Set
    End Property

    Public Property Mjesto() As String
        Get
            Return _mjesto
        End Get
        Set(ByVal value As String)
            _mjesto = value.Trim
        End Set
    End Property

    Public Property zip() As String
        Get
            Return _zip
        End Get
        Set(ByVal value As String)
            _zip = value
        End Set
    End Property

    Public Property Telefon() As String
        Get
            Return _telefon
        End Get
        Set(ByVal value As String)
            _telefon = value
        End Set
    End Property

    Public Property Napomena() As String
        Get
            Return _napomena
        End Get
        Set(ByVal value As String)
            _napomena = value
        End Set
    End Property

    Public Property Artikli() As List(Of ArtikalSession)
        Get
            Return _artikli
        End Get
        Set(ByVal value As List(Of ArtikalSession))
            _artikli = value
        End Set
    End Property

    Public ReadOnly Property Ukupno() As Decimal
        Get
            For Each Artikal In Me.Artikli
                Ukupno += Artikal.Iznos
            Next
        End Get
    End Property

    Public ReadOnly Property BrojArtikala() As Decimal
        Get
            For Each Artikal In Me.Artikli
                BrojArtikala += Artikal.Kolicina
            Next
        End Get
    End Property

    Public Sub New()
        _artikli = New List(Of ArtikalSession)
    End Sub

    Public Sub New(ByVal ime As String, ByVal mail As String, ByVal datum As DateTime, ByVal mjesto As String, ByVal zip As String, ByVal adresa As String, ByVal telefon As String)
        _ime = ime
        _mail = mail
        _datum = datum
        _mjesto = mjesto
        _zip = zip
        _adresa = adresa
        _telefon = telefon
        _napomena = Napomena
        _artikli = New List(Of ArtikalSession)
    End Sub

End Class