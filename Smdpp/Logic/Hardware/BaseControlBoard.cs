using Smdpp.Logic.Commands;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Hardware
{
    public class BaseControlBoard
    {
        private SerialPort serialPort = new SerialPort();

        public BaseControlBoard()
        {
            EventSink.ConnectRequested += OnConnectRequested;
            EventSink.JogPrecisionChangeRequested += EventSink_JogPrecisionChangeRequested;
        }

        public virtual bool OnConnectRequested(string comPort, int baudRate)
        {
            return Connect(comPort, baudRate);
        }

        private void EventSink_JogPrecisionChangeRequested(int value)
        {
            this.Send(new SetJogPrecisionCommand(value));
        }

        public virtual bool Connect(string portName, int baudRate)
        {
            try
            {
                if (serialPort == null)
                    serialPort = new SerialPort();

                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;

                if (!serialPort.IsOpen)
                    serialPort.Open();

                return true;
            }
            catch(Exception ex)
            {
                EventSink.InvokeError(ex);

                return false;
            }
        }

        public virtual bool Send(BaseCommand command)
        {
            if (IsConnected())
            {
                var cmd = command.ToString();
                serialPort.WriteLine(cmd);

                return true;
            }
            else
                return false;
        }

        public bool IsConnected()
        {
            return this.serialPort != null && this.serialPort.IsOpen;
        }
    }
}
