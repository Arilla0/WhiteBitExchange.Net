using WhiteBit.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using WhiteBit.Net.Interfaces.Clients.GeneralApi;
using WhiteBit.Net.Clients.SpotApi;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using WhiteBit.Net.Objects.Options;

namespace WhiteBit.Net.Clients.GeneralApi
{
    /// <inheritdoc cref="IWhiteBitRestClientGeneralApi" />
    public class WhiteBitRestClientGeneralApi : RestApiClient, IWhiteBitRestClientGeneralApi
    {
        #region fields 
        /// <inheritdoc />
        public new WhiteBitRestApiOptions ApiOptions => (WhiteBitRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new WhiteBitRestOptions ClientOptions => (WhiteBitRestOptions)base.ClientOptions;

        private readonly WhiteBitRestClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiBrokerage Brokerage { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiFutures Futures { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiSavings Savings { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiLoans CryptoLoans { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiMining Mining { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IWhiteBitRestClientGeneralApiStaking Staking { get; }
        #endregion

        #region constructor/destructor

        internal WhiteBitRestClientGeneralApi(ILogger logger, HttpClient? httpClient, WhiteBitRestClient baseClient, WhiteBitRestOptions options) 
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            Brokerage = new WhiteBitRestClientGeneralApiBrokerage(this);
            Futures = new WhiteBitRestClientGeneralApiFutures(this);
            Savings = new WhiteBitRestClientGeneralApiSavings(this);
            CryptoLoans = new WhiteBitRestClientGeneralApiLoans(this);
            Mining = new WhiteBitRestClientGeneralApiMining(this);
            SubAccount = new WhiteBitRestClientGeneralApiSubAccount(this);
            Staking = new WhiteBitRestClientGeneralApiStaking(this);

            requestBodyEmptyContent = "";
            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
        }

        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new WhiteBitAuthenticationProvider(credentials);

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result = result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            var result = await SendRequestAsync<T>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight, ignoreRatelimit: ignoreRateLimit).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                WhiteBitRestClientSpotApi._timeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }


        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), WhiteBitRestClientSpotApi._timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => WhiteBitRestClientSpotApi._timeSyncState.TimeOffset;

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, string data)
        {
            var errorData = ValidateJson(data);
            if (!errorData)
                return new ServerError(data);

            if (!errorData.Data.HasValues)
                return new ServerError(errorData.Data.ToString());

            if (errorData.Data["msg"] == null && errorData.Data["code"] == null)
                return new ServerError(errorData.Data.ToString());

            if (errorData.Data["msg"] != null && errorData.Data["code"] == null)
                return new ServerError((string)errorData.Data["msg"]!);

            return new ServerError((int)errorData.Data["code"]!, (string)errorData.Data["msg"]!);
        }
    }
}
