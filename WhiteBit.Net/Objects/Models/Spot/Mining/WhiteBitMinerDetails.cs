using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Miner details
    /// </summary>
    public class WhiteBitMinerDetails
    {
        /// <summary>
        /// Name of the worker
        /// </summary>
        public string WorkerName { get; set; } = string.Empty;

        /// <summary>
        /// Data type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Hash rate data
        /// </summary>
        public IEnumerable<WhiteBitHashRate> HashRateDatas { get; set; } = Array.Empty<WhiteBitHashRate>();
    }

    /// <summary>
    /// Hash rate
    /// </summary>
    public class WhiteBitHashRate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Hashrate
        /// </summary>
        public decimal HashRate { get; set; }
        /// <summary>
        /// Rejected
        /// </summary>
        public decimal Reject { get; set; }
    }
}
