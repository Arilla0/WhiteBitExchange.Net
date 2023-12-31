using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Objects.Models;
using WhiteBit.Net.Objects.Models.Spot.Loans;
using CryptoExchange.Net.Objects;

namespace WhiteBit.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// WhiteBit Spot Crypto loans endpoints
    /// </summary>
    public interface IWhiteBitRestClientGeneralApiLoans
    {
        /// <summary>
        /// Get income history from crypto loans
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#get-crypto-loans-income-history-user_data" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of incoming</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WhiteBitCryptoLoanIncome>>> GetIncomeHistoryAsync(string asset, LoanIncomeType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Take a crypto loan
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#borrow-crypto-loan-borrow-trade" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="loanTerm">Loan term in days, 7/14/30/90/180</param>
        /// <param name="loanQuantity">Quantity to loan in loan asset</param>
        /// <param name="collateralQuantity">Quantity to loan in collateral asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitCryptoLoanBorrow>> BorrowAsync(string loanAsset, string collateralAsset, int loanTerm, decimal? loanQuantity = null, decimal? collateralQuantity = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow order history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#borrow-get-loan-borrow-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanBorrowRecord>>> GetBorrowHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ongoing loan orders
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#borrow-get-loan-ongoing-orders-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanOpenBorrowOrder>>> GetOpenBorrowOrdersAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay a loan
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#repay-crypto-loan-repay-trade" /></para>
        /// </summary>
        /// <param name="orderId">Order id to repay</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="repayWithBorrowedAsset">True to repay with the borrowed asset, false to repay with collateral asset</param>
        /// <param name="collateralReturn">Return extra colalteral to spot account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitCryptoLoanRepay>> RepayAsync(long orderId, decimal quantity, bool? repayWithBorrowedAsset = null, bool? collateralReturn = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get loan repayment history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#repay-get-loan-repayment-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanRepayRecord>>> GetRepayHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust LTV for a loan
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#adjust-ltv-crypto-loan-adjust-ltv-trade" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="quantity">Adjustment quantity</param>
        /// <param name="addOrRmove">True for add, false to reduce</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitCryptoLoanLtvAdjust>> AdjustLTVAsync(long orderId, decimal quantity, bool addOrRmove, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get LTV adjustment history
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#adjust-ltv-get-loan-ltv-adjustment-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanLtvAdjustRecord>>> GetLtvAdjustHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate and borrow limit of loanable assets. The borrow limit is shown in USD value.
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#get-loanable-assets-data-user_data-2" /></para>
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="vipLevel">Vip level</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanAsset>>> GetLoanableAssetsAsync(string? loanAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get LTV information and collateral limit of collateral assets. The collateral limit is shown in USD value.
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#get-collateral-assets-data-user_data" /></para>
        /// </summary>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="vipLevel">Vip level</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanCollateralAsset>>> GetCollateralAssetsAsync(string? collateralAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the the rate of collateral coin / loan coin when using collateral repay, the rate will be valid within 8 second.
        /// <para><a href="https://WhiteBit-docs.github.io/apidocs/spot/en/#check-collateral-repay-rate-user_data" /></para>
        /// </summary>
        /// <param name="loanAsset">Loan asset</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitCryptoLoanRepayRate>> GetCollateralRepayRateAsync(string loanAsset, string collateralAsset, decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Customize margin call for ongoing orders only.
        /// </summary>
        /// <param name="marginCall">Margin call value</param>
        /// <param name="orderId">Order id. Required if collateralAsset is not send</param>
        /// <param name="collateralAsset">Collateral asset. Required if order id is not send</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WhiteBitQueryRecords<WhiteBitCryptoLoanMarginCallResult>>> CustomizeMarginCallAsync(decimal marginCall, string? orderId = null, string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
