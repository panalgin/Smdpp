using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class GerberTask
    {
        [JsonProperty(PropertyName = "tools")]
        public IEnumerable<BasePlotterTool> Tools { get; set; }

        [JsonProperty(PropertyName = "entries")]
        public IEnumerable<PlotEntry> Entries { get; set; }

        public GerberTask()
        {

        }
    }
}
