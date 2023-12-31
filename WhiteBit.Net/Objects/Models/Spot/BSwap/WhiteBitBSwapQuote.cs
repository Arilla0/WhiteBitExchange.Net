using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Quote info
    /// </summary>
    public class WhiteBitBSwapQuote
    {
        /// <summary>
        /// Quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("baseQty")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Slippage
        /// </summary>
        public decimal Slippage { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        public decimal Fee { get; set; }
    }
}
