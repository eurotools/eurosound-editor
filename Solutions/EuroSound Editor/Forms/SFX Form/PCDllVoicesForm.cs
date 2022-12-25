using PCAudioDLL;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class PCDllVoicesForm : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public PCDllVoicesForm()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void PCDllVoicesForm_Load(object sender, EventArgs e)
        {
            //Print items
            int totalItems = PCAudioDll.pcAudioVoices.Length;
            for (int i = 0; i < totalItems; i++)
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
                VoiceItem[] pcTool = PCAudioDLL.PCAudioDll.pcAudioVoices;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    VoiceItem currentVoice = pcTool[i];

                    try
                    {
                        dataGridView1.Invoke((MethodInvoker)delegate
                        {
                            DataGridViewRow itemToModify = dataGridView1.Rows[i];

                            //Check all properties
                            PropertyInfo[] propertiesToCheck = currentVoice.GetType().GetProperties();
                            for (int j = 0; j < propertiesToCheck.Length; j++)
                            {
                                PropertyInfo currentProperty = propertiesToCheck[j];
                                if (currentProperty.PropertyType == typeof(bool))
                                {
                                    //Update Boolean
                                    if ((bool)currentProperty.GetValue(currentVoice, null))
                                    {
                                        DataGridViewCellStyle style = new DataGridViewCellStyle
                                        {
                                            BackColor = Color.Blue
                                        };
                                        itemToModify.Cells[j + 1].Style = style;
                                    }
                                    else
                                    {
                                        DataGridViewCellStyle style = new DataGridViewCellStyle
                                        {
                                            BackColor = SystemColors.Window
                                        };
                                        itemToModify.Cells[j + 1].Style = style;
                                    }
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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
