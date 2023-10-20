using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot.SubAccountData
{
    internal class WhiteBitSubAccountAsset
    {
        public bool Success { get; set; } = true;
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
        public IEnumerable<WhiteBitBalance> Balances { get; set; } = Array.Empty<WhiteBitBalance>();
    }
}
