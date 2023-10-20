using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// IP Restriction
    /// </summary>
    public class WhiteBitBrokerageIpRestrictionBase
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Api key
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// IP list
        /// </summary>
        public IEnumerable<string> IpList { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
    
    /// <summary>
    /// IP Restriction
    /// </summary>
    public class WhiteBitBrokerageIpRestriction : WhiteBitBrokerageIpRestrictionBase
    {
        /// <summary>
        /// Ip Restrict
        /// </summary>
        public bool IpRestrict { get; set; }
    }
}