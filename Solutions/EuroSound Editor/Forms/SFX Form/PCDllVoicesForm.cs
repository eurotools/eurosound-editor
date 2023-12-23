//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// PCDLL Voices Form
//-------------------------------------------------------------------------------------------------------------------------------
using PCAudioDLL;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class PCDllVoicesForm : Form
    {
        private readonly PCAudio pcDllTester;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCDllVoicesForm(PCAudio pcDll)
        {
            InitializeComponent();
            pcDllTester = pcDll;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void PCDllVoicesForm_Load(object sender, EventArgs e)
        {
            //Print items
            for (int i = 0; i < pcDllTester.audioVoices.MixerTable.Length; i++)
            {
                dataGridView1.Rows.Add(new string[] { i.ToString(), "", "", "", "", "", "", "", "" });
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void PCDllVoicesForm_Shown(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(FillListView))
            {
                IsBackground = true
            };
            t.Start();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void FillListView()
        {
            while (!Disposing)
            {
                for (int i = 0; i < pcDllTester.audioVoices.MixerTable.Length; i++)
                {
                    ExVoice currentVoice = pcDllTester.audioVoices.MixerTable[i];
                    try
                    {
                        dataGridView1.Invoke((MethodInvoker)delegate
                        {
                            DataGridViewRow itemToModify = dataGridView1.Rows[i];
                            if (currentVoice != null)
                            {
                                SetItemState(itemToModify, currentVoice.Active, 1);
                                SetItemState(itemToModify, currentVoice.Played, 2);
                                SetItemState(itemToModify, currentVoice.Playing, 3);
                                SetItemState(itemToModify, currentVoice.Looping, 4);
                                SetItemState(itemToModify, currentVoice.Reverb, 5);
                                SetItemState(itemToModify, currentVoice.Stop_, 6);
                                SetItemState(itemToModify, currentVoice.Stopped, 7);
                                SetItemState(itemToModify, currentVoice.Locked, 8);
                            }
                            else
                            {
                                for (int j = 0; j < itemToModify.Cells.Count; j++)
                                {
                                    DataGridViewCellStyle style = new DataGridViewCellStyle
                                    {
                                        BackColor = SystemColors.Window
                                    };
                                    itemToModify.Cells[j].Style = style;
                                }
                            }
                        });
                    }
                    catch
                    {

                    }
                }
                Thread.Sleep(10);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SetItemState(DataGridViewRow rowToModify, bool status, int index)
        {
            //Update Boolean
            if (status)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle
                {
                    BackColor = Color.Blue
                };
                rowToModify.Cells[index].Style = style;
            }
            else
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle
                {
                    BackColor = SystemColors.Window
                };
                rowToModify.Cells[index].Style = style;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
