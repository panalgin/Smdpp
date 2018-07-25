using Newtonsoft.Json;

namespace Smdpp.Contracts
{
    public class AddComponentInfo
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "packageId")]
        public int PackageID { get; set; }
    }
}