﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFX Form Debug Form
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class DebugForm : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public DebugForm(string[] dataToDisplay)
        {
            InitializeComponent();
            txtDebugData.Text = string.Join(Environment.NewLine, dataToDisplay);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
