using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;

namespace WhiteBit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// WhiteBit Spot API endpoints
    /// </summary>
    public interface IWhiteBitRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IWhiteBitRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public IWhiteBitRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IWhiteBitRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        public ISpotClient CommonSpotClient { get; }
    }
}
