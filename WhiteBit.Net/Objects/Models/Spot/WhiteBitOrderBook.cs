using System;
using System.Collections.Generic;
using WhiteBit.Net.Interfaces;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class WhiteBitOrderBook : IWhiteBitOrderBook
    {
        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonProperty("lastUpdateId")]
        public long LastUpdateId { get; set; }
        
        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<WhiteBitOrderBookEntry> Bids { get; set; } = Array.Empty<WhiteBitOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<WhiteBitOrderBookEntry> Asks { get; set; } = Array.Empty<WhiteBitOrderBookEntry>();
    }
}
