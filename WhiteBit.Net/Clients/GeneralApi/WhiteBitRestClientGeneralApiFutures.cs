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
using WhiteBit.Net.Objects.Models.Spot;
using WhiteBit.Net.Objects.Models.Spot.Futures;
using WhiteBit.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace WhiteBit.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class WhiteBitRestClientGeneralApiFutures : IWhiteBitRestClientGeneralApiFutures
    {
        private const string futuresTransferEndpoint = "futures/transfer";
        private const string futuresTransferHistoryEndpoint = "futures/transfer";
        private const string futuresBorrowHistoryEndpoint = "futures/loan/borrow/history";
        private const string futuresRepayHistoryEndpoint = "futures/loan/repay/history";
        private const string futuresWalletEndpoint = "futures/loan/wallet";
        private const string futuresAdjustCrossCollateralHistoryEndpoint = "futures/loan/adjustCollateral/history";
        private const string futuresCrossCollateralLiquidationHistoryEndpoint = "futures/loan/liquidationHistory";

        private const string api = "sapi";
        private const string publicVersion = "1";

        private readonly WhiteBitRestClientGeneralApi _baseClient;

        internal WhiteBitRestClientGeneralApiFutures(WhiteBitRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region New Future Account Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitTransaction>> TransferFuturesAccountAsync(string asset, decimal quantity, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "type", JsonConvert.SerializeObject(transferType, new FuturesTransferTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitTransaction>(_baseClient.GetUrl(futuresTransferEndpoint, api, publicVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get future account transaction history

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitSpotFuturesTransfer>>> GetFuturesTransferHistoryAsync(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime)! }
            };

            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitSpotFuturesTransfer>>(_baseClient.GetUrl(futuresTransferHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Get cross collateral borrow history

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralBorrowHistory>>> GetCrossCollateralBorrowHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitCrossCollateralBorrowHistory>>(_baseClient.GetUrl(futuresBorrowHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Get cross collateral repay history

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralRepayHistory>>> GetCrossCollateralRepayHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitCrossCollateralRepayHistory>>(_baseClient.GetUrl(futuresRepayHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Cross collateral wallet

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitCrossCollateralWallet>> GetCrossCollateralWalletAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitCrossCollateralWallet>(_baseClient.GetUrl(futuresWalletEndpoint, api, "2"), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Adjust Cross-Collateral LTV History

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralAdjustLtvHistory>>> GetAdjustCrossCollateralLoanToValueHistoryAsync(string? collateralAsset = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("loanCoin", loanAsset);
            parameters.AddOptionalParameter("collateralCoin", collateralAsset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitCrossCollateralAdjustLtvHistory>>(_baseClient.GetUrl(futuresAdjustCrossCollateralHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Cross-Collateral Liquidation History

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralLiquidationHistory>>> GetCrossCollateralLiquidationHistoryAsync(string? collateralAsset = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("loanCoin", loanAsset);
            parameters.AddOptionalParameter("collateralCoin", collateralAsset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitCrossCollateralLiquidationHistory>>(_baseClient.GetUrl(futuresCrossCollateralLiquidationHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Cross-Collateral Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralInterestHistory>>> GetCrossCollateralInterestHistoryAsync(string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("collateralCoin", collateralAsset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<WhiteBitQueryRecords<WhiteBitCrossCollateralInterestHistory>>(_baseClient.GetUrl("futures/loan/interestHistory", api, publicVersion), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }

        #endregion
    }
}
