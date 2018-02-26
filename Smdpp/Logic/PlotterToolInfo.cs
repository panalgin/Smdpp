using System;

namespace Smdpp.Logic
{
    internal class PlotterToolInfo
    {
        public enum PlotterToolType
        {
            Rectangle,
            Circle,
            Square,
            PPad,
        }

        public string Name { get; internal set; }
        public PlotterToolType ToolType { get; internal set; }

        public PlotterToolInfo(string[] parameters)
        {
            ParseParameters(parameters);
        }

        private void ParseParameters(string[] parameters)
        {
            string name = parameters[0];
            string toolType = parameters[1];

            switch(toolType)
            {
                case "RECT": break;
                case "SQUARE": break;
                case "CIRCLE": break;
                case "DPAD": break;
                default: break;
            }
        }
    }
}