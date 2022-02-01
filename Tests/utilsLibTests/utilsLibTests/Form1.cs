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

        //IMPORT DLL
        [DllImport("EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BuildMusicFile(string soundMarkerFile, string soundSampleData, string filePath, uint hashcode, bool bigEndian);
        [DllImport("EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BuildStreamFile(string binFilePath, string lutFilePath, string outputFilePath, bool bigEndian);
        [DllImport("EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BuildSoundbankFile(string sfxFilePath, string sifFilePath, string sbFilePath, string ssFilePath, string outputFilePath, uint fileHashCode, bool bigEndian);

        private void button1_Click(object sender, EventArgs e)
        {
            string mainFolder = @"F:\Repositories\eurosound_project\OutputTests";

            //--------------------------------------------------[MUSIC TESTS]--------------------------------------------------
            //PC Test
            string outputDirPC = Path.Combine(mainFolder, "MusicFinalFilePC.SFX");
            string mainDirPC = Path.Combine(mainFolder, "InputFiles\\Musics\\PC");
            BuildMusicFile(Path.Combine(mainDirPC, "MFX_1.smf"), Path.Combine(mainDirPC, "MFX_1.ssd"), outputDirPC, 0, true);

            //GC Test
            string outputDirGC = Path.Combine(mainFolder, "MusicFinalFileGC.SFX");
            string mainDirGC = Path.Combine(mainFolder, "InputFiles\\Musics\\GC");
            BuildMusicFile(Path.Combine(mainDirGC, "MFX_1.smf"), Path.Combine(mainDirGC, "MFX_1.ssd"), outputDirGC, 0, true);

            //--------------------------------------------------[STREAM TESTS]--------------------------------------------------
            //PC Test
            outputDirPC = Path.Combine(mainFolder, "StreamFinalFilePC.SFX");
            mainDirPC = Path.Combine(mainFolder, "InputFiles\\Streams\\PC");
            BuildStreamFile(Path.Combine(mainDirPC, "STREAMS.bin"), Path.Combine(mainDirPC, "STREAMS.lut"), outputDirPC, false);

            //GC Test
            outputDirGC = Path.Combine(mainFolder, "StreamFinalFileGC.SFX");
            mainDirGC = Path.Combine(mainFolder, "InputFiles\\Streams\\GC");
            BuildStreamFile(Path.Combine(mainDirGC, "STREAMS.bin"), Path.Combine(mainDirGC, "STREAMS.lut"), outputDirGC, true);

            //--------------------------------------------------[SOUNDBANK TESTS]--------------------------------------------------
            //PC Test
            outputDirPC = Path.Combine(mainFolder, "SoundbankFinalFilePC.SFX");
            mainDirPC = Path.Combine(mainFolder, "InputFiles\\Soundbanks\\PC");
            BuildSoundbankFile(Path.Combine(mainDirPC, "1.sfx"), Path.Combine(mainDirPC, "1.sif"), Path.Combine(mainDirPC, "1.sbf"), "", outputDirPC, 1, false);

            //GC Test
            outputDirGC = Path.Combine(mainFolder, "SoundbankFinalFileGC.SFX");
            mainDirGC = Path.Combine(mainFolder, "InputFiles\\Soundbanks\\GC");
            BuildSoundbankFile(Path.Combine(mainDirGC, "1.sfx"), Path.Combine(mainDirGC, "1.sif"), Path.Combine(mainDirGC, "1.sbf"), Path.Combine(mainDirGC, "1.ssf"), outputDirGC, 1, true);
        }
    }
}
