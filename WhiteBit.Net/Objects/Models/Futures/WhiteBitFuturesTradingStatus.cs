using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trading rules status
    /// </summary>
    public class WhiteBitFuturesTradingStatus
    {
        /// <summary>
        /// The trading rule indicators
        /// </summary>
        public Dictionary<string, IEnumerable<WhiteBitFuturesTradingStatusIndicator>> Indicators { get; set; } = new Dictionary<string, IEnumerable<WhiteBitFuturesTradingStatusIndicator>>();
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Indicator details
    /// </summary>
    public class WhiteBitFuturesTradingStatusIndicator
    {
        /// <summary>
        /// Locked
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// Planned time when indicator is unlocked
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PlannedRecoveryTime { get; set; }
        /// <summary>
        /// The indicator name
        /// </summary>
        public string Indicator { get; set; } = string.Empty;
        /// <summary>
        /// Current value of the indicator
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// The trigger value of the indicator
        /// </summary>
        public decimal TriggerValue { get; set; }
    }
}
