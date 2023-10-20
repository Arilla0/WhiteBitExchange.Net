using System;
using System.Collections.Generic;
using System.Linq;
using WhiteBit.Net.Converters;
using WhiteBit.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public class WhiteBitSymbol
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonConverter(typeof(SymbolStatusConverter))]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonProperty("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }

        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(SpotOrderTypeConverter))]
        public IEnumerable<SpotOrderType> OrderTypes { get; set; } = Array.Empty<SpotOrderType>();
        /// <summary>
        /// Ice berg orders allowed
        /// </summary>
        public bool IceBergAllowed { get; set; }
        /// <summary>
        /// Cancel replace allowed
        /// </summary>
        public bool CancelReplaceAllowed { get; set; }
        /// <summary>
        /// Spot trading orders allowed
        /// </summary>
        public bool IsSpotTradingAllowed { get; set; }
        /// <summary>
        /// Trailling stop orders are allowed
        /// </summary>
        public bool AllowTrailingStop { get; set; }
        /// <summary>
        /// Margin trading orders allowed
        /// </summary>
        public bool IsMarginTradingAllowed { get; set; }
        /// <summary>
        /// If OCO(One Cancels Other) orders are allowed
        /// </summary>
        public bool OCOAllowed { get; set; }
        /// <summary>
        /// Whether or not it is allowed to specify the quantity of a market order in the quote asset
        /// </summary>
        [JsonProperty("quoteOrderQtyMarketAllowed")]
        public bool QuoteOrderQuantityMarketAllowed { get; set; }
        /// <summary>
        /// The precision of the base asset fee
        /// </summary>
        [JsonProperty("baseCommissionPrecision")]
        public int BaseFeePrecision { get; set; }
        /// <summary>
        /// The precision of the quote asset fee
        /// </summary>
        [JsonProperty("quoteCommissionPrecision")]
        public int QuoteFeePrecision { get; set; }
        /// <summary>
        /// Permissions types
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(EnumConverter))]
        public IEnumerable<AccountType> Permissions { get; set; } = Array.Empty<AccountType>();
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        public IEnumerable<WhiteBitSymbolFilter> Filters { get; set; } = Array.Empty<WhiteBitSymbolFilter>();
        /// <summary>
        /// Default self trade prevention
        /// </summary>
        [JsonProperty("defaultSelfTradePreventionMode")]
        [JsonConverter(typeof(EnumConverter))]
        public SelfTradePreventionMode DefaultSelfTradePreventionMode { get; set; }
        /// <summary>
        /// Allowed self trade prevention modes
        /// </summary>
        [JsonProperty("allowedSelfTradePreventionModes", ItemConverterType = typeof(EnumConverter))]
        public IEnumerable<SelfTradePreventionMode> AllowedSelfTradePreventionModes { get; set; } = Array.Empty<SelfTradePreventionMode>();
        /// <summary>
        /// Filter for max amount of iceberg parts for this symbol
        /// </summary>
        [JsonIgnore]        
        public WhiteBitSymbolIcebergPartsFilter? IceBergPartsFilter => Filters.OfType<WhiteBitSymbolIcebergPartsFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolLotSizeFilter? LotSizeFilter => Filters.OfType<WhiteBitSymbolLotSizeFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol, specifically for market orders
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolMarketLotSizeFilter? MarketLotSizeFilter => Filters.OfType<WhiteBitSymbolMarketLotSizeFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolMaxOrdersFilter? MaxOrdersFilter => Filters.OfType<WhiteBitSymbolMaxOrdersFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max algorithmic orders for this symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolMaxAlgorithmicOrdersFilter? MaxAlgorithmicOrdersFilter => Filters.OfType<WhiteBitSymbolMaxAlgorithmicOrdersFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the minimal quote quantity of an order for this symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolMinNotionalFilter? MinNotionalFilter => Filters.OfType<WhiteBitSymbolMinNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the minimal quote quantity of an order for this symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolNotionalFilter? NotionalFilter => Filters.OfType<WhiteBitSymbolNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolPriceFilter? PriceFilter => Filters.OfType<WhiteBitSymbolPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolPercentPriceFilter? PricePercentFilter => Filters.OfType<WhiteBitSymbolPercentPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the maximum deviation of the price per side
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolPercentPriceBySideFilter? PricePercentByPriceFilter => Filters.OfType<WhiteBitSymbolPercentPriceBySideFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the maximum position on a symbol
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolMaxPositionFilter? MaxPositionFilter => Filters.OfType<WhiteBitSymbolMaxPositionFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the trailing delta values
        /// </summary>
        [JsonIgnore]
        public WhiteBitSymbolTrailingDeltaFilter? TrailingDeltaFilter => Filters.OfType<WhiteBitSymbolTrailingDeltaFilter>().FirstOrDefault();
    }
}
