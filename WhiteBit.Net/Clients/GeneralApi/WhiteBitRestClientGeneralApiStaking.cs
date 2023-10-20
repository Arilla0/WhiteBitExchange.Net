using WhiteBit.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Objects;
using WhiteBit.Net.Objects.Models.Spot.Staking;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net;
using WhiteBit.Net.Interfaces.Clients.GeneralApi;
using WhiteBit.Net.Objects.Models;

namespace WhiteBit.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class WhiteBitRestClientGeneralApiStaking : IWhiteBitRestClientGeneralApiStaking
    {
        private readonly WhiteBitRestClientGeneralApi _baseClient;

        internal WhiteBitRestClientGeneralApiStaking(WhiteBitRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingResult>> SetAutoStakingAsync(StakingProductType product, string positionId, bool renewable, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "positionId", positionId },
                { "renewable", renewable },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingResult>(_baseClient.GetUrl("staking/setAutoStaking", "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingPersonalQuota>> GetStakingPersonalQuotaAsync(StakingProductType product, string productId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "productId", productId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingPersonalQuota>(_baseClient.GetUrl("staking/personalLeftQuota", "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitStakingProduct>>> GetStakingProductsAsync(StakingProductType product, string? asset = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitStakingProduct>>(_baseClient.GetUrl("staking/productList", "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingPositionResult>> PurchaseStakingProductAsync(StakingProductType product, string productId, decimal quantity, bool? renewable = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "productId", productId },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("renewable", renewable);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingPositionResult>(_baseClient.GetUrl("staking/purchase", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingResult>> RedeemStakingProductAsync(StakingProductType product, string productId, string? positionId = null, decimal? quantity = null, bool? renewable = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "productId", productId },
            };
            parameters.AddOptionalParameter("positionId", positionId);
            parameters.AddOptionalParameter("amount", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("renewable", renewable);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingResult>(_baseClient.GetUrl("staking/redeem", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitStakingPosition>>> GetStakingPositionsAsync(StakingProductType product, string? productId = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) }
            };
            parameters.AddOptionalParameter("productId", productId);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitStakingPosition>>(_baseClient.GetUrl("staking/position", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitStakingHistory>>> GetStakingHistoryAsync(StakingProductType product, StakingTransactionType transactionType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) },
                { "txnType", EnumConverter.GetString(transactionType) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitStakingHistory>>(_baseClient.GetUrl("staking/stakingRecord", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingResult>(_baseClient.GetUrl("eth-staking/eth/stake", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingResult>(_baseClient.GetUrl("eth-staking/eth/redeem", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitEthStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitEthStakingHistory>>(_baseClient.GetUrl("eth-staking/eth/history/stakingHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitEthRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitEthRedemptionHistory>>(_baseClient.GetUrl("eth-staking/eth/history/redemptionHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitEthRewardsHistory>>(_baseClient.GetUrl("eth-staking/eth/history/rewardsHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitEthStakingQuota>(_baseClient.GetUrl("eth-staking/eth/quota", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitBethRateHistory>>(_baseClient.GetUrl("eth-staking/eth/history/rateHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitEthStakingAccount>(_baseClient.GetUrl("eth-staking/account", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingResult>(_baseClient.GetUrl("eth-staking/wbeth/wrap", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitStakingResult>> UnwrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitStakingResult>(_baseClient.GetUrl("eth-staking/wbeth/unwrap", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitBethWrapHistory>>(_baseClient.GetUrl("eth-staking/wbeth/history/wrapHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitBethWrapHistory>>(_baseClient.GetUrl("eth-staking/wbeth/history/unwrapHistory ", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }
    }
}
