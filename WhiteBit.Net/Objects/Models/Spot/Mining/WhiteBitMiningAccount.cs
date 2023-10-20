using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining account
    /// </summary>
    public class WhiteBitMiningAccount
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Hash rates
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<WhiteBitHashRate> Hashrates { get; set; } = Array.Empty<WhiteBitHashRate>();
    }
}
