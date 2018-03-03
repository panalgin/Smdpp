using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class SquarePlotterTool : BasePlotterTool
    {
        [JsonProperty(PropertyName = "side")]
        public decimal Side { get; set; }

        public SquarePlotterTool(string data) : base(data)
        {

        }

        public override void ParseProperties() {
            string[] parsedData = this.Data.Split(',');

            string sideText = parsedData[2].Replace("S=", "");
            decimal side = 0.0m;

            if (sideText.EndsWith("th"))
                side = Utility.ConvertThouToMm(sideText.Replace("th", ""));
            else
                side = decimal.Parse(sideText.Replace("mm", "").Replace(".", ","));

            this.Side = side;
        }
    }
}
