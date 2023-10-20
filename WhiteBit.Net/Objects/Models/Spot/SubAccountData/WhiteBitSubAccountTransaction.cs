using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Transaction
    /// </summary>
    public class WhiteBitSubAccountTransaction
    {
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonProperty("txnId")]
        public string TransactionId { get; set; } = string.Empty;
    }
}
