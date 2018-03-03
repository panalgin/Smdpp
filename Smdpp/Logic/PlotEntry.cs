using Newtonsoft.Json;
using System;

namespace Smdpp.Logic
{
    public class PlotEntry
    {
        [JsonProperty(PropertyName = "x")]
        public decimal X { get; set; }

        [JsonProperty(PropertyName = "y")]
        public decimal Y { get; set; }

        [JsonProperty(PropertyName = "toolId")]
        public string ToolID { get; set; }

        public PlotEntry()
        {
             
        }
    }
}