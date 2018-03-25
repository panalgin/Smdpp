using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Callbacks
{
    public abstract class BaseScriptCallback : IScriptCallback
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
