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