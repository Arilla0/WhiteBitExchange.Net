using System;
using System.Collections.Generic;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Snapshot data of a spot account
    /// </summary>
    public class WhiteBitSpotAccountSnapshot
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonProperty("updateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Account type the data is for
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public AccountType Type { get; set; }
        /// <summary>
        /// Snapshot data
        /// </summary>
        [JsonProperty("data")]
        public WhiteBitSpotAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Data of the snapshot
    /// </summary>
    public class WhiteBitSpotAccountSnapshotData
    {
        /// <summary>
        /// The total value of assets in btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// List of balances
        /// </summary>
        public IEnumerable<WhiteBitBalance> Balances { get; set; } = Array.Empty<WhiteBitBalance>();

    }
}