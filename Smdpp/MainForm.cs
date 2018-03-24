using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
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
            EventSink.OpenGerberReqeusted += EventSink_OpenGerberReqeusted;
            EventSink.ImportSvgRequested += EventSink_ImportSvgRequested;
            EventSink.CloseRequested += EventSink_CloseRequested;
            EventSink.GerberParsed += EventSink_GerberParsed;
            EventSink.SvgParsed += EventSink_SvgParsed;
        }

        private void EventSink_SvgParsed(SvgTask task)
        {
            string data = JsonConvert.SerializeObject(task);
            ScriptRunner.Run(ScriptRunner.ScriptAction.SvgTaskResolved, Utility.HtmlEncode(data));
        }

        private void EventSink_ImportSvgRequested()
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                var dialog = SetupImportSvgDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = dialog.FileName;

                    SvgReader reader = new SvgReader(filePath);

                }
            });
        }

        private void EventSink_GerberParsed(GerberTask task)
        {
            string data = JsonConvert.SerializeObject(task, Formatting.Indented);
            ScriptRunner.Run(ScriptRunner.ScriptAction.GerberTaskResolved, Utility.HtmlEncode(data));
        }

        private void EventSink_CloseRequested()
        {
            Application.Exit();
        }

        private void EventSink_OpenGerberReqeusted()
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                var dialog = SetupGerberDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = dialog.FileName;

                    //LibraryManager.ParseAresLibrary(filePath);

                    GerberReader reader = new GerberReader(filePath);
                }
            });
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
            CefSharpSettings.ShutdownOnExit = true;
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

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
                DefaultEncoding = "UTF8",
            };

            this.Browser = browser;
            this.Controls.Add(Browser);

            this.Browser.RegisterJsObject("windowsApp", new JavascriptController());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                Cef.Shutdown();
            });
        }

        private OpenFileDialog SetupGerberDialog()
        {
            return SetupOpenFileDialog("Gerber Files|*.txt,*.gbr");
        }

        private OpenFileDialog SetupImportSvgDialog()
        {
            return SetupOpenFileDialog("Svg Files|*.svg");
        }

        private OpenFileDialog SetupOpenFileDialog(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = filter,
                FileName = "",
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            };

            return dialog;
        }
    }
}
