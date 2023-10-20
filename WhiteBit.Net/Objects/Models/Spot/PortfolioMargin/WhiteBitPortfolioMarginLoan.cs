using Newtonsoft.Json;
using System;

namespace WhiteBit.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Bankruptcy loan info
    /// </summary>
    public class WhiteBitPortfolioMarginLoan
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Loan amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
