using System;
using System.Collections.Generic;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin account snapshot
    /// </summary>
    public class WhiteBitMarginAccountSnapshot
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
        public WhiteBitMarginAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Margin snapshot data
    /// </summary>
    public class WhiteBitMarginAccountSnapshotData
    {
        /// <summary>
        /// The margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Total BTC asset
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total BTC liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net BTC asset
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }

        /// <summary>
        /// Assets
        /// </summary>
        public IEnumerable<WhiteBitMarginBalance> UserAssets { get; set; } = Array.Empty<WhiteBitMarginBalance>();
    }
}
