using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Margin Asset Info
    /// </summary>
    public class WhiteBitBrokerageMarginAssetInfo
    {
        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<WhiteBitBrokerageSubAccountMarginAssetInfo> Data { get; set; } = Array.Empty<WhiteBitBrokerageSubAccountMarginAssetInfo>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Account Margin Asset Info
    /// </summary>
    public class WhiteBitBrokerageSubAccountMarginAssetInfo
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Margin enable
        /// </summary>
        [JsonProperty("marginEnable")]
        public bool IsMarginEnable { get; set; }
        
        /// <summary>
        /// Total Asset Of Btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        
        /// <summary>
        /// Total Liability Of Btc
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        
        /// <summary>
        /// Total Net Asset Of Btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        
        /// <summary>
        /// Margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
    }
}