using Newtonsoft.Json;

namespace Smdpp.Logic
{
    public class SvgTask
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }

        [JsonProperty(PropertyName = "data")]
        public string Data { get; internal set; }
    }
}