using CryptoExchange.Net.Interfaces;
using System;

namespace WhiteBit.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// WhiteBit general API endpoints
    /// </summary>
    public interface IWhiteBitRestClientGeneralApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to brokerage
        /// </summary>
        public IWhiteBitRestClientGeneralApiBrokerage Brokerage { get; }

        /// <summary>
        /// Endpoints related to futures account interactions
        /// </summary>
        public IWhiteBitRestClientGeneralApiFutures Futures { get; }

        /// <summary>
        /// Endpoints related to savings
        /// </summary>
        public IWhiteBitRestClientGeneralApiSavings Savings { get; }

        /// <summary>
        /// Endpoints related to crypto loans
        /// </summary>
        public IWhiteBitRestClientGeneralApiLoans CryptoLoans { get; }

        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        public IWhiteBitRestClientGeneralApiMining Mining { get; }

        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        public IWhiteBitRestClientGeneralApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to staking
        /// </summary>
        IWhiteBitRestClientGeneralApiStaking Staking { get; }
    }
}
