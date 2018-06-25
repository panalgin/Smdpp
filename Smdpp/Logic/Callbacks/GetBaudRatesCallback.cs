using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Callbacks
{
    public class GetBaudRatesCallback : BaseScriptCallback
    {
        public string[] BaudRates { get; set; }

        public GetBaudRatesCallback()
        {
            this.BaudRates = World.BaudRates.ToArray();
            this.Success = true;
            this.Message = "Your request to get all available baud rates were successful.";
        }
    }
}
