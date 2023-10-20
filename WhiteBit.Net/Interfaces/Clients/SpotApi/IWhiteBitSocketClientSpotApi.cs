using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

namespace WhiteBit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API socket subscriptions and requests
    /// </summary>
    public interface IWhiteBitSocketClientSpotApi
    {
        /// <summary>
        /// Factory for websockets
        /// </summary>
        IWebsocketFactory SocketFactory { get; set; }

        /// <summary>
        /// Account streams and queries
        /// </summary>
        IWhiteBitSocketClientSpotApiAccount Account { get; }
        /// <summary>
        /// Exchange data streams and queries
        /// </summary>
        IWhiteBitSocketClientSpotApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Trading data and queries
        /// </summary>
        IWhiteBitSocketClientSpotApiTrading Trading { get; }
        /// <summary>
        /// Set the API credentials for this API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="credentials"></param>
        void SetApiCredentials<T>(T credentials) where T : ApiCredentials;
    }
}