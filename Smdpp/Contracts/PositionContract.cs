using Newtonsoft.Json;

namespace Smdpp.Contracts
{
    public class PositionContract
    {
        [JsonProperty(PropertyName = "x")]
        public double X { get; set; }

        [JsonProperty(PropertyName = "y")]
        public double Y { get; set; }
    }
}