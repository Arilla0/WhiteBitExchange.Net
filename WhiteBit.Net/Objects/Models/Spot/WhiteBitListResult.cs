using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// List result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WhiteBitListResult<T>
    {
        /// <summary>
        /// Data start time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Emd to,e
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// More data available
        /// </summary>
        public bool MoreData { get; set; }
        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<T> Data { get; set; } = Array.Empty<T>();
    }
}