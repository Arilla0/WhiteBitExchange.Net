using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces.Clients.UsdFuturesApi;
using WhiteBit.Net.Objects.Models.Futures;
using WhiteBit.Net.Objects.Models.Futures.AlgoOrders;
using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WhiteBit.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    public class WhiteBitRestClientUsdFuturesApiTrading : IWhiteBitRestClientUsdFuturesApiTrading
    {
        private const string newOrderEndpoint = "order";
        private const string multipleNewOrdersEndpoint = "batchOrders";
        private const string queryOrderEndpoint = "order";
        private const string cancelOrderEndpoint = "order";
        private const string cancelMultipleOrdersEndpoint = "batchOrders";
        private const string cancelAllOrdersEndpoint = "allOpenOrders";
        private const string openOrderEndpoint = "openOrder";
        private const string openOrdersEndpoint = "openOrders";
        private const string allOrdersEndpoint = "allOrders";
        private const string countDownCancelAllEndpoint = "countdownCancelAll";
        private const string forceOrdersEndpoint = "forceOrders";
        private const string myFuturesTradesEndpoint = "userTrades";

        // Futures algo 
        private const string placeVpOrderEndpoint = "algo/futures/newOrderVp";
        private const string placeTwapOrderEndpoint = "algo/futures/newOrderTwap";
        private const string cancelAlgoOrderEndpoint = "algo/futures/order";
        private const string getAlgoOpenOrdersEndpoint = "algo/futures/openOrders";
        private const string getAlgoHistoricalOrdersEndpoint = "algo/futures/historicalOrders";
        private const string getAlgoSubOrdersEndpoint = "algo/futures/subOrders";

        private const string api = "fapi";

        private readonly ILogger _logger;

        private readonly WhiteBitRestClientUsdFuturesApi _baseClient;
        private readonly string _spotBaseAddress; 

        internal WhiteBitRestClientUsdFuturesApiTrading(ILogger logger, WhiteBitRestClientUsdFuturesApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
            _spotBaseAddress = _baseClient.ClientOptions.Environment.SpotRestAddress;
        }

        #region New Order

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesPlacedOrder>> PlaceOrderAsync(
            string symbol,
            Enums.OrderSide side,
            FuturesOrderType type,
            decimal? quantity,
            decimal? price = null,
            Enums.PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            string? newClientOrderId = null,
            decimal? stopPrice = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? closePosition = null,
            OrderResponseType? orderResponseType = null,
            bool? priceProtect = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (closePosition == true && positionSide != null)
            {
                if (positionSide == Enums.PositionSide.Short && side == Enums.OrderSide.Sell)
                    throw new ArgumentException("Can't close short position with order side sell");
                if (positionSide == Enums.PositionSide.Long && side == Enums.OrderSide.Buy)
                    throw new ArgumentException("Can't close long position with order side buy");
            }

            if (orderResponseType == OrderResponseType.Full)
                throw new ArgumentException("OrderResponseType.Full is not supported in Futures");

            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, null, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<WhiteBitFuturesPlacedOrder>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new FuturesOrderTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("positionSide", positionSide == null ? null : JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("activationPrice", activationPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("callbackRate", callbackRate?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("workingType", workingType == null ? null : JsonConvert.SerializeObject(workingType, new WorkingTypeConverter(false)));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString().ToLower());
            parameters.AddOptionalParameter("closePosition", closePosition?.ToString().ToLower());
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("priceProtect", priceProtect?.ToString().ToUpper());

            var result = await _baseClient.SendRequestInternal<WhiteBitFuturesPlacedOrder>(_baseClient.GetUrl(newOrderEndpoint, api, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId
                {
                    SourceObject = result.Data,
                    Id = result.Data.Id.ToString(CultureInfo.InvariantCulture)
                });
            return result;
        }

        #endregion

        #region Multiple New Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>> PlaceMultipleOrdersAsync(
            IEnumerable<WhiteBitFuturesBatchOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (_baseClient.ApiOptions.TradeRulesBehaviour != TradeRulesBehaviour.None)
            {
                foreach (var order in orders)
                {
                    var rulesCheck = await _baseClient.CheckTradeRules(order.Symbol, order.Quantity, null, order.Price, order.StopPrice, order.Type, ct).ConfigureAwait(false);
                    if (!rulesCheck.Passed)
                    {
                        _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                        return new WebCallResult<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>(new ArgumentError(rulesCheck.ErrorMessage!));
                    }

                    order.Quantity = rulesCheck.Quantity;
                    order.Price = rulesCheck.Price;
                    order.StopPrice = rulesCheck.StopPrice;
                }
            }

            var parameters = new Dictionary<string, object>();
            var parameterOrders = new List<Dictionary<string, object>>();
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new Dictionary<string, object>()
                {
                    { "symbol", order.Symbol },
                    { "side", JsonConvert.SerializeObject(order.Side, new OrderSideConverter(false)) },
                    { "type", JsonConvert.SerializeObject(order.Type, new FuturesOrderTypeConverter(false)) },
                    { "newOrderRespType", "RESULT" }
                };

                orderParameters.AddOptionalParameter("quantity", order.Quantity?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", order.NewClientOrderId);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("timeInForce", order.TimeInForce == null ? null : JsonConvert.SerializeObject(order.TimeInForce, new TimeInForceConverter(false)));
                orderParameters.AddOptionalParameter("positionSide", order.PositionSide == null ? null : JsonConvert.SerializeObject(order.PositionSide, new PositionSideConverter(false)));
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("workingType", order.WorkingType == null ? null : JsonConvert.SerializeObject(order.WorkingType, new WorkingTypeConverter(false)));
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                orderParameters.AddOptionalParameter("priceProtect", order.PriceProtect?.ToString().ToUpper());
                parameterOrders.Add(orderParameters);
                i++;
            }

            parameters.Add("batchOrders", JsonConvert.SerializeObject(parameterOrders));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var response = await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesMultipleOrderPlaceResult>>(_baseClient.GetUrl(multipleNewOrdersEndpoint, api, "1"), HttpMethod.Post, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!response.Success)
                return response.As<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>(default);

            var result = new List<CallResult<WhiteBitFuturesPlacedOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<WhiteBitFuturesPlacedOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<WhiteBitFuturesPlacedOrder>(item));
            }

            return response.As<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>(result);
        }

        #endregion

        #region Query Order

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesOrder>(_baseClient.GetUrl(queryOrderEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Order Edit History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesOrderEditHistory>>> GetOrderEditHistoryAsync(string symbol, long? orderId = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", clientOrderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesOrderEditHistory>>(_baseClient.GetUrl("orderAmendment ", api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesCancelOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<WhiteBitFuturesCancelOrder>(_baseClient.GetUrl(cancelOrderEndpoint, api, "1"), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);

            if (result)
            {
                _baseClient.InvokeOrderCanceled(new OrderId
                {
                    SourceObject = result.Data,
                    Id = result.Data.Id.ToString(CultureInfo.InvariantCulture)
                });
            }
            return result;
        }

        #endregion

        #region Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesCancelAllOrders>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesCancelAllOrders>(_baseClient.GetUrl(cancelAllOrdersEndpoint, api, "1"), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesPlacedOrder>> EditOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", EnumConverter.GetString(side) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesPlacedOrder>(_baseClient.GetUrl("order", "fapi", "1"), HttpMethod.Put, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Edit Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>> EditMultipleOrdersAsync(
            IEnumerable<WhiteBitFuturesBatchEditOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            var parameterOrders = new List<Dictionary<string, object>>();
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new Dictionary<string, object>()
                {
                    { "symbol", order.Symbol },
                    { "side", JsonConvert.SerializeObject(order.Side, new OrderSideConverter(false)) },
                    { "quantity", order.Quantity.ToString(CultureInfo.InvariantCulture) },
                    { "price", order.Price.ToString(CultureInfo.InvariantCulture) },
                };

                orderParameters.AddOptionalParameter("orderId", order.OrderId);
                orderParameters.AddOptionalParameter("origClientOrderId", order.ClientOrderId);
                parameterOrders.Add(orderParameters);
                i++;
            }

            parameters.Add("batchOrders", JsonConvert.SerializeObject(parameterOrders));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var response = await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesMultipleOrderPlaceResult>>(_baseClient.GetUrl("batchOrders", api, "1"), HttpMethod.Post, ct, parameters, true, weight: 5).ConfigureAwait(false);
            if (!response.Success)
                return response.As<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>(default);

            var result = new List<CallResult<WhiteBitFuturesPlacedOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<WhiteBitFuturesPlacedOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<WhiteBitFuturesPlacedOrder>(item));
            }

            return response.As<IEnumerable<CallResult<WhiteBitFuturesPlacedOrder>>>(result);
        }

        #endregion

        #region Auto-Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "countdownTime", (int)countDownTime.TotalMilliseconds }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesCountDownResult>(_baseClient.GetUrl(countDownCancelAllEndpoint, api, "1"), HttpMethod.Post, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<WhiteBitFuturesCancelOrder>>>> CancelMultipleOrdersAsync(string symbol, List<long>? orderIdList = null, List<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderIdList == null && origClientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or origClientOrderIdList must be sent");

            if (orderIdList?.Count > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (origClientOrderIdList?.Count > 10)
                throw new ArgumentException("origClientOrderIdList cannot contain more than 10 items");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (origClientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", origClientOrderIdList.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var response = await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesMultipleOrderCancelResult>>(_baseClient.GetUrl(cancelMultipleOrdersEndpoint, api, "1"), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);

            if (!response.Success)
                return response.As<IEnumerable<CallResult<WhiteBitFuturesCancelOrder>>>(default);

            var result = new List<CallResult<WhiteBitFuturesCancelOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<WhiteBitFuturesCancelOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<WhiteBitFuturesCancelOrder>(item));
            }

            return response.As<IEnumerable<CallResult<WhiteBitFuturesCancelOrder>>>(result);
        }

        #endregion

        #region Query Current Open Order

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitFuturesOrder>(_baseClient.GetUrl(openOrderEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Current All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesOrder>>(_baseClient.GetUrl(openOrdersEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true, weight: symbol == null ? 40 : 1).ConfigureAwait(false);
        }

        #endregion

        #region All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesOrder>>(_baseClient.GetUrl(allOrdersEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        #endregion

        #region User's Force Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesOrder>>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("autoCloseType", closeType.HasValue ? JsonConvert.SerializeObject(closeType, new AutoCloseTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesOrder>>(_baseClient.GetUrl(forceOrdersEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true, weight: symbol == null ? 50 : 20).ConfigureAwait(false);
        }

        #endregion

        #region Account Trade List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WhiteBitFuturesUsdtTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? orderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<WhiteBitFuturesUsdtTrade>>(_baseClient.GetUrl(myFuturesTradesEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        #endregion

        #region Futures Algo

        #region Place VP Order
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitAlgoOrderResult>> PlaceVolumeParticipationOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            OrderUrgency urgency,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? limitPrice = null,
            PositionSide? positionSide = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "urgency", EnumConverter.GetString(urgency) },
            };
            parameters.AddOptionalParameter("positionSide", positionSide == null ? null : JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _spotBaseAddress.AppendPath("sapi", "v1", placeVpOrderEndpoint);
            return await _baseClient.SendRequestInternal<WhiteBitAlgoOrderResult>(new Uri(url), HttpMethod.Post, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }
        #endregion

        #region Place TWAP Order
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            int duration,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? limitPrice = null,
            PositionSide? positionSide = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "duration", duration },
            };
            parameters.AddOptionalParameter("positionSide", positionSide == null ? null : JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _spotBaseAddress.AppendPath("sapi", "v1", placeTwapOrderEndpoint);
            return await _baseClient.SendRequestInternal<WhiteBitAlgoOrderResult>(new Uri(url), HttpMethod.Post, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }
        #endregion

        #region Cancel algo order
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "algoId", algoOrderId },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _spotBaseAddress.AppendPath("sapi", "v1", cancelAlgoOrderEndpoint);
            return await _baseClient.SendRequestInternal<WhiteBitAlgoResult>(new Uri(url), HttpMethod.Delete, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _spotBaseAddress.AppendPath("sapi", "v1", getAlgoOpenOrdersEndpoint);
            return await _baseClient.SendRequestInternal<WhiteBitAlgoOrders>(new Uri(url), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #region Get historical Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null,long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null? null: JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _spotBaseAddress.AppendPath("sapi", "v1", getAlgoHistoricalOrdersEndpoint);
            return await _baseClient.SendRequestInternal<WhiteBitAlgoOrders>(new Uri(url), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #region Get Algo sub Orders
        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "algoId", algoId }
            };
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _spotBaseAddress.AppendPath("sapi", "v1", getAlgoSubOrdersEndpoint);
            return await _baseClient.SendRequestInternal<WhiteBitAlgoSubOrderList>(new Uri(url), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #endregion

    }
}
