using System;
using System.Collections.Generic;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    internal class WhiteBitSubAccountUniversalTransfersList
    {
        /// <summary>
        /// Transactions
        /// </summary>
        [JsonProperty("result")]
        public IEnumerable<WhiteBitSubAccountUniversalTransferTransaction> Transactions { get; set; } =
            new List<WhiteBitSubAccountUniversalTransferTransaction>();

    }

    /// <summary>
    /// WhiteBit sub account universal transaction
    /// </summary>
    public class WhiteBitSubAccountUniversalTransferTransaction
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// From email
        /// </summary>
        public string FromEmail { get; set; } = "";

        /// <summary>
        /// To email
        /// </summary>
        public string ToEmail { get; set; } = "";

        /// <summary>
        /// From account type
        /// </summary>
        [JsonConverter(typeof(TransferAccountTypeConverter))]
        public TransferAccountType FromAccountType { get; set; }

        /// <summary>
        /// To account type
        /// </summary>
        [JsonConverter(typeof(TransferAccountTypeConverter))]
        public TransferAccountType ToAccountType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The time the universal transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("createTimeStamp")]
        public DateTime CreateTime { get; set; }
    }
}
