namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rate limit info
    /// </summary>
    public class WhiteBitCurrentRateLimit: WhiteBitRateLimit
    {
        /// <summary>
        /// The current used amount
        /// </summary>
        public int Count { get; set; }
    }
}
