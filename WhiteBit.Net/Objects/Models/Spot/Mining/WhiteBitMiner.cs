using System;
using System.Collections.Generic;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Miner list
    /// </summary>
    public class WhiteBitMinerList
    {
        /// <summary>
        /// Total number of entries
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Worker data
        /// </summary>
        public IEnumerable<WhiteBitMinerInfo> WorkerDatas { get; set; } = Array.Empty<WhiteBitMinerInfo>();
    }

    /// <summary>
    /// Miner details
    /// </summary>
    public class WhiteBitMinerInfo
    {
        /// <summary>
        /// Worker id
        /// </summary>
        public string WorkerId { get; set; } = string.Empty;
        /// <summary>
        /// Worker name
        /// </summary>
        public string WorkerName { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(MinerStatusConverter))]
        public MinerStatus Status { get; set; }
        /// <summary>
        /// Hash rate
        /// </summary>
        public decimal HashRate { get; set; }
        /// <summary>
        /// Day hash rate
        /// </summary>
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// Reject rate
        /// </summary>
        public decimal RejectRate { get; set; }
        /// <summary>
        /// Last share time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastShareTime { get; set; }
    }
}
