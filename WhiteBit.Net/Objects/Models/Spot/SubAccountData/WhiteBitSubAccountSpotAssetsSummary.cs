using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts btc value summary
    /// </summary>
    public class WhiteBitSubAccountSpotAssetsSummary
    {
        /// <summary>
        /// Total records
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Master account total asset value
        /// </summary>
        public decimal MasterAccountTotalAsset { get; set; }
        /// <summary>
        /// Sub account values
        /// </summary>
        [JsonProperty("spotSubUserAssetBtcVoList")]
        public IEnumerable<WhiteBitSubAccountBtcValue> SubAccountsBtcValues { get; set; } = Array.Empty<WhiteBitSubAccountBtcValue>();
    }

    /// <summary>
    /// Sub account btc value
    /// </summary>
    public class WhiteBitSubAccountBtcValue
    {
        /// <summary>
        /// Sub account email
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Sub account total asset 
        /// </summary>
        public decimal TotalAsset { get; set; }
    }
}
