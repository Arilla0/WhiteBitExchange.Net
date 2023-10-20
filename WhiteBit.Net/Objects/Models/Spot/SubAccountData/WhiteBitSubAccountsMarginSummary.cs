using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts margin summary
    /// </summary>
    public class WhiteBitSubAccountsMarginSummary
    {
        /// <summary>
        /// Total btc asset
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Sub account details
        /// </summary>
        [JsonProperty("subAccountList")]
        public IEnumerable<WhiteBitSubAccountMarginInfo> SubAccounts { get; set; } = Array.Empty<WhiteBitSubAccountMarginInfo>();
    }

    /// <summary>
    /// Sub account margin info
    /// </summary>
    public class WhiteBitSubAccountMarginInfo
    {
        /// <summary>
        /// Sub account email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Total btc asset
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
    }
}
