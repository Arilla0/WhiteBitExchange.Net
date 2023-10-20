using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// User's position mode
    /// </summary>
    public class WhiteBitFuturesPositionMode
    {
        /// <summary>
        /// true": Hedge Mode mode; "false": One-way Mode
        /// </summary>
        [JsonProperty("dualSidePosition"), JsonConverter(typeof(PositionModeConverter))]
        public PositionMode PositionMode { get; set; }
    }
}
