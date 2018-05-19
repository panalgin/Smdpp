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
    }
}
