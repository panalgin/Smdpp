using Newtonsoft.Json;
using Smdpp.Contracts;

namespace Smdpp.Contracts
{
    public class FeederState
    {
        public FeederState()
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