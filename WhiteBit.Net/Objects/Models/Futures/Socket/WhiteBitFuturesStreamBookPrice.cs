using System;
using WhiteBit.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures book price
    /// </summary>
    public class WhiteBitFuturesStreamBookPrice : WhiteBitStreamBookPrice
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TransactionTime { get; set; }
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty("e")] 
        public string Event { get; set; } = string.Empty;
    }
}
