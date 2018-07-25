using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Callbacks
{
    public abstract class BaseScriptCallback : IScriptCallback
    {
        public BaseScriptCallback()
        {
        }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
