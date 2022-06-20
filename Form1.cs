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
using Guna;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace Classification_Tool_Recode
{
    public partial class Form1 : Form
    {

        public IXboxManager rghmanager;
        public IXboxConsole rgh;

        public bool connected = false;
        bool auto_connect = true;
        string file = @"C:\Classification\xex_paths.txt";

        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string replacelines = line.Replace(@"\\", @"\");
                    listBox1.Items.Add(replacelines);
                }
            }

            if (new FileInfo(file).Length != 0)
                listBox1.SelectedIndex = 0;

            if (auto_connect)
            {
                if (rgh.Connect(out rgh))
                {
                    connected = true;

                    label1.ForeColor = Color.FromArgb(45, 105, 67);

                    rgh.XNotify("[Classification] Welcome, " + Environment.MachineName);               

                    MessageBox.Show("Connected!", "Classification");

                    auto_connect = false;

                    Thread thread1 = new Thread(gettemps);
                    thread1.Start();
                }
                else
                {
                    connected = false;
                }
            }
        }

        private void gettemps()
        {
            if (connected)
            {
                while (true)
                {
                    label2.Invoke((MethodInvoker)(() => label2.Text = "CPU: " + rgh.GetTemperature(JRPC.TemperatureType.CPU) + "°"));
                    label3.Invoke((MethodInvoker)(() => label3.Text = "GPU: " + rgh.GetTemperature(JRPC.TemperatureType.GPU) + "°"));
                    label4.Invoke((MethodInvoker)(() => label4.Text = "RAM: " + rgh.GetTemperature(JRPC.TemperatureType.EDRAM) + "°"));

                    Thread.Sleep(100);
                }
            }
            else
            {
                label2.Text = "Not connected";
                label3.Text = "Not connected";
                label4.Text = "Not connected";
            }
        }

        private void guna2GradientButton11_Click_1(object sender, EventArgs e)
        {   
            auto_connect = true;
            Form1_Load(sender, e);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Black_Ops_2 bo2 = new Black_Ops_2();
            bo2.Show();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (!connected)
                return;

            rgh.XNotify(guna2TextBox1.Text);
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            guna2Panel1.BringToFront();
        }

        private void guna2GradientButton8_Click_1(object sender, EventArgs e)
        {
            string location1;
            location1 = "";

            string prompt = Interaction.InputBox("Type the directory of the XeX\nExample: \nHDD:\\Games\\COD_Black_Ops_II\\default_mp.xex", "Classification XeX launch", location1);

            if (prompt != "")
            {
                using (var newline = File.AppendText(file))
                {
                    if (new FileInfo(file).Length == 0)
                    {
                        newline.Write(prompt);
                    }
                    else
                        newline.Write("\n" + prompt);
                }
            }
        }

        private int _currSelIdx = -1;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == _currSelIdx)
                return;
        }

        private void guna2GradientButton12_Click(object sender, EventArgs e)
        {
            string a = listBox1.GetItemText(listBox1.SelectedItem.ToString());

            if (a == "")
                return;

            string b = "";

            if (a.Contains("default_mp.xex"))
                b = a.Replace(@"\default_mp.xex", "");
            if (a.Contains("default.xex"))
                b = a.Replace(@"\default.xex", "");
            if (a.Contains("default_zm.xex"))
                b = a.Replace(@"\default_zm.xex", "");

            rgh.Reboot(a, b, null, XboxRebootFlags.Title);

            MessageBox.Show("Launched the following:\n" + a + "\n" + b);
        }

        private void guna2GradientButton13_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string replacelines = line.Replace(@"\\", @"\");
                    listBox1.Items.Add(replacelines);
                }
            }

            if (new FileInfo(file).Length != 0)
                listBox1.SelectedIndex = 0;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copied Discord name to clipboard!");
            Clipboard.SetText("Desire2K17#7025");
        }

        private void guna2GradientButton20_Click(object sender, EventArgs e)
        {
            Skate_3 skate = new Skate_3();
            skate.Show();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();

            settings.Show();
        }

        private void guna2GradientButton3_Click_1(object sender, EventArgs e)
        {
            var random = new Random();
            int randomnumber = random.Next(int.MaxValue);

            string screenshot_file = @"C:\Classification\Screenshots\" + rgh.Name + "_" +randomnumber + ".bmp";
            rgh.ScreenShot(screenshot_file);

            Process.Start(screenshot_file);
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            Modern_Warfare_2 mw2 = new Modern_Warfare_2();
            mw2.Show();
        }
    }
}
