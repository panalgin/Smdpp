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

            ParsePlotterToolset(data); //rectangles, circles etc
        }

        void ParsePlotterToolset(string data)
        {
            var lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            lines.All(delegate (string line)
            {
                Dictionary<string, string> propertyList = new Dictionary<string, string>();

                string unformattedValue = "";

                line.ToCharArray().All(delegate (char c)
                {
                    if (c == ' ' || c == '\t')
                    {
                        if (!unformattedValue.EndsWith(","))
                            unformattedValue += ",";

                        return true;
                    }
                    else
                        unformattedValue += c;

                    return true;
                });

                ParseTool(unformattedValue);

                return true;
            });
        }

        void ParseTool(string data)
        {
            string[] splittedData = data.Split(',');

            PlotterToolInfo tool = new PlotterToolInfo(splittedData);
        }
    }
}
