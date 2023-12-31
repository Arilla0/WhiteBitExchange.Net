using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account position risk
    /// </summary>
    public class WhiteBitSubAccountFuturesPositionRisk
    {
        /// <summary>
        /// The entry price
        /// </summary>
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        public decimal Leverage { get; set; }
        /// <summary>
        /// Max notional
        /// </summary>
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonProperty("positionAmount")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Unrealized profit
        /// </summary>
        public decimal UnrealizedProfit { get; set; }
    }
}
