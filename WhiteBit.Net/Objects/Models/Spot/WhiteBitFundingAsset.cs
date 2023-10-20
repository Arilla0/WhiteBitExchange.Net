using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Funding wallet asset
    /// </summary>
    public class WhiteBitFundingAsset
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity available
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        public decimal Freeze { get; set; }
        /// <summary>
        /// Quantity withdrawing
        /// </summary>
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Value in btc
        /// </summary>
        public decimal BtcValuation { get; set; }
    }
}
