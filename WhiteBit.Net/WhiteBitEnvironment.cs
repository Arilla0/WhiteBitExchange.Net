using WhiteBit.Net.Objects;
using CryptoExchange.Net.Objects;

namespace WhiteBit.Net
{
    /// <summary>
    /// WhiteBit environments
    /// </summary>
    public class WhiteBitEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot Rest API address
        /// </summary>
        public string SpotRestAddress { get; }

        /// <summary>
        /// Spot Socket Streams address
        /// </summary>
        public string SpotSocketStreamAddress { get; }

        /// <summary>
        /// Spot Socket API address
        /// </summary>
        public string SpotSocketApiAddress { get; }

        /// <summary>
        /// Blvt Socket API address
        /// </summary>
        public string? BlvtSocketAddress { get; }

        /// <summary>
        /// Usd futures Rest address
        /// </summary>
        public string? UsdFuturesRestAddress { get; }

        /// <summary>
        /// Usd futures Socket address
        /// </summary>
        public string? UsdFuturesSocketAddress { get; }

        /// <summary>
        /// Coin futures Rest address
        /// </summary>
        public string? CoinFuturesRestAddress { get; }

        /// <summary>
        /// Coin futures Socket address
        /// </summary>
        public string? CoinFuturesSocketAddress { get; }

        internal WhiteBitEnvironment(
            string name, 
            string spotRestAddress, 
            string spotSocketStreamAddress, 
            string spotSocketApiAddress,
            string? blvtSocketAddress, 
            string? usdFuturesRestAddress, 
            string? usdFuturesSocketAddress,
            string? coinFuturesRestAddress,
            string? coinFuturesSocketAddress) :
            base(name)
        {
            SpotRestAddress = spotRestAddress;
            SpotSocketStreamAddress = spotSocketStreamAddress;
            SpotSocketApiAddress = spotSocketApiAddress;
            BlvtSocketAddress = blvtSocketAddress;
            UsdFuturesRestAddress = usdFuturesRestAddress;
            UsdFuturesSocketAddress = usdFuturesSocketAddress;
            CoinFuturesRestAddress = coinFuturesRestAddress;
            CoinFuturesSocketAddress = coinFuturesSocketAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static WhiteBitEnvironment Live { get; } 
            = new WhiteBitEnvironment(TradeEnvironmentNames.Live, 
                                     WhiteBitApiAddresses.Default.RestClientAddress,
                                     WhiteBitApiAddresses.Default.SocketClientStreamAddress,
                                     WhiteBitApiAddresses.Default.SocketClientApiAddress,
                                     WhiteBitApiAddresses.Default.BlvtSocketClientAddress,
                                     WhiteBitApiAddresses.Default.UsdFuturesRestClientAddress,
                                     WhiteBitApiAddresses.Default.UsdFuturesSocketClientAddress,
                                     WhiteBitApiAddresses.Default.CoinFuturesRestClientAddress,
                                     WhiteBitApiAddresses.Default.CoinFuturesSocketClientAddress);

        /// <summary>
        /// Testnet environment
        /// </summary>
        public static WhiteBitEnvironment Testnet { get; }
            = new WhiteBitEnvironment(TradeEnvironmentNames.Testnet,
                                     WhiteBitApiAddresses.TestNet.RestClientAddress,
                                     WhiteBitApiAddresses.TestNet.SocketClientStreamAddress,
                                     WhiteBitApiAddresses.TestNet.SocketClientApiAddress,
                                     WhiteBitApiAddresses.TestNet.BlvtSocketClientAddress,
                                     WhiteBitApiAddresses.TestNet.UsdFuturesRestClientAddress,
                                     WhiteBitApiAddresses.TestNet.UsdFuturesSocketClientAddress,
                                     WhiteBitApiAddresses.TestNet.CoinFuturesRestClientAddress,
                                     WhiteBitApiAddresses.TestNet.CoinFuturesSocketClientAddress);

        /// <summary>
        /// WhiteBit.us environment
        /// </summary>
        public static WhiteBitEnvironment Us { get; }
            = new WhiteBitEnvironment("Us",
                                     WhiteBitApiAddresses.Us.RestClientAddress,
                                     WhiteBitApiAddresses.Us.SocketClientStreamAddress,
                                     WhiteBitApiAddresses.Us.SocketClientApiAddress,
                                     null,
                                     null,
                                     null,
                                     null,
                                     null);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <param name="spotSocketApiAddress"></param>
        /// <param name="blvtSocketAddress"></param>
        /// <param name="usdFuturesRestAddress"></param>
        /// <param name="usdFuturesSocketAddress"></param>
        /// <param name="coinFuturesRestAddress"></param>
        /// <param name="coinFuturesSocketAddress"></param>
        /// <returns></returns>
        public static WhiteBitEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress,
                        string spotSocketApiAddress,
                        string? blvtSocketAddress,
                        string? usdFuturesRestAddress,
                        string? usdFuturesSocketAddress,
                        string? coinFuturesRestAddress,
                        string? coinFuturesSocketAddress)
            => new WhiteBitEnvironment(name, spotRestAddress, spotSocketStreamsAddress, spotSocketApiAddress, blvtSocketAddress, usdFuturesRestAddress, usdFuturesSocketAddress, coinFuturesRestAddress, coinFuturesSocketAddress);
    }
}
