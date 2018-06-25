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
        public FeederSlot Slot { get; set; }

        [JsonProperty(PropertyName = "currentPart")]
        public Component CurrentPart { get; set; }

        [JsonProperty(PropertyName = "suggestedPart")]
        public Component SuggestedPart { get; set; }
    }
}