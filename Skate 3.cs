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
    public partial class Skate_3 : Form
    {
        public Skate_3()
        {
            InitializeComponent();
        }

        public IXboxConsole rgh;
        public bool connected = false;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void Skate_3_Load(object sender, EventArgs e)
        {
            label7.Text = "Skate 3 - Skater";

            guna2Panel2.BringToFront();

            if (rgh.Connect(out rgh))
            {
                connected = true;
            }
        }

        private void guna2GradientButton31_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1161890022)
            {
                MessageBox.Show("You must be on Skate 3 before using any functions.", "Classification");
                return;
            }

            rgh.WriteString(2182918088U, guna2TextBox1.Text);
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox1.Text, "[^0-9]"))
            {
                guna2TextBox1.Text = guna2TextBox1.Text.Remove(guna2TextBox1.Text.Length - 1);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            label7.Text = "Skate 3 - Mods [Coming Soon]";
            guna2Panel2.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            label7.Text = "Skate 3 - Skater";
            guna2Panel2.BringToFront();
            guna2Panel2.Show();
        }

        private void guna2GradientButton21_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            if (rgh.XamGetCurrentTitleId() != 1161890022)
            {
                MessageBox.Show("You must be on Skate 3 before using any functions.", "Classification");
                return;
            }

            float R = 0f;
            float.TryParse(guna2TextBox5.Text, out R);
            float G = 0f;
            float.TryParse(guna2TextBox3.Text, out G);
            float B = 0f;
            float.TryParse(guna2TextBox4.Text, out B);

            if (this.guna2ComboBox1.Text == "Hat")
            {
                this.rgh.WriteFloat(1074855984U, R);
                this.rgh.WriteFloat(1074855988U, G);
                this.rgh.WriteFloat(1074855992U, B);
            }
            if (this.guna2ComboBox1.Text == "Shirt")
            {
                this.rgh.WriteFloat(1074856560U, R);
                this.rgh.WriteFloat(1074856564U, G);
                this.rgh.WriteFloat(1074856568U, B);
            }
            if (this.guna2ComboBox1.Text == "Pants")
            {
                this.rgh.WriteFloat(1074856752U, R);
                this.rgh.WriteFloat(1074856756U, G);
                this.rgh.WriteFloat(1074856760U, B);
            }
            if (this.guna2ComboBox1.Text == "Socks")
            {
                this.rgh.WriteFloat(1074856624U, R);
                this.rgh.WriteFloat(1074856628U, G);
                this.rgh.WriteFloat(1074856632U, B);
            }
            if (this.guna2ComboBox1.Text == "Body")
            {
                this.rgh.WriteFloat(1074856240U, R);
                this.rgh.WriteFloat(1074856244U, G);
                this.rgh.WriteFloat(1074856248U, B);
            }
            if (this.guna2ComboBox1.Text == "Hands")
            {
                this.rgh.WriteFloat(1074856176U, R);
                this.rgh.WriteFloat(1074856180U, G);
                this.rgh.WriteFloat(1074856184U, B);
            }
            if (this.guna2ComboBox1.Text == "Face")
            {
                this.rgh.WriteFloat(1074856112U, R);
                this.rgh.WriteFloat(1074856116U, G);
                this.rgh.WriteFloat(1074856120U, B);
            }
            if (this.guna2ComboBox1.Text == "Hair")
            {
                this.rgh.WriteFloat(1074856048U, R);
                this.rgh.WriteFloat(1074856052U, G);
                this.rgh.WriteFloat(1074856056U, B);
            }

            rgh.XNotify("[Classification] Successfully Set " + guna2ComboBox1.Text + " Color!");
        }
    }
}
