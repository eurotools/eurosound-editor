using System;
using System.Windows.Forms;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class PCAudioDebugConsole
    {
        public static TextBox TxtConsole { get; set; }
        public static bool PauseOutput { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static void WriteLine(string message)
        {
            if (!PauseOutput && TxtConsole != null)
            {
                if (TxtConsole.InvokeRequired)
                {
                    TxtConsole.Invoke((MethodInvoker)delegate
                    {

                        TxtConsole.Text += message + Environment.NewLine;
                    });
                }
                else
                {
                    TxtConsole.Text += message + Environment.NewLine;
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
