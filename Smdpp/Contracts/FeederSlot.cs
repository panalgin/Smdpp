using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Contracts
{
    [DataContract]
    public class FeederSlot
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "pickupX")]
        public decimal PickupX { get; set; }

        [JsonProperty(PropertyName = "pickupY")]
        public decimal? PickupY { get; set; }

        [JsonProperty(PropertyName = "depth")]
        public decimal? Depth { get; set; }
    }
}
