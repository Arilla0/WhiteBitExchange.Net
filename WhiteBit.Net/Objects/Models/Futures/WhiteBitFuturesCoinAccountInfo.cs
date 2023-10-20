using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account info
    /// </summary>
    public class WhiteBitFuturesCoinAccountInfo
    {
        /// <summary>
        /// Can deposit
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Fee tier
        /// </summary>
        public int FeeTier { get; set; }
        /// <summary>
        /// Update tier
        /// </summary>
        public int UpdateTier { get; set; }

        /// <summary>
        /// Account assets
        /// </summary>
        public IEnumerable<WhiteBitFuturesAccountAsset> Assets { get; set; } = Array.Empty<WhiteBitFuturesAccountAsset>();
        /// <summary>
        /// Account positions
        /// </summary>
        public IEnumerable<WhiteBitPositionInfoCoin> Positions { get; set; } = Array.Empty<WhiteBitPositionInfoCoin>();
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
