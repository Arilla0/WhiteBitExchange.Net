using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public class WhiteBitExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// The rate limits used
        /// </summary>
        public IEnumerable<WhiteBitRateLimit> RateLimits { get; set; } = Array.Empty<WhiteBitRateLimit>();
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<WhiteBitSymbol> Symbols { get; set; } = Array.Empty<WhiteBitSymbol>();
        /// <summary>
        /// Filters
        /// </summary>
        public IEnumerable<object> ExchangeFilters { get; set; } = Array.Empty<object>();
    }
}
