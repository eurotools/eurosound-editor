using System.Windows.Forms;

namespace sb_explorer
{
    public partial class SetPlatform : Form
    {
        public SetPlatform(string filePath)
        {
            InitializeComponent();
            Textbox_FilePath.Text = filePath;
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
