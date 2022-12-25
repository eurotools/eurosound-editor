using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class PCGameDebugForm : Form
    {
        private readonly PCAudioDLL.PCAudioDll audioTool = ((MainForm)Application.OpenForms[nameof(MainForm)]).audioTool;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCGameDebugForm()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_TestSfxDebug_Shown(object sender, EventArgs e)
        {
            audioTool.InitializeConsole(txtDebugData);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkPauseDebug_Click(object sender, EventArgs e)
        {
            audioTool.DebugConsoleState(chkPauseDebug.Checked);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void PCGameDebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioTool.DebugConsoleState(true);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDebugData.Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
