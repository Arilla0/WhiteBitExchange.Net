using WhiteBit.Net.Interfaces.Clients.CoinFuturesApi;
using WhiteBit.Net.Interfaces.Clients.GeneralApi;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Interfaces.Clients.UsdFuturesApi;
using WhiteBit.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

namespace WhiteBit.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the WhiteBit Rest API. 
    /// </summary>
    public interface IWhiteBitRestClient: IRestClient
    {
        /// <summary>
        /// General API endpoints
        /// </summary>
        IWhiteBitRestClientGeneralApi GeneralApi { get; }
        /// <summary>
        /// Coin futures API endpoints
        /// </summary>
        IWhiteBitRestClientCoinFuturesApi CoinFuturesApi { get; }
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        IWhiteBitRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usd futures API endpoints
        /// </summary>
        IWhiteBitRestClientUsdFuturesApi UsdFuturesApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
