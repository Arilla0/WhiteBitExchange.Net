using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace WhiteBit.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking account
    /// </summary>
    public class WhiteBitEthStakingAccount
    {
        /// <summary>
        /// Total profit in Beth
        /// </summary>
        [JsonProperty("cumulativeProfitInBETH")]
        public decimal TotalProfitInBeth { get; set; }
        /// <summary>
        /// Last day profit in Beth
        /// </summary>
        [JsonProperty("lastDayProfitInBETH")]
        public decimal LastDayProfitInBeth { get; set; }
    }
}
