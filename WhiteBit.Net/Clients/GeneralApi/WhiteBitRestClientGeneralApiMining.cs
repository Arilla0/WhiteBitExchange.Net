using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces.Clients.GeneralApi;
using WhiteBit.Net.Objects.Models;
using WhiteBit.Net.Objects.Models.Spot.Mining;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace WhiteBit.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class WhiteBitRestClientGeneralApiMining : IWhiteBitRestClientGeneralApiMining
    {
        private const string coinListEndpoint = "mining/pub/coinList";
        private const string algorithmEndpoint = "mining/pub/algoList";
        private const string minerDetailsEndpoint = "mining/worker/detail";
        private const string minerListEndpoint = "mining/worker/list";
        private const string miningRevenueEndpoint = "mining/payment/list";
        private const string miningOtherRevenueEndpoint = "mining/payment/other";
        private const string miningStatisticsEndpoint = "mining/statistics/user/status";
        private const string miningAccountListEndpoint = "mining/statistics/user/list";
        private const string miningHashrateResaleListEndpoint = "mining/hash-transfer/config/details/list";
        private const string miningHashrateResaleDetailsEndpoint = "mining/hash-transfer/profit/details";
        private const string miningHashrateResaleRequest = "mining/hash-transfer/config";
        private const string miningHashrateResaleCancel = "mining/hash-transfer/config/cancel";

        private readonly WhiteBitRestClientGeneralApi _baseClient;

        internal WhiteBitRestClientGeneralApiMining(WhiteBitRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }


        #region Acquiring CoinName
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitMiningCoin>>> GetMiningCoinListAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            var result = await _baseClient.SendRequestInternal<WhiteBitResult<IEnumerable<WhiteBitMiningCoin>>>(_baseClient.GetUrl(coinListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<WhiteBitMiningCoin>>(default);

            if (result.Data?.Code != 0)
                return result.AsError<IEnumerable<WhiteBitMiningCoin>>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion Acquiring CoinName

        #region Acquiring Algorithm 
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitMiningAlgorithm>>> GetMiningAlgorithmListAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            var result = await _baseClient.SendRequestInternal<WhiteBitResult<IEnumerable<WhiteBitMiningAlgorithm>>>(_baseClient.GetUrl(algorithmEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<WhiteBitMiningAlgorithm>>(default);

            if (result.Data?.Code != 0)
                return result.AsError<IEnumerable<WhiteBitMiningAlgorithm>>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Request Detail Miner List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitMinerDetails>>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));
            workerName.ValidateNotNull(nameof(workerName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"workerName", workerName}
            };

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<IEnumerable<WhiteBitMinerDetails>>>(_baseClient.GetUrl(minerDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<WhiteBitMinerDetails>>(default);

            if (result.Data?.Code != 0)
                return result.AsError<IEnumerable<WhiteBitMinerDetails>>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Request Miner List
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = null, bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sortAscending", sortAscending == null ? null : sortAscending == true ? "1" : "0");
            parameters.AddOptionalParameter("sortColumn", sortColumn);
            parameters.AddOptionalParameter("workerStatus", workerStatus == null ? null : JsonConvert.SerializeObject(workerStatus, new MinerStatusConverter(false)));

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitMinerList>>(_baseClient.GetUrl(minerListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitMinerList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<WhiteBitMinerList>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Revenue List
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endDate));

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitRevenueList>>(_baseClient.GetUrl(miningRevenueEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitRevenueList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<WhiteBitRevenueList>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Other Revenue List
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitOtherRevenueList>> GetMiningOtherRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endDate));

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitOtherRevenueList>>(_baseClient.GetUrl(miningOtherRevenueEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitOtherRevenueList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<WhiteBitOtherRevenueList>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Statistics list
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitMiningStatistic>>(_baseClient.GetUrl(miningStatisticsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitMiningStatistic>(default);

            if (result.Data?.Code != 0)
                return result.AsError<WhiteBitMiningStatistic>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Account List
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitMiningAccount>>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<IEnumerable<WhiteBitMiningAccount>>>(_baseClient.GetUrl(miningAccountListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<WhiteBitMiningAccount>>(default);

            if (result.Data?.Code != 0)
                return result.AsError<IEnumerable<WhiteBitMiningAccount>>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Hashrate Resale List
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitHashrateResaleList>> GetHashrateResaleListAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitHashrateResaleList>>(_baseClient.GetUrl(miningHashrateResaleListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitHashrateResaleList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<WhiteBitHashrateResaleList>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Hashrate Resale Details
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitHashrateResaleDetails>> GetHashrateResaleDetailsAsync(int configId, string userName, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                { "configId", configId.ToString(CultureInfo.InvariantCulture) },
                { "userName", userName }
            };

            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitHashrateResaleDetails>>(_baseClient.GetUrl(miningHashrateResaleDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitHashrateResaleDetails>(default);

            if (result.Data?.Code != 0)
                result.AsError<WhiteBitHashrateResaleDetails>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Hashrate Resale Request

        /// <inheritdoc />
        public async Task<WebCallResult<int>> PlaceHashrateResaleRequestAsync(string userName, string algorithm, DateTime startDate, DateTime endDate, string toUser, decimal hashRate, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));
            algorithm.ValidateNotNull(nameof(algorithm));
            toUser.ValidateNotNull(nameof(toUser));

            var parameters = new Dictionary<string, object>()
            {
                { "userName", userName },
                { "algo", algorithm },
                { "startDate", DateTimeConverter.ConvertToMilliseconds(startDate)! },
                { "endDate", DateTimeConverter.ConvertToMilliseconds(endDate)! },
                { "toPoolUser", toUser },
                { "hashRate", hashRate }
            };

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<int>>(_baseClient.GetUrl(miningHashrateResaleRequest, "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<int>(default);

            if (result.Data?.Code != 0)
                return result.AsError<int>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Cancel Hashrate Resale Configuration

        /// <inheritdoc />
        public async Task<WebCallResult<bool>> CancelHashrateResaleRequestAsync(int configId, string userName, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                { "configId", configId },
                { "userName", userName }
            };

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<bool>>(_baseClient.GetUrl(miningHashrateResaleCancel, "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<bool>(default);

            if (result.Data?.Code != 0)
                return result.AsError<bool>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Acquiring CoinName
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitMiningEarnings>> GetMiningAccountEarningsAsync(string algo, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "algo", algo }
            };
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<WhiteBitResult<WhiteBitMiningEarnings>>(_baseClient.GetUrl("mining/payment/uid", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!result.Success)
                return result.As<WhiteBitMiningEarnings>(default);

            if (result.Data?.Code != 0)
                return result.AsError<WhiteBitMiningEarnings>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion Acquiring CoinName
    }
}
