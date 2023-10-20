using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
namespace WhiteBit.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin collateral rate info
    /// </summary>
    public class WhiteBitPortfolioMarginCollateralRate
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Collateral rate
        /// </summary>
        public decimal CollateralRate { get; set; }
    }
}
