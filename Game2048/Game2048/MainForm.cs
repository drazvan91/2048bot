using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Game2048
{
    public partial class MainForm : Form
    {
        Timer timer = new Timer();


        bool initialized = false;
        public MainForm()
        {
            InitializeComponent();
            RegisterIE11();
            this.Load += MainForm_Load;
            timer.Interval = 3000;
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            webBrowser1.Focus();
            //SendKeys.Send("{RIGHT}");
            //string s = new string(new char[] { (char)Keys.Up, (char)Keys.Left, (char)Keys.Down, (char)Keys.Right });
            //string s = new string(new char[] { (char)39});

            ///SendKeys.Send(s);
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            btn_init_Click(null, null);
            this.Height = 800;

        }

        private void RegisterIE11()
        {
            string executablePath = Environment.GetCommandLineArgs()[0];
            string executableName = System.IO.Path.GetFileName(executablePath);

            RegistryKey registrybrowser = Registry.CurrentUser.OpenSubKey
               (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

            if (registrybrowser == null)
            {
                RegistryKey registryFolder = Registry.CurrentUser.OpenSubKey
                    (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", true);
                registrybrowser = registryFolder.CreateSubKey("FEATURE_BROWSER_EMULATION");
            }
            registrybrowser.SetValue(executableName, 0x02710, RegistryValueKind.DWord);
            registrybrowser.Close();
        }

        private void btn_init_Click(object sender, EventArgs e)
        {
            initialized = false;
            webBrowser1.Navigated += webBrowser1_Navigated;
            webBrowser1.Navigate("http://2048game.com/");
            webBrowser1.ScriptErrorsSuppressed = true;
            btn_init.Enabled = false;
            
        }

        class GameState
        {
            public class Cell
            {
                public int value { get; set; }
            }
            public class Row
            {
                public List<Cell> Cells { get; set; }
            }
            public class Grid
            {
                public int Size { get; set; }
                public List<List<Cell>> cells { get; set; }
            }

            public Grid grid { get; set; }
        }

      

        void UpdateTable(int[,] table, string gameState)
        {
            
            GameState state = JsonConvert.DeserializeObject<GameState>(gameState);
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (initialized) return;
            initialized = true;

            HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
            HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
            scriptEl.InnerText = "function sayHello() { return window.localStorage.gameState; }";
            head.AppendChild(scriptEl);
            string s = (string)webBrowser1.Document.InvokeScript("sayHello");
            UpdateTable(null, s);
            timer.Start();
        }
    }
}
