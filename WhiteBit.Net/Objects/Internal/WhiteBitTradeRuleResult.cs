namespace WhiteBit.Net.Objects.Internal
{
    internal class WhiteBitTradeRuleResult
    {
        public bool Passed { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QuoteQuantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? StopPrice { get; set; }
        public string? ErrorMessage { get; set; }

        public static WhiteBitTradeRuleResult CreatePassed(decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice)
        {
            return new WhiteBitTradeRuleResult
            {
                Passed = true,
                Quantity = quantity,
                Price = price,
                StopPrice = stopPrice,
                QuoteQuantity = quoteQuantity
            };
        }

        public static WhiteBitTradeRuleResult CreateFailed(string message)
        {
            return new WhiteBitTradeRuleResult
            {
                Passed = false,
                ErrorMessage = message
            };
        }
    }
}
