using System.Collections.Generic;
using WhiteBit.Net.Objects.Models.Spot;

namespace WhiteBit.Net.Objects
{
    /// <summary>
    /// WhiteBit response
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public class WhiteBitResponse<T>
    {
        /// <summary>
        /// Data result
        /// </summary>
        public T Result { get; set; } = default!;
        /// <summary>
        /// Rate limit info
        /// </summary>
        public IEnumerable<WhiteBitCurrentRateLimit> Ratelimits { get; set; } = new List<WhiteBitCurrentRateLimit>();
    }
}
