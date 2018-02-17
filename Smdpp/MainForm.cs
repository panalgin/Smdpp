﻿using System;
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
        }

        private void EventSink_DevToolsRequested()
        {
            Browser.ShowDevTools();
        }

        private void CreateBrowser()
        {
            var settings = new CefSettings();

            settings.CefCommandLineArgs.Add("enable-speech-input", "1");
            settings.CefCommandLineArgs.Add("--enable-media-stream", "--enable-media-stream");
            settings.CefCommandLineArgs.Add("--enable-usermedia-screen-capturing", "--enable-usermedia-screen-capturing");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing", "enable-usermedia-screen-capturing");
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");

            Cef.Initialize(settings);

            string filePath = Path.Combine(Application.StartupPath, "View", "index.html");

            var browser = new ChromiumWebBrowser(filePath)
            {
                Dock = DockStyle.Fill,
                MenuHandler = new CefSharpContextMenuHandler(),
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