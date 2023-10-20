using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteBit.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Adjust info
    /// </summary>
    public class WhiteBitCryptoLoanLtvAdjust
    {
        /// <summary>
        /// The loaning asset
        /// </summary>
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Direction
        /// </summary>
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Current ltv
        /// </summary>
        public decimal CurrentLtv { get; set; }
    }
}
