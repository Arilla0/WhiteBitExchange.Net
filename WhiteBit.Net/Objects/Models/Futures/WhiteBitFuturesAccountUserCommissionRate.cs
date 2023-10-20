namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// User commission rate
    /// </summary>
    public class WhiteBitFuturesAccountUserCommissionRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Maker commission rate
        /// </summary>
        public decimal MakerCommissionRate { get; set; }
        /// <summary>
        /// Taker commission rate
        /// </summary>
        public decimal TakerCommissionRate { get; set; }
    }
}
