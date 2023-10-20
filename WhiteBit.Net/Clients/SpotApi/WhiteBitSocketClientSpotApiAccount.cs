using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using WhiteBit.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using WhiteBit.Net.Objects.Models.Spot;
using CryptoExchange.Net.Converters;
using WhiteBit.Net.Interfaces.Clients.SpotApi;
using WhiteBit.Net.Objects;

namespace WhiteBit.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class WhiteBitSocketClientSpotApiAccount : IWhiteBitSocketClientSpotApiAccount
    {
        private const string executionUpdateEvent = "executionReport";
        private const string ocoOrderUpdateEvent = "listStatus";
        private const string accountPositionUpdateEvent = "outboundAccountPosition";
        private const string balanceUpdateEvent = "balanceUpdate";

        private readonly WhiteBitSocketClientSpotApi _client;

        private readonly ILogger _logger;

        #region constructor/destructor

        internal WhiteBitSocketClientSpotApiAccount(ILogger logger, WhiteBitSocketClientSpotApi client)
        {
            _client = client;
            _logger = logger;
        }

        #endregion

        #region Queries

        #region Get Account Info

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<WhiteBitAccountInfo>>> GetAccountInfoAsync(IEnumerable<string>? symbols = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            return await _client.QueryAsync<WhiteBitAccountInfo>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"account.status", parameters, true, true, weight: 20).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Rate Limits

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<IEnumerable<WhiteBitCurrentRateLimit>>>> GetOrderRateLimitsAsync(IEnumerable<string>? symbols = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            return await _client.QueryAsync<IEnumerable<WhiteBitCurrentRateLimit>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"account.rateLimits.orders", parameters, true, true, weight: 40).ConfigureAwait(false);
        }

        #endregion

        #region Start User Stream

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<string>>> StartUserStreamAsync()
        {
            var result = await _client.QueryAsync<WhiteBitListenKey>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"userDataStream.start", new Dictionary<string, object>(), true, weight: 2).ConfigureAwait(false);
            if (!result)
                return result.AsError<WhiteBitResponse<string>>(result.Error!);

            return result.As(new WhiteBitResponse<string>
            {
                Ratelimits = result.Data!.Ratelimits!,
                Result = result.Data!.Result?.ListenKey!
            });
        }

        #endregion

        #region Start User Stream

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<object>>> KeepAliveUserStreamAsync(string listenKey)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("listenKey", listenKey);
            return await _client.QueryAsync<object>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"userDataStream.ping", parameters, true, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Start User Stream

        /// <inheritdoc />
        public async Task<CallResult<WhiteBitResponse<object>>> StopUserStreamAsync(string listenKey)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("listenKey", listenKey);
            return await _client.QueryAsync<object>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"userDataStream.stop", parameters, true, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region User Data Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<WhiteBitStreamOrderUpdate>>? onOrderUpdateMessage,
            Action<DataEvent<WhiteBitStreamOrderList>>? onOcoOrderUpdateMessage,
            Action<DataEvent<WhiteBitStreamPositionsUpdate>>? onAccountPositionMessage,
            Action<DataEvent<WhiteBitStreamBalanceUpdate>>? onAccountBalanceUpdate,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var handler = new Action<DataEvent<string>>(data =>
            {
                var combinedToken = JToken.Parse(data.Data);
                var token = combinedToken["data"];
                if (token == null)
                    return;

                var evnt = token["e"]?.ToString();
                if (evnt == null)
                    return;

                switch (evnt)
                {
                    case executionUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<WhiteBitStreamOrderUpdate>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onOrderUpdateMessage?.Invoke(data.As(result.Data, result.Data.Id.ToString()));
                            }
                            else
                            {
                                _logger.Log(LogLevel.Warning,
                                    "Couldn't deserialize data received from order stream: " + result.Error);
                            }

                            break;
                        }
                    case ocoOrderUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<WhiteBitStreamOrderList>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onOcoOrderUpdateMessage?.Invoke(data.As(result.Data, result.Data.Id.ToString()));
                            }
                            else
                            {
                                _logger.Log(LogLevel.Warning,
                                    "Couldn't deserialize data received from oco order stream: " + result.Error);
                            }

                            break;
                        }
                    case accountPositionUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<WhiteBitStreamPositionsUpdate>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onAccountPositionMessage?.Invoke(data.As(result.Data));
                            }
                            else
                            {
                                _logger.Log(LogLevel.Warning,
                                    "Couldn't deserialize data received from account position stream: " + result.Error);
                            }

                            break;
                        }
                    case balanceUpdateEvent:
                        {
                            var result = _client.DeserializeInternal<WhiteBitStreamBalanceUpdate>(token);
                            if (result)
                            {
                                result.Data.ListenKey = combinedToken["stream"]!.Value<string>()!;
                                onAccountBalanceUpdate?.Invoke(data.As(result.Data, result.Data.Asset));
                            }
                            else
                            {
                                _logger.Log(LogLevel.Warning,
                                    "Couldn't deserialize data received from account position stream: " + result.Error);
                            }

                            break;
                        }
                    default:
                        _logger.Log(LogLevel.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await _client.SubscribeAsync(_client.BaseAddress, new[] { listenKey }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
