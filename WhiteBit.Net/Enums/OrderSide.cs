using CryptoExchange.Net.Attributes;

namespace WhiteBit.Net.Enums
{
    /// <summary>
    /// The side of an order
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL")]
        Sell
    }
}
