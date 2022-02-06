'-------------------------------------------------------------------------------------------------------------------------------
'  ______                                           _ 
' |  ____|                                         | |
' | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
' |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
' | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
' |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
'
'-------------------------------------------------------------------------------------------------------------------------------
' STARTUP OBJECT
'-------------------------------------------------------------------------------------------------------------------------------

Module Program
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        'Show Splash Screen
        Using ProgramSplash As New SplashScreen()
            ProgramSplash.ShowDialog()
        End Using

        'Start application
        Application.Run(New MainFrame)
    End Sub
End Module