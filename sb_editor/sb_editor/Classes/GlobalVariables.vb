Imports Scripting

Friend Module GlobalVariables
    Friend ReadOnly fso As New FileSystemObject

    'Globals
    Friend DefaultLanguage As String = "English"
    Friend WorkingDirectory As String = ""
    Friend EuroSoundIniFilePath As String = Application.StartupPath & "\EuroSound.ini"
    Friend EuroSoundUser As String = ""

    'Project Settings
    Friend ProjMasterFolder As String = ""
    Friend ProjAudioEditor As String = ""
    Friend ProjOutHashCodesFolder As String = ""
    Friend ProjOutEngineXFolder As String = ""
    Friend ProjOutEuroLandServer As String = ""
    Friend ProjTextEditor As String = ""

    'SystemFiles
    Friend SysFileSamples As String = ""
    Friend SysFileProperties As String = ""
    Friend SysFileProjectIniPath As String = ""
    Friend SysFileSfxDefaults As String = ""

    'Hashcodes
    Friend SFXHashCodeNumber As UInteger = 0
    Friend SoundBankHashCodeNumber As UInteger = 0
    Friend MFXHashCodeNumber As UInteger = 0
    Friend ReSampleStreams As Byte = 0

    'Date Formats
    Friend ReadOnly dateFormat As String = "yyyy/dd/MM HH:mm:ss"
    Friend ReadOnly filesDateFormat As String = "MM-dd-yyyy HH:mm:ss"
End Module
