using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Delist margin schedule
    /// </summary>
    public class WhiteBitMarginDelistSchedule
    {
        /// <summary>
        /// Delist time
        /// </summary>
        [JsonProperty("delistTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DelistTime { get; set; }
        /// <summary>
        /// Cross margin assets
        /// </summary>
        public IEnumerable<string> CrossMarginAssets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Isolated margin symbols
        /// </summary>
        public IEnumerable<string> IsolatedMarginSymbols { get; set; } = Array.Empty<string>();
    }
}
