﻿using System.Collections.Generic;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;

namespace WhiteBit.Net.Converters
{
    internal class MarginLevelStatusConverter : BaseConverter<MarginLevelStatus>
    {
        public MarginLevelStatusConverter() : this(true) { }
        public MarginLevelStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<MarginLevelStatus, string>> Mapping => new List<KeyValuePair<MarginLevelStatus, string>>
        {
            new KeyValuePair<MarginLevelStatus, string>(MarginLevelStatus.Excessive, "EXCESSIVE"),
            new KeyValuePair<MarginLevelStatus, string>(MarginLevelStatus.Normal, "NORMAL"),
            new KeyValuePair<MarginLevelStatus, string>(MarginLevelStatus.MarginCall, "MARGIN_CALL"),
            new KeyValuePair<MarginLevelStatus, string>(MarginLevelStatus.PreLiquidation, "PRE_LIQUIDATION"),
            new KeyValuePair<MarginLevelStatus, string>(MarginLevelStatus.ForceLiquidation, "FORCE_LIQUIDATION")
        };
    }
}
