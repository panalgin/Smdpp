using Newtonsoft.Json;
using Smdpp.Contracts;
using Smdpp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp
{
    [DataContract]
    public class PnpTaskContract
    {
        [JsonProperty(PropertyName = "availablePackages")]
        public List<PackageContract> AvailablePackages { get; set; }

        [JsonProperty(PropertyName = "parts")]
        public List<PnpPartContract> Parts { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public Position Offset { get; set; }

        [JsonProperty(PropertyName = "boardSize")]
        public Size BoardSize { get; set; }
    }
}
