using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class Logger
    {
        public static void Enqueue(Exception ex)
        {
            EventSink.InvokeError(ex);
        }

        public static void Enqueue(string message)
        {
            EventSink.InvokeError(new Exception(message));
            EventSink.InvokeError(new Exception(message));
        }
    }
}
