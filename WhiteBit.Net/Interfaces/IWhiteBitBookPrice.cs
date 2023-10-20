namespace WhiteBit.Net.Interfaces
{
    /// <summary>
    /// Book tick
    /// </summary>
    public interface IWhiteBitBookPrice
    {
        /// <summary>
        /// The symbol
        /// </summary>
        string Symbol { get; set; }
        /// <summary>
        /// Price of the best bid
        /// </summary>
        decimal BestBidPrice { get; set; }
        /// <summary>
        /// Quantity of the best bid
        /// </summary>
        decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Price of the best ask
        /// </summary>
        decimal BestAskPrice { get; set; }
        /// <summary>
        /// Quantity of the best ask
        /// </summary>
        decimal BestAskQuantity { get; set; }
    }
}
