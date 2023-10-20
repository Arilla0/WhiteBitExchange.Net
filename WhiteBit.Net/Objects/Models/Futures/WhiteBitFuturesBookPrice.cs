using WhiteBit.Net.Objects.Models.Spot;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Book price
    /// </summary>
    public class WhiteBitFuturesBookPrice: WhiteBitBookPrice
    {
        /// <summary>
        /// Pair
        /// </summary>
        public string Pair { get; set; } = string.Empty;
    }
}
