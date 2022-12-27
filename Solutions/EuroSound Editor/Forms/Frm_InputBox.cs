using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Frm_InputBox : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public Frm_InputBox()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_InputBox_Shown(object sender, System.EventArgs e)
        {
            txtInputData.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_InputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && !string.IsNullOrEmpty(txtInputData.Text))
            {
                Match match = Regex.Match(txtInputData.Text, "^[a-zA-Z0-9_]*$");
                if (!match.Success)
                {
                    e.Cancel = true;
                    MessageBox.Show(string.Format("Label '{0}' uses invalid characters, only numbers, digits and underscore characters are allowed.", txtInputData.Text), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
