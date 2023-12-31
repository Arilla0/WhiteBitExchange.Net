using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index info
    /// </summary>
    public class WhiteBitFuturesCompositeIndexInfo
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonProperty("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Component asset
        /// </summary>
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// Base asset list
        /// </summary>
        [JsonProperty("baseAssetList")]
        public IEnumerable<WhiteBitFuturesCompositeIndexInfoAsset> BaseAssets { get; set; } = Array.Empty<WhiteBitFuturesCompositeIndexInfoAsset>();
    }

    /// <summary>
    /// Composite index asset
    /// </summary>
    public class WhiteBitFuturesCompositeIndexInfoAsset
    {
        /// <summary>
        /// Base asset name
        /// </summary>
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Weight in quantity
        /// </summary>
        public decimal WeightInQuantity { get; set; }
        /// <summary>
        /// Weight in percentage
        /// </summary>
        public decimal WeightInPercentage { get; set; }
    }
}
