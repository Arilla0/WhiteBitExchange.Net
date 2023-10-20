namespace WhiteBit.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class WhiteBitApiAddresses
    {
        /// <summary>
        /// The address used by the WhiteBitClient for the Spot API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WhiteBitSocketClient for the Spot streams
        /// </summary>
        public string SocketClientStreamAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WhiteBitSocketClient for the Spot API
        /// </summary>
        public string SocketClientApiAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WhiteBitSocketClient for connecting to the BLVT streams
        /// </summary>
        public string? BlvtSocketClientAddress { get; set; }
        /// <summary>
        /// The address used by the WhiteBitClient for the USD futures API
        /// </summary>
        public string? UsdFuturesRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the WhiteBitSocketClient for the USD futures API
        /// </summary>
        public string? UsdFuturesSocketClientAddress { get; set; }

        /// <summary>
        /// The address used by the WhiteBitClient for the COIN futures API
        /// </summary>
        public string? CoinFuturesRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the WhiteBitSocketClient for the Coin futures API
        /// </summary>
        public string? CoinFuturesSocketClientAddress { get; set; }

        /// <summary>
        /// The default addresses to connect to the WhiteBit.com API
        /// </summary>
        public static WhiteBitApiAddresses Default = new WhiteBitApiAddresses
        {
            RestClientAddress = "https://api.WhiteBit.com",
            SocketClientStreamAddress = "wss://stream.WhiteBit.com:9443/",
            SocketClientApiAddress = "wss://ws-api.WhiteBit.com:443/",
            BlvtSocketClientAddress = "wss://nbstream.WhiteBit.com/",
            UsdFuturesRestClientAddress = "https://fapi.WhiteBit.com",
            UsdFuturesSocketClientAddress = "wss://fstream.WhiteBit.com/",
            CoinFuturesRestClientAddress = "https://dapi.WhiteBit.com",
            CoinFuturesSocketClientAddress = "wss://dstream.WhiteBit.com/",
        };

        /// <summary>
        /// The addresses to connect to the WhiteBit testnet
        /// </summary>
        public static WhiteBitApiAddresses TestNet = new WhiteBitApiAddresses
        {
            RestClientAddress = "https://testnet.WhiteBit.vision",
            SocketClientStreamAddress = "wss://testnet.WhiteBit.vision",
            SocketClientApiAddress = "wss://testnet.WhiteBit.vision",
            BlvtSocketClientAddress = "wss://fstream.WhiteBitfuture.com",
            UsdFuturesRestClientAddress = "https://testnet.WhiteBitfuture.com",
            UsdFuturesSocketClientAddress = "wss://fstream.WhiteBitfuture.com",
            CoinFuturesRestClientAddress = "https://testnet.WhiteBitfuture.com",
            CoinFuturesSocketClientAddress = "wss://dstream.WhiteBitfuture.com",
        };

        /// <summary>
        /// The addresses to connect to WhiteBit.us. (WhiteBit.us futures not are not available)
        /// </summary>
        public static WhiteBitApiAddresses Us = new WhiteBitApiAddresses
        {
            RestClientAddress = "https://api.WhiteBit.us",
            SocketClientApiAddress = "wss://ws-api.WhiteBit.us:443",
            SocketClientStreamAddress = "wss://stream.WhiteBit.us:9443",
        };
    }
}
