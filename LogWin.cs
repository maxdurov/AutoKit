using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoKit
{
    public partial class LogWin : Form
    {
        public RichTextBox logbox;
        public ProgressBar pbar;
        public Label countProg;
        public Button btnClose;
        public LogWin()
        {
            InitializeComponent();
            logbox = this.LogBox;
            pbar = this.pBar;
            countProg = this._countProg;
            btnClose = this._btnClose;

        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {
            logbox.SelectionStart = logbox.Text.Length; 
            logbox.ScrollToCaret();
        }
    }
}
