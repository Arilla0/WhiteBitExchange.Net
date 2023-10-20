using WhiteBit.Net.Enums;
using CryptoExchange.Net.Objects.Options;
using System;

namespace WhiteBit.Net.Objects.Options
{
    /// <summary>
    /// Options for WhiteBit Socket API
    /// </summary>
    public class WhiteBitSocketApiOptions : SocketApiOptions
    {
        /// <summary>
        /// Whether to check the trade rules when placing new orders and what to do if the trade isn't valid
        /// </summary>
        public TradeRulesBehaviour TradeRulesBehaviour { get; set; } = TradeRulesBehaviour.None;

        /// <summary>
        /// How often the trade rules should be updated. Only used when TradeRulesBehaviour is not None
        /// </summary>
        public TimeSpan TradeRulesUpdateInterval { get; set; } = TimeSpan.FromMinutes(60);

        internal WhiteBitSocketApiOptions Copy()
        {
            var result = Copy<WhiteBitSocketApiOptions>();
            result.TradeRulesBehaviour = TradeRulesBehaviour;
            result.TradeRulesUpdateInterval = TradeRulesUpdateInterval;
            return result;
        }
    }
}
