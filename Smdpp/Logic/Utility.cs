using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class Utility
    {
        private static decimal OneThouAsMm = 0.0254m;

        public static decimal ConvertThouToMm(string thou)
        {
            thou = thou.Replace("th", "");

            return decimal.Parse(thou) * OneThouAsMm;
        }
    }
}
