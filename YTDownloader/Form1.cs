using System;
using System.IO;
using System.Windows.Forms;
using MediaToolkit;
using MediaToolkit.Model;
using System.Diagnostics;
using VideoLibrary;

namespace YTDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var pathWithEnv = @"%USERPROFILE%\Videos\";
            var path = Environment.ExpandEnvironmentVariables(pathWithEnv);
            label2.Text = path;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            var pathWithEnv = @"%USERPROFILE%\Videos";
            var path = Environment.ExpandEnvironmentVariables(pathWithEnv);
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    path = folderDialog.SelectedPath;
                    label2.Text = path;
                }
                else
                {
                    if (metrics.Checked)
                    {
                        API.LogError("Path Selection Failed");
                    }
                        MessageBox.Show("Path selection failed!");
                }
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            string path = label2.Text;
            string link = textBox1.Text;
            if (link == "Paste link here")
            {
                MessageBox.Show("Please input a youtube video URL");
            }
            else
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    var youtube = YouTube.Default;
                    var vid = youtube.GetVideo(link);
                    File.WriteAllBytes(path + vid.FullName, vid.GetBytes());

                    if (metrics.Checked)
                    {
                        API.LogInfo("MP4Downl " + link);
                    }

                    if (checkBox1.Checked)
                    {
                        Process.Start(path);
                    }
                    else MessageBox.Show("Downloaded!");
                }
                else MessageBox.Show("Please connect to a network");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = label2.Text;
            string link = textBox1.Text;
            if (link == "Paste link here")
            {
                if (metrics.Checked)
                {
                    API.LogError("Not pasted youtube video URL");
                }
                MessageBox.Show("Please input a youtube video URL");
            }
            else
            {
                var youtube = YouTube.Default;
                var vid = youtube.GetVideo(link);
                 string mp4filepath = path + @"\" + "Temp.mp4";
                File.WriteAllBytes(mp4filepath, vid.GetBytes());

                var inputFile = new MediaFile { Filename = path + @"\" + "Temp.mp4" };
                var outputFile = new MediaFile { Filename = $"{path + @"\" + vid.Title}.mp3" };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.Convert(inputFile, outputFile);
                    File.Delete(mp4filepath);

                    if (metrics.Checked)
                    {
                        API.LogInfo("MP3Downl " + link);
                    }

                    if (checkBox1.Checked)
                    {
                        Process.Start(path);
                    }
                    else MessageBox.Show("Downloaded!");
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/theasern");
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metrics.Checked)
            {
                API.LogInfo("Changed folder on download state");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            string vidname = API.GetTitle(text);
            label5.Text = vidname;
            API.LogInfo(vidname);
        }


    }
}
