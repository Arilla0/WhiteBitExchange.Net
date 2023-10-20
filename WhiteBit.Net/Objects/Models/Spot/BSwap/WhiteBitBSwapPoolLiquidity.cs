using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Pool liquidity info
    /// </summary>
    public class WhiteBitBSwapPoolLiquidity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int PoolId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Liquidity
        /// </summary>
        public Dictionary<string, decimal> Liquidity { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Share
        /// </summary>
        public WhiteBitPoolShare Share { get; set; } = new WhiteBitPoolShare();
    }

    /// <summary>
    /// Pool share info
    /// </summary>
    public class WhiteBitPoolShare
    {
        /// <summary>
        /// Share quantity
        /// </summary>
        [JsonProperty("shareAmount")]
        public decimal ShareQuantity { get; set; }
        /// <summary>
        /// Share percentage
        /// </summary>
        public decimal SharePercentage { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public Dictionary<string, decimal> Asset { get; set; } = new Dictionary<string, decimal>();
    }
}
