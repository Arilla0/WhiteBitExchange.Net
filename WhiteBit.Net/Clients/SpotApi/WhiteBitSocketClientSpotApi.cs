using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Objects;
using WhiteBit.Net.Objects.Internal;
using WhiteBit.Net.Objects.Models.Spot;
using WhiteBit.Net.Objects.Options;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WhiteBit.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class WhiteBitSocketClientSpotApi : SocketApiClient, IWhiteBitSocketClientSpotApi
    {
        #region fields
        /// <inheritdoc />
        public new WhiteBitSocketOptions ClientOptions => (WhiteBitSocketOptions)base.ClientOptions;
        /// <inheritdoc />
        public new WhiteBitSocketApiOptions ApiOptions => (WhiteBitSocketApiOptions)base.ApiOptions;

        internal WhiteBitExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;
        #endregion

        /// <inheritdoc />
        public IWhiteBitSocketClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IWhiteBitSocketClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IWhiteBitSocketClientSpotApiTrading Trading { get; }

        #region constructor/destructor

        internal WhiteBitSocketClientSpotApi(ILogger logger, WhiteBitSocketOptions options) :
            base(logger, options.Environment.SpotSocketStreamAddress, options, options.SpotOptions)
        {
            SetDataInterpreter((data) => string.Empty, null);

            Account = new WhiteBitSocketClientSpotApiAccount(logger, this);
            ExchangeData = new WhiteBitSocketClientSpotApiExchangeData(logger, this);
            Trading = new WhiteBitSocketClientSpotApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new WhiteBitAuthenticationProvider(credentials);

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var request = new WhiteBitSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = topics.ToArray(),
                Id = ExchangeHelpers.NextId()
            };

            return SubscribeAsync(url.AppendPath("stream"), request, null, false, onData, ct);
        }

        internal Task<CallResult<WhiteBitResponse<T>>> QueryAsync<T>(string url, string method, Dictionary<string, object> parameters, bool authenticated = false, bool sign = false, int weight = 1)
        {
            if (authenticated)
            {
                if (AuthenticationProvider == null)
                    throw new InvalidOperationException("No credentials provided for authenticated endpoint");

                var authProvider = (WhiteBitAuthenticationProvider)AuthenticationProvider;
                if (sign)
                {
                    parameters = authProvider.AuthenticateSocketParameters(parameters);
                }
                else
                {
                    parameters.Add("apiKey", authProvider.GetApiKey());
                }
            }

            var request = new WhiteBitSocketQuery
            {
                Method = method,
                Params = parameters,
                Id = ExchangeHelpers.NextId()
            };

            return QueryAsync<WhiteBitResponse<T>>(url, request, false, weight);
        }

        internal CallResult<T> DeserializeInternal<T>(JToken obj, JsonSerializer? serializer = null, int? requestId = null)
        {
            return base.Deserialize<T>(obj, serializer, requestId);
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = null!;
            var bRequest = (WhiteBitSocketQuery)request;
            if (bRequest.Id != data["id"]?.Value<int>())
                return false;

            var status = data["status"]?.Value<int>();
            if (status != 200)
            {
                var error = data["error"]!;

                if (status == 429 || status == 418)
                {
                    DateTime? retryAfter = null;
                    var retryAfterVal = error["data"]?["retryAfter"]?.ToString();
                    if (long.TryParse(retryAfterVal, out var retryAfterMs))
                        retryAfter = DateTimeConverter.ConvertFromMilliseconds(retryAfterMs);

                    callResult = new CallResult<T>(new ServerRateLimitError(error["msg"]!.Value<string>()!)
                    {
                        RetryAfter = retryAfter
                    });
                }
                else
                    callResult = new CallResult<T>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.Value<string>()!));
                return true;
            }
            callResult = Deserialize<T>(data!);
            return true;
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var bRequest = (WhiteBitSocketRequest)request;
            if ((int)id != bRequest.Id)
                return false;

            var result = message["result"];
            if (result != null && result.Type == JTokenType.Null)
            {
                _logger.Log(LogLevel.Trace, $"Socket {s.SocketId} Subscription completed");
                callResult = new CallResult<object>(new object());
                return true;
            }

            var error = message["error"];
            if (error == null)
            {
                callResult = new CallResult<object>(new ServerError("Unknown error: " + message));
                return true;
            }

            callResult = new CallResult<object>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.ToString()));
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            if (message.Type != JTokenType.Object)
                return false;

            var bRequest = (WhiteBitSocketRequest)request;
            var stream = message["stream"];
            if (stream == null)
                return false;

            return bRequest.Params.Contains(stream.ToString());
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscription)
        {
            var topics = ((WhiteBitSocketRequest)subscription.Request!).Params;
            var topicsToUnsub = new List<string>();
            foreach(var topic in topics)
            {
                if (connection.Subscriptions.Where(s => s != subscription).Any(s => ((WhiteBitSocketRequest?)s.Request)?.Params.Contains(topic) == true))
                    continue;

                topicsToUnsub.Add(topic);
            }

            if (!topicsToUnsub.Any())
            {
                _logger.LogInformation("No topics need unsubscribing (still active on other subscriptions)");
                return true;
            }

            var unsub = new WhiteBitSocketRequest { Method = "UNSUBSCRIBE", Params = topicsToUnsub.ToArray(), Id = ExchangeHelpers.NextId() };
            var result = false;

            if (!connection.Connected)
                return true;

            await connection.SendAndWaitAsync(unsub, ClientOptions.RequestTimeout, null, 1, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var id = data["id"];
                if (id == null)
                    return false;

                if ((int)id != unsub.Id)
                    return false;

                var result = data["result"];
                if (result?.Type == JTokenType.Null)
                {
                    result = true;
                    return true;
                }

                return true;
            }).ConfigureAwait(false);
            return result;
        }

        internal async Task<WhiteBitTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, SpotOrderType? type)
        {
            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.None)
                return WhiteBitTradeRuleResult.CreatePassed(quantity, quoteQuantity, price, stopPrice);

            if (_exchangeInfo == null || _lastExchangeInfoUpdate == null || (DateTime.UtcNow - _lastExchangeInfoUpdate.Value).TotalMinutes > ApiOptions.TradeRulesUpdateInterval.TotalMinutes)
                await ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);

            if (_exchangeInfo == null)
                return WhiteBitTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");

            return WhiteBitHelpers.ValidateTradeRules(_logger, ApiOptions.TradeRulesBehaviour, _exchangeInfo, symbol, quantity, quoteQuantity, price, stopPrice, type);
        }

    }
}
