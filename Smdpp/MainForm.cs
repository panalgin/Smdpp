using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Smdpp.Logic;

namespace Smdpp
{
    public partial class MainForm : Form
    {
        public ChromiumWebBrowser Browser { get; set; }

        public MainForm()
        {
            InitializeComponent();

            CreateBrowser();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            EventSink.DevToolsRequested += EventSink_DevToolsRequested;

            new CameraTestForm().Show();
        }

        private void EventSink_DevToolsRequested()
        {
            Browser.ShowDevTools();
        }

        private void CreateBrowser()
        {
            Cef.Initialize();

            string filePath = Path.Combine(Application.StartupPath, "View", "index.html");

            var browser = new ChromiumWebBrowser(filePath)
            {
                Dock = DockStyle.Fill,
                MenuHandler = new CefSharpContextMenuHandler()
            };

            browser.BrowserSettings = new BrowserSettings()
            {
                FileAccessFromFileUrls = CefState.Enabled,
                UniversalAccessFromFileUrls = CefState.Enabled,
                DefaultEncoding = "UTF8"
            };

            this.Browser = browser;
            this.Controls.Add(Browser);

            this.Browser.RegisterJsObject("windowsApp", new JavascriptController());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
