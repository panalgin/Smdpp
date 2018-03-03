using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class GerberTask
    {
        public IEnumerable<BasePlotterTool> Tools { get; set; }
        public IEnumerable<PlotEntry> Entries { get; set; }

        public GerberTask()
        {

        }
    }
}
