using Newtonsoft.Json;
using Smdpp.Logic;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Smdpp
{
    public class JavascriptController
    {
        public JavascriptController()
        {

        }

        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        public string[] GetBaudRates()
        {
            return World.BaudRates.ToArray();
        }

        public void ShowDevTools()
        {
            EventSink.InvokeDevToolsRequested();
        }

        public void OpenGerber()
        {
            EventSink.InvokeOpenGerberRequested();
        }

        public void Close()
        {
            EventSink.InvokeCloseRequested();
        }
    }
}
