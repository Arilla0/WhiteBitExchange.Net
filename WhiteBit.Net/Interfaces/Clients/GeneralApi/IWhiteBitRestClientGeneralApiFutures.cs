using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Objects.Models;
using WhiteBit.Net.Objects.Models.Spot;
using WhiteBit.Net.Objects.Models.Spot.Futures;
using WhiteBit.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net.Objects;

namespace WhiteBit.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// WhiteBit futures interaction endpoints
    /// </summary>
    public interface IWhiteBitRestClientGeneralApiFutures
    {
        /// <summary>
        /// Execute a transfer between the spot account and a futures account
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#new-future-account-transfer-user_data" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="transferType">The transfer direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The transaction id</returns>
        Task<WebCallResult<WhiteBitTransaction>> TransferFuturesAccountAsync(string asset, decimal quantity, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers between spot and futures account
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#get-future-account-transaction-history-list-user_data" /></para>
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to return</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitSpotFuturesTransfer>>> GetFuturesTransferHistoryAsync(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral borrow history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#cross-collateral-borrow-history-user_data" /></para>
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralBorrowHistory>>> GetCrossCollateralBorrowHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral borrow history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#cross-collateral-repayment-history-user_data" /></para>
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralRepayHistory>>> GetCrossCollateralRepayHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross-collateral wallet info
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#cross-collateral-wallet-v2-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Wallet</returns>
        Task<WebCallResult<WhiteBitCrossCollateralWallet>> GetCrossCollateralWalletAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral LTV adjustment history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-history-user_data" /></para>
        /// </summary>
        /// <param name="loanAsset">The loan asset</param>
        /// <param name="collateralAsset">The collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjustment history</returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralAdjustLtvHistory>>> GetAdjustCrossCollateralLoanToValueHistoryAsync(string? collateralAsset = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral liquidation history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#cross-collateral-liquidation-history-user_data" /></para>
        /// </summary>
        /// <param name="collateralAsset">The collateral asset</param>
        /// <param name="loanAsset">The loan asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Liquidation history</returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralLiquidationHistory>>> GetCrossCollateralLiquidationHistoryAsync(string? collateralAsset = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral interest history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#cross-collateral-interest-history-user_data" /></para>
        /// </summary>
        /// <param name="collateralAsset">The collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCrossCollateralInterestHistory>>> GetCrossCollateralInterestHistoryAsync(string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
