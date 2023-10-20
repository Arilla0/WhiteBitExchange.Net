using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models
{
    /// <summary>
    /// Query result
    /// </summary>
    public class WhiteBitResult
    {
        /// <summary>
        /// Result code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Query result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class WhiteBitResult<T>: WhiteBitResult
    {
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; } = default!;
    }
}
