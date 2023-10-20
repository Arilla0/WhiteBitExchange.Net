using System;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Open Interest History info
    /// </summary>
    public class WhiteBitFuturesCoinOpenInterestHistory
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// Contract type
        /// </summary>
        [JsonConverter(typeof(ContractTypeConverter))]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// Total open interest
        /// </summary>
        public decimal SumOpenInterest { get; set; }

        /// <summary>
        /// Total open interest value
        /// </summary>
        public decimal SumOpenInterestValue { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}