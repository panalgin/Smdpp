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
    public class PnpJob
    {
        [JsonProperty(PropertyName = "availablePackages")]
        public List<Contracts.Package> AvailablePackages { get; set; }

        [JsonProperty(PropertyName = "parts")]
        public List<PnpPart> Parts { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public Position Offset { get; set; }

        [JsonProperty(PropertyName = "boardSize")]
        public Size BoardSize { get; set; }
    }
}
