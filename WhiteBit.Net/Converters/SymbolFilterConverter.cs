using WhiteBit.Net.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using WhiteBit.Net.Objects.Models.Spot;

namespace WhiteBit.Net.Converters
{
    internal class SymbolFilterConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
#pragma warning disable 8604, 8602
            var obj = JObject.Load(reader);
            var type = new SymbolFilterTypeConverter(false).ReadString(obj["filterType"].ToString());
            WhiteBitSymbolFilter result;
            switch (type)
            {
                case SymbolFilterType.LotSize:
                    result = new WhiteBitSymbolLotSizeFilter
                    {
                        MaxQuantity = (decimal)obj["maxQty"],
                        MinQuantity = (decimal)obj["minQty"],
                        StepSize = (decimal)obj["stepSize"]
                    };
                    break;
                case SymbolFilterType.MarketLotSize:
                    result = new WhiteBitSymbolMarketLotSizeFilter
                    {
                        MaxQuantity = (decimal)obj["maxQty"],
                        MinQuantity = (decimal)obj["minQty"],
                        StepSize = (decimal)obj["stepSize"]
                    };
                    break;
                case SymbolFilterType.MinNotional:
                    result = new WhiteBitSymbolMinNotionalFilter
                    {
                        MinNotional = (decimal)obj["minNotional"],
                        ApplyToMarketOrders = (bool)obj["applyToMarket"],
                        AveragePriceMinutes = (int)obj["avgPriceMins"]
                    };
                    break;
                case SymbolFilterType.Notional:
                    result = new WhiteBitSymbolNotionalFilter
                    {
                        MinNotional = (decimal)obj["minNotional"],
                        MaxNotional = (decimal)obj["maxNotional"],
                        ApplyMinToMarketOrders = (bool)obj["applyMinToMarket"],
                        ApplyMaxToMarketOrders = (bool)obj["applyMaxToMarket"],
                        AveragePriceMinutes = (int)obj["avgPriceMins"]
                    };
                    break;
                case SymbolFilterType.Price:
                    result = new WhiteBitSymbolPriceFilter
                    {
                        MaxPrice = (decimal)obj["maxPrice"],
                        MinPrice = (decimal)obj["minPrice"],
                        TickSize = (decimal)obj["tickSize"]
                    };
                    break;
                case SymbolFilterType.MaxNumberAlgorithmicOrders:
                    result = new WhiteBitSymbolMaxAlgorithmicOrdersFilter
                    {
                        MaxNumberAlgorithmicOrders = (int)obj["maxNumAlgoOrders"]
                    };
                    break;
                case SymbolFilterType.MaxNumberOrders:
                    result = new WhiteBitSymbolMaxOrdersFilter
                    {
                        MaxNumberOrders = (int)obj["maxNumOrders"]
                    };
                    break;

                case SymbolFilterType.IcebergParts:
                    result = new WhiteBitSymbolIcebergPartsFilter
                    {
                        Limit = (int)obj["limit"]
                    };
                    break;
                case SymbolFilterType.PricePercent:
                    result = new WhiteBitSymbolPercentPriceFilter
                    {
                        MultiplierUp = (decimal)obj["multiplierUp"],
                        MultiplierDown = (decimal)obj["multiplierDown"],
                        AveragePriceMinutes = (int)obj["avgPriceMins"]
                    };
                    break;
                case SymbolFilterType.MaxPosition:
                    result = new WhiteBitSymbolMaxPositionFilter
                    {
                        MaxPosition = obj.ContainsKey("maxPosition") ? (decimal)obj["maxPosition"] : 0
                    };
                    break;
                case SymbolFilterType.PercentagePriceBySide:
                    result = new WhiteBitSymbolPercentPriceBySideFilter
                    {
                        AskMultiplierUp = (decimal)obj["askMultiplierUp"],
                        AskMultiplierDown = (decimal)obj["askMultiplierDown"],
                        BidMultiplierUp = (decimal)obj["bidMultiplierUp"],
                        BidMultiplierDown = (decimal)obj["bidMultiplierDown"],
                        AveragePriceMinutes = (int)obj["avgPriceMins"]
                    };
                    break;
                case SymbolFilterType.TrailingDelta:
                    result = new WhiteBitSymbolTrailingDeltaFilter
                    {
                        MaxTrailingAboveDelta = (int)obj["maxTrailingAboveDelta"],
                        MaxTrailingBelowDelta = (int)obj["maxTrailingBelowDelta"],
                        MinTrailingAboveDelta = (int)obj["minTrailingAboveDelta"],
                        MinTrailingBelowDelta = (int)obj["minTrailingBelowDelta"],
                    };
                    break;
                case SymbolFilterType.IcebergOrders:
                    result = new WhiteBitMaxNumberOfIcebergOrdersFilter
                    {
                        MaxNumIcebergOrders = obj.ContainsKey("maxNumIcebergOrders") ? (int)obj["maxNumIcebergOrders"] : 0
                    };
                    break;
                default:
                    Trace.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} | Warning | Can't parse symbol filter of type: " + obj["filterType"]);
                    result = new WhiteBitSymbolFilter();
                    break;
            }
#pragma warning restore 8604
            result.FilterType = type;
            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var filter = (WhiteBitSymbolFilter)value!;
            writer.WriteStartObject();

            writer.WritePropertyName("filterType");
            writer.WriteValue(JsonConvert.SerializeObject(filter.FilterType, new SymbolFilterTypeConverter(false)));

            switch (filter.FilterType)
            {
                case SymbolFilterType.LotSize:
                    var lotSizeFilter = (WhiteBitSymbolLotSizeFilter)filter;
                    writer.WritePropertyName("maxQty");
                    writer.WriteValue(lotSizeFilter.MaxQuantity);
                    writer.WritePropertyName("minQty");
                    writer.WriteValue(lotSizeFilter.MinQuantity);
                    writer.WritePropertyName("stepSize");
                    writer.WriteValue(lotSizeFilter.StepSize);
                    break;
                case SymbolFilterType.MarketLotSize:
                    var marketLotSizeFilter = (WhiteBitSymbolMarketLotSizeFilter)filter;
                    writer.WritePropertyName("maxQty");
                    writer.WriteValue(marketLotSizeFilter.MaxQuantity);
                    writer.WritePropertyName("minQty");
                    writer.WriteValue(marketLotSizeFilter.MinQuantity);
                    writer.WritePropertyName("stepSize");
                    writer.WriteValue(marketLotSizeFilter.StepSize);
                    break;
                case SymbolFilterType.MinNotional:
                    var minNotionalFilter = (WhiteBitSymbolMinNotionalFilter)filter;
                    writer.WritePropertyName("minNotional");
                    writer.WriteValue(minNotionalFilter.MinNotional);
                    writer.WritePropertyName("applyToMarket");
                    writer.WriteValue(minNotionalFilter.ApplyToMarketOrders);
                    writer.WritePropertyName("avgPriceMins");
                    writer.WriteValue(minNotionalFilter.AveragePriceMinutes);
                    break;
                case SymbolFilterType.Price:
                    var priceFilter = (WhiteBitSymbolPriceFilter)filter;
                    writer.WritePropertyName("maxPrice");
                    writer.WriteValue(priceFilter.MaxPrice);
                    writer.WritePropertyName("minPrice");
                    writer.WriteValue(priceFilter.MinPrice);
                    writer.WritePropertyName("tickSize");
                    writer.WriteValue(priceFilter.TickSize);
                    break;
                case SymbolFilterType.MaxNumberAlgorithmicOrders:
                    var algoFilter = (WhiteBitSymbolMaxAlgorithmicOrdersFilter)filter;
                    writer.WritePropertyName("maxNumAlgoOrders");
                    writer.WriteValue(algoFilter.MaxNumberAlgorithmicOrders);
                    break;
                case SymbolFilterType.MaxPosition:
                    var maxPositionFilter = (WhiteBitSymbolMaxPositionFilter)filter;
                    writer.WritePropertyName("maxPosition");
                    writer.WriteValue(maxPositionFilter.MaxPosition);
                    break;
                case SymbolFilterType.MaxNumberOrders:
                    var orderFilter = (WhiteBitSymbolMaxOrdersFilter)filter;
                    writer.WritePropertyName("maxNumOrders");
                    writer.WriteValue(orderFilter.MaxNumberOrders);
                    break;
                case SymbolFilterType.IcebergParts:
                    var icebergPartsFilter = (WhiteBitSymbolIcebergPartsFilter)filter;
                    writer.WritePropertyName("limit");
                    writer.WriteValue(icebergPartsFilter.Limit);
                    break;
                case SymbolFilterType.PricePercent:
                    var pricePercentFilter = (WhiteBitSymbolPercentPriceFilter)filter;
                    writer.WritePropertyName("multiplierUp");
                    writer.WriteValue(pricePercentFilter.MultiplierUp);
                    writer.WritePropertyName("multiplierDown");
                    writer.WriteValue(pricePercentFilter.MultiplierDown);
                    writer.WritePropertyName("avgPriceMins");
                    writer.WriteValue(pricePercentFilter.AveragePriceMinutes);
                    break;
                case SymbolFilterType.TrailingDelta:
                    var TrailingDelta = (WhiteBitSymbolTrailingDeltaFilter)filter;
                    writer.WritePropertyName("maxTrailingAboveDelta");
                    writer.WriteValue(TrailingDelta.MaxTrailingAboveDelta);
                    writer.WritePropertyName("maxTrailingBelowDelta");
                    writer.WriteValue(TrailingDelta.MaxTrailingBelowDelta);
                    writer.WritePropertyName("minTrailingAboveDelta");
                    writer.WriteValue(TrailingDelta.MinTrailingAboveDelta);
                    writer.WritePropertyName("minTrailingBelowDelta");
                    writer.WriteValue(TrailingDelta.MinTrailingBelowDelta);
                    break;
                case SymbolFilterType.IcebergOrders:
                    var MaxNumIcebergOrders = (WhiteBitMaxNumberOfIcebergOrdersFilter)filter;
                    writer.WritePropertyName("maxNumIcebergOrders");
                    writer.WriteValue(MaxNumIcebergOrders.MaxNumIcebergOrders);                   
                    break;
                case SymbolFilterType.PercentagePriceBySide:
                    var pricePercentSideBySideFilter = (WhiteBitSymbolPercentPriceBySideFilter)filter;
                    writer.WritePropertyName("askMultiplierUp");
                    writer.WriteValue(pricePercentSideBySideFilter.AskMultiplierUp);
                    writer.WritePropertyName("askMultiplierDown");
                    writer.WriteValue(pricePercentSideBySideFilter.AskMultiplierDown);
                    writer.WritePropertyName("bidMultiplierUp");
                    writer.WriteValue(pricePercentSideBySideFilter.BidMultiplierUp);
                    writer.WritePropertyName("bidMultiplierDown");
                    writer.WriteValue(pricePercentSideBySideFilter.BidMultiplierDown);
                    writer.WritePropertyName("avgPriceMins");
                    writer.WriteValue(pricePercentSideBySideFilter.AveragePriceMinutes);
                    break;
                case SymbolFilterType.Notional:
                    var notionalFilter = (WhiteBitSymbolNotionalFilter)filter;
                    writer.WritePropertyName("minNotional");
                    writer.WriteValue(notionalFilter.MinNotional);
                    writer.WritePropertyName("maxNotional");
                    writer.WriteValue(notionalFilter.MaxNotional);
                    writer.WritePropertyName("applyMinToMarketOrders");
                    writer.WriteValue(notionalFilter.ApplyMinToMarketOrders);
                    writer.WritePropertyName("applyMaxToMarketOrders");
                    writer.WriteValue(notionalFilter.ApplyMaxToMarketOrders);
                    writer.WritePropertyName("avgPriceMins");
                    writer.WriteValue(notionalFilter.AveragePriceMinutes);
                    break;
                default:
                    Trace.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} | Warning | Can't write symbol filter of type: " + filter.FilterType);
                    break;
            }

            writer.WriteEndObject();
        }
    }
}
