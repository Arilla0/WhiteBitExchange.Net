using WhiteBit.Net.Clients.CoinFuturesApi;
using WhiteBit.Net.Clients.SpotApi;
using WhiteBit.Net.Clients.UsdFuturesApi;
using WhiteBit.Net.Interfaces.Clients;
using WhiteBit.Net.Interfaces.Clients.CoinFuturesApi;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Interfaces.Clients.UsdFuturesApi;
using WhiteBit.Net.Objects;
using WhiteBit.Net.Objects.Options;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using System;

namespace WhiteBit.Net.Clients
{
    /// <inheritdoc cref="IWhiteBitSocketClient" />
    public class WhiteBitSocketClient : BaseSocketClient, IWhiteBitSocketClient
    {
        #region fields
        #endregion

        #region Api clients

        /// <inheritdoc />
        public IWhiteBitSocketClientSpotApi SpotApi { get; set; }

        /// <inheritdoc />
        public IWhiteBitSocketClientUsdFuturesApi UsdFuturesApi { get; set; }

        /// <inheritdoc />
        public IWhiteBitSocketClientCoinFuturesApi CoinFuturesApi { get; set; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of WhiteBitSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public WhiteBitSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of WhiteBitSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public WhiteBitSocketClient(Action<WhiteBitSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of WhiteBitSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public WhiteBitSocketClient(Action<WhiteBitSocketOptions> optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "WhiteBit")
        {
            var options = WhiteBitSocketOptions.Default.Copy();
            optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new WhiteBitSocketClientSpotApi(_logger, options));
            UsdFuturesApi = AddApiClient(new WhiteBitSocketClientUsdFuturesApi(_logger, options));
            CoinFuturesApi = AddApiClient(new WhiteBitSocketClientCoinFuturesApi(_logger, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<WhiteBitSocketOptions> optionsDelegate)
        {
            var options = WhiteBitSocketOptions.Default.Copy();
            optionsDelegate(options);
            WhiteBitSocketOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            UsdFuturesApi.SetApiCredentials(credentials);
            CoinFuturesApi.SetApiCredentials(credentials);
        }
    }
}
