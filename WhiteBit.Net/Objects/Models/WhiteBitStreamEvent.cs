using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models
{
    /// <summary>
    /// A event received by a WhiteBit websocket
    /// </summary>
    public class WhiteBitStreamEvent
    {
        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }
    }
}
