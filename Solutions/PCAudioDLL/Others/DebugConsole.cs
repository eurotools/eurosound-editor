using System;
using System.Windows.Forms;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class DebugConsole
    {
        public TextBox TxtConsole { get; set; }
        public bool PauseOutput { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void WriteLine(string message)
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
