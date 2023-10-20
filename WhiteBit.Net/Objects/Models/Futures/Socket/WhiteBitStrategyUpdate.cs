using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace WhiteBit.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Strategy update
    /// </summary>
    public class WhiteBitStrategyUpdate: WhiteBitStreamEvent
    {
        /// <summary>
        /// Update info
        /// </summary>
        [JsonProperty("su")]
        public WhiteBitStrategyInfo StrategyUpdate { get; set; } = null!;

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Strategy update info
    /// </summary>
    public class WhiteBitStrategyInfo
    {
        /// <summary>
        /// The strategy id
        /// </summary>
        [JsonProperty("si")]
        public int StrategyId { get; set; }
        /// <summary>
        /// Strategy type
        /// </summary>
        [JsonProperty("st")]
        public string StrategyType { get; set; } = string.Empty;
        /// <summary>
        /// Stategy status
        /// </summary>
        [JsonProperty("ss")]
        public string StrategyStatus { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonProperty("ut")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Op code
        /// </summary>
        [JsonProperty("c")]
        public int OpCode { get; set; }
    }
}
