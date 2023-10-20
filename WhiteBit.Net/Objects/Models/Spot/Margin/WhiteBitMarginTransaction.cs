using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// The result of transferring
    /// </summary>
    public class WhiteBitTransaction
    {
        /// <summary>
        /// The Transaction id as assigned by WhiteBit
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
    }
}
