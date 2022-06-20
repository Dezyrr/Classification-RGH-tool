using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Classification_Tool_Recode
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            DebugConsole dbgcnsl = new DebugConsole();

            dbgcnsl.Show();
        }
    }
}
