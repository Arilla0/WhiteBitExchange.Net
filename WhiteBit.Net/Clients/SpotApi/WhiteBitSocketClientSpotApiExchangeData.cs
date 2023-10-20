
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Objects;
using WhiteBit.Net.Objects.Models;
using WhiteBit.Net.Objects.Models.Spot;
using WhiteBit.Net.Objects.Models.Spot.Blvt;
using WhiteBit.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WhiteBit.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class WhiteBitSocketClientSpotApiExchangeData : IWhiteBitSocketClientSpotApiExchangeData
    {
        private readonly ILogger _logger;
        private readonly WhiteBitSocketClientSpotApi _client;

        #region constructor/destructor

        internal WhiteBitSocketClientSpotApiExchangeData(ILogger logger, WhiteBitSocketClientSpotApi client)
        {
            _client = client;
            _logger = logger;
        }

        #endregion

        #region Queries

        #region Ping

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<object>>> PingAsync()
        {
            return await _client.QueryAsync<object>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ping", new Dictionary<string, object>()).ConfigureAwait(false);
        }

        #endregion

        #region Get Server Time

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<DateTime>>> GetServerTimeAsync()
        {
            var result = await _client.QueryAsync<WhiteBitCheckTime>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"time", new Dictionary<string, object>()).ConfigureAwait(false);
            if (!result)
                return result.AsError<WhiteBitResponse<DateTime>>(result.Error!);

            return result.As(new WhiteBitResponse<DateTime>
            {
                Ratelimits = result.Data!.Ratelimits!,
                Result = result.Data!.Result!.ServerTime!
            });
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<WhiteBitExchangeInfo>>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            var result = await _client.QueryAsync<WhiteBitExchangeInfo>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"exchangeInfo", parameters, weight: 20).ConfigureAwait(false);
            if (!result)
                return result;

            _client._exchangeInfo = result.Data.Result;
            _client._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return result;
        }

        #endregion

        #region Get Orderbook

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<WhiteBitOrderBook>>> GetOrderBookAsync(string symbol, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            int weight = limit <= 100 ? 2 : limit <= 500 ? 10 : limit <= 1000 ? 20 : 100;
            return await _client.QueryAsync<WhiteBitOrderBook>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"depth", parameters, weight: weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitRecentTradeQuote>>>> GetRecentTradesAsync(string symbol, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<IEnumerable<WhiteBitRecentTradeQuote>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.recent", parameters, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitRecentTradeQuote>>>> GetTradeHistoryAsync(string symbol, long? fromId = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);
            return await _client.QueryAsync<IEnumerable<WhiteBitRecentTradeQuote>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.historical", parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Get Aggregated Trades

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitStreamAggregatedTrade>>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("fromId", fromId);
            return await _client.QueryAsync<IEnumerable<WhiteBitStreamAggregatedTrade>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.aggregate", parameters, false, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitSpotKline>>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("interval", EnumConverter.GetString(interval));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _client.QueryAsync<IEnumerable<WhiteBitSpotKline>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"klines", parameters, false, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Get UI Klines

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitSpotKline>>>> GetUIKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("interval", EnumConverter.GetString(interval));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _client.QueryAsync<IEnumerable<WhiteBitSpotKline>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"uiKlines", parameters, false, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Get Average Price

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<WhiteBitAveragePrice>>> GetCurrentAvgPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _client.QueryAsync<WhiteBitAveragePrice>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"avgPrice", parameters, false, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBit24HPrice>>>> GetTickersAsync(IEnumerable<string>? symbols = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            var symbolCount = symbols?.Count();
            int weight = symbolCount == null || symbolCount > 100 ? 80 : symbolCount <= 20 ? 2 : 40;
            return await _client.QueryAsync<IEnumerable<WhiteBit24HPrice>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker.24hr", parameters, false, weight: weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Rolling Window Tickers

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitRollingWindowTick>>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            var symbolCount = symbols.Count();
            int weight = Math.Min(symbolCount * 4, 200);
            return await _client.QueryAsync<IEnumerable<WhiteBitRollingWindowTick>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker", parameters, false, weight: weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitBookPrice>>>> GetBookTickersAsync(IEnumerable<string>? symbols = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            return await _client.QueryAsync<IEnumerable<WhiteBitBookPrice>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker.book", parameters, false, weight: 4).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<DataEvent<WhiteBitStreamTrade>> onMessage, CancellationToken ct = default) =>
            await SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<WhiteBitStreamTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@trade").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol,
            Action<DataEvent<WhiteBitStreamAggregatedTrade>> onMessage, CancellationToken ct = default) =>
            await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<WhiteBitStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamAggregatedTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@aggTrade")
                .ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            KlineInterval interval, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            IEnumerable<KlineInterval> intervals, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols,
            KlineInterval interval, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols,
            IEnumerable<KlineInterval> intervals, Action<DataEvent<IWhiteBitStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamKlineData>>>(data => onMessage(data.As<IWhiteBitStreamKlineData>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.SelectMany(a =>
                intervals.Select(i =>
                    a.ToLower(CultureInfo.InvariantCulture) + "@kline" + "_" +
                    JsonConvert.SerializeObject(i, new KlineIntervalConverter(false)))).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol,
            Action<DataEvent<IWhiteBitMiniTick>> onMessage, CancellationToken ct = default) =>
            await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<IWhiteBitMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamMiniTick>>>(data => onMessage(data.As<IWhiteBitMiniTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@miniTicker")
                .ToArray();

            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(
            Action<DataEvent<IEnumerable<IWhiteBitMiniTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<WhiteBitCombinedStream<IEnumerable<WhiteBitStreamMiniTick>>>>(data => onMessage(data.As<IEnumerable<IWhiteBitMiniTick>>(data.Data.Data, data.Data.Stream)));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { "!miniTicker@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Market Rolling Window Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToRollingWindowTickerUpdatesAsync(string symbol, TimeSpan windowSize,
            Action<DataEvent<WhiteBitStreamRollingWindowTick>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamRollingWindowTick>>>(data => onMessage(data.As(data.Data.Data, data.Data.Stream)));
            var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { $"{symbol.ToLowerInvariant()}@ticker_{windowString}" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Rolling Window Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllRollingWindowTickerUpdatesAsync(TimeSpan windowSize,
            Action<DataEvent<IEnumerable<WhiteBitStreamRollingWindowTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<WhiteBitCombinedStream<IEnumerable<WhiteBitStreamRollingWindowTick>>>>(data => onMessage(data.As(data.Data.Data, data.Data.Stream)));
            var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { $"!ticker_{windowString}@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol,
            Action<DataEvent<WhiteBitStreamBookPrice>> onMessage, CancellationToken ct = default) =>
            await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<WhiteBitStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@bookTicker").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol,
            int levels, int? updateInterval, Action<DataEvent<IWhiteBitOrderBook>> onMessage, CancellationToken ct = default) =>
            await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage, ct)
                .ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(
            IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IWhiteBitOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitOrderBook>>>(data =>
            {
                data.Data.Data.Symbol = data.Data.Stream.Split('@')[0];
                onMessage(data.As<IWhiteBitOrderBook>(data.Data.Data, data.Data.Data.Symbol));
            });

            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Diff. Depth Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol,
            int? updateInterval, Action<DataEvent<IWhiteBitEventOrderBook>> onMessage, CancellationToken ct = default) =>
            await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols,
            int? updateInterval, Action<DataEvent<IWhiteBitEventOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateWhiteBitSymbol();

            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);
            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitEventOrderBook>>>(data => onMessage(data.As<IWhiteBitEventOrderBook>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + "@depth" +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IWhiteBitTick>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IWhiteBitTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamTick>>>(data => onMessage(data.As<IWhiteBitTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@ticker").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IWhiteBitTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<WhiteBitCombinedStream<IEnumerable<WhiteBitStreamTick>>>>(data => onMessage(data.As<IEnumerable<IWhiteBitTick>>(data.Data.Data, data.Data.Stream)));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { "!ticker@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Blvt info update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token,
            Action<DataEvent<WhiteBitBlvtInfoUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToBlvtInfoUpdatesAsync(new List<string> { token }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<WhiteBitBlvtInfoUpdate>> onMessage, CancellationToken ct = default)
        {
            var address = _client.ClientOptions.Environment.BlvtSocketAddress ?? throw new Exception("No url found for Blvt stream, check the `BlvtSocketAddress` in the client environment");

            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + "@tokenNav").ToArray();
            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitBlvtInfoUpdate>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.TokenName)));
            return await _client.SubscribeAsync(address.AppendPath("lvt-p"), tokens, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Blvt kline update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token,
            KlineInterval interval, Action<DataEvent<WhiteBitStreamKlineData>> onMessage, CancellationToken ct = default) =>
            SubscribeToBlvtKlineUpdatesAsync(new List<string> { token }, interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<WhiteBitStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            var address = _client.ClientOptions.Environment.BlvtSocketAddress ?? throw new Exception("No url found for Blvt stream, check the `BlvtSocketAddress` in the client environment");

            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + "@nav_kline" + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            var handler = new Action<DataEvent<WhiteBitCombinedStream<WhiteBitStreamKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await _client.SubscribeAsync(address.AppendPath("lvt-p"), tokens, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}
