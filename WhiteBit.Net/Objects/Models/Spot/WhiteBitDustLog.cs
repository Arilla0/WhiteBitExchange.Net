using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust log response details
    /// </summary>
    public class WhiteBitDustLogList
    {
        /// <summary>
        /// Total counts of exchange
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Rows
        /// </summary>
        public IEnumerable<WhiteBitDustLog> UserAssetDribblets { get; set; } = Array.Empty<WhiteBitDustLog>();
    }

    /// <summary>
    /// Dust log details
    /// </summary>
    public class WhiteBitDustLog
    {
        /// <summary>
        /// Total transferred
        /// </summary>
        [JsonProperty("totalTransferedAmount")]
        public decimal TransferredTotal { get; set; }
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonProperty("totalServiceChargeAmount")]
        public decimal ServiceChargeTotal { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("transId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Detail logs
        /// </summary>
        [JsonProperty("userAssetDribbletDetails")]
        public IEnumerable<WhiteBitDustLogDetails> Logs { get; set; } = Array.Empty<WhiteBitDustLogDetails>();
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
    }

    /// <summary>
    /// Dust log entry details
    /// </summary>
    public class WhiteBitDustLogDetails
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("transId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Service charge
        /// </summary>
        [JsonProperty("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// Transferred quantity
        /// </summary>
        [JsonProperty("transferedAmount")]
        public decimal TransferredQuantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
    }
}
