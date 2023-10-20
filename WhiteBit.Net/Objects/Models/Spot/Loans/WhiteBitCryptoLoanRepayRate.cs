using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteBit.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay rate info
    /// </summary>
    public class WhiteBitCryptoLoanRepayRate
    {
        /// <summary>
        /// Loan asset
        /// </summary>
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonProperty("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}
