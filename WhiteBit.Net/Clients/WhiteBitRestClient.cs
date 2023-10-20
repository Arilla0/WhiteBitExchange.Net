using WhiteBit.Net.Objects;
using CryptoExchange.Net;
using WhiteBit.Net.Interfaces.Clients;
using WhiteBit.Net.Interfaces.Clients.UsdFuturesApi;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Interfaces.Clients.GeneralApi;
using WhiteBit.Net.Interfaces.Clients.CoinFuturesApi;
using WhiteBit.Net.Clients.GeneralApi;
using WhiteBit.Net.Clients.SpotApi;
using WhiteBit.Net.Clients.UsdFuturesApi;
using WhiteBit.Net.Clients.CoinFuturesApi;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using WhiteBit.Net.Objects.Options;
using CryptoExchange.Net.Authentication;

namespace WhiteBit.Net.Clients
{
    /// <inheritdoc cref="IWhiteBitRestClient" />
    public class WhiteBitRestClient : BaseRestClient, IWhiteBitRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApi GeneralApi { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientUsdFuturesApi UsdFuturesApi { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientCoinFuturesApi CoinFuturesApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the WhiteBitRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public WhiteBitRestClient(Action<WhiteBitRestOptions> optionsDelegate) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the WhiteBitRestClient using provided options
        /// </summary>
        public WhiteBitRestClient(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) : this(httpClient, loggerFactory, null)
        {
        }

        /// <summary>
        /// Create a new instance of the WhiteBitRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public WhiteBitRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<WhiteBitRestOptions>? optionsDelegate = null) : base(loggerFactory, "WhiteBit")
        {
            var options = WhiteBitRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            GeneralApi = AddApiClient(new WhiteBitRestClientGeneralApi(_logger, httpClient, this, options));
            SpotApi = AddApiClient(new WhiteBitRestClientSpotApi(_logger, httpClient, options));
            UsdFuturesApi = AddApiClient(new WhiteBitRestClientUsdFuturesApi(_logger, httpClient, options));
            CoinFuturesApi = AddApiClient(new WhiteBitRestClientCoinFuturesApi(_logger, httpClient, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<WhiteBitRestOptions> optionsDelegate)
        {
            var options = WhiteBitRestOptions.Default.Copy();
            optionsDelegate(options);
            WhiteBitRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            GeneralApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
            UsdFuturesApi.SetApiCredentials(credentials);
            CoinFuturesApi.SetApiCredentials(credentials);
        }
    }
}
