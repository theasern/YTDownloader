using System;
using System.IO;
using System.Windows.Forms;
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
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(link); 
                File.WriteAllBytes(path + @"\" + video.FullName, video.GetBytes());
                MessageBox.Show("Downloaded!");            }

        }
    }
}
