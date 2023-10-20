namespace WhiteBit.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    public class WhiteBitStakingResult 
    {
        /// <summary>
        /// Successful
        /// </summary>
        public bool Success { get; set; }
    }

    /// <summary>
    /// Staking result
    /// </summary>
    public class WhiteBitStakingPositionResult: WhiteBitStakingResult
    {
        /// <summary>
        /// Id of the position
        /// </summary>
        public string? PositionId { get; set; }
    }
}
