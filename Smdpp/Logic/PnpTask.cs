using Newtonsoft.Json;
using Smdpp.Contracts;
using Smdpp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp
{
    public class PnpTask
    {
        [JsonProperty(PropertyName = "availablePackages")]
        public List<PackageContract> AvailablePackages { get; set; }

        [JsonProperty(PropertyName = "components")]
        public List<ComponentContract> Components { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public Position Offset { get; set; }
    }
}
