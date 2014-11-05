using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game2048.Core;
using Game2048.Models;
using Game2048.Utils;
using Game2048.View;
using Microsoft.Win32;
using Newtonsoft.Json;
using Game2048.Bot;

namespace Game2048
{
    public partial class MainForm : Form
    {
        Timer timer = new Timer();

        bool initialized = false;
        private IGameGrid _viewState;
        private GameAi _gameAi;
        private ILogger _logger;
        private DateTime dateStarted;
        private int moveNumber;

        public MainForm()
        {
            InitializeComponent();
            _logger=new DebugOutputLogger();
            this.Load += MainForm_Load;
            timer.Interval = 1;
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            webBrowser1.Focus();

            var state = GetGameState();
            var cell = this._viewState.UpdateFromState(state);
            if (cell != null)
            {
                if (cell.Value > 0)
                {
                    this._gameAi.AddTile(cell);
                }

                var direction = this._gameAi.Move();
                if (direction == Direction.None)
                {
                    _logger.WriteLine("GAME OVER");
                    return;
                }

                this._viewState.Move(direction);
                SendKeys.Send(DirectionHelper.GetSendKeyString(direction));
                moveNumber++;
                updateSpeedLabel();
                updateTimeLabel();
            }

            timer.Start();
            
        }

        private void updateTimeLabel()
        {
            var date = DateTime.Now.Subtract(dateStarted);
            int seconds = date.Seconds;
            int minuts = date.Minutes;

            timeLabel.Text = string.Format("{0:00}:{1:00}", minuts, seconds);

        }

        private void updateSpeedLabel()
        {
            DateTime date = DateTime.Now;
            double seconds = date.Subtract(dateStarted).TotalSeconds;
            if (seconds > 0)
            {
                int speed = (int)(60*moveNumber/seconds);
                speedLabel.Text = speed + " moves per minute";
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            NavigateToGameUrl();
            this.Height = 700;
        }

        private void btn_init_Click(object sender, EventArgs e)
        {
            initialized = false;
            InitGame();
            btn_init.Enabled = false;
            dateStarted = DateTime.Now;
        }

        private void NavigateToGameUrl()
        {
            webBrowser1.Navigated +=webBrowser1_Navigated;
            webBrowser1.Navigate("http://2048game.com/");
            webBrowser1.ScriptErrorsSuppressed = true;
        }


        private void InitGame()
        {
            var state = GetGameState();
            
            _viewState = GameGrid.FromState(state);
            _gameAi = new GameAi(_viewState);

            timer.Start();
            rich_info.Text = this._viewState.ToString();
        }

        private GameState GetGameState()
        {
            GameState state = GameState.FromJson((string) webBrowser1.Document.InvokeScript("sayHello"));
            return state;
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            InitializeWebBrowser();
        }

        private void InitializeWebBrowser()
        {
            if (initialized) return;
            initialized = true;

            InjectJavascriptSnippet();

           
        }

        private void InjectJavascriptSnippet()
        {
            HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
            HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
            scriptEl.InnerText = "function sayHello() { return window.localStorage.gameState; }";
            head.AppendChild(scriptEl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
