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
            {
                var toolSet = Parse(data);
                var topCopperObjects = ParseTopCopper(definitionsPath, toolSet);
            }
        }

        bool ParseTopCopper(string definitionsPath, List<BasePlotterTool> toolSet)
        {
            string data = "";
            string topCopperFilePath = definitionsPath.Replace("READ-ME", "Top Copper");

            if (!File.Exists(topCopperFilePath))
                topCopperFilePath = topCopperFilePath.Replace(".TXT", ".GBR");

            using (StreamReader reader = new StreamReader(topCopperFilePath, Encoding.UTF8, true))
            {
                data = reader.ReadToEnd();
            }

            int endIndex = data.IndexOf("%\r\nG54") > 0 ? data.IndexOf("%\r\nG54") : data.IndexOf("%\r\nD");
            data = data.Substring(endIndex + 3);

            List<PlotEntry> entries = new List<PlotEntry>();

            var lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            BasePlotterTool lastTool = null;

            lines.All(delegate (string line)
            {
                if (line.StartsWith("G") || line.StartsWith("D"))
                    lastTool = GetToolFromToolset(line, toolSet);
                else
                {
                    var entry = GetPlotEntry(line);
                }

                return true;
            });

            return true;
        }

        PlotEntry GetPlotEntry(string line)
        {
            PlotEntry entry = null;

            string[] values = line.Split('Y');

            if (values.Length == 2)
            {
                string xValue = values[0];
                string yValue = values[1].Split('D')[0];
                string decimalPlaces = values[1].Split('D')[1];
            }

            return null;
        }

        BasePlotterTool GetToolFromToolset(string line, List<BasePlotterTool> toolSet)
        {
            line.Replace("*", "");
             
            if (line.Contains("G54"))
                line = line.Replace("G54", "");

            var tool = toolSet.FirstOrDefault(q => q.Name == line);

            return tool;
        }

        public List<BasePlotterTool> Parse(string data)
        {
            var startsAt = data.IndexOf("Photoplotter Setup");
            data = data.Substring(startsAt + "Photoplotter Setup".Length);
            var definitionsEndAt = data.IndexOf("\r\n\r\n");
            data = data.Substring(definitionsEndAt + 4);
            var endOf = data.IndexOf("\r\n\r\n");
            data = data.Substring(0, endOf);

            return ParsePlotterToolset(data); //rectangles, circles etc
        }

        List<BasePlotterTool> ParsePlotterToolset(string data)
        {
            var lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<BasePlotterTool> tools = new List<BasePlotterTool>();

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

                var tool = ParseTool(unformattedValue);

                if (tool != null)
                    tools.Add(tool);

                return true;
            });

            return tools;
        }

        BasePlotterTool ParseTool(string data)
        {
            string[] splittedData = data.Split(',');

            var result = ToolFactory.GetTool(data);

            return result;
        }
    }
}
