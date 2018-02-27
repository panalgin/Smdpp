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



        }
    }
}
