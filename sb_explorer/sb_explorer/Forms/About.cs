using System.Windows.Forms;

namespace sb_explorer
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
