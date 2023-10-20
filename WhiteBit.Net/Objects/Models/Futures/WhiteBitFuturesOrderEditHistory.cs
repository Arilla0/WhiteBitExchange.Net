using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// The history of order edits
    /// </summary>
    public class WhiteBitFuturesOrderEditHistory
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        [JsonProperty("pair")]
        public string? Pair { get; set; }

        /// <summary>
        /// The id of the amendment
        /// </summary>
        [JsonProperty("amendmentId")]
        public long AmendmentId { get; set; }
        /// <summary>
        /// The order id as assigned by WhiteBit
        /// </summary>
        [JsonProperty("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonProperty("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Edit time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Edit info
        /// </summary>
        [JsonProperty("amendment")]
        public WhiteBitFuturesOrderChanges EditInfo { get; set; } = null!;

    }

    /// <summary>
    /// Order changes
    /// </summary>
    public class WhiteBitFuturesOrderChanges
    {
        /// <summary>
        /// Price change
        /// </summary>
        [JsonProperty("price")]
        public WhiteBitFuturesOrderChange Price { get; set; } = null!;
        /// <summary>
        /// Quantity change
        /// </summary>
        [JsonProperty("origQty")]
        public WhiteBitFuturesOrderChange Quantity { get; set; } = null!;

        /// <summary>
        /// Amount of times changed
        /// </summary>
        [JsonProperty("count")]
        public int EditCount { get; set; }
    }
    
    /// <summary>
    /// Change info
    /// </summary>
    public class WhiteBitFuturesOrderChange
    {
        /// <summary>
        /// Before edit
        /// </summary>
        [JsonProperty("before")]
        public decimal Before { get; set; }
        /// <summary>
        /// After edit
        /// </summary>
        [JsonProperty("after")]
        public decimal After { get; set; }
    }
}
