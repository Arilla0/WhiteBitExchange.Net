using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Information about margin account
    /// </summary>
    public class WhiteBitMarginAccount
    {
        /// <summary>
        /// Boolean indicating if this account can borrow
        /// </summary>
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// Boolean indicating if this account can transfer
        /// </summary>
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// Aggregate level of margin
        /// </summary>
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Aggregate total balance as BTC
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Aggregate total liability balance of BTC
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Aggregate total available net balance of BTC
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Balance list
        /// </summary>
        [JsonProperty("userAssets")]
        public IEnumerable<WhiteBitMarginBalance> Balances { get; set; } = Array.Empty<WhiteBitMarginBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class WhiteBitMarginBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that was borrowed
        /// </summary>
        public decimal Borrowed { get; set; }
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Fee to need pay by borrowed
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// The quantity that is netAsset
        /// </summary>
        public decimal NetAsset { get; set; }
        /// <summary>
        /// The total balance of this asset (Available + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
