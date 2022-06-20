using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XDevkit;
using JRPC_Client;

namespace Classification_Tool_Recode
{
    public partial class Modern_Warfare_2 : Form
    {
        public Modern_Warfare_2()
        {
            InitializeComponent();
        }

        public IXboxConsole rgh;
        public bool connected = false;

        private void Modern_Warfare_2_Load(object sender, EventArgs e)
        {
            if (rgh.Connect(out rgh))
            {
                connected = true;
            }
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

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            guna2Panel4.BringToFront();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (rgh.XamGetCurrentTitleId() != 1096157207)
            {
                MessageBox.Show("You must be on MW2 before using any functions.", "Classification");
                return;
            }

            if (!connected)
                return;

            if (checkBox1.Checked)
            {
                rgh.SetMemory(0x82135be3, new byte[1]);
            }
            else
            {
                rgh.SetMemory(0x82135be3, new byte[] { 7 });
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (rgh.XamGetCurrentTitleId() != 1096157207)
            {
                MessageBox.Show("You must be on MW2 before using any functions.", "Classification");
                return;
            }

            if (!connected)
                return;

            if (checkBox2.Checked)
            {
                rgh.SetMemory(0x82127cff, new byte[] { 1 });
            }
            else
            {
                rgh.SetMemory(0x82127cff, new byte[1]);
            }
        }
    }
}
