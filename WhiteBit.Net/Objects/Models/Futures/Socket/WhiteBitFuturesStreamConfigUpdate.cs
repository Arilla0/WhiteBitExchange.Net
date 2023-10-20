using System;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Information about leverage of symbol changed
    /// </summary>
    public class WhiteBitFuturesStreamConfigUpdate : WhiteBitStreamEvent
    {
        /// <summary>
        /// Leverage Update data
        /// </summary>
        [JsonProperty("ac")]
        public WhiteBitFuturesStreamLeverageUpdateData LeverageUpdateData { get; set; } = new WhiteBitFuturesStreamLeverageUpdateData();

        /// <summary>
        /// Position mode Update data
        /// </summary>
        [JsonProperty("ai")]
        public WhiteBitFuturesStreamConfigUpdateData ConfigUpdateData { get; set; } = new WhiteBitFuturesStreamConfigUpdateData();

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Config update data
    /// </summary>
    public class WhiteBitFuturesStreamLeverageUpdateData
    {
        /// <summary>
        /// The symbol this balance is for
        /// </summary>
        [JsonProperty("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// The symbol this leverage is for
        /// </summary>
        [JsonProperty("l")]
        public int Leverage { get; set; }
    }

    /// <summary>
    /// Position mode update data
    /// </summary>
    public class WhiteBitFuturesStreamConfigUpdateData
    {
        /// <summary>
        /// Multi-Assets Mode
        /// </summary>
        [JsonProperty("j")]
        public bool MultiAssetMode { get; set; }
    }
}
