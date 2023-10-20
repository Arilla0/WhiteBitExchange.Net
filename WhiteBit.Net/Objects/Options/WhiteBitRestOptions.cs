using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WhiteBit.Net.Objects.Options
{
    /// <summary>
    /// Options for the WhiteBitRestClient
    /// </summary>
    public class WhiteBitRestOptions : RestExchangeOptions<WhiteBitEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static WhiteBitRestOptions Default { get; set; } = new WhiteBitRestOptions()
        {
            Environment = WhiteBitEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Spot API options
        /// </summary>
        public WhiteBitRestApiOptions SpotOptions { get; private set; } = new WhiteBitRestApiOptions
        {
            RateLimiters = new List<IRateLimiter>
                {
                    new RateLimiter()
                        .AddPartialEndpointLimit("/api/", 1200, TimeSpan.FromMinutes(1))
                        .AddPartialEndpointLimit("/sapi/", 180000, TimeSpan.FromMinutes(1))
                        .AddEndpointLimit("/api/v3/order", 50, TimeSpan.FromSeconds(10), HttpMethod.Post, true)
                }
        };

        /// <summary>
        /// Usd futures API options
        /// </summary>
        public WhiteBitRestApiOptions UsdFuturesOptions { get; private set; } = new WhiteBitRestApiOptions();

        /// <summary>
        /// Coin futures API options
        /// </summary>
        public WhiteBitRestApiOptions CoinFuturesOptions { get; private set; } = new WhiteBitRestApiOptions();

        internal WhiteBitRestOptions Copy()
        {
            var options = Copy<WhiteBitRestOptions>();
            options.ReceiveWindow = ReceiveWindow;
            options.SpotOptions = SpotOptions.Copy();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy();
            options.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return options;
        }
    }
}
