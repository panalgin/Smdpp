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
        [JsonProperty(PropertyName = "referenceId")]
        public string ReferenceID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "packageId")]
        public int? PackageID { get; set; }

        [JsonProperty(PropertyName = "position")]
        public Position Position { get; set; }

        [JsonProperty(PropertyName = "layer")]
        public Layer Layer { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public double Rotation { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        public ComponentContract()
        {

        }
    }
}
