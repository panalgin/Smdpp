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

        /// <summary>
        /// Gets all available baud rates for the data transmission.
        /// </summary>
        /// <returns>Returns the baud rate speeds, such as 9600 etc.</returns>
        public IScriptCallback GetBaudRates() => new GetBaudRatesCallback();

        public void ShowDevTools() => EventSink.InvokeDevToolsRequested();
        public void OpenGerber() => EventSink.InvokeOpenGerberRequested();
        public void OpenSvg() => EventSink.InvokeImportGerberRequested();
        public void OpenCsv() => EventSink.InvokeImportPnpFileRequested();
        public void GetFeederSlots() => EventSink.InvokeFeedersRequested();
        public void GetFeederStates() => EventSink.InvokeFeederStatesRequested();
   
        /// <summary>
        /// Asyncronously crafts a connection request to controller board with given parameters.
        /// </summary>
        /// <param name="comPort">Desired port name of the serial port.</param>
        /// <param name="baudRate">Desired port baud rate for the data transmission.</param>
        /// <returns></returns>
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
            PnpJob job = World.GetCurrentJob();

            if (job != null)
            {
                PnpPart part = job.Parts.Where(q => q.ID == guid).FirstOrDefault();
                var result = FeederManager.GetAppropriateSlotFor(part);

                if (result != null)
                {
                    return new AppropriateSlotCallback()
                    {
                        Success = true,
                        Message = "Başarılı"
                    };
                }
                else
                    return new AppropriateSlotCallback()
                    {
                        Success = false,
                        Message = "Başarısız"
                    };
            }
            else
            {
                return new AppropriateSlotCallback()
                {
                    Success = false,
                    Message = "Başarısız, henüz bir iş emri yok."
                };
            }
        }

        public string GetAvailablePackageNames()
        {
            return JsonConvert.SerializeObject(new GetAvailablePackageNamesCallback());
        }

        public bool AddComponent(string data)
        {
            var contract = JsonConvert.DeserializeObject<Contracts.AddComponentInfo>(data);
            bool result = ComponentManager.Add(contract);

            return result;
        }

        public void ListPackages() => EventSink.InvokeListPackagesRequested();
        public void ListComponents() => EventSink.InvokeListComponentsRequested();

        public void Close()
        {
            EventSink.InvokeCloseRequested();
        }
    }
}
