Imports System.IO
Imports sb_editor.ParsersObjects
Imports sb_editor.ReaderClasses

Namespace WritersClasses
    Partial Public Class FileWriters
        Friend Sub WriteSfxFile(sfxFileObj As SfxFile, textFilePath As String)
            'Replace current file   
            Dim headerLib As New FileParsers
            Dim headerData As FileHeader = GetFileHeaderData(textFilePath, headerLib)

            'Update file
            Using outputFile As New StreamWriter(textFilePath)
                outputFile.WriteLine("## EuroSound SFX File")
                outputFile.WriteLine("## First Created ... " & headerData.FirstCreated)
                outputFile.WriteLine("## Created By ... " & headerData.CreatedBy)
                outputFile.WriteLine("## Last Modified ... " & headerData.LastModify)
                outputFile.WriteLine("## Last Modified By ... " & headerData.LastModifyBy)
                outputFile.WriteLine("")
                outputFile.WriteLine("#SFXParameters")
                outputFile.WriteLine("ReverbSend  " & sfxFileObj.Parameters.ReverbSend)
                outputFile.WriteLine("TrackingType  " & sfxFileObj.Parameters.TrackingType)
                outputFile.WriteLine("InnerRadius  " & sfxFileObj.Parameters.InnerRadius)
                outputFile.WriteLine("OuterRadius  " & sfxFileObj.Parameters.OuterRadius)
                outputFile.WriteLine("MaxVoices " & sfxFileObj.Parameters.MaxVoices)
                outputFile.WriteLine("Action1  " & sfxFileObj.Parameters.Action1)
                outputFile.WriteLine("Priority  " & sfxFileObj.Parameters.Priority)
                outputFile.WriteLine("Group  " & 0)
                outputFile.WriteLine("Action2  " & 0)
                outputFile.WriteLine("Alertness  " & sfxFileObj.Parameters.Alertness)
                outputFile.WriteLine("IgnoreAge  " & If(sfxFileObj.Parameters.IgnoreAge, 1, 0))
                outputFile.WriteLine("Ducker  " & sfxFileObj.Parameters.Ducker)
                outputFile.WriteLine("DuckerLength  " & sfxFileObj.Parameters.DuckerLength)
                outputFile.WriteLine("MasterVolume  " & sfxFileObj.Parameters.MasterVolume)
                outputFile.WriteLine("Outdoors  " & If(sfxFileObj.Parameters.Outdoors, 1, 0))
                outputFile.WriteLine("PauseInNis  " & If(sfxFileObj.Parameters.PauseInNis, 1, 0))
                outputFile.WriteLine("StealOnAge  " & If(sfxFileObj.Parameters.StealOnAge, 1, 0))
                outputFile.WriteLine("MusicType  " & If(sfxFileObj.Parameters.MusicType, 1, 0))
                If sfxFileObj.Parameters.Doppler Then
                    outputFile.WriteLine("Doppler  " & If(sfxFileObj.Parameters.Doppler, 1, 0))
                End If
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")

                'Write SFXSamplePoolFiles
                outputFile.WriteLine("#SFXSamplePoolFiles")
                For Each sampleObj As Sample In sfxFileObj.Samples
                    outputFile.WriteLine(sampleObj.FilePath)
                Next
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")

                'Write SFXSamplePoolModes
                outputFile.WriteLine("#SFXSamplePoolModes")
                For Each sampleObj As Sample In sfxFileObj.Samples
                    outputFile.WriteLine("BaseVolume  " & sampleObj.BaseVolume)
                    outputFile.WriteLine("PitchOffset  " & sampleObj.PitchOffset)
                    outputFile.WriteLine("RandomPitchOffset  " & sampleObj.RandomPitchOffset)
                    outputFile.WriteLine("RandomVolumeOffset  " & sampleObj.RandomVolumeOffset)
                    outputFile.WriteLine("Pan  " & sampleObj.Pan)
                    outputFile.WriteLine("RandomPan  " & sampleObj.RandomPan)
                Next
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")

                'Write SFXSamplePoolControl
                outputFile.WriteLine("#SFXSamplePoolControl")
                outputFile.WriteLine("Action1  " & sfxFileObj.SamplePool.Action1)
                outputFile.WriteLine("RandomPick  " & If(sfxFileObj.SamplePool.RandomPick, 1, 0))
                outputFile.WriteLine("Shuffled  " & If(sfxFileObj.SamplePool.Shuffled, 1, 0))
                outputFile.WriteLine("Loop  " & If(sfxFileObj.SamplePool.isLooped, 1, 0))
                outputFile.WriteLine("Polyphonic  " & If(sfxFileObj.SamplePool.Polyphonic, 1, 0))
                outputFile.WriteLine("MinDelay  " & sfxFileObj.SamplePool.MinDelay)
                outputFile.WriteLine("MaxDelay  " & sfxFileObj.SamplePool.MaxDelay)
                outputFile.WriteLine("EnableSubSFX  " & If(sfxFileObj.SamplePool.EnableSubSFX, 1, 0))
                outputFile.WriteLine("EnableStereo  " & 0)
                outputFile.WriteLine("#END")
                outputFile.WriteLine("")

                'Write HASHCODE
                outputFile.WriteLine("#HASHCODE")
                outputFile.WriteLine("HashCodeNumber " & sfxFileObj.HashCode)
                outputFile.WriteLine("#END")
            End Using
        End Sub
    End Class
End Namespace