using Newtonsoft.Json;
using Smdpp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Contracts
{

    public class Component
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Value { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "package")]
        public Package Package { get; set; }

        public Component()
        {

        }
    }
}
