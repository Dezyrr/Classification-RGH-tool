using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Classification_Tool_Recode
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();

            var random = new Random();

            var list = new List<string> 
            {
                "Earmuffs were invented by a 15 year old.",
                "Trained pigeons can differentiate between the paintings\nof Pablo Picasso and Claude Monet.",
                "The heads on Easter Island have bodies.",
                "Barbie's full name is Barbara Millicent Roberts.",
                "A group of pugs is called a grumble.",
                "Yoda was partly modeled after a photo of Albert Einstein.",
                "Ravens in captivity can learn to talk better than parrots.",
                "Movie trailers are called that for a reason:\nThey used to run after the movie.",
                "Pi is technically infinite.",
                "There are beaches in the Maldives that\nglow in the dark.",
                "IKEA consumes about 1 percent of the world's lumber.",
                "Goldfish can distinguish\nthe music of one composer from another.",
                "He was on the jet.",
                "A litter of kittens is also known as a “kindle.”",
                "Sloths may be the only mammals that don’t fart.",
                "You are an idiot.",
                "Desire was here.",
                "Yuri knew makarov."
            };

            int index = random.Next(list.Count);
            label5.Text = list[index];
        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  
            guna2ProgressBar1.Increment(1);

            string path = @"C:\Classification\";
            string path_screenshot = @"C:\Classification\Screenshots\";

            if (guna2ProgressBar1.Value < 15)
            {
                if (Directory.Exists(path))
                {
                    guna2ProgressBar1.Increment(3);
                    label3.Text = "Directory exists, speeding up...";
                }
                else if (Directory.Exists(path_screenshot))
                {
                    guna2ProgressBar1.Increment(3);
                    label3.Text = "Directory exists, speeding up...";
                }
                else
                {
                    guna2ProgressBar1.Increment(1);
                    Directory.CreateDirectory(path);
                    Directory.CreateDirectory(path_screenshot);
                    label3.Text = "Creating directory...";
                }
            }

            string file = @"C:\Classification\xex_paths.txt";

            if (guna2ProgressBar1.Value > 15 && guna2ProgressBar1.Value < 40)
            {
                if (File.Exists(file))
                {
                    guna2ProgressBar1.Increment(3);
                    label3.Text = "File exists, speeding up...";
                }
                else
                {
                    guna2ProgressBar1.Increment(1);
                    File.Create(file);
                    label3.Text = "Creating xex load file...";
                }
            }

            if (guna2ProgressBar1.Value > 40 && guna2ProgressBar1.Value < 70)
            {
                label3.Text = "Spinning up the records...";
            }

            if (guna2ProgressBar1.Value > 70)
            {
                label3.Text = "Attempting auto connection...";
            }

            if (guna2ProgressBar1.Value == 100)
            {
                guna2ProgressBar1.Value = 100;
                timer1.Stop();
                this.Hide();
                form1.Show();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
