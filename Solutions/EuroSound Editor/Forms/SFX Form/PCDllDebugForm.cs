using System;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class PCDllDebugForm : Form
    {
        private readonly PCAudioDLL.PCAudioDLL audioTool = ((MainForm)Application.OpenForms[nameof(MainForm)]).audioTool;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCDllDebugForm()
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
            audioTool.outputConsole.PauseOutput = chkPauseDebug.Checked;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void PCDllDebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioTool.outputConsole.TxtConsole = null;
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
