using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace WhiteBit.Net.Converters
{
    internal class WhiteBitEarningTypeConverter : BaseConverter<WhiteBitEarningType>
    {
        public WhiteBitEarningTypeConverter() : this(true) { }
        public WhiteBitEarningTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<WhiteBitEarningType, string>> Mapping => new List<KeyValuePair<WhiteBitEarningType, string>>
        {
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.MiningWallet, "0"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.MergedMining, "1"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.ActivityBonus, "2"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.Rebate, "3"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.SmartPool, "4"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.MiningAddress, "5"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.IncomeTransfer, "6"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.PoolSavings, "7"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.Transfered, "8"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.IncomeTransfer, "31"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.HashrateResaleMiningWallet, "32"),
            new KeyValuePair<WhiteBitEarningType, string>(WhiteBitEarningType.HashrateResalePoolSavings, "33")
        };
    }
}
