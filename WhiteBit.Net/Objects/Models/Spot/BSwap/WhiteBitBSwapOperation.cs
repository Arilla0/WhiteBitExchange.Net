using System;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Operation record
    /// </summary>
    public class WhiteBitBSwapOperation
    {
        /// <summary>
        /// Operation id
        /// </summary>
        public long OperationId { get; set; }
        /// <summary>
        /// Pool id
        /// </summary>
        public int PoolId { get; set; }

        /// <summary>
        /// Pool name
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// Operation
        /// </summary>
        [JsonConverter(typeof(BSwapOperationConverter))]
        public BSwapOperation Operation { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(BSwapStatusConverter))]
        public BSwapStatus Status { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Share quantity
        /// </summary>
        [JsonProperty("shareAmount")]
        public decimal ShareQuantity { get; set; }
    }
}
