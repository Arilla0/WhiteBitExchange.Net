using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account
    /// </summary>
    public class WhiteBitBrokerageSubAccount : WhiteBitBrokerageSubAccountCommission
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; } = string.Empty;

        /// <summary>
        /// Create Date
        /// </summary>
        [JsonProperty("createTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateDate { get; set; }
    }
}