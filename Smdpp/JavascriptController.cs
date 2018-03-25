using Newtonsoft.Json;
using Smdpp.Logic;
using Smdpp.Logic.Callbacks;
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

        public void OpenSvg()
        {
            EventSink.InvokeImportGerberRequested();
        }

        public IScriptCallback SavePackage(string packageName, string svgData)
        {
            var parsedData = JsonConvert.DeserializeObject<string>(svgData);

            using(SmdppEntities context = new SmdppEntities())
            {
                Package package = new Package()
                {
                    Name = packageName,
                    Data = parsedData
                };

                bool exists = context.Packages.FirstOrDefault(q => q.Name == packageName) != null;

                if (!exists)
                {
                    context.Packages.Add(package);
                    context.SaveChanges();

                    return new SavePackageCallback() { Success = true, Message = "Başarıyla kaydedildi." };
                }

                return new SavePackageCallback() { Success = false, Message = "Benzer isimde bir kılıf zaten mevcut." };
            }
        }

        public void Close()
        {
            EventSink.InvokeCloseRequested();
        }
    }
}
