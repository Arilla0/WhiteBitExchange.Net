namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Bnb burn status
    /// </summary>
    public class WhiteBitBnbBurnStatus
    {
        /// <summary>
        /// If spot trading BNB burn is enabled
        /// </summary>
        public bool SpotBnbBurn { get; set; }
        /// <summary>
        /// If margin interest BNB burn is enabled
        /// </summary>
        public bool InterestBnbBurn { get; set; }
    }
}
