using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Enable Or Disable BNB Burn Spot And Margin Result
    /// </summary>
    public class WhiteBitBrokerageChangeBnbBurnSpotAndMarginResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Is Spot BNB Burn
        /// </summary>
        [JsonProperty("spotBNBBurn")]
        public bool IsSpotBnbBurn { get; set; }
    }
}