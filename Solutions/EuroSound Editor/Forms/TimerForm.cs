﻿using System;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class TimerForm : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public TimerForm()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_TimerForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(new MemoryStream(Properties.Resources.ChristmasTree));
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}