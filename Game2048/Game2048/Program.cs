using System;
using System.Windows.Forms;
using Game2048.Utils;

namespace Game2048
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InternetExplorerRegisterer.RegisterIE11();

            Application.Run(new MainForm());
        }
    }
}