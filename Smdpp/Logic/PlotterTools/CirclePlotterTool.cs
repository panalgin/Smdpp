using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class CirclePlotterTool : BasePlotterTool
    {
        public decimal Diameter { get; set; }

        public CirclePlotterTool(string data) : base(data)
        {
            Debug.Write(this.Name);
        }

        public override void ParseProperties() {
            string[] parsedData = this.Data.Split(',');

            var result = parsedData[2].Replace("D=", "");
            decimal diameter = 0.0m;

            if (result.EndsWith("mm"))
                diameter = decimal.Parse(result.Replace("mm", "").Replace(".", ","));
            else if (result.EndsWith("th"))
                diameter = Utility.ConvertThouToMm(result);

            this.Diameter = diameter;
        }
    }
}
