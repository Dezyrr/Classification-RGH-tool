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
    public partial class Black_Ops_2 : Form
    {
        public Black_Ops_2()
        {
            InitializeComponent();
        }

        public IXboxConsole rgh;
        public bool connected = false;
        private bool isClicked;

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;
        public void raiseUpdate()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            args.Data = isClicked;
            UpdateEventHandler?.Invoke(this, args);
        }
        public class UpdateEventArgs : EventArgs
        {
            public bool Data { get; set; }
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

        private void Black_Ops_2_Load(object sender, EventArgs e)
        {
            label7.Text = "Call of Duty: Black Ops II - Multiplayer";

            if (rgh.Connect(out rgh))
            {
                connected = true;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            guna2Panel2.BringToFront();
            label7.Text = "Call of Duty: Black Ops II - Multiplayer";
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            guna2Panel5.BringToFront();
            label7.Text = "Call of Duty: Black Ops II - Zombies";
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            guna2Panel4.BringToFront();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            guna2Panel3.BringToFront();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            guna2Panel1.BringToFront();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton7_Click_1(object sender, EventArgs e)
        {
            guna2Panel7.BringToFront();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            guna2Panel8.BringToFront();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            guna2Panel6.BringToFront();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            if (checkBox1.Checked)
            {
                rgh.WriteByte(2183093119u, 1);
            }
            else
            {
                rgh.WriteByte(2183093119u, 0);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            if (checkBox2.Checked)
            {
                rgh.CallVoid(0x824015e0, new object[] { 0, "cg_fovscale 1.4" });
            }
            else
            {
                rgh.CallVoid(0x824015e0, new object[] { 0, "cg_fovscale 1" });
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            if (checkBox3.Checked)
            {
                rgh.CallVoid(2185237984u, new object[]
                {
                        0,
                        "party_connectToOthers 00; " +
                        "partyMigrate_disabled 01; " +
                        "sv_endGameIfISuck 0; " +
                        "badhost_endgameifisuck 0​; " +
                        "set party_gamestarttimelength 2; " +
                        "set party_pregamestarttimerlength 2; " +
                        "set party_timer 25"
                });
                rgh.XNotify("[Classification] Force Host Enabled!");
            }
            else
            {
                rgh.CallVoid(2185237984u, new object[]
                {
                        0,
                        "party_connectToOthers 01; " +
                        "partyMigrate_disabled 00"
                });
                rgh.XNotify("[Classification] Force Host Disabled!");
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            if (checkBox4.Checked)
            {
                rgh.SetMemory(2181054320U, new byte[18]);
                rgh.WriteBool(2210767803U, true);
            }
            else
            {
                rgh.SetMemory(2181054320U, new byte[18]);
                rgh.WriteBool(2210767803U, false);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            if (checkBox5.Checked)
            {
                rgh.WriteByte(0x821FC04C, new byte[] { 0x38, 0xc0, 0xff, 0xff });
            }
            else
            {
                rgh.WriteByte(0x821FC04C, new byte[] { 0x7f, 0xa6, 0xeb, 120 });
            }
        }

        private void guna2GradientButton31_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            JRPC.SetMemory(rgh, 0x843491A4, BitConverter.GetBytes(Convert.ToInt32(guna2TextBox1.Text)));
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            JRPC.SetMemory(rgh, 0x843491BC, BitConverter.GetBytes(Convert.ToInt32(guna2TextBox2.Text)));
        }

        private void guna2GradientButton11_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            JRPC.SetMemory(rgh, 0x84348D00, BitConverter.GetBytes(Convert.ToInt32(guna2TextBox4.Text)));
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            JRPC.SetMemory(rgh, 0x84348AD2, BitConverter.GetBytes(Convert.ToInt32(guna2TextBox3.Text)));
        }

        private void guna2GradientButton13_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            JRPC.SetMemory(rgh, 0x843492E2, BitConverter.GetBytes(Convert.ToInt32(guna2TextBox6.Text)));
        }

        private void guna2GradientButton12_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            JRPC.SetMemory(rgh, 0x84348D72, BitConverter.GetBytes(Convert.ToInt32(guna2TextBox5.Text)));
        }

        private void guna2GradientButton14_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            rgh.SetMemory(0x82C55D00, new byte[] { 0x7C, 0x83, 0x23, 0x78, 0x3D, 0x60, 0x82, 0xC5, 0x38, 0x8B, 0x5D, 0x60, 0x3D, 0x60, 0x82, 0x4A, 0x39, 0x6B, 0xDC, 0xA0, 0x38, 0xA0, 0x00, 0x20, 0x7D, 0x69, 0x03, 0xA6, 0x4E, 0x80, 0x04, 0x20 });
            rgh.SetMemory(0x8293D724, new byte[] { 0x3D, 0x60, 0x82, 0xC5, 0x39, 0x6B, 0x5D, 0x00, 0x7D, 0x69, 0x03, 0xA6, 0x4E, 0x80, 0x04, 0x20 });
            rgh.SetMemory(0x8259B6A7, new byte[] { 0x00 });
            rgh.SetMemory(0x822D1110, new byte[] { 0x40 });
            rgh.WriteString(0x82C55D60, guna2TextBox7.Text);
            rgh.WriteString(0x841E1B30, guna2TextBox7.Text);
        }

        private void guna2GradientButton15_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            rgh.WriteInt32(0x83551A10 + 0x55C8, Convert.ToInt32(guna2TextBox8.Text));
        }

        private void guna2GradientButton16_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1096157379)
            {
                MessageBox.Show("You must be on BO2 before using any functions.", "Classification");
                return;
            }

            isClicked = true;

            rgh.WriteUInt32(2186913372U, 1610612736U);
            rgh.WriteUInt32(2185854640U, 1610612736U);
            rgh.WriteUInt32(2185854768U, 1610612736U);
            rgh.WriteUInt32(2185854688U, 1610612736U);
            rgh.WriteUInt32(2185854664U, 1610612736U);
            rgh.WriteUInt32(2186909312U, 1610612736U);
            rgh.WriteUInt32(2186909296U, 1610612736U);
            rgh.WriteUInt32(2186909224U, 1610612736U);
            rgh.WriteUInt32(2186909260U, 1610612736U);
            rgh.WriteUInt32(2186909356U, 1610612736U);
            rgh.WriteUInt32(2186909364U, 1610612736U);
            rgh.WriteUInt32(2186909252U, 1610612736U);
            rgh.WriteUInt32(2186909260U, 1610612736U);
            rgh.WriteUInt32(2186909232U, 1610612736U);
            rgh.WriteUInt32(2186153452U, 960508700U);

            rgh.XNotify("[Classification] BO2 Ban Bypass Activated!");
        }
    }
}
