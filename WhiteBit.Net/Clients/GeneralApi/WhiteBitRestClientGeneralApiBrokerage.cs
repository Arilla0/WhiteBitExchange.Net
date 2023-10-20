using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces.Clients.GeneralApi;
using WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace WhiteBit.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class WhiteBitRestClientGeneralApiBrokerage : IWhiteBitRestClientGeneralApiBrokerage
    {
        private const string brokerageApi = "sapi";
        private const string brokerageVersion = "1";

        private const string subAccountEndpoint = "broker/subAccount";
        private const string brokerAccountInfoEndpoint = "broker/info";
        private const string enableMarginForSubAccountEndpoint = "broker/subAccount/margin";
        private const string enableFuturesForSubAccountEndpoint = "broker/subAccount/futures";
        private const string enableLeverageTokenForSubAccountEndpoint = "broker/subAccount/blvt";
        private const string apiKeyEndpoint = "broker/subAccountApi";
        private const string apiKeyPermissionEndpoint = "broker/subAccountApi/permission";
        private const string apiKeyCommissionEndpoint = "broker/subAccountApi/commission";
        private const string apiKeyCommissionFuturesEndpoint = "broker/subAccountApi/commission/futures";
        private const string apiKeyCommissionCoinFuturesEndpoint = "broker/subAccountApi/commission/coinFutures";
        private const string apiKeyIpRestrictionEndpoint = "broker/subAccountApi/ipRestriction";
        private const string apiKeyIpRestrictionListEndpoint = "broker/subAccountApi/ipRestriction/ipList";
        private const string transferEndpoint = "broker/transfer";
        private const string transferUniversalEndpoint = "broker/universalTransfer";
        private const string transferFuturesEndpoint = "broker/transfer/futures";
        private const string rebatesRecentEndpoint = "broker/rebate/recentRecord";
        private const string rebatesHistoryEndpoint = "broker/rebate/historicalRecord";
        private const string rebatesFuturesHistoryEndpoint = "broker/rebate/futures/recentRecord";
        private const string changeBnbBurnForSubAccountSpotAndMarginEndpoint = "broker/subAccount/bnbBurn/spot";
        private const string changeBnbBurnForSubAccountMarginInterestEndpoint = "broker/subAccount/bnbBurn/marginInterest";
        private const string bnbBurnForSubAccountStatusEndpoint = "broker/subAccount/bnbBurn/status";
        private const string spotSummaryEndpoint = "broker/subAccount/spotSummary";
        private const string marginSummaryEndpoint = "broker/subAccount/marginSummary";
        private const string futuresSummaryEndpoint = "broker/subAccount/futuresSummary";
        private const string depositHistoryEndpoint = "broker/subAccount/depositHist";

        private readonly WhiteBitRestClientGeneralApi _baseClient;

        internal WhiteBitRestClientGeneralApiBrokerage(WhiteBitRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Sub accounts

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSubAccountCreateResult>(_baseClient.GetUrl(subAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageSubAccount>>> GetSubAccountsAsync(string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageSubAccount>>(_baseClient.GetUrl(subAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts permissions

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"margin", true},  // only true for now
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageEnableMarginResult>(_baseClient.GetUrl(enableMarginForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"futures", true},  // only true for now
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageEnableFuturesResult>(_baseClient.GetUrl(enableFuturesForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageEnableLeverageTokenResult>> EnableLeverageTokenForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"blvt", true},  // only true for now
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageEnableLeverageTokenResult>(_baseClient.GetUrl(enableLeverageTokenForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts API keys

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string subAccountId, bool isSpotTradingEnabled,
            bool? isMarginTradingEnabled = null, bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"canTrade", isSpotTradingEnabled}
                             };
            parameters.AddOptionalParameter("marginTrade", isMarginTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("futuresTrade", isFuturesTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageApiKeyCreateResult>(_baseClient.GetUrl(apiKeyEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<object> DeleteSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(apiKeyEndpoint, brokerageApi, brokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSubAccountApiKey>> GetSubAccountApiKeyAsync(string subAccountId, string? apiKey = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                             };
            parameters.AddOptionalParameter("subAccountApiKey", apiKey);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("size", size);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSubAccountApiKey>(_baseClient.GetUrl(apiKeyEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSubAccountApiKey>> ChangeSubAccountApiKeyPermissionAsync(string subAccountId, string apiKey,
            bool isSpotTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"canTrade", isSpotTradingEnabled.ToString().ToLower()},
                                 {"marginTrade", isMarginTradingEnabled.ToString().ToLower()},
                                 {"futuresTrade", isFuturesTradingEnabled.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSubAccountApiKey>(_baseClient.GetUrl(apiKeyPermissionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageAddIpRestrictionResult>> AddIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, string ipAddress, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));
            ipAddress.ValidateNotNull(nameof(ipAddress));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipAddress", ipAddress}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageAddIpRestrictionResult>(_baseClient.GetUrl(apiKeyIpRestrictionListEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageIpRestriction>> ChangeIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, bool ipRestrict, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipRestrict", ipRestrict.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageIpRestriction>(_baseClient.GetUrl(apiKeyIpRestrictionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageIpRestriction>(_baseClient.GetUrl(apiKeyIpRestrictionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageIpRestrictionBase>> DeleteIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, string ipAddress, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));
            ipAddress.ValidateNotNull(nameof(ipAddress));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipAddress", ipAddress}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageIpRestrictionBase>(_baseClient.GetUrl(apiKeyIpRestrictionListEndpoint, brokerageApi, brokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts commission

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string subAccountId,
            decimal makerCommission, decimal takerCommission, decimal? marginMakerCommission = null, decimal? marginTakerCommission = null,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"makerCommission", makerCommission.ToString(CultureInfo.InvariantCulture)},
                                 {"takerCommission", takerCommission.ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("marginMakerCommission", marginMakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("marginTakerCommission", marginTakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSubAccountCommission>(_baseClient.GetUrl(apiKeyCommissionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string symbol,
            int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"symbol", symbol},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSubAccountFuturesCommission>(_baseClient.GetUrl(apiKeyCommissionFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string subAccountId,
            string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageSubAccountFuturesCommission>>(_baseClient.GetUrl(apiKeyCommissionFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSubAccountCoinFuturesCommission>> ChangeSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId,
            string pair, int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            pair.ValidateNotNull(nameof(pair));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"pair", pair},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSubAccountCoinFuturesCommission>(_baseClient.GetUrl(apiKeyCommissionCoinFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageSubAccountFuturesCommission>>> GetSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId,
            string? pair = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageSubAccountFuturesCommission>>(_baseClient.GetUrl(apiKeyCommissionCoinFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts asset summary

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageSpotAssetInfo>> GetSubAccountSpotAssetInfoAsync(
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageSpotAssetInfo>(_baseClient.GetUrl(spotSummaryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageMarginAssetInfo>> GetSubAccountMarginAssetInfoAsync(
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageMarginAssetInfo>(_baseClient.GetUrl(marginSummaryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageFuturesAssetInfo>> GetSubAccountFuturesAssetInfoAsync(WhiteBitBrokerageFuturesType futuresType,
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
                             {
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageFuturesAssetInfo>(_baseClient.GetUrl(futuresSummaryEndpoint, brokerageApi, "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts BNB burn

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageChangeBnbBurnSpotAndMarginResult>> ChangeBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"spotBNBBurn", spotBnbBurn.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageChangeBnbBurnSpotAndMarginResult>(_baseClient.GetUrl(changeBnbBurnForSubAccountSpotAndMarginEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageChangeBnbBurnMarginInterestResult>> ChangeBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"interestBNBBurn", interestBnbBurn.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageChangeBnbBurnMarginInterestResult>(_baseClient.GetUrl(changeBnbBurnForSubAccountMarginInterestEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageBnbBurnStatus>> GetBnbBurnStatusForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageBnbBurnStatus>(_baseClient.GetUrl(bnbBurnForSubAccountStatusEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Transfer & history

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageTransferResult>> TransferUniversalAsync(string asset, decimal quantity,
            string? fromId, BrokerageAccountType fromAccountType, string? toId, BrokerageAccountType toAccountType,
            string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", quantity.ToString(CultureInfo.InvariantCulture)},
                                 {"fromAccountType", JsonConvert.SerializeObject(fromAccountType, new BrokerageAccountTypeConverter(false))},
                                 {"toAccountType", JsonConvert.SerializeObject(toAccountType, new BrokerageAccountTypeConverter(false))}
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageTransferResult>(_baseClient.GetUrl(transferUniversalEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageTransferTransactionUniversal>>> GetTransferHistoryUniversalAsync(
            string? fromId = null, string? toId = null, string? clientTransferId = null, DateTime? startDate = null, DateTime? endDate = null,
            int? page = null, int? limit = null, bool showAllStatus = false, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
                             {
                                 {"showAllStatus", showAllStatus.ToString().ToLower()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageTransferTransactionUniversal>>(_baseClient.GetUrl(transferUniversalEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageTransferResult>> TransferAsync(string asset, decimal quantity,
            string? fromId, string? toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", quantity.ToString(CultureInfo.InvariantCulture)},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageTransferResult>(_baseClient.GetUrl(transferEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageTransferFuturesResult>> TransferFuturesAsync(string asset, decimal quantity, WhiteBitBrokerageFuturesType futuresType,
            string? fromId, string? toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", quantity.ToString(CultureInfo.InvariantCulture)},
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageTransferFuturesResult>(_baseClient.GetUrl(transferFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageTransferTransaction>>> GetTransferHistoryAsync(string? fromId = null, string? toId = null,
            string? clientTransferId = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, bool showAllStatus = false,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
                             {
                                 {"showAllStatus", showAllStatus.ToString().ToLower()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageTransferTransaction>>(_baseClient.GetUrl(transferEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageTransferFuturesTransactions>> GetTransferFuturesHistoryAsync(string subAccountId,
            WhiteBitBrokerageFuturesType futuresType, DateTime? startDate = null, DateTime? endDate = null,
            int? page = null, int? limit = null, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageTransferFuturesTransactions>(_baseClient.GetUrl(transferFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageSubAccountDepositTransaction>>> GetSubAccountDepositHistoryAsync(string? subAccountId = null,
            string? asset = null, WhiteBitBrokerageSubAccountDepositStatus? status = null, DateTime? startDate = null, DateTime? endDate = null,
            int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("status", status.HasValue ? ((int)status).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageSubAccountDepositTransaction>>(_baseClient.GetUrl(depositHistoryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Broker

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitBrokerageAccountInfo>(_baseClient.GetUrl(brokerAccountInfoEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Broker commission rebates

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string subAccountId,
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                             };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageRebate>>(_baseClient.GetUrl(rebatesRecentEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitBrokerageFuturesRebate>>> GetBrokerFuturesCommissionRebatesHistoryAsync(WhiteBitBrokerageFuturesType futuresType,
            DateTime startDate, DateTime endDate, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
                             {
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)},
                                 {"startTime", DateTimeConverter.ConvertToMilliseconds(startDate)!},
                                 {"endTime",  DateTimeConverter.ConvertToMilliseconds(endDate)!}
                             };
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitBrokerageFuturesRebate>>(_baseClient.GetUrl(rebatesFuturesHistoryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
