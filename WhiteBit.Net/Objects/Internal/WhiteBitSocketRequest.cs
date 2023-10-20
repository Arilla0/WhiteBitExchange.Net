using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Internal
{
    internal class WhiteBitSocketMessage
    {
        [JsonProperty("method")]
        public string Method { get; set; } = "";

        [JsonProperty("id")]
        public int Id { get; set; }
    }

    internal class WhiteBitSocketRequest : WhiteBitSocketMessage
    {
        [JsonProperty("params")]
        public string[] Params { get; set; } = Array.Empty<string>();
    }

    internal class WhiteBitSocketQuery : WhiteBitSocketMessage
    {
        [JsonProperty("params")]
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}
