using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Result
    /// </summary>
    public class WhiteBitBrokerageTransferResult
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("txnId")]
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// Client Transfer Id
        /// </summary>
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
    }
}