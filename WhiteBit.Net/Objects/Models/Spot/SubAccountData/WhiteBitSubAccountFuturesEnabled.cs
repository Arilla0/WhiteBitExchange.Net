namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures trading enabled
    /// </summary>
    public class WhiteBitSubAccountFuturesEnabled
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Whether futures trading is enabled
        /// </summary>
        public bool IsFuturesEnabled { get; set; }
    }
}
