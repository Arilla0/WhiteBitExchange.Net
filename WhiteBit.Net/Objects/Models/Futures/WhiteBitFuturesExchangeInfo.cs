using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public class WhiteBitFuturesExchangeInfo
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
        /// Filters
        /// </summary>
        public IEnumerable<object> ExchangeFilters { get; set; } = Array.Empty<object>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    public class WhiteBitFuturesUsdtExchangeInfo: WhiteBitFuturesExchangeInfo
    {
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<WhiteBitFuturesUsdtSymbol> Symbols { get; set; } = Array.Empty<WhiteBitFuturesUsdtSymbol>();

        /// <summary>
        /// All assets
        /// </summary>
        public IEnumerable<WhiteBitFuturesUsdtAsset> Assets { get; set; } = Array.Empty<WhiteBitFuturesUsdtAsset>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    public class WhiteBitFuturesCoinExchangeInfo : WhiteBitFuturesExchangeInfo
    {
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<WhiteBitFuturesCoinSymbol> Symbols { get; set; } = Array.Empty<WhiteBitFuturesCoinSymbol>();
    }
}
