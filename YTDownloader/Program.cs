using System;
using System.Windows.Forms;

namespace YTDownloader
{
    static class Program
    {
        /// <summary>
        /// Start point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Application.Run(new Form1());
        }

        static void OnProcessExit(object sender, EventArgs e)
        {

        }
    }
}
