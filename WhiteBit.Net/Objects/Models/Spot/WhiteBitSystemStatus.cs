using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// The status of WhiteBit
    /// </summary>
    public class WhiteBitSystemStatus
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(SystemStatusConverter))]
        public SystemStatus Status { get; set; }
        /// <summary>
        /// Additional info
        /// </summary>
        [JsonProperty("msg")]
        public string? Message { get; set; }
    }
}
