namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account margin trading enabled
    /// </summary>
    public class WhiteBitSubAccountMarginEnabled
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Whether Margin trading is enabled
        /// </summary>
        public bool IsMarginEnabled { get; set; }
    }
}
