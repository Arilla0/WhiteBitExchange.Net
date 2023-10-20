using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking product info
    /// </summary>
    public class WhiteBitStakingProduct
    {
        /// <summary>
        /// Project id
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Product details
        /// </summary>
        [JsonProperty("detail")]
        public WhiteBitStakingProductDetails Details { get; set; } = null!;
        /// <summary>
        /// Product quota
        /// </summary>
        public WhiteBitStakingQuota Quota { get; set; } = null!;
    }

    /// <summary>
    /// Staking product details
    /// </summary>
    public class WhiteBitStakingProductDetails
    {
        /// <summary>
        /// Lock up asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Reward asset
        /// </summary>
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Duration in days
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// Renewable
        /// </summary>
        public bool Renewable { get; set; }
        /// <summary>
        /// Apy
        /// </summary>
        public decimal Apy { get; set; }
    }

    /// <summary>
    /// Staking product quota
    /// </summary>
    public class WhiteBitStakingQuota
    {
        /// <summary>
        /// Total Personal quota
        /// </summary>
        [JsonProperty("totalPersonalQuota")]
        public decimal Quota { get; set; }
        /// <summary>
        /// Minimum amount per order
        /// </summary>
        public decimal Minimum { get; set; }
    }
}
