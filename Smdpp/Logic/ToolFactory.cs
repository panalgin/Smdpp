using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class ToolFactory
    {
        public static BasePlotterTool GetTool(string data)
        {
            BasePlotterTool resultInstance = null;

            string[] parameters = data.Split(',');

            string toolType = parameters[1];

            switch (toolType)
            {
                case "RECT": { resultInstance = new RectanglePlotterTool(data); break; }
                case "SQUARE": { resultInstance = new SquarePlotterTool(data); break; }
                case "CIRCLE": { resultInstance = new CirclePlotterTool(data); break; }
                case "SMT": { resultInstance = new RectanglePlotterTool(data); break; }

                default: break;
            }

            return resultInstance;
        }
    }
}
