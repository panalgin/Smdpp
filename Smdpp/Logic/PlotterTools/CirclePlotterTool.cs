﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class CirclePlotterTool : BasePlotterTool
    {
        public CirclePlotterTool(string data)
        {
            this.Data = data;

            this.ParseToolType();
            this.ParsePadType();
        }

        public override void ParseProperties() {
            string[] parsedData = this.Data.Split(',');



        }
    }
}
