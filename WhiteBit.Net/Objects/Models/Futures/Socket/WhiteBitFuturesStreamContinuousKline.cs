using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using WhiteBit.Net.Interfaces;
using WhiteBit.Net.Objects.Models.Spot.Socket;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wrapper for continuous kline information for a symbol
    /// </summary>
    public class WhiteBitStreamContinuousKlineData: WhiteBitStreamEvent, IWhiteBitStreamKlineData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonProperty("ps")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The contract type
        /// </summary>
        [JsonProperty("ct")]
        public ContractType ContractType { get; set; } = ContractType.Unknown;

        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("k")]
        [JsonConverter(typeof(InterfaceConverter<WhiteBitStreamKline>))]
        public IWhiteBitStreamKline Data { get; set; } = default!;
    }
}
