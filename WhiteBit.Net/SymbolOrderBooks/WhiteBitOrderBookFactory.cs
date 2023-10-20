using WhiteBit.Net.Interfaces;
using WhiteBit.Net.Interfaces.Clients;
using WhiteBit.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace WhiteBit.Net.SymbolOrderBooks
{
    /// <summary>
    /// WhiteBit order book factory
    /// </summary>
    public class WhiteBitOrderBookFactory : IWhiteBitOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public WhiteBitOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<WhiteBitOrderBookOptions>? options = null)
            => new WhiteBitSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILogger<WhiteBitSpotSymbolOrderBook>>(),
                                             _serviceProvider.GetRequiredService<IWhiteBitRestClient>(),
                                             _serviceProvider.GetRequiredService<IWhiteBitSocketClient>());

        
        /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<WhiteBitOrderBookOptions>? options = null)
            => new WhiteBitFuturesUsdtSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILogger<WhiteBitFuturesUsdtSymbolOrderBook>>(),
                                             _serviceProvider.GetRequiredService<IWhiteBitRestClient>(),
                                             _serviceProvider.GetRequiredService<IWhiteBitSocketClient>());

        
        /// <inheritdoc />
        public ISymbolOrderBook CreateCoinFutures(string symbol, Action<WhiteBitOrderBookOptions>? options = null)
            => new WhiteBitFuturesCoinSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILogger<WhiteBitFuturesCoinSymbolOrderBook>>(),
                                             _serviceProvider.GetRequiredService<IWhiteBitRestClient>(),
                                             _serviceProvider.GetRequiredService<IWhiteBitSocketClient>());
    }
}
