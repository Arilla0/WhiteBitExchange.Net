using System;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Adjust history
    /// </summary>
    public class WhiteBitCrossCollateralAdjustLtvHistory
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;

        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Pre adjustment rate
        /// </summary>
        public decimal PreCollateralRate { get; set; }
        /// <summary>
        /// After adjustment rate
        /// </summary>
        public decimal AfterCollateralRate { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        [JsonConverter(typeof(AdjustRateDirectionConverter))]
        public AdjustRateDirection Direction { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Time of adjustment
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime AdjustTime { get; set; }
    }
}
