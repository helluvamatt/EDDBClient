using CsvHelper.Configuration;
using EDDB.Data.Converters;
using EDDB.Data.Model;

namespace EDDB.Data.Import.CsvMappings
{
	internal sealed class ListingMapping : CsvClassMap<Listing>
	{
		// Corresponds to v5 of the EDDB.io API dumps
		public ListingMapping()
		{
			Map(l => l.ID).Name("id");
			Map(l => l.StationID).Name("station_id");
			Map(l => l.CommodityID).Name("commodity_id");
			Map(l => l.Supply).Name("supply");
			Map(l => l.Demand).Name("demand");
			Map(l => l.BuyPrice).Name("buy_price");
			Map(l => l.SellPrice).Name("sell_price");
			Map(l => l.CollectedAt).Name("collected_at").TypeConverter<UnixTimestampConverter>();
		}
	}
}
