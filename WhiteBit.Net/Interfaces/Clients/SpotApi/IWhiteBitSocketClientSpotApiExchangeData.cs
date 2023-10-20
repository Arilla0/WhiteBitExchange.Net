using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces;
using WhiteBit.Net.Objects;
using WhiteBit.Net.Objects.Models.Spot;
using WhiteBit.Net.Objects.Models.Spot.Blvt;
using WhiteBit.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WhiteBit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// WhiteBit Spot Exchange Data socket requests and subscriptions
    /// </summary>
    public interface IWhiteBitSocketClientSpotApiExchangeData
    {
        /// <summary>
        /// Ping to test connection
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#test-connectivity" /></para>
        /// </summary>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<object>>> PingAsync();

        /// <summary>
        /// Get the server time
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#check-server-time" /></para>
        /// </summary>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<DateTime>>> GetServerTimeAsync();

        /// <summary>
        /// Gets information about the exchange including rate limits and symbol list
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#exchange-information" /></para>
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<WhiteBitExchangeInfo>>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the same time, from the same order, with the same price will have the quantity aggregated.
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#aggregate-trades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="fromId">Filter by from trade id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitStreamAggregatedTrade>>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);
        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitBookPrice>>>> GetBookTickersAsync(IEnumerable<string>? symbols = null);
        /// <summary>
        /// Gets current average price for a symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#current-average-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<WhiteBitAveragePrice>>> GetCurrentAvgPriceAsync(string symbol);
        /// <summary>
        /// Get candlestick data for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#klines" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitSpotKline>>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);
        /// <summary>
        /// Gets the order book for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#order-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="limit">Number of entries</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<WhiteBitOrderBook>>> GetOrderBookAsync(string symbol, int? limit = null);
        /// <summary>
        /// Gets the recent trades for a symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#recent-trades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="limit">Max results</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitRecentTradeQuote>>>> GetRecentTradesAsync(string symbol, int? limit = null);
        /// <summary>
        /// Get data based on the last x time, specified as windowSize
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#rolling-window-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitRollingWindowTick>>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols);
        /// <summary>
        /// Get data regarding the last 24 hours
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBit24HPrice>>>> GetTickersAsync(IEnumerable<string>? symbols = null);
        /// <summary>
        /// Gets the historical trades for a symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#historical-trades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="fromId">Filter by from trade id</param>
        /// <param name="limit">Max results</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitRecentTradeQuote>>>> GetTradeHistoryAsync(string symbol, long? fromId = null, int? limit = null);
        /// <summary>
        /// Get candlestick data for the provided symbol. Returns modified kline data, optimized for the presentation of candlestick charts
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/websocket_api/en/#ui-klines" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <returns></returns>
        Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitSpotKline>>>> GetUIKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#aggregate-trade-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WhiteBitStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#aggregate-trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<WhiteBitStreamAggregatedTrade>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#all-market-mini-tickers-stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IWhiteBitMiniTick>>> onMessage, CancellationToken ct = default);
        
        /// <summary>
        /// Subscribe to rolling window ticker updates stream for all symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#all-market-rolling-window-statistics-streams" /></para>
        /// </summary>
        /// <param name="windowSize">Window size, either 1 hour or 4 hours</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllRollingWindowTickerUpdatesAsync(TimeSpan windowSize, Action<DataEvent<IEnumerable<WhiteBitStreamRollingWindowTick>>> onMessage, CancellationToken ct = default);
        
        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#all-market-tickers-stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IWhiteBitTick>>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to leveraged token info updates
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/futures/en/#blvt-info-streams" /></para>
        /// </summary>
        /// <param name="tokens">The tokens to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<WhiteBitBlvtInfoUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to leveraged token info updates
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/futures/en/#blvt-info-streams" /></para>
        /// </summary>
        /// <param name="token">The token to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token, Action<DataEvent<WhiteBitBlvtInfoUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to leveraged token kline updates
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/futures/en/#blvt-nav-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="tokens">The tokens to subscribe to</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<WhiteBitStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to leveraged token kline updates
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/futures/en/#blvt-nav-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="token">The token to subscribe to</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token, KlineInterval interval, Action<DataEvent<WhiteBitStreamKlineData>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WhiteBitStreamBookPrice>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<WhiteBitStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols and intervals
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol and intervals
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IWhiteBitMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IWhiteBitMiniTick>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IWhiteBitEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#diff-depth-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IWhiteBitEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IWhiteBitOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IWhiteBitOrderBook>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to rolling window ticker updates stream for a symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-rolling-window-statistics-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="windowSize">Window size, either 1 hour or 4 hours</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToRollingWindowTickerUpdatesAsync(string symbol, TimeSpan windowSize, Action<DataEvent<WhiteBitStreamRollingWindowTick>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IWhiteBitTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IWhiteBitTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WhiteBitStreamTrade>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<WhiteBitStreamTrade>> onMessage, CancellationToken ct = default);
    }
}