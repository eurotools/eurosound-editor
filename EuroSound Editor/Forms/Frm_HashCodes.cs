using EuroSound_Editor.Panels;
using System;
using System.Windows.Forms;

namespace EuroSound_Editor.Forms
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
