using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Futures
{
    /// <summary>
    /// A filter for order placed on a symbol. Can be either a <see cref="WhiteBitSymbolPriceFilter"/>, <see cref="WhiteBitSymbolLotSizeFilter"/>, <see cref="WhiteBitSymbolMaxAlgorithmicOrdersFilter"/>
    /// </summary>
    [JsonConverter(typeof(SymbolFuturesFilterConverter))]
    public class WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    public class WhiteBitSymbolPriceFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The minimal price the order can be for
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// The max price the order can be for
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// The tick size of the price. The price can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal TickSize { get; set; }
    }

    /// <summary>
    /// Lot size filter
    /// </summary>
    public class WhiteBitSymbolLotSizeFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    /// Market lot size filter
    /// </summary>
    public class WhiteBitSymbolMarketLotSizeFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    ///Max orders filter
    /// </summary>
    public class WhiteBitSymbolMaxOrdersFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The max number of orders for this symbol
        /// </summary>
        public int MaxNumberOrders { get; set; }
    }

    /// <summary>
    /// Max algo orders filter
    /// </summary>
    public class WhiteBitSymbolMaxAlgorithmicOrdersFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The max number of Algorithmic orders for this symbol
        /// </summary>
        public int MaxNumberAlgorithmicOrders { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    public class WhiteBitSymbolPercentPriceFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The max factor the price can deviate up
        /// </summary>
        public decimal MultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate down
        /// </summary>
        public decimal MultiplierDown { get; set; }
        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int MultiplierDecimal { get; set; }
    }

    /// <summary>
    /// Min notional filter
    /// </summary>
    public class WhiteBitSymbolMinNotionalFilter : WhiteBitFuturesSymbolFilter
    {
        /// <summary>
        /// The minimal total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }
    }
}
