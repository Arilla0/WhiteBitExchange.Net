using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces.Clients.UsdFuturesApi;
using WhiteBit.Net.Objects.Models;
using WhiteBit.Net.Objects.Models.Futures;
using WhiteBit.Net.Objects.Models.Spot;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace WhiteBit.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    public class WhiteBitRestClientUsdFuturesApiAccount : IWhiteBitRestClientUsdFuturesApiAccount
    {
        private const string positionModeSideEndpoint = "positionSide/dual";
        private const string changeInitialLeverageEndpoint = "leverage";
        private const string incomeHistoryEndpoint = "income";
        private const string positionMarginEndpoint = "positionMargin";
        private const string positionMarginChangeHistoryEndpoint = "positionMargin/history";
        private const string changeMarginTypeEndpoint = "marginType";
        private const string leverageBracketEndpoint = "leverageBracket";
        private const string adlQuantileEndpoint = "adlQuantile";

        private const string getFuturesListenKeyEndpoint = "listenKey";
        private const string keepFuturesListenKeyAliveEndpoint = "listenKey";
        private const string closeFuturesListenKeyEndpoint = "listenKey";

        private const string accountInfoEndpoint = "account";
        private const string futuresAccountBalanceEndpoint = "balance";
        private const string futuresAccountMultiAssetsModeEndpoint = "multiAssetsMargin";
        private const string positionInformationEndpoint = "positionRisk";
        private const string tradingStatusEndpoint = "apiTradingStatus";
        private const string futuresAccountUserCommissionRateEndpoint = "commissionRate";

        private const string downloadIdEndpoint = "income/asyn";
        private const string downloadLinkEndpoint = "income/asyn/id";
        private const string downloadIdOrdersEndpoint = "order/asyn";
        private const string downloadLinkOrdersEndpoint = "order/asyn/id";
        private const string downloadIdTradeEndpoint = "trade/asyn";
        private const string downloadLinkTradeEndpoint = "trade/asyn/id";

        private const string api = "fapi";
        private const string signedVersion = "1";

        private readonly WhiteBitRestClientUsdFuturesApi _baseClient;

        internal WhiteBitRestClientUsdFuturesApiAccount(WhiteBitRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Change Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitResult>> ModifyPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "dualSidePosition", dualPositionSide.ToString().ToLower() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitResult>(_baseClient.GetUrl(positionModeSideEndpoint, api, signedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Current Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesPositionMode>(_baseClient.GetUrl(positionModeSideEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 30).ConfigureAwait(false);
        }

        #endregion

        #region Change Initial Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesInitialLeverageChangeResult>> ChangeInitialLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default)
        {
            leverage.ValidateIntBetween(nameof(leverage), 1, 125);
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "leverage", leverage }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesInitialLeverageChangeResult>(_baseClient.GetUrl(changeInitialLeverageEndpoint, api, signedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Change Margin Type

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesChangeMarginTypeResult>> ChangeMarginTypeAsync(string symbol, FuturesMarginType marginType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "marginType", JsonConvert.SerializeObject(marginType, new FuturesMarginTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesChangeMarginTypeResult>(_baseClient.GetUrl(changeMarginTypeEndpoint, api, signedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Modify Isolated Position Margin

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesPositionMarginResult>> ModifyPositionMarginAsync(string symbol, decimal quantity, FuturesMarginChangeDirectionType type, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "type", JsonConvert.SerializeObject(type, new FuturesMarginChangeDirectionTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("positionSide", positionSide == null ? null : JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesPositionMarginResult>(_baseClient.GetUrl(positionMarginEndpoint, api, signedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Postion Margin Change History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesMarginChangeHistoryResult>>> GetMarginChangeHistoryAsync(string symbol, FuturesMarginChangeDirectionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new FuturesMarginChangeDirectionTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesMarginChangeHistoryResult>>(_baseClient.GetUrl(positionMarginChangeHistoryEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Income History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesIncomeHistory>>> GetIncomeHistoryAsync(string? symbol = null, string? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("incomeType", incomeType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesIncomeHistory>>(_baseClient.GetUrl(incomeHistoryEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 30).ConfigureAwait(false);
        }

        #endregion

        #region Notional and Leverage Brackets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesSymbolBracket>>> GetBracketsAsync(string? symbolOrPair = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var url = _baseClient.GetUrl(leverageBracketEndpoint, api, signedVersion);
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter(url.ToString().Contains("dapi") ? "pair" : "symbol", symbolOrPair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesSymbolBracket>>(url, HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Position ADL Quantile Estimations

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesQuantileEstimation>>> GetPositionAdlQuantileEstimationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            if(symbol == null)            
                return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesQuantileEstimation>>(_baseClient.GetUrl(adlQuantileEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);

            var result = await _baseClient.SendRequestInternal<WhiteBitFuturesQuantileEstimation>(_baseClient.GetUrl(adlQuantileEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<WhiteBitFuturesQuantileEstimation>>(null);

            return result.As<IEnumerable<WhiteBitFuturesQuantileEstimation>>(new[] { result.Data });
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<WhiteBitListenKey>(_baseClient.GetUrl(getFuturesListenKeyEndpoint, api, signedVersion), HttpMethod.Post, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Keepalive User Data Stream

        /// <inheritdoc />
        public async Task<WebCallResult<object>> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(keepFuturesListenKeyAliveEndpoint, api, signedVersion), HttpMethod.Put, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Close User Data Stream

        /// <inheritdoc />
        public async Task<WebCallResult<object>> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(closeFuturesListenKeyEndpoint, api, signedVersion), HttpMethod.Delete, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Account Information

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesAccountInfo>(_baseClient.GetUrl(accountInfoEndpoint, api, "2"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        #endregion

        #region Future Account Balance

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitUsdFuturesAccountBalance>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitUsdFuturesAccountBalance>>(_baseClient.GetUrl(futuresAccountBalanceEndpoint, api, "2"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        #endregion

        #region Multi assets mode

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesMultiAssetMode>> GetMultiAssetsModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesMultiAssetMode>(_baseClient.GetUrl(futuresAccountMultiAssetsModeEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true, weight: 30).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitResult>> SetMultiAssetsModeAsync(bool enabled, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "multiAssetsMargin", enabled.ToString() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitResult>(_baseClient.GetUrl(futuresAccountMultiAssetsModeEndpoint, api, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Position Information

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitPositionDetailsUsdt>>> GetPositionInformationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitPositionDetailsUsdt>>(_baseClient.GetUrl(positionInformationEndpoint, api, "2"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        #endregion

        #region Trading status
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesTradingStatus>(_baseClient.GetUrl(tradingStatusEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);

        }
        #endregion

        #region Future Account User Commission Rate
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesAccountUserCommissionRate>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesAccountUserCommissionRate>(_baseClient.GetUrl(futuresAccountUserCommissionRateEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 20).ConfigureAwait(false);
        }
        #endregion

        #region Get download id for transaction history
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesDownloadIdInfo>> GetDownloadIdForTransactionHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesDownloadIdInfo>(_baseClient.GetUrl(downloadIdEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion

        #region Download transaction history
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesDownloadLink>> GetDownloadLinkForTransactionHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "downloadId", downloadId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesDownloadLink>(_baseClient.GetUrl(downloadLinkEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion

        #region Get download id for transaction history
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesDownloadIdInfo>> GetDownloadIdForOrderHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesDownloadIdInfo>(_baseClient.GetUrl(downloadIdOrdersEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion

        #region Download order history
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesDownloadLink>> GetDownloadLinkForOrderHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "downloadId", downloadId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesDownloadLink>(_baseClient.GetUrl(downloadLinkOrdersEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion

        #region Get download id for trade history
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesDownloadIdInfo>> GetDownloadIdForTradeHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesDownloadIdInfo>(_baseClient.GetUrl(downloadIdTradeEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion

        #region Download trade history
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesDownloadLink>> GetDownloadLinkForTradeHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "downloadId", downloadId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<WhiteBitFuturesDownloadLink>(_baseClient.GetUrl(downloadLinkTradeEndpoint, "fapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion
    }
}
