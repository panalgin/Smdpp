﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class SquarePlotterTool : BasePlotterTool
    {
        public SquarePlotterTool(string data) : base(data)
        {

        }

        public override void ParseProperties() {
            string[] parsedData = this.Data.Split(',');



        }
    }
}
