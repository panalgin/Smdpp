using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class GerberReader
    {
        public GerberReader(string definitionsPath)
        {
            string data = "";

            using (StreamReader reader = new StreamReader(definitionsPath, Encoding.UTF8, true))
            {
                data = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(data))
                Parse(data);
        }

        public void Parse(string data)
        {
            var startsAt = data.IndexOf("Photoplotter Setup");
            data = data.Substring(startsAt + "Photoplotter Setup".Length);
            var definitionsEndAt = data.IndexOf("\r\n\r\n");
            data = data.Substring(definitionsEndAt + 4);
            var endOf = data.IndexOf("\r\n\r\n");
            data = data.Substring(0, endOf);

            var tools = ParsePlotterToolset(data); //rectangles, circles etc
        }

        void ParsePlotterToolset(string data)
        {

        }
    }
}
