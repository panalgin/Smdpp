using Newtonsoft.Json;
using System.Collections.Generic;

namespace Smdpp.Logic
{
    public class PnpTask
    {
        /// <summary>
        /// Dizilecek parçalar
        /// </summary>
        [JsonProperty(PropertyName = "smtParts")]
        public List<PnpPart> SmtParts { get; set; }

        /// <summary>
        /// Gözardı edilecek parçalar
        /// </summary>
        [JsonProperty(PropertyName = "dipParts")]
        public List<PnpPart> DipParts { get; set; }

        public PnpTask()
        {

        }
    }
}