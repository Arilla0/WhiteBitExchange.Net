using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin dust transfer info
    /// </summary>
    public class WhiteBitMarginDustTransfer
    {
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonProperty("totalServiceCharge")]
        public decimal TotalServiceCharge { get; set; }
        /// <summary>
        /// Total transfered
        /// </summary>
        [JsonProperty("totalTransfered")]
        public decimal TotalTransfered { get; set; }
        /// <summary>
        /// Transfer results
        /// </summary>
        [JsonProperty("transferResult")]
        public IEnumerable<WhiteBitMargingDustTransferResult> TransferResults { get; set; } = Array.Empty<WhiteBitMargingDustTransferResult>();
    }

    /// <summary>
    /// Transfer results
    /// </summary>
    public class WhiteBitMargingDustTransferResult
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Source asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// Service charge quantity
        /// </summary>
        [JsonProperty("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Transfered quantity
        /// </summary>
        [JsonProperty("transferedAmount")]
        public decimal TransferedQuantity { get; set; }
    }
}
