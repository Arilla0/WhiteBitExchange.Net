using WhiteBit.Net.Clients.SpotApi;
using WhiteBit.Net.Interfaces.Clients.CoinFuturesApi;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Interfaces.Clients.UsdFuturesApi;
using WhiteBit.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

namespace WhiteBit.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the WhiteBit websocket API
    /// </summary>
    public interface IWhiteBitSocketClient: ISocketClient
    {
        /// <summary>
        /// Coin futures streams
        /// </summary>
        IWhiteBitSocketClientCoinFuturesApi CoinFuturesApi { get; }
        /// <summary>
        /// Spot streams and requests
        /// </summary>
        IWhiteBitSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usd futures streams
        /// </summary>
        IWhiteBitSocketClientUsdFuturesApi UsdFuturesApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
