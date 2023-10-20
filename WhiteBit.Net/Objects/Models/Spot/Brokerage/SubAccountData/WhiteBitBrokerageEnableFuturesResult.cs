using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Futures Result
    /// </summary>
    public class WhiteBitBrokerageEnableFuturesResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Futures Enabled
        /// </summary>
        [JsonProperty("enableFutures")]
        public bool IsFuturesEnabled { get; set; }
        
        /// <summary>
        /// Update Date
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
}