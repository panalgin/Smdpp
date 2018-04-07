using Newtonsoft.Json;

namespace Smdpp.Logic
{
    public class Position
    {
        /// <summary>
        /// X axis position value
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public double X { get; set; }

        /// <summary>
        /// Y axis position value
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public double Y { get; set; }
    }
}