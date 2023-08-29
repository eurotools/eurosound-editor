//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Available SFXs Panel
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Panels;
using System;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Frm_HashCodes : Form
    {
        private UserControl_MainForm_AvailableSFX controlData;
        private readonly MainForm mainForm = (MainForm)Application.OpenForms[nameof(MainForm)];

        //-------------------------------------------------------------------------------------------------------------------------------
        public Frm_HashCodes()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_HashCodes_Load(object sender, EventArgs e)
        {
            //Update title bar
            Text = mainForm.Text;

            //Add control
            controlData = mainForm.UserControl_Available_SFXs;
            Controls.Add(controlData);

            //Enable ReadOnly
            controlData.EnableReadOnly = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_HashCodes_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Remove control
            mainForm.SplitContainers_Lists2.Panel2.Controls.Add(controlData);
            Controls.Remove(controlData);

            //Disable ReadOnly Mode
            controlData.EnableReadOnly = false;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
