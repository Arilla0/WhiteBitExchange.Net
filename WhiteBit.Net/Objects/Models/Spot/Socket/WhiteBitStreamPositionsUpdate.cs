using System;
using System.Collections.Generic;
using WhiteBit.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Positions update
    /// </summary>
    public class WhiteBitStreamPositionsUpdate : WhiteBitStreamEvent
    {
        /// <summary>
        /// Time of last account update
        /// </summary>
        [JsonProperty("u"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
        /// <summary>
        /// Balances
        /// </summary>
        [JsonProperty("B")]
        public IEnumerable<WhiteBitStreamBalance> Balances { get; set; } = Array.Empty<WhiteBitStreamBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class WhiteBitStreamBalance: IWhiteBitBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonProperty("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonProperty("f")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        [JsonProperty("l")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
