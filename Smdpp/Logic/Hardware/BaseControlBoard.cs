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
            EventSink.JogPrecisionChangeRequested += EventSink_JogPrecisionChangeRequested;
        }

        private void EventSink_JogPrecisionChangeRequested(int value)
        {
            this.Send(new SetJogPrecisionCommand(value));
        }

        public virtual void Connect(string portName, int baudRate)
        {
            try
            {
                if (serialPort == null)
                    serialPort = new SerialPort();

                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;

                if (!serialPort.IsOpen)
                    serialPort.Open();
            }
            catch(Exception ex)
            {
                EventSink.InvokeError(ex);
            }
        }

        public virtual bool Send(BaseCommand command)
        {
            return false;
        }
    }
}
