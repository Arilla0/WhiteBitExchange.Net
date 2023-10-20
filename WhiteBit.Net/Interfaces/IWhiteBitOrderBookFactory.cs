using WhiteBit.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using System;

namespace WhiteBit.Net.Interfaces
{
    /// <summary>
    /// WhiteBit order book factory
    /// </summary>
    public interface IWhiteBitOrderBookFactory
    {
        /// <summary>
        /// Create a Spot SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<WhiteBitOrderBookOptions>? options = null);

        /// <summary>
        /// Create a Usdt Futures SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateUsdtFutures(string symbol, Action<WhiteBitOrderBookOptions>? options = null);

        /// <summary>
        /// Create a Coin Futures SymbolOrderBook
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook CreateCoinFutures(string symbol, Action<WhiteBitOrderBookOptions>? options = null);
    }
}