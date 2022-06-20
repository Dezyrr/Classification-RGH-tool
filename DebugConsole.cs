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
using System.Threading;
using System.Runtime.InteropServices;

namespace Classification_Tool_Recode
{
    public partial class DebugConsole : Form
    {
        public DebugConsole()
        {
            InitializeComponent();
        }

        public IXboxConsole rgh;

        public bool connected = false;


        [DllImport("user32.dll", EntryPoint = "HideCaret")]

        public static extern long HideCaret(IntPtr hwnd);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);

            if (richTextBox1.CanFocus)
                HideCaret(richTextBox1.Handle);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void ConsoleLine(string text)
        {
            richTextBox1.AppendText("> " + text + Environment.NewLine);
        }

        private void SelectTextColor(RichTextBox richText, string p, Color textColor)
        {
            string[] lines = richText.Lines;

            int lengthAll = richTextBox1.TextLength;
            int location = richTextBox1.Find(p);
            int length = lengthAll - location;

            foreach (string line in lines)
            {
                richTextBox1.Select(location, length);
                richText.SelectionColor = textColor;
                richTextBox1.DeselectAll();
            }
        }

        string XamUserGetGamertag()
        {
            return Encoding.ASCII.GetString(rgh.GetMemory(0x81AA28FC, 30)).Replace("\0", string.Empty);
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Enter)
            {
                ask_console(sender, e);
                richTextBox2.Clear();
            }
            */
        }

        private void ask_console(object sender, EventArgs e)
        {
            if (rgh.Connect(out rgh))
            {
                if (richTextBox2.Text.Contains("/help"))
                {
                    richTextBox1.Clear();
                    ConsoleLine("--- All avaliable commands ---");
                    ConsoleLine("/clear (clears the console)");
                    ConsoleLine("/ip (returns your xbox's ip)");
                    ConsoleLine("/kernal (returns your xbox's kernal version)");
                    ConsoleLine("/motherboard (returns your xbox motherboard)");
                    ConsoleLine("/profile (returns current signed in profile)");
                    ConsoleLine("/connected_name (returns xbox name)");
                    ConsoleLine("------------------------------");
                }
                else if (richTextBox2.Text.Contains("/clear"))
                {
                    richTextBox1.Clear();
                }
                else if (richTextBox2.Text.Contains("/ip"))
                {
                    ConsoleLine("Console IP -> " + rgh.XboxIP());
                }
                else if (richTextBox2.Text.Contains("/kernal"))
                {
                    ConsoleLine("Kernal version -> " + rgh.GetKernalVersion());

                    if (rgh.GetKernalVersion().ToString() == "17559")
                    {
                        ConsoleLine("You are on the latest kernal version!");
                        SelectTextColor(richTextBox1, "You are on the latest kernal version!", Color.Green);
                    }
                    else
                    {
                        ConsoleLine("Your kernal version is outdated!");
                        SelectTextColor(richTextBox1, "Your kernal version is outdated!", Color.Red);
                    }
                }
                else if (richTextBox2.Text.Contains("/motherboard"))
                {
                    ConsoleLine("Console motherboard -> " + rgh.ConsoleType());
                }
                else if (richTextBox2.Text.Contains("/profile"))
                {
                    if (XamUserGetGamertag() != "")
                        ConsoleLine("Signed in as -> " + XamUserGetGamertag());
                    else
                        ConsoleLine("Not signed in");
                }
                else if (richTextBox2.Text.Contains("/connected_name"))
                {
                    ConsoleLine("Connected to -> " + rgh.Name);
                }
                else
                {
                    ConsoleLine("Invalid command. Type '/help' for a list of commands.");
                }
            }
        }

        private void DebugConsole_Load(object sender, EventArgs e)
        {
            richTextBox1.HideSelection = false;

            Screen mainmonitor = Screen.AllScreens[0];
            Screen screen = Screen.PrimaryScreen;

            if (screen.WorkingArea.Right > mainmonitor.WorkingArea.Right)
                mainmonitor = screen;
            
            this.Left = mainmonitor.WorkingArea.Right - this.Width;
            this.Top = mainmonitor.WorkingArea.Bottom - this.Height;

            this.TopMost = true;

            if (rgh.Connect(out rgh))
            {
                connected = true;

                ConsoleLine("Connected to -> " + rgh.Name);

                ConsoleLine("Console IP -> " + rgh.XboxIP());

                ConsoleLine("Kernal version -> " + rgh.GetKernalVersion());

                if (rgh.GetKernalVersion().ToString() == "17559")
                {
                    ConsoleLine("You are on the latest kernal version!");
                    SelectTextColor(richTextBox1, "You are on the latest kernal version!", Color.Green);
                }
                else
                {
                    ConsoleLine("Your kernal version is outdated!");
                    SelectTextColor(richTextBox1, "Your kernal version is outdated!", Color.Red);
                }

                ConsoleLine("Console motherboard -> " + rgh.ConsoleType());

                ConsoleLine("CPU key -> " + rgh.GetCPUKey());

                ConsoleLine("Current title -> " + rgh.XamGetCurrentTitleId());

                if (XamUserGetGamertag() != "")
                    ConsoleLine("Signed in as -> " + XamUserGetGamertag());
                else
                    ConsoleLine("Not signed in");

                Thread thread1 = new Thread(currenttitle);
                thread1.Start();

                Thread thread2 = new Thread(currentsignedinaccount);
                thread2.Start();
            }
            else
            {
                connected = false;
                ConsoleLine("Failed to connect!");
                SelectTextColor(richTextBox1, "Failed to connect!", Color.Red);
            }
        }

        private void currenttitle()
        {
            if (connected)
            {
                uint current_title = rgh.XamGetCurrentTitleId();

                while (true)
                {
                    if (rgh.XamGetCurrentTitleId() != current_title)
                    {
                        if (current_title.ToString() != "0")
                        {
                            Thread.Sleep(3000);

                            richTextBox1.Invoke((MethodInvoker)(() =>
                            richTextBox1.AppendText("> Current title -> " + rgh.XamGetCurrentTitleId() + Environment.NewLine)));
                        }

                        current_title = rgh.XamGetCurrentTitleId();
                    }
                }
            }
        }

        private void currentsignedinaccount()
        {
            if (connected)
            {
                string current_gamertag = XamUserGetGamertag();

                while (true)
                {
                    if (XamUserGetGamertag() != current_gamertag)
                    {
                        if (XamUserGetGamertag() != "")
                        {
                            richTextBox1.Invoke((MethodInvoker)(() =>
                            richTextBox1.AppendText("> Signed in as -> " + XamUserGetGamertag() + Environment.NewLine)));
                        }
                        else
                        {
                            richTextBox1.Invoke((MethodInvoker)(() =>
                            richTextBox1.AppendText("> Signed out of -> " + current_gamertag + Environment.NewLine)));
                        }

                        current_gamertag = XamUserGetGamertag();
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Screen mainmonitor = Screen.AllScreens[0];
            Screen screen = Screen.PrimaryScreen;

            if (screen.WorkingArea.Right > mainmonitor.WorkingArea.Right)
                mainmonitor = screen;

            this.Left = mainmonitor.WorkingArea.Right - this.Width;
            this.Top = mainmonitor.WorkingArea.Bottom - this.Height;

            this.TopMost = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
