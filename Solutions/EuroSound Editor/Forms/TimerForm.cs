using sb_editor.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class TimerForm : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public TimerForm()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_TimerForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(new MemoryStream(Properties.Resources.ChristmasTree));
            if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
            {
                TaskbarProgress.SetValue(Handle, 0, ProgressBar1.Maximum);
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
