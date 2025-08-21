Public Class ArtikalSession

    Private _id As String
    Private _naziv As String
    Private _jedCijena As Decimal
    Private _jedCijenaKartica As Decimal = 0
    Private _jedCijenaRate As Decimal = 0
    Private _jedCijenaB2B As Decimal = 0
    Private _kolicina As Int16
    Private _besplatnadostava As Boolean = 0

    Public Property id() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Public Property naziv() As String
        Get
            Return _naziv
        End Get
        Set(ByVal value As String)
            _naziv = value
        End Set
    End Property

    Public Property JedCijena() As Decimal
        Get
            Return _jedCijena
        End Get
        Set(ByVal value As Decimal)
            _jedCijena = value
        End Set
    End Property

    Public Property JedCijenaKartica() As Decimal
        Get
            Return _jedCijenaKartica
        End Get
        Set(ByVal value As Decimal)
            _jedCijenaKartica = value
        End Set
    End Property

    Public Property JedCijenaRatea() As Decimal
        Get
            Return _jedCijenaRate
        End Get
        Set(ByVal value As Decimal)
            _jedCijenaRate = value
        End Set
    End Property

    Public Property JedCijenaB2B() As Decimal
        Get
            Return _jedCijenaB2B
        End Get
        Set(ByVal value As Decimal)
            _jedCijenaB2B = value
        End Set
    End Property

    Public Property Kolicina() As Int16
        Get
            Return _kolicina
        End Get
        Set(ByVal value As Int16)
            _kolicina = value
        End Set
    End Property
    Public Property besplatnadostava() As Boolean
        Get
            Return _besplatnadostava
        End Get
        Set(ByVal value As Boolean)
            _besplatnadostava = value
        End Set
    End Property

    Public ReadOnly Property Iznos() As Decimal
        Get
            Return _jedCijena * _kolicina
        End Get
    End Property

    Public ReadOnly Property IznosKartica() As Decimal
        Get
            Return _jedCijenaKartica * _kolicina
        End Get
    End Property

    Public ReadOnly Property IznosRate() As Decimal
        Get
            Return _jedCijenaRate * _kolicina
        End Get
    End Property

    Public ReadOnly Property IznosB2B() As Decimal
        Get
            Return _jedCijenaB2B * _kolicina
        End Get
    End Property

    Public Sub New(ByVal id As String, ByVal naziv As String, ByVal jedCijena As Decimal, ByVal kolicina As Int16, ByVal jedCijenaKartica As Decimal, ByVal jedCijenaRate As Decimal, ByVal jedCijenaB2B As Decimal)
        _id = id
        _naziv = naziv
        _jedCijena = jedCijena
        _jedCijenaKartica = jedCijenaKartica
        _jedCijenaRate = jedCijenaRate
        _jedCijenaB2B = jedCijenaB2B
        _kolicina = kolicina
    End Sub

End Class
