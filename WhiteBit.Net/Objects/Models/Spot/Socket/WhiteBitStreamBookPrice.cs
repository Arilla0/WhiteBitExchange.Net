using WhiteBit.Net.Interfaces;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Book tick
    /// </summary>
    public class WhiteBitStreamBookPrice: IWhiteBitBookPrice
    {
        /// <summary>
        /// Update id
        /// </summary>
        [JsonProperty("u")]
        public long UpdateId { get; set; }
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price of the best bid
        /// </summary>
        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Quantity of the best bid
        /// </summary>
        [JsonProperty("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Price of the best ask
        /// </summary>
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        [JsonProperty("A")]
        public decimal BestAskQuantity { get; set; }
    }
}
