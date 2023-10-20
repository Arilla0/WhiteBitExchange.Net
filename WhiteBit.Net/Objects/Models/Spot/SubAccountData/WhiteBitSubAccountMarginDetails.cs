using System;
using System.Collections.Generic;
using WhiteBit.Net.Objects.Models.Spot.Margin;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account margin trade details
    /// </summary>
    public class WhiteBitSubAccountMarginDetails
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Total asset in btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net asset
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Trade details
        /// </summary>
        [JsonProperty("marginTradeCoeffVo")]
        public WhiteBitMarginTradeCoeff? MarginTradeCoeff { get; set; }
        /// <summary>
        /// Asset list
        /// </summary>
        [JsonProperty("marginUserAssetVoList")]
        public IEnumerable<WhiteBitMarginBalance> MarginUserAssets { get; set; } = Array.Empty<WhiteBitMarginBalance>();
    }

    /// <summary>
    /// Margin trade detail
    /// </summary>
    public class WhiteBitMarginTradeCoeff
    {
        /// <summary>
        /// Liquidation margin ratio
        /// </summary>
        public decimal ForceLiquidationBar { get; set; }
        /// <summary>
        /// Margin class margin ratio
        /// </summary>
        public decimal MarginCallBar { get; set; }
        /// <summary>
        /// Initial margin ratio
        /// </summary>
        public decimal NormalBar { get; set; }
    }
}
