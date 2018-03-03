using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class RectanglePlotterTool : BasePlotterTool
    {
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public RectanglePlotterTool(string data) : base(data)
        {

        }

        public override void ParseProperties() {
            string[] parsedData = this.Data.Split(',');

            string widthText = parsedData[2].Replace("W=", "");
            string heigthText = parsedData[3].Replace("H=", "");

            decimal width = 0.0m;
            decimal heigth = 0.0m;

            if (widthText.EndsWith("th"))
                width = Utility.ConvertThouToMm(widthText.Replace("th", ""));
            else
                width = decimal.Parse(widthText.Replace("mm", "").Replace(".", ","));

            if (heigthText.EndsWith("th"))
                heigth = Utility.ConvertThouToMm(heigthText.Replace("th", ""));
            else
                heigth = decimal.Parse(heigthText.Replace("mm", "").Replace(".", ","));

            this.Height = heigth;
            this.Width = width;
        }
    }
}
