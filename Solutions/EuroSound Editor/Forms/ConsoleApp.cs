using PCAudioDLL;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class ConsoleApp : Form
    {
        private readonly PCAudio pcDll = new PCAudio(0x1A000000);
        private ProjProperties projectSettings;
        private SoundPlayer audioPlayer;

        //-------------------------------------------------------------------------------------------------------------------------------
        public ConsoleApp()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            //Add available SoundBanks
            string soundbanksPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");
            if (Directory.Exists(soundbanksPath))
            {
                string[] dirFiles = Directory.GetFiles(soundbanksPath, "*.txt", SearchOption.AllDirectories);
                for (int i = 0; i < dirFiles.Length; i++)
                {
                    lstbAvailableSoundBanks.Items.Add(Path.GetFileNameWithoutExtension(dirFiles[i]));
                }
            }

            //Load project properties
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }

            //Reload SFXs folder
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                IniFile iniFile = new IniFile(systemIniFilePath);
                txtSoundBankFile.Text = iniFile.Read("OutputPath", "ConsoleApp");
                txtSoundDetails.Text = iniFile.Read("SoundDetails", "ConsoleApp");
                rbtn_PC.Checked = iniFile.Read("PlatformTypePC", "ConsoleApp").Equals("1");
                rbtn_PS2.Checked = iniFile.Read("PlatformTypePS2", "ConsoleApp").Equals("1");
                rbtn_GameCube.Checked = iniFile.Read("PlatformTypeGC", "ConsoleApp").Equals("1");
                rbtn_Xbox.Checked = iniFile.Read("PlatformTypeXB", "ConsoleApp").Equals("1");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawGrid(picBox_ZX, 30);
            DrawGrid(picBox_XY, 50, false, true);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ConsoleApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            BtnStopAllSFXs_Click(sender, e);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOkey_Click(object sender, EventArgs e)
        {
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkStreamingTest_CheckedChanged(object sender, EventArgs e)
        {
            //Load SFX File
            string sfxFilePath = Path.Combine(txtSoundBankFile.Text, string.Join(string.Empty, "HC00FFFF", ".SFX"));
            pcDll.LoadSoundBank(GetTestingPlatform(), sfxFilePath);
        }

        //-------------------------------------------------------------------------------------------
        //  Random Tests
        //-------------------------------------------------------------------------------------------
        private void BtnStartRandomTest_Click(object sender, EventArgs e)
        {
            //Stop any previous test that could be running
            BtnStopRandomTest_Click(sender, e);

            //Ensure that we have a soundbank loaded
            if (lstbLoadedSoundBanks.Items.Count == 1)
            {
                //Choose a random item
                Random random = new Random();
                lstBox_SFXs.SelectedIndex = random.Next(0, lstBox_SFXs.Items.Count);

                //Play
                BtnStart3dSound_Click(sender, e);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStopRandomTest_Click(object sender, EventArgs e)
        {
            pcDll.StopPlayer();
        }

        //-------------------------------------------------------------------------------------------
        //  SFX Play
        //-------------------------------------------------------------------------------------------
        private void BtnStartSFX_Click(object sender, EventArgs e)
        {
            pcDll.StartSound((uint)nudHashCode.Value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStart3dSound_Click(object sender, EventArgs e)
        {
            pcDll.StartSound3D((uint)nudHashCode.Value, new float[] { (float)nudX.Value, (float)nudY.Value, (float)nudZ.Value }, false, chxTestPan.Checked, trckBarMasterVolume.Value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStopSFX_Click(object sender, EventArgs e)
        {
            pcDll.StopHashCode((uint)nudHashCode.Value);
        }

        //-------------------------------------------------------------------------------------------
        //  Sample Play
        //-------------------------------------------------------------------------------------------
        private void BtnPlaySample_Click(object sender, EventArgs e)
        {
            if (lstbSamples.SelectedItem != null)
            {
                string samplePath = Path.Combine(projectSettings.SampleFilesFolder, "Master", lstbSamples.SelectedItem.ToString().TrimStart('\\'));
                if (File.Exists(samplePath))
                {
                    //Create a new SoundPlayer object with the full file path of the selected sample
                    audioPlayer = new SoundPlayer(samplePath);
                    audioPlayer.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        //  Play SFX
        //-------------------------------------------------------------------------------------------
        private void LstBox_SFXs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBox_SFXs.SelectedItems.Count > 0)
            {
                string sfxPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", lstBox_SFXs.SelectedItems[0].ToString() + ".txt");
                if (File.Exists(sfxPath))
                {
                    Objects.SFX sfxData = TextFiles.ReadSfxFile(sfxPath);

                    //Set Samples
                    lstbSamples.BeginUpdate();
                    lstbSamples.Items.Clear();
                    foreach (SfxSample sfxSample in sfxData.Samples)
                    {
                        lstbSamples.Items.Add(sfxSample.FilePath);
                    }
                    lstbSamples.EndUpdate();

                    //Set SFX data for being played
                    nudInnerRadius.Value = Math.Min(Math.Max(sfxData.Parameters.InnerRadius, 0), 500);
                    nudOuterRadius.Value = Math.Min(Math.Max(sfxData.Parameters.OuterRadius, 0), 500);
                    nudHashCode.Value = 0x1A000000 + sfxData.HashCode;
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        //  SoundBanks Control
        //-------------------------------------------------------------------------------------------
        private void BtnLoadSoundbanks_Click(object sender, EventArgs e)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();

            //Add items to the selected database
            for (int i = 0; i < lstbAvailableSoundBanks.SelectedItems.Count; i++)
            {
                if (!lstbLoadedSoundBanks.Items.Contains(lstbAvailableSoundBanks.SelectedItems[i]))
                {
                    lstbLoadedSoundBanks.Items.Add(lstbAvailableSoundBanks.SelectedItems[i]);

                    //Load SoundBank Text File
                    string sbPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", lstbAvailableSoundBanks.SelectedItems[i].ToString() + ".txt");
                    SoundBank soundBankData = TextFiles.ReadSoundbankFile(sbPath);

                    //Get SFXs
                    string[] SFXs = sbFunctions.GetSFXs(soundBankData.DataBases, GetTestingPlatform());
                    lstBox_SFXs.BeginUpdate();
                    lstBox_SFXs.Items.Clear();
                    lstBox_SFXs.Items.AddRange(SFXs);
                    lstBox_SFXs.EndUpdate();

                    //Load SFX File
                    string sfxFilePath = Path.Combine(txtSoundBankFile.Text, string.Join(string.Empty, "HC", soundBankData.HashCode.ToString("X6"), ".SFX"));
                    if (File.Exists(sfxFilePath))
                    {
                        pcDll.LoadSoundBank(GetTestingPlatform(), sfxFilePath);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} was not found.", sfxFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDeLoadSoundBanks_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstbLoadedSoundBanks.SelectedIndex;

            //Remove selected items
            for (int i = lstbLoadedSoundBanks.SelectedItems.Count - 1; i >= 0; i--)
            {
                //Load SoundBank Text File
                string sbPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", lstbAvailableSoundBanks.SelectedItems[i].ToString() + ".txt");
                SoundBank soundBankData = TextFiles.ReadSoundbankFile(sbPath);

                //Unload SoundBank
                pcDll.UnloadSoundbank(soundBankData.HashCode);
                
                //Remove item from list
                lstbLoadedSoundBanks.Items.Remove(lstbLoadedSoundBanks.SelectedItems[i]);
            }

            //Select next item
            if (selectedIndex < lstbLoadedSoundBanks.Items.Count)
            {
                lstbLoadedSoundBanks.SelectedIndex = selectedIndex;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnResetPos_Click(object sender, EventArgs e)
        {
            nudX.Value = 0;
            nudY.Value = 0;
            nudZ.Value = 0;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnTestLeft_Click(object sender, EventArgs e)
        {
            nudX.Value -= 10;
            nudY.Value = 0;
            nudZ.Value = 0;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnTestRight_Click(object sender, EventArgs e)
        {
            nudX.Value += 10;
            nudY.Value = 0;
            nudZ.Value = 0;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChxDrawCircle_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePictureBoxes(sender, e);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdatePictureBoxes(object sender, EventArgs e)
        {
            int innerRadius = (int)(nudInnerRadius.Value * nudScale.Value);
            int outerRadius = (int)(nudOuterRadius.Value * nudScale.Value);

            //Up view
            DrawGrid(picBox_ZX, 30);
            DrawEllipse(picBox_ZX, new Pen(Color.Green, 2), innerRadius);
            DrawEllipse(picBox_ZX, new Pen(Color.Red, 2), outerRadius);
            DrawListener(picBox_ZX, (int)nudX.Value, (int)nudZ.Value);

            //Lateral view
            DrawGrid(picBox_XY, 50, false, true);
            DrawEllipse(picBox_XY, new Pen(Color.Green, 2), innerRadius / 3, !chxDrawCircle.Checked);
            DrawEllipse(picBox_XY, new Pen(Color.Red, 2), outerRadius / 3, !chxDrawCircle.Checked);
            DrawListener(picBox_XY, (int)nudX.Value / 3, (int)nudY.Value);
        }

        //-------------------------------------------------------------------------------------------
        //  Platform Type
        //-------------------------------------------------------------------------------------------
        private void Rbtn_PC_CheckedChanged(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("PlatformTypePC", rbtn_PC.Checked ? "1" : "0", "ConsoleApp");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Rbtn_PS2_CheckedChanged(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("PlatformTypePS2", rbtn_PS2.Checked ? "1" : "0", "ConsoleApp");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Rbtn_GameCube_CheckedChanged(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("PlatformTypeGC", rbtn_GameCube.Checked ? "1" : "0", "ConsoleApp");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Rbtn_Xbox_CheckedChanged(object sender, EventArgs e)
        {
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("PlatformTypeXB", rbtn_Xbox.Checked ? "1" : "0", "ConsoleApp");
        }

        //-------------------------------------------------------------------------------------------
        //  Misc Module Commands
        //-------------------------------------------------------------------------------------------
        private void BtnStopAllSFXs_Click(object sender, EventArgs e)
        {
            pcDll.StopPlayer();
            if (audioPlayer != null)
            {
                audioPlayer.Stop();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSfxReset_Click(object sender, EventArgs e)
        {
            BtnLoadSoundbanks_Click(sender, e);
            BtnResetPos_Click(sender, e);
        }

        //-------------------------------------------------------------------------------------------
        //  Platform Output
        //-------------------------------------------------------------------------------------------
        private void TxtSoundBankFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Restore selected path
            if (Directory.Exists(txtSoundBankFile.Text))
            {
                folderBrowserDialog.SelectedPath = txtSoundBankFile.Text;
            }

            //Open folder browser
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtSoundBankFile.Text = folderBrowserDialog.SelectedPath;

                //Save State
                IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
                iniFile.Write("OutputPath", txtSoundBankFile.Text, "ConsoleApp");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSearchOutPath_Click(object sender, EventArgs e)
        {
            TxtSoundBankFile_MouseDoubleClick(sender, null);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void txtSoundDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Restore selected path
            if (!string.IsNullOrEmpty(txtSoundDetails.Text))
            {
                string fileDir = Path.GetDirectoryName(txtSoundDetails.Text);
                if (Directory.Exists(fileDir))
                {
                    openFileDialog.InitialDirectory = fileDir;
                }
            }

            //Open folder browser
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSoundDetails.Text = openFileDialog.FileName;

                //Save State
                IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
                iniFile.Write("SoundDetails", txtSoundDetails.Text, "ConsoleApp");

                //Read File
                if (File.Exists(txtSoundDetails.Text))
                {
                    pcDll.LoadSoundBank(GetTestingPlatform(), txtSoundDetails.Text);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void btnSearchSoundDetails_Click(object sender, EventArgs e)
        {
            txtSoundDetails_MouseDoubleClick(sender, null);
        }

        //-------------------------------------------------------------------------------------------
        //  Draw Methods
        //-------------------------------------------------------------------------------------------
        private void DrawGrid(PictureBox pictureBox, int gridSize, bool subGrid = true, bool drawOnlyXAxis = false)
        {
            // Obtener el objeto Graphics para dibujar en el PictureBox
            using (Graphics g = pictureBox.CreateGraphics())
            {
                //Reset picturebox
                pictureBox.Invalidate();
                pictureBox.Update();

                //Get width and height
                int width = pictureBox.Width;
                int height = pictureBox.Height;

                // Draw subgrid
                if (subGrid)
                {
                    Pen subGridPen = new Pen(Color.LightGray);
                    for (int x = -width / 2; x <= width / 2; x += gridSize / 2)
                    {
                        g.DrawLine(subGridPen, width / 2 + x, 0, width / 2 + x, height);
                    }
                    for (int z = -height / 2; z <= height / 2; z += gridSize / 2)
                    {
                        g.DrawLine(subGridPen, 0, height / 2 + z, width, height / 2 + z);
                    }
                }

                // Draw main grid
                Pen gridPen = new Pen(Color.Black);
                for (int x = -width / 2; x <= width / 2; x += gridSize)
                {
                    g.DrawLine(gridPen, width / 2 + x, 0, width / 2 + x, height);
                }
                for (int z = -height / 2; z <= height / 2; z += gridSize)
                {
                    g.DrawLine(gridPen, 0, height / 2 + z, width, height / 2 + z);
                }

                // Draw Axis X i Z
                Pen axisPen = new Pen(Color.Black, 3);
                g.DrawLine(axisPen, 0, height / 2, width, height / 2);
                if (!drawOnlyXAxis)
                {
                    g.DrawLine(axisPen, width / 2, 0, width / 2, height);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void DrawEllipse(PictureBox picBoxControl, Pen penColor, int radius, bool horizontalView = false)
        {
            using (Graphics g = picBoxControl.CreateGraphics())
            {
                // Dibujar círculo rojo de radio 30
                int circleX = picBoxControl.Width / 2 - (radius / 2);
                if (horizontalView)
                {
                    int circleY = picBoxControl.Height / 2;
                    g.DrawEllipse(penColor, circleX, circleY, radius, 1);
                }
                else
                {
                    int circleY = picBoxControl.Height / 2 - (radius / 2);
                    g.DrawEllipse(penColor, circleX, circleY, radius, radius);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void DrawListener(PictureBox pictureBox, int centerX, int centerY)
        {
            // Obtener el objeto Graphics para dibujar en el PictureBox
            using (Graphics g = pictureBox.CreateGraphics())
            {
                // Definir el radio del círculo
                int radius = 10;

                // Calcular las coordenadas del centro del PictureBox
                int pictureBoxCenterX = pictureBox.Width / 2;
                int pictureBoxCenterY = pictureBox.Height / 2;

                // Ajustar las coordenadas del centro del círculo en función del centro del PictureBox
                int adjustedCenterX = pictureBoxCenterX + centerX;
                int adjustedCenterY = pictureBoxCenterY - centerY; // Invertir el eje Y para que crezca hacia arriba

                // Calcular las coordenadas del rectángulo circunscrito
                int x = adjustedCenterX - radius;
                int y = adjustedCenterY - radius;

                // Dibujar el círculo
                g.DrawEllipse(new Pen(Color.Lime, 2), x, y, 20, 20);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private string GetTestingPlatform()
        {
            string selPlatform = "PC";

            if (rbtn_GameCube.Checked)
            {
                selPlatform = "GC";
            }
            if (rbtn_PS2.Checked)
            {
                selPlatform = "PS2";
            }
            if (rbtn_Xbox.Checked)
            {
                selPlatform = "XB";
            }

            return selPlatform;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Btn_musicApp_Click(object sender, EventArgs e)
        {
            using (MusicApp musApp = new MusicApp())
            {
                musApp.ShowDialog();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
