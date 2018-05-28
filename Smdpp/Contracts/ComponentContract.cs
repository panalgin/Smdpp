using Newtonsoft.Json;
using Smdpp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Contracts
{

    public class ComponentContract
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "package")]
        public Package Package { get; set; }

        public ComponentContract()
        {

        }
    }
}
