using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account status info
    /// </summary>
    public class WhiteBitAccountStatus
    {
        /// <summary>
        /// The result status
        /// </summary>
        [JsonProperty("data")]
        public string? Data { get; set; }
    }
}
