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
        public CirclePlotterTool(string data) : base(data)
        {
            Debug.Write(this.Name);
        }

        public override void ParseProperties() {
            string[] parsedData = this.Data.Split(',');



        }
    }
}
