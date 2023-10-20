using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models
{
    /// <summary>
    /// Represents the WhiteBit result for combined data on a single socket connection
    /// See on https://github.com/WhiteBit-exchange/WhiteBit-official-api-docs/blob/master/web-socket-streams.md
    /// Combined streams
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WhiteBitCombinedStream<T>
    {
        /// <summary>
        /// The stream combined
        /// </summary>
        [JsonProperty("stream")]
        public string Stream { get; set; } = string.Empty;

        /// <summary>
        /// The data of stream
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }
}
