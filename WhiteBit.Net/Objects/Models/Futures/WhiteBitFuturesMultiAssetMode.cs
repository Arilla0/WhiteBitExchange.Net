using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// Multi asset mode info
    /// </summary>
    public class WhiteBitFuturesMultiAssetMode
    {
        /// <summary>
        /// Is multi assets mode enabled
        /// </summary>
        [JsonProperty("multiAssetsMargin")]
        public bool MultiAssetMode { get; set; }
    }
}
