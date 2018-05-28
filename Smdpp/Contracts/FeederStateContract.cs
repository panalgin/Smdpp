using Newtonsoft.Json;
using Smdpp.Contracts;

namespace Smdpp.Contracts
{
    public class FeederStateContract
    {
        public FeederStateContract()
        {

        }

        [JsonProperty(PropertyName = "slot")]
        public FeederSlotContract Slot { get; set; }

        [JsonProperty(PropertyName = "currentPart")]
        public ComponentContract CurrentPart { get; set; }

        [JsonProperty(PropertyName = "suggestedPart")]
        public ComponentContract SuggestedPart { get; set; }
    }
}