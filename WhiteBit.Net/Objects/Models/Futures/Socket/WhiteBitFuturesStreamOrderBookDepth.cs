using System;
using System.Collections.Generic;
using WhiteBit.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class WhiteBitFuturesStreamOrderBookDepth : WhiteBitStreamEvent, IWhiteBitFuturesEventOrderBook
    {
        /// <summary>
        /// The symbol of the order book (only filled from stream updates)
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }

        /// <summary>
        /// The ID of the first update
        /// </summary>
        [JsonProperty("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonProperty("u")]
        public long LastUpdateId { get; set; }


        /// <summary>
        /// The ID of the last update Id in last stream
        /// </summary>
        [JsonProperty("pu")]
        public long LastUpdateIdStream { get; set; }


        /// <summary>
        /// The list of diff bids
        /// </summary>
        [JsonProperty("b")]
        public IEnumerable<WhiteBitOrderBookEntry> Bids { get; set; } = Array.Empty<WhiteBitOrderBookEntry>();

        /// <summary>
        /// The list of diff asks
        /// </summary>
        [JsonProperty("a")]
        public IEnumerable<WhiteBitOrderBookEntry> Asks { get; set; } = Array.Empty<WhiteBitOrderBookEntry>();
    }
}
