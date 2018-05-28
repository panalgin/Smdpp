using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Smdpp.Logic
{
    [DataContract]
    public class PnpPartContract
    {
        /// <summary>
        /// Referans numarası, U1, U2 gibi
        /// </summary>
        [JsonProperty(PropertyName = "referenceId")]
        public string ReferenceID { get; set; }

        /// <summary>
        /// Kılıf numarası, QFP28 gibi
        /// </summary>
        [JsonProperty(PropertyName = "packageId")]
        public int PackageID { get; set; }

        /// <summary>
        /// Pozisyon bilgisi
        /// </summary>
        [JsonProperty(PropertyName = "position")]
        public Position Position { get; set; }

        /// <summary>
        /// Alt-üst katman bilgisi
        /// </summary>
        [JsonProperty(PropertyName = "layer")]
        public Layer Layer { get; set; }

        /// <summary>
        /// Parçanın dönüklük bilgisi
        /// </summary>
        [JsonProperty(PropertyName = "rotation")]
        public double Rotation { get; set; }

        /// <summary>
        /// Parçaya ait değer, 10uF, spin-fv1 gibi.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "packageName")]
        public string PackageName { get; internal set; }

        public PnpPartContract()
        {

        }
    }
}