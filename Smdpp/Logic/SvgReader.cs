using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class SvgReader
    {
        public SvgReader(string filePath)
        {
            string data = "";

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
            {
                data = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(data))
            {
                FileInfo info = new FileInfo(filePath);

                SvgTask task = new SvgTask();
                task.Name = info.Name;
                task.Data = data;

                EventSink.InvokeSvgParsed(task);
            }
        }
    }
}
