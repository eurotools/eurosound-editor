using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace utilsLibTests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("..\\..\\OutputFolder\\Debug\\SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BuildMusicFile(string soundMarkerFile, string soundSampleData, string filePath, uint hashcode, bool bigEndian);
        private void button1_Click(object sender, EventArgs e)
        {
            string mainDir = @"C:\Users\Jordi Martinez\Desktop\ES_Music\TempOutputFolder\PC\Music\MFX_0";
            string outputDir = Path.Combine(@"C:\Users\Jordi Martinez\Desktop\MusicTests", "FinalFile.SFX");
            BuildMusicFile(Path.Combine(mainDir, "MFX_0.smf"), Path.Combine(mainDir, "MFX_0.ssd"), outputDir, 0, true);
        }
    }
}
