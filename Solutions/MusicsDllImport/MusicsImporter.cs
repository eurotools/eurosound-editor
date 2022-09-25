using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MusicsDllImport
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MusicsImporter : Form
    {
        private readonly Dictionary<string, Music> musicData = new Dictionary<string, Music>();

        //-------------------------------------------------------------------------------------------------------------------------------
        public MusicsImporter()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MusicsImporter_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If running cancel work
            if (backgroundWorker.IsBusy)
            {
                if (MessageBox.Show("Are you sure you wish to cancel the operation?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    backgroundWorker.CancelAsync();
                }
            }

            //Avoid Closing if running
            if (backgroundWorker.IsBusy || backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_DlcFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Set Folder For Output Music Files";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Textbox_DlcFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOutFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Set Folder For Input Music Files";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                TextBox_DestinationFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_Import_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TextBox_DestinationFolder.Text) && Directory.Exists(Textbox_DlcFolderPath.Text) && !backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
                Button_Import.Enabled = false;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AddMusicData();
            int currentIndex = 1;
            foreach (KeyValuePair<string, Music> musicToTest in musicData)
            {
                if (backgroundWorker.CancellationPending)
                {
                    break;
                }

                //Start Process
                string inputFilePath = Path.Combine(Textbox_DlcFolderPath.Text, musicToTest.Value.Name);
                string outputFilePath = Path.Combine(TextBox_DestinationFolder.Text, musicToTest.Key + ".wav");
                Process sox = new Process();
                sox.StartInfo.FileName = Path.Combine(Application.StartupPath, "SoX", "Sox.exe");
                sox.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" trim 0s {2}s", inputFilePath, outputFilePath, musicToTest.Value.endPos);
                sox.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                sox.StartInfo.CreateNoWindow = true;
                sox.Start();
                sox.WaitForExit();

                //Report progress
                backgroundWorker.ReportProgress((int)(decimal.Divide(currentIndex, musicData.Count) * 100));
                currentIndex++;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation cancelled by the user.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Finished!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void AddMusicData()
        {
            Music aby_council_int1 = new Music
            {
                Name = "(01) Abydos Council Interior (I).flac",
                endPos = 2182957
            };
            musicData.Add("Aby_Council_Int1", aby_council_int1);
            Music aby_council_int2 = new Music
            {
                Name = "(02) Abydos Council Interior (II).flac",
                endPos = 3113000
            };
            musicData.Add("Aby_Council_Int2", aby_council_int2);
            Music aby_juggler = new Music
            {
                Name = "(03) Abydos Juggler.flac",
                endPos = 4522831
            };
            musicData.Add("Aby_Juggler", aby_juggler);
            Music aby_main = new Music
            {
                Name = "(04) Abydos Main.flac",
                endPos = 5540963
            };
            musicData.Add("Aby_Main", aby_main);
            Music aby_north = new Music
            {
                Name = "(05) Abydos North.flac",
                endPos = 5096057
            };
            musicData.Add("Aby_North", aby_north);
            Music action1 = new Music
            {
                Name = "(06) Action (I).flac",
                endPos = 6099599
            };
            musicData.Add("Action1", action1);
            Music ambient1 = new Music
            {
                Name = "(07) Ambient (I).flac",
                endPos = 3697809
            };
            musicData.Add("Ambient1", ambient1);
            Music ambient2 = new Music
            {
                Name = "(08) Ambient (II).flac",
                endPos = 6436274
            };
            musicData.Add("Ambient2", ambient2);
            Music ambient3 = new Music
            {
                Name = "(09) Ambient (III).flac",
                endPos = 5912134
            };
            musicData.Add("Ambient3", ambient3);
            Music ambient4 = new Music
            {
                Name = "(10) Ambient (IV).flac",
                endPos = 5292057
            };
            musicData.Add("Ambient4", ambient4);
            Music boss1 = new Music
            {
                Name = "(11) Boss (I).flac",
                endPos = 5488033
            };
            musicData.Add("Boss1", boss1);
            Music boss2 = new Music
            {
                Name = "(12) Boss (II).flac",
                endPos = 3969054
            };
            musicData.Add("Boss2", boss2);
            Music boss3 = new Music
            {
                Name = "(13) Boss (III).flac",
                endPos = 2976807
            };
            musicData.Add("Boss3", boss3);
            Music boss4 = new Music
            {
                Name = "(14) Boss (IV).flac",
                endPos = 3100813
            };
            musicData.Add("Boss4", boss4);
            Music danger1 = new Music
            {
                Name = "(16) Danger (II).flac",
                endPos = 1396558
            };
            musicData.Add("Danger1", danger1);
            Music danger2 = new Music
            {
                Name = "(17) Danger (III).flac",
                endPos = 1894438
            };
            musicData.Add("Danger2", danger2);
            Music danger3 = new Music
            {
                Name = "(18) Military.flac",
                endPos = 1459921
            };
            musicData.Add("Danger3", danger3);
            Music military = new Music
            {
                Name = "(19) Minigame; Intro.flac",
                endPos = 1539547
            };
            musicData.Add("Military", military);
            Music minigame_intro = new Music
            {
                Name = "(20) Minigame; Pairs.flac",
                endPos = 2285005
            };
            musicData.Add("MiniGame_Intro", minigame_intro);
            Music minigame_pairs = new Music
            {
                Name = "(21) Minigame; Shoot.flac",
                endPos = 1528857
            };
            musicData.Add("MiniGame_Pairs", minigame_pairs);
            Music minigame_shoot = new Music
            {
                Name = "(22) Minigame; Simon.flac",
                endPos = 1058458
            };
            musicData.Add("MiniGame_Shoot", minigame_shoot);
            Music minigame_simon = new Music
            {
                Name = "(23) Minigame; Walls.flac",
                endPos = 1905178
            };
            musicData.Add("MiniGame_Simon", minigame_simon);
            Music minigame_walls = new Music
            {
                Name = "(24) Bat Mummy (I).flac",
                endPos = 3015602
            };
            musicData.Add("MiniGame_Walls", minigame_walls);
            Music mummy_bat1 = new Music
            {
                Name = "(25) Bat Mummy (II).flac",
                endPos = 1981995
            };
            musicData.Add("Mummy_Bat1", mummy_bat1);
            Music mummy_bat2 = new Music
            {
                Name = "(26) Bat Mummy (III).flac",
                endPos = 2230056
            };
            musicData.Add("Mummy_Bat2", mummy_bat2);
            Music mummy_bat3 = new Music
            {
                Name = "(27) Electric Mummy (I).flac",
                endPos = 1478177
            };
            musicData.Add("Mummy_Bat3", mummy_bat3);
            Music mummy_elec1 = new Music
            {
                Name = "(28) Electric Mummy (II).flac",
                endPos = 2101293
            };
            musicData.Add("Mummy_Elec1", mummy_elec1);
            Music mummy_elec2 = new Music
            {
                Name = "(29) Electric Mummy (III).flac",
                endPos = 2723882
            };
            musicData.Add("Mummy_Elec2", mummy_elec2);
            Music mummy_elec3 = new Music
            {
                Name = "(30) Fire Mummy (I).flac",
                endPos = 2490412
            };
            musicData.Add("Mummy_Elec3", mummy_elec3);
            Music mummy_fire1 = new Music
            {
                Name = "(31) Fire Mummy (II).flac",
                endPos = 2622642
            };
            musicData.Add("Mummy_Fire1", mummy_fire1);
            Music mummy_fire2 = new Music
            {
                Name = "(32) Fire Mummy (III).flac",
                endPos = 2247986
            };
            musicData.Add("Mummy_Fire2", mummy_fire2);
            Music mummy_fire3 = new Music
            {
                Name = "(33) Paper Mummy (I).flac",
                endPos = 2060660
            };
            musicData.Add("Mummy_Fire3", mummy_fire3);
            Music mummy_paper1 = new Music
            {
                Name = "(34) Paper Mummy (II).flac",
                endPos = 1596782
            };
            musicData.Add("Mummy_Paper1", mummy_paper1);
            Music mummy_paper2 = new Music
            {
                Name = "(35) Paper Mummy (III).flac",
                endPos = 1596783
            };
            musicData.Add("Mummy_Paper2", mummy_paper2);
            Music mummy_paper3 = new Music
            {
                Name = "(36) Smoke Mummy.flac",
                endPos = 1596782
            };
            musicData.Add("Mummy_Paper3", mummy_paper3);
            Music mummy_smoke = new Music
            {
                Name = "(37) Stone Mummy.flac",
                endPos = 2205022
            };
            musicData.Add("Mummy_Smoke", mummy_smoke);
            Music mummy_stone = new Music
            {
                Name = "(38) Nomads.flac",
                endPos = 1176026
            };
            musicData.Add("Mummy_Stone", mummy_stone);
            Music nomads = new Music
            {
                Name = "(39) Pause.flac",
                endPos = 4012033
            };
            musicData.Add("Nomads", nomads);
            Music pause = new Music
            {
                Name = "(40) Platform (I).flac",
                endPos = 4042761
            };
            musicData.Add("Pause", pause);
            Music platform1 = new Music
            {
                Name = "(41) Possess Dino.flac",
                endPos = 6132237
            };
            musicData.Add("Platform1", platform1);
            Music possess_dino = new Music
            {
                Name = "(42) Possess Critter.flac",
                endPos = 1587659
            };
            musicData.Add("Possess_Dino", possess_dino);
            Music possess_pokemon = new Music
            {
                Name = "(43) Puzzle (I).flac",
                endPos = 2560704
            };
            musicData.Add("Possess_Pokemon", possess_pokemon);
            Music puzzle1 = new Music
            {
                Name = "(44) Puzzle (II).flac",
                endPos = 2940007
            };
            musicData.Add("Puzzle1", puzzle1);
            Music puzzle2 = new Music
            {
                Name = "(45) Sakkara Main.flac",
                endPos = 2679550
            };
            musicData.Add("Puzzle2", puzzle2);
            Music sak_main = new Music
            {
                Name = "(46) Sneak (I) Jail.flac",
                endPos = 5639324
            };
            musicData.Add("Sak_Main", sak_main);
            Music silence = new Music
            {
                Name = "(47) Sneak (I) Jail Intro.flac",
                endPos = 441000
            };
            musicData.Add("Silence", silence);
            Music silence_loop = new Music
            {
                Name = "(48) Sneak (II) Puzzle.flac",
                endPos = 441000
            };
            musicData.Add("Silence_loop", silence_loop);
            Music sneak1_jail = new Music
            {
                Name = "(49) Sorrow.flac",
                endPos = 5526184
            };
            musicData.Add("Sneak1_Jail", sneak1_jail);
            Music sneak1_jail_intro = new Music
            {
                Name = "(50) Swim.flac",
                endPos = 936695
            };
            musicData.Add("Sneak1_Jail_Intro", sneak1_jail_intro);
            Music sneak2_puzzle = new Music
            {
                Name = "(51) Swim Danger.flac",
                endPos = 5001642
            };
            musicData.Add("Sneak2_Puzzle", sneak2_puzzle);
            Music sorrow = new Music
            {
                Name = "(52) Temple (I).flac",
                endPos = 1675859
            };
            musicData.Add("Sorrow", sorrow);
            Music swim = new Music
            {
                Name = "(53) Temple (II).flac",
                endPos = 4563995
            };
            musicData.Add("Swim", swim);
            Music swim_danger = new Music
            {
                Name = "(54) Title.flac",
                endPos = 3347600
            };
            musicData.Add("Swim_Danger", swim_danger);
            Music temple1 = new Music
            {
                Name = "(55) Title (End Sting).flac",
                endPos = 3137362
            };
            musicData.Add("Temple1", temple1);
            Music temple2 = new Music
            {
                Name = "(56) Nomad Outpost.flac",
                endPos = 2248480
            };
            musicData.Add("Temple2", temple2);
            Music title = new Music
            {
                Name = "(57) Trading Outpost.flac",
                endPos = 2813400
            };
            musicData.Add("Title", title);
            Music title_endsting = new Music
            {
                Name = "(58) Sunshrine.flac",
                endPos = 1249279
            };
            musicData.Add("Title_EndSting", title_endsting);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
