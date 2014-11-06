using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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
        private TimeSpan timeElapsed;
        private int moveNumber;
        private bool paused;
        private bool timeoutHandled;
        private object lockObject = new object();
        private int nextTileTarget = 1;

        SoundPlayer soundPlayer = new SoundPlayer(GameResources.beep_02);
        SoundPlayer soundPlayerApplause = new SoundPlayer(GameResources.applause2);

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
            

            if (!timeoutHandled && DateTime.Now.Subtract(dateStarted).TotalMinutes >= 1d)
            {
                timeoutHandled = true;
                ChangePauseState(true);
            }

            if (!paused)
            {
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

                    lock (lockObject)
                    {
                        SendKeys.Send(DirectionHelper.GetSendKeyString(direction));
                    }

                    moveNumber++;
                    updateMoveNumberLabel();
                    updateSpeedLabel();
                    updateTimeLabel();
                    updateEstimatedScoreLabel(state);
                    checkAudio();
                }

                timer.Start();
            }
        }

        private void checkAudio()
        {
            if (this._viewState.HasGreaterTile(this.nextTileTarget))
            {
                this.nextTileTarget++;

                if (this.nextTileTarget == 12)
                {
                    soundPlayerApplause.Play();
                }
                else
                {
                    soundPlayer.Play();
                }
            }
        }

        private void updateEstimatedScoreLabel(GameState state)
        {
            if (!timeoutHandled)
            {
                int score = state.score;
                var ellapesed = DateTime.Now.Subtract(dateStarted);
                int estimatedScore = (int)(score/ellapesed.TotalMinutes);
                estimatedScoreLabel.Text = estimatedScore.ToString();
            }
        }

        private void updateMoveNumberLabel()
        {
            this.moveNumberLabel.Text = moveNumber.ToString();
        }

        private void ChangePauseState(bool value)
        {
            this.paused = value;

            if (paused)
            {
                this.btn_pause.Text = "Resume";
                this.timeElapsed = DateTime.Now.Subtract(dateStarted);
            }
            else
            {
                this.btn_pause.Text = "Pause";
                this.dateStarted = DateTime.Now.Subtract(timeElapsed);

                var state = GetGameState();

                _viewState = GameGrid.FromState(state);
                _gameAi = new GameAi(_viewState);

                webBrowser1.Focus();
                timer.Start();
            }
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
                speedLabel.Text = speed.ToString();
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

            nextTileTarget = 1;
            timeoutHandled = false;
            timer.Start();
        }

        private GameState GetGameState()
        {
            string stringValue = null;
            int i = 0;
            while (stringValue == null && i<1000)
            {
                stringValue = (string) webBrowser1.Document.InvokeScript("sayHello");
                i++;
            }
            return GameState.FromJson(stringValue);
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

        private void btn_pause_Click(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                this.ChangePauseState(!this.paused);
            }

        }
    }
}
