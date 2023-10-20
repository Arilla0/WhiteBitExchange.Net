using System;
using System.Collections.Generic;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account position risk
    /// </summary>
    public class WhiteBitSubAccountFuturesPositionRiskV2
    {
        /// <summary>
        /// Futures account response (USDT margined)
        /// </summary>
        [JsonProperty("futurePositionRiskVos")]
        public IEnumerable<WhiteBitSubAccountFuturesPositionRisk> UsdtMarginedFutures { get; set; } = Array.Empty<WhiteBitSubAccountFuturesPositionRisk>();

        /// <summary>
        /// Delivery account response (COIN margined)
        /// </summary>
        [JsonProperty("deliveryPositionRiskVos")]
        public IEnumerable<WhiteBitSubAccountFuturesPositionRiskCoin> CoinMarginedFutures { get; set; } = Array.Empty<WhiteBitSubAccountFuturesPositionRiskCoin>();
    }

    /// <summary>
    /// Sub account position risk
    /// </summary>
    public class WhiteBitSubAccountFuturesPositionRiskCoin
    {
        /// <summary>
        /// The entry price
        /// </summary>
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        public decimal Leverage { get; set; }

        /// <summary>
        /// Isolated
        /// </summary>
        public bool Isolated { get; set; }

        /// <summary>
        /// Isolated wallet
        /// </summary>
        public decimal IsolatedWallet { get; set; }

        /// <summary>
        /// Isolated margin
        /// </summary>
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// Is auto add margin
        /// </summary>
        public bool IsAutoAddMargin { get; set; }

        /// <summary>
        /// Position side
        /// </summary>
        [JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Position amount
        /// </summary>
        [JsonProperty("positionAmount")]
        public decimal PositionQuantity { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Unrealized profit
        /// </summary>
        public decimal UnrealizedProfit { get; set; }
    }
}
