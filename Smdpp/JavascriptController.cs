﻿using Newtonsoft.Json;
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

        public void OpenCsv()
        {
            EventSink.InvokeImportPnpFileRequested();
        }

        public void GetFeederSlots()
        {
            EventSink.InvokeFeedersRequested();
        }

        public void GetFeederStates()
        {
            EventSink.InvokeFeederStatesRequested();
        }

        public IScriptCallback Connect(string comPort, int baudRate)
        {
            EventSink.InvokeConnectRequested(comPort, baudRate);

            return new ConnectRequestedCallback()
            {
                Success = true,
                Message = "Başarılı"
            };
        }

        public IScriptCallback SetJogPrecision(int value)
        {
            EventSink.InvokeJogPrecisionChangeRequested(value);

            return new SetPrecisionCallback()
            {
                Success = true,
                Message = "Başarılı."
            };
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


        public IScriptCallback GetAppropriateSlotFor(string guid)
        {
            PnpPArt
        }

        public void ListPackages()
        {
            EventSink.InvokeListPackagesRequested();
        }

        public void Close()
        {
            EventSink.InvokeCloseRequested();
        }
    }
}
