Imports Scripting

Friend Module GlobalVariables
    Friend ReadOnly fso As New FileSystemObject

    'Available languages
    Friend ReadOnly SfxLanguages As String() = New String() {"English", "American", "Japanese", "Danish", "Dutch", "Finnish", "French", "German", "Italian", "Norwegian", "Portuguese", "Spanish", "Swedish"}

    'Globals
    Friend DefaultLanguage As String = SfxLanguages(0)
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

    'Date and Text Formats
    Friend ReadOnly dateFormat As String = "yyyy/dd/MM HH:mm:ss"
    Friend ReadOnly filesDateFormat As String = "MM-dd-yyyy HH:mm:ss"
    Friend ReadOnly namesFormat As String = "^[a-zA-Z0-9_]*$"
End Module
