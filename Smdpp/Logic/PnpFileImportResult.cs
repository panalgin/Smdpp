using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class PnpFileImportResult
    {
        [JsonProperty(PropertyName = "width")]
        public decimal Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public decimal Height { get; set; }

        [JsonProperty(PropertyName = "tools")]
        public IEnumerable<BasePlotterTool> Tools { get; set; }

        [JsonProperty(PropertyName = "entries")]
        public IEnumerable<PlotEntry> Entries { get; set; }

        public PnpFileImportResult()
        {

        }
    }
}
