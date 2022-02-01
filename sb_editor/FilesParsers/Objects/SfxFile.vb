Public Class SfxFile
    Public Parameters As New SFXParameters
    Public Samples As New List(Of Sample)
    Public SamplePool As New SamplePoolControl
    Public HashCode As UInteger
End Class

Public Class SFXParameters
    Public ReverbSend As Integer
    Public TrackingType As Byte
    Public InnerRadius As Integer
    Public OuterRadius As Integer
    Public MaxVoices As Integer
    Public Action1 As Byte
    Public Priority As Integer
    Public Group As Integer
    Public Action2 As Byte
    Public Alertness As Integer
    Public IgnoreAge As Boolean
    Public Ducker As Integer
    Public DuckerLenght As Integer
    Public MasterVolume As Integer
    Public Outdoors As Boolean
    Public PauseInNis As Boolean
    Public StealOnAge As Boolean
    Public MusicType As Boolean
    Public Doppler As Boolean
End Class

Public Class Sample
    Public FilePath As String
    Public BaseVolume As SByte
    Public PitchOffset As Double
    Public RandomPitchOffset As Double
    Public RandomVolumeOffset As SByte
    Public Pan As SByte
    Public RandomPan As SByte
End Class

Public Class SamplePoolControl
    Public Action1 As Byte
    Public RandomPick As Boolean
    Public Shuffled As Boolean
    Public isLooped As Boolean
    Public Polyphonic As Boolean
    Public MinDelay As Integer
    Public MaxDelay As Integer
    Public EnableSubSFX As Boolean
End Class
