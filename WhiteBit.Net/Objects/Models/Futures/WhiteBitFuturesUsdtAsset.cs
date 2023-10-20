namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Asset info
    /// </summary>
    public class WhiteBitFuturesUsdtAsset
    {
        /// <summary>
        /// Name of the asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        public bool MarginAvailable { get; set; }
        /// <summary>
        /// Auto-exchange threshold in Multi-Assets margin mode
        /// </summary>
        public decimal? AutoAssetExchange { get; set; }
    }
}
