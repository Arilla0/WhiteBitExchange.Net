using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Oco info
    /// </summary>
    public class WhiteBitMarginOrderOcoList: WhiteBitOrderOcoList
    {
        /// <summary>
        /// Margin buy borrow quantity
        /// </summary>
        [JsonProperty("marginBuyBorrowAmount")]
        public decimal? MarginBuyBorrowQuantity { get; set; }
        /// <summary>
        /// Margin buy borrow asset
        /// </summary>
        public string? MarginBuyBorrowAsset { get; set; }
        /// <summary>
        /// Is isolated margin
        /// </summary>
        public bool IsIsolated { get; set; }
    }
}
