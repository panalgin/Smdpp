using Newtonsoft.Json;

namespace Smdpp.Logic
{
    public class Size
    {
        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }
    }
}