using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Quota left
    /// </summary>
    public class WhiteBitStakingPersonalQuota
    {
        /// <summary>
        /// Quota left
        /// </summary>
        [JsonProperty("leftPersonalQuota")]
        public decimal PersonalQuotaLeft { get; set; }
    }
}
