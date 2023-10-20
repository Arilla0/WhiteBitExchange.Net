using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Future hourly interest rate
    /// </summary>
    public class WhiteBitFuturesInterestRate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Next interest rate
        /// </summary>
        [JsonProperty("nextHourlyInterestRate")]
        public decimal NextHourlyInterestRate { get; set; }
    }
}
