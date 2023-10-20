using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Max quantities
    /// </summary>
    public class WhiteBitCrossCollateralAdjustMaxAmounts
    {
        /// <summary>
        /// The max in amount
        /// </summary>
        [JsonProperty("maxInAmount")]
        public decimal MaxInQuantity { get; set; }
        /// <summary>
        /// The max out amount
        /// </summary>
        [JsonProperty("maxOutAmount")]
        public decimal MaxOutQuantity { get; set; }
    }
}
