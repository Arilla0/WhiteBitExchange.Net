using System;
using System.Collections.Generic;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// WhiteBit pay trade
    /// </summary>
    public class WhiteBitPayTrade
    {
        /// <summary>
        /// Order type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public PayOrderType OrderType { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fund details
        /// </summary>
        [JsonProperty("fundsDetail")]
        public IEnumerable<WhiteBitPayTradeDetails> Details { get; set; } = Array.Empty<WhiteBitPayTradeDetails>();
        /// <summary>
        /// Payer info
        /// </summary>
        [JsonProperty("payerInfo")]
        public WhiteBitPayTradePayerInfo PayerInfo { get; set; } = new WhiteBitPayTradePayerInfo();
        /// <summary>
        /// Receiver info
        /// </summary>
        [JsonProperty("receiverInfo")]
        public WhiteBitPayTradeReceiverInfo ReceiverInfo { get; set; } = new WhiteBitPayTradeReceiverInfo();
    }

    /// <summary>
    /// Pay trade funds details
    /// </summary>
    public class WhiteBitPayTradeDetails
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }

    /// <summary>
    /// Pay trade payer info
    /// </summary>
    public class WhiteBitPayTradePayerInfo
    {
        /// <summary>
        /// Nickname or merchant name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Account type，USER for personal，MERCHANT for merchant
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// WhiteBit uid
        /// </summary>
        [JsonProperty("WhiteBitId")]
        public string WhiteBitId { get; set; } = string.Empty;
        /// <summary>
        /// WhiteBit pay id
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; } = string.Empty;
    }

    /// <summary>
    /// Pay trade receiver info
    /// </summary>
    public class WhiteBitPayTradeReceiverInfo
    {
        /// <summary>
        /// Nickname or merchant name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Account type，USER for personal，MERCHANT for merchant
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// WhiteBit uid
        /// </summary>
        [JsonProperty("WhiteBitId")]
        public string WhiteBitId { get; set; } = string.Empty;
        /// <summary>
        /// WhiteBit pay id
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// International area code
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; } = string.Empty;
        /// <summary>
        /// Phone number
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// Country code
        /// </summary>
        [JsonProperty("mobileCode")]
        public string MobileCode { get; set; } = string.Empty;
    }
}
