using CryptoExchange.Net.Objects.Options;
using System.Collections.Generic;
using System.Net.Http;
using System;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace WhiteBit.Net.Objects.Options
{
    /// <summary>
    /// Options for the WhiteBitSocketClient
    /// </summary>
    public class WhiteBitSocketOptions : SocketExchangeOptions<WhiteBitEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static WhiteBitSocketOptions Default { get; set; } = new WhiteBitSocketOptions()
        {
            Environment = WhiteBitEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Options for the Spot API
        /// </summary>
        public WhiteBitSocketApiOptions SpotOptions { get; private set; } = new WhiteBitSocketApiOptions()
        {
            RateLimiters = new List<IRateLimiter>
            {
                new RateLimiter()
                    .AddConnectionRateLimit("stream.WhiteBit.com", 5, TimeSpan.FromSeconds(1))
                    .AddConnectionRateLimit("ws-api.WhiteBit.com", 1200, TimeSpan.FromSeconds(60))
            }
        };

        /// <summary>
        /// Options for the Usd Futures API
        /// </summary>
        public WhiteBitSocketApiOptions UsdFuturesOptions { get; private set; } = new WhiteBitSocketApiOptions();

        /// <summary>
        /// Options for the Coin Futures API
        /// </summary>
        public WhiteBitSocketApiOptions CoinFuturesOptions { get; private set; } = new WhiteBitSocketApiOptions(); 

        internal WhiteBitSocketOptions Copy()
        {
            var options = Copy<WhiteBitSocketOptions>();
            options.SpotOptions = SpotOptions.Copy();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy();
            options.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return options;
        }
    }
}
