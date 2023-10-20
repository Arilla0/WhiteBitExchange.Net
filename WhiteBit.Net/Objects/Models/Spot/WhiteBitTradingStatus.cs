using System;
using System.Collections.Generic;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade status
    /// </summary>
    public class WhiteBitTradingStatus
    {
        /// <summary>
        /// Is locked
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// Planned time of recovery
        /// </summary>
        public int PlannedRecoverTime { get; set; }

        /// <summary>
        /// Conditions
        /// </summary>
        [JsonProperty("triggerCondition")]
        public Dictionary<string, int> TriggerConditions { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Dictionary of indicator lists for symbols
        /// </summary>
        public Dictionary<string, IEnumerable<WhiteBitIndicator>> Indicators { get; set; } = new Dictionary<string, IEnumerable<WhiteBitIndicator>>();
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Indicator info
    /// </summary>
    public class WhiteBitIndicator
    {
        /// <summary>
        /// Indicator name
        /// </summary>
        [JsonProperty("i")]
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty("c")]
        public int Count { get; set; }
        /// <summary>
        /// Current value
        /// </summary>
        [JsonProperty("v")]
        public decimal CurrentValue { get; set; }
        /// <summary>
        /// Trigger value
        /// </summary>
        [JsonProperty("t")]
        public decimal TriggerValue { get; set; }
    }
}
