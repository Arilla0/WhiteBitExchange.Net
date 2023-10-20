using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WhiteBit.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Earning info
    /// </summary>
    public class WhiteBitMiningEarnings
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Profit items
        /// </summary>
        public IEnumerable<WhiteBitMiningAccountEarning> AccountProfits { get; set; } = Array.Empty<WhiteBitMiningAccountEarning>();
    }

    /// <summary>
    /// Earning info
    /// </summary>
    public class WhiteBitMiningAccountEarning
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Coin
        /// </summary>
        [JsonProperty("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Earning type
        /// </summary>
        [JsonConverter(typeof(WhiteBitEarningTypeConverter))]
        [JsonProperty("type")]
        public WhiteBitEarningType Type { get; set; }
        /// <summary>
        /// Sub account id
        /// </summary>
        [JsonProperty("puid")]
        public long? SubAccountId { get; set; }
        /// <summary>
        /// Mining account
        /// </summary>
        [JsonProperty("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
